using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TowerDefence.World.Path.Data;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TowerDefence.Systems.Waves.Data
{
    [CustomEditor(typeof(EditableLevelData))]
    public class EditableLevelDataEditor : Editor
    {
        private SerializedProperty waves;
        private SerializedProperty pathdata;

        private GUIContent saveToJson = new GUIContent("Save To Json", "For development (indended json)");
        private GUIContent saveToLvL = new GUIContent("Save To lvl", "For production (single line json)");

        public void OnEnable()
        {
            waves = serializedObject.FindProperty("waves");
            pathdata = serializedObject.FindProperty("pathdata").FindPropertyRelative("pathPoints");
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
                        var editableLevel = target as EditableLevelData;
                        editableLevel.FromLevelData(JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path)));

                        serializedObject.Update();
                    }
                }

                if (GUILayout.Button(saveToJson))
                {
                    SaveData("json", Formatting.Indented);
                }

                if (GUILayout.Button(saveToLvL))
                {
                    SaveData("lvl", Formatting.None);
                }

                if (GUILayout.Button("Save File"))
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                }
                GUILayout.FlexibleSpace();
            }

            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                DrawPathData();
                EditorGUILayout.PropertyField(waves);

                if (changedScope.changed)
                {
                    serializedObject.ApplyModifiedProperties();
                }
            }

            void DrawPathData()
            {
                var pathPoints = (target as EditableLevelData).Pathdata.pathPoints;
                var points = pathPoints.Where(x => x.type != PointType.Entrance);
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
                            var connections = point.FindPropertyRelative("connections");

                            EditorGUILayout.PropertyField(name);
                            EditorGUILayout.PropertyField(position);
                            EditorGUILayout.PropertyField(type);

                            using (var disableGroup = new EditorGUI.DisabledScope(false))
                            {
                                EditorGUILayout.TextField("Id", pointData.id);

                                if (connections.isExpanded = EditorGUILayout.Foldout(connections.isExpanded, "Connections"))
                                {
                                    using (var indent2 = new EditorGUI.IndentLevelScope(1))
                                    {
                                        using (var h1 = new EditorGUILayout.HorizontalScope())
                                        {
                                            GUILayout.Space(28);
                                            if (GUILayout.Button("+"))
                                            {
                                                connections.arraySize++;
                                            }
                                            if (GUILayout.Button("-"))
                                            {
                                                connections.arraySize--;
                                                if (connections.arraySize < 0)
                                                    connections.arraySize = 0;
                                            }
                                            GUILayout.FlexibleSpace();
                                        }
                                        if (pointData.connections != null && pointData.connections.Length > 0)
                                        {
                                            var usableConnections = points.Where(x => x.id != pointData.id).ToList();
                                            for (int i = 0; i < pointData.connections.Length; i++)
                                            {
                                                var connection = pointData.connections[i];
                                                int currentID = usableConnections.FindIndex(x => x.id == connection);

                                                int newID = EditorGUILayout.Popup(currentID, usableConnections.Select(x => x.name).ToArray()); //Display names instead of ids
                                                if (newID != -1)
                                                    pointData.connections[i] = usableConnections[newID].id;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            void SaveData(string extension, Formatting formatting)
            {
                string path = EditorUtility.SaveFilePanel("Select Save Location", Application.dataPath, target.name, extension);
                var file = new FileInfo(path);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var stream = File.CreateText(path))
                {
                    stream.Write(JsonConvert.SerializeObject((target as EditableLevelData).ToLevelData(), formatting));
                }

                AssetDatabase.Refresh();
            }
        }
    }
}