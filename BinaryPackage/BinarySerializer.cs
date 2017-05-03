using System.Collections.Generic;
using System.IO;
using BinaryPackage.Protocol;

namespace BinaryPackage
{
    public class BinarySerializer
    {
        public void Write(string path, IEnumerable<InformationProtocol> messages)
        {
            using (var fileStream = File.OpenWrite(path))
            {
                fileStream.Seek(fileStream.Length, SeekOrigin.Begin);

                using (var bw = new BinaryWriter(fileStream))
                {
                    foreach (var message in messages)
                    {
                        bw.WritePacket(message);
                    }

                    bw.Flush();
                }
            }
        }

        public IEnumerable<InformationProtocol> Read(string path)
        {
            if (!File.Exists(path))
            {
                yield break;
            }

            using (var fileStream = File.OpenRead(path))
            {
                using (var bw = new BinaryReader(fileStream))
                {
                    InformationProtocol informationProtocol;

                    while ((informationProtocol = bw.ReadPacket()) != null)
                    {
                        yield return informationProtocol;
                    }
                }
            }
        }
    }
}