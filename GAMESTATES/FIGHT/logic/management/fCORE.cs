using Godot;
using System;
using System.Collections.Generic;

public partial class fCORE : Node
{
	[Signal] public delegate void CallAPlayerDistributeDataFEventHandler();
	[Signal] public delegate void CallDJAndTurnHandlerEventHandler();
	[Signal] public delegate void EnableControlsEventHandler(string newmode);

	//Starts fCORE when the gamestatewatcher gives the OK
	public void COREstartup()
	{
		GD.Print("fCORE recognizes the scene load. Running setup function.");
		
		//TEMP remove the temp template storage branch before runtime (and just delete it entirely before shipping)
		Node Removethesetemplates = GetNode("/root/Root3D/TEMP PackedSceneTemplates");
		Removethesetemplates.QueueFree();
		//TEMP this needs to be when the gameplay starts not just fight
		Node ResourceretrieverA = GetNode("/root/Auto_ResourceRetriever");
		ResourceretrieverA.Call("TEMPonfCOREinitialize");

		Node APlayerData = GetNode("/root/Auto_PlayerData");
		Callable callplayerdatadistribute = new Callable(APlayerData, "DistributeDataF");
		this.Connect("CallAPlayerDistributeDataF", callplayerdatadistribute);

		SETUPFUNCTION();
	}

	//CORE STARTUP ROUTINE
	
	private void SETUPFUNCTION()
	{
		//tell the auto dataretriever to get the correct fight data and distribute it to (places)
		EmitSignal(SignalName.CallAPlayerDistributeDataF);
		
		//tell auto playerflags to distribute (flags?)
		

		//attach 'constantvalues[location] to ScenarioConstantsHolder node for scenario setup to reference

		//Call the scenario setupper to set up all the game parts
		Node Scenariosetup = GetNode("/root/Root3D/LogicParent/GameLogic/ScenarioSetup");
		Scenariosetup.Call("onCOREcall");
		

		//Call the DJ Node to start the music (and tempo by extension) ((and everything that relies on the tempo))
		Node DJ = GetNode("/root/Root3D/LogicParent/GameLogic/DJ");
		DJ.Call("MusicStart");
		
		//signal turnhandler everythings set to go
		
		Node Turnhandler = GetNode("/root/Root3D/LogicParent/GameLogic/TurnHandler");
		Turnhandler.Call("onCOREcall");
		//GD.Print("fCORE initialization complete. Calling ((turnhandler)) and ((beathandler))");
		//GD.Print("There was a problem at the " + COREstartstate + " step. Shutting down CORE.");
		
	}

}
