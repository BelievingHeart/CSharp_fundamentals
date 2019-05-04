using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using System.Windows.Threading;

namespace ChartPaneDemo
{
    public class ChartForm: Form
    {
        private Chart _chart;
        private Form _parentForm;
        private ChartArea _chartArea;
        private List<Series> _seriesList;
        private string _chartAreaName = "data flow view";
        private int _numSeries;
        private int _numSamples;
        private List<Queue<double>> _data;


        public ChartForm(int numSamples, string axisName_Y, params Tuple<string, Color>[] seriesParams)
        {
            _numSeries = seriesParams.Length;
            _chart = new Chart();
            _chartArea = new ChartArea();
            _seriesList = new List<Series>();
            addSeriesParams(numSamples, seriesParams);


            SuspendLayout();

            initChart();
            initChartArea(axisName_Y);
            Controls.Add(_chart);
            resetData();

            ResumeLayout(false);
            PerformLayout();


        }

        private void resetData()
        {
            _data = new List<Queue<double>>();
            for (int i = 0; i < _numSeries; i++)
            {
                _data.Add(new Queue<double>());
            }
        }

        private void initChart()
        {
            _chart.Dock = DockStyle.Fill;

        }





        private void initChartArea(string axisName_Y)
        {
            ChartArea _chartArea = new ChartArea();
            _chartArea.Name = _chartAreaName;
            _chart.ChartAreas.Add(_chartArea);
            _chartArea.BackColor = Color.Azure;
            _chartArea.BackGradientStyle = GradientStyle.HorizontalCenter;
            _chartArea.BackHatchStyle = ChartHatchStyle.LargeGrid;
            _chartArea.BorderDashStyle = ChartDashStyle.Solid;
            _chartArea.BorderWidth = 1;
            _chartArea.BorderColor = Color.Red;
            _chartArea.ShadowColor = Color.Purple;
            _chartArea.ShadowOffset = 0;
  

            //Cursor：only apply the top area
            _chartArea.CursorX.IsUserEnabled = true;
            _chartArea.CursorX.AxisType = AxisType.Primary;//act on primary x axis
            _chartArea.CursorX.Interval = 1;
            _chartArea.CursorX.LineWidth = 1;
            _chartArea.CursorX.LineDashStyle = ChartDashStyle.Dash;
            _chartArea.CursorX.IsUserSelectionEnabled = true;
            _chartArea.CursorX.SelectionColor = Color.Yellow;
            _chartArea.CursorX.AutoScroll = true;

            _chartArea.CursorY.IsUserEnabled = true;
            _chartArea.CursorY.AxisType = AxisType.Primary;//act on primary y axis
            _chartArea.CursorY.Interval = 1;
            _chartArea.CursorY.LineWidth = 1;
            _chartArea.CursorY.LineDashStyle = ChartDashStyle.Dash;
            _chartArea.CursorY.IsUserSelectionEnabled = true;
            _chartArea.CursorY.SelectionColor = Color.Yellow;
            _chartArea.CursorY.AutoScroll = true;

            // Axis
            _chartArea.AxisY.Title = axisName_Y;
            _chartArea.AxisX.Minimum = 0d; //X axis Minimum value
            _chartArea.AxisX.Maximum = 10d;
            _chartArea.AxisX.IsLabelAutoFit = true;
            //chartArea.AxisX.LabelAutoFitMaxFontSize = 12;
            _chartArea.AxisX.LabelAutoFitMinFontSize = 5;
            _chartArea.AxisX.LabelStyle.Angle = -20;
            _chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;//show the last label
            _chartArea.AxisX.Interval = 1;
            _chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            _chartArea.AxisX.IntervalType = DateTimeIntervalType.NotSet;
            _chartArea.AxisX.Title = @"位置1234分别对应蓝绿黄粉";
            _chartArea.AxisX.TextOrientation = TextOrientation.Auto;
            _chartArea.AxisX.LineWidth = 2;
            _chartArea.AxisX.LineColor = Color.DarkOrchid;
            _chartArea.AxisX.Enabled = AxisEnabled.True;
            _chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Months;
            _chartArea.AxisX.ScrollBar = new AxisScrollBar();
        }

        public void addSeriesParams(int numSamples, params Tuple<string, Color>[] seriesParams)
        {

            // record the capacity of X-axis
            _numSamples = numSamples;

            foreach (var param in seriesParams)
            {
                var series = new Series(param.Item1);
                series.Color = param.Item2;

                series.ChartArea = _chartAreaName;
                series.ChartType = SeriesChartType.Line;  // type
                series.BorderWidth = 2;
                series.XValueType = ChartValueType.Int32;//x axis type
                series.YValueType = ChartValueType.Double;//y axis type
                // series.YValuesPerPoint = 6;

                //Marker
                series.MarkerStyle = MarkerStyle.Star4;
                series.MarkerSize = 10;
                series.MarkerStep = 1;
                series.MarkerColor = Color.Red;
                series.ToolTip = @"ToolTip";

                //Label
                series.IsValueShownAsLabel = true;
                series.SmartLabelStyle.Enabled = false;
                series.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                series.LabelForeColor = Color.Gray;
                series.LabelToolTip = @"LabelToolTip";

                //Empty Point Style 
                DataPointCustomProperties p = new DataPointCustomProperties();
                p.Color = Color.Green;
                series.EmptyPointStyle = p;

                //Legend
                series.LegendText = "LegendText：Test One";
                series.LegendToolTip = @"LegendToolTip";


                _seriesList.Add(series);
                _chart.Series.Add(series);
            }
        }

        public void updateSeries_Invoke(params double[] newData)
        {
            Debug.Assert(newData.Length == _numSeries);

            // If data enough, first pop then push, 
            if (dataEnough())
            {
                foreach (var q in _data)
                {
                    q.Dequeue();
                }
            }
            // otherwise just keep pushing
            for (int i = 0; i < _numSeries; i++)
            {
               _data[i].Enqueue(newData[i]);
            }

            // if data enough, add _numSample points to each series
            if (dataEnough())
            {
                if(this.Created)
                BeginInvoke((MethodInvoker) (() => { addPointsToSeries(); }));
                else
                {
                    addPointsToSeries();
                }
            }
           

        }

        private bool dataEnough()
        {
            return _data.All(a => a.Count == _numSamples);
        }


        private void addPointsToSeries()
        {

            for (int i = 0; i < _seriesList.Count; i++)
            {
                var q = _data[i];
                var series = _seriesList[i];

                if(series.Points != null)
                series.Points.Clear();

                for (int j = 0; j < _numSamples; j++)
                {
                    if(series!=null && series.Points != null)
                    series.Points.AddXY(j + 1, q.ElementAt(j));
                }
            }
        }

    }
}