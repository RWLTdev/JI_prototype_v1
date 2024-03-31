using Godot;
using System;
using System.Collections.Generic;
public partial class playerdataA : Node
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("<playerdataA> operational!");
	}

	//CURRENT PARTY MEMBERS
	public void PullDataFromSaveJsonX(string filename, string directory)
	{

	}

	public void DistributeDataF()
	{
		
	}

	//NOTE: All of these values are static.

	public static string playercurrentlocation = "TestArea";
	public static string playercurrentzone = "none";
	public static string playercurrentroomID = "none";

	public static Dictionary<int, string> partySlots = new Dictionary<int, string>()
	{
		{1, "none"},
		{2, "none"},
		{3, "none"},
		{4, "none"}
	};
	
	public static Dictionary<int, string> backupSlots = new Dictionary<int, string>()
	{
		{1, "none"},
		{2, "none"},
		{3, "none"},
		{4, "none"},
		{5, "none"},
		{6, "none"},
		{7, "none"},
		{8, "none"},
	};

	//PARTY DATA VALUES, check if null before using
	public static Dictionary<string, double?> partyHP = new Dictionary<string, double?>()
	{
		{"p1", null},
		{"p2", null},
		{"p3", null},
		{"p4", null}
	};

	public static Dictionary<string, int?> partyATK = new Dictionary<string, int?>()
	{
		{"p1", null},
		{"p2", null},
		{"p3", null},
		{"p4", null}
	};

	public static Dictionary<string, int?> partyDEF = new Dictionary<string, int?>()
	{
		{"p1", null},
		{"p2", null},
		{"p3", null},
		{"p4", null}
	};

	public static Dictionary<string, string> partyAffinites = new Dictionary<string, string>()
	{
		{"p1", "none"},
		{"p2", "none"},
		{"p3", "none"},
		{"p4", "none"}
	};

	public static Dictionary<string, string> partyWeaponEquips = new Dictionary<string, string>()
	{
		{"p1", "none"},
		{"p2", "none"},
		{"p3", "none"},
		{"p4", "none"}
	};

	//PLAYER INVENTORY ITEM LISTS

	//weapons- name, *attack type, +atk, durability left, *effect1name, *effect2name, *effect3name change later ##
	//* not implemented yet
	public static List<(string name, int atk, int hitsleft)> playerWeapons = new List<(string, int, int)>();

	//items- name, effectname
	public static List<(string name, string effectname)> playerItems = new List<(string, string)>();
}
