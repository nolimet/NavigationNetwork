using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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