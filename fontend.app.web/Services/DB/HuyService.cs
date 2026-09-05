using Microsoft.Extensions.Configuration;
using ServiceStack.OrmLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Data.SqlClient;
using System.Data;

namespace core.Services
{
    public class HuyService
    {
        public OrmLiteConnectionFactory _connectionData;
        protected static string ConnectionString = string.Empty;
        protected static double DefaultDurationMinutes = 5;
        protected static TimeSpan DefaultDurationcache = TimeSpan.FromMinutes(10);
     
        public HuyService()
        {
            var AppSetting = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
            var isOnline = AppSetting["MySettingsModel:Online"];
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = AppSetting["MySettingsModel:ConnectionString"];
                string mode_encrypt = AppSetting["MySettingsModel:ModeCrypt"];
                if (mode_encrypt == "1")
                {
                    ConnectionString = UtilCrypt.decrypt(ConnectionString);
                }//co ma hoa
            }
         
            DefaultDurationMinutes = double.Parse(AppSetting["MySettingsModel:CacheSettings:DefaultDurationMinutes"]);
            DefaultDurationcache = TimeSpan.FromMinutes(DefaultDurationMinutes);
            OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
            _connectionData = new OrmLiteConnectionFactory(ConnectionString, OrmLiteConfig.DialectProvider);
            OrmLiteConfig.DialectProvider.GetStringConverter().UseUnicode = true;
        }
    }
    internal class UtilCrypt
    {
        public static string key1 = "Abc123456789!";
        public static string key1_salt = "C11Da172@$!!@$nt";

        public static string encrypt(string original)
        {
            //string original = "The data to encrypt.";
            byte[] encrypted;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                // Setting a key size disposes the previously-set key. 
                // Setting a key size is redundant if a key going to be set after this statement. 
                // According to https://en.wikipedia.org/wiki/Advanced_Encryption_Standard, Supported key sizes are 128, 192 and 256
                aes.KeySize = 256;

                // aes.BlockSize = 128; // According to https://en.wikipedia.org/wiki/Advanced_Encryption_Standard: Block size for AES is always 128


                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key1, Encoding.UTF8.GetBytes(key1_salt));
                aes.Key = deriveBytes.GetBytes(128 / 8);
                byte[] key = aes.Key;


                encrypted = EncryptStringToBytes(original, key);
                return Convert.ToBase64String(encrypted);
            }
        }
        public static string decrypt(string encrypt)
        {
            //string original = "The data to encrypt.";
            byte[] encrypted = Convert.FromBase64String(encrypt);
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                // Setting a key size disposes the previously-set key. 
                // Setting a key size is redundant if a key going to be set after this statement. 
                // According to https://en.wikipedia.org/wiki/Advanced_Encryption_Standard, Supported key sizes are 128, 192 and 256
                aes.KeySize = 256;

                // aes.BlockSize = 128; // According to https://en.wikipedia.org/wiki/Advanced_Encryption_Standard: Block size for AES is always 128


                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key1, Encoding.UTF8.GetBytes(key1_salt));
                aes.Key = deriveBytes.GetBytes(128 / 8);
                byte[] key = aes.Key;


                string decrypted = DecryptStringFromBytes(encrypted, key);

                return decrypted;
            }
        }
        static byte[] EncryptStringToBytes(string str, byte[] keys)
        {
            byte[] encrypted;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = keys;

                aes.GenerateIV(); // The get method of the 'IV' property of the 'SymmetricAlgorithm' automatically generates an IV if it is has not been generate before. 

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aes.IV, 0, aes.IV.Length);
                    ICryptoTransform encoder = aes.CreateEncryptor();
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encoder, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(str);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] key)
        {
            string decrypted;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                // Setting a key size disposes the previously-set key. 
                // Setting a key size will generate a new key. 
                // Setting a key size is redundant if a key going to be set after this statement. 
                // aes.KeySize = 256; 

                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msDecryptor = new MemoryStream(cipherText))
                {
                    byte[] readIV = new byte[16];
                    msDecryptor.Read(readIV, 0, 16);
                    aes.IV = readIV;
                    ICryptoTransform decoder = aes.CreateDecryptor();
                    using (CryptoStream csDecryptor = new CryptoStream(msDecryptor, decoder, CryptoStreamMode.Read))
                    using (StreamReader srReader = new StreamReader(csDecryptor))
                    {
                        decrypted = srReader.ReadToEnd();
                    }
                }
            }
            return decrypted;
        }
    }

}