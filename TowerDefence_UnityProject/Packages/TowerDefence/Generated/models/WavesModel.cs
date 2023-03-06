 
 
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataBinding.BaseClasses;

namespace TowerDefence.Systems.LevelEditor.Models 
{
	public class WavesModel : TowerDefence.Systems.LevelEditor.Models.IWavesModel 
	{
		public event Action OnChange;
			// SpawnGroups
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.IWaveModel>> OnChangeSpawnGroups;
		private System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.IWaveModel> _SpawnGroups ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.IWaveModel> SpawnGroups 
		{
			get => _SpawnGroups;
			set 
			{
						
				if (_SpawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>)_SpawnGroups).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerSpawnGroupsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>) == null) 
				{
					_SpawnGroups = new ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>(value);
				}
				else
				{
					_SpawnGroups = value;
				}

				if (_SpawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>)_SpawnGroups).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerSpawnGroupsEvents);
				}

				OnChangeSpawnGroups?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WavesModel() { 
				SpawnGroups = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>();
			}

		private void TriggerSpawnGroupsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangeSpawnGroups?.Invoke(SpawnGroups);
			OnChange?.Invoke();
		}

		}
}