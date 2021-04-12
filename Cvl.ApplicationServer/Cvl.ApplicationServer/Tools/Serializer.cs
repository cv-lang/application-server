using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Cvl.ApplicationServer.Tools
{
    /// <summary>
    /// Klasa serializująca i deserializująca obiekty
    /// </summary>
    public class Serializer
    {
        #region Serialisacha SharpSerializer

        public static string SerializeObject(object obj)
        {
            if (obj == null)
            {
                return "<null/>";
            }

            //string preserveReferenacesAll = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            //{
            //    PreserveReferencesHandling = PreserveReferencesHandling.All
            //});

            //return preserveReferenacesAll;

            //return JsonConvert.SerializeObject(obj);

            var settings = new SharpSerializerXmlSettings();
            settings.IncludeAssemblyVersionInTypeName = true;
            settings.IncludeCultureInTypeName = true;
            settings.IncludePublicKeyTokenInTypeName = false;
            

            var serializer = new SharpSerializer(settings);
            serializer.PropertyProvider.AttributesToIgnore.Clear();
            // remove default ExcludeFromSerializationAttribute for performance gain
            //serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));

            using (var ms = new MemoryStream())
            {
                serializer.Serialize(obj, ms);
                ms.Position = 0;
                byte[] bajty = ms.ToArray();
                return Encoding.UTF8.GetString(bajty, 0, bajty.Length);
            }
        }


        public static T DeserializeObject<T>(string xmlOfAnObject)
        {
            if (xmlOfAnObject.StartsWith("?"))
            {
                xmlOfAnObject = xmlOfAnObject.Remove(0, 1);
            }

            if (xmlOfAnObject.Equals("<null/>"))
            {
                return default(T);
            }

            //return JsonConvert.DeserializeObject<T>(xmlOfAnObject);

            return (T)DeserializeObject(xmlOfAnObject);
        }

        public static object DeserializeObject(string xmlOfAnObject)
        {
            if (xmlOfAnObject.StartsWith("?"))
            {
                xmlOfAnObject = xmlOfAnObject.Remove(0, 1);
            }

            if (xmlOfAnObject.Equals("<null/>"))
            {
                return null;
            }

            //return JsonConvert.DeserializeObject(xmlOfAnObject);


            var settings = new SharpSerializerXmlSettings();
            settings.IncludeAssemblyVersionInTypeName = true;
            settings.IncludeCultureInTypeName = true;
            settings.IncludePublicKeyTokenInTypeName = false;

            var serializer = new SharpSerializer(settings);
            serializer.PropertyProvider.AttributesToIgnore.Clear();
            // remove default ExcludeFromSerializationAttribute for performance gain
            //serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));
            byte[] bajty = Encoding.UTF8.GetBytes(xmlOfAnObject);
            using (var ms = new MemoryStream(bajty))
            {
                object obiekt = serializer.Deserialize(ms);

                return obiekt;
            }
        }
        #endregion


        #region Binnary serialize


        public static byte[] SerializeObjectBinaryGzip(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var serializer = new SharpSerializer();
            serializer.PropertyProvider.AttributesToIgnore.Clear();
            // remove default ExcludeFromSerializationAttribute for performance gain
            //serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));

            using (var gzipMemoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(gzipMemoryStream, CompressionMode.Compress))
                {
                    serializer.Serialize(obj, gZipStream);
                }

                return gzipMemoryStream.ToArray();
            }
        }

        public static object DeserializeObjectBinaryGzip(byte[] serializeData)
        {
            if (serializeData == null)
            {
                return null;
            }

            var serializer = new SharpSerializer();
            serializer.PropertyProvider.AttributesToIgnore.Clear();
            // remove default ExcludeFromSerializationAttribute for performance gain
            //serializer.PropertyProvider.AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));
            using (var ms = new MemoryStream(serializeData))
            {
                using (var gZipStream = new GZipStream(ms, CompressionMode.Decompress))
                {
                    object obiekt = serializer.Deserialize(gZipStream);

                    return obiekt;
                }
            }
        }

        public static T DeserializeObjectBinaryGzip<T>(byte[] binarySErializedObject)
        {
            return (T)DeserializeObjectBinaryGzip(binarySErializedObject);
        }
        #endregion
    }
}
