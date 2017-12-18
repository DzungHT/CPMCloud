using System;
using System.Security.Cryptography;
using System.Text;

namespace CybertronFramework.Libraries
{
    public static class EncryptionExtension
    {
        /// <summary>
        /// Mã hóa một string thành một string MD5
        /// </summary>
        /// <param name="s">Chuỗi truyền vào</param>
        /// <returns>Chuỗi đã được mã hóa MD5</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static string ToMD5(this string s)
        {
            try
            {
                var bytes = Encoding.Unicode.GetBytes(s);
                var bytesMD5 = MD5.Create().ComputeHash(bytes);
                var md5Str = Convert.ToBase64String(bytesMD5);
                return md5Str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mã hóa một string theo thuật toán TripleDES
        /// </summary>
        /// <param name="strEnCrypt">string được mã hóa</param>
        /// <param name="key">Khóa để mã hóa</param>
        /// <returns>một string được mã hóa theo thuật toán TripleDES</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="CryptographicException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Encrypt(this string strEnCrypt, string key)
        {
            try
            {
                byte[] keyArr;
                byte[] EnCryptArr = UTF8Encoding.UTF8.GetBytes(strEnCrypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                return Convert.ToBase64String(arrResult, 0, arrResult.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Giải mã một string theo thuật toán TripleDES
        /// </summary>
        /// <param name="strDecypt">String được giải mã</param>
        /// <param name="key">Khóa giải mã</param>
        /// <returns>string đã được giải mã</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="CryptographicException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DecoderFallbackException"></exception>
        public static string Decrypt(this string strDecypt, string key)
        {
            //Khóa giải mã và mã khóa phải giống nhau
            try
            {
                byte[] keyArr;
                byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateDecryptor();
                byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                return UTF8Encoding.UTF8.GetString(arrResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}