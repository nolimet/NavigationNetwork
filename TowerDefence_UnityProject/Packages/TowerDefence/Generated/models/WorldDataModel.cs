 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;

namespace TowerDefence.Systems.WorldLoader.Models 
{
	public class WorldDataModel : TowerDefence.Systems.WorldLoader.Models.IWorldDataModel 
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
			// LevelType
		public event System.Action<TowerDefence.Systems.WorldLoader.LevelType> OnChangeLevelType;
		private TowerDefence.Systems.WorldLoader.LevelType _LevelType ; 
		public TowerDefence.Systems.WorldLoader.LevelType LevelType 
		{
			get => _LevelType;
			set 
			{
								_LevelType = value; 

				OnChangeLevelType?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// Waves
		public event System.Action<TowerDefence.Systems.Waves.Data.Wave[]> OnChangeWaves;
		private TowerDefence.Systems.Waves.Data.Wave[] _Waves ; 
		public TowerDefence.Systems.Waves.Data.Wave[] Waves 
		{
			get => _Waves;
			set 
			{
								_Waves = value; 

				OnChangeWaves?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WorldDataModel() { 
				}

			}
}