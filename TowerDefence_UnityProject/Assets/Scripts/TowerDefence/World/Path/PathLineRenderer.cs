using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TowerDefence.World.Path
{
    public class PathLineRenderer : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        public void SetPath(Vector3[] path)
        {
            lineRenderer.positionCount = path.Length;
            lineRenderer.SetPositions(path);
        }

        public class Factory : PlaceholderFactory<PathLineRenderer>
        {
            [Inject]
            private WorldContainer worldContainer;

            public override PathLineRenderer Create()
            {
                var newLine = base.Create();
                newLine.transform.SetParent(worldContainer.PathContainer);
                return newLine;
            }

            public PathLineRenderer Create(Vector3[] path)
            {
                var newLine = Create();
                newLine.SetPath(path);
                return newLine;
            }
        }
    }
}