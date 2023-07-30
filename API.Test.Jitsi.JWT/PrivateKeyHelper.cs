using System.Security.Cryptography;

namespace API.Test.Jitsi.JWT
{
    public class PrivateKeyHelper
    {

        public enum PKType
        {
            // PKCS#1 type key
            PKCS1,
            // PKCS#8 type key
            PKCS8
        }

        public const String BEGIN_PKCS1_PRIVATE_KEY = "-----BEGIN RSA PRIVATE KEY-----";
        public const String END_PKCS1_PRIVATE_KEY = "-----END RSA PRIVATE KEY-----";

        public const String BEGIN_PKCS8_PRIVATE_KEY = "-----BEGIN PRIVATE KEY-----";
        public const String END_PKCS8_PRIVATE_KEY = "-----END PRIVATE KEY-----";


        /*/// <summary>
        /// Generates a new RSA key pair and saves the private key to the specified file path.
        /// </summary>
        /// <param name="privateKeyFilePath">The file path to save the private key.</param>
        /// <param name="pkType">The type of key to generate: PKCS#1 or PKCS#8</param>
        /// <returns>The generated RSA private key.</returns>
        public static RSA GenerateAndSavePrivateKey(string privateKeyFilePath, PKType pkType)
        {
            var rsa = RSA.Create();

            if (pkType == PKType.PKCS1)
            {
                var privateKey = rsa.ExportRSAPrivateKey();
                var privateKeyText = $"{BEGIN_PKCS1_PRIVATE_KEY}{Convert.ToBase64String(privateKey)}{END_PKCS1_PRIVATE_KEY}";
                File.WriteAllText(privateKeyFilePath, privateKeyText);
            }
            else
            {
                var privateKey = rsa.ExportPkcs8PrivateKey();
                var privateKeyText = $"{BEGIN_PKCS8_PRIVATE_KEY}{Convert.ToBase64String(privateKey)}{END_PKCS8_PRIVATE_KEY}";
                File.WriteAllText(privateKeyFilePath, privateKeyText);
            }

            return rsa;
        }*/

        /// <summary>
        /// Reads a RSA private key PKCS#1 or PKCS#8 format from file.
        /// Use openssl rsa -in inputfile -out outputfile to convert to PKCS#1 if you need to.
        /// </summary>
        /// <param name="privateKeyFilePath"></param>
        /// <param name="pkType">The type of key from the file: PKCS#1 or PKCS#8</param>
        /// <returns>The private key object</returns>
        public static RSA ReadPrivateKeyFromFile(String privateKeyFilePath, PKType pkType)
        {
            var rsa = RSA.Create();
            var privateKeyContent = File.ReadAllText(privateKeyFilePath, System.Text.Encoding.UTF8);
            privateKeyContent = privateKeyContent.Replace(pkType == PKType.PKCS1 ? PrivateKeyHelper.BEGIN_PKCS1_PRIVATE_KEY : PrivateKeyHelper.BEGIN_PKCS8_PRIVATE_KEY, "");
            privateKeyContent = privateKeyContent.Replace(pkType == PKType.PKCS1 ? PrivateKeyHelper.END_PKCS1_PRIVATE_KEY : PrivateKeyHelper.END_PKCS8_PRIVATE_KEY, "");
            var privateKeyDecoded = Convert.FromBase64String(privateKeyContent);
            if (pkType == PKType.PKCS1)
            {
                rsa.ImportRSAPrivateKey(privateKeyDecoded, out _);
            }
            else
            {
                rsa.ImportPkcs8PrivateKey(privateKeyDecoded, out _);
            }

            return rsa;
        }
    }
}
