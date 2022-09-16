 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TowerDefence.UI.Models 
{
	public class UIContainers : TowerDefence.UI.Models.IUIContainers 
	{
		public event Action OnChange;
			// Containers
		public event System.Action<System.Collections.Generic.IList<TowerDefence.UI.Models.IUIContainer>> OnChangeContainers;
		private System.Collections.Generic.IList<TowerDefence.UI.Models.IUIContainer> _Containers ; 
		public System.Collections.Generic.IList<TowerDefence.UI.Models.IUIContainer> Containers 
		{
			get => _Containers;
			set 
			{
						
				if (_Containers != null)
				{
					((ObservableCollection<TowerDefence.UI.Models.IUIContainer>)_Containers).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerContainersEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.UI.Models.IUIContainer>) == null) 
				{
					_Containers = new ObservableCollection<TowerDefence.UI.Models.IUIContainer>(value);
				}
				else
				{
					_Containers = value;
				}

				if (_Containers != null)
				{
					((ObservableCollection<TowerDefence.UI.Models.IUIContainer>)_Containers).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerContainersEvents);
				}

				OnChangeContainers?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public UIContainers() { 
				Containers = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.UI.Models.IUIContainer>();
			}

		private void TriggerContainersEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeContainers?.Invoke(Containers);
			OnChange?.Invoke();
		}

		}
}