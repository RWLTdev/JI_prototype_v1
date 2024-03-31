using Godot;
using System;

public partial class usefulstuffcopypasta : Node
{
	//Connect Signal to node at path:

	#if false
	Node NodeToConnectTo = GetNode("That/Nodes/NodePath");
	Callable bullshitabstractionlayer = new Callable(NodeToConnectTo, "WantedMethodInThatNode");
	this.Connect("SignalNameInThisNode", bullshitabstractionlayer);
	EmitSignal(SignalName.SignalNameInThisNode);

	#endif
}
