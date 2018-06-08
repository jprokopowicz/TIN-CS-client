using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace TIN
{
    /// <summary>
    /// Ecripts and decripts fuffers using the loaded key
    /// </summary>
    class Encryptor
    {
        private static int keyLength = 16;
        private static int initVectorLength = 16;

        public Tuple<byte[], byte[]> GetKey()
        {
            byte[] key = new byte[keyLength];
            byte[] IV = new byte[initVectorLength];
            const string fileName = "../../Resources/key.bin";
            try
            {
                BinaryReader binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open));
                for (int i = 0; i < keyLength; ++i)
                {
                    key[i] = binaryReader.ReadByte();
                }

                for (int i = 0; i < initVectorLength; ++i)
                {
                    IV[i] = binaryReader.ReadByte();
                }
                binaryReader.Close();
            }
            catch (Exception exc)
            {
                throw new Exception("Getting key exception: " + exc.Message);
            }

            return new Tuple<byte[], byte[]>(key, IV);
        }

        public byte[] Encrypt(byte[] source, byte[] key, byte[] IV)
        {

            byte[] encrypted; ;
            using (MemoryStream mstream = new MemoryStream())
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    aesProvider.Mode = CipherMode.CBC;
                    aesProvider.Padding = PaddingMode.PKCS7;
                    using (CryptoStream cryptoStream = new CryptoStream(mstream, aesProvider.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(source, 0, source.Length);
                    }
                    encrypted = mstream.ToArray();
                }
            }
            return encrypted;
        }

        public byte[] Decrypt(byte[] sorce, byte[] key, byte[] IV)
        {

            byte[] result;
            int count;
            using (MemoryStream mStream = new MemoryStream(sorce))
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    aesProvider.Mode = CipherMode.CBC;
                    aesProvider.Padding = PaddingMode.PKCS7;
                    using (CryptoStream cryptoStream = new CryptoStream(mStream,
                    aesProvider.CreateDecryptor(key, IV), CryptoStreamMode.Read))
                    {
                        result = new byte[sorce.Length];
                        count = cryptoStream.Read(result, 0, result.Length);
                    }
                }
            }

            byte[] returnval = new byte[count];
            Array.Copy(result, returnval, count);
            return returnval;
        }

    }
}
