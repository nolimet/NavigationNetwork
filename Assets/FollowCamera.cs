using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    [SerializeField]
    Transform target;

    [SerializeField]
    bool lockY;

   public enum RoundMode
    {
        floor,
        ceil,
        round,
        none
    }
    [SerializeField]
    RoundMode mode;

	void Update () {
        Vector3 targetPos = target.position;
	    switch (mode)
        {
            case RoundMode.ceil:
                transform.position = new Vector3(Mathf.Ceil(targetPos.x), lockY ? transform.position.y : Mathf.Ceil(targetPos.y), Mathf.Ceil(targetPos.z));
                break;

            case RoundMode.floor:
                transform.position = new Vector3(Mathf.Floor(targetPos.x), lockY ? transform.position.y : Mathf.Floor(targetPos.y), Mathf.Floor(targetPos.z));
                break;

            case RoundMode.round:
                transform.position = new Vector3(Mathf.Round(targetPos.x), lockY ? transform.position.y : Mathf.Round(targetPos.y), Mathf.Round(targetPos.z));
                break;

            case RoundMode.none:
                transform.position = new Vector3(targetPos.x, lockY ? transform.position.y : targetPos.y, targetPos.z);
                break;
        }
	}
}
