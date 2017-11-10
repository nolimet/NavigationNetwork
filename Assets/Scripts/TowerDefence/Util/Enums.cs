using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefence
{
    public enum Target_Priority
    {
        First,
        Last,
        Closest,
        Weakest,
        Strongest,
        ClosestToEnd
    }

    public enum BulletType
    {
        Base
    }

    [System.Serializable]
    public enum NodeTypes
    {
        Regular = 0,
        Endpoint = 1,
        Spawnpoint = 2
    }

    public enum GameState
    {
        building,
        playing,
        editing
    }

    public enum Towers
    {
        Laser = 0,
        Rocket_Launcher = 1
    }
}
