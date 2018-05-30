using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TIN
{
    public class ConnectionMenager
    {
        private Socket socket;
        private IPAddress address;
        private int port;
        private Thread reciveThread;
        private int maxSize = 512;
        private byte[] buffer;

        

        public ConnectionMenager(IPAddress address_, int port_)
        {
            address = address_;
            port = port_;
            buffer = new byte[maxSize];
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
                    socket.Disconnect(true);
            }
            catch(Exception e)
            {
                MessageBox.Show("disconnect error: " + e.Message);
            }
        }

        public void Send(byte[] image)
        {
            int result = socket.Send(image);
            MessageBox.Show(result.ToString());
        }

        private void reciveThreadFunction()
        {
            socket.Connect(address, port);
            MessageBox.Show("Connected");

        
            try
            {
                
                while (true)
                {
                    MessageBox.Show("before recive");
                    int result = socket.Receive(buffer);
                    if (result == 0)
                    {//TODO: opracowac procedure dla rozłączania serwera
                        break;
                        // locker.WaitOne();
                    }
                    EventArgs args = new EventArgs();

                    byte[] transferBuffer = CopyBuffer(buffer);

                    MessageBox.Show("Recived " + result.ToString() + " bytes");
                } 
            }
            catch (Exception exc)
            {
                MessageBox.Show("recive thread exception: " + exc.Message);
            }
        }

        //TODO: replace
        private byte[] CopyBuffer(byte[] sorce)
        {
            byte[] result = new byte[maxSize];
            for(int i = 0; i < maxSize; ++i){
                result[i] = sorce[i];
            }
            return result;
        } 
    }
}
