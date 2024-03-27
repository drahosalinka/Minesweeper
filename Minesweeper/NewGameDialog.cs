using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    using System;
    using System.Windows.Forms;

    public partial class NewGameDialog : Form
    {
        private Label label;
        private NumericUpDown numericUpDown;
        private Button okButton;
        private Button cancelButton;

        public int SelectedSize { get; private set; }

        public NewGameDialog()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Új játék indítása";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            label = new Label();
            label.Text = "Kérem adja meg a sorok és oszlopok számát:";
            label.Dock = DockStyle.Top;

            numericUpDown = new NumericUpDown();
            numericUpDown.Minimum = 1;
            numericUpDown.Maximum = 100; // Példa maximum érték
            numericUpDown.Value = 10; // Alapértelmezett érték
            numericUpDown.Dock = DockStyle.Top;

            okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Dock = DockStyle.Left;
            okButton.Click += OkButton_Click;

            cancelButton = new Button();
            cancelButton.Text = "Mégse";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Dock = DockStyle.Right;

            this.Controls.Add(numericUpDown);
            this.Controls.Add(label);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SelectedSize = (int)numericUpDown.Value;
        }
    }

}
