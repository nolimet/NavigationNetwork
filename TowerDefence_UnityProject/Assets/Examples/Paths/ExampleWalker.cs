using NoUtil.Extentsions;
using TowerDefence.Entities.Enemies;
using UnityEngine;

public class ExampleWalker : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer.color = renderer.color.GetRandom();
        name = "enemy-" + Random.Range(0, 10000);
    }

    private void Start()
    {
        var enemyObject = GetComponent<IEnemyObject>();

        var model = enemyObject.Model;
        model.Health = 10;
        model.MaxHealth = 10;
    }
}