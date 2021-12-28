namespace DataBinding
{
    [UnityEngine.CreateAssetMenu(fileName = "Model Installer", menuName = "Installers/Model Installer")]
    public class ModelsInstaller : Zenject.ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TowerDefence.World.Towers.Models.ITowerModel>().FromMethod(ctx => ModelFactory.Create<TowerDefence.World.Towers.Models.ITowerModel>()).AsSingle();
        }
    }
}