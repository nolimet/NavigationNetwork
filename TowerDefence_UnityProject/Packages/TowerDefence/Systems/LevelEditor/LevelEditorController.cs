using DataBinding;
using TowerDefence.Systems.LevelEditor.Models;

namespace TowerDefence.Systems.LevelEditor
{
    public class LevelEditorController
    {
        private readonly ILevelEditorModel levelEditorModel;

        internal LevelEditorController(ILevelEditorModel levelEditorModel)
        {
            this.levelEditorModel = levelEditorModel;
        }

        public void ResetLevelEditor()
        {
            levelEditorModel.waves = ModelFactory.Create<IWavesModel>();
            levelEditorModel.world = ModelFactory.Create<IWorldLayoutModel>();
        }
    }
}