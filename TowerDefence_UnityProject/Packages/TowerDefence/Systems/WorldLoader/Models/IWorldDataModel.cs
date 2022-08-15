using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.Systems.Waves.Data;

namespace TowerDefence.Systems.WorldLoader.Models
{
    [DataModel(Shared = true, AddToZenject = true)]
    public interface IWorldDataModel : IModelBase
    {
        string LevelName { get; set; }

        LevelType LevelType { get; set; }

        Wave[] Waves { get; set; }
    }
}