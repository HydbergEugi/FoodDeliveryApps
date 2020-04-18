using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;


namespace WindowsFormsApp2
{
    
    public partial class Autorization : Form
    {
        DataSet ds;
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
        public Autorization()
        {
            InitializeComponent();
            getClientList();
        }

        public void getClientList()
        {
            MySqlConnection conn = createConnection();
            string query1 = "select * from clients";
            MySqlCommand command = new MySqlCommand(query1, conn);
            MySqlDataAdapter adapt = new MySqlDataAdapter(command);
            ds = new DataSet();
            adapt.Fill(ds);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if ((textBox1.Text == ds.Tables[0].Rows[i][3].ToString()) && (textBox2.Text == ds.Tables[0].Rows[i][4].ToString()))
                {
                    MainForm mainForm = new MainForm(this, Convert.ToInt32(ds.Tables[0].Rows[i]["id"]));
                    mainForm.Show();
                    this.Hide();
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regestration regForm = new Regestration(this);
            regForm.Show();
        }
    }
}
