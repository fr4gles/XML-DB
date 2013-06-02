using System;
using System.Collections.Generic;
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
using Microsoft.Win32;


namespace XML_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_openXmlFile_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
                {
                    Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                    InitialDirectory = Environment.CurrentDirectory,
                    Title = "Wybierz plik xml"
                };

            if (!openDialog.ShowDialog().Value) return;

            var pathToFile = openDialog.FileName;
            var result = new XmlParser(pathToFile);
            textBox_main.Text = result.ResultText;
        }
    }
}
