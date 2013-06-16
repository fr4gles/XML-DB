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
    /// Interaction logic for DbToXmlWindow.xaml
    /// </summary>
    public partial class DbToXmlWindow : Window
    {
        private ConnectToSql sql;

        public DbToXmlWindow()
        {
            InitializeComponent();
        }

        private void button_openSdfFile_Click(object sender, RoutedEventArgs e)
        {

            var openDialog = new OpenFileDialog
            {
                Filter = "xml files (*.sdf)|*.sdf|All files (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Wybierz plik sdf"
            };

            if (!openDialog.ShowDialog().Value) return;

            var pathToFile = openDialog.FileName;
            textBox_pathToSDF.Text = pathToFile;

            sql = new ConnectToSql(pathToFile);

            if (sql.isEmpty)
            {
                MessageBox.Show("Wczytana baza danych: " + pathToFile +
                                " --> niestety nie zawiera żadnych tabel. Wybierz inny plik.");
                return;
            }

            try
            {
                listBox_tables.ItemsSource = sql.GetTablesNames();
                listBox_tables.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błą podczas wczytywania listBoxa - operacja wczytywania zostaje przerwana: " + ex.Message + "\n" + ex.StackTrace);
                return;
            }

            RefreshView();
        }

        private void listBox_tables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshView();

            ConvertDbToXml();
        }

        private void RefreshView()
        {
            try
            {
                datagrid_tableRows.ItemsSource = sql.GetDataFromTable(listBox_tables.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błą podczas wczytywania listView - operacja wczytywania zostaje przerwana: " + ex.Message +
                                "\n" + ex.StackTrace);
                return;
            }
        }

        private void ConvertDbToXml()
        {

        }
    }
}
