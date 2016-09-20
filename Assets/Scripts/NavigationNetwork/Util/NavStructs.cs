using UnityEngine;


namespace NavigationNetwork
{
    namespace structs
    {
        public struct NavPullObject
        {
            public NavigationBase ClosestNode;
            public float Distance;

            public NavPullObject (NavigationBase ClosestNode, float Distance)
            {
                this.Distance = Distance;
                this.ClosestNode = ClosestNode;
            }
        }
    }
}