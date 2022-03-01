using System.IO;
using UnityEditor;

namespace TowerDefence.Systems.Waves.Data
{
    /// <summary>
    /// Inspector for .lvl assets
    /// </summary>
    [CustomEditor(typeof(DefaultAsset))]
    public class LevelDataEditor : Editor
    {
        private string path = string.Empty;
        private bool isLevel;

        private string levelData = null;

        private void OnEnable()
        {
            path = AssetDatabase.GetAssetPath(target);

            isLevel = path.EndsWith(".lvl");
            if (isLevel)
            {
                levelData = File.ReadAllText(path);
            }
        }

        private void OnDisable()
        {
            levelData = null;
        }

        public override void OnInspectorGUI()
        {
            if (isLevel)
            {
                LevelInspectorGUI();
            }
            else
            {
                base.OnInspectorGUI();
            }
        }

        private void LevelInspectorGUI()
        {
            EditorGUILayout.TextArea(levelData);
        }
    }
}