using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TowerDefence.World.Grid;

namespace TowerDefence.JsonConverters
{
    internal class GridSettingsGridLayoutConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(GridSettings.GridLayout);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (existingValue is string value)
            {
                List<GridSettings.LayoutNode> nodes = new();
                var rows = value.Split('\n');
                foreach (var row in rows)
                {
                    var elements = row.Split(' ');
                    foreach (var element in elements)
                    {
                        byte v = Convert.ToByte(element);
                        nodes.Add(new GridSettings.LayoutNode(v));
                    }
                }
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
