using UnityEngine;
using Zenject;

namespace TowerDefence.World.Path
{
    public abstract class PathRendererBase : MonoBehaviour
    {
        public abstract void SetPath(Vector3[] path);

        public class Factory : PlaceholderFactory<PathRendererBase>
        {
            [Inject]
            private WorldContainer worldContainer;

            public override PathRendererBase Create()
            {
                var newLine = base.Create();
                newLine.transform.SetParent(worldContainer.PathContainer);
                return newLine;
            }

            public PathRendererBase Create(Vector3[] path)
            {
                var newLine = Create();
                newLine.SetPath(path);
                return newLine;
            }
        }
    }
}