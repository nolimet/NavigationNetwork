using UnityEngine;
using Zenject;

namespace TowerDefence.Systems.LevelEditor
{
    public class LevelEditorInitializer : MonoBehaviour
    {
        [Inject] private LevelEditorController levelEditorController;

        private void Start()
        {
            levelEditorController.ResetLevelEditor();
        }
    }
}