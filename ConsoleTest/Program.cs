using System;
using BinaryPackage;
using FluentAssertions;

namespace ConsoleTest
{
    internal class Program
    {
        private static void Main()
        {
            var sample = new SampleMessage
            {
                Text = "Sample Text",
                Value = 100
            };

            var message = new MessageType
            {
                DateTime = DateTime.Now,
                Data = sample,
                Id = Guid.NewGuid(),
                IsInput = 1
            };

            var ser = new BinarySerializer();

            const string Path = "sample.dat";

            ser.Write(Path, message);
            var messageType = ser.Read(Path);

            messageType.ShouldBeEquivalentTo(message);
        }
    }
}