using System;
using System.Windows.Controls;

using LiveCharts.Wpf;
using LiveCharts;
using System.Linq;
using System.Collections.Generic;

namespace ShutterVerse.Controls
{
    /// <summary>
    /// Interaction logic for FocalLengthBarChart.xaml
    /// </summary>
    public partial class FocalLengthBarChart : UserControl
    {
        public FocalLengthBarChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private IOrderedEnumerable<string> _FocalLengthLabels;
        public IOrderedEnumerable<string> FocalLengthLabels
        {
            get { return _FocalLengthLabels; }
            set
            {
                _FocalLengthLabels = value;
                Labels = value.ToArray();
                DataContext = this;
            }
        }

        private IEnumerable<int> _FocalLengthValues;
        public IEnumerable<int> FocalLengthValues
        {
            get { return _FocalLengthValues; }
            set
            {
                _FocalLengthValues = value;
                SeriesCollection.Add(new ColumnSeries
                {
                    Values = new ChartValues<int>(value)
                });
                DataContext = this;
            }
        }
    }
}
