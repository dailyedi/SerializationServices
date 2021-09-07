using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationServices
{
    public static class JsonXmlConverter
    {
        public static string ConvertXmlToJson<T>(string xml) => 
            JSONSerializationServices.Serialize(XMLSerializationServices.Deserialize<T>(xml));

        public static string ConvertJsonToXml<T>(string json) => 
            XMLSerializationServices.Serialize(JSONSerializationServices.Deserialize<T>(json));
    }
}
