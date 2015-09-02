using UnityEngine;
using System.Collections;

namespace util
{
    public class InputTextField : MonoBehaviour
    {
        public string stringToEdit = "Hello World";
        public GUIStyle Style;
        
        [Range (0,1)]
        public float posX;

        [Range(0, 1)]
        public float posY;

        [Range(0, 1)]
        public float SizeX = 0.1f;
        [Range(0, 1)]
        public float SizeY =  0.1f;

        private Vector2 realPos;
        private Vector2 realSize;

        void Update(){
            realPos = new Vector2(Screen.width * posX, Screen.height * posY);
            realSize = new Vector2(Screen.width * SizeX, Screen.height * SizeY);
            //Debug.Log(Screen.height);
            //Debug.Log(realSize +""+ realPos);
            GetComponent<GUIText>().text = stringToEdit;
        }

        

        void OnGUI()
        {
            stringToEdit = GUI.TextField(new Rect(realPos.x,realPos.y,realSize.x,realSize.y), stringToEdit, 265,Style);
        }
    }
}