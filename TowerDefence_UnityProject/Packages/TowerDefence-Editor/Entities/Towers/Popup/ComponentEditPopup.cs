using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Popup
{
    internal class ComponentEditPopup : PopupWindowContent
    {
        public readonly DisplayData displayData;
        private string jsondata;

        public ComponentEditPopup(DisplayData displayData)
        {
            this.displayData = displayData;
            jsondata = displayData.displayJson;
        }

        public override void OnGUI(Rect rect)
        {
            using (var h1 = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Save"))
                {
                    displayData.displayJson = jsondata;
                    displayData.ComponentFromJson();
                    displayData.UpdateTowerComponentData();

                    editorWindow.Close();
                }

                if (GUILayout.Button("Cancel"))
                {
                    editorWindow.Close();
                }
                GUILayout.FlexibleSpace();
            }

            jsondata = EditorGUILayout.TextArea(jsondata);
        }
    }
}