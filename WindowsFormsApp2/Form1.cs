using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        //qxivertippiphljd
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-AVGELME\STP;Initial Catalog=root;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();

            bool loginInData = false;

            string login = textBox1.Text;
            string password = textBox2.Text;
            string familiya = textBox3.Text;
            string imya = textBox4.Text;
            string mail = textBox5.Text;


            SqlCommand cmd = new SqlCommand($"select * from users where login = '{login}'", connection);
            dt.Load(cmd.ExecuteReader());

            if (dt.Rows.Count > 0)
            {
                loginInData = true;
            }

            if (!loginInData)
            {
                SqlCommand icmd = new SqlCommand($"insert into users values('{login}', '{password}', '{familiya}', '{imya}', '{mail}')", connection);
                icmd.ExecuteNonQuery();
                MessageBox.Show("Вы успешно зарегистрировались!");

                MailAddress from = new MailAddress("solovov2003@yandex.ru", "Регистрация");
                MailAddress to = new MailAddress(mail);

                MailMessage m = new MailMessage(from, to);
                m.Subject = "Регистрация в системе";
                m.Body = "Вы успешно зарегистрировались в системе";

                SmtpClient smtp = new SmtpClient("smtp.yandex.ru");
                smtp.Credentials = new NetworkCredential("solovov2003@yandex.ru", "xsaitdgqetmkcwsf");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }

            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }

            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();
            
            dataBase.OpenConnection();
            DataTable dm = dataBase.UpdateData("select * from users");
            dataBase.CloseConnection();
        }
    }
}
