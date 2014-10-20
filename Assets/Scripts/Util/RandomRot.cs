using UnityEngine;
using System.Collections;

public class RandomRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.Euler(360f * Random.value, 360f * Random.value, 360f * Random.value);
	}
}
