using Godot;
using System;
using System.Collections.Generic;

public partial class constantvalsinTestArea : Node
{
    //TEST AREA CONSTANT VALUES INTERCHANGEABLE
    //Party's over no more statics eg. interface necessitated

    //music track names, music track paths, ((current music track)), BPM values, 
    public struct cvMusicTrack
    {
        public string Name;
        public string FilePath;
        public double BPM;

        //constructorsss babyyyyyy
        public cvMusicTrack(string name, string filepath, double bpm)
        {
            Name = name;
            FilePath = filepath;
            BPM = bpm;
        }
    }       
            //randomizer list ((change to be used on zone load eventually))
            public static Dictionary<string, cvMusicTrack> MusicTrackList = new Dictionary<string, cvMusicTrack>
            {
                { "DealEmOut", new cvMusicTrack ("Deal 'Em Out", "res://ALLTEMP stuffstorage/assets/audio/music/Deal 'Em Out.mp3", 120.0) },
                { "GetEnuf", new cvMusicTrack ("GET ENUF", "res://ALLTEMP stuffstorage/assets/audio/music/GET ENUF.mp3", 136.0) },
                { "Crypteque", new cvMusicTrack ("Crypteque", "res://ALLTEMP stuffstorage/assets/audio/music/Crypteque.mp3", 130.0) },
            };

     //bg element node arrangements in coords from the bgparent/ close/medium/far,
    public struct BackgroundElementNodeArrangements
    {
        public enum Distance{Close, Medium, Far}
    }
     //texture and animation info, filepath...

    public struct BackgroundElementNodeData
    {
        public string FileName;
    }
             
    
}
