using DataBinding.BaseClasses;
using DataBinding.Helpers;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ILevelEditorModel : IModelBase
    {
        IWavesModel waves { get; set; }
        IWorldLayout world { get; set; }
    }
}