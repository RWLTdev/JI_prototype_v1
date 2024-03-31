using Godot;
using System;

/* this script gets and holds the current values of the player's input controls
it collects what control type the player is using, and then pulls that control method's bind
settings from the player's saved settings file. It then holds those values on standby
*/


public partial class Auto_holdscurrentinputbinds : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
