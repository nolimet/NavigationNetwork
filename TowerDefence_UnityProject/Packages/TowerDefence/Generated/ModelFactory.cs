// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================

namespace DataBinding 
{
	public static class ModelFactory 
	{

		private static TowerDefence.Entities.Enemies.Models.IEnemiesModel CreateEnemiesModel() { return new TowerDefence.Entities.Enemies.Models.EnemiesModel(); }
			private static TowerDefence.Entities.Enemies.Models.IEnemyModel CreateEnemyModel() { return new TowerDefence.Entities.Enemies.Models.EnemyModel(); }
			private static TowerDefence.Systems.Selection.Models.ISelectionModel CreateSelectionModel() { return new TowerDefence.Systems.Selection.Models.SelectionModel(); }
			private static TowerDefence.Entities.Towers.Models.ITowerModel CreateTowerModel() { return new TowerDefence.Entities.Towers.Models.TowerModel(); }
			private static TowerDefence.Entities.Towers.Models.ITowerModels CreateTowerModels() { return new TowerDefence.Entities.Towers.Models.TowerModels(); }
			private static TowerDefence.UI.Models.IUIContainers CreateUIContainers() { return new TowerDefence.UI.Models.UIContainers(); }
			private static TowerDefence.Systems.Waves.Models.IWavePlayStateModel CreateWavePlayStateModel() { return new TowerDefence.Systems.Waves.Models.WavePlayStateModel(); }
			private static TowerDefence.Systems.WorldLoader.Models.IWorldDataModel CreateWorldDataModel() { return new TowerDefence.Systems.WorldLoader.Models.WorldDataModel(); }
	
		public static T Create<T>() where T : DataBinding.BaseClasses.IModelBase {

						if (typeof(T) == typeof(TowerDefence.Entities.Enemies.Models.IEnemiesModel )) { return (T)(CreateEnemiesModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Enemies.Models.IEnemyModel )) { return (T)(CreateEnemyModel()); }
					if (typeof(T) == typeof(TowerDefence.Systems.Selection.Models.ISelectionModel )) { return (T)(CreateSelectionModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Towers.Models.ITowerModel )) { return (T)(CreateTowerModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Towers.Models.ITowerModels )) { return (T)(CreateTowerModels()); }
					if (typeof(T) == typeof(TowerDefence.UI.Models.IUIContainers )) { return (T)(CreateUIContainers()); }
					if (typeof(T) == typeof(TowerDefence.Systems.Waves.Models.IWavePlayStateModel )) { return (T)(CreateWavePlayStateModel()); }
					if (typeof(T) == typeof(TowerDefence.Systems.WorldLoader.Models.IWorldDataModel )) { return (T)(CreateWorldDataModel()); }
					return default(T);
		}
	}
}