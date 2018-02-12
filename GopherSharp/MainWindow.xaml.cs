using System;
using System.Windows;

namespace GopherSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GopherModel Document = new GopherModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = Document;
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            string portString = portInput.Text;
            ushort port;
            if (portString == "")
            {
                port = 70;
            }
            else if (ushort.TryParse(portString, out port))
            {
                Document.Load(new Locator(hostInput.Text, selectorInput.Text, port, DocumentType.Menu));
            }
            else
            {
                MessageBox.Show("Port must be blank or 1–65535");
            }
        }
    }
}
