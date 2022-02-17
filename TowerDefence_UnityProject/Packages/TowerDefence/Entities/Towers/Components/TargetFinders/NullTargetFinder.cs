using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    internal class NullTargetFinder : ITargetFindComponent
    {
        private NullTargetFinder()
        {

        }

        public static readonly NullTargetFinder Instance = new NullTargetFinder();
        public IEnumerable<IEnemyObject> FoundTargets => Array.Empty<IEnemyObject>();

        public void Tick()
        {
            
        }
    }
}
