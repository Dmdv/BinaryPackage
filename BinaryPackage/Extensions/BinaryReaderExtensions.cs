using System.IO;

namespace BinaryPackage.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static byte[] ReadAllBytes(this BinaryReader reader)
        {
            const int BufferSize = 4096;

            using (var ms = new MemoryStream())
            {
                var buffer = new byte[BufferSize];
                int count;
                while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                {
                    ms.Write(buffer, 0, count);
                }
                return ms.ToArray();
            }
        }
    }
}