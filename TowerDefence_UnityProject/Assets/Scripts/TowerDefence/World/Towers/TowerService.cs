using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TowerDefence.World.Towers
{
    public class TowerService : ITickable
    {
        private readonly List<TowerBase> towers = new List<TowerBase>();
        private readonly DiContainer diContainer;
        private readonly ResolveProvider resolveProvider;

        private readonly TowerConfigurationData towerConfiguration;
        private readonly WorldContainer worldContainer;

        public TowerService(DiContainer diContainer, TowerConfigurationData towerConfiguration, WorldContainer worldContainer)
        {
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
                towers.Add(newTower);
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
            if (tower && towers.Contains(tower))
            {
                tower.Destroy();
                towers.Remove(tower);
            }
        }

        public void DestroyAllTowers()
        {
            foreach (var tower in towers)
            {
                tower.Destroy();
            }
            towers.Clear();
        }

        public void Tick()
        {
            for (int i = towers.Count - 1; i >= 0; i--)
            {
                var tower = towers[i];
                if (!tower)
                {
                    towers.Remove(tower);
                    continue;
                }

                tower.Tick();
            }
        }
    }
}