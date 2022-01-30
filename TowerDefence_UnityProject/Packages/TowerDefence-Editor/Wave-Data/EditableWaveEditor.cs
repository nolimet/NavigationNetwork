using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NoUtil.Editor;

public class EditableWaveEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}