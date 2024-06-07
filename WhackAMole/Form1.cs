using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WhackAMole
{
    public partial class Form1 : Form
    {
        //Initialize
        private int score = 0;
        private const int maxScore = 30;
        private Random rand = new Random();
        private Button activeButton;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            //Setup 4 round for a game
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnCount = 2;

            //Initialize Click on Layout
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Button btn = new Button();

                    //Fill up screen and create click on Layout
                    btn.Dock = DockStyle.Fill;
                    btn.Click += new EventHandler(Button_Click);
                    tableLayoutPanel1.Controls.Add(btn, j, i);
                }
            }

            //Initialize Time 
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(Timer_Tick);
            timer1.Start();

            //Initialize Score
            lblScore.Text = "Score: 0";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //Add point when click at right place
            if (sender == activeButton)
            {
                //Plus
                score++;
                lblScore.Text = "Score: " + score;
                activeButton.BackColor = default(Color);
                activeButton = null;

                //Reach 30 point will reset 
                if (score >= maxScore)
                {
                    timer1.Stop();
                    MessageBox.Show("Congratulations! You reached the maximum score!", "Game Over");
                    ResetGame();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = default(Color);
            }
            
            //Random appearance
            int row = rand.Next(2);
            int col = rand.Next(2);
            var control = tableLayoutPanel1.GetControlFromPosition(col, row);
            if (control is Button button)
            {
                activeButton = button;
                activeButton.BackColor = Color.Red;
            }
        }

        private void ResetGame()
        {
            //Reset when reach 30
            score = 0;
            lblScore.Text = "Score: 0";
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = default(Color);
                }
            }
            timer1.Start();
        }
    }
}
