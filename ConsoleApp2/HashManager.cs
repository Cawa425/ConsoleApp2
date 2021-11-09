using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp2
{
    public class HashManager
    {
        public static string HashString(string text)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
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