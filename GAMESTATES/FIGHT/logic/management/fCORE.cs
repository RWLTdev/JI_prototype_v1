using Godot;
using System;
using System.Collections.Generic;

public partial class fCORE : Node
{
	//Starts fCORE when the gamestatewatcher gives the OK
	public void COREstartup()
	{
		GD.Print("fCORE recognizes the scene load. Running setup function.");

		SETUPFUNCTION();
	}

	//CORE STARTUP ROUTINE
	
	private void SETUPFUNCTION()
	{
		AnimatedSprite2D ScreenEffects = GetNode<AnimatedSprite2D>("/root/Root3D/GUI&CameraParent/CanvasLayer/ScreenEffects");
		ScreenEffects.Visible = true; ScreenEffects.Play("StaticBlackScreen");

		//connect signal and tell auto playerdata to distribute fight data
		Node APlayerData = GetNode("/root/Auto_PlayerData");
		Callable callplayerdatadistribute = new Callable(APlayerData, "DistributeDataF");
		this.Connect("CallAPlayerDistributeDataF", callplayerdatadistribute);
		EmitSignal(SignalName.CallAPlayerDistributeDataF);
		
		//tell auto playerflags to distribute (flags?)
		

		//attach 'constantvalues[location] to ScenarioConstantsHolder node for scenario setup to reference


		//signal scenario setup
		//signal GUI animation handler

		//Call the DJ Node to start the music (and tempo by extension)
		Node DJ = GetNode("/root/Root3D/LogicParent/GameLogic/DJ");
		DJ.Call("MusicStart");
		
		//signal turnhandler everythings set to go
		
		CallTurnHandler();
		
		//GD.Print("There was a problem at the " + COREstartstate + " step. Shutting down CORE.");
		
	}
	
	[Signal]
	public delegate void CallAPlayerDistributeDataFEventHandler();
	[Signal]
	public delegate void CallDJStartEventHandler();
	[Signal]
	public delegate void EnableControlsEventHandler(string newmode);

	private void CallTurnHandler()
	{
		GD.Print("fCORE initialization complete. Calling ((turnhandler)) and ((beathandler))");
	}
}
