using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TowerDefence.World.Path
{
    public class PathLineRenderer : PathRendererBase
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