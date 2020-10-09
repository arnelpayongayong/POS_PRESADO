using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace POS.View.UserControlList
{
    public partial class uscDashboard : UserControl
    {
        public uscDashboard()
        {
            InitializeComponent();
            Initiliaze();
        }

        private void Initiliaze()
        {
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,10),
                        new ObservablePoint(4,13),
                        new ObservablePoint(5,6),
                        new ObservablePoint(10,20),

                    }
                },
                new LineSeries
                {
                    Title = "Sample",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0,2),
                        new ObservablePoint(3,5),
                        new ObservablePoint(10,6),
                        new ObservablePoint(10,20),

                    }
                }
            };

            pieChart1.Series = new SeriesCollection
            {
              new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
        
                    }
                },
                new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
                    }
                },
                 new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))

                    }
                },
                new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
                    }
                }
            };

            pieChart2.Series = new SeriesCollection
            {
              new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))

                    }
                },
                new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
                    }
                },
                 new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))

                    }
                },
                new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
                    }
                }
            };

            pieChart3.Series = new SeriesCollection
            {
              new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))

                    }
                },
                new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
                    }
                },
                 new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))

                    }
                },
                new PieSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                       new ObservablePoint(new Random().Next(10,20),new Random().Next(10,20))
                    }
                }
            };
        }
    }
}
