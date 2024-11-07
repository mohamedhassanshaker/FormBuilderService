using System.Text;

namespace SharedKernel.Utilities.Hashing
{
    public class Hash
    {
        public static string HashSHA256(string value)
        {
            var sha256 = System.Security.Cryptography.SHA256.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha256.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}