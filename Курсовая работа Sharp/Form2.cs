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

namespace Курсовая_работа_Sharp
{
    public partial class Form2 : Form
    {
        DataBase db = new DataBase();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                lvl = 1;
            }
            else if (checkBox2.Checked == true)
            {
                lvl = 2;
            }
            else if (checkBox3.Checked == true)
            {
                lvl = 3;
            }
            else
            {
                MessageBox.Show("Вы не выбрали уровень!");
                return;
            }

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `AllUsers` WHERE `Login` = @userL AND `Password` = @userP", db.getConn());
            // Расшифровка заглушек
            command.Parameters.Add("@userL", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@userP", MySqlDbType.VarChar).Value = textBox2.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                Form1 mainF = new Form1(this.lvl, this.textBox1.Text);
                this.Hide();

                // Открываем главную форму
                mainF.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int lvl;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Register sdsadas = new Register();
            this.Hide();
            sdsadas.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Scores sdsadas = new Scores();
            this.Hide();
            sdsadas.ShowDialog();
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

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        private void hide_MouseLeave(object sender, EventArgs e)
        {
            hide.ForeColor = Color.White;
        }
    }
}
