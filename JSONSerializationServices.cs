using System;
using System.IO;
using Newtonsoft.Json;

namespace SerializationServices
{
    public static class JSONSerializationServices
    {

        /// <summary>
        /// Serializes the data as XML in the object to the designated file path
        /// </summary>
        /// <typeparam name="T">Type of Object to serialize</typeparam>
        /// <param name="dataToSerialize">Object to serialize</param>
        /// <param name="filePath">FilePath for the XML file</param>
        public static void SerializeToFile<T>(T dataToSerialize, string filePath) =>
            File.WriteAllText(filePath, JsonConvert.SerializeObject(dataToSerialize));

        public static void SerializeToStream<T>(T dataToSerialize, StreamWriter sw) => 
            sw.Write(JsonConvert.SerializeObject(dataToSerialize));

        public static async void SerializeToStreamAsync<T>(T dataToSerialize, StreamWriter sw) => 
            await sw.WriteAsync(JsonConvert.SerializeObject(dataToSerialize));

        public static void SerializeToStream<T>(T dataToSerialize, MemoryStream ms)
        {
            using (var sw = new StreamWriter(ms))
                sw.Write(JsonConvert.SerializeObject(dataToSerialize));
        }

        public static async void SerializeToStreamAsync<T>(T dataToSerialize, MemoryStream ms)
        {
            using (var sw = new StreamWriter(ms))
                await sw.WriteAsync(JsonConvert.SerializeObject(dataToSerialize));
        }

        /// <summary>
        /// Deserializes the data in the XML file into an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">FilePath to XML file</param>
        /// <returns>Object containing deserialized data</returns>
        public static T DeserializeFromFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                return DeserializeFrom<T>(stream);
        }

        public static T DeserializeFrom<T>(Stream st)
        {
            using (var sr = new StreamReader(st))
                return DeserializeFrom<T>(sr);
        }

        public static T DeserializeFrom<T>(StreamReader sr)
        {
            using (var jsonTextReader = new JsonTextReader(sr))
                return DeserializeFrom<T>(jsonTextReader);
        }

        public static T DeserializeFrom<T>(JsonTextReader jsonTextReader)
        {
            try
            {
                return new JsonSerializer().Deserialize<T>(jsonTextReader);
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}