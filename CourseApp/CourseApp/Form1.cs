namespace CourseApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
            radioButton1.Checked = true;

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

        private void button1_Click(object sender, EventArgs e)
        {
            AdminMain adminMain = new AdminMain();
            adminMain.Show();
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
                Form3 registerForm = new Form3(); // ���� Form2 ����ʵ��
                registerForm.Show(); // ��ʾע�ᴰ��
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
