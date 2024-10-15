using Godot;
using System;

public partial class Menu : Control
{
	private PackedScene worldScene = (PackedScene)ResourceLoader.Load("res://world/world.tscn");

	public void Host()
	{
		GD.Print("Hosting...");
		GetTree().ChangeSceneToPacked(worldScene);
	}

	public void Join()
	{
		GD.Print("Joining...");
	}
}
