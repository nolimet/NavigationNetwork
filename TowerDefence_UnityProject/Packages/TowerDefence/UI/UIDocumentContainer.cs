using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI
{
    internal sealed class UIDocumentContainer : MonoBehaviour, IUIContainer
    {
        private IUIContainers containers;

        [field: SerializeField] public UIDocument Document { get; private set; }

        [field: SerializeField] public string Name { get; private set; }

        [Inject]
        public void Inject(IUIContainers containers)
        {
            this.containers = containers;
            this.containers.Containers.Add(this);
        }

        private void OnDestroy()
        {
            containers.Containers.Remove(this);
        }
    }
}