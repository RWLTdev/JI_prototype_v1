using Godot;
using System;

public partial class initializescenarioF : Node
{
	public void onCOREcall()
	{
		GD.Print("<initializerF> loud and clear! Getting things set up.");
		getstuffinorderTEMP();
	}
	private void getstuffinorderTEMP()
	{
		GetSetStageElements();
	}
	private void GetSetStageElements()
	{
		Node Holdingareaconstantvalues = GetNode("/root/Root3D/LogicParent/DataHolders/HoldingAreaConstantValues");
		Holdingareaconstantvalues.Call("onScenarioSetupRequest", "getrandombackdroppreset");
	}
	
	private string resourcepath;
	public void returnResourceAndLoad(string iresourcetype, string iresourcepath)
	{
		resourcepath = iresourcepath;	
	}
	
	public void returnAreaConstantBackdropElementAndSet(string animationname, string distanceanchor, Vector2 offset, Vector3 position, Vector3 scale)
	{	
		//When the current AreaConstants calls this method with all the requested values, it iterates this method for as many backdrop items are in the random selected array
		switch (distanceanchor)
		{
			case "close":
				Node3D Thisbackdropanchor = GetNode<Node3D>("/root/Root3D/EnvironmentParent/CloseBackdropAnchor");
				PackedScene closebackdropobjPckd = (PackedScene)GD.Load("res://GAMEASSETS/In Fight/backdrop/packed game objects/fBackdropObjClose.tscn");
				AnimatedSprite3D Thisclosebackdropobj = (AnimatedSprite3D)closebackdropobjPckd.Instantiate();
				Thisbackdropanchor.AddChild(Thisclosebackdropobj);
				Thisclosebackdropobj.SpriteFrames = (SpriteFrames)GD.Load(resourcepath);
				Thisclosebackdropobj.Animation = animationname;
				Thisclosebackdropobj.Offset = offset;
				Thisclosebackdropobj.Position = position;
				Thisclosebackdropobj.Scale = scale;
				break;
			default:
				break;

			GD.Print("Backdrop loaded via <initializescenarioF -> constantvalsinTestArea>");
		}
	}
	
	//Enemies are already predetermined in Roam
	private void GetSetPlayerElements()
	{
		
	}

	private void GetSetEnemies()
	{

	}

}
