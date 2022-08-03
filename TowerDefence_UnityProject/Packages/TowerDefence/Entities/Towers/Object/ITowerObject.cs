using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    public interface ITowerObject : IEntityObjectBase
    {
        ITowerModel Model { get; }

        Vector2Int GetGridPosition();
    }
}