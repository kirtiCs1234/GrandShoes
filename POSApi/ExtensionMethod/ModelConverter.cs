using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApi.ExtensionMethod
{
    public static class ModelConverter
    {
        public static T ToModelConverter<T>(this Object obj)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body);
            return model;
        }
    }
}