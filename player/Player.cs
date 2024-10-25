using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public Camera3D Camera;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		Move(delta, ref velocity, Camera);
		Look();

		Velocity = velocity;
		MoveAndSlide();
	}
}
