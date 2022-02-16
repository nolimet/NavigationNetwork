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
            foreach (var tower in towerModels.Towers)
            {
                tower.Tick();
            }
        }
    }
}