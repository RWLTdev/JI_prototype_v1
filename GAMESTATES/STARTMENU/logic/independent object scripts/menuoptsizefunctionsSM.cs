using Godot;
using System;

public partial class menuoptsizefunctionsSM : Node2D
{
	[Export]
	private string thisNodepath;
	private Vector2 toBiggerTarget = new Vector2(1.26f, 1.26f);
	private Vector2 toSmallerTarget = new Vector2(1.0f, 1.0f);
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
