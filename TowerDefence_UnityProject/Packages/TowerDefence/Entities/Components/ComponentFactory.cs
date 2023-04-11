using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TowerDefence.Entities.Components.Data;
using UnityEngine;
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

        public async Task<IEnumerable<IComponent>> GetComponents(IEnumerable<ComponentData> componentDatas, Action<List<IComponent>> assign, Func<IComponent, UniTask<IComponent>> initHandler)
        {
            List<IComponent> components = new();
            foreach (var componentData in componentDatas)
            {
                var component = componentData.DeserializeComponent();
                try
                {
                    if (component is null)
                        throw new NullReferenceException($"Component is null. Should be of type {componentData.Type}");
                    diContainer.Inject(component);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    throw;
                }

                components.Add(component);
            }

            assign(components);
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