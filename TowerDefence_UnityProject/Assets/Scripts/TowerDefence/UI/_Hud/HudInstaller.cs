using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.UI.Hud;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace TowerDefence.UI.Hud
{
    public class HudInstaller : MonoInstaller
    {
        [SerializeField]
        private TowerHudDrawer towerHud;

        public override void InstallBindings()
        {
            Container.BindInstance(towerHud).AsCached();
        }
    }
}