 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class WorldLayoutModel : TowerDefence.Systems.LevelEditor.Models.IWorldLayoutModel 
	{
		public event Action OnChange;
			// Cells
		public event System.Action<System.Collections.Generic.IList<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>>> OnChangeCells;
		private System.Collections.Generic.IList<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>> _Cells ; 
		public System.Collections.Generic.IList<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>> Cells 
		{
			get => _Cells;
			set 
			{
						
				if (_Cells != null)
				{
					((ObservableCollection<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>>)_Cells).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerCellsEvents);
				}

				if (value != null && (value as ObservableCollection<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>>) == null) 
				{
					_Cells = new ObservableCollection<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>>(value);
				}
				else
				{
					_Cells = value;
				}

				if (_Cells != null)
				{
					((ObservableCollection<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>>)_Cells).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerCellsEvents);
				}

				OnChangeCells?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// EntryPoints
		public event System.Action<System.Collections.Generic.IList<UnityEngine.Vector2Int>> OnChangeEntryPoints;
		private System.Collections.Generic.IList<UnityEngine.Vector2Int> _EntryPoints ; 
		public System.Collections.Generic.IList<UnityEngine.Vector2Int> EntryPoints 
		{
			get => _EntryPoints;
			set 
			{
						
				if (_EntryPoints != null)
				{
					((ObservableCollection<UnityEngine.Vector2Int>)_EntryPoints).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerEntryPointsEvents);
				}

				if (value != null && (value as ObservableCollection<UnityEngine.Vector2Int>) == null) 
				{
					_EntryPoints = new ObservableCollection<UnityEngine.Vector2Int>(value);
				}
				else
				{
					_EntryPoints = value;
				}

				if (_EntryPoints != null)
				{
					((ObservableCollection<UnityEngine.Vector2Int>)_EntryPoints).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerEntryPointsEvents);
				}

				OnChangeEntryPoints?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// ExitPoints
		public event System.Action<System.Collections.Generic.IList<UnityEngine.Vector2Int>> OnChangeExitPoints;
		private System.Collections.Generic.IList<UnityEngine.Vector2Int> _ExitPoints ; 
		public System.Collections.Generic.IList<UnityEngine.Vector2Int> ExitPoints 
		{
			get => _ExitPoints;
			set 
			{
						
				if (_ExitPoints != null)
				{
					((ObservableCollection<UnityEngine.Vector2Int>)_ExitPoints).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerExitPointsEvents);
				}

				if (value != null && (value as ObservableCollection<UnityEngine.Vector2Int>) == null) 
				{
					_ExitPoints = new ObservableCollection<UnityEngine.Vector2Int>(value);
				}
				else
				{
					_ExitPoints = value;
				}

				if (_ExitPoints != null)
				{
					((ObservableCollection<UnityEngine.Vector2Int>)_ExitPoints).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerExitPointsEvents);
				}

				OnChangeExitPoints?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Height
		public event System.Action<System.UInt32> OnChangeHeight;
		private System.UInt32 _Height ; 
		public System.UInt32 Height 
		{
			get => _Height;
			set 
			{
								_Height = value; 

				OnChangeHeight?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Width
		public event System.Action<System.UInt32> OnChangeWidth;
		private System.UInt32 _Width ; 
		public System.UInt32 Width 
		{
			get => _Width;
			set 
			{
								_Width = value; 

				OnChangeWidth?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WorldLayoutModel() { 
				Cells = new System.Collections.ObjectModel.ObservableCollection<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ICellModel>>();
			EntryPoints = new System.Collections.ObjectModel.ObservableCollection<UnityEngine.Vector2Int>();
			ExitPoints = new System.Collections.ObjectModel.ObservableCollection<UnityEngine.Vector2Int>();
			}

		private void TriggerCellsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeCells?.Invoke(Cells);
			OnChange?.Invoke();
		}

	private void TriggerEntryPointsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeEntryPoints?.Invoke(EntryPoints);
			OnChange?.Invoke();
		}

	private void TriggerExitPointsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeExitPoints?.Invoke(ExitPoints);
			OnChange?.Invoke();
		}

		}
}