using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartPaneDemo
{
    public partial class FormMain : Form
    {
        private static int _numSamples = 10;
        private static int _timeStamp = 0;

        private ChartForm _chartForm = new ChartForm(_numSamples,"HH", new Tuple<string, Color>("Hime", Color.Pink),
            new Tuple<string, Color>("Hina", Color.Blue));
        private Random _ran1 = new Random(42);


        public FormMain()
        {
            InitializeComponent();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnPopChartPanelForm_Click(object sender, EventArgs e)
        {
            _chartForm.Show();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            feedData(_timeStamp++);
        }


        private void feedData(int timeStamp)
        {
            int offset = timeStamp / 30;
            double data1 = _ran1.Next(-10 + offset, 10 + offset);
            double data2 = _ran1.Next(-10 + offset, 10 + offset);

            if (_chartForm != null)
                _chartForm.updateSeries_Invoke(data1, data2);


            textBox1.Text = data2.ToString();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                _chartForm.resizeData(Convert.ToInt32(textBox2.Text));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _chartForm.resetSummery();
        }

    }
}
