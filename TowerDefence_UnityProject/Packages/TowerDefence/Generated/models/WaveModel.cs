 
 
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
			// spawnGroups
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>> OnChangespawnGroups;
		private System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel> _spawnGroups ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel> spawnGroups 
		{
			get => _spawnGroups;
			set 
			{
						
				if (_spawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>)_spawnGroups).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerspawnGroupsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>) == null) 
				{
					_spawnGroups = new ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>(value);
				}
				else
				{
					_spawnGroups = value;
				}

				if (_spawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>)_spawnGroups).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerspawnGroupsEvents);
				}

				OnChangespawnGroups?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WaveModel() { 
				spawnGroups = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.LevelEditor.Models.ISpawnGroupModel>();
			}

		private void TriggerspawnGroupsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangespawnGroups?.Invoke(spawnGroups);
			OnChange?.Invoke();
		}

		}
}