using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace CourseApp
{
    public partial class Form2 : Form
    {

        private string code;
        private string email;
        public Form2()
        {
            InitializeComponent();
        }

        private bool IsValid(string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Z0-9]+$");
        }



        private void Form2_Load(object sender, EventArgs e)
        {

        }



        private void button0_Click(object sender, EventArgs e)
        {
            string userEmail = textBox2.Text; // 获取用户输入的邮箱地址
            email = textBox2.Text.Trim();
            string verificationCode = GenerateVerificationCode();
            SendVerificationCode(userEmail, verificationCode);
        }

        
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            code = random.Next(000000, 999999).ToString();
            return code;
        }


        private void SendVerificationCode(string toEmail, string verificationCode)
        {

            if (CheckEmail(toEmail))
            {
                MessageBox.Show("该邮箱已被注册！");
                return;
            }

            try
            {
                // 发件人邮箱和密码
                string fromEmail = "13416367426@163.com"; // 发送方邮箱地址
                string fromPassword = "GFndNubcFf5nnfQ6"; // 授权码

                // 配置 SMTP 客户端
                SmtpClient smtpClient = new SmtpClient("smtp.163.com")
                {
                    Port = 25, // 使用25端口进行发送 测了587 994 465都不行
                    Credentials = new NetworkCredential(fromEmail, fromPassword), 
                    EnableSsl = true,  // SSL加密
                };

                // 创建邮件内容
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = "验证码",
                    Body = $"您的验证码是：{verificationCode}", // 邮件正文
                    IsBodyHtml = false, // 邮件正文是纯文本格式
                };

                // 设置收件人邮箱
                mailMessage.To.Add(toEmail);

                // 发送邮件
                smtpClient.Send(mailMessage);

                
                MessageBox.Show("验证码已发送到您的邮箱！");
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"发送验证码失败: {ex.Message}");
            }
        }

        // 数据库检索是否有相同的
        private bool IsEmailOrUsernameExists(string email, string username)
        {
            string connString = "User Id=course;Password=123456;Data Source=localhost:1521/orcl";

            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM admin WHERE email = :email OR admin_username = :username";
                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(new OracleParameter(":email", email));
                    cmd.Parameters.Add(new OracleParameter(":username", username));

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }


        private bool CheckEmail(string email)
        {
            string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";

            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM student WHERE email = :email";

                using (OracleCommand cmd = new OracleCommand(checkQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter(":student_id", email));
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;  // true = 已存在，false = 不存在
                }
            }
        }

        // 用户信息写入数据库
        private bool RegisterUser(string username, string email, string password)
        {

            string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";

            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO admin (email, admin_username, admin_password) VALUES (:email, :username, :password)";

                using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter(":email", email));
                    cmd.Parameters.Add(new OracleParameter(":username", username));
                    cmd.Parameters.Add(new OracleParameter(":password", password));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            email = textBox2.Text.Trim();
            string password = textBox4.Text;
            string confirmPassword = textBox5.Text;
            string inputCode = textBox3.Text;
            string checkCode = textBox6.Text;

            code = "111111";

            // 验证管理员认证码
            if (checkCode!="123456")
            {
                MessageBox.Show("请输入有效管理员认证码！");
                return;
            }

            // 验证用户名格式
            if (!IsValid(username))
            {
                MessageBox.Show("用户名只能包含英文和数字！");
                return;
            }

            if (CheckEmail(email))
            {
                MessageBox.Show("该邮箱已被注册！");
                return;
            }

            // 验证密码格式
            if (!IsValid(password))
            {
                MessageBox.Show("密码只能包含英文和数字！");
                return;
            }

            // 确保两次密码输入相同
            if (password != confirmPassword)
            {
                MessageBox.Show("两次输入的密码不一致！");
                return;
            }

            if (code == null)
            {
                MessageBox.Show("未发送验证码！");
                return;
            }

            // 检查验证码是否正确
            if (inputCode != code)
            {
                MessageBox.Show("验证码错误！");
                return;
            }

            // 检查邮箱和用户名是否已经存在
            if (IsEmailOrUsernameExists(email, username))
            {
                MessageBox.Show("用户名或邮箱已存在，请更换后再试！");
                return;
            }

            // 插入数据到 Oracle 数据库
            if (RegisterUser(username, email, password))
            {
                MessageBox.Show("注册成功！");
                code = null;
            }
            else
            {
                MessageBox.Show("注册失败，请稍后再试！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            string connString = "User Id=course;Password=123456;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Connection Timeout=30";

            try
            {
                using (OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();
                    MessageBox.Show("Oracle连接成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败: " + ex.Message);
            }
            */
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
