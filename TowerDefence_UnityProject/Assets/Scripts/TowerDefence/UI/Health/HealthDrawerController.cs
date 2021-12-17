using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using Zenject;

namespace TowerDefence.UI.Health
{
    public class HealthDrawerController : ITickable
    {
        private readonly HealthDrawer.Factory healthDrawerFactory;
        private readonly List<HealthDrawer> healthDrawers = new();

        public HealthDrawerController(HealthDrawer.Factory healthDrawerFactory)
        {
            this.healthDrawerFactory = healthDrawerFactory;
        }

        public void AddHealthBar(EnemyBase enemyBase)
        {
            healthDrawers.Add(healthDrawerFactory.Create(enemyBase));
        }

        public void Tick()
        {
            TrimDrawers();
            foreach (HealthDrawer healthDrawer in healthDrawers)
            {
                healthDrawer.UpdateHealthBar();
            }
        }

        private void TrimDrawers()
        {
            if (healthDrawers.Any(x => !x.TargetIsAlive()))
            {
                var deadDrawers = healthDrawers.Where(x => !x.TargetIsAlive()).ToArray();
                foreach (var deadDrawer in deadDrawers)
                {
                    deadDrawer.Destroy();
                    healthDrawers.Remove(deadDrawer);
                }
            }
        }
    }
}