using Godot;
using System;

//TEMPORARY SCRIPT, DELETE LATER
/*this script rotates between each of the scenes when the scene change button in the Dev Panel
is pressed.*/

public partial class rotatessceneonbuttonTEMP : Button
{
	private void onRotateSceneButtonPressed()
	{
        /*NOTE: This swap uses a method that deletes the old scene and its data. Only for Start Menu to
        Gameplay Scenes, use another method that HIDES the gameplay scenes and runs them in tandem for
        Fight/Roam, and Hub uses this method as well. */

        SceneTree sceneTree = GetTree();
        
        // Change to the new scene.
        sceneTree.ChangeSceneToFile("res://GAMESTATES/FIGHT/SCENE_Fight.tscn");
        

        GD.Print("Fight Scene Loaded via <rotatesceneonbuttonTEMP>");
        //NOTE: CODE BELOW IS A BANDAID, CHANGE WHEN ACTUAL MENUS ARE IMPLEMENTED

        /*if ( = */
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //don't put on rotatebuttonpressed here
    }

}
