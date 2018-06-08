using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINtest
{
    public partial class TestServerWindow : Form
    {
        int port;
        bool launched;
        Socket socket;
        Socket connctionSocket;
        public TestServerWindow()
        {
            port = 8080;
            launched = false;
           
            InitializeComponent();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (launched)
                return;
            launched = true;
            try
            {
                port = Int32.Parse(textBox1.Text);
                if (port < 1024 || port > 65535)
                    throw new FormatException();
            }
            catch (Exception exc)
            {
                MessageBox.Show("server: Invalide port: " + exc.Message);
                launched = false;
            }

            try
            {
                EndPoint endPoint = new IPEndPoint(IPAddress.Parse(GetLocalIPAddress()), port);
                socket.Bind(endPoint);
                socket.Listen(1);
                MessageBox.Show("server: server launched");
            }
            catch (Exception exc)
            {
                MessageBox.Show("server: Binding error: " + exc.Message);
                launched = false;
            }

            try
            {
                connctionSocket = socket.Accept();
                MessageBox.Show("server: accept!");
            }
            catch(Exception exc)
            {
                MessageBox.Show("server: accept error: " + exc.Message);
            }

            //System.Threading.Thread.Sleep(5000);
            //socket.Close();
            //Close();
            //return;
            while (true)
            {
                try
                {
                    byte[] buffor = new byte[DataConverter.maxSize];
                    byte[] recivedBuffor = new byte[DataConverter.maxSize];
                    bool waitingForTheRest = false;
                    int n = 0;
                    int numOfSentBytes = 0;


                    do
                    {
                        int recived = connctionSocket.Receive(recivedBuffor);

                        if (recived == 0)
                        {
                            socket.Close();
                            connctionSocket.Close();
                            MessageBox.Show("server: client disconnected");
                            launched = false;
                            break;
                        }

                        if (!waitingForTheRest)
                        {
                            byte[] header = DataConverter.CopyAndCutBuffer(recivedBuffor, 4);
                            int netOrderedBytes = BitConverter.ToInt32(header,0);
                            numOfSentBytes = IPAddress.NetworkToHostOrder(netOrderedBytes);
                        }

                        if (!DataConverter.CopyBuffer(recivedBuffor, buffor, 4, n, recived))
                        {
                            continue;
                            throw new Exception("Message too large");
                        }

                        n += recived;

                        waitingForTheRest = (n - 4) != numOfSentBytes;

                    } while (waitingForTheRest);
                    n -= 4;
                    
                    MessageBox.Show("server: recived " + n + " bytes");

                    ///
                    Encryptor encryptor = new Encryptor();

                    var keyData = encryptor.GetKey();

                    var encriptedBuffor = DataConverter.CopyAndCutBuffer(buffor, n);

                    /*var decryptedBuffer = encryptor.Decrypt(encriptedBuffor, keyData.Item1, keyData.Item2);
                    ///
                    try
                    {
                        Image image = DataConverter.ConvertToImage(decryptedBuffer);
                        pictureBox1.Image = image;
                   
                    }
                    catch (Exception exc1) { MessageBox.Show("server:"+ exc1.Message); }
                    pictureBox1.Refresh();

                    byte[] toSendBuffer = encryptor.Encrypt(decryptedBuffer, keyData.Item1, keyData.Item2);
                    */
                    ///
                    var toSendBuffer = encriptedBuffor;
                    //
                    numOfSentBytes = toSendBuffer.Length;

                    int netOrderedNumber = IPAddress.HostToNetworkOrder(numOfSentBytes);
                    byte[] sendHeader = BitConverter.GetBytes(netOrderedNumber);
                    byte[][] buffersList = { sendHeader, toSendBuffer };
                    byte[] toSend = DataConverter.ConnectBuffors(buffersList, 2);
                    

                    connctionSocket.Send(toSend);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("server: Recive excepton: " + exc.Message);
                    socket.Close();
                    launched = false;
                    break;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Show();
        }
    }
}
