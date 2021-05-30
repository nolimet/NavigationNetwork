using System.Collections;
using System.Collections.Generic;
using TowerDefence.World.Path;
using UnityEngine;

public class ExampleWalker : WalkerBase
{
    public override void ReachedEnd()
    {
        Debug.Log("Reached End");
    }
}