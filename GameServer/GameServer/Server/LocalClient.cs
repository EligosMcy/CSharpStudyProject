using System;
using System.Dynamic;
using System.Net.Sockets;

namespace GameServer.Server
{
    public class LocalClient
    {

        private Socket _localClientSocket;

        private Server _server;

        private Message _message;
        public LocalClient()
        {
        }
        public LocalClient(Socket clientSocket , Server server)
        {
            _localClientSocket = clientSocket;
            _server = server;
        }

        public void Start()
        {
            _localClientSocket.BeginReceive(null, 0, 0, SocketFlags.None, ReceiveCallBack,null);
        }


        public void ReceiveCallBack(IAsyncResult ar)
        {

            try
            {
                int count = _localClientSocket.EndReceive(ar);

                //
                if (count == 0)
                {
                    Close();
                }

                //
                _message.ReadMessage(count);

                //
                Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Close();
                throw;
            }
           
        }

        private void Close()
        {
            if (_localClientSocket != null)
            {
                _localClientSocket.Close();
                _server.RemoveClientSocket(this);
            }
        }

    }
}