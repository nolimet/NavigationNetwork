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
            gen.MaxTicksTillSpawn = EditorGUILayout.IntField("Ticks between spawns",gen.MaxTicksTillSpawn);
            if (gen.MaxTicksTillSpawn <= 0)
            {
                gen.MaxTicksTillSpawn = 1;
            }

            ProgressBar((float)gen.waitedTicks / (float)gen.MaxTicksTillSpawn, "Spawn Progress");
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