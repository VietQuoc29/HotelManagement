using System;
using System.Security.Cryptography;
using System.Text;

namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// EncriptFunctions
    /// </summary>
    public static class EncriptFunctions
    {
        /// <summary>
        /// GenerateMd5String
        /// </summary>
        /// <param name="strChange"></param>
        /// <returns></returns>
        public static string GenerateMd5String(string strChange)
        {
            var md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(Encoding.UTF8.GetBytes(strChange));
            var encodedBytes = md5.Hash;
            string encodedPassword = BitConverter.ToString(encodedBytes);
            return encodedPassword;
        }

        /// <summary>
        /// VerifyMd5String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMd5String(string str, string hash)
        {
            var hashinput = GenerateMd5String(str);
            var comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashinput, hash);
        }

        /// <summary>
        /// GeneratePassword
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GeneratePassword(string password)
        {
            return GenerateMd5String(password);
        }

        /// <summary>
        /// VerifyPassword
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hash)
        {
            return VerifyMd5String(password, hash);
        }
    }
}
