using System.Collections.Generic;
using System.IO;
using NoUtil.Debugging;
using NoUtil.Extensions;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.Systems.WorldLoader.Models;
using TowerDefence.World.Grid;
using UnityEngine;

namespace TowerDefence.Systems.WorldLoader
{
    public sealed class WorldLoadController
    {
        private readonly GridWorld gridWorld;
        private readonly IWorldDataModel worldDataModel;

        internal WorldLoadController(GridWorld gridWorld, IWorldDataModel worldDataModel)
        {
            this.gridWorld = gridWorld;
            this.worldDataModel = worldDataModel;
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

        public async void StartLevelLoading()
        {
            "Starting world loading".QuickCLog("World Builder");
            string filePath = FormatWorldName();
            if (!filePath.FromPath(out LevelData lvlData))
            {
                $"No level {filePath}".QuickCLog("World Builder", LogType.Error);
                return;
            }

            if (lvlData.GridSettings.HasValue)
                await gridWorld.CreateWorld(lvlData.GridSettings.Value);
            else "There where no grid settings".QuickCLog("World builder", LogType.Error);

            worldDataModel.Waves = lvlData.Waves;

            string FormatWorldName()
            {
                return Path.Combine(GetLevelFolder(), $"{worldDataModel.LevelName}.{worldDataModel.LevelType}");
            }
        }

        private static string GetLevelFolder() => Path.Combine(Application.streamingAssetsPath, "Levels");
    }
}