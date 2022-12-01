using System;
using Newtonsoft.Json;
using TowerDefence.World.Grid.Data;

namespace TowerDefence.JsonConverters
{
    internal sealed class CellConverter : JsonConverter<GridSettings.Cell[]>
    {
        public override void WriteJson(JsonWriter writer, GridSettings.Cell[] value, JsonSerializer serializer)
        {
            byte[] bytes = new byte[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                bytes[i] = value[i].weight;
            }

            writer.WriteValue(Convert.ToBase64String(bytes));
        }

        public override GridSettings.Cell[] ReadJson(JsonReader reader, Type objectType, GridSettings.Cell[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is null) return Array.Empty<GridSettings.Cell>();

            var value = reader.Value as string ?? string.Empty;
            byte[] bytes = Convert.FromBase64String(value);

            var cells = new GridSettings.Cell[bytes.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new GridSettings.Cell(bytes[i]);
            }

            return cells;
        }
    }
}