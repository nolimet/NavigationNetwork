using System;
using System.IO;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.EditorScripts.Systems.Waves
{
    internal static class GridSettingsImageImporter
    {
        public static GridSettings Convert(string path)
        {
            if (!File.Exists(path))
            {
                return default;
            }

            var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            var bytes = File.ReadAllBytes(path);
            tex.LoadImage(bytes);

            bytes = null;
            uint h = (uint)tex.height;
            uint w = (uint)tex.width;
            var colors = tex.GetPixels32();
            var cells = new GridSettings.Cell[colors.Length];

            for (int i = 0; i < colors.Length; i++)
            {
                cells[i] = new GridSettings.Cell(colors[i].r, true);
            }

            return new GridSettings(h, w, cells, Array.Empty<Vector2Int>(), Array.Empty<Vector2Int>());
        }
    }
}