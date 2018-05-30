using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace TIN
{
    public partial class ConnectionFrom : Form
    {
        public ConnectionFrom()
        {
            this.MaximizeBox = false;
            InitializeComponent();
        }

         private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {

            String serverAddress;
            int port;
            IPAddress serverIP;
            Connection connection;
            try{
                serverAddress = textBox1.Text;
                if (serverAddress == "localhost")
                    serverAddress = GetLocalIPAddress();
                serverIP = IPAddress.Parse(serverAddress);
            }
            catch (FormatException){
                MessageBox.Show("Invalide IP");
                return;
            }
           
            try{
                port = Int32.Parse(textBox2.Text);
                if (port < 1024 || port > 65535){
                    MessageBox.Show("Invalid port");
                    return;
                }
            }
            catch(Exception){
                MessageBox.Show("Invalid port");
                return;
            }

            connection = new Connection(serverIP, port);
            try
            {
                connection.Connect();
                Client client = new Client(connection, this, serverIP, port);
                this.Enabled = false;
                client.Show();
            }
            catch(SocketException){
                MessageBox.Show("Connection error");
            }
           
        }

        private static string GetLocalIPAddress()
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
