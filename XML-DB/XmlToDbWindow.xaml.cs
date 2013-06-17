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
        string createCommand;
        string insertCommand;

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

            createCommand = xmlResult.MakeSqlCreateDbCommandFrom(result.ReadStructure()); ;
            textBox_main.Text = createCommand;
            
            textBox_main.Text += "\n\n";

            insertCommand = xmlResult.MakeSqlInsertValuesDbCommandFrom(result.ReadRecords()); ;
            textBox_main.Text += insertCommand;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            DropAllTablesInDb();

            SplitCreateCommand();

            SplitInsertCommand();
        }

        private static void DropAllTablesInDb()
        {
            // usuwannie wszystkich table z bazy
            try
            {
                var tmpConn = new ConnectToSql(MainWindow.mainSettings.databasePath);
                foreach (var tableName in tmpConn.GetTableNames())
                {
                    CommandLauncher.LaunchSqlCommand("drop table " + tableName, tmpConn.connection);
                }
            }
            catch (Exception)
            {
                // puste, for testing
            }
        }

        private void SplitInsertCommand()
        {
            string[] words2 = insertCommand.Split(';');
            for (int i = 0; i < words2.Length - 1; i++)
            {
                CommandLauncher.LaunchSqlCommand(words2[i]);
            }
        }

        private void SplitCreateCommand()
        {
            string[] words = createCommand.Split(';');

            for (int i = 0; i < words.Length - 1; i++)
            {
                CommandLauncher.LaunchSqlCommand(words[i]);
            }
        }
    }
}
