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
            var msg1 = new Protocol
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

            var msg2 = new Protocol
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

            const string Path = "sample.dat";

            var protocolFile = new ProtocolFile<Protocol>(Path, new ProtocolSerializer());
            
            var list1 = protocolFile.ReadAll().ToList();

            var protocol = protocolFile.ReadAll().First();

            var findAll = protocolFile.FindAll(x => x.Body.Text == "Sample Text 2").ToList();

            protocolFile.Write(msg1, msg2);

            var list2 = protocolFile.ReadAll().ToList();

            //messageType.ShouldBeEquivalentTo(msg1, options => options.Excluding(x => x.PacketLength));
        }
    }
}