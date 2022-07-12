using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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

        public async void LoadLevel(string level, LevelType type)
        {
            var file = new FileInfo(FormatWorldName());
            if (file.Exists)
            {

                string json;
                using (var reader = file.OpenText())
                {
                    json = reader.ReadToEnd();
                }
                var lvlData = JsonConvert.DeserializeObject<LevelData>(json);

                if (lvlData.gridSettings.HasValue)
                    await gridWorld.CreateWorld(lvlData.gridSettings.Value);
                else
                    Debug.LogError("There where on grid settings");

                waveController.SetWaves(lvlData.waves);
            }

            string FormatWorldName()
            {
                return Path.Combine(GetLevelFolder(), $"{level}.{type}");
            }
        }

        private string GetLevelFolder()
        {
            return Path.Combine(Application.streamingAssetsPath, "Levels");
        }
    }
}
