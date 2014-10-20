using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SomeTest : MonoBehaviour {

    public string Test = "wello world";
    public string ShowingText;
    //private string temp = "test";
   // [Range (0,1)]
    private Vector4 pos;
    public GUIStyle Style;
    public float delay;


    [Range(0, 1)]
    public float posX;

    [Range(0, 1)]
    public float posY;

    [Range(0, 1)]
    public float SizeX = 0.1f;
    [Range(0, 1)]
    public float SizeY = 0.1f;

    void Start()
    {
        pos.x = Screen.width * posX; pos.y = Screen.height * posY;
        pos.z=Screen.width * SizeX;pos.w= Screen.height * SizeY;
    }

     void Awake()
        {
            //guiText.text = "";
            StartCoroutine("Typer");
        }


     void OnApplicationStop()
     {
         StopCoroutine("Typer");
     }

     IEnumerator Typer()
     {
         yield return new WaitForSeconds(delay);
         foreach (char c in Test)
         {
             if (c =='\n')
             {
                 pos.y -= Style.fontSize+2;
             }
             ShowingText += c;
            // Debug.Log(ShowingText.Length);
             if (ShowingText.Length > 256)
             {
                 Debug.Log(ShowingText[1]);
                 ShowingText.Remove(0, 1);
             }
             yield return new WaitForSeconds(Random.value*0.01f);
         }
         StopCoroutine("Typer");
     }

      void OnGUI() {
         GUI.Label(new Rect(pos.x, pos.y, pos.z, pos.w), ShowingText, Style);
     }
}
