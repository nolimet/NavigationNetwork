
// ========================================================================
// !! DO NOT EDIT THIS SCRIPT, AUTO GENERATED !!
// ========================================================================

namespace DataBinding 
{
	[UnityEngine.CreateAssetMenu(fileName = "Model Installer", menuName = "Installers/Model Installer")]
	public class ModelsInstaller : Zenject.ScriptableObjectInstaller {
		public override void InstallBindings() {

					Container.Bind<TowerDefence.Systems.CameraManager.ICameraContainer>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Systems.CameraManager.ICameraContainer>()).AsSingle();
					Container.Bind<TowerDefence.Entities.Enemies.Models.IEnemiesModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Entities.Enemies.Models.IEnemiesModel>()).AsSingle();
					Container.Bind<TowerDefence.Systems.LevelEditor.Models.ILevelEditorModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Systems.LevelEditor.Models.ILevelEditorModel>()).AsSingle();
					Container.Bind<TowerDefence.Systems.Selection.Models.ISelectionModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Systems.Selection.Models.ISelectionModel>()).AsSingle();
					Container.Bind<TowerDefence.Entities.Towers.Models.ITowerModels>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Entities.Towers.Models.ITowerModels>()).AsSingle();
					Container.Bind<TowerDefence.UI.Models.IUIContainers>().FromMethod(ctx => ModelFactory.Create<TowerDefence.UI.Models.IUIContainers>()).AsSingle();
					Container.Bind<TowerDefence.Systems.Waves.Models.IWavePlayStateModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Systems.Waves.Models.IWavePlayStateModel>()).AsSingle();
					Container.Bind<TowerDefence.Systems.WorldLoader.Models.IWorldDataModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Systems.WorldLoader.Models.IWorldDataModel>()).AsSingle();
				}
	}
}