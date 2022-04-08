using TowerDefence.Systems.Selection;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class SelectableNode : MonoBehaviour, ISelectable
    {
        public IGridNode GridNode { get; set; }
    }
}
