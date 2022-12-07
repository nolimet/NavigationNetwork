using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI.Containers
{
    internal class UIDocumentContainer : MonoBehaviour, IUIContainer
    {
        protected IUIContainers containers;

        [field: SerializeField] public UIDocument Document { get; protected set; }

        [field: SerializeField] public string Name { get; private set; }

        public virtual VisualElement VisualRoot => Document.rootVisualElement;

        [Inject]
        public void Inject(IUIContainers containers)
        {
            this.containers = containers;
        }

        protected virtual void Start()
        {
            containers.Containers.Add(this);
        }

        protected virtual void OnDestroy()
        {
            containers.Containers.Remove(this);
        }
    }
}