using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

    public Transform parent;

    public Vector2 direction;
    public float heightchange;

	// Use this for initialization
	void Start () {
        if (parent != null)
        {
            transform.parent = parent;
        }
        direction = (Random.value/2+0.5f) * direction;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.parent.position);
        float z = 0;
        if(heightchange!=0)
            z = Mathf.PingPong(Time.time*3,heightchange);
        Vector3 dir = new Vector3(direction.x,direction.y,z);
        transform.Translate(dir * Time.deltaTime);
	}
}
