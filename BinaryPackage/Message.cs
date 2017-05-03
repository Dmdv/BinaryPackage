using System;
using System.Text;

namespace BinaryPackage
{
    /// <summary>
    /// Serialization inside the type doesn't seem the right idea
    /// This is just for the sake of example
    /// </summary>
    public class Message
    {
        public string Text { get; set; }

        public int Value { get; set; }

        public byte[] ToByteArray()
        {
            var valueArray = BitConverter.GetBytes(Value);
            var stringArray = Encoding.UTF8.GetBytes(Text);

            var array = new byte[valueArray.Length + stringArray.Length];

            Buffer.BlockCopy(valueArray, 0, array, 0, valueArray.Length);
            Buffer.BlockCopy(stringArray, 0, array, valueArray.Length, stringArray.Length);

            return array;
        }

        public void FromByteArray(byte[] buffer)
        {
            Value = BitConverter.ToInt32(buffer, 0);
            Text = Encoding.UTF8.GetString(buffer, sizeof(int), buffer.Length - sizeof(int));
        }
    }
}