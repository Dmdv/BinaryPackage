using System;
using System.IO;

namespace BinaryPackage.Protocol
{
    public static class InformationProtocolSerializer
    {
        public static void WritePacket(this BinaryWriter bw, InformationProtocol message)
        {
            bw.Write(message.Id.ToByteArray());
            bw.Write(BitConverter.GetBytes(message.DateTime.ToBinary()));
            bw.Write(Convert.ToByte(message.InformationType));
            bw.Write(Convert.ToByte(message.ProtocolType));
            var body = message.Body.ToByteArray();
            bw.Write(BitConverter.GetBytes(body.Length));
            bw.Write(body);
        }

        public static InformationProtocol ReadPacket(this BinaryReader bw)
        {
            if (!bw.BaseStream.CanRead || bw.BaseStream.Position == bw.BaseStream.Length)
            {
                return null;
            }

            var informationProtocol = new InformationProtocol
            {
                Id = new Guid(bw.ReadBytes(16)),
                DateTime = DateTime.FromBinary(bw.ReadInt64()),
                InformationType = (TypeInformation)bw.ReadByte(),
                ProtocolType = (ProtocolType)bw.ReadByte(),
                PacketLength = bw.ReadInt32()
            };

            informationProtocol.Body =
                Message.FromByteArray(bw.ReadBytes(informationProtocol.PacketLength));

            return informationProtocol;
        }
    }
}