using DataBinding;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence.World.Towers.Models;
using UnityEngine;
using Zenject;

namespace TowerDefence.World.Towers
{
    public class TowerService : ITickable
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
                var newTower = gameObject.GetComponent<TowerBase>();
                var newTowerModel = ModelFactory.Create<ITowerModel>();
                newTowerModel.TowerRenderer = newTower;
                newTowerModel.Position = position;

                newTower.Setup(newTowerModel);

                towerModel.Towers.Add(newTowerModel);
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
            if (tower && towerModel.Towers.Any(x => x is T && x == tower))
            {
                var model = towerModel.Towers.First(x => x == tower);
                tower.Destroy();
                towerModel.Towers.Remove(model);
            }
        }

        public void DestroyAllTowers()
        {
            foreach (var tower in towerModel.Towers)
            {
                tower.TowerRenderer.Destroy();
            }

            towerModel.SelectedTower = null;
            towerModel.Towers.Clear();
        }

        public void Tick()
        {
            for (int i = towerModel.Towers.Count - 1; i >= 0; i--)
            {
                var tower = towerModel.Towers[i];
                if (!tower.TowerRenderer)
                {
                    towerModel.Towers.Remove(tower);
                    continue;
                }

                tower.TowerRenderer.Tick();
            }
        }
    }
}