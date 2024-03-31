using Godot;
using System;

public partial class detectsmenuoptionnodesinareaSM : Area2D
{
	string submenuareaname;
	private void GetMenuOptAreaName(Area2D area)
	{
		submenuareaname = area.Name;

		if (submenuareaname == area.Name)
		{
			GD.Print(submenuareaname);
		}
	}
	public void ToggleSelectedSubmenu(string onoroff)
	{
		if (submenuareaname == "SelectDetectorLoad")
		{
			AnimatedSprite2D LoadSubmenu = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/SubmenuLoadBG");
			if (onoroff == "on")
			{
				if (LoadSubmenu.Visible != true)
				{
					LoadSubmenu.Visible = true;
				}
				EmitSignal(SignalName.change_inputwatcher_controlmode_on_interact, "LoadSubMenu");
			}
			if (onoroff == "off")
			{
				if (LoadSubmenu.Visible != false)
				{
					LoadSubmenu.Visible = false;
				}
				EmitSignal(SignalName.change_inputwatcher_controlmode_on_interact, "ScrollMenu");
			}
		}
		if (submenuareaname == "SelectDetectorResume")
		{

		}
		if (submenuareaname == "SelectDetectorSettings")
		{

		}
		if (submenuareaname == "SelectDetectorQuit")
		{

		}
	}

	[Signal]
	public delegate void change_inputwatcher_controlmode_on_interactEventHandler(string newmode);

}