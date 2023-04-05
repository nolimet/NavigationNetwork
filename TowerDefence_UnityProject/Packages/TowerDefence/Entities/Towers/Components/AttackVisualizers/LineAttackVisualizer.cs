using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Towers.Components.AttackVisualizers
{
    [Serializable, Component(ComponentType.Tower, AllowMultiple = true)]
    [JsonObject(MemberSerialization.OptIn)]
    public class LineAttackVisualizer : BaseAttackVisualizer, ITickableComponent
    {
        [JsonProperty] [SerializeField] private AssetReferenceT<GameObject> attackVisual;

        [JsonProperty] private float decayDelay = 1f;
        [JsonProperty] private float decayRate = .8f;
        [JsonProperty] private float lowestAlphaBound = 0.1f;

        [JsonProperty] private Color attackColor = new(0.3f, 1f, .7f, 1f);
        [JsonProperty] private bool trackTarget;

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

                lineRender.SetPosition(0, startPos);
                lineRender.SetPosition(1, target.GetWorldPosition());

                attackVisuals.Add(new AttackVisual(decayDelay, decayRate, lowestAlphaBound, trackTarget, target, lineRender, attackColor));
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
            public AttackVisual(float decayDelay, float decayRate, float lowestAlphaBound, bool trackTarget, IEnemyObject target, LineRenderer renderer, Color color)
            {
                this.decayDelay = decayDelay;
                this.decayRate = decayRate;
                this.lowestAlphaBound = lowestAlphaBound;

                this.renderer = renderer;
                this.target = target;

                this.trackTarget = trackTarget;

                renderer.material.SetColor(colorShaderProperty, color);

                alpha = renderer.material.GetFloat(alphaShaderProperty);
            }

            public bool Alive = true;
            private float alpha;

            private float decayDelay;
            private readonly float decayRate;

            private readonly LineRenderer renderer;
            private readonly IEnemyObject target;
            private readonly bool trackTarget;
            private readonly float lowestAlphaBound;

            private static readonly int alphaShaderProperty = Shader.PropertyToID("_Alpha");
            private static readonly int colorShaderProperty = Shader.PropertyToID("_Color");

            public void Tick(float delta)
            {
                decayDelay -= delta;
                if (decayDelay > 0) return;

                alpha *= decayRate;
                renderer.material.SetFloat(alphaShaderProperty, alpha);

                if (trackTarget && target.ExistsInWorld)
                    renderer.SetPosition(1, target.GetWorldPosition());

                if (alpha > lowestAlphaBound) return;
                Object.Destroy(renderer.gameObject);
                Alive = false;
            }
        }
    }
}