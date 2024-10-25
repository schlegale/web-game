using Godot;

public partial class Player
{
    [Export] public Node3D Head;

    public void Look()
    {
        if (Input.IsActionJustPressed("rotate_right"))
        {
            float radians = Mathf.DegToRad(90);
            Head.RotateY(radians);
        }
        else if (Input.IsActionJustPressed("rotate_left"))
        {
            float radians = Mathf.DegToRad(-90);
            Head.RotateY(radians);
        }
    }
}
