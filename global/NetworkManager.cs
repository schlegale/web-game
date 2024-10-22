using Godot;
using System;

public partial class NetworkManager : Node
{
	private const int SERVER_PORT = 8080;
	private const string SERVER_IP = "127.0.0.1";
	private ENetMultiplayerPeer enetNetworkPeer;

	private PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://player/player.tscn");

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
		AddPlayer(1);
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

	public void AddPlayer(int networkId)
	{
		GD.Print("Adding player: " + networkId);
		CharacterBody3D playerInstance = (CharacterBody3D)playerScene.Instantiate();
		playerInstance.Name = "Player_" + networkId;

		if (networkId == 1)
		{
			playerInstance.Position = new Vector3(-40, 0, 40); // First player
		}
		else
		{
			playerInstance.Position = new Vector3(-42, 0, 42); // Second player
		}
		AddChild(playerInstance);
	}

	public void RemovePlayer(int networkId)
	{
		GD.Print("Removing player: " + networkId);
		Node playerInstance = GetNodeOrNull("Player_" + networkId);
		if (playerInstance != null)
		{
			RemoveChild(playerInstance);
			playerInstance.QueueFree();
			GD.Print("Player " + networkId + " removed successfully.");
		}
		else
		{
			GD.PrintErr("Player " + networkId + " not found.");
		}

	}

	public void OnClientConnected(int networkId)
	{
		GD.Print("Client connected: " + networkId);
		AddPlayer(networkId);
	}

	public void OnClientDisconnected(int networkId)
	{
		GD.Print("Client disconnected: " + networkId);
		RemovePlayer(networkId);
	}

}
