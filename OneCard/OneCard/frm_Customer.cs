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
    public partial class frm_Customer : Form
    {

        OleDbDataAdapter oda;
        DataTable dt;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Tony\\Desktop\\OneCard\\dbOneCard.accdb");

        public frm_Customer()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frm_Customer_Load(object sender, EventArgs e)
        {
            oda = new OleDbDataAdapter("SELECT * FROM Customer", conn);
            dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                oda = new OleDbDataAdapter("SELECT * FROM Customer WHERE CardID like '" + textBox1.Text + "%'", conn);
                dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                frm_Customer_Load(sender, e);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                oda = new OleDbDataAdapter("SELECT * FROM Customer WHERE (FirstName like '%" + textBox2.Text + "%') OR (LastName like '%" + textBox2.Text + "%')", conn);
                dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                frm_Customer_Load(sender, e);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand checkInsertOrUpdate = new OleDbCommand();
            checkInsertOrUpdate.Connection = conn;
            checkInsertOrUpdate.CommandText = "SELECT * FROM Customer WHERE CardID = '"+textBox3.Text+"'";
            OleDbDataReader readerCheckInsertOrUpdate = checkInsertOrUpdate.ExecuteReader();
            int count = 0;
            while (readerCheckInsertOrUpdate.Read())
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
                Console.WriteLine(textBox6.Text);
                update.CommandText = "UPDATE Customer SET Customer.Money = '" + textBox6.Text + "' , FirstName = '" + textBox4.Text+"' , LastName = '"+textBox5.Text+"' WHERE CardID = '"+textBox3.Text+"'";
                update.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Completely UPDATE !");
            }
            else
            {
                if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "")
                {
                    conn.Open();
                    OleDbCommand insert = new OleDbCommand();
                    insert.Connection = conn;
                    insert.CommandText = "INSERT INTO Customer (CardID, FirstName, LastName, Type, Faculty, Point, Valid) VALUES ('"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"','"+textBox7.Text+"','"+textBox8.Text+"','"+textBox9.Text+"','"+textBox10.Text+"')";
                    insert.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Completely INSERT !");
                }
                else
                {
                    MessageBox.Show("กรอกข้อมูลไม่ครบ");
                }
            }
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            frm_Customer_Load(sender, e);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand checkInsertOrUpdate = new OleDbCommand();
            checkInsertOrUpdate.Connection = conn;
            checkInsertOrUpdate.CommandText = "SELECT * FROM Customer WHERE CardID = '"+textBox3.Text+"'";
            OleDbDataReader readerCheckInsertOrUpdate = checkInsertOrUpdate.ExecuteReader();
            int count = 0;
            while (readerCheckInsertOrUpdate.Read())
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
                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT CardID, FirstName, LastName, Money, Type, Faculty, Point, Valid FROM Customer WHERE CardID = '"+textBox3.Text+"'", conn);
                da.Fill(ds, "Customer");
                foreach (DataRow dr in ds.Tables["Customer"].Rows)
                {
                    textBox3.Text = dr.ItemArray[0] + "";
                    textBox4.Text = dr.ItemArray[1] + "";
                    textBox5.Text = dr.ItemArray[2] + "";
                    textBox6.Text = dr.ItemArray[3] + "";
                    textBox7.Text = dr.ItemArray[4] + "";
                    textBox8.Text = dr.ItemArray[5] + "";
                    textBox9.Text = dr.ItemArray[6] + "";
                    textBox10.Text = dr.ItemArray[7] + "";
                    break;
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand delete = new OleDbCommand();
            delete.Connection = conn;
            delete.CommandText = "DELETE * FROM Customer WHERE CardID = '"+textBox11.Text+"'";
            delete.ExecuteNonQuery();
            conn.Close();
            textBox11.Clear();
            MessageBox.Show("Completely DELETE !");
            frm_Customer_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 next = new Form1();
            this.Hide();
            next.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_Promotion next = new frm_Promotion();
            this.Hide();
            next.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CodeHide_Click(object sender, EventArgs e)
        {
            label13.Text = "";
            frm_Customer_Load(sender, e);
        }

        private void CodeAdd_Click(object sender, EventArgs e)
        {
            label13.Text = "INSERT INTO Customer (CardID, FirstName, LastName, Type, Faculty, Point, Valid) \nVALUES ('ID-text' , 'FN-text' , 'LN-text' , 'TYP-text' , 'FAC-text' , 'POI-text' , 'Val-text')";
            frm_Customer_Load(sender, e);
        }

        private void CodeUpdate_Click(object sender, EventArgs e)
        {
            label13.Text = "UPDATE Customer SET Customer.FirstName = 'FN-text' , LastName = 'LN-text' WHERE CardID = 'ID-text'";
            frm_Customer_Load(sender, e);
        }

        private void CodeSearch_Click(object sender, EventArgs e)
        {
            label13.Text = "SELECT * FROM Customer WHERE (FirstName like '%FN-text%') OR (LastName like '%LN-text%')";
            frm_Customer_Load(sender, e);
        }

        private void CodeDelete_Click(object sender, EventArgs e)
        {
            label13.Text = "DELETE * FROM Customer WHERE CardID = 'ID-text'";
            frm_Customer_Load(sender, e);
        }
    }
}
