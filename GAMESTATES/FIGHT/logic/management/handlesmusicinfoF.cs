using Godot;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;

public partial class handlesmusicinfoF : Node
{
	private string currenttrackpath;

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
			AudioStreamPlayer musicplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/MusicPlayer1");
			musicplayer1.Stream = GD.Load<AudioStream>(selectedtrack.FilePath);
			AudioStreamPlayer guisfxplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/GUISFX1");
			guisfxplayer1.Stream = GD.Load<AudioStream>("res://ALLTEMP stuffstorage/assets/audio/HeartBeatEchoSFX.wav");
			
			GD.Print("Music will play at " + selectedtrack.BPM + " BPM!");
			await ToSignal(GetTree().CreateTimer(2), "timeout");

			//Start with sfx and start BPM counter
			BPMCounterCentral(selectedtrack.BPM);
	}

	/*NOTES: the duration of most things like SFX and animations should be templated on 120BPM and then sped up or 
	down based on the current track's bpm interval using the speed scale property*/

	//The code that runs the BPM timer that everything relies on.
	public int BEATCOUNT = 0;
	private bool startsongflag = false;
	private ManualResetEvent beatCountIncreased = new ManualResetEvent(false);

	private void BPMCounterCentral(dynamic bpm)
	{
		//calculate the interval, create the timer
		double interval = 60000.0 / bpm;
		System.Timers.Timer KickoffCounter = new System.Timers.Timer(interval);		
		KickoffCounter.Elapsed += Heartbeat;

		//kickoff counter for BPM before the song starts
		KickoffCounter.Start();

		AudioStreamPlayer guisfxplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/GUISFX1");
		AnimatedSprite2D ScreenEffects = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/ScreenEffects");
		int onoffnum = 0;
		while (BEATCOUNT < 9)
		{
			if (onoffnum == 1)
			{
				guisfxplayer1.Play(0.0f);
			}
			onoffnum = 1 - onoffnum;
			beatCountIncreased.WaitOne();
			beatCountIncreased.Reset();
		}

		StartSong();
		ScreenEffects.Play("StartTransitionBasic");
	}

	/*private Task optionalTask;
	public void SetOptionalTask(Task task)
	{
		this.optionalTask = task;
	}*/
	public void Heartbeat(Object source, ElapsedEventArgs e)
	{
		//NOTE: This needs to be reformatted to not use a timer and instead use the playback of the song to determine the timing

		//CallDeferred(nameof(EmitSignal), SignalName.Beat);
		EmitSignal(SignalName.Beat);
		BEATCOUNT++;
		beatCountIncreased.Set();
		GD.Print("BEATCOUNT: " + BEATCOUNT);
		/*if (this.optionalTask != null)
		{
			await this.optionalTask;
			this.optionalTask = null;
		}*/
	}
	
	/*public void EmitBeatSignal()
	{
		CallDeferred(nameof(EmitBeatSignal), SignalName.Beat);
	}*/



	private async void StartSong()
	{
		AudioStreamPlayer musicplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/MusicPlayer1");
		musicplayer1.Play(0.0f);
	}

	[Signal]
	public delegate void BeatEventHandler();
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
