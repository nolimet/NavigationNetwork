 
 
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
			// entranceId
		public event System.Action<System.Int32> OnChangeentranceId;
		private System.Int32 _entranceId ; 
		public System.Int32 entranceId 
		{
			get => _entranceId;
			set 
			{
								_entranceId = value; 

				OnChangeentranceId?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// exitId
		public event System.Action<System.Int32> OnChangeexitId;
		private System.Int32 _exitId ; 
		public System.Int32 exitId 
		{
			get => _exitId;
			set 
			{
								_exitId = value; 

				OnChangeexitId?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// groupSize
		public event System.Action<System.UInt64> OnChangegroupSize;
		private System.UInt64 _groupSize ; 
		public System.UInt64 groupSize 
		{
			get => _groupSize;
			set 
			{
								_groupSize = value; 

				OnChangegroupSize?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// spawnInterval
		public event System.Action<System.Double> OnChangespawnInterval;
		private System.Double _spawnInterval ; 
		public System.Double spawnInterval 
		{
			get => _spawnInterval;
			set 
			{
								_spawnInterval = value; 

				OnChangespawnInterval?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// startDelay
		public event System.Action<System.Double> OnChangestartDelay;
		private System.Double _startDelay ; 
		public System.Double startDelay 
		{
			get => _startDelay;
			set 
			{
								_startDelay = value; 

				OnChangestartDelay?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public SpawnGroupModel() { 
				}

			}
}