using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridCreator))]
public class LaunchMazeInEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Maze myScript = (Maze)target;

        if (GUILayout.Button("Custom Button"))
        {
            myScript.StartCoroutine(myScript.Generate());
        }
    }
}
