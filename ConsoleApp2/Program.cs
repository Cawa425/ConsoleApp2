using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Encoders;

namespace ConsoleApp2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var response = Get("http://188.93.211.195/chain");
            var a = JArray.Parse(response).Last();
            var neural = new Neural();
            var publicKey = Get("http://188.93.211.195/public");

            var previousData = a["prevhash"] + a["data"]?.ToString() + a["ts"];

            var prevhash = Encoding.GetEncoding("ISO-8859-1").GetString(Hex.Encode(Encoding.ASCII.GetBytes(previousData)));
            var signature = HashManager.SignData(previousData);

            var data = neural.Start(a["data"]);
            var info = "Kazakov Alexandr 11-809";
            var ts = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ff") + "+03";


            var bm = new BlockchainModel
            {
                prevhash = prevhash,
                data = data,
                signature = signature,
                info = info,
                ts = ts,
                arbitersignature = ""
            };

            var output = JsonConvert.SerializeObject(bm);
            var path = $"http://188.93.211.195/newblock?block={output}";
            var response2 = Get(path);
        }

        public static string Get(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}