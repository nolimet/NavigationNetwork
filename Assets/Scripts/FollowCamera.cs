using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    [SerializeField]
    Transform target;

    [SerializeField]
    bool lockY,
        lockZ;

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
                transform.position = new Vector3(Mathf.Ceil(targetPos.x), lockY ? transform.position.y : Mathf.Ceil(targetPos.y), lockZ ? transform.position.z : Mathf.Ceil(targetPos.z));
                break;

            case RoundMode.floor:
                transform.position = new Vector3(Mathf.Floor(targetPos.x), lockY ? transform.position.y : Mathf.Floor(targetPos.y), lockZ ? transform.position.z : Mathf.Floor(targetPos.z));
                break;

            case RoundMode.round:
                transform.position = new Vector3(Mathf.Round(targetPos.x), lockY ? transform.position.y : Mathf.Round(targetPos.y), lockZ ? transform.position.z : Mathf.Round(targetPos.z));
                break;

            case RoundMode.none:
                transform.position = new Vector3(targetPos.x, lockY ? transform.position.y : targetPos.y, lockZ ? transform.position.z : targetPos.z);
                break;
        }
	}
}
