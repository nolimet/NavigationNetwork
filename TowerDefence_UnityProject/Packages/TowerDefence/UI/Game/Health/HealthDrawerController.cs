﻿using System.Collections.Generic;
using System.Linq;
using DataBinding;
using NoUtil.Debugging;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Enemies.Models;
using Zenject;

namespace TowerDefence.UI.Game.Health
{
    public sealed class HealthDrawerController : ITickable
    {
        private readonly HealthDrawer.Factory healthDrawerFactory;
        private readonly List<HealthDrawer> healthBars = new();
        private readonly BindingContext bindingContext = new();

        public HealthDrawerController(HealthDrawer.Factory healthDrawerFactory, IEnemiesModel enemiesModel)
        {
            this.healthDrawerFactory = healthDrawerFactory;

            bindingContext.Bind(enemiesModel, x => x.Enemies, OnEnemiesChanged);
        }

        ~HealthDrawerController()
        {
            "Disposed".QuickCLog("HealthBarController");
            bindingContext.Dispose();
        }

        private void OnEnemiesChanged(IList<IEnemyObject> obj)
        {
            var newEnemies = obj.Where(x => !x.Model.HealthBar);
            var deadHealthBars = healthBars.Where(x => !x);

            if (deadHealthBars.Any())
            {
                foreach (var deadHealthbar in deadHealthBars.ToArray())
                {
                    healthBars.Remove(deadHealthbar);
                }
            }

            foreach (var enemy in newEnemies)
            {
                var newHealthBar = healthDrawerFactory.Create(enemy, OnHeathbarDestroyed);
                enemy.Model.HealthBar = newHealthBar;
                healthBars.Add(newHealthBar);
            }
        }

        private void OnHeathbarDestroyed(HealthDrawer arg0)
        {
            healthBars.Remove(arg0);
        }

        public void Tick()
        {
            foreach (HealthDrawer healthBar in healthBars)
            {
                healthBar.UpdateHealthBar();
            }
        }
    }
}