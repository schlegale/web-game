using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public Profile profile;
	[Export] public string inputType = "mouse";
	[Export] public Camera3D Camera;
	[Export] public Node3D Head;
	[Export] public Node3D Body;

	private RayCast3D rayCast;
	public Vector3 direction;
	Vector3 lookDirection;
	public Vector2 mouseDelta;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		Move(delta, ref velocity, Camera);
		Look();
		Attack();

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseEvent)
		{
			mouseDelta = mouseEvent.Relative;
		}
	}
}
