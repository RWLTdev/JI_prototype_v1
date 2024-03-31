using Godot;
using System;

public partial class scrollmenufollowfunctionsSM : PathFollow2D
{
	[Export]
	private string thisNodepath;
	private bool isCounting = false;

	[Signal]
	public delegate void isMovingReportEventHandler(string ControlType = "MenuMove");
	
	public async void ScrollUp()
	{
		EmitSignal(SignalName.isMovingReport);
		Tween tween = GetTree().CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		if(this.ProgressRatio < 0.95f)
		{
			tween.TweenProperty(GetNode(thisNodepath), "progress_ratio", this.ProgressRatio + 0.15f, 0.5f).SetTrans(Tween.TransitionType.Back);
		}
		else if(this.ProgressRatio >= 0.95f)
		{
			tween.TweenProperty(GetNode(thisNodepath), "progress_ratio", this.ProgressRatio + 0.10f, 0.5f).SetTrans(Tween.TransitionType.Back);
		}
	}

	public async void ScrollDown()
	{
		EmitSignal(SignalName.isMovingReport);
		Tween tween = GetTree().CreateTween();
		//tween.Connect("finished", ReturnControl());
		tween.SetEase(Tween.EaseType.Out);
		if(this.ProgressRatio >= 0.05f)
		{
			tween.TweenProperty(GetNode(thisNodepath), "progress_ratio", this.ProgressRatio - 0.15f, 0.5f).SetTrans(Tween.TransitionType.Back);
		}
		else if(this.ProgressRatio < 0.15f)
		{
			tween.TweenProperty(GetNode(thisNodepath), "progress_ratio", this.ProgressRatio - 0.10f, 0.5f).SetTrans(Tween.TransitionType.Back);
		}
	}
}