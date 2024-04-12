using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class constantareavalsinTestArea : Node
{
    //TEST AREA CONSTANT VALUES INTERCHANGEABLE
    
    #region MUSIC TRACKS

    //struct: music track information
    private struct cvMusicTrack
    {
        public string Name;
        public string FilePath;
        public float BPM;

        public float StartTrimValue;
        public float EndTrimValue;

        //constructor
        public cvMusicTrack(string name, string filepath, float bpm, float starttrimvalue, float endtrimvalue) //add custom startbeforebeat offset for dead space and custom beat to start the ready set go on
        {
            Name = name;
            FilePath = filepath;
            BPM = bpm;
            StartTrimValue = starttrimvalue;
            EndTrimValue = endtrimvalue;
        }
    }       
    //randomizer list ((change to be used on zone load eventually))
        private Dictionary<string, cvMusicTrack> MusicTrackList = new Dictionary<string, cvMusicTrack>
        {
            { "DealEmOut", new cvMusicTrack ("Deal 'Em Out", "res://ALLTEMP stuffstorage/assets/audio/music/Deal 'Em Out.mp3", 120.0f, 0.0f, 0.0f) },
            //{ "GetEnuf", new cvMusicTrack ("GET ENUF", "res://ALLTEMP stuffstorage/assets/audio/music/GET ENUF.mp3", 135.74555f, 0.0f, 0.0f) },
            { "Crypteque", new cvMusicTrack ("Crypteque", "res://ALLTEMP stuffstorage/assets/audio/music/Crypteque.mp3", 130.0f, 0.0f, 0.0f) },
            { "JackDaFunk", new cvMusicTrack ("JACK DA FUNK", "res://ALLTEMP stuffstorage/assets/audio/music/JACK DA FUNK.mp3", 112.0f, 0.0f, 0.0f) }, 
        };
    
    //randomly picks a track when musicpicker requests it and sends the relevant info back to it
    dynamic thistrackselected;      
    public void onMusicPickerRequest(string action)
    {
        Node Musicpicker = GetNode("/root/Root3D/LogicParent/GameLogic/DJ");
        switch (action)
        {
            case "pickrandomtrack":
                //Get struct/dict and randomly pick a track inside its dict (uses linq)
                var keys = MusicTrackList.Keys.ToList();
                var randomKey = keys[new Random().Next(keys.Count)];
                thistrackselected = MusicTrackList[randomKey];

                //send the info back to musicpickerRequest
                Musicpicker.Call("returnAreaConstantValsTrack", thistrackselected.FilePath, thistrackselected.BPM, thistrackselected.StartTrimValue, thistrackselected.EndTrimValue);
            break;
        }
    }
    
    #endregion



    #region FIGHTBACKDROPPRESETS

    //FIGHT BACKDROP ELEMENTS
    public string resourcetype = "s";
    public string resourcefilepath = "res://GAMEASSETS/in Fight/backdrop/animation packs/SpriteFrames_TestAreafBackgroundObjClose.tres";

    //struct: backdrop element information
    private struct cvBackdropElement
    {
        public string AnimationName;
        public string DistanceAnchor; //fore, close, mid, far

        public Vector2 Offset;
        public Vector3 Position;
        public Vector3 Scale;

        //beatscale and rhythmsync will be handled in another visual manager
        public cvBackdropElement(string animationname, string distanceanchor, Vector2 offset, Vector3 position, Vector3 scale)
        {
            AnimationName = animationname;
            DistanceAnchor = distanceanchor;
            Offset = offset;
            Position = position;
            Scale = scale;
        }
    }       
            //an array of structs for each item in BG preset and a dictionary to pick one of them
            //note: when you need to iterate over all items in one of these arrays, just use a foreach it does the job for you
            private cvBackdropElement[] Preset1 = new cvBackdropElement[]
            {
               new cvBackdropElement("BGObjTVSpikesTEMP", "close", new Vector2(0, 400), new Vector3(5.683f, 0, 1.141f), new Vector3(0.3f, 0.3f, 0.3f)),
               new cvBackdropElement("BGObjPunch!TEMP", "close", new Vector2(0, 400), new Vector3(2.18f, 0, -1.759f), new Vector3(0.2f, 0.2f, 0.2f)),
               new cvBackdropElement("BGObjFantasyRockTEMP", "close", new Vector2(0, 450), new Vector3(-2.295f, 0, 1.912f), new Vector3(0.5f, 0.5f, 0.5f)),
               new cvBackdropElement("BGObjThinkerTEMP", "close", new Vector2(0, 300), new Vector3(-3.955f, 0, -1.21f), new Vector3(0.3f, 0.3f, 0.3f)),
            };
            //just dupe this setup^ when you need to add more presets

            private Dictionary<string, cvBackdropElement[]> BackdropPresets;
            public constantareavalsinTestArea() // Assuming this is your constructor
            {
                BackdropPresets = new Dictionary<string, cvBackdropElement[]>
                {
                    {"Preset1", Preset1},
                };
            }
    
    dynamic thispresetselected;
    public void onScenarioSetupRequest(string action)
    {
        Node Scenariosetup = GetNode("/root/Root3D/LogicParent/GameLogic/ScenarioSetup");
        switch (action)
        {
            case "getrandombackdroppreset":
                //Get struct/array/dict and randomly pick a track inside its dict (uses linq)
                var keys = BackdropPresets.Keys.ToList();
                var randomKey = keys[new Random().Next(keys.Count)];
                thispresetselected = BackdropPresets[randomKey];

                Scenariosetup.Call("returnResourceAndLoad", resourcetype, resourcefilepath);
                //iterate over each point in the struct and relay its info for senariosetup to act on
                foreach (cvBackdropElement element in thispresetselected)
                {
                    Scenariosetup.Call("returnAreaConstantBackdropElementAndSet", element.AnimationName, element.DistanceAnchor, element.Offset, element.Position, element.Scale);
                }     
                break;
            default:
                break;
        }
    }

    #endregion
    



}
