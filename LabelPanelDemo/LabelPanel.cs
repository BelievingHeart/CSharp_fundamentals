using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPanelDemo
{
    class LabelPanel: System.Windows.Forms.Panel
    {
        private Label label;

        public LabelPanel(string label_text, Form parentForm)
        {
            label = new Label();
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(39, 34);
            this.label.Name = "label1";
            this.label.Size = new System.Drawing.Size(62, 18);
            this.label.TabIndex = 0;
            this.label.Text = label_text;
            Controls.Add(label);
            initMe();

            parentForm.Controls.Add(this);
        }

        private void initMe()
        {
            SuspendLayout();
            Location = new System.Drawing.Point(142, 71);
            Name = "panel1";
            Size = new System.Drawing.Size(408, 275);
            TabIndex = 0;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
