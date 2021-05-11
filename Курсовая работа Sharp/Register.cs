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
    public partial class Register : Form
    {
        DataBase db = new DataBase();
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            // Проверяем свободный ли логин. Если нет, выходим из функции 
            if (isUser())
                return;

            MySqlCommand command = new MySqlCommand("INSERT INTO `AllUsers` (`Login`, `Password`) VALUES (@login, @pass)", db.getConn());
            MySqlCommand commands = new MySqlCommand("INSERT INTO `Score` (`Name`, `Score`) VALUES (@login, @Score)", db.getConn());
            // Снимаем заглушки
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;
            commands.Parameters.Add("@login", MySqlDbType.VarChar).Value = login.Text;
            commands.Parameters.Add("@Score", MySqlDbType.VarChar).Value = 0;

            db.openConn();  // Открываем соединение
            command.ExecuteNonQuery();
            commands.ExecuteNonQuery();
            db.closeConn(); // Закрываем соединение
            MessageBox.Show("Ви успішно зареєструвались!");
        }

        public Boolean isUser()
        {
            try
            {
                DataBase db = new DataBase();
                // Создаем таблицу в которой будут проверяться данные
                DataTable table = new DataTable();

                // Получаем и Сохраняем данные в adapter
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                // Ищем в базе данных Логин который ввёл пользователь раннее
                MySqlCommand command = new MySqlCommand("SELECT * FROM `AllUsers` WHERE `login` = @userL", db.getConn());
                command.Parameters.Add("@userL", MySqlDbType.VarChar).Value = login.Text;

                adapter.SelectCommand = command;    // Выполняем комманду
                adapter.Fill(table);    // Записываем итог выполения комманды в таблицу
                if (table.Rows.Count > 0)   // Проверяем есть ли совпадения с логином
                {
                    MessageBox.Show("Цей логін уже зареєстрований!");
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                MessageBox.Show("Помилка!");
                return true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 ijfjg = new Form2();
            this.Hide();
            ijfjg.Show();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Black;
        }

        private void hide_MouseEnter(object sender, EventArgs e)
        {
            hide.ForeColor = Color.Black;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        private void hide_MouseLeave(object sender, EventArgs e)
        {
            hide.ForeColor = Color.White;
        }

        private void login_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
