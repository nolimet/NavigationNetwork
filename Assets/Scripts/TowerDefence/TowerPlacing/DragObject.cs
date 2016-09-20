using UnityEngine;
using System.Collections;

namespace TowerDefence.Building
{
    public class DragObject : MonoBehaviour
    {
        [Tooltip("Tag objects that can be moved around have"),HideInInspector]
        public string dragableObjectTag;
        [HideInInspector]
        public int tagIndex;

        [SerializeField]
        new Camera camera;

        Transform SelectedObject;
        void Start()
        {
            if (camera == null)
            {
                camera = FindObjectOfType<Camera>();
            }
        }

        void Update()
        {
            onMouseDown();
            onMouseUp();
            onMousePressed();
        }

        void onMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.tag == dragableObjectTag)
                    {
                        SelectedObject = hit.transform;
                    }
                    else
                    {
                        SelectedObject = null;
                    }
                }  
                else
                {
                    SelectedObject = null;
                }
            }
        }

        void onMouseUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                SelectedObject = null;
            }
        }

        void onMousePressed()
        {
            if (Input.GetMouseButton(0) && SelectedObject)
            {
                Vector3 screenPoint = Input.mousePosition;
                screenPoint.z = camera.nearClipPlane;
                Vector3 p = camera.ScreenToWorldPoint(screenPoint);
                p.z = 0;
                SelectedObject.position = p;
            }
        }
    }
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(DragObject))]
    public class CannonTowerEditor : UnityEditor.Editor
    {

        
        public override void OnInspectorGUI()
        {
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

            base.OnInspectorGUI();
            DragObject d = target as DragObject;
            d.tagIndex = UnityEditor.EditorGUILayout.Popup(d.tagIndex, tags);


            d.dragableObjectTag = tags[d.tagIndex];

            UnityEditor.EditorUtility.SetDirty(target);
        }
    }
#endif
}