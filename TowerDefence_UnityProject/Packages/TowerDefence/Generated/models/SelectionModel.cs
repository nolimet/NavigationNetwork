 
 
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
			// DragEndPosition
		public event System.Action<UnityEngine.Vector3> OnChangeDragEndPosition;
		private UnityEngine.Vector3 _DragEndPosition ; 
		public UnityEngine.Vector3 DragEndPosition {
			get => _DragEndPosition;
			set {
								_DragEndPosition = value; 

				OnChangeDragEndPosition?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Dragging
		public event System.Action<System.Boolean> OnChangeDragging;
		private System.Boolean _Dragging ; 
		public System.Boolean Dragging {
			get => _Dragging;
			set {
								_Dragging = value; 

				OnChangeDragging?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// DragStartPosition
		public event System.Action<UnityEngine.Vector3> OnChangeDragStartPosition;
		private UnityEngine.Vector3 _DragStartPosition ; 
		public UnityEngine.Vector3 DragStartPosition {
			get => _DragStartPosition;
			set {
								_DragStartPosition = value; 

				OnChangeDragStartPosition?.Invoke(value);
				OnChange?.Invoke();
			}
		}
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