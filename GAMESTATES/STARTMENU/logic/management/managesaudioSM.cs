using Godot;
using System;

public partial class managesaudioSM : Node
{
	Node AudioManager;

	AudioStreamPlayer musicplayer1;
	private musicplayerfunctionsSM Musicplayer1functions;
	string musicplayer1path = "/root/Root3D/AudioParent/MusicPlayer1";
	
	private bool firstplay = true;

	private float length;
	private float end;
	private float playbackposition;

	public override void _Ready()
	{
		SetProcess(false);
	}
	
	public async void onCOREsignal()
	{
		musicplayer1 = GetNode<AudioStreamPlayer>(musicplayer1path);
		musicplayer1.Stream = GD.Load<AudioStream>("res://ALLTEMP stuffstorage/assets/audio/music/LonelyWindFade.wav");
		Musicplayer1functions = (musicplayerfunctionsSM)musicplayer1;

		length = (float)musicplayer1.Stream.GetLength();
		GD.Print("length" + length);
		end = length - Musicplayer1functions.TrimEndSeconds;
		GD.Print("end:" + end);
		await ToSignal(GetTree().CreateTimer(2), "timeout");
		
		SetProcess(true);
		musicplayer1.Play(0.0f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (musicplayer1.Playing)
		{
			playbackposition = musicplayer1.GetPlaybackPosition();
	
			if (firstplay && playbackposition >= Musicplayer1functions.TrimStartSeconds)
			{
				firstplay = false;
			}
			if(!firstplay && (playbackposition < Musicplayer1functions.TrimStartSeconds || Math.Abs(playbackposition - end) <0.2))
			{
				GD.Print("Looping music at " + musicplayer1.GetPlaybackPosition() + "seconds via <managesaudio>.");
				musicplayer1.Seek(Musicplayer1functions.TrimStartSeconds); 
			}
		}
	}
}
