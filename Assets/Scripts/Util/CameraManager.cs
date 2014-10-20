using UnityEngine;
using System.Collections;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {

        public Transform Target;
        public float MoveTime = 1f;
        public float speed = 1f;
        public float accurcy = 0.1f;

        void Start()
        {
            if (Target == null)
            {
                Destroy(this);
            }

            Vector3 temp = Target.position;
            temp = Target.transform.position;
            temp.y = transform.position.y;
            transform.position = temp;
        }

        void Update()
        {
            transform.position = new Vector3(transform.position.x, Target.position.y, transform.position.z);
            Vector3 temp = Target.position;
            temp.y = transform.position.y;

            if (Vector3.Distance(transform.position, temp) >= accurcy)
            {
                Vector3 dir = Target.position - transform.position;
                dir.y = 0;
                transform.Translate(dir * speed * Time.deltaTime);
                Debug.DrawLine(transform.position, temp, Color.green);
            }
            transform.position = new Vector3(transform.position.x, 17f, transform.position.z);
        }
    }
}