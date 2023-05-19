 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class CellModel : TowerDefence.Systems.LevelEditor.Models.ICellModel 
	{
		public event Action OnChange;
			// SupportsTower
		public event System.Action<System.Boolean> OnChangeSupportsTower;
		private System.Boolean _SupportsTower ; 
		public System.Boolean SupportsTower 
		{
			get => _SupportsTower;
			set 
			{
								_SupportsTower = value; 

				OnChangeSupportsTower?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Weight
		public event System.Action<System.Byte> OnChangeWeight;
		private System.Byte _Weight ; 
		public System.Byte Weight 
		{
			get => _Weight;
			set 
			{
								_Weight = value; 

				OnChangeWeight?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// WorldCellGroup
		public event System.Action<TowerDefence.World.Grid.SelectableCellGroup> OnChangeWorldCellGroup;
		private TowerDefence.World.Grid.SelectableCellGroup _WorldCellGroup ; 
		public TowerDefence.World.Grid.SelectableCellGroup WorldCellGroup 
		{
			get => _WorldCellGroup;
			set 
			{
								_WorldCellGroup = value; 

				OnChangeWorldCellGroup?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public CellModel() { 
				}

			}
}