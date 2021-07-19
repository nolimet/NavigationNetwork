using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using UnityEngine;
using Zenject;

namespace TowerDefence.Installers
{
    [CreateAssetMenu(fileName = "Entities Installer", menuName = "Installers/Entities Installer")]
    public class EntitiesInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private EnemyConfigurationData enemyConfigurationData;

        public override void InstallBindings()
        {
            //Enemies
            Container.BindInstance(enemyConfigurationData).AsSingle();
            Container.Bind<EnemyController>().ToSelf().AsSingle();
        }
    }
}