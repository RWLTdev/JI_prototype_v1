using Godot;
using System;

public partial class rhythmbarfunctions : PathFollow2D
{
	handlesmusictimingF Heart;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Heart = GetNode<handlesmusictimingF>("/root/Root3D/LogicParent/GameLogic/DJ/Heart");
		float bpm = Heart.songbpm;
		this.ProgressRatio = 0;

		Tween thisprogresstween = GetTree().CreateTween();
		thisprogresstween.TweenProperty(this, "progress_ratio", 1, (bpm/60)*1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Sprite2D Thisrhythmbarwsprite = GetNode<Sprite2D>("Sprite2D");
		float thisprogressratio =  this.ProgressRatio;

		//sets the rhythmbar's sprite scale as the progress increases
		float thistothatscale = Lerp(0.24f, 0.555f, thisprogressratio);
		Vector2 scalingsizenow = new Vector2(thistothatscale, thistothatscale);
		Thisrhythmbarwsprite.Scale = scalingsizenow;

		//fade in and out as its close to the start and end of its path
		if (thisprogressratio <= 0.2f)
		{
			float newAlpha = Lerp(0.0f, 1.0f, thisprogressratio/0.2f);
   			Thisrhythmbarwsprite.Modulate = new Color(Thisrhythmbarwsprite.Modulate.R, Thisrhythmbarwsprite.Modulate.G, Thisrhythmbarwsprite.Modulate.B, newAlpha);
		}
		else if (thisprogressratio >= 0.95f)
		{
			float newAlpha = Lerp(1.0f, 0.0f, (thisprogressratio -0.9f)/0.1f);
			Thisrhythmbarwsprite.Modulate = new Color(Thisrhythmbarwsprite.Modulate.R, Thisrhythmbarwsprite.Modulate.G, Thisrhythmbarwsprite.Modulate.B, newAlpha);
		
		}
	}

	private float Lerp(float startsize, float endsize, float time)
	{
		return startsize + (endsize - startsize) * time;
	}
}
