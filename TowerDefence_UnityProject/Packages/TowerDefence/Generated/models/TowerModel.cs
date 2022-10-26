 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Entities.Towers.Models 
{
	public class TowerModel : TowerDefence.Entities.Towers.Models.ITowerModel 
	{
		public event Action OnChange;
			// Components
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Entities.Components.IComponent>> OnChangeComponents;
		private System.Collections.Generic.IList<TowerDefence.Entities.Components.IComponent> _Components ; 
		public System.Collections.Generic.IList<TowerDefence.Entities.Components.IComponent> Components 
		{
			get => _Components;
			set 
			{
						
				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.Entities.Components.IComponent>)_Components).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Entities.Components.IComponent>) == null) 
				{
					_Components = new ObservableCollection<TowerDefence.Entities.Components.IComponent>(value);
				}
				else
				{
					_Components = value;
				}

				if (_Components != null)
				{
					((ObservableCollection<TowerDefence.Entities.Components.IComponent>)_Components).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerComponentsEvents);
				}

				OnChangeComponents?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public TowerModel() { 
				Components = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Entities.Components.IComponent>();
			}

		private void TriggerComponentsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeComponents?.Invoke(Components);
			OnChange?.Invoke();
		}

		}
}