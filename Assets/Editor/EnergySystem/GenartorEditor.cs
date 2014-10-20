using UnityEngine;
using System.Collections;

using UnityEditor;

namespace EnergyNet._Editor
{
    [CustomEditor(typeof(EnergyGenator))]
    public class GenartorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EnergyGenator gen = (EnergyGenator)target;

            EditorGUILayout.LabelField("ID: " + gen.ID);
            EditorGUILayout.Space();
            gen.Storage = EditorGUILayout.FloatField("Storage", gen.Storage);
            gen.MaxStorage = EditorGUILayout.IntField("MaxStorage", gen.MaxStorage);
            gen.transferRate = EditorGUILayout.IntField("TransferRate", gen.transferRate);
            gen.Range = EditorGUILayout.IntField("Range ", gen.Range);

            EditorGUILayout.Space();
            gen.useFuel = EditorGUILayout.Toggle("Use Fuel", gen.useFuel);
            if (gen.useFuel)
            {
                if(GUILayout.Button("Refill fuel"))
                    gen.Fuel=gen.maxFuel;
                if(gen.Fuel!=0&&gen.maxFuel!=0)
                 ProgressBar(gen.Fuel / gen.maxFuel,"Fuel: " + gen.Fuel.ToString());
                gen.Fuel = EditorGUILayout.FloatField("Fuel", gen.Fuel);
                gen.maxFuel = EditorGUILayout.IntField("MaxFuel", gen.maxFuel);
                gen.EnergyDensity = EditorGUILayout.FloatField("EnergyDensity", gen.EnergyDensity);

                EditorGUILayout.Space();
                gen.activated = EditorGUILayout.Toggle("Activate Genartor", gen.activated);
                gen.generation = EditorGUILayout.IntField("generation", gen.generation);
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