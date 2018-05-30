using System.Net.Sockets;
using System.Net;
using System;

namespace TIN
{
    class Connection
    {
        Socket socket;
        IPAddress address;
        int port;

        public Connection(IPAddress address_,int port_)
        {
            address = address_;
            port = port_;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            try
            {
                socket.Connect(address, port);
            }catch(Exception exc)
            {
                throw exc;
            }
        }

        public void Disconnect()
        {
            socket.Disconnect(true);
        }

        public int Send(byte[] buffer)
        {
            return socket.Send(buffer);
        }

        public int Recive(byte[] buffer)
        {
            return socket.Receive(buffer);
        }
        public static string GetLocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
