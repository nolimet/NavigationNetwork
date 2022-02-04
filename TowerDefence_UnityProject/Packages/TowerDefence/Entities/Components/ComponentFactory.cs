using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Components.Data;

namespace TowerDefence.Entities.Components
{
    internal class ComponentFactory
    {
        public IEnumerable<IComponent> GetComponents(IEnumerable<ComponentData> componentDatas, Func<IComponent,IComponent> InitHandler)
        {
            List<IComponent> components = new(); 
            foreach (var componentData in componentDatas)
            {
                components.Add(componentData.DeserializeComponent());
            }

            foreach(var component in components)
            {
                InitHandler(component);
            }

            return components;
        }
    }
}