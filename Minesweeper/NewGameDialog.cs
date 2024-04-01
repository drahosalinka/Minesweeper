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
        public string SelectedDifficulty { get; private set; }

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
            label.Text = "Add meg a méretet:";
            label.Dock = DockStyle.Top;

            numericUpDown = new NumericUpDown();
            numericUpDown.Minimum = 10;
            numericUpDown.Maximum = 100; // Példa maximum érték
            numericUpDown.Value = 10; // Alapértelmezett érték
            numericUpDown.Dock = DockStyle.Top;

            okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Dock = DockStyle.Bottom;
            okButton.Click += OkButton_Click;

            cancelButton = new Button();
            cancelButton.Text = "Mégse";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Dock = DockStyle.Bottom;

            this.Controls.Add(numericUpDown);
            this.Controls.Add(label);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;

            // Rádiógomb csoport létrehozása
            RadioButton easyRadioButton = new RadioButton() { Text = "Könnyű", AutoSize = true, Dock = DockStyle.Top };
            RadioButton normalRadioButton = new RadioButton() { Text = "Normál", AutoSize = true, Dock = DockStyle.Top };
            RadioButton hardRadioButton = new RadioButton() { Text = "Nehéz", AutoSize = true, Dock = DockStyle.Top };

            // Az eseménykezelők hozzáadása a rádiógombokhoz
            easyRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            normalRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            hardRadioButton.CheckedChanged += RadioButton_CheckedChanged;

            Controls.Add(easyRadioButton);
            Controls.Add(normalRadioButton);
            Controls.Add(hardRadioButton);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                SelectedDifficulty = radioButton.Text;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // Ellenőrizzük, hogy van-e kiválasztott nehézségi szint
            if (SelectedDifficulty == null)
            {
                // Ha nincs, akkor dobunk egy üzenetet
                MessageBox.Show("Kérem válasszon nehézségi szintet!", "Nehézségi szint hiányzik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; // Ne zárjuk be a dialógusablakot
                return; // Kilépünk a metódusból, nem engedjük tovább a dialógusablakot
            }

            SelectedSize = (int)numericUpDown.Value;
        }

    }

}
