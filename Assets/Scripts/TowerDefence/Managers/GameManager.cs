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
        public static bool isPaused;
        public event Utils.VoidDelegate onStartWave;
        public event Utils.VoidDelegate onLoadLevel;

        [SerializeField]
        private Utils.Level _currentLevel;
        [SerializeField]
        private int _currentWave;

        public void Start()
        {
            StartCoroutine(LoadGameLevel("lvl0"));
            _currentWave = 0;
            
            InputManager.instance.onEscape += onEscape;
        }

        private void onEscape()
        {
            isPaused = !isPaused;
        }

        public Utils.Level loadLevel(string LevelName)
        {
            return JsonUtility.FromJson<Utils.Level>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/" + LevelName + ".json", System.Text.Encoding.UTF32));
        }


        IEnumerator LoadGameLevel(string LevelName)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                WWW request = new WWW("http://jessestam.nl/Games/WebGL/TowerDef/StreamingAssets/" + LevelName + ".json");
                while (!request.isDone)
                {
                    Debug.Log("got: " + request.progress);
                    yield return new WaitForEndOfFrame();
                }

                Debug.Log("done loading!");
                //string lvlJson = System.Text.Encoding.UTF8.GetString(request.bytes);
                Debug.Log(request.text);
                _currentLevel = JsonUtility.FromJson<Utils.Level>(request.text);

                yield return new WaitForEndOfFrame();
            }
            else
            {
                _currentLevel = JsonUtility.FromJson<Utils.Level>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/" + LevelName + ".json", System.Text.Encoding.UTF8));
            }
            if (onLoadLevel != null)
            {
                onLoadLevel();
            }

            ///Should me removed!
            Invoke("StartWave", 2f);
           
        }

        public void SaveLevel()
        {
            System.IO.File.WriteAllText(Application.streamingAssetsPath +"/" +  _currentLevel.LevelName + ".json", JsonUtility.ToJson(_currentLevel), System.Text.Encoding.UTF8);
        }

        public void StartWave()
        {
            if (onStartWave != null)
            {
                onStartWave();
            }
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