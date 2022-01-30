 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Entities.Towers.Models {
	public class TowerModel : TowerDefence.Entities.Towers.Models.ITowerModel {
		public event Action OnChange;
			// Components
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Entities.Towers.Components.ITowerComponent>> OnChangeComponents;
		private System.Collections.Generic.IList<TowerDefence.Entities.Towers.Components.ITowerComponent> _Components ; 
		public System.Collections.Generic.IList<TowerDefence.Entities.Towers.Components.ITowerComponent> Components {
			get => _Components;
			set {
						
				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.Entities.Towers.Components.ITowerComponent>)_Components).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Entities.Towers.Components.ITowerComponent>) == null) 
				{
					_Components = new ObservableCollection<TowerDefence.Entities.Towers.Components.ITowerComponent>(value);
				}
				else
				{
					_Components = value;
				}

				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.Entities.Towers.Components.ITowerComponent>)_Components).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				OnChangeComponents?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Range
		public event System.Action<System.Double> OnChangeRange;
		private System.Double _Range ; 
		public System.Double Range {
			get => _Range;
			set {
								_Range = value; 

				OnChangeRange?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public TowerModel() { 
				Components = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Entities.Towers.Components.ITowerComponent>();
			}

		private void TriggerComponentsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeComponents?.Invoke(Components);
			OnChange?.Invoke();
		}

		}
}