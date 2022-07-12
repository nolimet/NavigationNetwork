using UnityEditor;
using UnityEngine;

namespace TowerDefence.Examples.Paths
{
    [CustomEditor(typeof(ExamplePathGenerator))]
    public class ExamplePathGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var target = this.target as ExamplePathGenerator;
            if (!target) { return; }
            using (var disableScope = new EditorGUI.DisabledGroupScope(!Application.isPlaying))
            {
                using (new EditorGUILayout.HorizontalScope())
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

                        if (GUILayout.Button("Create 20 walkers"))
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                target.CreateWalkerDelayed(Random.Range(0, 1f));
                            }
                        }
                    }

                    GUILayout.FlexibleSpace();
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Build Gridworld"))
                    {
                        target.BuildGridWorld();
                    }

                    if (GUILayout.Button("Create grid walker"))
                    {
                        target.CreateGridWalker();
                    }
                    GUILayout.FlexibleSpace();
                }
            }
            if (target.ConstructedPath != null && Application.isPlaying)
            {
                var paths = target.ConstructedPath.paths;

                foreach (var path in paths)
                {
                    using (var h1 = new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("DumpCurve Keys"))
                        {
                            path.Value.LogCurveValues();
                        }
                        GUILayout.FlexibleSpace();
                    }
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