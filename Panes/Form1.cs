using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Panes
{
    public partial class Form1 : Form
    {

        private List<Panel> panels;
        private int index = 0;
        private ExternalLabel externalLabel;

        public Form1()
        {
            InitializeComponent();
            externalLabel = new ExternalLabel(this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panels = new List<Panel>();
            panels.Add(panel1);
            panels.Add(panel2);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (index == panels.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            panels[index].BringToFront();
        }
    }
}
