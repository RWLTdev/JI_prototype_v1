using Godot;
using System;

public partial class scrollmenufollowfunctionsSM : PathFollow2D
{
	[Export]
	private string thisNodepath;
	private bool isCounting = false;

	//was going to be for disabling player input on moving but that would feel bad, switch to menu options magnetizing and independently spacing themselves
	//[Signal]
	//public delegate void isMovingReportEventHandler(string ControlType = "MenuMove");
	
	//moves this menu option up along the path2d when prompted by the input watcher
	public void ScrollUp()
	{
		//EmitSignal(SignalName.isMovingReport);
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

	//moves this menu option down along the path2d when prompted by the input watcher
	public void ScrollDown()
	{
		//EmitSignal(SignalName.isMovingReport);
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