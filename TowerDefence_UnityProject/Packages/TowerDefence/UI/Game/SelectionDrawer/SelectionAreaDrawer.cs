using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DataBinding;
using TowerDefence.Input;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.SelectionDrawer
{
    public class SelectionAreaDrawer : IDisposable
    {
        private const string UIContainerId = "GameUI-Selection";
        private const string SelectionAreaId = "SelectionArea";

        private readonly ISelectionModel selectionModel;
        private readonly SelectionInputActions selectionInput;

        private readonly BindingContext bindingContext = new();
        private readonly CancellationTokenSource ctx;
        private readonly IUIContainers uiContainers;

        private VisualElement selectionArea;

        public SelectionAreaDrawer(ISelectionModel selectionModel, IUIContainers uiContainers, SelectionInputActions selectionInput)
        {
            this.selectionModel = selectionModel;
            this.selectionInput = selectionInput;
            this.uiContainers = uiContainers;

            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainersChanged);
            bindingContext.Bind(selectionModel, x => x.Dragging, OnDragChanged);
        }

        private void OnDragChanged(bool isDragging)
        {
            if (selectionArea is null) return;

            selectionArea.visible = isDragging;

            if (isDragging)
            {
                DragUpdateTask().Preserve().SuppressCancellationThrow();
            }
        }

        private void OnUIContainersChanged(IList<IUIContainer> _)
        {
            if (uiContainers.TryGetContainer(UIContainerId, out UIDocumentContainer documentContainer))
            {
                var root = documentContainer.Document.rootVisualElement;

                selectionArea = root.Q(SelectionAreaId);
                selectionArea.visible = false;
                selectionArea.SetEnabled(false);

                Debug.Log(selectionArea.style);
            }
        }

        async UniTask DragUpdateTask()
        {
            var token = ctx.Token;
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(token))
            {
                if (!selectionModel.Dragging || selectionArea is null)
                    break;

                var mousePosition = selectionInput.Main.MousePosition.ReadValue<Vector2>();
                var min = Vector2.Min(selectionModel.DragStartPosition, mousePosition);
                var max = Vector2.Max(selectionModel.DragStartPosition, mousePosition);

                var size = max - min;
                var center = min + size / 2f;

                selectionArea.style.height = size.y;
                selectionArea.style.width = size.x;
                selectionArea.transform.position = center;

                Debug.Log(center);
            }
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
            ctx.Cancel();
            ctx.Dispose();
        }
    }
}