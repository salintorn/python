using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
namespace ร้านผลไม้
{
    public partial class stock2 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        
        private void showstock() //รายละเอียดสินค้า
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM stock";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        public stock2()
        {
            InitializeComponent();
        }
        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login1 to = new Login1();
            to.Show();
        }
        
        private void stockToolStripMenuItem_Click(object sender, EventArgs e)//ปุ่มhomepageหน้าขายสินค้า
        {
            this.Hide();
            Order3 to = new Order3();
            to.Show();
        }
        //ปุ่มLogout 
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login1 to = new Login1();
            to.Show();
        }
        //ปุ่มexitออกจากโปรแกรม
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            showstock();
        }
        private void button1_Click(object sender, EventArgs e)//ปุ่มเพิ่มข้อมูล
        {
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connection);
            byte[] image = null;
            pictureBox2.ImageLocation = textBox5.Text;
            string filepath = textBox5.Text;
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image = br.ReadBytes((int)fs.Length);
            string sql = $" INSERT INTO stock (sushilist,type,price,Image) VALUES(\"{ textBox1.Text}\",\"{ textBox2.Text}\",\"{ textBox3.Text}\",@Imgg)";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@Imgg", image));
                int x = cmd.ExecuteNonQuery();
                conn.Close();
                showstock();
            }

        }  
        private void button4_Click(object sender, EventArgs e)//ปุ่มเพิ่มรูปภาพ
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(open.FileName);
                textBox5.Text = open.FileName;
            }
        }  
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //กดเลือกสินค้า
        {
            dataGridView1.CurrentRow.Selected = true;

            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            int Image = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
            //textBox1.Text = ShowImaget.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["sushilist"].FormattedValue.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["type"].FormattedValue.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();

            MySqlConnection conn = databaseConnection();
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = ($"SELECT Image FROM stock WHERE id =\"{ Image}\"");
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Image"]);
                pictureBox2.Image = new Bitmap(ms);
            }

        }
        private void button3_Click(object sender, EventArgs e)//ปุ่มลบข้อมูล
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "DELETE FROM stock WHERE id = '" + deleteId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("ข้อมูลถูกลบแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                showstock();
            }
        }    
        private void button2_Click(object sender, EventArgs e) //ปุ่มแก้ไขข้อมูล
        {
            int selectedRowmm = dataGridView1.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataGridView1.Rows[selectedRowmm].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "UPDATE  stock SET sushilist = '" + textBox1.Text + "',type='" + textBox2.Text + "',price= '" + textBox3.Text + "' WHERE id = '" + editId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
                showstock(); //รายละเอียดสินค้า
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            history to = new history();
            to.Show();
        }
    }
}
