using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace XML_DB
{
    /// <summary>
    /// Interaction logic for XmlToDbWindow.xaml
    /// </summary>
    public partial class XmlToDbWindow : Window
    {
        public XmlToDbWindow()
        {
            InitializeComponent();
        }

        private void button_openXmlFile_Click(object sender, RoutedEventArgs e)
        {
            textBox_main.Text = "";



            var openDialog = new OpenFileDialog
            {
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Wybierz plik xml"
            };

            if (!openDialog.ShowDialog().Value) return;

            var pathToFile = openDialog.FileName;
            textBox_pathToXML.Text = pathToFile;

            webBrowserXml.Navigate(pathToFile);

            var result = new XmlParseAndRead(pathToFile);
            var xmlResult = new XmlToSql(result.ReadTableName());

            textBox_main.Text = xmlResult.MakeSqlCreateDbCommandFrom(result.ReadStructure());
            textBox_main.Text += "\n\n";
            textBox_main.Text += xmlResult.MakeSqlInsertValuesDbCommandFrom(result.ReadRecords());

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            



        }
    }
}
