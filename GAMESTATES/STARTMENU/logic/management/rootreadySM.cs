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

		EmitSignal(SignalName.RootReady);
		
		/* CALLABLES?? Create a Callable from the smCORE node and the OnRootReady method
		Callable callable = new Callable(CORE, "OnRootReady");

		// Connect the signal to the OnRootReady method in the smCORE node
		this.Connect("tree_entered", callable); */
	}
}
