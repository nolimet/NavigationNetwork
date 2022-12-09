using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ILevelEditorModel : IModelBase
    {
        string levelName { get; set; }
        IWavesModel waves { get; set; }
        IWorldLayoutModel world { get; set; }
    }
}