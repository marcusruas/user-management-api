using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.SharedKernel.Helpers.Services
{
    public class RSAEncoderService
    {
        public RSAEncoderService(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        private string PublicKey;
        private string PrivateKey;

        public string EncriptarDados(string dados)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(PublicKey);

                byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(dados);
                byte[] encryptedData = rsa.Encrypt(bytesToEncrypt, true);

                return Convert.ToBase64String(encryptedData);
            }
        }

        public string DecriptarDados(string dados)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(PrivateKey);

                var toBeDecryptedData = Convert.FromBase64String(dados);

                byte[] decryptedData = rsa.Decrypt(toBeDecryptedData, true);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}
