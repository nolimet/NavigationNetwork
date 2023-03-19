using TowerDefence.Systems.Selection;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    public sealed class SelectableCell : MonoBehaviour, ISelectable
    {
        public IGridCell GridCell { get; set; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            foreach (var cell in GridCell.ConnectedCells)
            {
                Gizmos.DrawSphere(cell.WorldPosition, 0.5f);
            }
        }
    }
}