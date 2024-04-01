using Godot;
using System;

public partial class rootreadyF : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("RootF recognizes the fight scene is loaded. Waking up fCORE- It's Showtime!!");
		
		Node CORE = GetNode("/root/Root3D/LogicParent/GameLogic/CORE");
		CORE.Call("COREstartup");
	}
}
