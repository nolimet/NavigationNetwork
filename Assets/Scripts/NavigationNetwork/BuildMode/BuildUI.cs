using UnityEngine;
using System.Collections;
namespace NavigationNetwork.Build
{
    public class BuildUI : MonoBehaviour
    {
        #region DropDown vars
        public Rect dropDownRect = new Rect(125, 50, 125, 300);
        //public GUIStyle style;
        public string[] list = { "node","generator", "endPoint" };
        Vector2 scrollViewVector = Vector2.zero;

        int indexNumber;
        int UpdateSpeed;
        bool show;
        #endregion

        [SerializeField]
        Builder b;

        void Start()
        {
            if(b==null)
                b = GetComponent<Builder>();
        }

        void OnGUI()
        {
            #region DropDown Code
            if (GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), "" ))
            {
                if (!show)
                {
                    show = true;
                }
                else
                {
                    show = false;
                }
            }

            if (show)
            {
                scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height), scrollViewVector, new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))));
                
                GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length * 25))), "");

                for (int index = 0; index < list.Length; index++)
                {

                    if (GUI.Button(new Rect(0, (index * 25), dropDownRect.height, 25), "" ))
                    {
                        show = false;
                        indexNumber = index;
                    }

                    GUI.Label(new Rect(5, (index * 25), dropDownRect.height, 25), list[index]);

                }

                GUI.EndScrollView();
            }
            else
            {
                GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), list[indexNumber]);
            }
            #endregion

            if (GUI.Button(new Rect(dropDownRect.xMin - 100, dropDownRect.yMin - 30, 150, 20), "Create New " + list[indexNumber]))
                Build();
        }

        void Build()
        {
            b.create((Builder.Type)indexNumber);
        }
    }
}
