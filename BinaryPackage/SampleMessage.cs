using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryPackage
{
    [Serializable]
    public class SampleMessage : IRawData
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public byte[] ToByteArray()
        {
            var binaryFormatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, this);
                return stream.ToArray();
            }
        }

        public static SampleMessage FromByteArray(byte[] buffer)
        {
            using (var stream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                var obj = binaryFormatter.Deserialize(stream);
                return obj as SampleMessage;
            }
        }
    }
}