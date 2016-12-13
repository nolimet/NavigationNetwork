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
        ClostestToEnd
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
}
