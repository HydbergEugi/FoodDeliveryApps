using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public int userIdG;
        DataTable dt = new DataTable();
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
            enterFormG = enterForm;
            userIdG = userId;
            this.FormClosing += AppClose;
            label1.Text = "0";
            MySqlConnection mySQLConn = createConnection();
            string query = "SELECT * FROM fullProducts ";
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            MySqlDataReader dr = command.ExecuteReader();

            dt.Load(dr);

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Название", 150);
            listView1.Columns.Add("Тип", 150);
            listView1.Columns.Add("Стоимость", 50);
            foreach (DataRow row in dt.Rows)
            {
                string[] itemRow = { row["name"].ToString(), row["type"].ToString(), row["cost"].ToString() };
                ListViewItem items = new ListViewItem(itemRow);
                listView1.Items.Add(items);
            }

            listView2.View = View.Details;
            listView2.Columns.Add("Название", 150);
            listView2.Columns.Add("Gога", 0);
            listView2.Columns.Add("Тип", 150);
            listView2.Columns.Add("Количество", 50);
        }

        public void fillOrderList(ListViewItem items)
        {
            listView2.Items.Add(items);
            MySqlConnection conn = createConnection();
            string query;
            double cost = 0;
            foreach (ListViewItem item in listView2.Items)
            {
                query = "SELECT cost from fullProducts where name = '" + item.SubItems[0].Text + "'";
                MySqlCommand command = new MySqlCommand(query, conn);
                cost = cost + Convert.ToDouble(command.ExecuteScalar())*Convert.ToInt32(item.SubItems[3].Text);
            }

            label1.Text = cost.ToString("#.##");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void listView1_Click(object sender, EventArgs e)
        {
            ProductInfo preorderWindow = new ProductInfo(listView1.SelectedItems, this);
            preorderWindow.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = createConnection();
            string query;
            query = "insert into orderNum(row) values (' ')";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            foreach (ListViewItem item in listView2.Items)
            {
                query = "INSERT INTO orders (id_product, id_client, address, date, amount, id_OfOneOrder) values ( (select id from products" +
                               " where name = '" + item.SubItems[0].Text + "'), " + userIdG + ", '" + textBox3.Text + "', " + "CURRENT_DATE(), " +
                               item.SubItems[3].Text + ", (select max(number) from orderNum)" + ")";

                command = new MySqlCommand(query, conn);
                command.ExecuteNonQuery();
            }

            listView2.Items.Clear();
            label1.Text = "0";
        }

        private void AppClose(object sender, FormClosingEventArgs e)
        {

            enterFormG.Close();

        }
    }
}
