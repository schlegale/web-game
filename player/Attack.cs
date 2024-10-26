using Godot;

public partial class Player
{
	[Export] public int damage = 250;

	public void Attack()
	{
		if (Input.IsActionJustPressed("attack_1"))
		{
			GD.Print("Attack 1");
			var power = profile.Powers[0];
			if (power?.Model == null)
			{
				GD.Print("Power model is not set.");
				return;
			}

			Node3D powerInstance = power.Model.Instantiate<Node3D>();
			if (powerInstance != null)
			{
				powerInstance.GlobalTransform = GlobalTransform;
				GetParent().AddChild(powerInstance);
				powerInstance.LookAt(GlobalTransform.Origin + lookDirection, Vector3.Up);
			}
		}

	}
}
