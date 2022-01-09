using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.World.Towers.Components;
using TowerDefence.World.Towers.Models;
using Zenject;

namespace TowerDefence.World.Towers
{
    public class TowerController : ITickable
    {
        private readonly ITowerModels towerModels;

        public TowerController(ITowerModels towerModels)
        {
            this.towerModels = towerModels;
        }

        public void Tick()
        {
            foreach (var model in this.towerModels.Towers)
            {
                foreach (var component in model.Components.Where(x => x is ITickableTowerComponent).Cast<ITickableTowerComponent>().OrderBy(x => x.TickPriority))
                {
                    component.Tick();
                }
            }
        }
    }
}