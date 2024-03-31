using Godot;
using System;
using System.Threading.Tasks;
public partial class smCORE : Node
{
	//Custom Signals
	[Signal]
	public delegate void EnableControlsEventHandler(string newmode);
	[Signal]
	public delegate void QueueMusicEventHandler();

	public void onRootReady()
	{
		GD.Print("smCORE hears Root. Beginning startup processes.");
		STARTUPFUNCTION();
	}

	//STARTUP ROUTINE ON ROOT'S SIGNAL

		//vars for animations
		private Vector2 usePosition = new Vector2(1501.92f, 532.225f);
		private string menuoptionsnodepath = "/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuOptions";
	private async void STARTUPFUNCTION()
	{	
		//set menu options position offscreen
		Node2D MenuOptionsRoot = GetNode<Node2D>(menuoptionsnodepath);	
		MenuOptionsRoot.Position = new Vector2(1932.69f, 532.225f);
		
		//await ToSignal(GetTree().CreateTimer(1), "timeout");
		
		//player startup bg animation
		AnimatedSprite2D StartMenuBG = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuBGAnim");
		StartMenuBG.Play("SMBGStartSwipe");

		//Then wait for signal, play menu options tween to use position
		await StartMenuBGAnimFinished();
		StartMenuBG.Pause();
		Tween tween = GetTree().CreateTween();
        tween.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(GetNode(menuoptionsnodepath), "position", usePosition, 0.6f).SetTrans(Tween.TransitionType.Back);

		//let input watcher enable user control
		GD.Print("<smCORE> Scroll Menu all set. Handing basic control to <watchesforinputs>.");
		EmitSignal(SignalName.EnableControls, "ScrollMenu");

		//Signal starting music off to the audio manager
		EmitSignal(SignalName.QueueMusic);

	}

	public async Task StartMenuBGAnimFinished()
	{
		Node bganim = GetNode("/root/Root3D/GUI&CameraParent/CanvasLayer/2DElements/StartMenuBGAnim");
		await ToSignal(bganim, "animation_finished");
		GD.Print("<smCORE> Start Animation Done! CORE STARTFUNCTION continuing.");
	}
}
