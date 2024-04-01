using Godot;
using System;

public partial class managesplayergui : Node
{
	AnimatedSprite2D Screeneffects;
	AnimatedSprite2D Rhythmlock;
	AnimatedSprite2D RhythmlockDuplicateOver;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Screeneffects = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/ScreenEffects");
		Screeneffects.Visible = true; Screeneffects.Play("StaticBlackScreen");
		Rhythmlock = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/LockHUD/RhythmLock");
		Node2D Lockhud = GetNode<Node2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/LockHUD");
		RhythmlockDuplicateOver = Rhythmlock.Duplicate() as AnimatedSprite2D;
		Lockhud.AddChild(RhythmlockDuplicateOver);
		RhythmlockDuplicateOver.Visible = true;
		RhythmlockDuplicateOver.Animation = "StartBeats";
		Rhythmlock.Visible = false;
		RhythmlockDuplicateOver.Connect("animation_finished", new Callable(this, nameof(LockDupeDone)));
	}

	//plays the start animations on heartbeat's call during the startbeats phase and trashes the duplicate lock when done
	public void onStartBeats(string animationname)
	{
		if (animationname == "LockStartBeats")
		{
			RhythmlockDuplicateOver.Play();
		}
		else if (animationname == "StartLockThink")
		{
			Rhythmlock.Visible = true;
			Rhythmlock.Animation = "LockThinking";
		}
		else if (animationname == "StartTransitionBasic")
		{
			Screeneffects.Play("StartTransitionBasic");
		}
		else if (animationname == "RhythmLockDupeDone")
		{
			RhythmlockDuplicateOver.QueueFree();
		}
	}
	public void SetDupeLockSpeedScale(int bpm)
	{
		RhythmlockDuplicateOver.SpeedScale = 120f / bpm;
	}
	public void LockDupeDone()
	{
		onStartBeats("RhythmLockDupeDone");
	}
	

	//plays the lock animation every other beat upon recieving the beat signal from heartbeat
	private int onoffnum = 0;
	public void onHeartbeat()
	{
		onoffnum = 1 - onoffnum;
		if (onoffnum == 1)
			{
				Rhythmlock.Stop();
				Rhythmlock.Play();
			}
	}
}
