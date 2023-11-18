﻿using System;
using System.Windows;

namespace WpfApp4
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

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            MyDocumentViewer myDocumentViewer  = new MyDocumentViewer();
            myDocumentViewer.Show();
        }

        
    }
}
