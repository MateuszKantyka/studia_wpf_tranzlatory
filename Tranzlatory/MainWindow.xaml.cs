using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Tranzlatory
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void chooseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                fileNameTb.Text = filename;
                readFromFileTb.Text = File.ReadAllText(filename);

                List<Item> itemList = new List<Item>();
                Item.getDataFromTxt(filename, itemList);
                lvDataBinding.ItemsSource = itemList;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // jak wywalić?
        }
    }
}
