 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class WaveModel : TowerDefence.Systems.LevelEditor.Models.IWaveModel 
	{
		public event Action OnChange;
			// SpawnGroups
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>> OnChangeSpawnGroups;
		private System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel> _SpawnGroups ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel> SpawnGroups 
		{
			get => _SpawnGroups;
			set 
			{
						
				if (_SpawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>)_SpawnGroups).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerSpawnGroupsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>) == null) 
				{
					_SpawnGroups = new ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>(value);
				}
				else
				{
					_SpawnGroups = value;
				}

				if (_SpawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>)_SpawnGroups).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerSpawnGroupsEvents);
				}

				OnChangeSpawnGroups?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WaveModel() { 
				SpawnGroups = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>();
			}

		private void TriggerSpawnGroupsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeSpawnGroups?.Invoke(SpawnGroups);
			OnChange?.Invoke();
		}

		}
}