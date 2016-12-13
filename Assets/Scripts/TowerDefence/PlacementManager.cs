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
        GameObject ghost;
        Vector3 StartPos = Vector3.zero;
        bool isNew = false;

        public void onBeginPlace(GameObject placeAble, bool isNew)
        {
            if (!isPlacing)
            {
                isPlacing = true;
            }
            this.placeAble = placeAble;
            this.isNew = isNew;
            ghost = makeGhostSprite(placeAble);
            InputManager.instance.onLeftMouseClick += EndPlacement;
            InputManager.instance.onRightMouseClick += CancelPlacement;

            if (!isNew)
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
                ghost.transform.position = newPos;

                if (!PlaceLocationIsValid())
                {
                    ghost.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0, 0, 0.5f);
                }
                else
                {
                    ghost.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                }
            }
        }

        public void EndPlacement()
        {
            if (PlaceLocationIsValid())
            {
                
                if (isNew)
                {
                    //TODO tell resource manager to substractCost
                }
                else
                {
                    isPlacing = false;
                    InputManager.instance.onLeftMouseClick -= EndPlacement;
                    InputManager.instance.onRightMouseClick -= CancelPlacement;
                    placeAble.transform.position = ghost.transform.position;
                    Destroy(ghost);
                    ghost = null;
                    placeAble = null;
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
            if (!isNew)
            {
                Destroy(ghost);
                placeAble = null;
            }
            else
            {
                //RemoveGhostImage;
                Destroy(ghost);
                placeAble = null;
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
                Bounds p = ghost.transform.getBounds();
                
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

        GameObject makeGhostSprite(GameObject Gameobj)
        {
            GameObject g = new GameObject("PlacementGhost");
            SpriteRenderer spr = g.AddComponent<SpriteRenderer>();
            spr.sprite = Gameobj.GetComponent<SpriteRenderer>().sprite;

            spr.color = new Color(1, 1, 1, 0.5f);

            return g;
        }
    }
}
