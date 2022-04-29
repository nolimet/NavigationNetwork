using TowerDefence.Systems.Selection;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class SelectableCell : MonoBehaviour, ISelectable
    {
        public IGridCell GridNode { get; set; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            foreach (var cell in GridNode.ConnectedCells)
            {
                Gizmos.DrawSphere(cell.WorldPosition, 0.5f);
            }
        }
    }
}
