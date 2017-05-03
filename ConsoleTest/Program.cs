using System;
using System.Linq;
using BinaryPackage;
using BinaryPackage.Protocol;
using FluentAssertions;

namespace ConsoleTest
{
    internal class Program
    {
        private static void Main()
        {
            var msg1 = new InformationProtocol
            {
                DateTime = DateTime.Now,
                Body = new Message
                {
                    Text = "Sample Text",
                    Value = 100
                },
                Id = Guid.NewGuid(),
                InformationType = TypeInformation.Incoming,
                ProtocolType = ProtocolType.Baikal
            };

            var msg2 = new InformationProtocol
            {
                DateTime = DateTime.Now.AddDays(1),
                Body = new Message
                {
                    Text = "Sample Text 2",
                    Value = 200
                },
                Id = Guid.NewGuid(),
                InformationType = TypeInformation.Outcoming,
                ProtocolType = ProtocolType.Lignis
            };

            var ser = new BinarySerializer();

            const string Path = "sample.dat";

            var list1 = ser.Read(Path).ToList();

            ser.Write(Path, new[] {msg1, msg2});

            var list2 = ser.Read(Path).ToList();

            //messageType.ShouldBeEquivalentTo(msg1, options => options.Excluding(x => x.PacketLength));
        }
    }
}