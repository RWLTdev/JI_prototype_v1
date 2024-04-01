using Godot;
using System;

public partial class detectsmenuoptionnodesinareaSM : Area2D
{
	[Signal]
	public delegate void SwapInputWatcherControlModeEventHandler(string newmode);
	string submenuareaname;

	//on ready connect this nodes area2d detection signal
	public override void _Ready()
	{
		this.Connect("area_entered", new Callable(this, nameof(GetMenuOptionAreaName)));
	}
	
	//called when the main menu selector detector detects an new menu option area2d and sets its name to submenuareaname string
	private void GetMenuOptionAreaName(Area2D area)
	{
		submenuareaname = area.Name;

		/*if (submenuareaname == area.Name)
		{
			GD.Print(submenuareaname);
		}*/
	}

	// Control switch case that decides what submenu is turned on/off based on which main scroll menu option is interacted with
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
				EmitSignal(SignalName.SwapInputWatcherControlMode, "LoadSubMenu");
			}
			if (onoroff == "off")
			{
				if (LoadSubmenu.Visible != false)
				{
					LoadSubmenu.Visible = false;
				}
				EmitSignal(SignalName.SwapInputWatcherControlMode, "ScrollMenu");
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

}