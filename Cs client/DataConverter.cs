using System;
using System.Drawing;
using System.IO;

namespace TIN
{
    static class DataConverter
    {
        public static int maxSize = 786486; 

        public static byte[] GenerateBuffer(int size)
        {
            return new byte[size];
        }

        public static byte[] ConvertToBuffer(Image image)
        {
            
            using (var ms = new MemoryStream())
            {
                if (ms.Length > maxSize)
                    throw new Exception("Too big image");
                image.Save(ms, image.RawFormat);    
                return ms.ToArray();
            }
        }

        public static Image ConvertToImage(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                if (ms.Length > maxSize)
                    throw new Exception("Too big image"); 
                return Image.FromStream(ms);
            }
        }

        public static bool CopyBuffer(byte[] from, byte[] to, int startingIndexFrom, int stratingIndexTo, int amount) {

            if (startingIndexFrom + amount - 1 >= from.Length && stratingIndexTo + amount - 1 >= to.Length)
                return false;
            for (int i = 0 ; i < amount ; ++i)
            {
                to[stratingIndexTo + i] = from[startingIndexFrom + i];
            }
            return true;
        }

        public static byte[] CopyAndCutBuffer(byte[] sorce, int size)
        {
            byte[] result = new byte[size];
            for (int i = 0; i < size; ++i)
            {
                result[i] = sorce[i];
            }
            return result;
        }



        public static byte[] ConnectBuffors(byte[][] sorce, int amount)
        {
            int length = 0;
            for(int i = 0; i < amount; ++i)
            {
                length += sorce[i].Length;
            }

            byte[] result = new byte[length];

            int copied = 0;
            for(int i = 0; i < amount; ++i)
            {
                for(int j = 0; j < sorce[i].Length; ++j)
                {
                    result[copied + j] = sorce[i][j];
                }
                copied += sorce[i].Length;
            }

            return result;
        }
    }
}
