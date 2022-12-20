 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class LevelEditorModel : TowerDefence.Systems.LevelEditor.Models.ILevelEditorModel 
	{
		public event Action OnChange;
			// LevelName
		public event System.Action<System.String> OnChangeLevelName;
		private System.String _LevelName ; 
		public System.String LevelName 
		{
			get => _LevelName;
			set 
			{
								_LevelName = value; 

				OnChangeLevelName?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// RebuildingWorld
		public event System.Action<System.Boolean> OnChangeRebuildingWorld;
		private System.Boolean _RebuildingWorld ; 
		public System.Boolean RebuildingWorld 
		{
			get => _RebuildingWorld;
			set 
			{
								_RebuildingWorld = value; 

				OnChangeRebuildingWorld?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Waves
		public event System.Action<IModelBase> OnChangeWaves;
		private TowerDefence.Systems.LevelEditor.Models.IWavesModel _Waves ; 
		public TowerDefence.Systems.LevelEditor.Models.IWavesModel Waves 
		{
			get => _Waves;
			set 
			{
								_Waves = value; 

				OnChangeWaves?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// World
		public event System.Action<IModelBase> OnChangeWorld;
		private TowerDefence.Systems.LevelEditor.Models.IWorldLayoutModel _World ; 
		public TowerDefence.Systems.LevelEditor.Models.IWorldLayoutModel World 
		{
			get => _World;
			set 
			{
								_World = value; 

				OnChangeWorld?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public LevelEditorModel() { 
				}

			}
}