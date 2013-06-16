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

            var sql = new ConnectToSql(pathToFile);

        }
    }
}
