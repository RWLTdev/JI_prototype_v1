using Godot;
using System;

public partial class musicplayerfunctionsSM : AudioStreamPlayer
{
	[Export]
	public float TrimStartSeconds {get; set;}
	[Export]
	public float TrimEndSeconds {get; set;}

}
