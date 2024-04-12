using Godot;
using System;

public partial class turnhandlerF : Node
{

	private int ROUNDCOUNT = 0;

	private enum TurnState
	{
		PlayerTurn, EnemyTurn, Pause
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Inputwatcher = GetNode("/root/Root3D/LogicParent/Watchers/WatchingForInputs");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	Node Inputwatcher;
	public void onCOREcall()
	{
		GD.Print("TurnhandlerSetups Teset");
		Inputwatcher.Call("EnablefControlMode", "PlayerTurn");
		
	}

	public void WaitForBeatGO()
	{

	}
}
