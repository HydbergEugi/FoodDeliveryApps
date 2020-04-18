using System;

using System.Data;

using System.Windows.Forms;

using MySql.Data.MySqlClient;


namespace WindowsFormsApp2
{
    public partial class ProductInfo : Form
    {
        public MainForm mainFormG;
        public ProductInfo(ListView.SelectedListViewItemCollection listRow, MainForm mainForm)
        {
            InitializeComponent();
            mainFormG = mainForm;
            label1.Text = listRow[0].SubItems[0].Text;
            label2.Text = listRow[0].SubItems[1].Text;
            label3.Text = listRow[0].SubItems[2].Text;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] itemRow = { label1.Text, label2.Text, label3.Text, Convert.ToString(numericUpDown1.Value) };
            ListViewItem items = new ListViewItem(itemRow);
            mainFormG.fillOrderList(items);
            this.Dispose();
        }
    }
}
