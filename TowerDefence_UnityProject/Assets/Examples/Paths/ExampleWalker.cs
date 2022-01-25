using System.Collections;
using System.Collections.Generic;
using TowerDefence.World.Path;
using UnityEngine;
using NoUtil.Extentsions;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Enemies.Components;

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