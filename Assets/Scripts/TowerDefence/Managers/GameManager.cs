using UnityEngine;
using System.Collections;

namespace TowerDefence.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<GameManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO GAMEMANGER FOUND! Check what is calling it");
                return null;
            }
        }
        static GameManager _instance;
        public static Utils.Level currentLevel { get { return instance._currentLevel; } set { if (Application.isEditor) { instance._currentLevel = value; } } }
        public static int currentWave {get { return instance._currentWave; } set { if (Application.isEditor) { instance._currentWave = value; } } }
        public event Utils.VoidDelegate onStartWave;
        public event Utils.VoidDelegate onLoadLevel;

        [SerializeField]
        private Utils.Level _currentLevel;
        [SerializeField]
        private int _currentWave;

        public void Start()
        {
            _currentLevel = loadLevel("lvl0");
            _currentWave = 0;
            if(onLoadLevel != null)
            {
                onLoadLevel();
            }

            if(onStartWave != null)
            {
                onStartWave();
            }
        }

        public Utils.Level loadLevel(string LevelName)
        {
            return JsonUtility.FromJson<Utils.Level>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/" + LevelName + ".json", System.Text.Encoding.UTF32));
        }

        public void SaveLevel()
        {
            System.IO.File.WriteAllText(Application.streamingAssetsPath +"/" +  _currentLevel.LevelName + ".json", JsonUtility.ToJson(_currentLevel), System.Text.Encoding.UTF32);
        }
    }
}
#if UNITY_EDITOR

namespace TowerDefence.EditorScripts
{
    using UnityEditor;
    using Managers;
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        string loadLevel;
        public override void OnInspectorGUI()
        {
            GameManager t = (GameManager)target;

            loadLevel = EditorGUILayout.TextField("Load Level with name", loadLevel);
            if(GUILayout.Button("Load Level"))
            {
                GameManager.currentLevel = t.loadLevel(loadLevel);
                
            }
            if(GUILayout.Button("Save Level"))
            {
                t.SaveLevel();
            }
            base.OnInspectorGUI();
        }

    }
}
#endif