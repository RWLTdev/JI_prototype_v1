using Godot;
using System;

public partial class watchesforinputsSM : Node
{
		//what menu should currently be controlled
		public enum smControlMode
		{
			NoControl, MainScrollMenu, LoadSubMenu, ResumeSubMenu, SettingsSubMenu, QuitSubMenu
		}
		private smControlMode currentsmControlMode;
		/*These are the game controls. They use a static StringName when calling controls 
		or performance will tank*/
		static StringName upmoveString = new StringName("upmove");
		static StringName upselectString = new StringName("upselect");
		static StringName downmoveString = new StringName("downmove");
		static StringName downselectString = new StringName("downselect");
		static StringName leftmoveString = new StringName("leftmove");
		static StringName leftselectString = new StringName("leftselect");
		static StringName rightmoveString = new StringName("rightmove");
		static StringName rightselectString = new StringName("rightselect");
		static StringName interactString = new StringName("interact");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Disables user controls until CORE gives the OK
		currentsmControlMode = smControlMode.NoControl;
		SetProcessInput(true);
	}

	public void EnableControlMode(string newmode)
	{
		//Activates on CORE's Signal
		if (newmode == "ScrollMenu")
		{
			currentsmControlMode = smControlMode.MainScrollMenu;
			GD.Print("Main scroll menu controls enabled via <watchesforinputsSM>");
		}
		if (newmode == "LoadSubMenu")
		{
			currentsmControlMode = smControlMode.LoadSubMenu;
			GD.Print("Load save submenu controls enabled via <watchesforinputsSM>");
		}
	}

 	public override void _Input(InputEvent @event)
	{
		//THE BIGGOL INPUT SWITCHIN' STATEMENT
        switch (currentsmControlMode)
	    {
		case smControlMode.NoControl:
			//no normal inputs
			break;
		case smControlMode.MainScrollMenu:
			if (Input.IsActionJustPressed(upmoveString) || Input.IsActionJustPressed(upselectString))
			{				
				EmitSignal(SignalName.ScrollMenuUp);
				GD.Print("Player input: scroll menu up");		
			}
			if (Input.IsActionJustPressed(downmoveString) || Input.IsActionJustPressed(downselectString))
			{
				EmitSignal(SignalName.ScrollMenuDown);
				GD.Print("Player input: scroll menu down");		
			}
			if (Input.IsActionJustPressed(leftmoveString) || Input.IsActionJustPressed(leftselectString))		
			{
				EmitSignal(SignalName.ToggleThisSubmenu, "on");
				GD.Print("Player input: toggle the [ ] submenu");
			}	
			if (Input.IsActionJustPressed(interactString))
			{
				EmitSignal(SignalName.ToggleThisSubmenu, "on");
				GD.Print("Player input: toggle the [ ] submenu");
			}
			break;
		case smControlMode.LoadSubMenu:
			if (Input.IsActionJustPressed(rightmoveString) || Input.IsActionJustPressed(rightselectString))
			{		
				EmitSignal(SignalName.ToggleThisSubmenu, "off");
				GD.Print("Player input: toggle the [ ] submenu");
			}
			if (Input.IsActionJustPressed(interactString))
			{
				
				GD.Print("Player input: load save [  ]");
			}
			break;
    	case smControlMode.ResumeSubMenu:
			break;
    	case smControlMode.SettingsSubMenu:
			break;
    	case smControlMode.QuitSubMenu:
			break;
	   }	   	   	    								
    }

	//Disabled controls until x reports
	public void DisableControls(string ControlType = "Check if needed later")
	{
		currentsmControlMode = smControlMode.NoControl;
	}

	[Signal]
	public delegate void ScrollMenuUpEventHandler();
	[Signal]
	public delegate void ScrollMenuDownEventHandler();
	[Signal]
	public delegate void ToggleThisSubmenuEventHandler();
}
