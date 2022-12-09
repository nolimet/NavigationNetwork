 
 
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
			// spawnGroups
		public event System.Action<System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.IWaveModel>> OnChangespawnGroups;
		private System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.IWaveModel> _spawnGroups ; 
		public System.Collections.Generic.IList<TowerDefence.Systems.LevelEditor.Models.IWaveModel> spawnGroups 
		{
			get => _spawnGroups;
			set 
			{
						
				if (_spawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>)_spawnGroups).CollectionChanged -= new NotifyCollectionChangedEventHandler(TriggerspawnGroupsEvents);
				}

				if (value != null && (value as ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>) == null) 
				{
					_spawnGroups = new ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>(value);
				}
				else
				{
					_spawnGroups = value;
				}

				if (_spawnGroups != null)
				{
					((ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>)_spawnGroups).CollectionChanged += new NotifyCollectionChangedEventHandler(TriggerspawnGroupsEvents);
				}

				OnChangespawnGroups?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public WavesModel() { 
				spawnGroups = new System.Collections.ObjectModel.ObservableCollection<TowerDefence.Systems.LevelEditor.Models.IWaveModel>();
			}

		private void TriggerspawnGroupsEvents(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnChangespawnGroups?.Invoke(spawnGroups);
			OnChange?.Invoke();
		}

		}
}