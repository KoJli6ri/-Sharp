using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using Курсовая_работа_Sharp.Properties;

namespace Курсовая_работа_Sharp
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public Bitmap HandlerTexutere = Resource1.handler1,
                      TargetTexture = Resource1.target;
        private Point _targetPosition = new Point(300, 300);
        private Point _direction = Point.Empty;
        Timer t = new Timer();

        DataBase db = new DataBase();
        int lvl;
        string Login;


        public Form1(int lvl, string Login)
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            this.lvl = lvl;
            this.Login = Login;
            if(lvl == 1)
            {
                t.Interval = 2000;
                player.URL = "1lvl.mp3";
                player.settings.volume = 1;
            }
            if (lvl == 2)
            {              
                t.Interval = 900;
                player.URL = "2lvl.mp3";
                player.settings.volume = 1;
            }
            if (lvl == 3)
            {
                t.Interval = 600;
                player.URL = "3lvl.mp3";
                player.settings.volume = 1;
            }
            t.Tick += new EventHandler(save);
            t.Start();

            UpdateStyles();
        }

        void save(Object Sender, EventArgs e)
        {
            if (life == 0)
            {
                t.Enabled = false;
            }
            asfdsadasd();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r = new Random();

            timer2.Interval = r.Next(25, 100);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);
        }

        private void asfdsadasd()
        {            
            int s1 = pictureBox1.Size.Width;
            int s2 = pictureBox1.Size.Height;
            Random rnd = new Random();
            pictureBox1.Location = new Point(rnd.Next(1000 + s1), rnd.Next(1570 + s2));
            pictureBox1.Visible = true;
            int x1 = pictureBox1.Location.X;                        // Левый край контрола
            int x2 = pictureBox1.Location.X + pictureBox1.Size.Width;  // Правый край контрола
            int y1 = pictureBox1.Location.Y;                        // Верхний край контрола
            int y2 = pictureBox1.Location.Y + pictureBox1.Size.Height; // Нижний край контрола
            if (x1 > 887)
                pictureBox1.Location = new Point(rnd.Next(300 + s1), rnd.Next(100 + s2));
            if (x2 > 887)
                pictureBox1.Location = new Point(rnd.Next(300 + s1), rnd.Next(300 + s2));
            if (y1 > 535)
                pictureBox1.Location = new Point(rnd.Next(300 + s1), rnd.Next(300 + s2));
            if (y2 > 535)
                pictureBox1.Location = new Point(rnd.Next(300 + s1), rnd.Next(300 + s2));
        }

        private void ScoreSet()
        {
            db.openConn();
                MySqlCommand commands = new MySqlCommand("SELECT `Score` FROM `Score` WHERE `Name` = @Login", db.getConn()); // Вносим данные в базу
                commands.Parameters.Add("@Login", MySqlDbType.VarChar).Value = Login;
                object result = commands.ExecuteScalar();
                int kod = Convert.ToInt32(result);
                db.closeConn();

                if (i > kod)
                {
                    MySqlCommand command = new MySqlCommand("UPDATE `Score` SET Score = @Score WHERE Name = @Login", db.getConn()); // Вносим данные в базу
                    command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = Login;
                    command.Parameters.Add("@Score", MySqlDbType.VarChar).Value = i;
                    db.openConn();
                    // Проверяем удачно ли выполнилась команда
                    command.ExecuteNonQuery();
                    db.closeConn();
                }

        }


        static int i = 1;
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            t.Stop();
            t.Start();

            label1.Text = Convert.ToString(life);
            if (life == 0)
            {
                player.controls.stop();
                DialogResult result = MessageBox.Show("Начать знову??", "Game Over",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    ScoreSet();
                    scorelabel1.Text = "0";
                    label1.Text = "3";
                    i = 1;
                    x = 0;
                    life = 3;
                    player.controls.play();
                }
                if (result == DialogResult.No)
                {
                    ScoreSet();
                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                }
            }
            else
            {
                asfdsadasd();
                scorelabel1.Text = Convert.ToString(i++);
                x = 0;
            }
        }

        private void scorelabel1_Click(object sender, EventArgs e)
        {

        }

        int x = 0;
        int life = 3;
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (life == 0)
            {
                player.controls.stop();
                DialogResult result = MessageBox.Show("Начать знову?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Yes)
                {
                    ScoreSet();
                    scorelabel1.Text = "0";
                    label1.Text = "3";
                    i = 1;
                    x = 0;
                    life = 3;
                    player.controls.play();
                }
                if (result == DialogResult.No)
                {
                    
                    ScoreSet();
                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                }
            }
            else
            {
                x++;
                if (x > 0)
                {
                    asfdsadasd();
                    x = 0;
                    label1.Text = Convert.ToString(--life);

                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void hide_MouseEnter(object sender, EventArgs e)
        {
            hide.ForeColor = Color.Black;
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void hide_MouseLeave(object sender, EventArgs e)
        {
            hide.ForeColor = Color.White;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }     

    }
}
