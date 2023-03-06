using System;
using System.IO;
using NoUtil.Extensions;
using UnityEngine;

namespace TowerDefence.Systems.WorldLoader.Data
{
    public readonly struct LevelMetadata
    {
        public const string RelativePath = "LevelData.json";
        public readonly string DisplayName;
        public readonly string RelativeLevelPath;

        public LevelMetadata(string displayName, string relativeLevelPath)
        {
            DisplayName = displayName;
            RelativeLevelPath = relativeLevelPath;
        }

        public static LevelMetadata[] LoadLevels()
        {
            string path = Path.Combine(Application.streamingAssetsPath, RelativePath);
            return path.FromPath(out LevelMetadata[] result) ? result : Array.Empty<LevelMetadata>();
        }
    }
}