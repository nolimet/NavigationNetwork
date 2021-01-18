using UnityEngine;
using System.Collections;
namespace NavigationNetwork
{
    public class Options : MonoBehaviour
    {

        void OnGUI()
        {

            GUI.Label(new Rect(20, 20, 200, 40), "NavigationNetwork.Options is obsolite please remove!");
            //if (GUI.Button(new Rect(20, 20, 200, 40), "Use LightParticles : " + NavUtil.useLightParticles))
            //    NavUtil.useLightParticles = !NavUtil.useLightParticles;
            //GUI.Label(new Rect(20, 60, 200, 40), "Real TPS : " + NavUtil.RealTPS);
            //if (GUI.Button(new Rect(230, 20, 180, 40), "Restart System"))
            //    Application.LoadLevel(Application.loadedLevel); // this thing is obsolite anyway
        }
    }

    
}