//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//
//[CustomEditor(typeof(DungeonGeneratorNavMeshSurface))] 	
//public class DungeonManagerNavMeshSurface : Editor {
//
//	// Use this for initialization
//	 public DungeonGeneratorNavMeshSurface script;
//	 
//	public override void OnInspectorGUI()
//	{
//		base.OnInspectorGUI();
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Generate Snake Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.GenSnakeDungeon();
//		}
//		EditorGUILayout.EndHorizontal ();
//		
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Generate Line Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.GenLineDungeon();
//		}
//		EditorGUILayout.EndHorizontal ();
//		
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Generate Pependecular Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.GenPerpendicularDungeon();
//		}
//		EditorGUILayout.EndHorizontal ();
//		
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Generate Broken Rec Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.GenBrokenRectangleDungeon();
//		}
//		EditorGUILayout.EndHorizontal ();
//		
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Generate Progression Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.GenProgressionDungeon(script.groundTransX,script.groundTransZ,-1,-1,-1,-1,-1,-1);
//		}
//		EditorGUILayout.EndHorizontal ();
//		
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Generate Normal Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.GenDungeon(script.groundTransX,script.groundTransZ,-1,-1,-1,-1,-1,-1);
//		}
//		EditorGUILayout.EndHorizontal ();
//		
//		EditorGUILayout.BeginHorizontal();
//		if (GUILayout.Button(new GUIContent("Clean Generated Dungeon"), GUILayout.Width(250)))
//		{
//			script = FindObjectOfType(typeof(DungeonGeneratorNavMeshSurface)) as DungeonGeneratorNavMeshSurface; 
//			script.cleanDungeons();
//		}
//		EditorGUILayout.EndHorizontal ();
//	}
//	
//	
//}
