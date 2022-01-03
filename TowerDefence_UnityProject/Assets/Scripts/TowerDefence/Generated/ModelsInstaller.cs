
namespace DataBinding {
[UnityEngine.CreateAssetMenu(fileName = "Model Installer", menuName = "Installers/Model Installer")]
	public class ModelsInstaller : Zenject.ScriptableObjectInstaller {
		public override void InstallBindings() {

					Container.Bind<TowerDefence.Entities.Enemies.Models.IEnemiesModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.Entities.Enemies.Models.IEnemiesModel>()).AsSingle();
					Container.Bind<TowerDefence.World.Towers.Models.ITowerModels>().FromMethod(ctx => ModelFactory.Create<TowerDefence.World.Towers.Models.ITowerModels>()).AsSingle();
				}
	}
}