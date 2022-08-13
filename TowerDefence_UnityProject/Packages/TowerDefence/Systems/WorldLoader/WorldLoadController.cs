using System.Collections.Generic;
using System.IO;
using NoUtil.Debugging;
using NoUtil.Extensions;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.World.Grid;
using TowerDefence.World.Grid.Models;
using UnityEngine;

namespace TowerDefence.Systems.WorldLoad
{
    internal class WorldLoadController
    {
        private readonly GridWorld gridWorld;
        private readonly IWorldData worldData;
        private string relativeLevelPath;
        private LevelType levelType;

        public WorldLoadController(GridWorld gridWorld, IWorldData worldData)
        {
            this.gridWorld = gridWorld;
            this.worldData = worldData;
        }

        public IReadOnlyCollection<string> GetLevels(LevelType type)
        {
            string path = GetLevelFolder();
            DirectoryInfo dir = new(path);
            List<string> files = new();

            foreach (var file in dir.EnumerateFiles($".{type}"))
            {
                files.Add(file.FullName);
            }

            return files.ToArray();
        }

        public void SetLevel(string level, LevelType type)
        {
            worldData.LevelName = level;
            levelType = type;
        }

        public async void StartLevelLoading()
        {
            string filePath = FormatWorldName();
            if (filePath.FromPath(out LevelData lvlData))
            {
                if (lvlData.gridSettings.HasValue)
                    await gridWorld.CreateWorld(lvlData.gridSettings.Value);
                else "There where no grid settings".QuickCLog("World builder", LogType.Error);

                worldData.Waves = lvlData.waves;
            }

            string FormatWorldName()
            {
                return Path.Combine(GetLevelFolder(), $"{relativeLevelPath}.{levelType}");
            }
        }

        private string GetLevelFolder()
        {
            return Path.Combine(Application.streamingAssetsPath, "Levels");
        }
    }
}