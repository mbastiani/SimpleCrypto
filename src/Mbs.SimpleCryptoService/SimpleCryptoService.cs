using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Mbs.SimpleCrypto
{
    public class SimpleCryptoService : ISimpleCryptoService
    {
        private const int IV_LENGTH = 16;
        private const int KEY_SIZE = 256;

        public string AES_Decrypt(string contentToBeDecrypted, string privateKey)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(contentToBeDecrypted);

            (byte[] encryptedBytes, byte[] IV) = DissociateEncryptedBytesAndIV(bytesToBeDecrypted);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KEY_SIZE;
                aes.Key = Encoding.UTF8.GetBytes(privateKey);
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private (byte[] encryptedBytes, byte[] IV) DissociateEncryptedBytesAndIV(byte[] bytesToBeDecrypted)
        {
            var totalLength = bytesToBeDecrypted.Length;

            var encryptedBytes = bytesToBeDecrypted.Take(totalLength - IV_LENGTH).ToArray();
            var IV = bytesToBeDecrypted.Skip(encryptedBytes.Length).Take(IV_LENGTH).Reverse().ToArray();

            return (encryptedBytes, IV);
        }

        public string AES_Encrypt(string contentToBeEncrypted, string privateKey)
        {
            byte[] encryptedBytes;
            byte[] encryptedBytesAndIV;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KEY_SIZE;
                aes.Key = Encoding.UTF8.GetBytes(privateKey);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(contentToBeEncrypted);
                        }

                        encryptedBytes = memoryStream.ToArray();
                    }
                }
                encryptedBytesAndIV = ConcatenateEncryptedBytesAndIV(encryptedBytes, aes.IV);
            }

            return Convert.ToBase64String(encryptedBytesAndIV);
        }

        private byte[] ConcatenateEncryptedBytesAndIV(byte[] encryptedBytes, byte[] IV)
        {
            var reversedIV = IV.Reverse();

            return encryptedBytes.Concat(reversedIV).ToArray();
        }
    }
}