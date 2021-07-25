using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TowerDefence.Systems.Waves.Data
{
    [CustomEditor(typeof(EditableLevelData))]
    public class EditableLevelDataEditor : Editor
    {
        private ReorderableList waves;
        private SerializedProperty pathdata;

        public void OnEnable()
        {
            waves = new ReorderableList(serializedObject, serializedObject.FindProperty("waves"), true, true, true, true);
            pathdata = serializedObject.FindProperty("pathdata").FindPropertyRelative("pathPoints");
            waves.drawElementCallback += OnDrawWave;

            waves.drawHeaderCallback += (rect) => { EditorGUI.LabelField(rect, "Waves"); };
        }

        private void OnDrawWave(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.LabelField(rect, index.ToString());
        }

        public override void OnInspectorGUI()
        {
            using (var horizontalLayout = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Load from Json"))
                {
                    string path = EditorUtility.OpenFilePanelWithFilters("Select json", Application.dataPath, new[] { "LevelData", "json,lvl" });
                    if (File.Exists(path))
                    {
                        string jsonData = File.ReadAllText(path);
                        var levelData = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(jsonData);

                        var editableLevel = target as EditableLevelData;
                        var waveCount = levelData.waves?.Length ?? 0;
                        var waves = new EditableLevelData.EditableWave[waveCount];
                        for (int i = 0; i < waveCount; i++)
                        {
                            waves[i] = levelData.waves[i];
                        }
                        editableLevel.Waves = waves;
                        editableLevel.Pathdata = levelData.path;

                        serializedObject.Update();
                    }
                }

                if(GUILayout.Button("Save File"))
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                }
                GUILayout.FlexibleSpace();
            }


            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                DrawPathData();
                waves.DoLayoutList();
                if (changedScope.changed)
                {
                    serializedObject.ApplyModifiedProperties();
                }
            }

            void DrawPathData()
            {
                var pathPoints = (target as EditableLevelData).Pathdata.pathPoints;
                for (int i = 0; i < pathdata.arraySize; i++)
                {
                    DrawPathPoint(pathdata.GetArrayElementAtIndex(i), i);
                }

                void DrawPathPoint(SerializedProperty point, int index)
                {
                    var pointData = pathPoints[index];
                    if (point.isExpanded = EditorGUILayout.Foldout(point.isExpanded, $"{index} - {pointData.type}"))
                    {
                        using (var indent = new EditorGUI.IndentLevelScope(1))
                        {
                            var name = point.FindPropertyRelative("name");
                            var position = point.FindPropertyRelative("position");
                            var type = point.FindPropertyRelative("type");


                            EditorGUILayout.PropertyField(name);
                            EditorGUILayout.PropertyField(position);
                            EditorGUILayout.PropertyField(type);

                            using (var disableGroup = new EditorGUI.DisabledScope(true))
                            {
                                EditorGUILayout.TextField("Id", pointData.id);
                                if (pointData.connections != null && pointData.connections.Length > 0)
                                {
                                    if (name.isExpanded = EditorGUILayout.Foldout(name.isExpanded, "Connections"))
                                    {
                                        for (int i = 0; i < pointData.connections.Length; i++)
                                        {
                                            EditorGUILayout.TextField(i.ToString(), pointData.connections[i]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}