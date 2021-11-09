using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


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

            var prevhash = HashManager.HashString(previousData);
            var signature = HashManager.SignData(previousData);

            var data = neural.Start(a["data"]);
            var info = "Kazakov Alexandr 11-809";
            var ts = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ff") + "+03";

            var test = JsonConvert.SerializeObject(data);
            var bm = new BlockchainModel()
            {
                prevhash = prevhash,
                data =  test,
                signature = signature,
                info=info,
                ts= ts,
                arbitersignature=""
            };
            
            var output = JsonConvert.SerializeObject(bm);
            var path = $"http://188.93.211.195/newblock?block={output}";
            var t = Get(path);

        }

        public static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}