using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
namespace NavigationNetwork._Editor
{
    [CustomEditor(typeof(NavigationForcedConnectionNode))]
    public class ForecedConnectionNodeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            NavigationForcedConnectionNode node = (NavigationForcedConnectionNode)target;
            EditorGUILayout.LabelField("ID: " + node.ID.ToString());
            node.Range = EditorGUILayout.IntField("Range", node.Range);
            node.nonRecivend = EditorGUILayout.Toggle("Not Reciving", node.nonRecivend);
            node.isEndNode = EditorGUILayout.Toggle("IsEndNode", node.isEndNode);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Connections"), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
            if (node.Pull != null)
            {
                EditorGUILayout.LabelField("Number of Nodes : " + node.Pull.Count);
                if (node.Pull.Count > 0)
                {
                    foreach (KeyValuePair<int, structs.NavPullObject> keypair in node.Pull)
                    {
                        EditorGUILayout.LabelField("EndPoint ID: " + keypair.Key.ToString());
                        EditorGUILayout.LabelField("- Distance " + keypair.Value.Distance.ToString());
                        EditorGUILayout.LabelField("- Connecting Node ID " + keypair.Value.ClosestNode.ID.ToString());

                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("No End Points found...");
            }
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