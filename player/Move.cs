using Godot;

public partial class Player
{
	[Export] public float acceleration = 0.1f;
	[Export] public float deceleration = 0.25f;
	[Export] public float speed = 5.0f;
	[Export] public float sprintingSpeed = 10.0f;

	private float currentSpeed;

	public void Move(double delta, ref Vector3 velocity, Camera3D camera)
	{
		Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
		Vector3 forward = camera.GlobalTransform.Basis.Z; // Forward direction (Z is inverted in Godot)
		Vector3 right = camera.GlobalTransform.Basis.X; // Right direction

		// Calculate movement direction relative to the camera's orientation
		Vector3 direction = (right * inputDir.X + forward * inputDir.Y).Normalized();
		// Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		currentSpeed = speed;

		if (direction != Vector3.Zero)
		{
			velocity.X = Mathf.Lerp(velocity.X, direction.X * currentSpeed, acceleration);
			velocity.Z = Mathf.Lerp(velocity.Z, direction.Z * currentSpeed, acceleration);
		}
		else
		{
			velocity.X = Mathf.Lerp(velocity.X, 0, deceleration);
			velocity.Z = Mathf.Lerp(velocity.Z, 0, deceleration);
		}
	}
}
