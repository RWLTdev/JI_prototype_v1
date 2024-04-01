using Godot;
using System;
using System.Timers;



public partial class handlesmusictimingF : Node
{
	private Godot.Timer Beattimer;

	Node Guimanager;
    public override void _Ready()
    {
        SetProcess(false);

		Beattimer = new Godot.Timer();
		AddChild(Beattimer);
		Beattimer.Connect("timeout", new Callable(this, nameof(Heartbeat)));

		//Also connect all of the beat signal recievers here!
		//here
		this.Connect("Beat", new Callable(this, nameof(StartBeats)));
		//gui manager
		Guimanager = GetNode("/root/Root3D/LogicParent/GameLogic/GUIManager");
		Callable heartbeattoguimanager2 = new Callable(Guimanager, "onHeartbeat");
		this.Connect("Beat", heartbeattoguimanager2);
		
    }
    [Signal] public delegate void BeatEventHandler();

	AudioStreamPlayer musicplayer1; AudioStreamPlayer guisfxplayer1; 

	//whole bunch variables for calculating start/stop/duration/expected point in time of the current playing track
	private float songbpm; private float songstarttrimvalue; private float songendtrimvalue;
	public int BEATCOUNT = 0;

	private bool firstplay = true;
	private float length; private float trimmedend; private float playbackposition; private float fulltrimmedduration;

	//actually caluclating those values
	public void StartMusic(float bpm, float starttrim, float endtrim)
	{

		musicplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/MusicPlayer1");
		guisfxplayer1 = GetNode<AudioStreamPlayer>("/root/Root3D/AudioParent/GUISFX1");
		
		songbpm = bpm;
		songstarttrimvalue = starttrim;
		songendtrimvalue = endtrim;
		length = (float)musicplayer1.Stream.GetLength();
		GD.Print("song original length: " + length);
		trimmedend = length - songendtrimvalue;
		GD.Print("song now ends at: " + trimmedend);
		fulltrimmedduration = trimmedend - songstarttrimvalue;
		Beattimer.WaitTime = 60f / songbpm;
		SetProcess(true);
		GD.Print("Timer Value: " + Beattimer.WaitTime);	
		//start the heartbeat
		Beattimer.Start();
	}
	
	//The code that runs the BPM timer that everything relies on. The beating heart if you will
	public void Heartbeat()
	{
		EmitSignal(SignalName.Beat);
		BEATCOUNT++;
		GD.Print(BEATCOUNT);	
	}

	//Queuing stuff that happens before the player actually gets control (start animations & stuff)
	private async void StartBeats()
	{
		this.Disconnect("Beat", new Callable(this, nameof(StartBeats)));
		int onoffnum = 0;
		Guimanager.Call("onStartBeats", "LockStartBeats");
		while (BEATCOUNT < 10)
		{
			if (onoffnum == 1)
			{
				guisfxplayer1.Play(0.0f);
			}
			onoffnum = 1 - onoffnum;
			//start the song
			await ToSignal(Beattimer, "timeout"); //wait for next beat
		}
		musicplayer1.Play(0.0f);
		Guimanager.Call("onStartBeats", "StartTransitionBasic");
		Guimanager.Call("onStartBeats", "StartLockThink");
	}

	//using the _process function to tell whether or not the playing song is out of sync w/ the game rhythm, when to loop the song etc
	private int previousbeatcount;
	private int adjustmentcounter;
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (musicplayer1.Playing)
		{
			//get the current playback position
			playbackposition = musicplayer1.GetPlaybackPosition();
			//is the timer position ahead, behind or on
			float expectedPosition = (float)(Beattimer.TimeLeft * songbpm / 60) * fulltrimmedduration + songstarttrimvalue;
			//decrease or increase wait time as necessary every 10 beats (if it gets out of sync w/ music)
			if (BEATCOUNT > previousbeatcount)
			{
				adjustmentcounter++;
				if(adjustmentcounter >= 10)
					if (playbackposition > expectedPosition)
					{
						Beattimer.WaitTime -= 0.01f;
					
					}
					else if (playbackposition < expectedPosition)
					{
						Beattimer.WaitTime += 0.01f;
						
					}
					adjustmentcounter = 0;
				previousbeatcount = BEATCOUNT;
			}
			//if the song is at the end of the trim, loop it back to the start
			if (firstplay && playbackposition >= songstarttrimvalue)
			{
				firstplay = false;
			}
			if(!firstplay && (playbackposition < songstarttrimvalue || Math.Abs(playbackposition - trimmedend) <0.2))
			{
				GD.Print("Looping music at " + musicplayer1.GetPlaybackPosition() + "seconds via <managesaudio>.");
				musicplayer1.Seek(songstarttrimvalue); 
			}
		}
	}
}
