using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class Profile : Resource
{
    [Export] public int Health = 100;
    [Export] public PackedScene PlayerModel;
    [Export] public Array<Power> Powers = new Array<Power>();
}
