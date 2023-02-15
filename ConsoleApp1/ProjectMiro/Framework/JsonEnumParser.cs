using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectMiro.Framework
{
    public class JsonEnumConverter : JsonConverter<AccessEnum.Access>
    {
        public override void WriteJson(JsonWriter writer, AccessEnum.Access value, JsonSerializer serializer)
        {
            string OutValue = AccessEnum.AccessDictionary.FirstOrDefault(x => x.Value == value).Key;
            writer.WriteValue(OutValue);
        }

        public override AccessEnum.Access ReadJson(JsonReader reader, Type objectType, AccessEnum.Access existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
