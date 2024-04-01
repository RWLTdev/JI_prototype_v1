using Godot;
using System;

public partial class watchesforinputsSM : Node
{
		[Signal]
		public delegate void ScrollMainMenuUpEventHandler();
		[Signal]
		public delegate void ScrollMainMenuDownEventHandler();
		[Signal]
		public delegate void ToggleThisSubmenuEventHandler();
		
		//state for what menu should currently be controlled/what inputs are active
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

		//Connects each of the signals for the possible operations linked to the inputs (on each menu option... is there a better way to do this?)
		//load save menu button
		PathFollow2D Pathfollowload = GetNode<PathFollow2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuOptions/MenuOptionsPath/PathFollowLoad");
		Callable loadscrollupCall = new Callable(Pathfollowload, "ScrollUp");
		this.Connect("ScrollMainMenuUp", loadscrollupCall);
		Callable loadscrolldownCall = new Callable(Pathfollowload, "ScrollDown");
		this.Connect("ScrollMainMenuDown", loadscrolldownCall);
		//continue menu button
		PathFollow2D Pathfollowresume = GetNode<PathFollow2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuOptions/MenuOptionsPath/PathFollowResume");
		Callable resumescrollupCall = new Callable(Pathfollowresume, "ScrollUp");
		this.Connect("ScrollMainMenuUp", resumescrollupCall);
		Callable resumescrolldownCall = new Callable(Pathfollowresume, "ScrollDown");
		this.Connect("ScrollMainMenuDown", resumescrolldownCall);
		//settings menu button
		PathFollow2D Pathfollowsettings = GetNode<PathFollow2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuOptions/MenuOptionsPath/PathFollowSettings");
		Callable settingsscrollupCall = new Callable(Pathfollowsettings, "ScrollUp");
		this.Connect("ScrollMainMenuUp", settingsscrollupCall);
		Callable settingsscrolldownCall = new Callable(Pathfollowsettings, "ScrollDown");
		this.Connect("ScrollMainMenuDown", settingsscrolldownCall);
		//quit menu button
		PathFollow2D Pathfollowquit = GetNode<PathFollow2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuOptions/MenuOptionsPath/PathFollowQuit");
		Callable quitscrollupCall = new Callable(Pathfollowquit, "ScrollUp");
		this.Connect("ScrollMainMenuUp", quitscrollupCall);
		Callable quitscrolldownCall = new Callable(Pathfollowquit, "ScrollDown");
		this.Connect("ScrollMainMenuDown", quitscrolldownCall);

		//connects the toggle this submenu signal to the main menu options selector detector(the better version of the above)
		Area2D Mainmenuselector = GetNode<Area2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/MainMenuOptionSelectorDetector");
		Callable watchesforinputstomainmenuselectorCall = new Callable(Mainmenuselector, "ToggleSelectedSubmenu");
		this.Connect("ToggleThisSubmenu", watchesforinputstomainmenuselectorCall);
		//and also connects the submenu toggle feedback signal back to here
		Mainmenuselector.Connect("SwapInputWatcherControlMode", new Callable(this, nameof(SwapControlMode)));
	}

	public void SwapControlMode(string newmode)
	{
		//this one also activates on CORE's startup signal
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

	//THE BIGGOL INPUT SWITCHIN' STATEMENT	
 	public override void _Input(InputEvent @event)
	{
        switch (currentsmControlMode)
	    {
		case smControlMode.NoControl:
			//no normal inputs
			break;
		case smControlMode.MainScrollMenu:
			if (Input.IsActionJustPressed(upmoveString) || Input.IsActionJustPressed(upselectString))
			{				
				EmitSignal(SignalName.ScrollMainMenuUp);
				GD.Print("Player input: scroll menu up");		
			}
			if (Input.IsActionJustPressed(downmoveString) || Input.IsActionJustPressed(downselectString))
			{
				EmitSignal(SignalName.ScrollMainMenuDown);
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

}
