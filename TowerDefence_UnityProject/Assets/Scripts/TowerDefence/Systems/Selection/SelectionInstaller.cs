using UnityEngine;
using Zenject;

namespace TowerDefence.Systems.Selection
{
    [CreateAssetMenu(fileName = "Selection Installer", menuName = "Installers/Selection Installer")]
    public class SelectionInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            if (!Container.HasBinding<Input.SelectionInputActions>())
            {
                Debug.LogError("Make sure this context can reach in input Installer");
            }

            Container.Bind<SelectionController>().AsSingle().NonLazy();
        }
    }
}