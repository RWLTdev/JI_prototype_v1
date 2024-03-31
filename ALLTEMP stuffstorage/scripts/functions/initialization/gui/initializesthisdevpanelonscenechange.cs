using Godot;
using System;

public partial class initializesthisdevpanelonscenechange : Sprite2D
{
	public void DisableThisNodeOnLoad()
	{
		this.Visible = false;
		GD.Print("Hid the Dev Panel via <initializesthisdevpanelonscenechange>");
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DisableThisNodeOnLoad();
	}
}
