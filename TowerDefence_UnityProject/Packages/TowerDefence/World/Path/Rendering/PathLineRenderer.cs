using UnityEngine;

namespace TowerDefence.World.Path.Rendering
{
    public sealed class PathLineRenderer : PathRendererBase
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        public override void SetPath(Vector3[] path)
        {
            lineRenderer.positionCount = path.Length;
            lineRenderer.SetPositions(path);
        }
    }
}