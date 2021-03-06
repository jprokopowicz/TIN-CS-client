﻿using System.Net.Sockets;
using System.Net;
using System;

namespace TIN
{
    /// <summary>
    /// Uses sockets for connections operations.
    /// </summary>
    public class Connection
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
            socket.Shutdown(SocketShutdown.Both);
            socket.Close(1000);
            
        }

        public int Send(byte[] buffer)
        {
            int numOfSentBytes = buffer.Length;
            int netOrderedNumber = IPAddress.HostToNetworkOrder(numOfSentBytes);
            byte[] header = BitConverter.GetBytes(netOrderedNumber);
            byte[][] buffersList = { header , buffer };
            byte[] toSend = DataConverter.ConnectBuffors(buffersList, 2);
            return socket.Send(toSend);
        }

        public int Recive(byte[] buffer)
        {
            byte[] reciveBuffer = new byte[DataConverter.maxSize];
            bool waitingForTheRest = false;
            int n = 0;
            int numOfSentBytes = 0;
            do
            {
                int recived = socket.Receive(reciveBuffer);
                if (recived == 0)
                    return 0;
                if (!waitingForTheRest)
                {
                    byte[] header = DataConverter.CopyAndCutBuffer(reciveBuffer, 4);
                    int netOrderedNumber = BitConverter.ToInt32(header, 0);
                    numOfSentBytes = IPAddress.NetworkToHostOrder(netOrderedNumber);
                }

                if (!DataConverter.CopyBuffer(reciveBuffer, buffer, waitingForTheRest ? 0 : 4, Math.Max(0,n-4) , recived - (waitingForTheRest ? 0 : 4)))
                    throw new Exception("Message too large");
                n += recived;
                waitingForTheRest = (n - 4) != numOfSentBytes;
            } while (waitingForTheRest);
            return n-4;
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
