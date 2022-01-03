 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.World.Towers.Models {
	public class TowerModels : TowerDefence.World.Towers.Models.ITowerModels {
		public event Action OnChange;
			// SelectedTower
		public event System.Action<TowerDefence.World.Towers.TowerBase> OnChangeSelectedTower;
		private TowerDefence.World.Towers.TowerBase _SelectedTower ; 
		public TowerDefence.World.Towers.TowerBase SelectedTower {
			get => _SelectedTower;
			set {
								_SelectedTower = value; 

				OnChangeSelectedTower?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Towers
		public event System.Action<System.Collections.Generic.IList<TowerDefence.World.Towers.Models.ITowerModel>> OnChangeTowers;
		private System.Collections.Generic.IList<TowerDefence.World.Towers.Models.ITowerModel> _Towers ; 
		public System.Collections.Generic.IList<TowerDefence.World.Towers.Models.ITowerModel> Towers {
			get => _Towers;
			set {
						
				if (_Towers != null)
				{
					((ObservableCollection<TowerDefence.World.Towers.Models.ITowerModel>)_Towers).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerTowersEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.World.Towers.Models.ITowerModel>) == null) 
				{
					_Towers = new ObservableCollection<TowerDefence.World.Towers.Models.ITowerModel>(value);
				}
				else
				{
					_Towers = value;
				}

				if (_Towers != null)
				{
					((ObservableCollection<TowerDefence.World.Towers.Models.ITowerModel>)_Towers).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerTowersEvents);
				}

				OnChangeTowers?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public TowerModels() { 
				Towers = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.World.Towers.Models.ITowerModel>();
			}

		private void TriggerTowersEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeTowers?.Invoke(Towers);
			OnChange?.Invoke();
		}

		}
}