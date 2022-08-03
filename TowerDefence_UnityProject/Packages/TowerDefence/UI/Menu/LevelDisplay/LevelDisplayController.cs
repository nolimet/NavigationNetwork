using DataBinding;
using System;
using TowerDefence.Systems.WorldLoad;
using TowerDefence.UI.Models;

namespace TowerDefence.UI.Menu.LevelDisplay
{
    internal class LevelDisplayController : IDisposable
    {
        private readonly WorldLoadController worldLoadController;
        private readonly BindingContext bindingContext = new(true);

        public LevelDisplayController(WorldLoadController worldLoadController, IUIContainers uiContainers)
        {
            this.worldLoadController = worldLoadController;

            //bindingContext.Bind<IUIContainers>(uiContainers, x => x.UIContainers);
        }

        public void Dispose() => bindingContext.Dispose();
    }
}