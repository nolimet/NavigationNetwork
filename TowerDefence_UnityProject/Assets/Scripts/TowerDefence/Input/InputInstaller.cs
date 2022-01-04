using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TowerDefence.Input
{
    [CreateAssetMenu(fileName = "Input Installer", menuName = "Installers/Input Installer")]
    public class InputInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SelectionInputActions>().AsSingle();
        }
    }
}