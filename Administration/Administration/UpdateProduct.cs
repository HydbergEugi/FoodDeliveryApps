using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Administration
{
    
    public partial class UpdateProduct : Form
    {
        public MainForm mainFormG;
        public DataGridViewRow d1;
        public UpdateProduct(DataGridViewRow d, MainForm mainForm)
        {
            InitializeComponent();
            mainFormG = mainForm;
            d1 = d;
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

            comboBox1.SelectedValue = Convert.ToInt32(d.Cells["type"].Value);
            textBox1.Text = Convert.ToString(d.Cells["name"].Value);
            textBox2.Text = Convert.ToString(d.Cells["cost"].Value);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = mainFormG.createConnection();

            string query1 = "update products set name = '" + textBox1.Text + "' where id = " + Convert.ToInt32(d1.Cells["id"].Value);
            string query2 = "update products set type = " + comboBox1.SelectedValue + " where id = " + Convert.ToInt32(d1.Cells["id"].Value);
            string query3 = "update products set cost = " + textBox2.Text + " where id = " + Convert.ToInt32(d1.Cells["id"].Value);

            MySqlCommand command = new MySqlCommand(query1, conn);
            command.ExecuteNonQuery();
            command = new MySqlCommand(query2, conn);
            command.ExecuteNonQuery();
            command = new MySqlCommand(query3, conn);
            command.ExecuteNonQuery();

            mainFormG.fillProductList();
            this.Dispose();
        }

    }
}
