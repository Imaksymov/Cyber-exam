using System;
using System.Text;
using System.Security.Cryptography;

namespace Exam
{
    class Program
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cypherBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);
                cypherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cypherBytes;
        }
        static void Main(string[] args)
        {

            var rsaPairs = new Program();
            const string original = "Some text";
            rsaPairs.AssignNewKey();
            var en = rsaPairs.EncryptData(Encoding.UTF8.GetBytes(original));
            Console.WriteLine("Text: " + original);
            Console.WriteLine("Encrypted: " + Convert.ToBase64String(en));
            Console.ReadKey();
        }
    }
}
