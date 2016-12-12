using UnityEngine;
using Util;
using System.Collections;

namespace TowerDefence.Managers
{
    public class PlacementManager : MonoBehaviour
    {
        #region Initilazition
        public static PlacementManager instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<PlacementManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO PlacementManager FOUND! Check what is calling it. Add it to scene if it's missing");
                return null;
            }
        }
        static PlacementManager _instance;
        #endregion

        public event Utils.VoidDelegate onStartPlacing;
        public event Utils.VoidDelegate onEndPlacing; 

        public bool isPlacing;
        [SerializeField]
        GameObject placeAble;
        Vector3 StartPos = Vector3.zero;
        bool isGhost = false;

        public void onBeginPlace(GameObject placeAble, bool isGhost)
        {
            if (!isPlacing)
            {
                isPlacing = true;
            }
            this.placeAble = placeAble;
            this.isGhost = isGhost;

            InputManager.instance.onLeftMouseClick += EndPlacement;
            InputManager.instance.onRightMouseClick += CancelPlacement;

            if (!isGhost)
            {
                StartPos = placeAble.transform.position;
            }

            if (onStartPlacing != null)
            {
                onStartPlacing();
            }
        }

       private void Update()
        {
            //For debugging the bouding boxes of the nodes
            //foreach (Bounds b in PathManager.NodeBounds)
            //{
            //    Common.DrawBounds(b);
            //}
            if (isPlacing)
            {
                Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPos.z = placeAble.transform.position.z;
                placeAble.transform.position = newPos;
            }
        }

        public void EndPlacement()
        {
            if (PlaceLocationIsValid())
            {
                isPlacing = false;
                InputManager.instance.onLeftMouseClick -= EndPlacement;
                InputManager.instance.onRightMouseClick -= CancelPlacement;


                placeAble = null;
                if (isGhost)
                {
                    //TODO tell resource manager to substractCost
                }

                if (onEndPlacing != null)
                {
                    onEndPlacing();
                }
            }
        }

        public void CancelPlacement()
        {
            isPlacing = false;
            InputManager.instance.onLeftMouseClick -= EndPlacement;
            InputManager.instance.onRightMouseClick -= CancelPlacement;

            //RemovePlaceAble;
            if (!isGhost)
            {
                placeAble.transform.position = StartPos;
                placeAble = null;
            }
            else
            {
                //RemoveGhostImage;
            }

            if (onEndPlacing != null)
            {
                onEndPlacing();
            }

        }

        public bool PlaceLocationIsValid()
        {
            if(placeAble != null)
            {
                Bounds p = placeAble.transform.getBounds();
                
                foreach (Bounds b in PathManager.NodeBounds)
                {
                    if (p.Intersects(b))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
