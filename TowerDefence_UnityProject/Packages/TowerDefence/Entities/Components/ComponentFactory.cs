using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TowerDefence.Entities.Components.Data;

namespace TowerDefence.Entities.Components
{
    internal class ComponentFactory
    {
        public async Task<IEnumerable<IComponent>> GetComponents(IEnumerable<ComponentData> componentDatas, Func<IComponent,Task<IComponent>> InitHandler)
        {
            List<IComponent> components = new(); 
            foreach (var componentData in componentDatas)
            {
                components.Add(componentData.DeserializeComponent());
            }

            List<Task> componentTasks = new();
            foreach(var component in components)
            {
                componentTasks.Add(InitHandler(component));
            }

            await componentTasks.WaitForAll();

            return components;
        }
    }
}