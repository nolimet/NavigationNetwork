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
using TowerDefence.UI.Game.Tower.Properties.Attributes;
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
        [UIProperty("Cooldown", suffix: "s")] [JsonProperty]
        private readonly double fireCooldownInSeconds = .1f;

        [UIProperty] [JsonProperty] private readonly double damagePerShot = 5;
        [UIProperty] [JsonProperty] private readonly double powerPerShot = 20;
        [SerializeField] private AssetReferenceT<GameObject> bulletPrefabReference;
        [SerializeField] [Min(1)] private int maxTargetsPerShot = 1;
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
            this.towerObject = towerObject;

            var handle = bulletPrefabReference.LoadAssetAsync();
            await handle.Task;
            bulletPrefab = handle.Result;
        }

        public void Tick()
        {
            if (liveBullets.Count > 0)
                for (var i = liveBullets.Count - 1; i >= 0; i--)
                {
                    var liveBullet = liveBullets[i];
                    if (!liveBullet || !liveBullet.gameObject) liveBullets.Remove(liveBullet);
                }

            if (cooldownTimer >= 0f)
            {
                cooldownTimer -= Time.deltaTime;
                return;
            }

            if (targetFindComponent.FoundTargets.Count == 0)
                return;

            for (var i = 0; i < targetFindComponent.FoundTargets.Count; i++)
            {
                if (i > maxTargetsPerShot) break;
                var target = targetFindComponent.FoundTargets[i];
                if (!powerConsumer.TryConsume(powerPerShot)) break;

                FireTarget(target);
            }

            cooldownTimer = fireCooldownInSeconds;

            void FireTarget(IEnemyObject target)
            {
                var newBulletGameObject = Object.Instantiate(bulletPrefab, towerObject.GetWorldPosition(), Quaternion.identity);
                liveBullets.Add(newBulletGameObject);

                var movingProjectile = newBulletGameObject.GetComponent<MovingProjectile>();
                movingProjectile.Setup(target, damagePerShot, 20);
                target.Model.VirtualHealth -= damagePerShot;
            }
        }

        public void Dispose()
        {
            foreach (var liveBullet in liveBullets.Where(liveBullet => liveBullet && liveBullet.gameObject))
                Object.Destroy(liveBullet.gameObject);
        }
    }
}