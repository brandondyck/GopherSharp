using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Document.Load(hostInput.Text, selectorInput.Text, UInt16.Parse(portInput.Text));
        }
    }
}
