using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace DataLibrary.DataAccess
{
    public static class JsonLoader
    {
        public static T JsonDeserializer<T>(HttpRequestBase httpRequest)
        {
            httpRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(httpRequest.InputStream).ReadToEnd();

            return (T)Convert.ChangeType(
                JsonConvert.DeserializeObject<T>(json), typeof(T));
        }
    }
}
