using System.Linq;
using TowerDefence.Systems.WorldLoader;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.Systems.WorldLoader.Models;
using UnityEngine;
using Zenject;

namespace TowerDefence.Project
{
    public sealed class DefaultWorldLoader : MonoBehaviour
    {
        [Inject] private WorldLoadController worldLoadController;
        [Inject] private IWorldDataModel worldDataModel;

        private void Start()
        {
            if (string.IsNullOrEmpty(worldDataModel.LevelName))
            {
                worldDataModel.LevelType = LevelType.lvl;
                worldDataModel.LevelName = LevelMetadata.LoadLevels().First().RelativeLevelPath;
            }

            worldLoadController.StartLevelLoading();
        }
    }
}