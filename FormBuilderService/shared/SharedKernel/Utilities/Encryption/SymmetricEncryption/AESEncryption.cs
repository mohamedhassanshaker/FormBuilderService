using System.Security.Cryptography;

namespace SharedKernel.Utilities.Encryption.SymmetricEncryption
{
    public class AESEncryption
    {
        private static byte[] Key = default!;
        private static byte[] IV = default!;

        public static void SetKeyAndIV(string key, string iv)
        {
            Key = StringToByteArray(key);
            IV = StringToByteArray(iv);
        }

        public static string Encrypt(string plainText)
        {
            byte[] encryptedBytes;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        encryptedBytes = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string plainText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }

        public static byte[] StringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static string GenerateAESKey()
        {
            try
            {
                using (Aes aesProvider = Aes.Create())
                {
                    aesProvider.KeySize = 256; // Key length in bits (AES-256)

                    // Generate a random AES key
                    aesProvider.GenerateKey();

                    // Get the generated key
                    byte[] key = aesProvider.Key;

                    return Convert.ToHexString(key);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error generating AES key: " + ex.Message);
                throw;
            }
        }
    }
}