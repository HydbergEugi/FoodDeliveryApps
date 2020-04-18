using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace CouriersApp
{
    
    public partial class MainForm : Form
    {
        public int userIdG;
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
        public MainForm(Autorization enterForm, int userId)
        {
            InitializeComponent();
            this.FormClosing += AppClose;
            enterFormG = enterForm;
            userIdG = userId;
            loadOrders();
        }

        public void loadOrders()
        {
            MySqlConnection mySQLConn = createConnection();
            string query = "SELECT distinct orderNum FROM ordersForCouriers";
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            MySqlDataReader dr = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Clear();
            listView1.Columns.Add("", 70);
            listView1.Columns.Add("", 150);
            foreach (DataRow row in dt.Rows)
            {
                string[] itemRow = { "Заказ № ", row["orderNum"].ToString() };
                ListViewItem items = new ListViewItem(itemRow);
                listView1.Items.Add(items);
            }
            query = "SELECT distinct orderNum FROM takenOrder where id_courier = " + userIdG;
            command = new MySqlCommand(query, mySQLConn);
            dr = command.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);

            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.Clear();
            listView2.Columns.Add("", 70);
            listView2.Columns.Add("", 150);
            foreach (DataRow row in dt.Rows)
            {
                string[] itemRow = { "Заказ № ", row["orderNum"].ToString() };
                ListViewItem items = new ListViewItem(itemRow);
                listView2.Items.Add(items);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            int orderNum = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text);
            AcceptOrder orderInfo = new AcceptOrder(orderNum, this, userIdG);
            orderInfo.Show();
        }

        private void listView2_Click(object sender, EventArgs e)
        {
            int orderNum = Convert.ToInt32(listView2.SelectedItems[0].SubItems[1].Text);
            OrderInfo orderInfo = new OrderInfo(orderNum);
            orderInfo.Show();
        }

        private void AppClose(object sender, FormClosingEventArgs e)
        {

            enterFormG.Close();

        }
    }

    
}
