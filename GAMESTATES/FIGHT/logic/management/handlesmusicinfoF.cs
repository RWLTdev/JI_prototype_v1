using Godot;
using System;
using System.Collections.Generic;

public partial class handlesmusicinfoF : Node
{
	string trackfilepath; public float trackbpm;   float trackstarttrim;   float trackendtrim;

	//interface for the class types stored in the dict below, for storing playercurrentlocation values compared to constant
	
	public void returnAreaConstantValsTrack(string afilepath, float abpm, float astarttrim, float aendtrim)
	{
		trackfilepath = afilepath;
		trackbpm = abpm;
		trackstarttrim = astarttrim;
		trackendtrim = aendtrim;
	}
	public async void MusicStart()
	{
		GD.Print("Starting the music via <djhandlesmusicandtempo>");

		Node Holdingareaconstantvals = GetNode("/root/Root3D/LogicParent/DataHolders/HoldingAreaConstantValues");
		Holdingareaconstantvals.Call("onMusicPickerRequest", "pickrandomtrack");

		AudioStreamPlayer musicplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/MusicPlayer1");
		musicplayer1.Stream = GD.Load<AudioStream>(trackfilepath);
		AudioStreamPlayer guisfxplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/GUISFX1");
		guisfxplayer1.Stream = GD.Load<AudioStream>("res://ALLTEMP stuffstorage/assets/audio/HeartBeatEchoSFX.wav");
		//guisfxplayer1.speedscale = ????

		//Set all of the speed scale values affected by the new BPM- animations, sfx, systems
		//rhythmlock
		AnimatedSprite2D Rhythmlock = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/LockHUD/RhythmLock");
		Rhythmlock.SpeedScale = 120f / trackbpm;
		GD.Print(Rhythmlock.SpeedScale);
		AnimatedSprite2D Screeneffects = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/ScreenEffects");
		Screeneffects.SpeedScale = 120f / trackbpm;
		Node Guimanager = GetNode("/root/Root3D/LogicParent/GameLogic/GUIManager"); Guimanager.Call("SetDupeLockSpeedScale", trackbpm);
		
		GD.Print("Music will play at " + trackbpm + " BPM!");
		await ToSignal(GetTree().CreateTimer(2), "timeout");

		//Start with sfx and start BPM counter
		Node Heart = GetNode("/root/Root3D/LogicParent/GameLogic/DJ/Heart");
		Heart.Call("StartMusic", trackbpm, trackstarttrim, trackendtrim);
	}

	/*NOTES: the duration of most things like SFX and animations should be templated on 120BPM and then sped up or 
	down based on the current track's bpm interval using the speed scale property*/
	
}
