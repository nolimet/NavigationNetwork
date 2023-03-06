 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class SpawnGroupModel : TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel 
	{
		public event Action OnChange;
			// EnemyId
		public event System.Action<System.String> OnChangeEnemyId;
		private System.String _EnemyId ; 
		public System.String EnemyId 
		{
			get => _EnemyId;
			set 
			{
								_EnemyId = value; 

				OnChangeEnemyId?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// EntranceId
		public event System.Action<System.Int32> OnChangeEntranceId;
		private System.Int32 _EntranceId ; 
		public System.Int32 EntranceId 
		{
			get => _EntranceId;
			set 
			{
								_EntranceId = value; 

				OnChangeEntranceId?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// ExitId
		public event System.Action<System.Int32> OnChangeExitId;
		private System.Int32 _ExitId ; 
		public System.Int32 ExitId 
		{
			get => _ExitId;
			set 
			{
								_ExitId = value; 

				OnChangeExitId?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// GroupSize
		public event System.Action<System.UInt64> OnChangeGroupSize;
		private System.UInt64 _GroupSize ; 
		public System.UInt64 GroupSize 
		{
			get => _GroupSize;
			set 
			{
								_GroupSize = value; 

				OnChangeGroupSize?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// SpawnInterval
		public event System.Action<System.Double> OnChangeSpawnInterval;
		private System.Double _SpawnInterval ; 
		public System.Double SpawnInterval 
		{
			get => _SpawnInterval;
			set 
			{
								_SpawnInterval = value; 

				OnChangeSpawnInterval?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// StartDelay
		public event System.Action<System.Double> OnChangeStartDelay;
		private System.Double _StartDelay ; 
		public System.Double StartDelay 
		{
			get => _StartDelay;
			set 
			{
								_StartDelay = value; 

				OnChangeStartDelay?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public SpawnGroupModel() { 
				}

			}
}