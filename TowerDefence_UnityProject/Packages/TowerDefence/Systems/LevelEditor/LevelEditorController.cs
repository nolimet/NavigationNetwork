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
    }
}