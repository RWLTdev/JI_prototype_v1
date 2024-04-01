using Godot;
using System;

public partial class rootreadySM : Node
{
		[Signal]
		public delegate void RootReadyEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("RootSM recognizes scene is loaded! Waking up smCORE.");

		//Root -> CORE "Scene's Ready"		
		Node CORE = GetNode("/root/Root3D/LogicParent/GameLogic/CORE");
		Callable roottoCORE = new Callable(CORE, "onRootReady");
		this.Connect("RootReady", roottoCORE);
		EmitSignal(SignalName.RootReady);
	}
}
