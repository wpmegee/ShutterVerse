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
    /// Interaction logic for ShutterSpeedBarChart.xaml
    /// </summary>
    public partial class ShutterSpeedBarChart : UserControl
    {
        public ShutterSpeedBarChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> Formatter { get; set; }

        private IOrderedEnumerable<string> _ShutterSpeedLabels;
        public IOrderedEnumerable<string> ShutterSpeedLabels
        {
            get => _ShutterSpeedLabels;
            set
            {
                _ShutterSpeedLabels = value;
                Labels = value.ToArray();
                DataContext = this;
            }
        }

        private IEnumerable<int> _ShutterSpeedValues;
        public IEnumerable<int> ShutterSpeedValues
        {
            get => _ShutterSpeedValues;
            set
            {
                _ShutterSpeedValues = value;
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "Uses",
                    Values = new ChartValues<int>(value),
                    Fill = new SolidColorBrush(Colors.Red)
                });
                DataContext = this;
            }
        }
    }
}
