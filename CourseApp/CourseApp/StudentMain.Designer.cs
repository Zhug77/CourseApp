namespace CourseApp
{
    partial class StudentMain
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

        private ComboBox comboBoxTable;
        private DataGridView dataGridView;
        private Button buttonSelectCourse;
        private Button buttonCancelCourse;

        private TextBox textBoxSearch;
        private Button buttonSearch;
        private ComboBox comboBoxSearchType;

        private Label labelStudentIdTitle;
        private Label labelStudentId;

        private Label labelStudentNameTitle;
        private Label labelStudentName;

        private Label labelDepartmentTitle;
        private Label labelDepartment;

        private Label labelEnrollYearTitle;
        private Label labelEnrollYear;




        private string studentDepartment; // 当前学生学院
        private string studentEnrollYear; // 当前学生入学年份
        private string studentId; // 当前学生学号，可根据需要添加

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxTable = new ComboBox();
            dataGridView = new DataGridView();
            buttonSelectCourse = new Button();
            buttonCancelCourse = new Button();
            textBoxSearch = new TextBox();
            buttonSearch = new Button();
            comboBoxSearchType = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // comboBoxTable
            // 
            comboBoxTable.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTable.Items.AddRange(new object[] { "可选课表", "已选课表", "已选未结课课表", "已选已结课课表", "所有课程" });
            comboBoxTable.Location = new Point(218, 12);
            comboBoxTable.Name = "comboBoxTable";
            comboBoxTable.Size = new Size(250, 28);
            comboBoxTable.TabIndex = 0;
            comboBoxTable.SelectedIndexChanged += comboBoxTable_SelectedIndexChanged;
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
            dataGridView.Size = new Size(1462, 796);
            dataGridView.TabIndex = 1;
            // 
            // buttonSelectCourse
            // 
            buttonSelectCourse.Location = new Point(12, 60);
            buttonSelectCourse.Name = "buttonSelectCourse";
            buttonSelectCourse.Size = new Size(160, 60);
            buttonSelectCourse.TabIndex = 2;
            buttonSelectCourse.Text = "选课";
            buttonSelectCourse.Click += buttonSelectCourse_Click;
            // 
            // buttonCancelCourse
            // 
            buttonCancelCourse.Location = new Point(12, 161);
            buttonCancelCourse.Name = "buttonCancelCourse";
            buttonCancelCourse.Size = new Size(160, 60);
            buttonCancelCourse.TabIndex = 3;
            buttonCancelCourse.Text = "取消选课";
            buttonCancelCourse.Click += buttonCancelCourse_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(1390, 11);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.PlaceholderText = "请输入关键词...";
            textBoxSearch.Size = new Size(200, 27);
            textBoxSearch.TabIndex = 0;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(1600, 11);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(80, 29);
            buttonSearch.TabIndex = 0;
            buttonSearch.Text = "搜索";
            buttonSearch.Click += buttonSearch_Click;
            // 
            // comboBoxSearchType
            // 
            comboBoxSearchType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSearchType.Items.AddRange(new object[] { "课程号", "课程名称", "是否可选", "所属学院", "所属入学年份" });
            comboBoxSearchType.Location = new Point(1254, 12);
            comboBoxSearchType.Name = "comboBoxSearchType";
            comboBoxSearchType.Size = new Size(120, 28);
            comboBoxSearchType.TabIndex = 4;

            // 
            // labelStudentIdTitle
            // 
            labelStudentIdTitle = new Label();
            labelStudentIdTitle.Text = "学号：";
            labelStudentIdTitle.Location = new Point(12, 740);
            labelStudentIdTitle.Size = new Size(60, 25);
            Controls.Add(labelStudentIdTitle);

            // 
            // labelStudentId
            // 
            labelStudentId = new Label();
            labelStudentId.Location = new Point(75, 740);
            labelStudentId.Size = new Size(100, 25);
            Controls.Add(labelStudentId);

            // 
            // labelStudentNameTitle
            // 
            labelStudentNameTitle = new Label();
            labelStudentNameTitle.Text = "姓名：";
            labelStudentNameTitle.Location = new Point(12, 775);
            labelStudentNameTitle.Size = new Size(60, 25);
            Controls.Add(labelStudentNameTitle);

            // 
            // labelStudentName
            // 
            labelStudentName = new Label();
            labelStudentName.Location = new Point(75, 775);
            labelStudentName.Size = new Size(100, 25);
            Controls.Add(labelStudentName);

            // 
            // labelDepartmentTitle
            // 
            labelDepartmentTitle = new Label();
            labelDepartmentTitle.Text = "所属学院：";
            labelDepartmentTitle.Location = new Point(12, 810);
            labelDepartmentTitle.Size = new Size(95, 25);
            Controls.Add(labelDepartmentTitle);

            // 
            // labelDepartment
            // 
            labelDepartment = new Label();
            labelDepartment.Location = new Point(105, 810);
            labelDepartment.Size = new Size(100, 25);
            Controls.Add(labelDepartment);

            // 
            // labelEnrollYearTitle
            // 
            labelEnrollYearTitle = new Label();
            labelEnrollYearTitle.Text = "入学年份：";
            labelEnrollYearTitle.Location = new Point(12, 845);
            labelEnrollYearTitle.Size = new Size(95, 25);
            Controls.Add(labelEnrollYearTitle);

            // 
            // labelEnrollYear
            // 
            labelEnrollYear = new Label();
            labelEnrollYear.Location = new Point(105, 845);
            labelEnrollYear.Size = new Size(100, 25);
            Controls.Add(labelEnrollYear);

            // 
            // StudentMain
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1718, 887);
            Controls.Add(comboBoxTable);
            Controls.Add(dataGridView);
            Controls.Add(buttonSelectCourse);
            Controls.Add(buttonCancelCourse);
            Controls.Add(textBoxSearch);
            Controls.Add(buttonSearch);
            Controls.Add(comboBoxSearchType);
            Name = "StudentMain";
            Text = "学生选课系统";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
