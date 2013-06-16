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
using System.Data.SqlClient;
using System.Data.SqlServerCe;


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

            var result = new XmlParseAndRead(pathToFile);
            var xmlResult = new XmlToSql(result.ReadTableName());

            textBox_main.Text = xmlResult.MakeSqlCreateDbCommandFrom(result.ReadStructure());
            
            textBox_main.Text += "\n\n";

            textBox_main.Text += xmlResult.MakeSqlInsertValuesDbCommandFrom(result.ReadRecords());
        }

        private void button_openSdfFile_Click(object sender, RoutedEventArgs e)
        {
            textBox_main.Text = "";

            var openDialog = new OpenFileDialog
            {
                Filter = "xml files (*.sdf)|*.sdf|All files (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Wybierz plik sdf"
            };

            if (!openDialog.ShowDialog().Value) return;

            var pathToFile = openDialog.FileName;
            textBox_pathToSDF.Text = pathToFile;

            var sql = new ConnectToSql(pathToFile);


        }

        private void buttonLaunch_Click(object sender, RoutedEventArgs e)
        {

            var openDialog = new OpenFileDialog
            {
                Filter = "xml files (*.sdf)|*.sdf|All files (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Wybierz plik sdf"
            };

            if (!openDialog.ShowDialog().Value) return;

            var pathToFile = openDialog.FileName;
            

            var connection = new SqlCeConnection(@"Data Source=" + pathToFile + ";password=xml-db123");
            connection.Open();
            

            SqlCeCommand cmd = connection.CreateCommand();

            cmd.CommandText = "select * from TestTable";
            SqlCeDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               int j=0;
               string row = "Rekord: " + j;
                
                
                for (int i = 0; i < reader.FieldCount; i++)
                {
                  row+= "\nCol name:: " + reader.GetName(i) + " Col type: " + reader.GetDataTypeName(i);
                    
                }
                
                MessageBox.Show(row);
                j++;
                

            }          
            
        }
    }
}
