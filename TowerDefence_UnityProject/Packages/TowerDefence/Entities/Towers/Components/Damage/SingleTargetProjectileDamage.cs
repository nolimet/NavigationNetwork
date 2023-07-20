using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Damage.SubComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    [RequiredComponent(typeof(ITargetFindComponent), typeof(IPowerConsumer), typeof(TowerSettings))]
    [Component(ComponentType.Tower, typeof(IDamageComponent))]
    public class SingleTargetProjectileDamage : IDamageComponent, IAsyncInitializer, IDisposable
    {
        [JsonProperty] private readonly double fireCooldownInSeconds = .1f;
        [JsonProperty] private readonly double damagePerShot = 5;
        [JsonProperty] private readonly double powerPerShot = 20;
        [SerializeField] private AssetReferenceT<GameObject> bulletPrefabReference;

        private readonly List<GameObject> liveBullets = new();

        public event Action<IEnumerable<IEnemyObject>> AppliedDamageToTargets;
        public double DamagePerSecond => damagePerShot / fireCooldownInSeconds;

        private ITowerObject towerObject;
        private GameObject bulletPrefab;

        private TowerSettings towerSettings;
        private ITargetFindComponent targetFindComponent;
        private IPowerConsumer powerConsumer;
        private double cooldownTimer;

        public async Task AsyncPostInit(ITowerObject towerObject, ITowerModel model)
        {
            targetFindComponent = model.Components.GetComponent<ITargetFindComponent>();
            towerSettings = model.Components.GetComponent<TowerSettings>();
            powerConsumer = model.Components.GetComponent<IPowerConsumer>();

            var handle = bulletPrefabReference.LoadAssetAsync();
            await handle.Task;
            bulletPrefab = handle.Result;
        }

        public void Tick()
        {
            if (targetFindComponent.FoundTargets.Count == 0)
                return;

            if (cooldownTimer >= 0f)
            {
                cooldownTimer -= Time.deltaTime;
                return;
            }

            if (!powerConsumer.TryConsume(powerPerShot))
            {
                return;
            }

            cooldownTimer = fireCooldownInSeconds;
            var firstTarget = targetFindComponent.FoundTargets[0];
            var newBulletGameObject = Object.Instantiate(bulletPrefab);
            liveBullets.Add(newBulletGameObject);
            var movingProjectile = newBulletGameObject.GetComponent<MovingProjectile>();
            movingProjectile.Setup(firstTarget, damagePerShot, 20);
        }

        public void Dispose()
        {
            foreach (var liveBullet in liveBullets.Where(liveBullet => liveBullet && liveBullet.gameObject))
            {
                Object.Destroy(liveBullet.gameObject);
            }
        }
    }
}