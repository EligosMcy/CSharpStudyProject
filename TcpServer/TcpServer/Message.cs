using System;
using System.Text;

namespace TcpServer
{
    public class Message
    {
        private byte[] data = new byte[1024];

        private static int wholeIndex = 0; //我们存取了多少个字节的数据在数组里面

        public byte[] Data
        {
            get => data;
        }

        public int WholeIndex => wholeIndex;

        public int RemainSize
        {
            get => (data.Length - wholeIndex);
        }

        public void AddCount(int count)
        {
            //修改 当前读取到的字节的数据内容 总长度
            wholeIndex += count;
        }

        public void ReadMessage()
        {
            while (true)
            {
                if (wholeIndex <= 4) return;

                int count = BitConverter.ToInt32(data, 0);

                if (wholeIndex - 4 >= count)
                {
                    //
                    string str = Encoding.UTF8.GetString(data, 4, count);
                    Console.WriteLine("Read a message : " + str);

                    //
                    wholeIndex -= (count + 4);
                    Array.Copy(data, count + 4, data, 0, wholeIndex);
                    
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