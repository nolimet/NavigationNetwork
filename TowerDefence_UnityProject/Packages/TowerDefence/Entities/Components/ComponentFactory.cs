using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TowerDefence.Entities.Components.Data;
using Zenject;

namespace TowerDefence.Entities.Components
{
    internal class ComponentFactory
    {
        private readonly DiContainer diContainer;

        public async Task<IEnumerable<IComponent>> GetComponents(IEnumerable<ComponentData> componentDatas, Func<IComponent, Task<IComponent>> InitHandler)
        {
            List<IComponent> components = new();
            foreach (var componentData in componentDatas)
            {
                var component = componentData.DeserializeComponent();
                diContainer.Inject(component);

                components.Add(component);
            }

            List<Task> componentTasks = new();
            foreach (var component in components)
            {
                componentTasks.Add(InitHandler(component));
            }

            await componentTasks.WaitForAll();

            return components;
        }
    }
}