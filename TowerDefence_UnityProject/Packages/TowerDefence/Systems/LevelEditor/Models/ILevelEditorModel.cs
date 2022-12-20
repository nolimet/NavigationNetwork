using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ILevelEditorModel : IModelBase
    {
        bool RebuildingWorld { get; set; }
        string LevelName { get; set; }
        IWavesModel Waves { get; set; }
        IWorldLayoutModel World { get; set; }
    }
}