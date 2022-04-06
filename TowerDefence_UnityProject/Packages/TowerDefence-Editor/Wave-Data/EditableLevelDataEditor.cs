using Newtonsoft.Json;
using System.IO;
using System.Linq;
using TowerDefence.World.Path.Data;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Systems.Waves.Data
{
    [CustomEditor(typeof(EditableLevelData))]
    public class EditableLevelDataEditor : Editor
    {
        private SerializedProperty waves;
        private SerializedProperty pathdata;
        private SerializedProperty gridSettingData;

        private readonly GUIContent savePathToJson = new("Save Path To Json", "For development (indended json)");
        private readonly GUIContent savePathToLvL = new("Save Path To lvl", "For production (single line json)");

        private readonly GUIContent saveGridToJson = new("Save Grid To Json", "For development (indended json)");
        private readonly GUIContent saveGridToLvL = new("Save Grid To lvl", "For production (single line json)");

        public void OnEnable()
        {
            waves = serializedObject.FindProperty("waves");
            pathdata = serializedObject.FindProperty("pathdata").FindPropertyRelative("pathPoints");
            gridSettingData = serializedObject.FindProperty("gridSettings");
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

                if (GUILayout.Button(savePathToJson))
                {
                    SavePath("json", Formatting.Indented);
                }

                if (GUILayout.Button(savePathToLvL))
                {
                    SavePath("lvl", Formatting.None);
                }

                if (GUILayout.Button("Save File"))
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                }
                GUILayout.FlexibleSpace();
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(saveGridToJson))
                {
                    SaveGrid("json", Formatting.Indented);
                }

                if (GUILayout.Button(saveGridToLvL))
                {
                    SaveGrid("lvl", Formatting.None);
                }

                GUILayout.FlexibleSpace();
            }


            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                DrawPathData();

                EditorGUILayout.PropertyField(waves);
                DrawGridSettings();

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

            void DrawGridSettings()
            {
                if (gridSettingData.isExpanded = EditorGUILayout.Foldout(gridSettingData.isExpanded, gridSettingData.displayName))
                {
                    using (new EditorGUI.IndentLevelScope(1))
                    {
                        var width = gridSettingData.FindPropertyRelative("GridWidth");
                        var height = gridSettingData.FindPropertyRelative("GridHeight");
                        EditorGUILayout.PropertyField(height);
                        EditorGUILayout.PropertyField(width);

                        var layout = gridSettingData.FindPropertyRelative("gridLayout");
                        var nodes = layout.FindPropertyRelative("nodes");
                        if (nodes.isExpanded = EditorGUILayout.Foldout(nodes.isExpanded, nodes.displayName))
                        {
                            using (new EditorGUI.IndentLevelScope(1))
                            {
                                nodes.arraySize = height.intValue * width.intValue;
                                for (int i = 0; i < nodes.arraySize; i++)
                                {
                                    var node = nodes.GetArrayElementAtIndex(i);
                                    EditorGUILayout.PropertyField(node.FindPropertyRelative("weight"));
                                }
                            }
                        }
                    }
                }
            }

            void SavePath(string extension, Formatting formatting)
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

            void SaveGrid(string extension, Formatting formatting)
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
                    stream.Write(JsonConvert.SerializeObject((target as EditableLevelData).ToGridSettings(), formatting));
                }

                AssetDatabase.Refresh();
            }
        }
    }
}