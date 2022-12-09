using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IWavesModel : IModelBase
    {
        IList<IWaveModel> spawnGroups { get; }
    }

    [DataModel(AddToZenject = false, Shared = false)]
    public interface IWaveModel : IModelBase
    {
        IList<ISpawnGroupModel> spawnGroups { get; }
    }

    [DataModel(AddToZenject = false, Shared = false)]
    public interface ISpawnGroupModel : IModelBase
    {
        string EnemyId { get; set; }

        int entranceId { get; set; }
        int exitId { get; set; }

        ulong groupSize { get; set; }
        double startDelay { get; set; }
        double spawnInterval { get; set; }
    }
}