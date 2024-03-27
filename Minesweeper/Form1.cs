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
    public partial class Form1 : Form
    {
        private TableLayoutPanel gamePanel; 
        private Button[,] gameFields;
        private int gridSize = 10; 
        public Form1()
        {
            InitializeComponent();
            InitializeMenu();
            InitializeGamePanel(gridSize);
        }

        private void InitializeMenu()
        {
            // Létrehozunk egy menüsort
            MenuStrip menuStrip = new MenuStrip();

            // Létrehozunk menüelemeket
            ToolStripMenuItem newGameMenu = new ToolStripMenuItem("New Game");
            ToolStripMenuItem exitMenu = new ToolStripMenuItem("Exit");

            // A menüelemekhez hozzáadjuk az eseménykezelőket
            newGameMenu.Click += NewGameMenuItem_Click;
            exitMenu.Click += ExitMenuItem_Click;

            // Az "Exit" menüelemhez hozzáadjuk a gyorsbillentyűt
            exitMenu.ShortcutKeys = Keys.Control | Keys.X;

            // A menüsort hozzáadjuk a formhoz
            menuStrip.Items.Add(newGameMenu);
            menuStrip.Items.Add(exitMenu);

            // A formhoz hozzáadjuk a menüsort
            Controls.Add(menuStrip);
        }

        private void NewGameMenuItem_Click(object sender, EventArgs e)
        {
                NewGameDialog dialog = new NewGameDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    gridSize = dialog.SelectedSize;
                    // Tisztító lépések: töröld az összes gombot a játékmezőről
                    gamePanel.Controls.Clear();
                    InitializeGamePanel(gridSize);
                }

        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            // Kilépés az alkalmazásból
            Close();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void InitializeGamePanel(int gridSize)
        {
            gamePanel = new TableLayoutPanel();
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.BackColor = Color.LightGray;

            for (int i = 0; i < gridSize; i++)
            {
                gamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30)); // Oszlopok szélessége 30 pixel
                gamePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // Sorok magassága 30 pixel
            }


            // A játékteret hozzáadni a formhoz
            Controls.Add(gamePanel);

            // Játékmezők létrehozása a Panelen belül
            CreateGameFields();
        }

        private void CreateGameFields()
        {
            gameFields = new Button[gridSize, gridSize]; // Inicializáljuk a gombok tömbjét
            gamePanel.Padding = new Padding(5, 30, 5, 30);

            // Játékmezők létrehozása és elhelyezése a Panelen belül
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    // Játékmező létrehozása
                    Button field = new Button();
                    field.Size = new Size(30, 30);
                    field.Margin = new Padding(0);
                    field.FlatStyle = FlatStyle.Flat; // Kicsit szebb megjelenítés
                    field.BackColor = Color.White; // Alapértelmezett szín

                    // Az aktuális cellához adás
                    gamePanel.Controls.Add(field, j, i);

                    // Gomb eseménykezelőjének hozzáadása
                    field.Click += Field_Click;
                }
            }
        }

        private void Field_Click(object sender, EventArgs e)
        {
            // Az esemény kiváltója (sender) egy gomb, így el tudjuk érni annak tulajdonságait
            Button clickedField = (Button)sender;
            // Itt teheted meg, amit a gombra kattintva szeretnél
            // Például:
            clickedField.BackColor = Color.Red; // A gomb háttere piros lesz a kattintásra
        }
    }
}
