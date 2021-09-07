using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationServices
{
    public static class XMLSerializationServices
    {
        /// <summary>
        /// Serializes the data as XML in the object to the designated file path
        /// </summary>
        /// <typeparam name="T">Type of Object to serialize</typeparam>
        /// <param name="dataToSerialize">Object to serialize</param>
        /// <param name="filePath">FilePath for the XML file</param>
        public static void SerializeToFile<T>(T dataToSerialize, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                var writer = new XmlTextWriter(stream, Encoding.Default) { Formatting = Formatting.Indented };
                new XmlSerializer(typeof(T)).Serialize(writer, dataToSerialize);
                writer.Close();
            }
        }

        public static void SerializeToStream<T>(T dataToSerialize, Stream st)
        {
            var writer = new XmlTextWriter(st, Encoding.Default) { Formatting = Formatting.Indented };
            new XmlSerializer(typeof(T)).Serialize(writer, dataToSerialize);
            writer.Close();
        }

        /// <summary>
        /// Deserializes the data in the XML file into an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">FilePath to XML file</param>
        /// <returns>Object containing deserialized data</returns>
        public static T DeserializeFromFile<T>(string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    return (T)(new XmlSerializer(typeof(T))).Deserialize(stream);
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }

        public static T DeserializeFromStream<T>(Stream st)
        {
            try
            {
                return (T)(new XmlSerializer(typeof(T))).Deserialize(st);
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}