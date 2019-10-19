using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using FreeImageAPI;
namespace Inventory.Infrastructrue.Images
{
    public class ImageResize
    {
        public static void Compress(string image)
        {
            byte[] bytes = Convert.FromBase64String(image.Split(',')[1]);
            using (Stream stream = new MemoryStream(bytes))
            {
                const int size = 150;
                using (var original = FreeImageBitmap.FromStream(stream))
                {
                    int width, height;
                    if (original.Width > original.Height)
                    {
                        width = size;
                        height = original.Height * size / original.Width;
                    }
                    else
                    {
                        width = original.Width * size / original.Height;
                        height = size;
                    }
                    var resized = new FreeImageBitmap(original, width, height);
                    Stream newStream = new MemoryStream();
                    resized.Save(newStream, FREE_IMAGE_FORMAT.FIF_JPEG);
                    var newBytes = ReadFully(newStream);
                    var contents = new StreamContent(new MemoryStream(newBytes));
                }
            }

        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
