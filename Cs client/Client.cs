using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Drawing;

namespace TIN
{
    /// <summary>
    /// Cient window. Opens images to send and recives broadcasted ones.
    /// </summary>
    public partial class Client : Form
    {

        private ConnectionMenager connManager;

        private ConnectionFrom connForm;

        private Image toSendImage;

        public Client(ConnectionFrom connForm_, IPAddress ip_, int port_)
        {
            InitializeComponent();

            connManager = new ConnectionMenager(ip_, port_, imageBox,this);
            connManager.Connect();
            connForm = connForm_;
            toSendImage = null;
            IPLabel.Text = ip_.ToString();
            PortLabel.Text = port_.ToString();
            this.MaximizeBox = false;
        }
       
        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileWindow = new OpenFileDialog();
            fileWindow.InitialDirectory = "C:\\";
            fileWindow.Filter = "Image files (*.jpg) | *.jpg";
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
            catch
            {

            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            connManager.Disconnect(false);
            connForm.Enabled = true;
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                connManager.Disconnect(false);
                Close();
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
                connManager.Send(toSendImage);
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

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            imageBox.Show();
        }
    }
}
