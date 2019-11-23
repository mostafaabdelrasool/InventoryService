using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructrue.Json
{
    public static class CastJson
    {
        public static string toJson(this object data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string output = JsonConvert.SerializeObject(data, settings);
            return output;
        }
        public static T toType<T>(this string data)
        {
            var result=JsonConvert.DeserializeObject<T>(data);
            return result;
        }
    }
}
