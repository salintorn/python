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
    public partial class Login1 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Login1()
        {
            InitializeComponent();
        }
   
        private void button1_Click(object sender, EventArgs e)  //ปุ่มlogin
        {
            MySqlConnection conn = databaseConnection();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM user WHERE username = \"{textBox1.Text}\" AND password = \"{textBox2.Text}\"";

            MySqlDataReader row = cmd.ExecuteReader();
            if (row.HasRows)
            {
                MessageBox.Show("เข้าสู่ระบบสำเร็จ"); //กรอกข้อมูลถูกต้อง
                Program.username = textBox1.Text;
                MySqlConnection con2 = databaseConnection();
                con2.Open();
                MySqlCommand cmd2 = con2.CreateCommand();
                cmd2.CommandText = $"SELECT status FROM user WHERE username = \"{textBox1.Text}\"";
                MySqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    string status = dr.GetValue(0).ToString();
                    if (status == "admin")
                    {
                        stock2 a = new stock2();
                        this.Hide();
                        a.Show();
                    }
                    else
                    {
                        Order3 a = new Order3();
                        this.Hide();
                        a.Show();
                    }
                }
                else
                {
                    MessageBox.Show("ชื่อผู้ใช้ หรือ รหัสผ่านไม่ถูกต้อง"); //กรอกข้อมูลไม่ถูกต้อง
                }
                conn.Close();
            }
            
        }

        
        private void button2_Click_3(object sender, EventArgs e)//ปุ่มสมัครเข้าใช้งาน
        {
            this.Hide();
            signup4 f = new signup4();
            f.Show();

        }
    }
}
