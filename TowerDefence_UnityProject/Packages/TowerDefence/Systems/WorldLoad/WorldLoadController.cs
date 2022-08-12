using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using NoUtil.Debugging;
using NoUtil.Extensions;
using TowerDefence.Systems.Waves;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.World.Grid;
using UnityEngine;

namespace TowerDefence.Systems.WorldLoad
{
    internal class WorldLoadController
    {
        public enum LevelType
        {
            json,
            lvl
        }

        private readonly GridWorld gridWorld;
        private readonly WaveController waveController;
        private string relativeLevelPath;
        private LevelType levelType;
        
        public WorldLoadController(GridWorld gridWorld, WaveController waveController)
        {
            this.gridWorld = gridWorld;
            this.waveController = waveController;
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
            relativeLevelPath = level;
            levelType = type;
        }
        
        public async void StartLevelLoading()
        {
            string filePath = FormatWorldName();
            if(filePath.FromPath(out LevelData lvlData))
            {
                if (lvlData.gridSettings.HasValue)
                    await gridWorld.CreateWorld(lvlData.gridSettings.Value);
                else "There where no grid settings".QuickCLog("World builder", LogType.Error);

                waveController.SetWaves(lvlData.waves);
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