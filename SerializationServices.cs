using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationServices
{
    public static class SerializationServices
    {

        /// <summary>
        /// Serializes the data in the object to the designated file path
        /// </summary>
        /// <typeparam name="T">Type of Object to serialize</typeparam>
        /// <param name="dataToSerialize">Object to serialize</param>
        /// <param name="filePath">FilePath for the XML file</param>
        public static void Serialize<T>(T dataToSerialize, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                var serializer = new XmlSerializer(typeof(T));
                var writer = new XmlTextWriter(stream, Encoding.Default) { Formatting = Formatting.Indented };
                serializer.Serialize(writer, dataToSerialize);
                writer.Close();
            }
        }

        /// <summary>
        /// Deserializes the data in the XML file into an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">FilePath to XML file</param>
        /// <returns>Object containing deserialized data</returns>
        public static T Deserialize<T>(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                T serializedData;
                using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    serializedData = (T)serializer.Deserialize(stream);
                }
                return serializedData;
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}
