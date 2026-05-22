using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddress = IPAddress.Parse("192.168.5.105");

            clientsocket.Connect(ipAddress, 2468);

            
            byte[] dataBytes = new byte[1024];
            int count = clientsocket.Receive(dataBytes);//暂停 没接受到就一直暂停
            string msg = Encoding.UTF8.GetString(dataBytes,0,count);
            Console.WriteLine("client Receive a message from server :" + msg);

            // while (true)
            // {
            //     Console.WriteLine("\nyou need Input some to server");
            //     string str = Console.ReadLine();
            //     if (str == "c" || str == "C")
            //     {
            //         clientsocket.Close();
            //         return;
            //     }
            //     clientsocket.Send(Encoding.UTF8.GetBytes(str));
            //     Console.WriteLine("client send str to server:" + str);
            // }


            // 频繁发送 简短 数据 就会出现 粘包问题
            // for (int i = 0; i < 100; i++)
            // {
            //     clientsocket.Send(Encoding.UTF8.GetBytes(i.ToString()));
            // }


            // 单次发送 庞大 数据 就是出现 分包问题
            // string s = "庞大数据";
            // clientsocket.Send(Encoding.UTF8.GetBytes(s));


            // 发送一个 在首位 添加了包长度的 数据 避免粘包问题
            // 使用 Message 进行 文件 处理 
            for (int i = 0; i < 100; i++)
            {
                clientsocket.Send(Message.GetBytes(i.ToString()));
            }
            
             
            Console.ReadKey(true);
        }
    }
}
