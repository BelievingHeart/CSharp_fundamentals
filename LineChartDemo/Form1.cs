using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LineChartDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Chart chart1 = null;
        private Chart chart2;
        private void Form1_Load(object sender, EventArgs e)
        {
            chart1 = new Chart();
            chart1.Location = new Point(10, 10);
            chart1.Width = 600;
            chart1.Height = 500;
            chart1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
            this.Controls.Add(chart1);
            // one chart can have many ChartAreas，one ChartAreas can have many Series
            firstSeries();
//            secondSeries();
        }

        private void firstSeries()
        {
            // chartArea
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "First Area";
            chart1.ChartAreas.Add(chartArea);
            chartArea.BackColor = Color.Azure;
            chartArea.BackGradientStyle = GradientStyle.HorizontalCenter;
            chartArea.BackHatchStyle = ChartHatchStyle.LargeGrid;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.Red;
            chartArea.ShadowColor = Color.Purple;
            chartArea.ShadowOffset = 0;
           // chart1.ChartAreas[0].Axes[0].MajorGrid.Enabled = false;//x axis
           // chart1.ChartAreas[0].Axes[1].MajorGrid.Enabled = false;//y axis

            //Cursor：only apply the top area
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.AxisType = AxisType.Primary;//act on primary x axis
            chartArea.CursorX.Interval = 1;
            chartArea.CursorX.LineWidth = 1;
            chartArea.CursorX.LineDashStyle = ChartDashStyle.Dash;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.SelectionColor = Color.Yellow;
            chartArea.CursorX.AutoScroll = true;

            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.AxisType = AxisType.Primary;//act on primary y axis
            chartArea.CursorY.Interval = 1;
            chartArea.CursorY.LineWidth = 1;
            chartArea.CursorY.LineDashStyle = ChartDashStyle.Dash;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.SelectionColor = Color.Yellow;
            chartArea.CursorY.AutoScroll = true;

            // Axis
            chartArea.AxisY.Minimum = -10d;//Y axis Minimum value
            chartArea.AxisY.Title = @"Consumption Value";
            //chartArea.AxisY.Maximum = 100d;//Y axis Maximum value
            chartArea.AxisX.Minimum = 0d; //X axis Minimum value
            chartArea.AxisX.Maximum = 12d;
            chartArea.AxisX.IsLabelAutoFit = true;
            //chartArea.AxisX.LabelAutoFitMaxFontSize = 12;
            chartArea.AxisX.LabelAutoFitMinFontSize = 5;
            chartArea.AxisX.LabelStyle.Angle = -20;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;//show the last label
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chartArea.AxisX.IntervalType = DateTimeIntervalType.NotSet;
            chartArea.AxisX.Title = @"Month";
            chartArea.AxisX.TextOrientation = TextOrientation.Auto;
            chartArea.AxisX.LineWidth = 2;
            chartArea.AxisX.LineColor = Color.DarkOrchid;
            chartArea.AxisX.Enabled = AxisEnabled.True;
            chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Months;
            chartArea.AxisX.ScrollBar = new AxisScrollBar();

            //Series
            Series series = new Series();
            series.ChartArea = "First Area";
            chart1.Series.Add(series);
            //Series style
            series.Name = @"series：Test One";
            series.ChartType = SeriesChartType.Line;  // type
            series.BorderWidth = 2;
            series.Color = Color.Green;
            series.XValueType = ChartValueType.Int32;//x axis type
            series.YValueType = ChartValueType.Int32;//y axis type
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

            float[] values = { 0, 70, 90, 20, 70, 220, 30, 60, 30, 81, 10, 39 };

            int x = 1;
            foreach (float v in values)
            {
                series.Points.AddXY(x, v);
                x++;
            }
        }


        private void secondSeries()
        {
            //Series
            Series series1 = new Series("");
            chart1.Series.Add(series1);
            chart1.Series[1].YAxisType = AxisType.Secondary;//Secondary axis

            series1.Name = @"series：Test Two";
            series1.ChartType = SeriesChartType.Spline;
            series1.BorderWidth = 2;
            series1.Color = Color.Red;
            series1.XValueType = ChartValueType.Int32;
            series1.YValueType = ChartValueType.Int32;

            //Marker
            series1.MarkerStyle = MarkerStyle.Triangle;
            series1.MarkerSize = 10;
            series1.MarkerStep = 1;
            series1.MarkerColor = Color.Gray;
            series1.ToolTip = @"ToolTip";

            //Label:
            series1.IsValueShownAsLabel = true;
            series1.SmartLabelStyle.Enabled = false;
            series1.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
            series1.LabelForeColor = Color.Gray;
            series1.LabelToolTip = @"LabelToolTip";

            //Legend
            series1.LegendText = "LegendText：Test Two";
            series1.LegendToolTip = @"LegendToolTip";

            float[] values = { 10, 40, 20, 30, 70, 80, 80, 30, 90, 50 };
            int x = 1;
            foreach (float v in values)
            {
                series1.Points.AddXY(x, v);
                x++;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();

            float[] values = { 10, 40, 20, 30, 70, 80, 80, 30, 90, 50 };
            int x = 1;
            foreach (float v in values)
            {
                chart1.Series[0].Points.AddXY(x, v);
                x++;
            }
        }


    }
}
