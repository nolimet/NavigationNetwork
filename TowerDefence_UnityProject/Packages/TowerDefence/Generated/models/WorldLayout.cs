 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class WorldLayout : TowerDefence.Systems.LevelEditor.Models.IWorldLayout 
	{
		public event Action OnChange;
			// Cells
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.Cell>> OnChangeCells;
		private System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.Cell> _Cells ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.Cell> Cells 
		{
			get => _Cells;
			set 
			{
						
				if (_Cells != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.Cell>)_Cells).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerCellsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.LevelEditor.Models.Cell>) == null) 
				{
					_Cells = new ObservableCollection<TowerDefence.Systems.LevelEditor.Models.Cell>(value);
				}
				else
				{
					_Cells = value;
				}

				if (_Cells != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.Cell>)_Cells).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerCellsEvents);
				}

				OnChangeCells?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WorldLayout() { 
				Cells = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.LevelEditor.Models.Cell>();
			}

		private void TriggerCellsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeCells?.Invoke(Cells);
			OnChange?.Invoke();
		}

		}
}