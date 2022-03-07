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
	
		public static T Create<T>() where T : DataBinding.BaseClasses.IModelBase {

						if (typeof(T) == typeof(TowerDefence.Entities.Enemies.Models.IEnemiesModel )) { return (T)(CreateEnemiesModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Enemies.Models.IEnemyModel )) { return (T)(CreateEnemyModel()); }
					if (typeof(T) == typeof(TowerDefence.Systems.Selection.Models.ISelectionModel )) { return (T)(CreateSelectionModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Towers.Models.ITowerModel )) { return (T)(CreateTowerModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Towers.Models.ITowerModels )) { return (T)(CreateTowerModels()); }
					return default(T);
		}
	}
}