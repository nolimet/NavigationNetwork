using System.Collections.Generic;
using System.IO;
using System.Linq;
using NoUtil.Extensions;
using TowerDefence.Systems.WorldLoader.Data;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.EditorScripts
{
    public static class EditorFunctions
    {
        [MenuItem("Tools/TowerDefence/Export Levels")]
        public static void ExportLevels()
        {
            var paths = AssetDatabase.FindAssets("t:EditableLevelData").Select(AssetDatabase.GUIDToAssetPath).ToArray();
            Debug.Log(string.Join(",", paths));

            var levelAssets = paths.Select(AssetDatabase.LoadAssetAtPath<EditableLevelData>).ToArray();
            Debug.Log(levelAssets.Length);

            List<LevelMetadata> metadata = new();
            foreach (var level in levelAssets)
            {
                var gridData = level.ToLevelDataGrid();
                string relativePath = $"Levels/{level.name}.lvl";
                metadata.Add(new LevelMetadata(level.name, level.name));
                gridData.ToPath(Path.Combine(Application.streamingAssetsPath, relativePath));
            }

            metadata.ToPath(Path.Combine(Application.streamingAssetsPath, LevelMetadata.relativePath));
        }
    }
}