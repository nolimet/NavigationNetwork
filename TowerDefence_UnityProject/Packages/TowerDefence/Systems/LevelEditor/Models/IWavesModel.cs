using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IWavesModel : IModelBase
    {
        IList<IWaveModel> SpawnGroups { get; }
    }

    [DataModel(AddToZenject = false, Shared = false)]
    public interface IWaveModel : IModelBase
    {
        IList<ISpawnGroupModel> SpawnGroups { get; }
    }

    [DataModel(AddToZenject = false, Shared = false)]
    public interface ISpawnGroupModel : IModelBase
    {
        string EnemyId { get; set; }

        int EntranceId { get; set; }
        int ExitId { get; set; }

        ulong GroupSize { get; set; }
        double StartDelay { get; set; }
        double SpawnInterval { get; set; }
    }
}