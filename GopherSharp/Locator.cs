using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GopherSharp
{
    public enum DocumentType
    {
        Menu,
    }

    public class Locator
    {
        public readonly string Host;

        public readonly string Selector;

        public readonly UInt16 Port;

        public readonly DocumentType Type;

        public Locator(string host, string selector, UInt16 port, DocumentType type)
        {
            Host = host;
            Selector = selector;
            Port = port;
            Type = type;
        }
    }
}
