using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NoUtil.Debugging;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.JsonConverters
{
    internal sealed class CellConverter : JsonConverter<GridSettings.Cell[]>
    {
        public override void WriteJson(JsonWriter writer, GridSettings.Cell[] value, JsonSerializer serializer)
        {
            byte[] weights = new byte[value.Length];
            StringBuilder supportsTower = new();
            for (int i = 0; i < value.Length; i++)
            {
                var cell = value[i];
                weights[i] = cell.weight;
                supportsTower.Append(cell.supportsTower ? '1' : '0');
            }

            writer.WriteValue($"{Convert.ToBase64String(weights)}|{supportsTower}");
        }

        public override GridSettings.Cell[] ReadJson(JsonReader reader, Type objectType, GridSettings.Cell[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is null) return Array.Empty<GridSettings.Cell>();

            var value = reader.Value as string ?? string.Empty;
            var split = value.Split("|");

            if (split.Length != 2)
            {
                $"Length does not equal 2 Length was {split.Length}".QuickCLog("Cell Converter", LogType.Error);
                return Array.Empty<GridSettings.Cell>();
            }

            byte[] weights = Convert.FromBase64String(split[0]);
            bool[] supportsTower = split[1].Select(x => x == '1').ToArray();

            var cells = new GridSettings.Cell[weights.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new GridSettings.Cell(weights[i], supportsTower[i]);
            }

            return cells;
        }
    }
}