using Godot;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

public partial class handlesmusicinfoF : Node
{
	private string currenttrackpath;
	public float bpm;

	//interface for the class types stored in the dict below, for storing playercurrentlocation values compared to constant
	public async void MusicStart()
	{
		GD.Print("Starting Fight Music Via <djhandlesmusicandtempo>");
	
		/* uses playervals to pick a random track from the relevant struct list and grab all its relevant data
		 and (this is literally the simplest way to do it WTF????)*/
		string currentplayerlocation = playerdataA.playercurrentlocation;
		dynamic selectedtrack;
		switch (currentplayerlocation)
		{
			case "TestArea":
				List<string> keys = new List<string>(constantvalsinTestArea.MusicTrackList.Keys);
				Random random = new Random();
				int randomIndex = random.Next(keys.Count);
				string randomKey = keys[randomIndex];
				constantvalsinTestArea.cvMusicTrack getstruct = constantvalsinTestArea.MusicTrackList[randomKey];
				selectedtrack = getstruct;
				break;
			default:
				return;
		}

			bpm = selectedtrack.BPM;
			float starttrim = selectedtrack.StartTrimValue;
			float endtrim = selectedtrack.EndTrimValue;
			AudioStreamPlayer musicplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/MusicPlayer1");
			musicplayer1.Stream = GD.Load<AudioStream>(selectedtrack.FilePath);
			AudioStreamPlayer guisfxplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/GUISFX1");
			guisfxplayer1.Stream = GD.Load<AudioStream>("res://ALLTEMP stuffstorage/assets/audio/HeartBeatEchoSFX.wav");
			//guisfxplayer1.speedscale = ????

			//Set all of the speed scale values affected by the new BPM- animations, sfx, systems
			//rhythmlock
			AnimatedSprite2D Rhythmlock = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/LockHUD/RhythmLock");
			Rhythmlock.SpeedScale = 120f / bpm;
			GD.Print(Rhythmlock.SpeedScale);
			AnimatedSprite2D Screeneffects = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/ScreenEffects");
			Screeneffects.SpeedScale = 120f / bpm;
			Node Guimanager = GetNode("/root/Root3D/LogicParent/GameLogic/GUIManager"); Guimanager.Call("SetDupeLockSpeedScale", bpm);
			
			GD.Print("Music will play at " + selectedtrack.BPM + " BPM!");
			await ToSignal(GetTree().CreateTimer(2), "timeout");

			//Start with sfx and start BPM counter
			Node Heart = GetNode("/root/Root3D/LogicParent/GameLogic/DJ/Heart");
			Heart.Call("StartMusic", bpm, starttrim, endtrim);
	}

	/*NOTES: the duration of most things like SFX and animations should be templated on 120BPM and then sped up or 
	down based on the current track's bpm interval using the speed scale property*/
	
}
