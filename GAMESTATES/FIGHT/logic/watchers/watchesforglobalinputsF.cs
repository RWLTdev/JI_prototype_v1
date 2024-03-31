using Godot;
using System;

public partial class watchesforglobalinputsF : Node
{
	
	//DevPanel toggle, 
	public void ToggleDevPanel()
	{
		Sprite2D DevpanelMenuBG = GetNode<Sprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/DevpanelMenuBG");

		if (DevpanelMenuBG.Visible == true)
		{
			DevpanelMenuBG.Visible = false;
		}
		else if (DevpanelMenuBG.Visible == false)
		{
			DevpanelMenuBG.Visible = true;
		}	
		GD.Print("Fight Dev Panel toggled via <watchesforglobalinputsF>");	
	}									
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("[global] toggle dev panel"))
		{
			ToggleDevPanel();
		}
	}

}