namespace CourseApp
{
    partial class AdminMain
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
        private ComboBox comboBoxTable;
        private DataGridView dataGridView;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private bool isAdding = false;
        private bool isEditing = false;
        private int editingRowIndex = -1;


        private void InitializeComponent()
        {
            comboBoxTable = new ComboBox();
            dataGridView = new DataGridView();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            cancelButton = new Button();
            saveButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // comboBoxTable
            // 
            comboBoxTable.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTable.Items.AddRange(new object[] { "课程信息（course_admin）", "选课记录（course_studentchosen）" });
            comboBoxTable.Location = new Point(218, 12);
            comboBoxTable.Name = "comboBoxTable";
            comboBoxTable.Size = new Size(250, 28);
            comboBoxTable.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ColumnHeadersHeight = 29;
            dataGridView.Location = new Point(218, 60);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(1156, 648);
            dataGridView.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(12, 60);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(160, 60);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "添加";
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(12, 161);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(160, 60);
            buttonEdit.TabIndex = 3;
            buttonEdit.Text = "修改";
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(12, 267);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(160, 60);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "删除";
            buttonDelete.Click += buttonDelete_Click;
            // 
            // cancelButton
            // 
            cancelButton.Enabled = false;
            cancelButton.Location = new Point(1254, 723);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(120, 40);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "取消";
            cancelButton.Click += cancelButton_Click;
            // 
            // saveButton
            // 
            saveButton.Enabled = false;
            saveButton.Location = new Point(1117, 723);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(120, 40);
            saveButton.TabIndex = 6;
            saveButton.Text = "确认保存";
            saveButton.Click += saveButton_Click;
            // 
            // AdminMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 789);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            Controls.Add(comboBoxTable);
            Controls.Add(dataGridView);
            Controls.Add(buttonAdd);
            Controls.Add(buttonEdit);
            Controls.Add(buttonDelete);
            Name = "AdminMain";
            Text = "管理员课程管理";
            Load += AdminMain_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }


        #endregion

        private Button cancelButton;
        private Button saveButton;
    }
}