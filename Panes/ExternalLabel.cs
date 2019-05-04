using System.Drawing;
using System.Windows.Forms;

namespace Panes
{
    public class ExternalLabel
    {
        private Label label;
        private Form1 parentForm;

        public ExternalLabel(Form1 parentForm)
        {
            this.parentForm = parentForm;
            label = new Label();
            label.AutoSize = true;
            label.Location = new Point(86, 481);
            label.Name = "label";
            label.Size = new Size(62, 18);
            label.TabIndex = 2;
            label.Text = "label";
            this.parentForm.Controls.Add(label);

        }
    }
}