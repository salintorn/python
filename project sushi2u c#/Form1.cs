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
using System.Text.RegularExpressions;

namespace ร้านผลไม้
{
    public partial class signup4 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public signup4()
        {
            InitializeComponent();
        }
        public Boolean checkuser()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=data;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            string username = textBox3.Text;
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE username = @user", conn);

            command.Parameters.Add("@user", MySqlDbType.VarChar).Value = username;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Register()
        {
            string txtusername = textBox3.Text; //กำหนัดตัวแปร
            string txtpassword = textBox4.Text;
            string txtname = textBox1.Text;
            string txtphon = textBox2.Text;
            string txtaddress = textBox5.Text;
            string txtemail = textBoxemail.Text;


            MySqlConnection conn = databaseConnection();
            MySqlCommand command = new MySqlCommand("INSERT INTO `user`(`username`,`password`,`name`,`phon`,`address`,`email`) VALUES (@username,@password,@name,@phon,@address,@email)", conn);
            command.Parameters.Add("@username", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@phon", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@address", MySqlDbType.VarChar).Value = textBox5.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBoxemail.Text;
            conn.Open();

            if (textBox4.Text.Equals(textBox6.Text))
            {
                if (textBox3.Text == "" || textBox4.Text == ""  || textBox1.Text == "" || textBox2.Text == "" || textBox5.Text == "" || textBoxemail.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("กรุณากรอกให้ครบ!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (txtusername.Length < 6 || txtpassword.Length < 6)
                {
                    MessageBox.Show("กรุณากรอก username,password 6-20 ตัว", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtphon.Length < 10)
                {
                    MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ห้ครบ 10 ตัว", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtphon.Length > 10)
                {
                    MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ไม่เกิน 10 ตัว", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (checkuser())
                {
                    MessageBox.Show("มีบัญชีผู้ใช้นี้อยู่แล้ว โปรดใช้'Username'อื่น!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("สมัครสมาชิกสำเร็จ");

                    }

                }
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ตรงกัน!", "รหัสผ่านไม่ตรงกัน!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           
        }
        private void button1_Click(object sender, EventArgs e)//บันทึกข้อมูลการสมัคร
        {
            Register();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) //กรอกเบอร์
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
                MessageBox.Show("กรุณากรอกตัวเลขเท่านั่น", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e) //กรอกuser
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
                MessageBox.Show("กรุณากรอกภาษาอังกฤษหรือตัวเลข", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if ((e.KeyChar == ' '))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e) //กรอกpassword
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }  
        }
        private bool validateURL()//เช็คอีเมล
        {

            Regex urlCheck = new Regex("^[a-zA-Z0-9/@/./]+(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$");

            if (urlCheck.IsMatch(textBoxemail.Text))
            {
                return true;
            }
            else
            {
                return false;
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login1 to = new Login1();
            to.Show();
        }
    }
}
