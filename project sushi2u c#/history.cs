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

namespace ร้านผลไม้
{
    public partial class history : Form
    {
        List<Bill> allbill = new List<Bill>();
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void showdatasaleuser() //รายการที่สั่ง
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT sushilist,type,price,datetime,username FROM stockshow WHERE status = '" + "PAID" + "' ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        public history()
        {
            InitializeComponent();
        }
        private void homePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order3 to = new Order3();
            to.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            stock2 to = new stock2();
            to.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ออกจากโปรแกรมสำเร็จ");
            Application.Exit();
        }

        private void history_Load(object sender, EventArgs e)
        {

            summm = 0;
            dateTimePicker2.Value = System.DateTime.Now;
            dateTimePicker1.Value = System.DateTime.Now;
            label6.Text = Program.username;
            if (label6.Text == "admin")
            {
                ShowdataproductAdmin();
            }
            else
            {
                showhistoryuser();
            }

            //showdatasaleuser();
            Usernamesale();

        }

        int total;
        int summm;
        private void showhistoryuser() //โชว์ประวัติการขายuser
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT sushilist,type,price,datetime,username FROM stockshow WHERE status = '" + "PAID" + "' AND username = '" + label6.Text + "' ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void ShowdataproductAdmin()
        {
            MySqlConnection conn = databaseConnection();

            DataSet ds = new DataSet();

            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT sushilist,type,price,datetime,username FROM stockshow WHERE status = '" + "PAID" + "' ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void Usernamesale() //รวมราคาทั้งหมด
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT price FROM stockshow ";
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                int pp = read.GetInt32("price");
                summm = summm + pp;
            }
            conn.Close();
            textBox3.Text = Convert.ToString(summm);
        }
        private void button1_Click(object sender, EventArgs e) //ปุ่มค้นหา
        {
            string su = Program.username;
            if (textBox2.Text != "")
            {

                MySqlConnection conn = databaseConnection();

                DataSet ds = new DataSet();

                conn.Open();
                MySqlCommand cmd;

                cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT sushilist,price,datetime,username FROM stockshow WHERE  username=@data OR sushilist=@data  AND datetime between @date1 and @date2  "; //ค้นหาชื่อจากUser,=อาหาร

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand.Parameters.AddWithValue("@date1", dateTimePicker2.Value.ToString("dd/MM/yyyy")); //เอาค่าจาก dateTimePicker ไปเก็บที่ parameters @date1
                da.SelectCommand.Parameters.AddWithValue("@date2", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@data", textBox2.Text);
                da.SelectCommand.Parameters.AddWithValue("@data2", su);

                da.Fill(ds);
                conn.Close();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                total = 0; //ตัวแปรยอดรวมจำนวนเงิน
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    total = total + int.Parse(read.GetString(1));
                }

                textBox4.Text = $"{total}"; //โชว์เงินในเทคบล็อก4
                conn.Close();
            }
            else
            {

                MySqlConnection conn = databaseConnection();

                DataSet ds = new DataSet();

                conn.Open();
                MySqlCommand cmd;

                cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT sushilist,price,datetime,username FROM stockshow WHERE datetime between @date1 and @date2 ";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.SelectCommand.Parameters.AddWithValue("@date1", dateTimePicker2.Value.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@date2", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@data3", su);

                da.Fill(ds);
                conn.Close();
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                total = 0; //ตัวแปรยอดรวมจำนวนเงิน
                conn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {

                    total = total + int.Parse(read.GetString(1));
                }

                textBox4.Text = $"{total}";
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            allbill.Clear();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("รายการสินค้า", new Font("supermarket", 20, FontStyle.Bold), Brushes.Black, new Point(370, 50));
            e.Graphics.DrawString("sushi2u", new Font("supermarket", 24, FontStyle.Bold), Brushes.Black, new Point(130, 90));
            e.Graphics.DrawString("พิมพ์เมื่อ " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new PointF(550, 150));
            e.Graphics.DrawString("ข้อมูลร้าน : ศรินทร์ธร ฝ่ายสูน 0993835340", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 150));
            e.Graphics.DrawString("              7/1 หมู่ที่11 ตำบลเมืองเพีย ", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 195));
            e.Graphics.DrawString("               อำเภอบ้านไผ่ จังหวัดขอนแก่น 40110", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 240));
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------------------------------------", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 285));
            e.Graphics.DrawString("    ลำดับ     ชื่อซูชิ                ประเภท                  วัน/เดือน/ปี          ชื่อผู้ขาย                         ราคา ", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 315));
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------------------------------------", new Font("supermarket", 12, FontStyle.Regular), Brushes.Black, new Point(80, 345));
            int y = 345;
            int number = 1;
            allbill.Clear();
            data();
            foreach (var i in allbill)
            {
                y = y + 35;
                e.Graphics.DrawString("   " + number.ToString(), new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(100, y));
                e.Graphics.DrawString("   " + i.sushilist, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(140, y));
                e.Graphics.DrawString("   " + i.type, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(250, y));
                e.Graphics.DrawString("   " + i.datetime, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(370, y));
                e.Graphics.DrawString("   " + i.username, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(500, y));
                e.Graphics.DrawString("   " + i.price, new Font("supermarket", 10, FontStyle.Regular), Brushes.Black, new PointF(660, y));


                number = number + 1;
            }
            e.Graphics.DrawString("---------------------------------------------------------------------------------------------------------------------------------", new Font("supermarket", 16, FontStyle.Regular), Brushes.Black, new Point(80, y + 30));
            e.Graphics.DrawString("ราคารวม  " + textBox4.Text + "  บาท", new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new Point(570, (y + 30) + 45));
            e.Graphics.DrawString("ชื่อผู้ให้บริการ        " + Program.username.ToString(), new Font("supermarket", 14, FontStyle.Bold), Brushes.Black, new Point(80, (y + 30) + 45));
            e.Graphics.DrawString("ยอดขายทั้งหมด    " + textBox3.Text + "  บาท", new Font("supermarket", 14, FontStyle.Regular), Brushes.Black, new Point(570, ((y + 30) + 45) + 45));
        }
        private void data()
        {
            MySqlConnection conn = databaseConnection();
            string name = textBox2.Text;
            conn.Open();




            //Program.checksearch= name;
            if (name == "")
            {

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM stockshow  ", conn);
                MySqlDataReader adapter = cmd.ExecuteReader();

                while (adapter.Read())
                {
                    Program.sushilist = adapter.GetString("sushilist");
                    Program.type = adapter.GetString("type");
                    Program.datetime = adapter.GetString("datetime");
                    Program.price = adapter.GetString("price");
                    Program.username = adapter.GetString("username");

                    Bill item = new Bill()
                    {
                        sushilist = Program.sushilist,
                        type = Program.type,
                        price = Program.price,
                        username = Program.username,
                        datetime = Program.datetime


                    };
                    allbill.Add(item);
                }
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("SELECT sushilist,type,datetime,price,username FROM stockshow WHERE sushilist='" + name + "' OR type = '" + name + "'OR datetime = '" + name + "' OR price='" + name + "' OR username = '" + name + "'  ", conn); ;
                MySqlDataReader adapter = cmd.ExecuteReader();
                while (adapter.Read())
                {
                    Program.sushilist = adapter.GetString("sushilist");
                    Program.type = adapter.GetString("type");
                    Program.datetime = adapter.GetString("datetime");
                    Program.price = adapter.GetString("price");
                    Program.username = adapter.GetString("username");
                    Bill item = new Bill()
                    {
                        sushilist = Program.sushilist,
                        type = Program.type,
                        price = Program.price,
                        username = Program.username,
                        datetime = Program.datetime
                    };
                    allbill.Add(item);
                }


            }
        }
    }
} 

