using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

using System.Windows.Forms;
using System.Linq;

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
            DataLoaded = false;
            DataContext = this;
        }

        public ImageWithExif selectedImage { get; set; }
        public bool DataLoaded { get; set; }
        public bool NotLoaded { get => !DataLoaded; set => DataLoaded = !value; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            String path = String.Empty;

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }

            List<ImageWithExif> imageList = new List<ImageWithExif>();

            if (path != String.Empty)
            {
                directoryLabel.Content = path;

                foreach (var file in Directory.EnumerateFiles(path, "*.jpg", SearchOption.AllDirectories))
                {
                    try
                    {
                        ImageWithExif image = new ImageWithExif(file);

                        imageList.Add(image);
                    }
                    catch
                    {

                    }
                }

                FocalLengthBarChart.FocalLengthLabels = imageList.GroupBy(l => l.FocalLength)
                                  .Select(g => g.Key).OrderBy(g => g);

                FocalLengthBarChart.FocalLengthValues = imageList.GroupBy(l => l.FocalLengthDouble)
                                  .Select(g => g.Select(l => l.FocalLengthDouble).Count());


                dataGrid1.ItemsSource = imageList;
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
