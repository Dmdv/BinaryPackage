using System;

namespace BinaryPackage
{
    public class MessageType
    {
        public IRawData Data { get; set; }
        public DateTime DateTime { get; set; }
        public Guid Id { get; set; }
        public byte IsInput { get; set; }
    }
}