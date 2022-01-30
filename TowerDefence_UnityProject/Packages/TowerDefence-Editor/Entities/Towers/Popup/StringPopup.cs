using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Popup
{
    internal class StringPopup : PopupWindowContent
    {
        private readonly IEnumerable<string> values;
        public string SelectedValue { get; private set; } = string.Empty;
        public bool InputCancelled { get; private set; }

        private Vector2 scrollRectPosition = Vector2.zero;

        public StringPopup(IEnumerable<string> values)
        {
            this.values = values;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(400, 800);
        }

        public override void OnGUI(Rect rect)
        {
            using (var h1 = new EditorGUILayout.HorizontalScope())
            {
                using (var d1 = new EditorGUI.DisabledGroupScope(SelectedValue == string.Empty))
                {
                    if (GUILayout.Button("Confirm"))
                    {
                        InputCancelled = false;
                        editorWindow.Close();
                    }
                }

                if (GUILayout.Button("Cancel"))
                {
                    InputCancelled = true;
                    editorWindow.Close();
                }
            }
            using (var scrollRect = new EditorGUILayout.ScrollViewScope(scrollRectPosition))
            {
                foreach (string value in values)
                {
                    using (var disableGroup = new EditorGUI.DisabledGroupScope(value == SelectedValue))
                    {
                        if (GUILayout.Button(value))
                        {
                            SelectedValue = value;
                        }
                    }
                }

                scrollRectPosition = scrollRect.scrollPosition;
            }
        }
    }
}