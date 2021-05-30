using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TowerDefence.World.Towers
{
    public class TowerService : ITickable
    {
        private readonly List<TowerBase> towers = new List<TowerBase>();
        private readonly IInstantiator instantiator;

        public TowerService(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public T PlaceTower<T>(T towerPrefab) where T : TowerBase
        {
            if (towerPrefab == null || !towerPrefab)
            {
                throw new System.NullReferenceException("towerPrefab was not set! Set the prefab in the installer or check if it's corrupted");
            }

            var newTower = instantiator.InstantiatePrefabForComponent<T>(towerPrefab);
            towers.Add(newTower);

            return newTower;
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
            }
        }

        public void DestroyAllTowers()
        {
            foreach (var tower in towers)
            {
                DestroyTower(tower);
            }
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