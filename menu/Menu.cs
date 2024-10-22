using Godot;
using System;

public partial class Menu : Control
{
	private PackedScene worldScene = (PackedScene)ResourceLoader.Load("res://world/world.tscn");

	public void Host()
	{
		GD.Print("Hosting...");
		var networkManager = (NetworkManager)GetNode("/root/NetworkManager");
		networkManager.Host();
		GetTree().ChangeSceneToPacked(worldScene);
	}

	public void Join()
	{
		GD.Print("Joining...");
		var networkManager = (NetworkManager)GetNode("/root/NetworkManager");
		networkManager.Join();
		GetTree().ChangeSceneToPacked(worldScene);
	}
}
