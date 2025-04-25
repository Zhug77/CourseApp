namespace CourseApp
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label3 = new Label();
            textBox4 = new TextBox();
            label4 = new Label();
            button0 = new Button();
            button1 = new Button();
            button2 = new Button();
            label5 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(185, 60);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(237, 27);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 104);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 1;
            label1.Text = "请输入邮箱：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 60);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 2;
            label2.Text = "请输入用户名：";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(185, 104);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(237, 27);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(185, 150);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(237, 27);
            textBox3.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 150);
            label3.Name = "label3";
            label3.Size = new Size(114, 20);
            label3.TabIndex = 4;
            label3.Text = "请输入验证码：";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(185, 195);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(237, 27);
            textBox4.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 195);
            label4.Name = "label4";
            label4.Size = new Size(99, 20);
            label4.TabIndex = 6;
            label4.Text = "请输入密码：";
            // 
            // button0
            // 
            button0.Location = new Point(439, 103);
            button0.Name = "button0";
            button0.Size = new Size(94, 29);
            button0.TabIndex = 8;
            button0.Text = "发送验证码";
            button0.UseVisualStyleBackColor = true;
            button0.Click += button0_Click;
            // 
            // button1
            // 
            button1.Location = new Point(117, 335);
            button1.Name = "button1";
            button1.Size = new Size(115, 42);
            button1.TabIndex = 10;
            button1.Text = "确认注册";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(307, 335);
            button2.Name = "button2";
            button2.Size = new Size(115, 42);
            button2.TabIndex = 11;
            button2.Text = "返回";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(46, 240);
            label5.Name = "label5";
            label5.Size = new Size(129, 20);
            label5.TabIndex = 1;
            label5.Text = "请再次输入密码：";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(185, 240);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(237, 27);
            textBox5.TabIndex = 7;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(185, 288);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(237, 27);
            textBox6.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(46, 288);
            label6.Name = "label6";
            label6.Size = new Size(114, 20);
            label6.TabIndex = 12;
            label6.Text = "管理员认证码：";
            label6.Click += label6_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 430);
            Controls.Add(textBox6);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(button0);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox5);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "管理员注册";
            Text = "管理员注册";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private Button button0;
        private Button button1;
        private Button button2;
        private Label label5;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label6;
    }
}