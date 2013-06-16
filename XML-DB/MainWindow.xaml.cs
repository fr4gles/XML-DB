﻿using System;
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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace XML_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Settings mainSettings = null;
        public MainWindow()
        {
            InitializeComponent();
            
            if (System.IO.File.Exists("settingsFile.obj"))
            {
                mainSettings = LoadSettingsFromFile("settingsFile.obj");
            }
            else
            {                
                MessageBox.Show("Nie wykryto pliku ustawień bazy danych. Wprowadź ustawienia");
                var newWindow = new SettingsWindow();
                newWindow.ShowDialog();
                mainSettings = LoadSettingsFromFile("settingsFile.obj");                 
            }
            MessageBox.Show(mainSettings.databasePath);//do testowania
            MessageBox.Show(mainSettings.password);//jw

            
        }

        public Settings LoadSettingsFromFile(string settingsFilePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(settingsFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Settings obj = (Settings)formatter.Deserialize(stream);
            stream.Close();
            return obj;
            
        }

        public void SaveSettingsToFile(Settings tempSettings,string settingsFilePath)
        {                     
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(settingsFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, tempSettings);
            stream.Close();
        }



      
        private void buttonXmlToDb_Click(object sender, RoutedEventArgs e)
        {

            var newWindow = new XmlToDbWindow();
            newWindow.Show();            
        }

        private void buttonDbToXml_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new DbToXmlWindow();
            newWindow.Show();   
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new SettingsWindow();
            newWindow.ShowDialog();

        }

        private void buttonCommand_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new LaunchSqlCommand();
            newWindow.Show();
            mainSettings = LoadSettingsFromFile("settingsFile.obj");     
        }
    }
}
