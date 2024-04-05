using Godot;
using System;

public partial class backdropobjclosefunctions : AnimatedSprite3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/*ThatNodeType variablename = GetNode<ThatNodeType>("/root/Root3D/That/Nodes/NodePath");
		thisnodevariablename.Connect("ThatNodesSignalName", new Callable(this, nameof(ThisScriptsMethodName)));
		await ThisScriptsMethodname();*/

		/*leaving this here for later but backdrop objects need to be connected to another visual animation manager that
		interprets the beat signal and sends its own signal for certain backdrop objects to play every x beat etc*/
	}

	private void onBeatPlayThisAnimation()
	{
		//this.Play();
	}
}
