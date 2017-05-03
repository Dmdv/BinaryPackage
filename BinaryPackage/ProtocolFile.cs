using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BinaryPackage.Protocol;

namespace BinaryPackage
{
    public class ProtocolFile<TMessage> where TMessage : IProtocol
    {
        private readonly string _path;

        public ProtocolFile(string path, IProtocolSerializer<TMessage> serializer)
        {
            _path = path;
            Serializer = serializer;
        }
        
        public IProtocolSerializer<TMessage> Serializer { get; private set; }

        public void Write(params TMessage[] messages)
        {
            using (var fileStream = File.OpenWrite(_path))
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

        public IEnumerable<TMessage> FindAll(Predicate<TMessage> predicate)
        {
            return ReadAll().Where(message => predicate(message));
        }

        public IEnumerable<TMessage> ReadAll()
        {
            if (!File.Exists(_path))
            {
                yield break;
            }

            using (var fileStream = File.OpenRead(_path))
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

        public void Clear()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }
    }
}