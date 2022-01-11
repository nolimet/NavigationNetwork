using DataBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using UnityEngine.AddressableAssets;

namespace TowerDefence.UI.Tower
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

            bindingContext.Bind(selectionModel, x => x.Selection, SelectionChanged);

            SetRangeDrawer(rangeDrawer);
        }

        private async void SetRangeDrawer(AssetReference rangeDrawer)
        {
            await AsyncAwaiters.NextFrame;
            this.rangeDrawer = await GetTowerRangeDrawer();
            async Task<TowerRangeDrawer> GetTowerRangeDrawer()
            {
                var task = rangeDrawer.InstantiateAsync();
                await task;
                return task.Result.GetComponent<TowerRangeDrawer>();
            }
        }

        private void SelectionChanged(IList<ISelectable> selection)
        {
            var subSelection = selection.Where(x => x is TowerBase);
            if (subSelection.Count() == 0)
            {
                rangeDrawer?.gameObject.SetActive(false);
            }
            else if (subSelection.First() is TowerBase tower)
            {
                var towerModel = towerModels.Towers.FirstOrDefault(x => x.TowerRenderer == tower);

                rangeDrawer.gameObject.SetActive(towerModel?.TowerRenderer);
                if (towerModel?.TowerRenderer)
                {
                    rangeDrawer.DrawRange(towerModel);
                }
            }
        }

        ~TowerRangeDrawerController()
        {
            bindingContext.Dispose();
        }
    }
}