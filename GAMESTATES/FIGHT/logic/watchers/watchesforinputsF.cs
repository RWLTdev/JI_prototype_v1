using Godot;
using System;

public partial class watchesforinputsF : Node
{
		/*These are the main control modes. Since some states within the larger player and enemy turn dynamic
		can and can't be overridden by the SHIFT move state, (and most importantly NoControl), there need to
		be multiple submovestates underneath the higher ones to dictate when the player is and isn't allowed
		to freely move their characters*/
		public enum fControlMode
		{
			NoControl, PlayerTurn, EnemyTurn
		}

		public fControlMode currentfControlMode;

		public enum fSubControlMode
		{
			PlayerMain, PlayerFight, PlayerIdle, PlayerGear, PlayerParty, PlayerRun, 
			EnemyMain, HazardIncoming,
		}

		public fSubControlMode currentfSubControlMode;

		public enum MoveOverride { On, Off }
		public MoveOverride moveoverridei = MoveOverride.Off;

		private enum MoveDirection { up, down, left, right }
		private MoveDirection movedirectioni;

	
		
		
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
		static StringName movetoggleString = new StringName("MoverOverride");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Disables user controls until CORE gives the OK
		currentfControlMode = fControlMode.NoControl;
	}

	public void ConnectSignalstoInstances()
	{

	}

	//Disabled controls until x reports
	public void DisableControls(string ControlType = "Check if needed later")
	{
		currentfControlMode = fControlMode.NoControl;
	}

	public void EnablefControlMode(string newmode)
	{
		//Activates on CORE's Signal
		if (newmode == "PlayerTurn")
		{
			currentfControlMode = fControlMode.PlayerTurn;
			GD.Print("Main Act menu controls enabled via <watchesforinputsSM>");
		}
		if (newmode == "LoadSubMenu")
		{
			currentfControlMode = fControlMode.NoControl;
			GD.Print("Load save submenu controls enabled via <watchesforinputsSM>");
		}
	}

	//Inmates only not selectable in player submenus
	[Signal] public delegate void SelectInmateEventHandler(string atplace, bool onoff);


    public override void _Input(InputEvent @event)
    {
		//THE BIGGOL INPUT SWITCHIN' STATEMENT
        switch (currentfControlMode)
	    {
		case fControlMode.NoControl:
			//no normal inputs
			break;
		case fControlMode.PlayerTurn:
			//OVERRIDE ACTIONS, also maybe add another switch case for things that can be overridden
			if (@event is InputEventAction m && m.Action == movetoggleString)
			{
				if(Input.IsActionJustPressed(movetoggleString))
				{
					EmitSignal(SignalName.MoveOverrideTrigger);
					moveoverridei = MoveOverride.On;
					return;
				}
				if(Input.IsActionJustReleased(movetoggleString))
				{
					EmitSignal(SignalName.MoveOverrideTrigger);
					moveoverridei = MoveOverride.Off;
					return;
				}
			}
			if (@event is InputEventAction up && up.Action == upselectString)
			{
				GD.Print("inputtest");
				if(Input.IsActionJustPressed(upselectString))
				{
					//indicators on
					EmitSignal(SignalName.SelectInmate, "up", true);
					return;
				}
				if(Input.IsActionJustReleased(upselectString))
				{
					//indicators off
					EmitSignal(SignalName.SelectInmate, "up", false);
					return;
				}
			}
			if (@event is InputEventAction down && down.Action == downselectString)
			{
				if(Input.IsActionJustPressed(downselectString))
				{
					//indicators on
					EmitSignal(SignalName.SelectInmate, "down", true);
					return;
				}
				if(Input.IsActionJustReleased(downselectString))
				{
					//indicators off
					EmitSignal(SignalName.SelectInmate, "down", false);
					return;
				}
			}
			if (@event is InputEventAction left && left.Action == leftselectString)
			{
				if(Input.IsActionJustPressed(leftselectString))
				{
					//indicators on
					EmitSignal(SignalName.SelectInmate, "left", true);
					return;
				}
				if(Input.IsActionJustReleased(leftselectString))
				{
					//indicators off
					EmitSignal(SignalName.SelectInmate, "left", false);
					return;
				}
			}
			if (@event is InputEventAction right && right.Action == rightselectString)
			{
				if(Input.IsActionJustPressed(rightselectString))
				{
					//indicators on
					EmitSignal(SignalName.SelectInmate, "right", true);
					return;
				}
				if(Input.IsActionJustReleased(rightselectString))
				{
					//indicators off
					EmitSignal(SignalName.SelectInmate, "right", false);
					return;
				}
			}
			if (moveoverridei == MoveOverride.On)
				if(Input.IsActionJustPressed(upmoveString))
				{		
					//move selected up						
					EmitSignal(SignalName.MoveInmate, "Up");
					return;									
				}
				if(Input.IsActionJustPressed(downmoveString))
				{		
					//move selected up						
					EmitSignal(SignalName.MoveInmate, "Down");
					return;									
				}
				if(Input.IsActionJustPressed(leftmoveString))
				{		
					//move selected up						
					EmitSignal(SignalName.MoveInmate, "Left");
					return;										
				}
				if(Input.IsActionJustPressed(rightmoveString))
				{		
					//move selected up						
					EmitSignal(SignalName.MoveInmate, "Right");
					return;									
				}
				//Add abilities and gimmcks stuff here
			if (moveoverridei == MoveOverride.Off)
				{
					switch (currentfSubControlMode)
					{
						case fSubControlMode.PlayerMain:
							if(Input.IsActionJustPressed(interactString))
							{

							}
							break;
					}
				}
			break;
		case fControlMode.EnemyTurn:
			
				break;
		}
	}		
		

				   	
		   	   	    								
    
	//MoverOverride Movement Signals


	//Move Cam in Thinking + If its down zoom out and show all spirit clocks
	[Signal]
	public delegate void ShiftCameraEventHandler(string direction);

	//Trigger Move Override- available even in submenus
	[Signal]
	public delegate void MoveOverrideTriggerEventHandler();

	//When Move Override is on only
	[Signal]
	public delegate void MoveInmateEventHandler(string direction);

	

	//Player Menus
	[Signal]
	public delegate void SelectThisEventHandler();
	[Signal]
	public delegate void MoveMenuSelectorEventHandler(string direction);
    
	
}
