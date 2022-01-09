// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================

namespace DataBinding 
{
	public static class ModelFactory 
	{

			private static TowerDefence.Entities.Enemies.Models.IEnemiesModel CreateEnemiesModel() { return new TowerDefence.Entities.Enemies.Models.EnemiesModel(); }
			private static TowerDefence.Entities.Enemies.Models.IEnemyBaseModel CreateEnemyBaseModel() { return new TowerDefence.Entities.Enemies.Models.EnemyBaseModel(); }
			private static TowerDefence.Systems.Selection.Models.ISelectionModel CreateSelectionModel() { return new TowerDefence.Systems.Selection.Models.SelectionModel(); }
			private static TowerDefence.World.Towers.Models.ITowerModel CreateTowerModel() { return new TowerDefence.World.Towers.Models.TowerModel(); }
			private static TowerDefence.World.Towers.Models.ITowerModels CreateTowerModels() { return new TowerDefence.World.Towers.Models.TowerModels(); }
	
		public static T Create<T>() where T : DataBinding.BaseClasses.IModelBase {

						if (typeof(T) == typeof(TowerDefence.Entities.Enemies.Models.IEnemiesModel )) { return (T)(CreateEnemiesModel()); }
					if (typeof(T) == typeof(TowerDefence.Entities.Enemies.Models.IEnemyBaseModel )) { return (T)(CreateEnemyBaseModel()); }
					if (typeof(T) == typeof(TowerDefence.Systems.Selection.Models.ISelectionModel )) { return (T)(CreateSelectionModel()); }
					if (typeof(T) == typeof(TowerDefence.World.Towers.Models.ITowerModel )) { return (T)(CreateTowerModel()); }
					if (typeof(T) == typeof(TowerDefence.World.Towers.Models.ITowerModels )) { return (T)(CreateTowerModels()); }
					return default(T);
		}
	}
}