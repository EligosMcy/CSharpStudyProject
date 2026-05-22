using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace GameServer.Server
{
    public class Server
    {
        private IPEndPoint _ipEndPoint;
        private Socket _serverSocket;
        private List<LocalClient> _localClientSocketList = new List<LocalClient>();

        public Server()
        {

        }

        public Server(string ipStr,int port)
        {
            SetIpAndPort(ipStr,port);
        }

        public void SetIpAndPort(string ipStr,int port)
        {
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //
            _serverSocket.Bind(_ipEndPoint);

            //无限连接
            _serverSocket.Listen(0);

            //
            _serverSocket.BeginAccept(AcceptCallBack, _serverSocket);
        }

        public void AcceptCallBack(IAsyncResult ar)
        {
            //
            Socket clientSocket = _serverSocket.EndAccept(ar);

            //
            LocalClient localClient = new LocalClient(clientSocket,this);

            //
            localClient.Start();

            //
            _localClientSocketList.Add(localClient);
        }

        public void RemoveClientSocket(LocalClient localClient)
        {
            lock (_localClientSocketList)
            {
                _localClientSocketList.Remove(localClient);
            }
        }
    }
}