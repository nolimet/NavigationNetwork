using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Systems.CameraManager;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.World.Grid;
using UnityEngine;

namespace TowerDefence.Systems.LevelEditor.Managers
{
    public class WorldEditorManager : IDisposable
    {
        private readonly ILevelEditorModel levelEditorModel;
        private readonly ICameraContainer cameraContainer;
        private readonly BindingContext levelEditorBindingContext = new();
        private readonly GridWorld gridWorld;

        private BindingContext gridBindingContext;
        private CameraReference cameraReference;

        internal WorldEditorManager(ILevelEditorModel levelEditorModel, GridWorld gridWorld, ICameraContainer cameraContainer)
        {
            this.cameraContainer = cameraContainer;
            this.levelEditorModel = levelEditorModel;
            this.gridWorld = gridWorld;

            levelEditorBindingContext.Bind(levelEditorModel, x => x.World, OnWorldChanged);
            levelEditorBindingContext.Bind(cameraContainer, x => x.Cameras, OnCamerasChanged);
        }

        private void OnCamerasChanged(IEnumerable<CameraReference> _) => cameraReference = cameraContainer.TryGetCameraById("MainCamera", out var camera) ? camera : null;

        private void OnWorldChanged(IWorldLayoutModel world)
        {
            gridBindingContext?.Dispose();
            gridBindingContext = new BindingContext();
            if (world is null) return;
        }

        public UniTask RebuildWorld()
        {
            if (levelEditorModel.World is null) return UniTask.CompletedTask;
            var world = levelEditorModel.World;
            var size = world.Height * world.Width;


            while (world.Cells.Count < size)
            {
                world.Cells.Add(ModelFactory.Create<ICellModel>());
            }

            while (world.Cells.Count > size && world.Cells.Count != 0)
            {
                world.Cells.Remove(world.Cells[^1]);
            }

            if (levelEditorModel.RebuildingWorld) return UniTask.CompletedTask;
            levelEditorModel.RebuildingWorld = true;
            return gridWorld.CreateWorld(world.ToGridSettings())
                .ContinueWith(() =>
                {
                    UpdateCameraView();
                    return levelEditorModel.RebuildingWorld = false;
                });
        }

        private void UpdateCameraView()
        {
            var c = cameraReference.Camera;
            Vector2 cameraBottomLeft = c.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector2 cameraTopRight = c.ViewportToWorldPoint(new Vector3(1, 1, 0));
            Rect cameraRect = new Rect
            {
                xMin = cameraBottomLeft.x,
                yMin = cameraBottomLeft.y,
                xMax = cameraTopRight.x,
                yMax = cameraTopRight.y
            };

            c.orthographicSize = (cameraRect.height < cameraRect.width ? levelEditorModel.World.Height : levelEditorModel.World.Width) / 2f;
        }

        public void Dispose()
        {
            levelEditorBindingContext?.Dispose();
            gridBindingContext?.Dispose();
        }
    }
}