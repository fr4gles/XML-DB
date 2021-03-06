﻿using Microsoft.Win32;
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
using System.Xml;

namespace XML_DB
{
    /// <summary>
    /// Interaction logic for DbToXmlWindow.xaml
    /// </summary>
    public partial class DbToXmlWindow : Window
    {
        private ConnectToSql sql;
        XmlDocument xmlDoc;

        public DbToXmlWindow()
        {
            InitializeComponent();

            if (!loadData()) return;

            RefreshView();
            ConvertDbToXml();
        }

        private bool loadData()
        {
            sql = new ConnectToSql(MainWindow.mainSettings.databasePath);

            if (sql.isEmpty)
            {
                MessageBox.Show("Wczytana baza danych: " + (MainWindow.mainSettings.databasePath) +
                                " --> niestety nie zawiera żadnych tabel. Wybierz inny plik.");
                return false;
            }

            try
            {
                listBox_tables.ItemsSource = sql.GetTableNames();
                listBox_tables.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania listBoxa - operacja wczytywania zostaje przerwana: " + ex.Message + "\n" + ex.StackTrace);
                return false;
            }

            return true;
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
            }
        }

        private void ConvertDbToXml()
        {            
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(DbToXmlWriter.CreateXmlBody(listBox_tables.SelectedItem.ToString()));

            xmlDoc.Save("tempXml.xml");            
            webBrowserXml.Navigate(Environment.CurrentDirectory+"\\tempXml.xml");
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var tempSaveFileDialog = new SaveFileDialog {Filter = "XML |*.xml", Title = "Zapisz plik XML"};

            tempSaveFileDialog.ShowDialog();

             if(tempSaveFileDialog.FileName!= "")
             {
                xmlDoc.Save(tempSaveFileDialog.FileName); 
             }

        }
        
    }
}
