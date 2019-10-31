using System;
using System.Windows.Controls;

using LiveCharts.Wpf;
using LiveCharts;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;

namespace ShutterVerse.Controls
{
    /// <summary>
    /// Interaction logic for ApertureBarChart.xaml
    /// </summary>
    public partial class ApertureBarChart : UserControl
    {
        public ApertureBarChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        private IOrderedEnumerable<string> _ApertureLabels;
        public IOrderedEnumerable<string> ApertureLabels
        {
            get => _ApertureLabels;
            set
            {
                _ApertureLabels = value;
                Labels = value.ToArray();
                DataContext = this;
            }
        }

        private IEnumerable<int> _ApertureValues;
        public IEnumerable<int> ApertureValues
        {
            get => _ApertureValues;
            set
            {
                _ApertureValues = value;
                SeriesCollection.Clear();
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Uses",
                    Values = new ChartValues<int>(value),
                    Fill = new SolidColorBrush(Colors.Green)
                });
                DataContext = this;
            }
        }
    }
}
