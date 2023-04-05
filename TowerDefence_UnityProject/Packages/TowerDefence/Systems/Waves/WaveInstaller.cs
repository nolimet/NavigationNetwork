﻿using Zenject;

namespace TowerDefence.Systems.Waves
{
    public sealed class WaveInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WaveController>().AsSingle();
        }
    }
}