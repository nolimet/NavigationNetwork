using DataBinding.Editor.Generation;
using UnityEditor;
using UnityEngine;

public static class BindGenerator
{
    private static string GenerationPath = $"{Application.dataPath}/../Packages/TowerDefence/Generated";

    [MenuItem("Plugins/Models/Create Models")]
    private static void CreateModels()

    {
        DataBindingCodeGeneration.Generate(GenerationPath);
        AssetDatabase.Refresh();
    }

    [MenuItem("Plugins/Models/Clear Models")]
    private static void ClearModels()
    {
        DataBindingCodeGeneration.GenerateEmpty(GenerationPath);
        AssetDatabase.Refresh();
    }
}