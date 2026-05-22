using System;
using System.Linq;
using System.Text;

namespace TcpClient
{
    public class Message
    {
        public static byte[] GetBytes(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            int dataLength = dataBytes.Length;
            byte[] lengthBytes = BitConverter.GetBytes(dataLength);

            byte[] newBytes = lengthBytes.Concat(dataBytes).ToArray();

            return newBytes;
        } 
    }
}