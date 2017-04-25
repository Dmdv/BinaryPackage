using System;
using System.IO;

namespace BinaryPackage
{
    public class BinarySerializer
    {
        public void Write(string path, MessageType message)
        {
            using (var fileStream = File.OpenWrite(path))
            {
                using (var bw = new BinaryWriter(fileStream))
                {
                    bw.Write(message.Id.ToByteArray());
                    bw.Write(BitConverter.GetBytes(message.DateTime.ToBinary()));
                    bw.Write(Convert.ToByte(message.IsInput));
                    bw.Write(message.Data.ToByteArray());

                    bw.Flush();
                }
            }
        }

        public MessageType Read(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                using (var bw = new BinaryReader(fileStream))
                {
                    return new MessageType
                    {
                        Id = new Guid(bw.ReadBytes(16)),
                        DateTime = DateTime.FromBinary(bw.ReadInt64()),
                        IsInput = bw.ReadByte(),
                        Data = SampleMessage.FromByteArray(bw.ReadAllBytes())
                    };
                }
            }
        }
    }
}