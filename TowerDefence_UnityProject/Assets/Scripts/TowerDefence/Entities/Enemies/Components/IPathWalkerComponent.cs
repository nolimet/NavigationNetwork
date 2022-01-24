using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Entities.Enemies.Components
{
    public interface IPathWalkerComponent : ITickableEnemyComponent
    {
        float PathProgress { get; }
    }
}