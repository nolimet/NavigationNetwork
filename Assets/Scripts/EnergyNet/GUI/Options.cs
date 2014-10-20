using UnityEngine;
using System.Collections;
namespace EnergyNet
{
    public class Options : MonoBehaviour
    {

        void OnGUI()
        {
            if (GUI.Button(new Rect(20, 20, 200, 40), "Use LightParticles : " + EnergyGlobals.useLightParticles))
                EnergyGlobals.useLightParticles = !EnergyGlobals.useLightParticles;
            GUI.Label(new Rect(20, 60, 200, 40), "Real TPS : " + EnergyGlobals.RealTPS);
            if (GUI.Button(new Rect(230, 20, 180, 40), "Restart System"))
                Application.LoadLevel(Application.loadedLevel);
        }
    }

    
}