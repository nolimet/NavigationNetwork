using Newtonsoft.Json;
using System;
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

        public string[] GetLevels()
        {
            //TODO get all levels
            throw new NotImplementedException("TODO Implement getting all levels");
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
                return Path.Combine(Application.streamingAssetsPath, "Levels", $"{level}.{type}");
            }
        }
    }
}
