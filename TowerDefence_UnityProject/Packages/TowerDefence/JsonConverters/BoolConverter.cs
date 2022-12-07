using System;
using System.Text;
using Newtonsoft.Json;

namespace TowerDefence.JsonConverters
{
    internal sealed class BoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value?.ToString() == "1";
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }

    internal sealed class BoolArrayConverter : JsonConverter
    {
        private static readonly StringBuilder stringBuilder = new();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is bool[] arr)
            {
                stringBuilder.Clear();
                foreach (bool b in arr)
                {
                    stringBuilder.Append(b ? 1 : 0);
                }

                writer.WriteValue(stringBuilder.ToString());
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string str)
            {
                bool[] arr = new bool[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    arr[i] = str[i] == '1';
                }

                return arr;
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool[]);
        }
    }
}