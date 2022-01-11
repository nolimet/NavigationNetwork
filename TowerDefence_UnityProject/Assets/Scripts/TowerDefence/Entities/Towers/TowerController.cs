using System.Linq;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Models;
using Zenject;

namespace TowerDefence.Entities.Towers
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