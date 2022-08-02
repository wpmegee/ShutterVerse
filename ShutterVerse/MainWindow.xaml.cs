using System;
using System.Windows;
using System.Windows.Media.Imaging;

using System.Windows.Forms;

using ExifLib;
using MahApps.Metro;

namespace ShutterVerse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataLoaded = false;
            DataContext = this;
            _darkMode = false;
        }

        public ImageWithExif selectedImage { get; set; }
        public bool DataLoaded { get; set; }
        public bool NotLoaded { get => !DataLoaded; set => DataLoaded = !value; }
        public ExifLib.ImageList list { get; set; }

        private bool _darkMode;

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            var path = string.Empty;

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }

            if (path != string.Empty)
            {
                Spinner.Visibility = Visibility.Visible;

                directoryLabel.Content = path;
                list = new ExifLib.ImageList(path);

                await list.Load();

                FocalLengthBarChart.FocalLengthLabels = list.FocalLengths;
                FocalLengthBarChart.FocalLengthValues = list.FocalLengthCounts;

                ShutterSpeedBarChart.ShutterSpeedLabels = list.ShutterSpeeds;
                ShutterSpeedBarChart.ShutterSpeedValues = list.ShutterSpeedCounts;

                ApertureBarChart.ApertureLabels = list.Apertures;
                ApertureBarChart.ApertureValues = list.Aperturecounts;

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

        private void DarkModeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (_darkMode)
            {
                ThemeManager.ChangeAppTheme(System.Windows.Application.Current, "BaseLight");
                _darkMode = false;
            }
            else
            {
                ThemeManager.ChangeAppTheme(System.Windows.Application.Current, "BaseDark");
                _darkMode = true;
            }
           
        }
    }
}
