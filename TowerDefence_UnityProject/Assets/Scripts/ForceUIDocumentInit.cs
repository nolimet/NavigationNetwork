using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ForceUIDocumentInit : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
       var documents = FindObjectsOfType<UIDocument>();
       Debug.Log($"Found {documents.Length} documents");
       foreach (var document in documents)
       {
           document.SendMessage("Awake");
           document.SendMessage("OnEnable");
           Debug.Log(document.rootVisualElement);
       }
    }
}
