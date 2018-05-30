using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Drawing;

namespace TIN
{
    public partial class Client : Form
    {
        /// <summary>
        /// Connection socket
        /// </summary>
        private Connection connection;

        private ConnectionFrom connForm;

        private Image toSendImage;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serverIP_"> server IP </param>
        /// <param name="port_"> connection port </param>
        /// <param name="socket_"> connection socket </param>
        public Client(Connection connection_, ConnectionFrom connForm_, IPAddress ip_, int port_)
        {
            connection = connection_;
            connForm = connForm_;
            toSendImage = null;
            InitializeComponent();
            IPLabel.Text = ip_.ToString();
            PortLabel.Text = port_.ToString();
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileWindow = new OpenFileDialog();
            fileWindow.InitialDirectory = "C:\\";
            fileWindow.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            fileWindow.RestoreDirectory = true;
            fileWindow.ShowDialog();
            try
            {
                Stream imageStream = fileWindow.OpenFile();
                FileInfo fInfo = new FileInfo(fileWindow.FileName);

                String imagePath = fInfo.Directory.ToString();
                String imageName = fInfo.Name.ToString();
                toSendImage= Image.FromStream(imageStream);

                pictureNameLabel.Text = imagePath + "\\" + imageName; 
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            connForm.Enabled = true;
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Disconnect();
                MessageBox.Show("disconnected");
            }
            catch (Exception exc)
            {
                MessageBox.Show("disconnect error" + exc.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (toSendImage == null)
                    throw new Exception("No selected image");
                byte[] imageBuffer = ImageToByteArray(toSendImage);
                connection.Send(imageBuffer);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
