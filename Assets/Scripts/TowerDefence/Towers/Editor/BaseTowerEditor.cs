using UnityEngine;
using UnityEditor;
using Util;
namespace TowerDefence.EditorScripts
{
    [CustomEditor(typeof(BaseTower))]
    public class BaseTowerEditor : Editor
    {
        
            protected void OnSceneGUI()
            {
            BaseTower t = (BaseTower)target;
                //FireOffset Handle;
                EditorGUI.BeginChangeCheck();
                Vector3 pos1 = Handles.FreeMoveHandle(t.fireLocation.Rotate(t.transform.rotation) + (Vector2)t.transform.position, Quaternion.identity, .05f, new Vector3(.5f, .5f, .5f), Handles.CircleCap);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Free Move Fire Location");
                    t.fireLocation = pos1 - t.transform.position;
                    t.fireLocation = t.fireLocation.Rotate(-t.transform.rotation.eulerAngles.z);
                    //t.Update();
                }

            //ContextMenuOffset
                EditorGUI.BeginChangeCheck();
                Vector3 pos2 = Handles.FreeMoveHandle(t.ContextMenuOffset.Rotate(t.transform.rotation) + (Vector2)t.transform.position, Quaternion.identity, .05f, new Vector3(.5f, .5f, .5f), Handles.CircleCap);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Free Move ContextMenu Location");
                    t.ContextMenuOffset = pos1 - t.transform.position;
                    t.ContextMenuOffset = t.ContextMenuOffset.Rotate(-t.transform.rotation.eulerAngles.z);
                    //t.Update();
                }
        }
    }
}
