using Godot;
using System;

public partial class menuoptsizefunctionsSM : Node2D
{
	[Export]
	private string thisNodepath;
	[Export]
	private string thisnodesarea2dNodepath;

	//on ready, connects the signals for the functions below to the signals emitted from its nodes' respective area2d
	public override void _Ready()
	{
		Area2D myselectdetector = GetNode<Area2D>(thisnodesarea2dNodepath);
		
		myselectdetector.Connect("area_entered", new Callable(this, nameof(MakeBiggerOnAreaEnter)));
		myselectdetector.Connect("area_exited", new Callable(this, nameof(MakeSmallerOnAreaExit)));
	}
	private Vector2 toBiggerTarget = new Vector2(1.26f, 1.26f);
	private Vector2 toSmallerTarget = new Vector2(1.0f, 1.0f);
	
	//makes the menu option bigger when its area2d enters the main menu selector detector area2d (detected by this one's area2d)
	public void MakeBiggerOnAreaEnter(Area2D area)
	{
		Tween tween = GetTree().CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(GetNode(thisNodepath), "scale", toBiggerTarget, 0.5f).SetTrans(Tween.TransitionType.Expo);
	}
	

	public void MakeSmallerOnAreaExit(Area2D area)
	{
		Tween tween = GetTree().CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(GetNode(thisNodepath), "scale", toSmallerTarget, 0.5f).SetTrans(Tween.TransitionType.Expo);
	}
}
