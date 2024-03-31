using Godot;
using System;

public partial class musicplayerfunctions : AudioStreamPlayer
{
	[Export]
	public float TrimStartSeconds {get; set;}
	[Export]
	public float TrimEndSeconds {get; set;}

}
