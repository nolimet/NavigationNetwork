using System.Threading.Tasks;
using TowerDefence.World.Towers.Models;
using UnityEngine;
using Zenject;

namespace TowerDefence.World.Towers
{
    public class TowerService : ITickable
    {
        private readonly ITowerModel towerModel;
        private readonly DiContainer diContainer;

        private readonly TowerConfigurationData towerConfiguration;
        private readonly WorldContainer worldContainer;

        public TowerService(DiContainer diContainer, ITowerModel towerModel, TowerConfigurationData towerConfiguration, WorldContainer worldContainer)
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
                var newTower = gameObject.GetComponent<TowerBase>();
                towerModel.Towers.Add(newTower);
                return gameObject.GetComponent<TowerBase>();
            }
            return null;
        }

        public void DestroyTower<T>(T tower) where T : TowerBase
        {
            if (tower == null)
            {
                throw new System.NullReferenceException("tower is null");
            }
            if (tower && towerModel.Towers.Contains(tower))
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
            towerModel.Towers.Clear();
        }

        public void Tick()
        {
            for (int i = towerModel.Towers.Count - 1; i >= 0; i--)
            {
                var tower = towerModel.Towers[i];
                if (!tower)
                {
                    towerModel.Towers.Remove(tower);
                    continue;
                }

                tower.Tick();
            }
        }
    }
}