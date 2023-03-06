using System.Collections.Generic;
using DataBinding;
using TowerDefence.Systems.CameraManager;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence.Systems.LevelEditor.Managers
{
    public class EditorCameraManager
    {
        private const string CameraID = "MainCamera";
        private const string ContainerID = "LevelEdiitorUI";
        private readonly ICameraContainer cameraContainer;
        private readonly ILevelEditorModel levelEditorModel;
        private readonly IUIContainers uiContainers;

        private UIDocumentContainer uiContainer;
        private CameraReference cameraReference;

        private readonly BindingContext bindingContext = new();
        private BindingContext levelEditorBindingContext;

        private VisualElement sideElement;
        private VisualElement visualRoot;
        private Button rebuildButton;

        public EditorCameraManager(ICameraContainer cameraContainer, ILevelEditorModel levelEditorModel, IUIContainers uiContainers)
        {
            this.cameraContainer = cameraContainer;
            this.levelEditorModel = levelEditorModel;
            this.uiContainers = uiContainers;

            bindingContext.Bind(levelEditorModel, x => x.World, OnWorldChanged);
            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainersChanged);
            bindingContext.Bind(cameraContainer, x => x.Cameras, OnCamerasChanged);
        }

        private void OnCamerasChanged(IList<CameraReference> _)
        {
            cameraContainer.TryGetCameraById(CameraID, out cameraReference);

            OnWorldRebuildClicked();
        }

        private void OnWorldChanged(IWorldLayoutModel world)
        {
            levelEditorBindingContext?.Dispose();
            levelEditorBindingContext = new BindingContext();

            if (world is null) return;

            OnWorldRebuildClicked();
        }

        private void OnUIContainersChanged(IList<IUIContainer> _)
        {
            if (rebuildButton is not null)
            {
                rebuildButton.clicked -= OnWorldRebuildClicked;
            }


            if (uiContainers.TryGetContainer(ContainerID, out uiContainer))
            {
                visualRoot = uiContainer.VisualRoot;
                sideElement = visualRoot.Q("SideBar");

                rebuildButton = visualRoot.Q<Button>("RebuildGrid");
                rebuildButton.clicked += OnWorldRebuildClicked;
            }

            OnWorldRebuildClicked();
        }

        private void OnWorldRebuildClicked()
        {
            //TODO handle tall screens
            if (cameraReference == null || visualRoot == null || sideElement == null)
            {
                return;
            }

            var c = cameraReference.Camera;
            var world = levelEditorModel.World;
            float screenWidth = visualRoot.contentRect.width;
            float sideBarWidth = sideElement.contentRect.width;
            float cameraViewWidth = 1f - sideBarWidth / screenWidth;

            c.rect = new Rect(c.rect)
            {
                width = float.IsNaN(cameraViewWidth) ? 1 : cameraViewWidth
            };

            Vector2 cameraBottomLeft = c.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector2 cameraTopRight = c.ViewportToWorldPoint(new Vector3(1, 1, 0));
            var cameraRect = new Rect
            {
                xMin = cameraBottomLeft.x,
                yMin = cameraBottomLeft.y,
                xMax = cameraTopRight.x,
                yMax = cameraTopRight.y
            };

            if (world.Width < world.Height)
            {
                c.orthographicSize = (cameraRect.height < cameraRect.width ? world.Height : world.Width) / 2f;
            }
            else
            {
                c.orthographicSize = (cameraRect.height < cameraRect.width ? world.Width : world.Height) / 2f;
            }
        }
    }
}