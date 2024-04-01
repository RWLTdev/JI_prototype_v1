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
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void onCOREcall()
	{
		GD.Print("TurnhandlerSetups Teset");

		
	}

	public void WaitForBeatGO()
	{

	}
}
