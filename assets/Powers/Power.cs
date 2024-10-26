using Godot;

[GlobalClass]
public partial class Power : Resource
{
    [Export] public PackedScene Model;
    [Export] public int Damage = 10;
    [Export] public float Cooldown = 1.0f;
}