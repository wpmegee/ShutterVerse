﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

using System.Windows.Forms;
using System.Linq;

using ExifLib;
using ImageList = ExifLib.ImageList;

namespace ShutterVerse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataLoaded = false;
            DataContext = this;
        }

        public ImageWithExif selectedImage { get; set; }
        public bool DataLoaded { get; set; }
        public bool NotLoaded { get => !DataLoaded; set => DataLoaded = !value; }
        public ImageList list { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            String path = String.Empty;

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }

            if (path != String.Empty)
            {
                directoryLabel.Content = path;
                list = new ImageList(path);
                list.Load();

                FocalLengthBarChart.FocalLengthLabels = list.FocalLengths;
                FocalLengthBarChart.FocalLengthValues = list.FocalLengthCounts;

                dataGrid1.ItemsSource = list.list;
                DataLoaded = true;
            }
        }

        private void DataGrid1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var image = dataGrid1.SelectedItem as ImageWithExif;
            ImagePreview.Source = new BitmapImage(new Uri(image.FileName));
        }
    }
}
