using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    public interface ITowerObject : IEntityObjectBase
    {
        ITowerModel Model { get; }

        Vector2 GetGridPosition();
    }
}