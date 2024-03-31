using Godot;
using System;

public partial class Rez_Lgridnotchdata : Node
{
    [Export]
    public string gridside = "L";
    [Export]
    public int X;
    [Export]
    public int Y;
    [Export]
    public bool upborder;
    [Export]
    public bool downborder;
    [Export]
    public bool leftborder;
    [Export]
    public bool rightborder;

    public bool occupied = false;
    
}
