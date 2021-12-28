// ========================================================================
// !! DO NOT EDIT THIS SCRIPT !! This script is auto generated and will be replaced soon
// ========================================================================

namespace DataBinding
{
    public static class ModelFactory
    {
        private static TowerDefence.World.Towers.Models.ITowerModel CreateTowerModel()
        { return new TowerDefence.World.Towers.Models.TowerModel(); }

        public static T Create<T>() where T : DataBinding.BaseClasses.IModelBase
        {
            if (typeof(T) == typeof(TowerDefence.World.Towers.Models.ITowerModel)) { return (T)(CreateTowerModel()); }
            return default(T);
        }
    }
}