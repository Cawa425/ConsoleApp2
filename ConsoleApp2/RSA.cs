using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp2
{
    public class RSAPKCS1SHA256SignatureDescription :SignatureDescription
    {
        public RSAPKCS1SHA256SignatureDescription()
        {
            base.KeyAlgorithm = "System.Security.Cryptography.RSACryptoServiceProvider";
            base.DigestAlgorithm = "System.Security.Cryptography.SHA256Managed";
            base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
            base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
        }

        public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
        {
            AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)
                CryptoConfig.CreateFromName(base.DeformatterAlgorithm);
            asymmetricSignatureDeformatter.SetKey(key);
            asymmetricSignatureDeformatter.SetHashAlgorithm("SHA256");
            return asymmetricSignatureDeformatter;
        }
    }
}