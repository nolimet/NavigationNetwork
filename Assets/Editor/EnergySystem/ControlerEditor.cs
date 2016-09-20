using UnityEngine;
using System.Collections;
using UnityEditor;
using NavigationNetwork;
namespace NavigationNetwork._Editor
{
    [CustomEditor(typeof(NavigationNetworkControler))]
    public class ControlerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            NavigationNetworkControler net = (NavigationNetworkControler)target;
            //net.TicksPerSecond = EditorGUILayout.IntSlider("TickPerSecond", net.TicksPerSecond, 1, 144);
            //EditorGUILayout.LabelField("TimePerTick: " + 1000 / net.TicksPerSecond + "ms");
            net.collectAllNetworkObjects = EditorGUILayout.Toggle("Collect all network Objects", net.collectAllNetworkObjects);
            EditorGUILayout.LabelField("CurrenTPS " + net.tps);


            if (GUILayout.Button("Restart Controler"))
            {
                net.Stop();
                net.Start();
                
            }
        }
    }
}