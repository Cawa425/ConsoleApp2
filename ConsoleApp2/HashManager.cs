using System;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2
{
    public static class HashManager
    {
        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
        
        // public static string HashString(string text)
        // {
        //     var crypt = new SHA256Managed();
        //     var hash = String.Empty;
        //     var crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(text));
        //     foreach (byte theByte in crypto)
        //     {
        //         hash += theByte.ToString("x2");
        //     }
        //     return hash;
        // }
        
        public static string SignData(string originalMessage)
        {
            string success = "";
            var encoder = new UTF8Encoding();
            byte[] bytesToSign = encoder.GetBytes(originalMessage);


            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    SHA256Managed Hash = new SHA256Managed();

                    byte[] hashedData = Hash.ComputeHash(bytesToSign);
                    success = Convert.ToBase64String(rsa.SignData(hashedData, CryptoConfig.MapNameToOID("SHA256")));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return success;
        }
    }
}