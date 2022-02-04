
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================

namespace DataBinding 
{
	[UnityEngine.CreateAssetMenu(fileName = "Model Installer", menuName = "Installers/Model Installer")]
	public class ModelsInstaller : Zenject.ScriptableObjectInstaller {
		public override void InstallBindings() {

					Container.Bind<TowerDefence.Entities.Enemies.Models.IEnemiesModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Entities.Enemies.Models.IEnemiesModel>()).AsSingle();
					Container.Bind<TowerDefence.Systems.Selection.Models.ISelectionModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Systems.Selection.Models.ISelectionModel>()).AsSingle();
					Container.Bind<TowerDefence.Entities.Towers.Models.ITowerModels>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Entities.Towers.Models.ITowerModels>()).AsSingle();
				}
	}
}