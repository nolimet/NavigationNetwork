using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection;

namespace TowerDefence.Utility
{
    internal static class SelectionUtility
    {
        public static bool TryFindTower(this IList<ISelectable> selection, out ITowerObject towerObject)
        {
            return selection.TryFindObject(out towerObject);
        }

        public static bool TryFindEnemy(this IList<ISelectable> selection, out IEnemyObject enemyObject)
        {
            return selection.TryFindObject(out enemyObject);
        }

        public static bool TryFindObject<T>(this IList<ISelectable> selection, out T selected) where T : class, ISelectable
        {
            if (selection.Any(x => x is T))
            {
                selected = selection.First(x => x is T) as T;
                return true;
            }

            selected = null;
            return false;
        }

        public static bool TryGetComponent<T>(this IList<IComponent> components, out T component) where T : class, IComponent
        {
            if (components.Any(x => x is T))
            {
                component = components.First(x => x is T) as T;
                return true;
            }

            component = null;
            return false;
        }

        public static T GetComponent<T>(this IList<IComponent> components) where T : class, IComponent
        {
            return components.FirstOrDefault(x => x is T) as T;
        }
    }
}