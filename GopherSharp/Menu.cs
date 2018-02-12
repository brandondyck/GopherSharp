using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace GopherSharp
{
    public class Menu
    {
        private List<Item> Items;

        public static Menu Parse(string data)
        {
            Parser<DocumentType> documentType =
                from typeCode in Sprache.Parse.Char(c => Enum.IsDefined(typeof(DocumentType), (byte)c), "")
                select (DocumentType)typeCode;
            Parser<string> field = Sprache.Parse.CharExcept("\t\r\n").Many().Text();
            Parser<char> delimiter = Sprache.Parse.Char('\t');
            Parser<IEnumerable<char>> newline = Sprache.Parse.String("\r\n");
            Parser<Item> item =
                from type in documentType
                from displayString in field
                from delim0 in delimiter
                from selector in field
                from delim1 in delimiter
                from host in field
                from delim2 in delimiter
                from port in field.Select(ushort.Parse)
                from rest in Sprache.Parse.AnyChar.Until(newline)
                select new Item(type, displayString, host, selector, port);
            var lastLine = Sprache.Parse.Char('.').Then(_ => newline).Optional();
            Parser<IEnumerable<Item>> menu =
                from items in item.AtLeastOnce()
                from end in lastLine
                select items;

            var result = menu.TryParse(data);
            if (result.WasSuccessful)
            {
                return new Menu(result.Value);
            }
            else
            {
                throw new Exception("Could not parse data: " + result.Message + "\nIs the document type wrong?");
            }
        }
        
        private Menu(IEnumerable<Item> items)
        {

            this.Items = items.ToList();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            Items.ForEach(item => builder.AppendLine(item.DisplayString));
            return builder.ToString();
        }

        public struct Item
        {
            public readonly DocumentType DocumentType;
            public readonly string DisplayString;
            public readonly string Host;
            public readonly string Selector;
            public readonly ushort Port;

            public Item(DocumentType documentType, string displayString, string host, string selector, ushort port)
            {
                DocumentType = documentType;
                DisplayString = displayString;
                Host = host;
                Selector = selector;
                Port = port;
            }
        }
    }
}
