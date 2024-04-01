using Godot;
using System;
using System.Threading.Tasks;
public partial class smCORE : Node
{
	//Custom Signals
	[Signal]
	public delegate void EnableMenuControlsEventHandler(string newmode);
	[Signal]
	public delegate void StartMusicEventHandler();

	public void onRootReady()
	{
		GD.Print("smCORE hears Root. Beginning startup processes.");
		STARTUPFUNCTION();
	}

	//STARTUP ROUTINE ON ROOT'S SIGNAL

		//vars for animations
		private Vector2 usePosition = new Vector2(1501.92f, 532.225f);
		private string menuoptionsNodepath = "/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuOptions";
	private async void STARTUPFUNCTION()
	{	
		//set main menu options path position offscreen
		Node2D MenuOptionsRoot = GetNode<Node2D>(menuoptionsNodepath);	
		MenuOptionsRoot.Position = new Vector2(1932.69f, 532.225f);
		
		//player startup bg animation
		AnimatedSprite2D Startmenubganim = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuBGAnim");
		Startmenubganim.Play("SMBGStartSwipe");

		//Then wait for signal, play menu options tween to use position
		Startmenubganim.Connect("animation_finished", new Callable(this, nameof(StartMenuBGAnimFinished)));
		await StartMenuBGAnimFinished();
		Startmenubganim.Pause();
		Tween scrollmenuoptionspan = GetTree().CreateTween();
        scrollmenuoptionspan.SetEase(Tween.EaseType.Out);
        scrollmenuoptionspan.TweenProperty(GetNode(menuoptionsNodepath), "position", usePosition, 0.6f).SetTrans(Tween.TransitionType.Back);

		//connect & signal input watcher and let it enable user control/switch to main scroll controlmode
		GD.Print("<smCORE> Scroll Menu all set. Handing basic control to <watchesforinputs>.");
		Node Watchingforinputs = GetNode("/root/Root3D/LogicParent/Watchers/WatchingForInputs");
		Callable COREtoinputwatcher = new Callable(Watchingforinputs, "SwapControlMode");
		this.Connect("EnableMenuControls", COREtoinputwatcher);
		EmitSignal(SignalName.EnableMenuControls, "ScrollMenu");

		//if reviewing code integrity here,
		//Check the input watcher
		//Check the menu option selector (Note to self: merge the menu size functions and menu select/activate submenu functions into one script eventually)
		//PS: its the menu selector so connecting signals isnt a pain in the ass thanks

		//connect & signal the audio manager to start playing "music"
		Node Audiomanager = GetNode("/root/Root3D/LogicParent/GameLogic/AudioManager");
		Callable COREtoaudiomanager = new Callable(Audiomanager, "onCOREsignal");
		this.Connect("StartMusic", COREtoaudiomanager);
		EmitSignal(SignalName.StartMusic);	
	}

	public async Task StartMenuBGAnimFinished()
	{
		//the main startup function has an await waiting on the swipe animation to finish
		Node Startmenubganim = GetNode("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuBGAnim");
		await ToSignal(Startmenubganim, "animation_finished");
		GD.Print("<smCORE> Start Animation Done! CORE STARTFUNCTION continuing.");
	}
}
