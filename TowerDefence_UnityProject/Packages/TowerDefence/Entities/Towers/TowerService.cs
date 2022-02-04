using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Builder;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    public class TowerService
    {
        private readonly ITowerModels towerModel;

        private readonly TowerConfigurationData towerConfiguration;
        private readonly TowerFactory towerFactory;

        internal TowerService(ITowerModels towerModel, TowerConfigurationData towerConfiguration, TowerFactory towerFactory)
        {
            this.towerModel = towerModel;
            this.towerConfiguration = towerConfiguration;
            this.towerFactory = towerFactory;
        }

        public async Task<ITowerObject> PlaceTower(string towerID, Vector2 position)
        {
            var configuration = await towerConfiguration.GetTowerAsync(towerID);
            if (configuration == null)
            {
                throw new System.NullReferenceException("Tower ID seems to be invalid! Case does not matter just check the spelling or if it exists in the configuration data for the towers");
            }

            var newTower = await towerFactory.CreateTower(configuration, position);
            newTower.Transform.position = position;

            return newTower;
        }

        public void DestroyTower<T>(T tower) where T : ITowerObject
        {
            if (tower == null)
            {
                throw new System.NullReferenceException("tower is null");
            }

            if (towerModel.Towers.Contains(tower))
            {
                tower.Destroy();
                towerModel.Towers.Remove(tower);
            }
        }

        public void DestroyAllTowers()
        {
            foreach (var tower in towerModel.Towers)
            {
                tower.Destroy();
            }

            towerModel.SelectedTower = null;
            towerModel.Towers.Clear();
        }
    }
}