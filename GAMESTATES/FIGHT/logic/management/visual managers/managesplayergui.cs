using Godot;
using System;

public partial class managesplayergui : Node
{
	AnimatedSprite2D Screeneffects;
	AnimatedSprite2D Rhythmlock;
	AnimatedSprite2D RhythmlockDuplicateOver;
	Path2D RhythmbarL;   Path2D RhythmbarR;

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

		RhythmbarL = GetNode<Path2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/LockHUD/RhythmBar/BarPathL");
		RhythmbarR = GetNode<Path2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/LockHUD/RhythmBar/BarPathR");
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
	PackedScene whiterhythmbarPckd = (PackedScene)GD.Load("res://GAMEASSETS/In Fight/GUI/packed game objects/Pgo_WhiteRhythmBar.tscn");
	PackedScene greyrhythmbarPckd = (PackedScene)GD.Load("res://GAMEASSETS/In Fight/GUI/packed game objects/Pgo_GreyRhythmBar.tscn");
	public void onHeartbeat(string notetype)
	{
		onoffnum = 1 - onoffnum;
		if (onoffnum == 1)
			{
				Rhythmlock.Stop();
				Rhythmlock.Play();
			}
		PathFollow2D RbarpathL = null;
		PathFollow2D RbarpathR = null;
		switch (notetype)
		{
			case "greyed":
			RbarpathL = (PathFollow2D)greyrhythmbarPckd.Instantiate();
			RbarpathR = (PathFollow2D)greyrhythmbarPckd.Instantiate();	break;
			case "white":
			RbarpathL = (PathFollow2D)whiterhythmbarPckd.Instantiate();
			RbarpathR = (PathFollow2D)whiterhythmbarPckd.Instantiate();	break;
			case "blacK":
			break;
		}
		RhythmbarL.AddChild(RbarpathL);
		RhythmbarR.AddChild(RbarpathR);
		
	}

	public void onStartBeatsDone()
	{
		Node Heart = GetNode("/root/Root3D/LogicParent/GameLogic/DJ/Heart");
		Heart.Connect("Beat", new Callable(this, nameof(onHeartbeat)));
	}
}
