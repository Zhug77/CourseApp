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
                MessageBox.Show("�������˺ź����룬��ѡ���¼��ʽ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";

            if (radioButton2.Checked) // ����Ա
            {
                if (accountType == "ѧ��")
                {
                    MessageBox.Show("����Ա����ʹ��ѧ�ŵ�¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = "";
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    conn.Open();

                    if (accountType == "����")
                    {
                        sql = "SELECT * FROM admin WHERE email = :input AND admin_password = :password";
                    }
                    else if (accountType == "�û���")
                    {
                        sql = "SELECT * FROM admin WHERE admin_username = :input AND admin_password = :password";
                    }
                    else
                    {
                        MessageBox.Show("��ѡ����ȷ�ĵ�¼��ʽ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                MessageBox.Show("����Ա��¼�ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AdminMain adminMain = new AdminMain();
                                adminMain.Show();
                                // this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("�˺Ż�������������ԡ�", "��¼ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else if (radioButton1.Checked) // ѧ��
            {
                string sql = "";
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    conn.Open();

                    if (accountType == "����")
                    {
                        sql = "SELECT student_id, department, enroll_year FROM student WHERE email = :input AND password = :password";
                    }
                    else if (accountType == "ѧ��")
                    {
                        sql = "SELECT student_id, department, enroll_year FROM student WHERE student_id = :input AND password = :password";
                    }
                    else
                    {
                        MessageBox.Show("ѧ��ֻ��ʹ�������ѧ�ŵ�¼��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                                MessageBox.Show("��ӭѧ�� " + studentId + " ��ѧ����¼", "��¼�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                StudentMain stu = new StudentMain(studentId, studentDepartment, studentEnrollYear);
                                stu.Show();
                                // this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("�˺Ż�������������ԡ�", "��¼ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Form2 registerForm = new Form2(); // ���� Form2 ����ʵ��
                registerForm.Show(); // ��ʾע�ᴰ��
            }
            else if(radioButton1.Checked)
            {
                Form3 registerForm = new Form3(); 
                registerForm.Show(); // ��ʾע�ᴰ��
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
                                MessageBox.Show("���ݻ�ȡʧ��...", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }
                MessageBox.Show("��ӭѧ��"+ studentId+"ѧ����¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
