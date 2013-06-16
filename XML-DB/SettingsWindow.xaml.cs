using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            if (MainWindow.mainSettings != null)
            {
                textBox_pathToSDF.Text = MainWindow.mainSettings.databasePath;
                textBoxPass.Text = MainWindow.mainSettings.password;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Settings newSettings = new Settings(textBox_pathToSDF.Text,textBoxPass.Text);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("settingsFile.obj", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, newSettings);
            stream.Close();
            this.Close();
        }

        private void button_openSdfFile_Click(object sender, RoutedEventArgs e)
        {
             var openDialog = new OpenFileDialog()
            {
                Filter = "xml files (*.sdf)|*.sdf|All files (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Wybierz plik sdf"
            };

            if (!openDialog.ShowDialog().Value) return;

            var pathToFile = openDialog.FileName;
            textBox_pathToSDF.Text = pathToFile;          


        }
    }
}
