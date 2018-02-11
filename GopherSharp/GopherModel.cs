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

        public void Load(Locator locator)
        {
            TcpClient conn = new TcpClient(locator.Host, locator.Port);
            NetworkStream stream = conn.GetStream();

            byte[] selectorMsg = Encoding.ASCII.GetBytes(locator.Selector + "\r\n");
            stream.Write(selectorMsg, 0, selectorMsg.Length);

            StringBuilder bodyBuilder = new StringBuilder();
            byte[] recvBuf = new byte[256];
            int count = 0;
            do
            {
                count = stream.Read(recvBuf, 0, recvBuf.Length);
                bodyBuilder.Append(Encoding.ASCII.GetString(recvBuf, 0, count));
            } while (count > 0);

            this.CurrentLocator = locator;

            Body = bodyBuilder.ToString();
        }

        private Locator CurrentLocator;

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

        public string Host
        {
            get => CurrentLocator.Host;
        }

        public string Selector
        {
            get => CurrentLocator.Selector;
        }

        public UInt16 Port
        {
            get => CurrentLocator.Port;
        }
    }
}
