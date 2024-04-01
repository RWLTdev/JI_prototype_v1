using Godot;
using System;

public partial class usefulstuffcopypasta : Node
{
	//Connect Signal to node at path:

	#if false
	//Connect Signal here to other node at path:
	NodeType NodeToConnectTo = GetNode<NodeType>("/root/That/Nodes/NodePath");
	Callable bullshitabstractionlayer = new Callable(NodeToConnectTo, "WantedMethodInThatNode");
	this.Connect("SignalNameInThisNode", bullshitabstractionlayer);
	EmitSignal(SignalName.SignalNameInThisNode);

	//Connect Signal from other node to a method in this one:
	NodeType nodevariablename = GetNode<NodeType>("/root/That/Nodes/NodePath");
	nodevariablename.Connect("ThatNodesSignalName", new Callable(this, nameof(ThisScriptsMethodName)));
	await ThisScriptsMethodname(); //if you want to wait for that signal before continuing

	//Storing a nodepath/filepath/any same-y value without having to type it in the code:
	[Export]
	public string thisNodepath; //Then you can set it in the editor. Useful for template scripts with pathing

	//Easy way to create a timer that waits and then continues
	await ToSignal(GetTree().CreateTimer(5), "timeout");



	#endif
}
