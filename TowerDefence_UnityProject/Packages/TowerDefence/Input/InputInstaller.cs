using UnityEngine;
using Zenject;

namespace TowerDefence.Input
{
    [CreateAssetMenu(fileName = "Input Installer", menuName = "Installers/Input Installer")]
    public sealed class InputInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SelectionInputActions>().AsSingle();
            Container.Bind<DebugActions>().AsSingle();
        }
    }
}