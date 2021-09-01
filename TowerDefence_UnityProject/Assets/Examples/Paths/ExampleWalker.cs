using System.Collections;
using System.Collections.Generic;
using TowerDefence.World.Path;
using UnityEngine;
using NoUtil.Extentsions;
using TowerDefence.Entities.Enemies;

public class ExampleWalker : EnemyBase
{
    [SerializeField]
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer.color = renderer.color.GetRandom();
        name = "enemy-" + Random.Range(0, 10000);
    }

    public override void ReachedEnd()
    {
    }
}