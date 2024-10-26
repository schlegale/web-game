using System;
using Godot;

public partial class Player
{
    public void Look()
    {
        // Player rotation
        if (inputType == "mouse")
        {
            Vector2 mousePos = GetViewport().GetMousePosition();
            Vector3 rayOrigin = Camera.ProjectRayOrigin(mousePos);
            Vector3 rayDirection = Camera.ProjectRayNormal(mousePos);

            float playerY = GlobalTransform.Origin.Y;
            float intersection = (playerY - rayOrigin.Y) / rayDirection.Y;
            Vector3 mouseWorldPos = rayOrigin + rayDirection * intersection;

            lookDirection = mouseWorldPos - GlobalTransform.Origin;
            lookDirection.Y = 0;

            float targetAngle = Mathf.Atan2(-lookDirection.X, -lookDirection.Z);
            Body.Rotation = new Vector3(0, targetAngle, 0);
        }
        else if (direction != Vector3.Zero)
        {
            float targetAngle = Mathf.Atan2(-direction.X, -direction.Z);
            Body.Rotation = new Vector3(0, targetAngle, 0);
        }

        // Camera rotation
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
