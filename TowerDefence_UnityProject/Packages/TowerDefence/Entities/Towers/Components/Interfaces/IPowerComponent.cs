﻿using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IPowerComponent : IComponent
    {
        void PowerTick(float delta);
    }
}