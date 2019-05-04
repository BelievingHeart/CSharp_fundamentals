using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPanelDemo
{
    public partial class Form1 : Form
    {
        private  static double previousHeight = 960, previousWidth = 1280;
        private static double aspectRatio = previousWidth / previousHeight;

        public Form1()
        {
            InitializeComponent();
            // LabelPanel
            labelPanel = new LabelPanel("world", this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
                Width = (int)(Height * aspectRatio);
                previousHeight = Height;
        }
    }
}
