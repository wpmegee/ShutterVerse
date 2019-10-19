﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

using System.Windows.Forms;

using ExifLib;

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
        }

        public ImageWithExif selectedImage { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            String path=String.Empty;

            if (result == System.Windows.Forms.DialogResult.OK)
            {
               path = dialog.SelectedPath;
            }

            List<ImageWithExif> imageList=new List<ImageWithExif>();

            if (path != String.Empty)
            {
                directoryLabel.Content = path;

                foreach (var file in Directory.EnumerateFiles(path, "*.jpg", SearchOption.AllDirectories))
                {
                    try
                    {
                        ImageWithExif image = new ImageWithExif(file);

                        imageList.Add(image);
                    } catch
                    {

                    }
                }

                dataGrid1.ItemsSource = imageList;
            }
        }

        private void DataGrid1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var image = dataGrid1.SelectedItem as ImageWithExif;
            ImagePreview.Source = new BitmapImage(new Uri(image.FileName));
        }
    }
}
