using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;

namespace CourseApp
{
    public partial class Form3 : Form
    {

        private string code;
        private string email;

        public Form3()
        {
            InitializeComponent();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            string userEmail = textBox5.Text; // 获取用户输入的邮箱地址
            email = textBox5.Text.Trim();
            string verificationCode = GenerateVerificationCode(); // 生成验证码
            // 发送验证码到用户邮箱
            SendVerificationCode(userEmail, verificationCode);
        }

        // 验证码随机生成
        private string GenerateVerificationCode()
        {
            // 生成一个六位数的随机验证码
            Random random = new Random();
            code = random.Next(000000, 999999).ToString();
            return code;
        }

        // 发送验证码至邮箱
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
                    Credentials = new NetworkCredential(fromEmail, fromPassword), // 邮箱认证
                    EnableSsl = true,  // 启用 SSL 加密
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

                // 提示用户验证码已发送
                MessageBox.Show("验证码已发送到您的邮箱！");
            }
            catch (Exception ex)
            {
                // 如果发生错误，显示错误信息
                // MessageBox.Show($"发送验证码失败: {ex.Message}");
                MessageBox.Show($"请输入合法邮箱地址。");
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool RegisterStudent(string studentId, string studentName, string email, string department, string enrollYear, string password)
        {
            string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";

            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();

                string insertQuery = @"
            INSERT INTO student 
            (student_id, name, email, department, enroll_year, password) 
            VALUES 
            (:student_id, :student_name, :email, :department, :enroll_year, :password)";

                using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter(":student_id", studentId));
                    cmd.Parameters.Add(new OracleParameter(":name", studentName));
                    cmd.Parameters.Add(new OracleParameter(":email", email));
                    cmd.Parameters.Add(new OracleParameter(":department", department));
                    cmd.Parameters.Add(new OracleParameter(":enroll_year", enrollYear));
                    cmd.Parameters.Add(new OracleParameter(":password", password));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        private bool CheckStudentId(string studentId)
        {
            string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";

            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM student WHERE student_id = :student_id";

                using (OracleCommand cmd = new OracleCommand(checkQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter(":student_id", studentId));
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;  // true = 已存在，false = 不存在
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


        private void button1_Click(object sender, EventArgs e)
        {
            string studentId = textBox1.Text.Trim();       // 学号
            string name = textBox2.Text.Trim();            // 姓名
            string department = textBox3.Text.Trim();      // 学院
            string enrollYear = textBox4.Text.Trim();  // 入学年份
            string password = textBox7.Text;               // 密码
            string confirmPassword = textBox8.Text;        // 确认密码
            email = textBox5.Text;
            string inputCode = textBox6.Text;

            code = "111111";

            // 检查学号格式（必须是6位数字）
            if (!Regex.IsMatch(studentId, @"^\d{8}$"))
            {
                MessageBox.Show("学号不合法！");
                return;
            }

            if (CheckStudentId(studentId))
            {
                MessageBox.Show("学号已存在，无法注册！");
                return;
            }

            // 检查姓名是否为空
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("姓名不能为空！");
                return;
            }

            // 检查学院是否为空
            if (string.IsNullOrEmpty(department))
            {
                MessageBox.Show("学院不能为空！");
                return;
            }

            // 检查入学年份格式（必须是6位数字）
            if (!Regex.IsMatch(enrollYear, @"^\d{4}$"))
            {
                MessageBox.Show("入学年份不合法！");
                return;
            }


            if (CheckEmail(email))
            {
                MessageBox.Show("该邮箱已被注册！");
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

            // 检查密码格式（只能是英文和数字）
            if (!Regex.IsMatch(password, @"^[A-Za-z0-9]+$"))
            {
                MessageBox.Show("密码只能包含英文和数字！");
                return;
            }

            // 检查两次密码是否一致
            if (password != confirmPassword)
            {
                MessageBox.Show("两次输入的密码不一致！");
                return;
            }

            // 可以添加是否已存在的检查，比如：
            // if (IsStudentIdExists(studentId))
            // {
            //     MessageBox.Show("该学号已被注册！");
            //     return;
            // }

            // 插入到数据库
            if (RegisterStudent(studentId, name, email, department, enrollYear, password))
            {
                MessageBox.Show("学生注册成功！");
            }
            else
            {
                MessageBox.Show("注册失败，请稍后再试！");
            }
        }
    }
}
