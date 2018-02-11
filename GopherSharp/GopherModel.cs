using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GopherSharp
{
    public class GopherModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Load(string host, string selector, UInt16 port)
        {
            TcpClient conn = new TcpClient(host, port);
            NetworkStream stream = conn.GetStream();

            byte[] selectorMsg = Encoding.ASCII.GetBytes(selector + "\r\n");
            stream.Write(selectorMsg, 0, selectorMsg.Length);

            StringBuilder bodyBuilder = new StringBuilder();
            byte[] recvBuf = new byte[256];
            int count = 0;
            do
            {
                count = stream.Read(recvBuf, 0, recvBuf.Length);
                bodyBuilder.Append(Encoding.ASCII.GetString(recvBuf, 0, count));
            } while (count > 0);

            Host = host;
            Selector = selector;
            Port = port;

            Body = bodyBuilder.ToString();
        }

        private string body;
        public string Body
        {
            get => body;
            private set
            {
                body = value;
                NotifyPropertyChanged("Body");
            }
        }

        private string host;
        public string Host
        {
            get => host;
            private set
            {
                host = value;
                NotifyPropertyChanged("Host");
            }
        }

        private string selector;
        public string Selector
        {
            get => selector;
            private set
            {
                selector = value;
                NotifyPropertyChanged("Selector");
            }
        }

        private UInt16 port;
        public UInt16 Port
        {
            get => port;
            private set {
                port = value;
                NotifyPropertyChanged("Port");
            }
        }
    }
}
