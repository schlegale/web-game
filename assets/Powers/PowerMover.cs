using Godot;
using System;

public partial class PowerMover : MeshInstance3D
{
	private float speed = 20.0f;
	private float lifetime = 1.0f;
	private int damage;

	public override void _Ready()
	{
		GetTree().CreateTimer(lifetime).Timeout += () => QueueFree();
	}

	public override void _Process(double delta)
	{
		TranslateObjectLocal(Vector3.Forward * speed * (float)delta);
	}
}
