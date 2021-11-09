using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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

            var dtmp = a["prevhash"] + a["data"]?.ToString()  + a["ts"] ;
            var prevhash = HashManager.GetHash(dtmp,publicKey);
            
            var data = neural.Start(a["data"]);
            var info = "Kazakov Alexandr 11-809";
            var ts = DateTime.Now;
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