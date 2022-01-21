using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator))] 	
public class DungeonManager : Editor {

	// Use this for initialization
	 public DungeonGenerator script;
	 
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Generate Snake Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.GenSnakeDungeon();
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Generate Line Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.GenLineDungeon();
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Generate Pependecular Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.GenPerpendicularDungeon();
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Generate Broken Rec Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.GenBrokenRectangleDungeon();
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Generate Progression Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.GenProgressionDungeon(script.groundTransX,script.groundTransZ,-1,-1,-1,-1,-1,-1);
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Generate Normal Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.GenDungeon(script.groundTransX,script.groundTransZ,-1,-1,-1,-1,-1,-1);
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent("Clean Generated Dungeon"), GUILayout.Width(250)))
		{
			script = FindObjectOfType(typeof(DungeonGenerator)) as DungeonGenerator; 
			script.cleanDungeons();
		}
		EditorGUILayout.EndHorizontal ();
	}
	
	
}
