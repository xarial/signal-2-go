/*********************************************************************
Signal2Go
Copyright(C) 2018 www.xarial.net
Product URL: https://www.xarial.net/products/developers/signal-2-go
License: https://github.com/xarial/signal-2-go/blob/master/LICENSE
*********************************************************************/

using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Xarial.Signal2Go.Helpers
{
    public static class JsonFileSerializer
    {
        public static void SerializeToFile<T>(T obj, string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(filePath, JsonConvert.SerializeObject(obj));
        }
    }
}
