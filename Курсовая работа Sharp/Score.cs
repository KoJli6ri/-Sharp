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
    public partial class Scores : Form
    {
        DataBase db = new DataBase();
        public Scores()
        {
            InitializeComponent();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private void LoadData()
        {
            // Выбираем данные из таблицы пользователя сортированные по ID
            MySqlCommand command = new MySqlCommand("SELECT * FROM  `Score` ORDER BY `Score`", db.getConn());

            db.openConn();
            // Считываем данные из базы данных
            MySqlDataReader reader = command.ExecuteReader();
            // Создаем список строкового масива
            List<string[]> data = new List<string[]>();
            
            // Получаем данные
            while (reader.Read())
            {
                // Добавляем новую строку состоящую с двух елементов в список
                data.Add(new string[2]);

                // Вносим первый елемент масива в Название
                data[data.Count - 1][0] = reader[0].ToString();

                // Вносим второй елемент масива в Сообщение
                data[data.Count - 1][1] = reader[1].ToString();

            }
            reader.Close();

            // Закрываем соединение
            db.closeConn();

            // Выводим данные в таблицу
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

            for (int v = 0; v < dataGridView1.Rows.Count; v++)
            {
                if(string.Equals(dataGridView1[1, v].Value as string, "0"))
                {
                    dataGridView1.Rows.RemoveAt(v);
                    v--;
                }
            }
        }

        private void Score_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
