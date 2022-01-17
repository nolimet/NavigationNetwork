using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components.Damage;
using TowerDefence.Entities.Towers.Components.TargetFinders;
using UnityEngine;

namespace Examples.Towers
{
    public class ExampleTower : MonoBehaviour
    {
        [SerializeField]
        private double range;

        [SerializeField]
        private double damage;

        [SerializeField]
        private float attackCooldown;

        private async void Start()
        {
            var towerObject = GetComponent<ITowerObject>();
            await new WaitUntil(() => towerObject.Model != null);

            var model = towerObject.Model;

            model.Range = range;
            model.Components.Add(new NearestTargetFinder(towerObject, model));
            model.Components.Add(new DamageAllTargets(model, damage, attackCooldown));
        }
    }
}