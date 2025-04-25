using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CourseApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
            radioButton1.Checked = true;
            comboBox1.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBoxShowPassword.Checked ? '\0' : '*';
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string accountType = comboBox1.SelectedItem?.ToString();
            string input = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(accountType) || string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入账号和密码，并选择登录方式。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";

            if (radioButton2.Checked) // 管理员
            {
                if (accountType == "学号")
                {
                    MessageBox.Show("管理员不能使用学号登录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = "";
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    conn.Open();

                    if (accountType == "邮箱")
                    {
                        sql = "SELECT * FROM admin WHERE email = :input AND admin_password = :password";
                    }
                    else if (accountType == "用户名")
                    {
                        sql = "SELECT * FROM admin WHERE admin_username = :input AND admin_password = :password";
                    }
                    else
                    {
                        MessageBox.Show("请选择正确的登录方式。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(":input", OracleDbType.Varchar2).Value = input;
                        cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show("管理员登录成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AdminMain adminMain = new AdminMain();
                                adminMain.Show();
                                // this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("账号或密码错误，请重试。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else if (radioButton1.Checked) // 学生
            {
                string sql = "";
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    conn.Open();

                    if (accountType == "邮箱")
                    {
                        sql = "SELECT student_id, department, enroll_year FROM student WHERE email = :input AND password = :password";
                    }
                    else if (accountType == "学号")
                    {
                        sql = "SELECT student_id, department, enroll_year FROM student WHERE student_id = :input AND password = :password";
                    }
                    else
                    {
                        MessageBox.Show("学生只能使用邮箱或学号登录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(":input", OracleDbType.Varchar2).Value = input;
                        cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentId = reader.GetString(0);
                                studentDepartment = reader.GetString(1);
                                studentEnrollYear = reader.GetString(2);

                                MessageBox.Show("欢迎学号 " + studentId + " 的学生登录", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                StudentMain stu = new StudentMain(studentId, studentDepartment, studentEnrollYear);
                                stu.Show();
                                // this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("账号或密码错误，请重试。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                Form2 registerForm = new Form2(); // 创建 Form2 窗口实例
                registerForm.Show(); // 显示注册窗口
            }
            else if(radioButton1.Checked)
            {
                Form3 registerForm = new Form3(); 
                registerForm.Show(); // 显示注册窗口
                /*string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    conn.Open();
                    string sql = @"SELECT student_id, department, enroll_year 
                       FROM student 
                       WHERE email = :email";
                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(":email", OracleDbType.Varchar2).Value = textBox1.Text.Trim();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentId = reader.GetString(0);
                                studentDepartment = reader.GetString(1);
                                studentEnrollYear = reader.GetString(2);
                                
                            }
                            else
                            {
                                MessageBox.Show("数据获取失败...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
                MessageBox.Show("欢迎学号"+ studentId+"学生登录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StudentMain stu = new StudentMain(studentId,studentDepartment, studentEnrollYear);
                stu.Show();*/
            }
            
            


        }

        private void search_info()
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
