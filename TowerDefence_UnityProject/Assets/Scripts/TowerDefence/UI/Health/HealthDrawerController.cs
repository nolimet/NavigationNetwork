using DataBinding;
using NoUtil.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Health
{
    public class HealthDrawerController : ITickable
    {
        private readonly HealthDrawer.Factory healthDrawerFactory;
        private readonly IEnemiesModel enemiesModel;
        private readonly List<HealthDrawer> healthBars = new List<HealthDrawer>();
        private readonly BindingContext bindingContext = new(true);

        public HealthDrawerController(HealthDrawer.Factory healthDrawerFactory, IEnemiesModel enemiesModel)
        {
            this.healthDrawerFactory = healthDrawerFactory;
            this.enemiesModel = enemiesModel;

            bindingContext.Bind(enemiesModel, x => x.Enemies, OnEnemiesChanged);
        }

        ~HealthDrawerController()
        {
            "Disposed".QuickCLog("HealthBarController");
            bindingContext.Dispose();
        }

        private void OnEnemiesChanged(IList<IEnemyBase> obj)
        {
            var newEnemies = obj.Where(x => !x.healthBar);
            var removedEnemies = healthBars.Where(x => !x.TargetIsAlive());

            if (removedEnemies.Count() > 0)
            {
                foreach (var deadHealthbar in removedEnemies.ToArray())
                {
                    healthBars.Remove(deadHealthbar);
                }
            }

            foreach (var enemy in newEnemies)
            {
                var newHealthBar = healthDrawerFactory.Create(enemy);
                enemy.healthBar = newHealthBar;
                healthBars.Add(newHealthBar);
            }
        }

        public void Tick()
        {
            TrimDrawers();
            foreach (HealthDrawer healthBar in healthBars)
            {
                healthBar.UpdateHealthBar();
            }
        }

        private void TrimDrawers()
        {
            if (healthBars.Any(x => enemiesModel.Enemies.All(y => y.healthBar != x)))
            {
                "Triming Healthbars".QuickCLog("HealthBarController");

                var deadDrawers = healthBars.Where(x => !x.TargetIsAlive()).ToArray();
                foreach (var deadDrawer in deadDrawers)
                {
                    deadDrawer.Destroy();
                    healthBars.Remove(deadDrawer);
                }
            }
        }
    }
}