using System.Collections.Generic;
using System.IO;
using BinaryPackage.Protocol;

namespace BinaryPackage
{
    public class ProtocolFile<TMessage> where TMessage : IProtocol
    {
        public ProtocolFile(IProtocolSerializer<TMessage> serializer)
        {
            Serializer = serializer;
        }
        
        public IProtocolSerializer<TMessage> Serializer { get; private set; }

        public void Write(string path, IEnumerable<TMessage> messages)
        {
            using (var fileStream = File.OpenWrite(path))
            {
                fileStream.Seek(fileStream.Length, SeekOrigin.Begin);

                using (var bw = new BinaryWriter(fileStream))
                {
                    foreach (var message in messages)
                    {
                        Serializer.WritePacket(bw, message);
                    }

                    bw.Flush();
                }
            }
        }

        public IEnumerable<TMessage> Read(string path)
        {
            if (!File.Exists(path))
            {
                yield break;
            }

            using (var fileStream = File.OpenRead(path))
            {
                using (var bw = new BinaryReader(fileStream))
                {
                    TMessage protocol;

                    while ((protocol = Serializer.ReadPacket(bw)) != null)
                    {
                        yield return protocol;
                    }
                }
            }
        }
    }
}