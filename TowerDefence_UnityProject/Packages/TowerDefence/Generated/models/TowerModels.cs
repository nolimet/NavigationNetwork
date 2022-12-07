 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Entities.Towers.Models 
{
	public class TowerModels : TowerDefence.Entities.Towers.Models.ITowerModels 
	{
		public event Action OnChange;
			// SelectedTower
		public event System.Action<TowerDefence.Entities.Towers.ITowerObject> OnChangeSelectedTower;
		private TowerDefence.Entities.Towers.ITowerObject _SelectedTower ; 
		public TowerDefence.Entities.Towers.ITowerObject SelectedTower 
		{
			get => _SelectedTower;
			set 
			{
								_SelectedTower = value; 

				OnChangeSelectedTower?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Towers
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Entities.Towers.ITowerObject>> OnChangeTowers;
		private System.Collections.Generic.IList<TowerDefence.Entities.Towers.ITowerObject> _Towers ; 
		public System.Collections.Generic.IList<TowerDefence.Entities.Towers.ITowerObject> Towers 
		{
			get => _Towers;
			set 
			{
						
				if (_Towers != null)
				{
					((ObservableCollection<TowerDefence.Entities.Towers.ITowerObject>)_Towers).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerTowersEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Entities.Towers.ITowerObject>) == null) 
				{
					_Towers = new ObservableCollection<TowerDefence.Entities.Towers.ITowerObject>(value);
				}
				else
				{
					_Towers = value;
				}

				if (_Towers != null)
				{
					((ObservableCollection<TowerDefence.Entities.Towers.ITowerObject>)_Towers).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerTowersEvents);
				}

				OnChangeTowers?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public TowerModels() { 
				Towers = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Entities.Towers.ITowerObject>();
			}

		private void TriggerTowersEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeTowers?.Invoke(Towers);
			OnChange?.Invoke();
		}

		}
}