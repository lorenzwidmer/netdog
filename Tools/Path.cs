using Newtonsoft.Json;
using System;
using System.IO;

namespace NetDog
{
    [JsonConverter(typeof(PathConverter))]
    class Path
    {
        public readonly string RelativePath;
        private readonly string AbsolutePath;

        public Path(string path)
        {
            RelativePath = path;
            AbsolutePath = path;

            if (AbsolutePath.Substring(0, 2) == @".\")
            {
                AbsolutePath = Directory.GetCurrentDirectory() + AbsolutePath.Substring(1);
            }
        }

        public new string ToString()
        {
            return AbsolutePath;
        }

        public static implicit operator string(Path self)
        {
            return self.ToString();
        }
    }

    class PathConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(Path);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new Path(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Path)value).RelativePath);
        }
    }
}
