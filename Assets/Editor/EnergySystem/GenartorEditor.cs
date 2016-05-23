using UnityEngine;
using System.Collections;

using UnityEditor;

namespace NavigationNetwork._Editor
{
    [CustomEditor(typeof(NavigatorSpawnPoint))]
    public class SpawnPointEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            NavigatorSpawnPoint gen = (NavigatorSpawnPoint)target;

            EditorGUILayout.LabelField("ID: " + gen.ID);
            EditorGUILayout.Space();
            gen.Range = EditorGUILayout.IntField("Range ", gen.Range);
        }

        

        void ProgressBar(float value, string label)
        {
            // Get a rect for the progress bar using the same margins as a textfield:
            Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
            EditorGUI.ProgressBar(rect, value, label);
            EditorGUILayout.Space();
        }
    }
}