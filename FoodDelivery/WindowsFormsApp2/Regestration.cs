using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class Regestration : Form
    {
        Autorization autoFormG;
        public MySqlConnection createConnection()
        {

            string _host = "server92.hosting.reg.ru";
            string _login = "u0928571_mukuro";
            string _password = "exb[fvflfhf";
            string _dataBaseName = "u0928571_testbd";
            int _port = 3306;
            string connStr = String.Format("server={0}; database={3}; port={4}; user id={1}; password={2};  pooling=false; connection timeout=50; CharSet=cp1251",
                    _host, _login, _password, _dataBaseName, _port);

            MySqlConnection mySQLConn = new MySqlConnection(connStr);

            try
            {
                mySQLConn.Open();
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Не удалось подключиться к БД." + Environment.NewLine +
                    ex.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
            return mySQLConn;
        }
        public Regestration(Autorization autoForm)
        {
            InitializeComponent();
            autoFormG = autoForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = createConnection();
            string query1 = "INSERT INTO clients(name, surname, login, password) values ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "')";
            MySqlCommand command = new MySqlCommand(query1, conn);
            command.ExecuteNonQuery();
            autoFormG.getClientList();
            this.Dispose();
        }

    }
}
