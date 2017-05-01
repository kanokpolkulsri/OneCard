using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OneCard
{
    public partial class frm_Promotion : Form
    {
        OleDbDataAdapter oda;
        DataTable dt;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Tony\\Desktop\\OneCard\\dbOneCard.accdb");

        public frm_Promotion()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frm_Promotion_Load(object sender, EventArgs e)
        {
            oda = new OleDbDataAdapter("SELECT * FROM Promotion", conn);
            dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT Point FROM Customer WHERE CardID = '" + textBox1.Text + "'", conn);
                da.Fill(ds, "Customer");
                foreach (DataRow dr in ds.Tables["Customer"].Rows)
                {
                    textBox2.Text = dr.ItemArray[0] + "";
                    oda = new OleDbDataAdapter("SELECT * FROM Promotion WHERE Point <= "+textBox2.Text+"", conn);
                    dt = new DataTable();
                    oda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    break;
                }
            }
            else
            {
                textBox2.Clear();
                frm_Promotion_Load(sender, e);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                conn.Open();
                OleDbCommand checkProId = new OleDbCommand();
                checkProId.Connection = conn;
                checkProId.CommandText = "SELECT Promotion.Money FROM Promotion WHERE PromotionID = '" + textBox3.Text + "'";
                OleDbDataReader readerCheckProID = checkProId.ExecuteReader();
                int count = 0;
                while (readerCheckProID.Read())
                {
                    count = count + 1;
                    if (count == 1)
                    {
                        break;
                    }
                }
                conn.Close();
                if (count == 1)
                {
                    conn.Open();
                    OleDbCommand update = new OleDbCommand();
                    update.Connection = conn;
                    update.CommandText = "UPDATE Customer, Promotion SET Customer.Money = Customer.Money + Promotion.Money , Customer.Point = Customer.Point - Promotion.Point WHERE Customer.CardID = '" + textBox1.Text + "' AND Promotion.PromotionID = '" + textBox3.Text + "'";
                    update.ExecuteNonQuery();
                    conn.Close();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    MessageBox.Show("Completely UPDATE !");
                }
                else
                {
                    MessageBox.Show("ไม่มีรหัส promotion นี้");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 next = new Form1();
            this.Hide();
            next.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_Customer next = new frm_Customer();
            this.Hide();
            next.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CodePoint_Click(object sender, EventArgs e)
        {
            label6.Text = "UPDATE Customer, Promotion \nSET\nCustomer.Money = Customer.Money + Promotion.Money , \nCustomer.Point = Customer.Point - Promotion.Point \nWHERE\nCustomer.CardID = 'CardID-text' AND \nPromotion.PromotionID = 'ProID-text'";
            frm_Promotion_Load(sender, e);
        }

        private void CodeHide_Click(object sender, EventArgs e)
        {
            label6.Text = "";
            frm_Promotion_Load(sender, e);
        }
    }
}
