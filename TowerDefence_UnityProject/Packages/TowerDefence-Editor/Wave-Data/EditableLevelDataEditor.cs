using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.World.Path.Data;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.EditorScripts.Systems.Waves.Data
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

        private readonly GUIContent loadGridFromImage = new("Load grid from image", "Converts a image to GridSettings");

        public void OnEnable()
        {
            waves = serializedObject.FindProperty("waves");
            pathdata = serializedObject.FindProperty("pathData").FindPropertyRelative("pathPoints");
            gridSettingData = serializedObject.FindProperty("gridSettings");
        }

        public override void OnInspectorGUI()
        {
            if (target is not EditableLevelData editableLevel) return;

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Load from Json"))
                {
                    string path = EditorUtility.OpenFilePanelWithFilters("Select json", Application.dataPath, new[] { "LevelData", "json,lvl" });
                    if (File.Exists(path))
                    {
                        editableLevel.FromLevelData(JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path)));

                        serializedObject.Update();
                    }
                }

                if (GUILayout.Button(savePathToJson))
                {
                    SavePathData("json", Formatting.Indented);
                }

                if (GUILayout.Button(savePathToLvL))
                {
                    SavePathData("lvl", Formatting.None);
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
                    SaveGridData("json", Formatting.Indented);
                }

                if (GUILayout.Button(saveGridToLvL))
                {
                    SaveGridData("lvl", Formatting.None);
                }

                if (GUILayout.Button(loadGridFromImage))
                {
                    LoadGridDataFromImage();
                }

                GUILayout.FlexibleSpace();
            }


            using (var changedScope = new EditorGUI.ChangeCheckScope())
            {
                DrawPathData();

                EditorGUILayout.PropertyField(waves);
                EditorGUILayout.PropertyField(gridSettingData);
                UpdateGridSettings();

                if (changedScope.changed)
                {
                    serializedObject.ApplyModifiedProperties();
                }
            }

            void UpdateGridSettings()
            {
                var width = gridSettingData.FindPropertyRelative("GridWidth");
                var height = gridSettingData.FindPropertyRelative("GridHeight");
                var nodes = gridSettingData.FindPropertyRelative("nodes");
                nodes.arraySize = height.intValue * width.intValue;
            }

            void DrawPathData()
            {
                var pathPoints = editableLevel.PathData.PathPoints;
                var points = pathPoints.Where(x => x.Type != PointType.Entrance).ToArray();
                for (int i = 0; i < pathdata.arraySize; i++)
                {
                    DrawPathPoint(pathdata.GetArrayElementAtIndex(i), i);
                }

                void DrawPathPoint(SerializedProperty point, int index)
                {
                    var pointData = pathPoints[index];
                    if (!(point.isExpanded = EditorGUILayout.Foldout(point.isExpanded, $"{index} - {pointData.Type}"))) return;

                    using var indent = new EditorGUI.IndentLevelScope(1);

                    var pointName = point.FindPropertyRelative("name");
                    var position = point.FindPropertyRelative("position");
                    var type = point.FindPropertyRelative("type");
                    var connections = point.FindPropertyRelative("connections");

                    EditorGUILayout.PropertyField(pointName);
                    EditorGUILayout.PropertyField(position);
                    EditorGUILayout.PropertyField(type);

                    using var disableGroup = new EditorGUI.DisabledScope(false);

                    EditorGUILayout.TextField("Id", pointData.ID);

                    if (!(connections.isExpanded = EditorGUILayout.Foldout(connections.isExpanded, "Connections"))) return;

                    using var indent2 = new EditorGUI.IndentLevelScope(1);
                    using var h1 = new EditorGUILayout.HorizontalScope();

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

                    if (pointData.Connections is not { Length: > 0 }) return;

                    var usableConnections = points.Where(x => x.ID != pointData.ID).ToList();
                    for (int i = 0; i < pointData.Connections.Length; i++)
                    {
                        var connection = pointData.Connections[i];
                        int currentID = usableConnections.FindIndex(x => x.ID == connection);

                        int newID = EditorGUILayout.Popup(currentID, usableConnections.Select(x => x.Name).ToArray()); //Display names instead of ids
                        if (newID != -1)
                            pointData.Connections[i] = usableConnections[newID].ID;
                    }
                }
            }
        }

        private void SavePathData(string extension, Formatting formatting)
        {
            if (!target || target == null || target is not EditableLevelData levelData)
            {
                Debug.LogError("Target is null writing failed");
                return;
            }

            string path = EditorUtility.SaveFilePanel("Select Save Location", Application.dataPath, target.name, extension);
            var file = new FileInfo(path);
            if (!file.Directory!.Exists)
            {
                file.Directory.Create();
            }

            if (file.Exists)
            {
                file.Delete();
            }

            using (var stream = File.CreateText(path))
            {
                stream.Write(JsonConvert.SerializeObject(levelData.ToLevelDataPath(), formatting));
            }

            AssetDatabase.Refresh();
        }

        private void SaveGridData(string extension, Formatting formatting)
        {
            if (!target || target == null || target is not EditableLevelData levelData)
            {
                Debug.LogError("Target is null writing failed");
                return;
            }

            string basePath = Path.Combine(Application.streamingAssetsPath, "Levels");
            string path = EditorUtility.SaveFilePanel("Select Save Location", basePath, target.name, extension);
            var file = new FileInfo(path);
            if (!file.Directory!.Exists)
            {
                file.Directory.Create();
            }

            if (file.Exists)
            {
                file.Delete();
            }

            using (var stream = File.CreateText(path))
            {
                stream.Write(JsonConvert.SerializeObject(levelData.ToLevelDataGrid(), formatting));
            }

            AssetDatabase.Refresh();
        }

        private void LoadGridDataFromImage()
        {
            string path = EditorUtility.OpenFilePanelWithFilters("Select GridSettings Image", Application.streamingAssetsPath, new[] { "png", "png", "jpg", "jpg" });
            var settings = GridSettingsImageImporter.Convert(path);

            var levelData = target as EditableLevelData;
            levelData!.FromGridSettings(settings);
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            serializedObject.Update();
        }
    }
}