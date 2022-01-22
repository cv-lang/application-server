using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Tools.Serializers
{
    /// <summary>
    /// Json serializer
    /// </summary>
    public class JsonSerializer : IJsonSerializer
    {
        public virtual string Serialize(object obj)
        {
            var settings = new JsonSerializerSettings();
            var text = JsonConvert.SerializeObject(obj, settings);
            return text;
        }

        public virtual T? Deserialize<T>(string json)
        {
            var settings = new JsonSerializerSettings();
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
