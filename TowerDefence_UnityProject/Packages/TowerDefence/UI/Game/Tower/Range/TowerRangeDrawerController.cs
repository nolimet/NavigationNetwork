using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.Utility;
using TowerDefence.World.Grid;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace TowerDefence.UI.Game.Tower.Range
{
    public sealed class TowerRangeDrawerController : IDisposable

    {
        private readonly ISelectionModel selectionModel;
        private readonly ITowerModels towerModels;
        private readonly BindingContext bindingContext = new();
        private TowerRangeDrawer rangeDrawer;

        public TowerRangeDrawerController(ISelectionModel selectionModel, ITowerModels towerModels, AssetReference rangeDrawer)
        {
            this.selectionModel = selectionModel;
            this.towerModels = towerModels;

            SetRangeDrawer(rangeDrawer, SetBindings);
        }

        private async void SetRangeDrawer(AssetReference rangeDrawer, UnityAction onComplete)
        {
            await AsyncAwaiters.NextFrame;
            this.rangeDrawer = await GetTowerRangeDrawer();

            async Task<TowerRangeDrawer> GetTowerRangeDrawer()
            {
                var task = rangeDrawer.InstantiateAsync();
                await task;
                return task.Result.GetComponent<TowerRangeDrawer>();
            }

            onComplete?.Invoke();
        }

        private void SetBindings()
        {
            bindingContext.Bind(selectionModel, x => x.Selection, SelectionChanged);
        }

        private void SelectionChanged(IList<ISelectable> selection)
        {
            if (TryGetTower(out var tower))
            {
                rangeDrawer.gameObject.SetActive(true);
                rangeDrawer.DrawRange(tower);
            }
            else
            {
                rangeDrawer.gameObject.SetActive(false);
            }

            bool TryGetTower(out ITowerObject tower)
            {
                if (selection.TryFindTower(out tower))
                {
                    return true;
                }

                if (selection.TryFindObject<SelectableCell>(out var cell))
                {
                    var position = cell.GridCell.Position;
                    return towerModels.Towers.TryFind(x => x.GetGridPosition() == position, out tower);
                }

                return false;
            }
        }

        ~TowerRangeDrawerController()
        {
            Dispose(false);
        }

        private void ReleaseUnmanagedResources()
        {
            Object.Destroy(rangeDrawer);
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                bindingContext?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}