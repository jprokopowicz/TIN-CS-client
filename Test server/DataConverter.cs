using System;
using System.Drawing;
using System.IO;

namespace TINtest
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
            /*if (ms.Length > maxSize)
                throw new Exception("Too big image");*/
            byte[] result;
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                result = ms.ToArray();
            }
            return result;
        }

        public static Image ConvertToImage(byte[] buffer)
        {
            /*if (ms.Length > maxSize)
                    throw new Exception("Too big image");*/
            Image result;
            using (var ms = new MemoryStream(buffer))
            {

                result = Image.FromStream(ms);
            }

            return result;
        }

        public static byte[] CopyBuffer(byte[] sorce, int size)
        {
            byte[] result = new byte[size];
            for (int i = 0; i < size; ++i)
            {
                result[i] = sorce[i];
            }
            return result;
        }
    }
}
