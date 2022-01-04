 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.Selection.Models {
	public class SelectionModel : TowerDefence.Systems.Selection.Models.ISelectionModel {
		public event Action OnChange;
			// Selection
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.Selection.ISelectable>> OnChangeSelection;
		private System.Collections.Generic.IList<TowerDefence.Systems.Selection.ISelectable> _Selection ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.Selection.ISelectable> Selection {
			get => _Selection;
			set {
						
				if (_Selection != null)
				{
					((ObservableCollection<TowerDefence.Systems.Selection.ISelectable>)_Selection).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerSelectionEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.Selection.ISelectable>) == null) 
				{
					_Selection = new ObservableCollection<TowerDefence.Systems.Selection.ISelectable>(value);
				}
				else
				{
					_Selection = value;
				}

				if (_Selection != null)
				{
					((ObservableCollection<TowerDefence.Systems.Selection.ISelectable>)_Selection).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerSelectionEvents);
				}

				OnChangeSelection?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public SelectionModel() { 
				Selection = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.Selection.ISelectable>();
			}

		private void TriggerSelectionEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeSelection?.Invoke(Selection);
			OnChange?.Invoke();
		}

		}
}