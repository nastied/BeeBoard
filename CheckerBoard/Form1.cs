using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckerBoard
{
    public partial class Form1 : Form
    {
        int n, size;
        int x = 0, y = 0;
        int xcare, ycare;
        int scor = 0;
        bool gameStarted = false;
        bool justScored = false;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            label5.Text = "Score: ";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = Convert.ToInt32(comboBox1.SelectedItem);
            size = 500 / n;
            x = 0;
            y = 0;
            scor = 0;
            label4.Text = scor.ToString();

            Random rnd = new Random();
            xcare = rnd.Next(0, n);
            ycare = rnd.Next(0, n);

            label3.Text = "(x, y): (" + (xcare + 1).ToString() + ", " + (ycare + 1).ToString() + ")";

            gameStarted = true;
            justScored = false;
            timer1.Start();

            pictureBox1.Invalidate();

            this.ActiveControl = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!gameStarted) return;

            Graphics g = e.Graphics;

            drawBoard(g);

            if (justScored)
            {
                SolidBrush sb = new SolidBrush(Color.Coral);
                Rectangle rectangle = new Rectangle(x * size + size / 4, y * size + size / 4, size / 2, size / 2);
                g.FillRectangle(sb, rectangle);
                sb.Dispose();

                string imagePath1 = Application.StartupPath + @"\bee1.png";
                Image img1 = Image.FromFile(imagePath1);
                g.DrawImage(img1, x * size + size / 4, y * size + size / 4, size / 2, size / 2);
            }
            else
            {
                pion(g, x, y);
            }
        }

        private void drawBoard(Graphics g)
        {
            SolidBrush sb = new SolidBrush(Color.PapayaWhip);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        sb.Color = Color.Goldenrod;
                    }
                    else
                    {
                        sb.Color = Color.PapayaWhip;
                    }
                    Rectangle rectangle = new Rectangle(i * size, j * size, size, size);
                    g.FillRectangle(sb, rectangle);
                }
            }
            sb.Dispose();
        }

        private void pion(Graphics g, int px, int py)
        {
            string imagePath = Application.StartupPath + @"\bee.png";
            Image img = Image.FromFile(imagePath);
            g.DrawImage(img, px * size + size / 4, py * size + size / 4, size / 2, size / 2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (x == xcare && y == ycare && !justScored)
            {
                scor++;
                label4.Text = scor.ToString();

                Random rnd = new Random();

                do
                {
                    xcare = rnd.Next(0, n);
                    ycare = rnd.Next(0, n);
                } while (xcare == x && ycare == y);

                label3.Text = "(x, y): (" + (xcare + 1).ToString() + ", " + (ycare + 1).ToString() + ")";

                justScored = true;
                pictureBox1.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameStarted) return;

            justScored = false;

            if (e.KeyCode == Keys.D && x < n - 1)
            {
                x++;
            }
            if (e.KeyCode == Keys.A && x > 0)
            {
                x--;
            }
            if (e.KeyCode == Keys.W && y > 0)
            {
                y--;
            }
            if (e.KeyCode == Keys.S && y < n - 1)
            {
                y++;
            }

            pictureBox1.Invalidate();
        }
    }
}