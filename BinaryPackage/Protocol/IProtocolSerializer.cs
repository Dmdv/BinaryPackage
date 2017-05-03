using System.IO;

namespace BinaryPackage.Protocol
{
    public interface IProtocolSerializer<TMessage> where TMessage : IBinary
    {
        void WritePacket(BinaryWriter bw, TMessage body);

        TMessage ReadPacket(BinaryReader bw);
    }
}