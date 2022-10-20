using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Towers.Components.AttackVisualizers
{
    [Serializable, Component(ComponentType.Tower, AllowMultiple = true)]
    [JsonObject(MemberSerialization.OptIn)]
    public class LineAttackVisualizer : BaseAttackVisualizer, ITickableComponent
    {
        [JsonProperty] [SerializeField] private AssetReferenceT<GameObject> attackVisual;
        [JsonProperty] private float decayDuration;
        [JsonProperty] private Color attackColor;

        private readonly List<AttackVisual> attackVisuals = new();
        
        public override async Task AsyncPostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            await base.AsyncPostInit(towerObject, towerModel);
            await attackVisual.LoadAssetAsync();
        }

        protected override void OnTargetsDamaged(IEnumerable<IEnemyObject> targets)
        {
            var startPos = TowerObject.GetWorldPosition();
            var visualPrefab = attackVisual.Asset as GameObject;
            foreach (var target in targets)
            {
                var newVisual = Object.Instantiate(visualPrefab, Vector3.zero, Quaternion.identity);
                var lineRender = newVisual.GetComponent<LineRenderer>();
                
                lineRender.SetPosition(0,startPos);
                lineRender.SetPosition(1, target.GetWorldPosition());
                
                attackVisuals.Add(new(decayDuration, target, lineRender, attackColor));
            }
        }

        public void Tick()
        {
            float delta = Time.deltaTime;
            for (int i = attackVisuals.Count - 1; i >= 0; i--)
            {
                var visual = attackVisuals[i];
                if (!visual.Alive)
                {
                    attackVisuals.Remove(visual);
                    continue;
                }

                visual.Tick(delta);
            }
        }

        private class AttackVisual
        {
            public AttackVisual(float maxTime, IEnemyObject target, LineRenderer renderer, Color color)
            {
                timeLeft = this.maxTime = maxTime;
                this.renderer = renderer;
                this.target = target;

                renderer.material.SetColor(colorShaderProperty, color);
            }
            
            public bool Alive = true;
            
            private float timeLeft;
            private readonly float maxTime;

            private readonly LineRenderer renderer;
            private readonly IEnemyObject target;
            
            private static readonly int alphaShaderProperty = Shader.PropertyToID("alpha");
            private static readonly int colorShaderProperty = Shader.PropertyToID("color");

            public void Tick(float delta)
            {
                timeLeft -= delta;
                renderer.material.SetFloat(alphaShaderProperty, 1f / maxTime * timeLeft);
                if(target.ExistsInWorld)
                renderer.SetPosition(1, target.GetWorldPosition());
                
                if (!(timeLeft <= 0)) return;
                Object.Destroy(renderer.gameObject);
                Alive = false;
            }
        }
    }
}