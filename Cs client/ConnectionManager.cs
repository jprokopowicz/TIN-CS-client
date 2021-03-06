﻿using System;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace TIN
{
    /// <summary>
    /// Manage sending and reciving messages. Creates recive thread. Uses connection as the lowest layer.
    /// </summary>
    public class ConnectionMenager
    {
        private IPAddress address;
        private int port;
        private PictureBox recivedImage;
        private Client window;

        private Thread reciveThread;
        private Connection connection;
        private Encryptor encryptor;
        private Boolean disconnected;

        public ConnectionMenager(IPAddress address_, int port_, PictureBox recivedImage_, Client window_)
        {
            address = address_;
            port = port_;
            recivedImage = recivedImage_;
            window = window_;
            connection = new Connection(address,port);
            encryptor = new Encryptor();
            disconnected = false;
        }

        public void Connect()
        {

            reciveThread = new Thread(reciveThreadFunction);
            try
            {
                connection.Connect();
                disconnected = false;
                reciveThread.Start();
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

        public void Disconnect(bool close)
        {
            try
            {
                disconnected = true;
                connection.Disconnect();
                if (close)
                {
                    MessageBox.Show("should close window");
                    window.Close();
                }
            }
            catch(Exception e)
            {
                if (!disconnected)
                    MessageBox.Show("disconnect error: " + e.Message);
            }
        }

        public void Send(Image image)
        {
            Encryptor encryptor = new Encryptor();
            var keyData = encryptor.GetKey();

            var buffer = DataConverter.ConvertToBuffer(image);
           
            var encriptedBuffer = encryptor.Encrypt(buffer, keyData.Item1, keyData.Item2);

            connection.Send(encriptedBuffer);
        }

        private void reciveThreadFunction()
        {
            MessageBox.Show("Connected");
            try
            {
                var buffer = DataConverter.GenerateBuffer(DataConverter.maxSize);
                while (true)
                {
                    int n = connection.Recive(buffer);
                    if (n == 0)
                    {
                        Disconnect(false);
                        break;
                    }

                    var keyData = encryptor.GetKey();
                    var encryptedBuffer = DataConverter.CopyAndCutBuffer(buffer, n);
                    var decryptedBuffer = encryptor.Decrypt(encryptedBuffer, keyData.Item1, keyData.Item2);

                    recivedImage.Image = DataConverter.ConvertToImage(decryptedBuffer);

                    MessageBox.Show("Recived " + n.ToString() + " bytes");
                } 
            }
            catch (Exception exc)
            {
                if(!disconnected)
                    MessageBox.Show("recive thread exception: " + exc.Message);
            }
        }
    }
}
