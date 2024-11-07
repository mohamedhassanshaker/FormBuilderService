using SharedKernel.Utilities.Logging.Services;
using System.Security.Cryptography;
using System.Text;

namespace SharedKernel.Utilities.Encryption.AsymmetricEncryption
{
    public class RSAEncryption
    {
        public static (string publicKey, string privateKey) GenerateRSAKeys()
        {
            (string publicKey, string privateKey) keys = (string.Empty, string.Empty);

            try
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    keys.publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                    keys.privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                }
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());
            }

            return keys;
        }

        public static string Encrypt(string publicKeyString, string plaintext)
        {
            try
            {
                using (var rsa = RSA.Create())
                {
                    rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKeyString), out _);

                    byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), RSAEncryptionPadding.Pkcs1);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());

                return string.Empty;
            }
        }

        public static string Decrypt(string privateKeyString, string ciphertext)
        {
            try
            {
                using (var rsa = RSA.Create())
                {
                    rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyString), out _);

                    byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(ciphertext), RSAEncryptionPadding.Pkcs1);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());

                return string.Empty;
            }
        }

        public static string ConvertKeyFormate(string key, string formate)
        {
            string result = string.Empty;

            try
            {
                using (var rsa = RSA.Create())
                {
                    byte[] keyBytes = Convert.FromBase64String(key);

                    rsa.ImportRSAPublicKey(keyBytes, out _);

                    switch (formate)
                    {
                        case "base64":
                            return Convert.ToBase64String(rsa.ExportRSAPublicKey());

                        case "xml":
                            string publicKeyXml = rsa.ToXmlString(false);

                            return result = publicKeyXml;

                        default:
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                LogService.LogError(ex.ToString());

                return string.Empty;
            }

            return result;
        }
    }
}