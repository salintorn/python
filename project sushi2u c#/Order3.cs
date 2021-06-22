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
    public partial class Order3 : Form
    {
        List<Bill> allbill = new List<Bill>();
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        private void showstock() //รายการสินค้า
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
        private void showstocks() //รายการที่สั่ง
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id,sushilist,type,price FROM stockshow WHERE status = '" + "NOTPAID" + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
        }
        public Order3()
        {
            InitializeComponent();
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("ออกจากโปรแกรมสำเร็จ");
            Application.Exit();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            showstock();
            showstocks();
            money();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //กดเลือกสินค้า
        {
            dataGridView1.CurrentRow.Selected = true;
            int selectedRows = dataGridView1.CurrentCell.RowIndex;
            int Image = Convert.ToInt32(dataGridView1.Rows[selectedRows].Cells["id"].Value);
            textBoxname.Text = dataGridView1.Rows[e.RowIndex].Cells["sushilist"].FormattedValue.ToString();
            textBoxprice.Text = dataGridView1.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();
            textBoxtype.Text = dataGridView1.Rows[e.RowIndex].Cells["type"].FormattedValue.ToString();

            MySqlConnection conn = databaseConnection();
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = ($"SELECT Image FROM stock WHERE id =\"{Image}\"");
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Image"]);
                pictureBox2.Image = new Bitmap(ms);
            }
        }

        private void money() //ชำระเงิน
        {
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connection);
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT SUM(price) FROM stockshow WHERE status = '"+ "NOTPAID" +"' ";
            Object sum = cmd.ExecuteScalar();
            conn.Close();
            if (Convert.ToString(sum) != "")
            {
                textBox2.Text = Convert.ToString(sum); //รวมเงิน
            }
        }
        private void button3_Click(object sender, EventArgs e)//ปุ่มสั่งสินค้า
        {
            MySqlConnection conn = databaseConnection();
            string sql = $"INSERT INTO stockshow (sushilist,type,price,status,username,datetime) VALUES(\"{textBoxname.Text}\",\"{ textBoxtype.Text}\",\"{ textBoxprice.Text}\",\"{ "NOTPAID"}\",\"{ Program.username}\",\"{DateTime.Now.ToString("dd/MM/yyyy")}\")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();

            int row = cmd.ExecuteNonQuery();
            if (row > 0)
            {
                //MessageBox.Show("สั่งรายการสินค้าเรียบร้อยแล้วนะคะ");
                showstocks(); //รายการที่สั่ง
                money(); //ชำระเงิน
            }

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e) //กดเลือกรายการที่สั่ง
        {
            dataGridView2.CurrentRow.Selected = true;
            textBoxname.Text = dataGridView2.Rows[e.RowIndex].Cells["sushilist"].FormattedValue.ToString();
            textBoxprice.Text = dataGridView2.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();
            textBoxtype.Text = dataGridView2.Rows[e.RowIndex].Cells["type"].FormattedValue.ToString();
        }
        private void button1_Click(object sender, EventArgs e)//ปุ่มคิดเงิน
        {
            int A = int.Parse(textBox2.Text); //รวมเงิน
            int B = int.Parse(textBox3.Text); //รับเงิน
            string time = DateTime.Now.ToString("dd/MM/yyyy");

            if (textBox2.Text == "0" || textBox2.Text == "") //รวมเงิน
            {
                MessageBox.Show("กรุณาสั่งสินค้าด้วยนะคะ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox3.Text == "0" || textBox3.Text == "") //รับเงิน
            {
                MessageBox.Show("กรุณาจ่ายเงินด้วยนะคะ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (B < A)
                {
                    MessageBox.Show("กรุณาจ่ายเงินให้ครบด้วยนะคะ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int C = B - A; //เงินทอน
                    textBox4.Text = C.ToString();

                    showstocks(); /////////////// รายการที่สั่ง
                    allbill.Clear();
                    MySqlConnection conn2 = databaseConnection();
                    MySqlCommand cmd1 = new MySqlCommand($"SELECT * FROM stockshow WHERE username ='" + Program.username + "'AND status = '" + "NOTPAID" + "'", conn2);
                    conn2.Open();
                    MySqlDataReader adapter = cmd1.ExecuteReader();
                    //Program.sum = 0;
                    while (adapter.Read())
                    {
                        Program.sushilist = adapter.GetString("sushilist").ToString();
                        Program.price = adapter.GetString("price").ToString();
                        Program.type = adapter.GetString("type").ToString();
                        Bill item = new Bill()
                        {
                            sushilist = Program.sushilist,
                            price = Program.price,
                            type = Program.type,
                        };
                        allbill.Add(item);
                    }
                    


                    MySqlConnection conn = databaseConnection();
                    String sql = "UPDATE stockshow SET status = '" + "PAID" + "' ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("ขอบคุณที่ใช้บริการค่ะ");
                        printPreviewDialog1.Document = printDocument1;
                        printPreviewDialog1.ShowDialog();
                        showstocks();
                        money();
                    }

                }
            }

        } 
        private void button4_Click_1(object sender, EventArgs e)//ปุ่มลบสินค้า
        {
            if (textBox2.Text == "0") //รวมเงิน
            {
                MessageBox.Show("กรุณาสั่งสินค้าด้วยนะคะ");
            }
            else
            {
                int selectedRow = dataGridView2.CurrentCell.RowIndex;
                int deleteid = Convert.ToInt32(dataGridView2.Rows[selectedRow].Cells["id"].Value);
                MySqlConnection conn = databaseConnection();
                String sql = "DELETE FROM stockshow WHERE id = '" + deleteid + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                {
                    MessageBox.Show("ลบข้อมูลสำเร็จ");
                    showstocks();
                    money();
                }
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//ใบเสร็จ
        {
            e.Graphics.DrawString("ใบเสร็จ", new Font("supermarket", 20, FontStyle.Bold), Brushes.Black, new Point(370, 50));
            e.Graphics.DrawString("sushi2u", new Font("supermarket", 24, FontStyle.Bold), Brushes.Black, new Point(130, 90));
            e.Graphics.DrawString("พิมพ์เมื่อ " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(550, 150));
            e.Graphics.DrawString("ข้อมูลร้าน : ศรินทร์ธร ฝ่ายสูน 0993835340", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            e.Graphics.DrawString("              7/1 หมู่ที่11 ตำบลเมืองเพีย ", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 195));
            e.Graphics.DrawString("               อำเภอบ้านไผ่ จังหวัดขอนแก่น 40110", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 240));
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------------------------------------", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 285));
            e.Graphics.DrawString("    ลำดับ     ชื่อซูชิ           ประเภท                                                                              ราคา ", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 315));
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------------------------------------", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 345));
            int y = 345;
            int number = 1;
            foreach (var i in allbill)
            {
                y = y + 35;
                e.Graphics.DrawString("   " + number.ToString(), new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(100, y));
                e.Graphics.DrawString("   " + i.sushilist, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(140, y));
                e.Graphics.DrawString("   " + i.type, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(220, y));
                e.Graphics.DrawString("   " + i.price, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(630, y));


                number = number + 1;
            }
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, y + 30));
            e.Graphics.DrawString("รวมทั้งสิ้น         " + textBox2.Text + "  บาท", new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new Point(570, (y + 30) + 45));
            e.Graphics.DrawString("ชื่อผู้ให้บริการ        " + Program.username.ToString(), new Font("supermarket", 14, FontStyle.Bold), Brushes.Black, new Point(80, (y + 30) + 45));
            e.Graphics.DrawString("รับเงิน            " + textBox3.Text + "  บาท", new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new Point(570, ((y + 30) + 45) + 45));
            e.Graphics.DrawString("เงินทอน           " + textBox4.Text + "  บาท", new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new Point(570, (((y + 30) + 45) + 45) + 45));
        } // พิมพ์ใบเสร็จ

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            history to = new history();
            to.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login1 to = new Login1();
            to.Show();
        }
    }
}

