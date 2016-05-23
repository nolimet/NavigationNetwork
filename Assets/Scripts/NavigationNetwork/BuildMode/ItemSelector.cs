using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSelector: MonoBehaviour {


    public int Range;

    [SerializeField]
    private Transform currentItem;

    public Transform lastItem;

    private Vector3 ClickOffSet;

    private Color OriginalColor;

    [SerializeField]
    Material LinMaterial;

    [SerializeField]
    private Transform Selector;
    
    [SerializeField]
    private List<LineRenderer> lines = new List<LineRenderer>();

    private bool ColourChanged;

    void Update()
    {
        RayUpdate();
        selectorUpdate();
    }

    void selectorUpdate()
    {
        if (currentItem != null)
        {
            Selector.position = currentItem.position + new Vector3(0,-1,0);
            Selector.Rotate(0, 0, 1);
        }
        else if (lastItem != null)
        {
            Selector.position = lastItem.position + new Vector3(0, -1, 0);
            Selector.Rotate(0, 0, 1);
        }
        else
        {
            Selector.position = new Vector3(0, -200, 0);
        }
    }

    #region RayUpdate()
        #region HitRays
        void RayUpdate()
        {
            mouseRight();
            mouseLeft();
        }

        void mouseLeft()
        {
            if (Input.GetMouseButton(0))
            {
                if (currentItem != null)
                {
                    Vector3 temp = getNewPos();
                    temp.y = 0f;
                    currentItem.position = temp;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                selectObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (currentItem != null)
                {
                    if (ColourChanged)
                    {
                        currentItem.gameObject.GetComponent<Renderer>().material.color -= Color.gray;
                        ColourChanged = false;
                    }
                    lastItem = currentItem;
                    currentItem = null;
                }
            }
        }

   
        void selectObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null && hit.transform.gameObject.tag != "ground" && hit.transform.gameObject.tag != "NotDragable")
                {
                    hit.transform.gameObject.SendMessage("3dHitray", SendMessageOptions.DontRequireReceiver);
                    if (currentItem == hit.transform && currentItem != null)
                    {
                       // currentItem.gameObject.renderer.material.color = Color.white;
                        currentItem = null;
                    }
                    else
                    {
                        if (currentItem != null)
                            if (ColourChanged)
                            {
                                currentItem.gameObject.GetComponent<Renderer>().material.color -= Color.gray;
                                ColourChanged = false;
                            }
                        if (!ColourChanged)
                        {
                            hit.transform.gameObject.GetComponent<Renderer>().material.color += Color.gray;
                            ColourChanged = true;
                        }
                        currentItem = hit.transform;

                        ClickOffSet = currentItem.position - hit.point;
                        ClickOffSet.z = -1f;
                        return;
                    }

                }
            }
        }

        Vector3 getNewPos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, Range);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.transform.gameObject.tag == "ground")
                    {
                        return hit.point;
                    }
                }
            }
            return Vector3.zero;
        }
        #endregion

        #region LineSpider
        void CreatLines()
        {
            if (currentItem == null)
                return;

                if (currentItem.tag == NavigationNetwork.NavTags.EnergyNode || currentItem.tag == NavigationNetwork.NavTags.EnergyGenartor)
                {
                    NavigationNetwork.NavigationBase node = currentItem.gameObject.GetComponent<NavigationNetwork.NavigationBase>();

                    foreach (NavigationNetwork.NavigationBase n in node.ReturnInRangeNodes())
                    {
                        if (n != null)
                            lines.Add(CreateLine(node.transform.position, n.transform.position));
                    }
                }
            }
        

        void RemoveLines()
        {
            if (lines != null)
                foreach (LineRenderer l in lines)
                {
                    //lines.Remove(l);
                    Destroy(l.gameObject, 0.1f);
                }

            lines = new List<LineRenderer>();
        }

        LineRenderer CreateLine(Vector3 A, Vector3 B)
        {
            GameObject g = new GameObject();
            g.transform.parent = currentItem;
            g.transform.localPosition = Vector3.zero;

            LineRenderer l = g.AddComponent<LineRenderer>();
            l.material = LinMaterial;
            l.useWorldSpace = true;
            l.SetPosition(0, A);
            l.SetPosition(1, B);
            l.SetWidth(0.1f, 0.1f);

            return l;
        }

        void mouseRight()
        {
            if (Input.GetMouseButton(1))
            {
                if (currentItem != null)
                {

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                selectObject();
                CreatLines();
            }

            if (Input.GetMouseButtonUp(1) && currentItem != null && lines.Count > 0)
            {
                RemoveLines();
                if (ColourChanged)
                {
                    currentItem.gameObject.GetComponent<Renderer>().material.color -= Color.gray;
                    ColourChanged = false;
                    lastItem = currentItem;
                    currentItem = null;

                }
            }
        }

        #endregion
    #endregion
}
