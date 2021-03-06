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
using System.Windows.Shapes;

namespace XML_DB
{
    /// <summary>
    /// Interaction logic for LaunchSqlCommand.xaml
    /// </summary>
    public partial class LaunchSqlCommand : Window
    {
        public LaunchSqlCommand()
        {
            InitializeComponent();
        }

        private void buttonLaunchSql_Click(object sender, RoutedEventArgs e)
        {
            textBoxReturn.Text = CommandLauncher.LaunchSqlCommand(textBoxCommand.Text);           
        }
    }
}
