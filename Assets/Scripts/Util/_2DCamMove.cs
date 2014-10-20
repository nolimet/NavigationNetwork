using UnityEngine;
using System.Collections;

public class _2DCamMove : MonoBehaviour {

    [SerializeField]
    private float speed;

	void Update () {
        transform.position += move();
	}

    Vector3 move()
    {
        Vector3 output = new Vector3();
        output.x = Input.GetAxis("Horizontal") * speed;
        output.y = Input.GetAxis("Vertical") * speed;

        return output;
    }
}
