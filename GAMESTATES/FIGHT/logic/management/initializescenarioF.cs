using Godot;
using System;
using System.Collections.Generic;

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
		GetSetPlayerElements();
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
		PackedScene playercharacterbase = (PackedScene)GD.Load("res://GAMEASSETS/in Fight/field units/player units/packed game objects/PgoF_PlayerCharacterBase.tscn");
		Node Playerunitcontainer = GetNode("/root/Root3D/UnitContainerParent/PlayerUnitContainer");

		Dictionary<int, string[]> rotationplacelist = new Dictionary<int, string[]>()
  		{
        { 1, new[] { "right" } },
        { 2, new[] { "right", "left" } },
        { 3, new[] { "right", "down", "left" } },
        { 4, new[] { "right", "down", "left", "up" } },
    	};
		string[] rotationplaces = rotationplacelist[playerdataA.partycount];

		//create the player characters and set the rotation mode, camera mode etc
		switch (playerdataA.partycount)
		{
			case 1:

				break;
			case 2:
				
				break;
			case 3:
				
				break;
			case 4:
				for (int i = 1; i <= playerdataA.partycount; i++)
				{
					
					//create a player character instance, add it to the container and name it (1-4)
					Node3D thisCharacteranchor = (Node3D)playercharacterbase.Instantiate();
					Playerunitcontainer.AddChild(thisCharacteranchor);
					thisCharacteranchor.Name = "PCharAnchor" + i;

					Node thisCharacterlogic = thisCharacteranchor.GetNode("PCharLogic");

					//set its location to its current snap point (in order of rotation) and set its rotation place
					Node3D thisSnappoint = GetNode<Node3D>("/root/Root3D/GridParent/GridL/RotateAxle/AxleSnap" + i);
					thisCharacteranchor.GlobalTransform = thisSnappoint.GlobalTransform;
					thisCharacterlogic.Call("setRotationPlace", rotationplaces[i - 1]);
					//call its internal logic nodes' setup function
					thisCharacterlogic.Call("onSetupSetThisUnit", playerdataA.partySlots[i]);
				}
				break;
			default:
				break;
		}
		GD.Print("<initializescenarioF> loaded player character elements");
	}

	private void GetSetEnemies()
	{

	}

}
