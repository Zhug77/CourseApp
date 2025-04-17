namespace CourseApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button LoginButton;
            RegisterButton = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            LoginButton = new Button();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(124, 263);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(115, 42);
            LoginButton.TabIndex = 0;
            LoginButton.Text = "登录";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += button1_Click;
            // 
            // RegisterButton
            // 
            RegisterButton.Location = new Point(295, 263);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(115, 42);
            RegisterButton.TabIndex = 1;
            RegisterButton.Text = "注册";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "邮箱", "用户名" });
            comboBox1.Location = new Point(75, 101);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(89, 28);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedItem = "邮箱"; // 默认选择“邮箱”
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(75, 152);
            label1.Name = "label1";
            label1.Size = new Size(53, 20);
            label1.TabIndex = 5;
            label1.Text = "密码";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(212, 102);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(198, 27);
            textBox1.TabIndex = 6;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(212, 152);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(198, 27);
            textBox2.TabIndex = 7;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(124, 211);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(90, 24);
            radioButton1.TabIndex = 8;
            radioButton1.TabStop = true;
            radioButton1.Text = "学生登录";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(295, 211);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(105, 24);
            radioButton2.TabIndex = 9;
            radioButton2.TabStop = true;
            radioButton2.Text = "管理员登录";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(531, 349);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(RegisterButton);
            Controls.Add(LoginButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoginButton;
        private Button RegisterButton;
        private ComboBox comboBox1;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
    }
}
