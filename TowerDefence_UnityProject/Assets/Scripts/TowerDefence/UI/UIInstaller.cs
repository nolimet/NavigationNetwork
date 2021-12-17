﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.UI.Health;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI
{
    [CreateAssetMenu(fileName = "UI Installer", menuName = "Installers/UI Installer")]
    public class UIInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private HealthDrawer healthbarPrefab;

        [SerializeField]
        private GameObject worldUIContainerPrefab;

        [SerializeField]
        private GameObject screenUIContainerPrefab;

        public override void InstallBindings()
        {
            var uiContainer = new GameObject("UI Container", typeof(UIContainer)).GetComponent<UIContainer>();
            var worldUIContainer = Instantiate(worldUIContainerPrefab, uiContainer.transform, false);
            var screenUIContainer = Instantiate(screenUIContainerPrefab, uiContainer.transform, false);

            uiContainer.Setup(worldUIContainer.transform, screenUIContainer.transform);

            Container.BindInstance(uiContainer).AsSingle();
            Container.BindInterfacesAndSelfTo<HealthDrawerController>().AsSingle();

            Container.BindFactory<HealthDrawer, HealthDrawer.Factory>().FromComponentInNewPrefab(healthbarPrefab);
        }
    }
}