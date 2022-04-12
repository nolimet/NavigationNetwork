using TowerDefence.Systems.Selection;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class SelectableCell : MonoBehaviour, ISelectable
    {
        public IGridCell GridNode { get; set; }
    }
}
