using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TowerDefence.Entities.Components.Data;
using Zenject;

namespace TowerDefence.Entities.Components
{
    internal sealed class ComponentFactory
    {
        private readonly DiContainer diContainer;

        public ComponentFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public async Task<IEnumerable<IComponent>> GetComponents(IEnumerable<ComponentData> componentDatas, Func<IComponent, UniTask<IComponent>> initHandler)
        {
            List<IComponent> components = new();
            foreach (var componentData in componentDatas)
            {
                var component = componentData.DeserializeComponent();
                diContainer.Inject(component);

                components.Add(component);
            }

            List<UniTask> componentTasks = new();
            foreach (var component in components)
            {
                componentTasks.Add(initHandler(component));
            }

            await UniTask.WhenAll(componentTasks);

            return components;
        }
    }
}