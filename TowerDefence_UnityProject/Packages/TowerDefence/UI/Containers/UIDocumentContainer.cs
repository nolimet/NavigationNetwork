using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI.Containers
{
    internal class UIDocumentContainer : MonoBehaviour, IUIContainer
    {
        protected IUIContainers Containers;

        [field: SerializeField] public UIDocument Document { get; protected set; }

        [field: SerializeField] public string Name { get; private set; }

        public virtual VisualElement VisualRoot => Document.rootVisualElement;

        [Inject]
        public void Inject(IUIContainers containers)
        {
            Containers = containers;
        }

        protected virtual void Start()
        {
            Containers.Containers.Add(this);
        }

        protected virtual void OnDestroy()
        {
            Containers.Containers.Remove(this);
        }
    }
}