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
            Document.Load(new Locator(hostInput.Text, selectorInput.Text, UInt16.Parse(portInput.Text), DocumentType.Menu));
        }
    }
}
