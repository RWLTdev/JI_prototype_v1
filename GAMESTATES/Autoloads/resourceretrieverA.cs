using Godot;
using System;

public partial class resourceretrieverA : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("<constantvalretrieverA> operational!");
	}

	public void TEMPonfCOREinitialize()
	{
		//TEMP remove this when more areas are added
		onAreaChangeGetAreaConstants("TestArea");
	}

	//sets the area constants holder nodes script to the current area (meant to be used on area change)
	public void onAreaChangeGetAreaConstants(string newareaID)
	{
		Node Areaconstantsholder = GetNode("/root/Root3D/LogicParent/DataHolders/HoldingAreaConstantValues");
		switch (newareaID)
		{
			case "TestArea":
				Script areaconstants = (Script)GD.Load("res://GAMEDATA/attachables/constant area values/constantareavalsinTestArea.cs");
				Areaconstantsholder.SetScript(areaconstants);
				break;
			default:
				return;
		}
	}
}
