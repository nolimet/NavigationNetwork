using UnityEngine;
using Util;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence.Managers
{
    //ADD ADD NEW TOWER TO TOWER BOUNDS!

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
        public event Utils.VoidDelegate onSuccesfullPlacement;

        [SerializeField]
        GameObject BackDrop;
        Bounds BackDropBounds;

        List<Bounds> Towers;

        public bool isPlacing;
        GameObject placeAble;
        GameObject ghost;
        Vector3 StartPos = Vector3.zero;
        bool isNew = false;

        void Awake()
        {
            GameManager.instance.onLoadLevel += Instance_onLoadLevel;
        }

        public static void Place(GameObject placeAble, bool isNew)
        {
            instance.onBeginPlace(placeAble, isNew);
        }

        private void Instance_onLoadLevel()
        {
            BackDrop.transform.localScale = GameManager.currentLevel.worldSize.BuildablePlaneSize;
            BackDropBounds = BackDrop.transform.getBounds();

            Towers = new List<Bounds>();

            BaseTower[] tb = FindObjectsOfType<BaseTower>();
            foreach (BaseTower t in tb)
            {
                Towers.Add(t.transform.getBounds());
            }
        }

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
                    InputManager.instance.onLeftMouseClick -= EndPlacement;
                    InputManager.instance.onRightMouseClick -= CancelPlacement;

                    if (onSuccesfullPlacement != null)
                    {
                        onSuccesfullPlacement();
                    }

                    Destroy(ghost);
                    ghost = null;
                    placeAble = null;
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
                Destroy(placeAble);
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

                foreach (Bounds b in Towers)
                {
                    if (p.Intersects(b))
                    {
                        return false;
                    }
                }
                if (BackDrop != null)
                {
                    if (!BackDropBounds.ContainBounds(p))   
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
            spr.sprite = Gameobj.GetComponentInChildren<SpriteRenderer>().sprite;

            spr.color = new Color(1, 1, 1, 0.5f);

            return g;
        }
    }
}
