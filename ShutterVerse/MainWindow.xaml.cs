using System;
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

        private async void Button_Click_1(object sender, RoutedEventArgs e)
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
                Spinner.Visibility = Visibility.Visible;

                directoryLabel.Content = path;
                list = new ImageList(path);

                await list.Load();

                FocalLengthBarChart.FocalLengthLabels = list.FocalLengths;
                FocalLengthBarChart.FocalLengthValues = list.FocalLengthCounts;

                dataGrid1.ItemsSource = list.list;
                DataLoaded = true;
            }

            Spinner.Visibility = Visibility.Hidden;
        }

        private void DataGrid1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem is ImageWithExif image)
            {
                // only preview the image for jpegs, for now.
                if (image.FileName.Contains("jpg"))
                {
                    ImagePreview.Source = new BitmapImage(new Uri(image.FileName));
                }
            }
        }
    }
}
