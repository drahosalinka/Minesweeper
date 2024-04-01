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
        private int difficulty = 2;
        private string[,] bombGrid;

        public Form1()
        {
            InitializeComponent();
            InitializeMenu();
            InitializeGamePanel(gridSize, difficulty);
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

                    switch (dialog.SelectedDifficulty)
                    {
                        case "Könnyű":
                        difficulty = 1;
                            break;
                        case "Normál":
                        difficulty = 2;
                            break;
                        case "Nehéz":
                        difficulty = 3;
                            break;
                        default:
                            break;
                    }

                InitializeGamePanel(gridSize, difficulty);
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

        private void InitializeGamePanel(int gridSize, int difficulty)
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
            InitializeBombGrid(gridSize);
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
                    field.Tag = bombGrid[i, j];

                    // Gomb eseménykezelőjének hozzáadása
                    field.MouseDown += gameField_MouseDown;
                }
            }
        }

        private void InitializeBombGrid(int gridSize)
        {
            bombGrid = new string[gridSize, gridSize];

            PlaceBombsAndNumbers(gridSize * difficulty); // Aknák véletlenszerű elhelyezése
        }

        private void PlaceBombsAndNumbers(int bombCount)
        {
            Random random = new Random();
            List<Point> availablePositions = new List<Point>();

            // Gyűjtsd össze az összes lehetséges pozíciót
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    availablePositions.Add(new Point(i, j));
                }
            }

            // Véletlenszerű mintavételezés a rendelkezésre álló pozíciókból
            for (int i = 0; i < bombCount; i++)
            {
                // Véletlenszerűen válasszunk egy pozíciót a rendelkezésre álló pozíciók közül
                int index = random.Next(0, availablePositions.Count);
                Point position = availablePositions[index];

                // Jelöld meg az akna pozícióját a bombGrid mátrixban
                bombGrid[position.X, position.Y] = "X";

                // Távolítsd el a kiválasztott pozíciót a rendelkezésre álló pozíciók listájából
                availablePositions.RemoveAt(index);
            }

            // Az availablePositions lista végigmegy és minden pozícióra megvizsgálja a körülötte lévő mezőket
            foreach (Point position in availablePositions)
            {
                int count = 0; // Mezők számolása

                // Szomszédos mezők körülbelül 8 pozíciója
                for (int i = position.X - 1; i <= position.X + 1; i++)
                {
                    for (int j = position.Y - 1; j <= position.Y + 1; j++)
                    {
                        // Ellenőrizzük, hogy a jelenlegi pozíció a griden belül van-e
                        if (i >= 0 && i < gridSize && j >= 0 && j < gridSize)
                        {
                            // Ellenőrizd a szomszédos mezőket
                            if (bombGrid[i, j] == "X")
                            {
                                count++;
                            }
                        }
                    }
                }

                // Ha a count értéke nagyobb, mint 0 (vagyis van legalább egy aknás szomszéd), akkor beállítja a számozást
                if (count > 0)
                {
                    bombGrid[position.X, position.Y] = count.ToString();
                } 
                else if(count == 0)
                {
                    bombGrid[position.X, position.Y] = "";
                }
            }

        }



        private void gameField_MouseDown(object sender, MouseEventArgs e)
        {
            Button clickedField = (Button)sender;

            int x = gamePanel.GetColumn(clickedField);
            int y = gamePanel.GetRow(clickedField);

            if (e.Button == MouseButtons.Right && clickedField.BackColor == Color.White)
            {
                clickedField.BackColor = Color.Red;
            } else if(e.Button == MouseButtons.Right && clickedField.BackColor == Color.Blue) {
                clickedField.BackColor = Color.White;
            }

            if (e.Button == MouseButtons.Left)
            {
                if((string)clickedField.Tag != "X")
                {
                    clickedField.BackColor = Color.LightGray;
                    string content = (string)clickedField.Tag;
                    clickedField.Text = (string)clickedField.Tag;
                    if(string.IsNullOrEmpty((string)clickedField.Tag))
                    {
                        RevealEmptyFields(x, y);
                    }
                }else
                {
                    RevealAllFields();
                    MessageBox.Show("Sajnos vesztettél", "Vesztes!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gamePanel.Controls.Clear();
                    InitializeGamePanel(gridSize, difficulty);
                }


            }

        }

        private void RevealAllFields()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Button field = gamePanel.GetControlFromPosition(i, j) as Button;
                    string content = (string)field.Tag;
                    field.Text = content;
                    if (content == "X")
                    {
                        field.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void RevealEmptyFields(int x, int y)
        {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(x, y));

            while (queue.Count > 0)
            {
                Point currentPoint = queue.Dequeue();
                x = currentPoint.X;
                y = currentPoint.Y;

                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && i < gridSize && j >= 0 && j < gridSize)
                        {
                            Button field = gamePanel.GetControlFromPosition(i, j) as Button;

                            if (field != null && field.BackColor != Color.LightGray)
                            {
                                field.BackColor = Color.LightGray;
                                field.Text = (string)field.Tag;

                                if (string.IsNullOrEmpty((string)field.Tag))
                                {
                                    queue.Enqueue(new Point(i, j));
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
