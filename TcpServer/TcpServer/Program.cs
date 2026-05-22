using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpServer
{
    class Program
    {
        static byte[] bufferBytes = new byte[1024];
        static Message msg = new Message();

        static void Main(string[] args)
        {
            startServerAsync();

            Console.ReadKey(true);
        }

        //异步方式 进行 BeginAccept  EndAccept  Connect  BeginReceive  EndReceive
        private static void startServerAsync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("192.168.5.105");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 2468);

            serverSocket.Bind(ipEndPoint);

            serverSocket.Listen(10);
            Console.WriteLine("Bind IpEndPoint : " + ipEndPoint);

            Console.WriteLine("Waiting Accept a client");
            serverSocket.BeginAccept(AcceptCallBakc,serverSocket);
            
        }

        private static void AcceptCallBakc(IAsyncResult ar)
        {
            Console.WriteLine("Accept a client");
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);
            //发送一次问候
            string msgStr = "Hello client .....";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msgStr);
            clientSocket.Send(data);
            Console.WriteLine("Send a Message to client : " + msgStr);

            clientSocket.BeginReceive(msg.Data, msg.WholeIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            
            // clientSocket.BeginReceive(bufferBytes, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);

            //Accept a client after BeginAccept
            serverSocket.BeginAccept(AcceptCallBakc, serverSocket);
        }

        private static void ReceiveCallBack(IAsyncResult ar)
        {
            Console.WriteLine("Receive a message");

            //将clientSocket 作为参数传递给 ReceiveCallBack ar.AsyncState
            //他们都是 object类型

            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);
                if (count == 0)//正常空数据是 不会传递过来的 只有 客户端正常关闭了 才会 传递空数据过来
                {
                    clientSocket.Close();


                    return;
                }

                //读取到count个字节后 修改下一次的开始字节需要
                msg.AddCount(count);

                msg.ReadMessage();

                // string msgStr = Encoding.UTF8.GetString(bufferBytes, 0, count);
                // Console.WriteLine("Receive a message from client : " + msgStr);

                //Receive a message after BeginReceive
                clientSocket.BeginReceive(msg.Data, msg.WholeIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error : " + exception);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            finally
            {

            }

            
        }


        //同步的方式 进行 Accept Connect Receive 
        private static void startServerSync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("192.168.5.105");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 2468);

            serverSocket.Bind(ipEndPoint);

            serverSocket.Listen(10);
            Console.WriteLine("Bind IpEndPoint : " + ipEndPoint);

            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("Accept a client");


            string msg = "Hello client .....";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);
            Console.WriteLine("Send a Message to client : " + msg);

            byte[] dataBuffer = new byte[1024];
            int count = clientSocket.Receive(dataBuffer);
            string msgReceive = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count);
            Console.WriteLine("Receive a message from Client : " + msgReceive);

            clientSocket.Close(); //关闭 client 连接
            serverSocket.Close(); //关闭 Server 

            Console.ReadKey(true);
        }
    }
}
