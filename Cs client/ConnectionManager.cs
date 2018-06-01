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
        PictureBox recivedImage;

        private Thread reciveThread;
        private Connection connection;

        public ConnectionMenager(IPAddress address_, int port_, PictureBox recivedImage_)
        {
            address = address_;
            port = port_;
            recivedImage = recivedImage_;
            connection = new Connection(address,port);
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
            connection.Send(DataConverter.ConvertToBuffer(image));
        }

        private void reciveThreadFunction()
        {
            MessageBox.Show("Connected");
            try
            {
                var buffer = DataConverter.GenerateBuffer(DataConverter.maxSize);
                while (true)
                {
                    int result = connection.Recive(buffer);
                    if (result == 0)
                    {
                        Disconnect();
                        break;
                    }

                    recivedImage.Image = DataConverter.ConvertToImage(buffer);

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
