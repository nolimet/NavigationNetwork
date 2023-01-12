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
            levelEditorModel.Waves = ModelFactory.Create<IWavesModel>();
            levelEditorModel.World = ModelFactory.Create<IWorldLayoutModel>();
            levelEditorModel.World.Height = 1;
            levelEditorModel.World.Width = 1;
        }
    }
}