using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefence.Managers
{
    public class CameraManager : MonoBehaviour
    {
        float speed = 5f;
        Vector2 ViewSize;
        [SerializeField]
        new Camera camera;

        void Start()
        {
            GameManager.instance.onLoadLevel += onLevelLoaded;
        }

        void onLevelLoaded()
        {
            camera.transform.position = new Vector3(0, 0, camera.transform.position.z);
            ViewSize = GameManager.currentLevel.worldSize.ViewSize;
        }

        Vector3 CameraInViewAfterMove(Vector3 move)
        {
            Vector2 res = new Vector2(Screen.width, Screen.height);
            bool[]
                a = CameraOutOfViewBox((camera.ScreenToWorldPoint(res) + move), ViewSize, 1),
                b = CameraOutOfViewBox((camera.ScreenToWorldPoint(Vector2.zero) + move), -ViewSize, -1);

            Vector3 c = new Vector3(1,1);

            if (a[0] || b[0])
            {
                c.x = 0;
            }

            if (a[1] || b[1])
            {
                c.y = 0;
            }

            return c;
        }
        bool[] CameraOutOfViewBox(Vector3 Position, Vector3 ViewConstraint, int neg)
        {
            Position *= neg;
            return new bool[] { (Position.x > ViewSize.x), (Position.y > ViewSize.y) };
        }

        Vector3 move;
        void Update()
        {

            if (GameManager.isPaused)
            {
                return;
            }
            /*
             * Standaard desktop input
             * 
             * Touch Input
             */

            move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            move = move * speed * Time.deltaTime;
            move.Scale(CameraInViewAfterMove(move));
            camera.transform.Translate(move);
        }
    }
}