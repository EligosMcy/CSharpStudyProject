using System;
using System.Text;

namespace GameServer.Server
{
    public class Message
    {
        private byte[] data = new byte[1024];

        private static int _startIndex = 0; // 我们存取了多少个字节的数据在数组里面

        public byte[] Data
        {
            get => data;
        }

        public int StartIndex => _startIndex;

        public int RemainSize
        {
            get => (data.Length - _startIndex);
        }

        public void ReadMessage(int newDataAmount)
        {
            _startIndex += newDataAmount;
            while (true)
            {
                if (_startIndex <= 4) return;

                int count = BitConverter.ToInt32(data, 0);

                if (_startIndex - 4 >= count)
                {
                    //
                    string str = Encoding.UTF8.GetString(data, 4, count);
                    Console.WriteLine("Read a message : " + str);

                    //
                    _startIndex -= (count + 4);
                    Array.Copy(data, count + 4, data, 0, _startIndex);

                }
                else
                {
                    //当前 数据 不 完整
                    break;
                }
            }

        }
    }
}