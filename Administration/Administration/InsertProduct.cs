using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Administration
{
    public partial class InsertProduct : Form
    {
        public MainForm mainFormG;
        public InsertProduct(MainForm mainForm)
        {
            InitializeComponent();
            mainFormG = mainForm;
            MySqlConnection conn = mainForm.createConnection();

            string query1 = "select * from product_type;";
            MySqlCommand command = new MySqlCommand(query1, conn);
            MySqlDataAdapter adapt = new MySqlDataAdapter(command);
            DataSet dt1 = new DataSet();
            adapt.Fill(dt1);

            comboBox1.DataSource = dt1.Tables[0].DefaultView;
            comboBox1.Name = "type_product";
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MySqlConnection mySQLConn = mainFormG.createConnection();
            string query = "INSERT INTO products (name, type, cost) values ('" + textBox1.Text + "', " + Convert.ToInt32(comboBox1.SelectedValue) + ", " + textBox2.Text + ")";
            MySqlCommand command = new MySqlCommand(query, mySQLConn);
            command.ExecuteReader();
            mainFormG.fillProductList();
            this.Dispose();
        }
    }
}
