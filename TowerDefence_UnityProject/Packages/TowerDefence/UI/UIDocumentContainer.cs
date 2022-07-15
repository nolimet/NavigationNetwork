using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI
{
    internal class UIDocumentContainer : MonoBehaviour, IUIContainer
    {
        [field: SerializeField]
        public UIDocument Document { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }

        [Inject]
        public void Inject(IUIContainers containers)
        {
            containers.Containers.Add(this);
        }
    }
}
