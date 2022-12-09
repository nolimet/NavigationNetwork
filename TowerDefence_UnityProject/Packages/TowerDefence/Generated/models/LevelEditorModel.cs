 
 
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
			// waves
		public event System.Action<IModelBase> OnChangewaves;
		private TowerDefence.Systems.LevelEditor.Models.IWavesModel _waves ; 
		public TowerDefence.Systems.LevelEditor.Models.IWavesModel waves 
		{
			get => _waves;
			set 
			{
								_waves = value; 

				OnChangewaves?.Invoke(value);
				OnChange?.Invoke();
			}
		}
			// world
		public event System.Action<IModelBase> OnChangeworld;
		private TowerDefence.Systems.LevelEditor.Models.IWorldLayout _world ; 
		public TowerDefence.Systems.LevelEditor.Models.IWorldLayout world 
		{
			get => _world;
			set 
			{
								_world = value; 

				OnChangeworld?.Invoke(value);
				OnChange?.Invoke();
			}
		}
	
		public LevelEditorModel() { 
				}

			}
}