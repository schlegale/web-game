using Godot;
using System;

public partial class NetworkManager : Node
{
	private const int SERVER_PORT = 8080;
	private const string SERVER_IP = "127.0.0.1";
	private ENetMultiplayerPeer enetNetworkPeer;


	private PackedScene _loadingScene = (PackedScene)ResourceLoader.Load("res://menu/loading.tscn");
	private Node _activeLoadingScene;

	public override void _Ready()
	{
		GetMultiplayer().Connect("peer_connected", new Callable(this, nameof(OnClientConnected)));
		GetMultiplayer().Connect("peer_disconnected", new Callable(this, nameof(OnClientDisconnected)));
	}


	public void Host()
	{
		GD.Print("Host!!");
		ShowLoading();
		CreateServerPeer();
		HideLoading();
	}

	public void Join()
	{
		GD.Print("Join!!");
		ShowLoading();
		CreateClientPeer();
		HideLoading();
	}

	public void ShowLoading()
	{
		GD.Print("Show loading");

		_activeLoadingScene = _loadingScene.Instantiate();
		AddChild(_activeLoadingScene);
	}

	public void HideLoading()
	{
		GD.Print("Hide loading");

		if (_activeLoadingScene != null)
		{
			RemoveChild(_activeLoadingScene);
			_activeLoadingScene.QueueFree();
			_activeLoadingScene = null;
		}
	}

	public void CreateServerPeer()
	{
		GD.Print("Creating server...");

		enetNetworkPeer = new ENetMultiplayerPeer();
		var result = enetNetworkPeer.CreateServer(SERVER_PORT);

		if (result == Error.Ok)
		{
			GD.Print("Server created successfully on port " + SERVER_PORT);
			GetMultiplayer().MultiplayerPeer = enetNetworkPeer;
		}
		else
		{
			GD.PrintErr("Failed to create server: " + result.ToString());
		}
	}

	public void CreateClientPeer()
	{
		GD.Print("Creating client...");
		enetNetworkPeer = new ENetMultiplayerPeer();
		var result = enetNetworkPeer.CreateClient(SERVER_IP, SERVER_PORT);

		if (result == Error.Ok)
		{
			GD.Print("Client connected to server at " + SERVER_IP + ":" + SERVER_PORT);
			GetMultiplayer().MultiplayerPeer = enetNetworkPeer;
		}
		else
		{
			GD.PrintErr("Failed to connect to server: " + result.ToString());
		}
	}

	public void OnClientConnected(int networkId)
	{
		GD.Print("Client connected: " + networkId);
	}

	public void OnClientDisconnected(int networkId)
	{
		GD.Print("Client disconnected: " + networkId);
	}

}
