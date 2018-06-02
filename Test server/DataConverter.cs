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
