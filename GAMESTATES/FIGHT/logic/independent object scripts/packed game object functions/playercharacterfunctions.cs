using Godot;
using System;
using System.Collections.Generic;

public partial class playercharacterfunctions : Node
{
	[Export] public Resource thisCharacterConstants;

	Node3D thisCharacteranchor;
	AnimatedSprite3D thisCharactersprite;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		thisCharacteranchor = GetParent<Node3D>();
		thisCharactersprite = thisCharacteranchor.GetNode<AnimatedSprite3D>("CharacterSprite");
	}

	/*this script isn't meant to be an attachable actually this one will be baked into the packed scene and contain 
	the basic FUNCTIONS for its container and is able to call and use the attachable basic data*/
	
	public Dictionary<string, (string, Type)> constantsDict = new Dictionary<string, (string, Type)>()
	{
		{"none", ("res://GAMEASSETS/in Fight/field units/player units/constant value resources/Rez_InmateConstantsDefault.tres", typeof(rez_inmateconstantsdefault))},
		{"Merlin", ("res://GAMEASSETS/in Fight/field units/player units/constant value resources/RezScript_InmateConstantsMerlin.tres", typeof(rez_inmateconstantsmerlin))},
		{"Cebin", ("res://GAMEASSETS/in Fight/field units/player units/constant value resources/RezScript_InmateConstantsCebin.tres", typeof(rez_inmateconstantscebin))},
		{"Louis", ("res://GAMEASSETS/in Fight/field units/player units/constant value resources/RezScript_InmateConstantsLouis.tres", typeof(rez_inmateconstantslouis))},
		{"Reprobate", ("res://GAMEASSETS/in Fight/field units/player units/constant value resources/RezScript_InmateConstantsReprobate.tres", typeof(rez_inmateconstantsreprobate))},
	};
	
	public string TEMPcharname;
	public void onSetupSetThisUnit(string charactername = "none")
	{	
		TEMPcharname = charactername;
		//connect input signals
		Node Inputwatcher = GetNode("/root/Root3D/LogicParent/Watchers/WatchingForInputs");
		Inputwatcher.Connect("SelectInmate", new Callable(this, nameof(onInputsignalSelectToggle)));
		
		if (constantsDict.TryGetValue(charactername, out var resourceinfo))
		{
			//load the new character constants resource from the dict
			thisCharacterConstants = (Resource)GD.Load(resourceinfo.Item1);
			Type ResourceType = resourceinfo.Item2;

			//set & play sprite animation
			string animpackpath;
			if (thisCharacterConstants.GetType() == ResourceType)
			{
				dynamic inmateconstants = Convert.ChangeType(thisCharacterConstants, ResourceType);
				animpackpath = inmateconstants.animationpack_filepath_F;
				thisCharactersprite.SpriteFrames = (SpriteFrames)GD.Load(animpackpath);
			}
			thisCharactersprite.Animation = "TEMPStatic";
			thisCharactersprite.Play();
		}	
	}

	string[] partyrotationplace = { "up", "down", "left", "right" }; string myrotationplace;
	public bool isselected = false;
	public void setRotationPlace(string place)
	{
		int index = Array.IndexOf(partyrotationplace, place);
		if (index != -1)
			{
				myrotationplace = partyrotationplace[index];
			}
	}
	public void onInputsignalSelectToggle(string atplace, bool onoff)
	{
		if (atplace == myrotationplace)
		{
			switch (onoff)
			{
				case true: isselected = true; GD.Print(TEMPcharname + "Select True"); break;
				case false: isselected = false; GD.Print(TEMPcharname + "Select False"); break;
			}
		}
	}
	public void onSelectedMove(string direction)
	{
		
	}
}
