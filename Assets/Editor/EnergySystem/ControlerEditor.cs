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
            net.TicksPerSecond = EditorGUILayout.IntSlider("TickPerSecond", net.TicksPerSecond, 1, 40);
            EditorGUILayout.LabelField("TimePerTick: " + 1000 / net.TicksPerSecond + "ms");
            EditorGUILayout.LabelField("CurrenTPS " + net.tps);

            if (GUILayout.Button("Restart Controler"))
            {
                net.Start();
                net.Stop();
            }
        }
    }
}