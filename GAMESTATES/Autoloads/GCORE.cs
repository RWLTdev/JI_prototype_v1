using Godot;
using System;

public partial class GCORE : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("GCORE operational.");
	}

	//Player's savedata
	public bool? saveopen = null;
	public int secssincelastsave = 0;

	//Current gamestate
	public string gamestate = "null";

	//Player's current location in the game world
	public string currentjaillocation = "none";
	public string currentlocalarea = "none";

	
}
