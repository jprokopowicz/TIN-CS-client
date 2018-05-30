using System;
using System.Drawing;
using System.IO;

namespace Cs_client
{
    class Converter
    {
        public static int maxImageSize = 786486; 

        Converter() { }

        public byte[] ConvertToBuffer(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                if (ms.Length > maxImageSize)
                    throw new Exception("Too big image");
                return ms.ToArray();
            }
        }

        public Image ConvertToImage(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                if (ms.Length > maxImageSize)
                    throw new Exception("Too big image");
                return Image.FromStream(ms);
            }
        } 
    }
}
