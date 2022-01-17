using DataBinding;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.World;
using UnityEngine;
using Zenject;

namespace TowerDefence.Entities.Towers
{
    public class TowerService
    {
        private readonly ITowerModels towerModel;
        private readonly DiContainer diContainer;

        private readonly TowerConfigurationData towerConfiguration;
        private readonly WorldContainer worldContainer;

        public TowerService(DiContainer diContainer, ITowerModels towerModel, TowerConfigurationData towerConfiguration, WorldContainer worldContainer)
        {
            this.towerModel = towerModel;
            this.diContainer = diContainer;
            this.towerConfiguration = towerConfiguration;
            this.worldContainer = worldContainer;
        }

        public async Task<TowerBase> PlaceTower(string towerID, Vector2 position)
        {
            var assetReference = towerConfiguration.GetTower(towerID);
            if (assetReference == null)
            {
                throw new System.NullReferenceException("Tower ID seems to be invalid! Case does not matter just check the spelling or if it exists in the configuration data for the towers");
            }

            var newTowerObject = await assetReference.InstantiateAsync(position, Quaternion.identity, worldContainer.TurretContainer);
            diContainer.Inject(newTowerObject);

            if (newTowerObject is GameObject gameObject)
            {
                var newTower = gameObject.GetComponent<TowerObject>() ?? gameObject.AddComponent<TowerObject>();
                var newTowerModel = ModelFactory.Create<ITowerModel>();
                newTower.Setup(newTowerModel);

                towerModel.Towers.Add(newTower);
                return gameObject.GetComponent<TowerBase>();
            }
            return null;
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