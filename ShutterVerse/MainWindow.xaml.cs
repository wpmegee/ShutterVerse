using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

using System.Windows.Forms;
using System.Linq;

using ExifLib;

using LiveCharts.Wpf;
using LiveCharts;


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
            SeriesCollection = new SeriesCollection();
        }

        public ImageWithExif selectedImage { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

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


                var labels = imageList.GroupBy(l => l.FocalLength)
                                  .Select(g => g.Key).OrderBy(g=>g);


                var values = imageList.GroupBy(l => l.FocalLengthDouble)
                                  .Select(g => g.Select(l => l.FocalLengthDouble).Count());

                SeriesCollection.Add(new ColumnSeries
                {
                    
                 Values = new ChartValues<int> (values)
    
                });

                Labels = labels.ToArray();
                Formatter = value => value.ToString("N");
                DataContext = this;

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
