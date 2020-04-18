using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Administration
{
    public partial class InsertCourier : Form
    {
        public int userId = 1;
        MainForm mainFormG;
        public InsertCourier(MainForm mainForm)
        {
            InitializeComponent();
            mainFormG = mainForm;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection mySQLConn = mainFormG.createConnection();
            string query = "INSERT INTO couriers (name, surname, patronymic, telephone, login, password) " +
                           "values ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" 
                           + textBox5.Text + "', '" + textBox6.Text +  "')";
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            command.ExecuteReader();
            mainFormG.fillCourierList();
            this.Dispose();
        }
    }
}
