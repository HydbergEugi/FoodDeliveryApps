using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace CouriersApp
{
    public partial class AcceptOrder : Form
    {
        int userIdG;
        int orderNumG;
        DataTable dt = new DataTable();
        MainForm mainFormG;
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
        public AcceptOrder(int orderNum, MainForm mainForm, int userId)
        {
            InitializeComponent();
            mainFormG = mainForm;
            orderNumG = orderNum;
            userIdG = userId;
            MySqlConnection mySQLConn = createConnection();
            string query = "SELECT * FROM ordersForCouriers where orderNum = " + orderNum;
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            MySqlDataReader dr = command.ExecuteReader();

            dt.Load(dr);

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Название", 150);
            listView1.Columns.Add("Имя клиента", 150);
            listView1.Columns.Add("Количество", 150);
            listView1.Columns.Add("Адрес", 150);
            listView1.Columns.Add("Дата заказа", 150);
            foreach (DataRow row in dt.Rows)
            {
                string[] itemRow = { row["name"].ToString(), row["clientName"].ToString(), row["amount"].ToString(), row["address"].ToString(), row["date"].ToString() };
                ListViewItem items = new ListViewItem(itemRow);
                listView1.Items.Add(items);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection mySQLConn = createConnection();
            string query = "update orders set id_courier = " + userIdG + " where id_OfOneOrder = " + orderNumG;
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            command.ExecuteNonQuery();
            mainFormG.loadOrders();
            this.Dispose();
        }
    }
}
