using UnityEngine;
using System.Collections;
using UnityEditor;
using EnergyNet;
namespace EnergyNet._Editor
{
    [CustomEditor(typeof(EnergyNet.EnergyNode))]
    public class NodeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EnergyNode node = (EnergyNode)target;
            EditorGUILayout.LabelField("ID: " + node.ID.ToString());
            EditorGUILayout.LabelField("MaxStorage: " + node.MaxStorage.ToString());
            ProgressBar(node.Storage / node.MaxStorage, "Storage: " + node.Storage.ToString());
            EditorGUILayout.LabelField("Transfer Rate: " + node.transferRate.ToString());
            node.Range = EditorGUILayout.IntField("Range", node.Range);
            EditorGUILayout.Space();

            node.StaticPull = EditorGUILayout.Toggle("Static",node.StaticPull);
            if (node.StaticPull)
                node.Pull = EditorGUILayout.IntField("Pull",node.Pull);
            else
                EditorGUILayout.LabelField("Pull: " + node.Pull);
            EditorGUILayout.Space();

            node.nonRecivend = EditorGUILayout.Toggle("Not Reciving" ,node.nonRecivend);
            node.endPoint = EditorGUILayout.Toggle("End point",node.endPoint);
            if (node.endPoint)
                node.MaxStorage = 1000000;
               
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