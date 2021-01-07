using UnityEngine;
using TowerDefence.Utils;
using System.Collections;

namespace TowerDefence.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Initilazition
        public static InputManager instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<InputManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO PlacementManager FOUND! Check what is calling it. Add it to scene if it's missing");
                return null;
            }
        }
        static InputManager _instance;
        #endregion
        public event VoidDelegate onLeftMouseClick, onEscape, onRightMouseClick;

        void Update()
        {
            if (!GameManager.isPaused)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (onLeftMouseClick != null)
                    {
                        onLeftMouseClick();
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (onRightMouseClick != null)
                    {
                        onRightMouseClick();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (onEscape != null)
                {
                    onEscape();
                }
            }
        }
    }
}
