using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Examples.Paths
{
    [CustomEditor(typeof(ExamplePathGenerator))]
    public class ExamplePathGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var target = this.target as ExamplePathGenerator;
            using (var disableScope = new EditorGUI.DisabledGroupScope(!Application.isPlaying))
            {
                using (var h1 = new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Build Path"))
                    {
                        target.BuildExample();
                    }

                    if (GUILayout.Button("Generate Path File"))
                    {
                        target.GenerateExamplePath();
                    }

                    using (var disableScope2 = new EditorGUI.DisabledGroupScope(target.ConstructedPath == null))
                    {
                        if (GUILayout.Button("Create Walker"))
                        {
                            target.CreateWalker();
                        }
                    }
                    GUILayout.FlexibleSpace();
                }
                if (target.ConstructedPath != null && Application.isPlaying)
                {
                    var paths = target.ConstructedPath.paths;

                    foreach (var path in paths)
                    {
                        EditorGUILayout.LabelField($"path {path.Key}");
                        using (var indent = new EditorGUI.IndentLevelScope(1))
                        {
                            EditorGUILayout.CurveField("X", path.Value.curveX);
                            EditorGUILayout.CurveField("Y", path.Value.curveY);
                            EditorGUILayout.CurveField("Z", path.Value.curveZ);
                        }
                        EditorGUILayout.Space();
                    }
                }
            }
        }
    }
}