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
        public Form1()
        {
            InitializeComponent();
            InitializeMenu();
            InitializeGamePanel();
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
            // Új játék kezdése
            // Implementáld ezt a metódust a saját játékod logikájával
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

        private void InitializeGamePanel()
        {
            gamePanel = new TableLayoutPanel();
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.BackColor = Color.LightGray;

            for (int i = 0; i < 10; i++)
            {
                gamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30)); // Oszlopok szélessége 30 pixel
                gamePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // Sorok magassága 30 pixel
            }
            gamePanel.AutoSize = false;

            // A játékteret hozzáadni a formhoz
            Controls.Add(gamePanel);

            // Játékmezők létrehozása a Panelen belül
            CreateGameFields();
        }

        private void CreateGameFields()
        {
            int gridSize = 10; // Például 10x10-es játékteret készítünk
            gameFields = new Button[gridSize, gridSize]; // Inicializáljuk a gombok tömbjét
            gamePanel.Padding = new Padding(0, 30, 0, 0);

            // Játékmezők létrehozása és elhelyezése a Panelen belül
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    // Játékmező létrehozása
                    Button field = new Button();
                    field.Size = new Size(30, 30);
                    field.FlatStyle = FlatStyle.Flat; // Kicsit szebb megjelenítés
                    field.BackColor = Color.White; // Alapértelmezett szín
                    field.Dock = DockStyle.Fill; // A gomb dockolása Fill-re

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
