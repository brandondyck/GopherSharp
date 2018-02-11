using System;

namespace GopherSharp
{
    public enum DocumentType : byte
    {
        Text = (byte)'0',
        Menu = (byte)'1',
        Info = (byte)'i',
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

        public override string ToString() => String.Format("{0} {1} {2} {3}", Host, Selector, Port, ((char)Type).ToString());
    }
}
