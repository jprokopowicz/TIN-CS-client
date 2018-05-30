using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TIN
{
    public class Connection
    {
        private Socket socket;
        private IPAddress address;
        private int port;
        private Thread reciveThread;
        private int maxSize = 512;

        public Connection(IPAddress address_, int port_)
        {
            address = address_;
            port = port_;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect()
        {
            reciveThread = new Thread(reciveThreadFunction);
            reciveThread.Start();
        }

        public void Disconnect()
        {
            try
            {
                reciveThread.Interrupt();
                socket.Disconnect(true);
            }
            catch(Exception e)
            {
                MessageBox.Show("disconnect error:" + e.Message);
            }
        }

        public void Send(byte[] image)
        {
            int result = socket.Send(image);
            MessageBox.Show(result.ToString());
        }

        private void reciveThreadFunction()
        {
            try
            {
                socket.Connect(address, port);
                MessageBox.Show("Connected");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Connection creation error:" + exc.Message);
            }

            try
            {
                byte[] buffer = new byte[maxSize];
                while (true)
                {
                    int result = socket.Receive(buffer);
                    MessageBox.Show("Recived " + result.ToString() + " bytes");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("recive thread exception: " + exc.Message);
            }
        }
    }
}
