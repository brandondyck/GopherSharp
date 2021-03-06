﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;

namespace GopherSharp
{
    public class GopherModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ObservableCollection<Locator> history ;
        private readonly ReadOnlyObservableCollection<Locator> historyReadOnly;
        public ReadOnlyObservableCollection<Locator> History {
            get => historyReadOnly;
        }

        public GopherModel()
        {
            history = new ObservableCollection<Locator>();
            historyReadOnly = new ReadOnlyObservableCollection<Locator>(history);
        }

        private void NotifyPropertiesChanged(params string[] propNames)
        {
            foreach (string name in propNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Load(Locator locator)
        {
            try
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

                Body = Menu.Parse(bodyBuilder.ToString()).ToString();
                history.Add(locator);
            } catch (Exception e)
            {
                Body = e.Message;
            }
        }

        private string body;
        public string Body
        {
            get => body;
            private set
            {
                body = value;
                NotifyPropertiesChanged("Body");
            }
        }
    }
}
