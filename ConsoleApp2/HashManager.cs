using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2
{
    public class HashManager
    {
        

        public static byte[] GetHash(string str, string publicKey)
        {
            RSACryptoServiceProvider signer = new RSACryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(str);
            var signature = signer.SignData( data, new SHA256CryptoServiceProvider());
            RSACryptoServiceProvider verifier = new RSACryptoServiceProvider();

            return signature;
        }
    }
}