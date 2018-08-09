/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using System.IO;
using System.Runtime.Serialization.Json;

namespace Xarial.AppLaunchKit.Helpers
{
    public static class JsonSerializer
    {
        public static T Deserialize<T>(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            var ser = new DataContractJsonSerializer(typeof(T));
            return (T)ser.ReadObject(stream);
        }

        public static void Serialize<T>(T obj, Stream stream)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(stream, obj);
        }

        public static string SerializeToText<T>(T obj)
        {
            using (MemoryStream memStm = new MemoryStream())
            {
                Serialize(obj, memStm);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
