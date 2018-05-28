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
        private Socket socket;

        private ConnectionFrom connForm;

        private Stream imageStream;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serverIP_"> server IP </param>
        /// <param name="port_"> connection port </param>
        /// <param name="socket_"> connection socket </param>
        public Client(Socket socket_, ConnectionFrom connForm_, IPAddress ip_, int port_)
        {
            socket = socket_;
            connForm = connForm_;
            InitializeComponent();
            IPLabel.Text = ip_.ToString();
            PortLabel.Text = port_.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {

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
                imageStream = fileWindow.OpenFile();
                FileInfo fInfo = new FileInfo(fileWindow.FileName);

                String imagePath = fInfo.Directory.ToString();
                String imageName = fInfo.Name.ToString();
                //if(fInfo.Length > MAXSIZE)
                //TODO sprawdzenie rozmiaru
                //
                Image image = Image.FromStream(imageStream);

                imageBox.Image = image;
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
                socket.Disconnect(true);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                MessageBox.Show("disconnected");
                this.Close();
            }
        }
    }
}
