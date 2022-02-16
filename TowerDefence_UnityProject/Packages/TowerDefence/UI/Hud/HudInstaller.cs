using UnityEngine;
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