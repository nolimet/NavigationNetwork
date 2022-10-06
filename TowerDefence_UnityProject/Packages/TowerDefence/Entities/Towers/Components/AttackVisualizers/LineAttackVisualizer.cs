using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Towers.Components.AttackVisualizers
{
    [Serializable, Component(ComponentType.Tower, AllowMultiple = true)]
    [JsonObject(MemberSerialization.OptIn)]
    public class LineAttackVisualizer : BaseAttackVisualizer
    {
        [JsonProperty] [SerializeField] private AssetReferenceT<GameObject> attackVisual;
        [JsonProperty] private float decayDuration;
        [JsonProperty] private Color attackColor;

        public override async Task AsyncPostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            await base.AsyncPostInit(towerObject, towerModel);
            await attackVisual.LoadAssetAsync();
        }

        protected override void OnTargetsDamaged(IEnumerable<IEnemyObject> targets)
        {
            Vector3 startPos = TowerObject.GetWorldPosition();
            var visualPrefab = attackVisual.Asset as GameObject;
            foreach (var target in targets)
            {
                var newVisual = Object.Instantiate(visualPrefab, Vector3.zero, Quaternion.identity);
                var lineRender = newVisual.GetComponent<LineRenderer>();
                
                lineRender.SetPosition(0,startPos);
                lineRender.SetPosition(1, target.GetWorldPosition());
                lineRender.startColor = lineRender.endColor = attackColor;
                
                Object.Destroy(newVisual, decayDuration);
            }
        }
    }
}