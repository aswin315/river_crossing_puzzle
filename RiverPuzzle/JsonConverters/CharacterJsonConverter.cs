using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiverPuzzle.Factories;
using RiverPuzzle1.Models;
using System;

namespace RiverPuzzle1.JsonConverters
{
    public class CharacterJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Character);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = GetCharacter(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private Character GetCharacter(Type objectType, JObject jsonObject)
        {
            string typeName = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase).ToString();
            return CharacterFactory.Build(typeName);
        }
    }
}
