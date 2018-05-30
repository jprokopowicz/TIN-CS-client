using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TIN
{
    public class ConnectionMenager
    {
        private IPAddress address;
        private int port;

        private Thread reciveThread;
        private Connection connection;
        private DataConverter converter;

        public ConnectionMenager(IPAddress address_, int port_)
        {
            address = address_;
            port = port_;
            //buffer = new byte[maxSize];
            connection = new Connection(address,port);
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect()
        {

                reciveThread = new Thread(reciveThreadFunction);
            try
            {
                connection.Connect();
                reciveThread.Start();
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

        public void Disconnect()
        {
            try
            {
                connection.Disconnect();
            }
            catch(Exception e)
            {
                MessageBox.Show("disconnect error: " + e.Message);
            }
        }

        public void Send(Image image)
        {
            connection.Send(converter.ConvertToBuffer(image));
        }

        private void reciveThreadFunction()
        {
            MessageBox.Show("Connected");
            try
            {
                var buffer = converter.GenerateBuffer();
                while (true)
                {
                    //MessageBox.Show("before recive");
                    int result = connection.Recive(buffer);
                    if (result == 0)
                    {//TODO: opracowac procedure dla rozłączania serwera
                        break;
                        // locker.WaitOne();
                    }
                    EventArgs args = new EventArgs();

                    var transferBuffer = converter.CopyBuffer(buffer);

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
