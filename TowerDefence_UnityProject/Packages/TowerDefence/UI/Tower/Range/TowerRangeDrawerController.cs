using DataBinding;
using NoUtil.Extentsions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.Utility;
using TowerDefence.World.Grid;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace TowerDefence.UI.Tower.Range
{
    public class TowerRangeDrawerController
    {
        private readonly ISelectionModel selectionModel;
        private readonly ITowerModels towerModels;
        private readonly BindingContext bindingContext = new(true);
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
                else if (selection.TryFindObject<SelectableCell>(out var cell))
                {
                    var position = cell.GridNode.Position;
                    return towerModels.Towers.TryFind(x => x.GetGridPosition() == position, out tower);
                }
                return false;
            }
        }

        ~TowerRangeDrawerController()
        {
            bindingContext.Dispose();
        }
    }
}