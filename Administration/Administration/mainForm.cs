using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Administration
{

    public partial class MainForm : Form
    {
        public int userId = 1;
        DataTable dt;
        Autorization enterFormG;
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
        public MainForm(Autorization enterForm)
        {
            InitializeComponent();
            this.FormClosing += AppClose;
            enterFormG = enterForm;
            fillProductList();
            fillCourierList();

        }

        public void fillProductList()
        {
            MySqlConnection mySQLConn = createConnection();
            string query = "select products.*, product_type.name as typeName from products join product_type on products.type = product_type.id ";
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            MySqlDataReader dr = command.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["typeName"].Width = 350;
        }

        public void fillCourierList()
        {
            MySqlConnection mySQLConn = createConnection();
            string query = "select * from couriers ";
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            MySqlDataReader dr = command.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dataGridView2.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertProduct insertProduct = new InsertProduct(this);
            insertProduct.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateProduct updateProduct = new UpdateProduct(dataGridView1.CurrentRow, this);
            updateProduct.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertCourier insertCourier = new InsertCourier(this);
            insertCourier.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void AppClose(object sender, FormClosingEventArgs e)
        {

            enterFormG.Close();

        }
    }
}
