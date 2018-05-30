using System;
using System.Drawing;
using System.IO;

namespace TIN
{
    class DataConverter
    {
        public static int maxSize = 786486; 

        public DataConverter() { }

        public byte[] GenerateBuffer()
        {
            return new byte[maxSize];
        }

        public byte[] ConvertToBuffer(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                if (ms.Length > maxSize)
                    throw new Exception("Too big image");
                return ms.ToArray();
            }
        }

        public Image ConvertToImage(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                if (ms.Length > maxSize)
                    throw new Exception("Too big image");
                return Image.FromStream(ms);
            }
        }

        public byte[] CopyBuffer(byte[] sorce)
        {
            byte[] result = new byte[maxSize];
            for (int i = 0; i < maxSize; ++i)
            {
                result[i] = sorce[i];
            }
            return result;
        }
    }
}
