using UnityEngine;
using System.Collections;

public class GoToLink : MonoBehaviour {

    public string url = "https://www.facebook.com/pages/The-Moonrabbit/459833187476932?ref=ts&fref=ts";
    void OnGUI()
    {
        if(GUI.Button(new Rect(50,50,100,50),"GO TO LINK")){
            Application.OpenURL(url);
        }
    }
}
