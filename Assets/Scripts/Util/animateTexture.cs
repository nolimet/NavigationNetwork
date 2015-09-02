using UnityEngine;
using System.Collections;

public class animateTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().materials[0].SetColor(0,Color.black);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
