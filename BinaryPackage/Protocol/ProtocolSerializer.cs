using System;
using System.IO;

namespace BinaryPackage.Protocol
{
    public class ProtocolSerializer : IProtocolSerializer<Protocol>
    {
        public void WritePacket(BinaryWriter bw, Protocol message)
        {
            bw.Write(message.Id.ToByteArray());
            bw.Write(BitConverter.GetBytes(message.DateTime.ToBinary()));
            bw.Write(Convert.ToByte(message.InformationType));
            bw.Write(Convert.ToByte(message.ProtocolType));
            var body = message.Body.ToByteArray();
            bw.Write(BitConverter.GetBytes(body.Length));
            bw.Write(body);
        }

        public Protocol ReadPacket(BinaryReader bw)
        {
            if (!bw.BaseStream.CanRead || bw.BaseStream.Position == bw.BaseStream.Length)
            {
                return null;
            }

            const int GuidLength = 16;

            var protocol = new Protocol
            {
                Id = new Guid(bw.ReadBytes(GuidLength)),
                DateTime = DateTime.FromBinary(bw.ReadInt64()),
                InformationType = (TypeInformation)bw.ReadByte(),
                ProtocolType = (ProtocolType)bw.ReadByte(),
                PacketLength = bw.ReadInt32()
            };

            protocol.Body.FromByteArray(bw.ReadBytes(protocol.PacketLength));

            return protocol;
        }
    }
}