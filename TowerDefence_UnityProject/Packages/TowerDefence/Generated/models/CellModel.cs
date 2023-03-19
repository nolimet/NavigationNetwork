 
 
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
			// worldCell
		public event System.Action<TowerDefence.World.Grid.SelectableCell> OnChangeworldCell;
		private TowerDefence.World.Grid.SelectableCell _worldCell ; 
		public TowerDefence.World.Grid.SelectableCell worldCell 
		{
			get => _worldCell;
			set 
			{
								_worldCell = value; 

				OnChangeworldCell?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public CellModel() { 
				}

			}
}