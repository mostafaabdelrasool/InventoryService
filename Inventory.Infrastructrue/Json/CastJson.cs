using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructrue.Json
{
    public static class CastJson
    {
       
        public static string ToJson(this object data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string output = JsonConvert.SerializeObject(data, settings);
            return output;
        }
        public static T ToType<T>(this string data)
        {
            return (T)Excute(data, typeof(T));
        }
        public static object ToType(this string data,Type type)
        {
            return  Convert.ChangeType( Excute(data, type),type);
        }
        private static object Excute(string data ,Type type)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            var result = JsonConvert.DeserializeObject(data,type, settings);
            return result;
        }
    }
}
