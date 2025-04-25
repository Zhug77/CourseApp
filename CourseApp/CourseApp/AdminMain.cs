using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseApp
{
    public partial class AdminMain : Form
    {
        private string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";
        private DataTable currentDataTable;
        private string currentMode = "course_admin";


        public AdminMain()
        {
            InitializeComponent();
            comboBoxSearchType.SelectedIndex = 0;
            comboBoxTable.SelectedIndex = 0;
        }

        private void AdminMain_Load(object sender, EventArgs e)
        {
            LoadCourseAdmin();  // 默认加载课程信息
        }

        /*private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("切换了表格视图");
            //if (comboBoxTable.SelectedIndex == 0)
            //LoadCourseAdmin();
            //else
            //LoadCourseStudentChosen();
        }*/

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSearchType.Items.Clear();
            comboBoxSearchType.Text = "";

            if (comboBoxTable.SelectedIndex == 0)
            {
                // 显示课程信息表
                LoadCourseAdmin();
                currentMode = "course_admin";
                comboBoxSearchType.Items.AddRange(new object[] { "课程号", "课程名称", "是否可选", "所属学院", "所属入学年份" });
                comboBoxSearchType.SelectedIndex = 0;
            }
            else if (comboBoxTable.SelectedIndex == 1)
            {
                // 显示选课记录表
                LoadCourseStudentChosen();
                currentMode = "course_studentchosen";
                comboBoxSearchType.Items.AddRange(new object[] { "学生学号","学生姓名","课程号","课程名称","是否结课","所属学院","所属入学年份"});
                comboBoxSearchType.SelectedIndex = 0;
            }
        }

        private void LoadCourseStudentChosen()
        {
            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM course_studentchosen";
                OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                currentDataTable = new DataTable();
                adapter.Fill(currentDataTable);

                dataGridView.DataSource = currentDataTable;
                dataGridView.ReadOnly = true;

                SetStudentChosenHeaders(); // 设置中文列头
            }
        }

        private void SetStudentChosenHeaders()
        {
            dataGridView.Columns["student_id"].HeaderText = "学号";
            dataGridView.Columns["student_name"].HeaderText = "姓名";
            dataGridView.Columns["course_id"].HeaderText = "课程编号";
            dataGridView.Columns["course_name"].HeaderText = "课程名称";
            dataGridView.Columns["teacher"].HeaderText = "授课教师";
            dataGridView.Columns["department"].HeaderText = "学院";
            dataGridView.Columns["semester"].HeaderText = "学期";
            dataGridView.Columns["score"].HeaderText = "成绩";
            dataGridView.Columns["is_completed"].HeaderText = "是否结课";
            dataGridView.Columns["is_passed"].HeaderText = "是否通过";
        }




        private void LoadCourseAdmin()
        {
            /*using (OracleConnection conn = new OracleConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM course_admin";

                    using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载课程信息失败: " + ex.Message);
                }
            }*/
            string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";
            string query = "SELECT * FROM course_admin";

            using (OracleConnection conn = new OracleConnection(connString))
            using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
            {
                currentDataTable = new DataTable();
                adapter.Fill(currentDataTable);
                dataGridView.DataSource = currentDataTable;
                SetColumnHeaders();
            }
        }


        private void SetColumnHeaders()
        {
            dataGridView.Columns["course_id"].HeaderText = "课程编号";
            dataGridView.Columns["course_name"].HeaderText = "课程名称";
            dataGridView.Columns["teacher"].HeaderText = "教师";
            dataGridView.Columns["capacity"].HeaderText = "容量";
            dataGridView.Columns["count"].HeaderText = "已选人数";
            dataGridView.Columns["semester"].HeaderText = "学期";
            dataGridView.Columns["schedule_time"].HeaderText = "时间安排";
            dataGridView.Columns["location"].HeaderText = "上课地点";
            dataGridView.Columns["department"].HeaderText = "开设学院";
            dataGridView.Columns["enroll_year"].HeaderText = "开课年级";
            dataGridView.Columns["is_avi"].HeaderText = "是否可选";
            dataGridView.Columns["created_by"].HeaderText = "添加人";
        }



        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (isAdding || isEditing) return;

            isAdding = true;
            saveButton.Enabled = true;
            cancelButton.Enabled = true;

            DataRow newRow = currentDataTable.NewRow();
            currentDataTable.Rows.Add(newRow);
            int newRowIndex = currentDataTable.Rows.IndexOf(newRow);
            dataGridView.ClearSelection();
            dataGridView.Rows[newRowIndex].Selected = true;
            dataGridView.CurrentCell = dataGridView.Rows[newRowIndex].Cells[0];


            dataGridView.ReadOnly = false;
            currentDataTable.DefaultView.AllowNew = true;

            buttonEdit.Enabled = false;
            buttonAdd.Enabled = false;
            buttonDelete.Enabled = false;
            comboBoxTable.Enabled = false;
        }

        
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (isAdding || isEditing) return;

            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isEditing = true;
            saveButton.Enabled = true;
            cancelButton.Enabled = true;
            dataGridView.ReadOnly = false;

            if (currentMode == "course_admin")
            {
                // 禁止修改主键 course_id
                if (dataGridView.Columns.Contains("course_id"))
                {
                    dataGridView.Columns["course_id"].ReadOnly = true;
                }
            }
            else if (currentMode == "course_studentchosen")
            {
                // 只允许编辑 score, is_completed, is_passed
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    col.ReadOnly = true;
                }

                if (dataGridView.Columns.Contains("score"))
                    dataGridView.Columns["score"].ReadOnly = false;

                if (dataGridView.Columns.Contains("is_completed"))
                    dataGridView.Columns["is_completed"].ReadOnly = false;

                if (dataGridView.Columns.Contains("is_passed"))
                    dataGridView.Columns["is_passed"].ReadOnly = false;
            }

            buttonEdit.Enabled = false;
            buttonAdd.Enabled = false;
            buttonDelete.Enabled = false;
            comboBoxTable.Enabled = false;
        }

       

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一行要删除的记录！");
                return;
            }

            if (isAdding || isEditing)
            {
                MessageBox.Show("当前正在添加或编辑，请先完成操作再进行删除！");
                return;
            }

            DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
            string courseId = selectedRow.Cells["course_id"].Value?.ToString();

            if (string.IsNullOrEmpty(courseId))
            {
                MessageBox.Show("无法获取课程编号，删除失败！");
                return;
            }

            // 判断是删除课程记录还是学生选课记录
            if (currentMode == "course_admin") // 课程信息表 course_admin
            {
                DialogResult result = MessageBox.Show($"确定要删除课程 {courseId} 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";
                        using (OracleConnection conn = new OracleConnection(connString))
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM course_admin WHERE course_id = :courseId";
                            using (OracleCommand cmd = new OracleCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.Add(new OracleParameter(":courseId", courseId));
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    dataGridView.Rows.Remove(selectedRow);
                                    MessageBox.Show("课程记录删除成功！");
                                }
                                else
                                {
                                    MessageBox.Show("未找到对应课程，删除失败！");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除时出错: " + ex.Message);
                    }
                }
            }
            else if (currentMode == "course_studentchosen") // 学生选课记录表 course_studentchosen
            {
                string studentId = selectedRow.Cells["student_id"].Value?.ToString();

                if (string.IsNullOrEmpty(studentId))
                {
                    MessageBox.Show("无法获取学生ID，删除失败！");
                    return;
                }

                DialogResult result = MessageBox.Show($"确定要删除学号为 {studentId} 的选课记录吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string connString = "data source=localhost:1521/orcl;user id=course;password=123456;";
                        using (OracleConnection conn = new OracleConnection(connString))
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM course_studentchosen WHERE course_id = :courseId AND student_id = :studentId";
                            using (OracleCommand cmd = new OracleCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.Add(new OracleParameter(":courseId", courseId));
                                cmd.Parameters.Add(new OracleParameter(":studentId", studentId));
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    dataGridView.Rows.Remove(selectedRow);
                                    MessageBox.Show("选课记录删除成功！");
                                }
                                else
                                {
                                    MessageBox.Show("未找到对应选课记录，删除失败！");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除时出错: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一个有效的表格！");
            }

            // 刷新数据
            LoadCourseAdmin();
        }


       
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!isAdding && !isEditing) return;

            DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;
            try
            {
                dataGridView.EndEdit();  // 应用用户编辑的更改

                using (OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();

                    if (currentMode == "course_admin")
                    {
                        if (isAdding)
                        {
                            // 检查是否存在相同 course_id
                            string checkQuery = "SELECT COUNT(*) FROM course_admin WHERE course_id = :course_id";
                            using (OracleCommand checkCmd = new OracleCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"]));
                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (count > 0)
                                {
                                    MessageBox.Show("课程编号已存在，请使用唯一的课程编号！", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            string insertQuery = "INSERT INTO course_admin " +
                                "(course_id, course_name, teacher, capacity, count, semester, schedule_time, location, department, enroll_year, is_avi, created_by) " +
                                "VALUES (:course_id, :course_name, :teacher, :capacity, :count, :semester, :schedule_time, :location, :department, :enroll_year, :is_avi, :created_by)";
                            using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                            {
                                cmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"]));
                                cmd.Parameters.Add(new OracleParameter(":course_name", row["course_name"]));
                                cmd.Parameters.Add(new OracleParameter(":teacher", row["teacher"]));
                                cmd.Parameters.Add(new OracleParameter(":capacity", row["capacity"]));
                                cmd.Parameters.Add(new OracleParameter(":count", row["count"]));
                                cmd.Parameters.Add(new OracleParameter(":semester", row["semester"]));
                                cmd.Parameters.Add(new OracleParameter(":schedule_time", row["schedule_time"]));
                                cmd.Parameters.Add(new OracleParameter(":location", row["location"]));
                                cmd.Parameters.Add(new OracleParameter(":department", row["department"]));
                                cmd.Parameters.Add(new OracleParameter(":enroll_year", row["enroll_year"]));
                                cmd.Parameters.Add(new OracleParameter(":is_avi", row["is_avi"]));
                                cmd.Parameters.Add(new OracleParameter(":created_by", row["created_by"]));
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("添加成功！");
                        }
                        else if (isEditing)
                        {
                            string updateQuery = "UPDATE course_admin SET " +
                                "course_name = :course_name, teacher = :teacher, capacity = :capacity, count = :count, semester = :semester, " +
                                "schedule_time = :schedule_time, location = :location, department = :department, enroll_year = :enroll_year, " +
                                "is_avi = :is_avi, created_by = :created_by WHERE course_id = :course_id";
                            using (OracleCommand cmd = new OracleCommand(updateQuery, conn))
                            {
                                cmd.Parameters.Add(new OracleParameter(":course_name", row["course_name"]));
                                cmd.Parameters.Add(new OracleParameter(":teacher", row["teacher"]));
                                cmd.Parameters.Add(new OracleParameter(":capacity", row["capacity"]));
                                cmd.Parameters.Add(new OracleParameter(":count", row["count"]));
                                cmd.Parameters.Add(new OracleParameter(":semester", row["semester"]));
                                cmd.Parameters.Add(new OracleParameter(":schedule_time", row["schedule_time"]));
                                cmd.Parameters.Add(new OracleParameter(":location", row["location"]));
                                cmd.Parameters.Add(new OracleParameter(":department", row["department"]));
                                cmd.Parameters.Add(new OracleParameter(":enroll_year", row["enroll_year"]));
                                cmd.Parameters.Add(new OracleParameter(":is_avi", row["is_avi"]));
                                cmd.Parameters.Add(new OracleParameter(":created_by", row["created_by"]));
                                cmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"]));
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("修改成功！");
                        }

                        LoadCourseAdmin(); // 刷新
                    }
                    else if (currentMode == "course_studentchosen")
                    {
                        if (isEditing)
                        {
                            string updateQuery = "UPDATE course_studentchosen SET " +
                                "score = :score, is_completed = :is_completed, is_passed = :is_passed " +
                                "WHERE student_id = :student_id AND course_id = :course_id";
                            using (OracleCommand cmd = new OracleCommand(updateQuery, conn))
                            {
                                cmd.Parameters.Add(new OracleParameter(":score", row["score"]));
                                cmd.Parameters.Add(new OracleParameter(":is_completed", row["is_completed"]));
                                cmd.Parameters.Add(new OracleParameter(":is_passed", row["is_passed"]));
                                cmd.Parameters.Add(new OracleParameter(":student_id", row["student_id"]));
                                cmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"]));
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("修改成功！");
                        }
                        else if (isAdding)
                        {
                            string insertQuery = "INSERT INTO course_studentchosen " +
                                "(student_id, student_name, course_id, course_name, teacher, department, semester, score, is_completed, is_passed) " +
                                "VALUES (:student_id, :student_name, :course_id, :course_name, :teacher, :department, :semester, :score, :is_completed, :is_passed)";
                            using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                            {
                                cmd.Parameters.Add(new OracleParameter(":student_id", row["student_id"]));
                                cmd.Parameters.Add(new OracleParameter(":student_name", row["student_name"]));
                                cmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"]));
                                cmd.Parameters.Add(new OracleParameter(":course_name", row["course_name"]));
                                cmd.Parameters.Add(new OracleParameter(":teacher", row["teacher"]));
                                cmd.Parameters.Add(new OracleParameter(":department", row["department"]));
                                cmd.Parameters.Add(new OracleParameter(":semester", row["semester"]));
                                cmd.Parameters.Add(new OracleParameter(":score", row["score"]));
                                cmd.Parameters.Add(new OracleParameter(":is_completed", row["is_completed"]));
                                cmd.Parameters.Add(new OracleParameter(":is_passed", row["is_passed"]));
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("添加成功！");
                        }

                        LoadCourseStudentChosen(); // 刷新
                    }

                    isAdding = false;
                    isEditing = false;
                    saveButton.Enabled = false;
                    cancelButton.Enabled = false;
                    dataGridView.ReadOnly = true;

                    buttonEdit.Enabled = true;
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = true;
                    comboBoxTable.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message);
            }
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (!isAdding && !isEditing) return;

            if (isAdding)
            {
                // 删除刚才添加的空行
                DataRow lastRow = currentDataTable.Rows[currentDataTable.Rows.Count - 1];
                currentDataTable.Rows.Remove(lastRow);
            }
            else if (isEditing)
            {
                // 放弃对当前行的编辑
                dataGridView.CancelEdit();
                currentDataTable.RejectChanges(); // 撤销修改（需使用 DataTable.Copy() 作为备份才能更彻底）
            }

            isAdding = false;
            isEditing = false;
            saveButton.Enabled = false;
            cancelButton.Enabled = false;
            dataGridView.ReadOnly = true;

            buttonEdit.Enabled = true;
            buttonAdd.Enabled = true;
            buttonDelete.Enabled = true;
            comboBoxTable.Enabled = true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            if (comboBoxTable.SelectedItem == null)
            {
                MessageBox.Show("请选择查询类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                MessageBox.Show("请输入关键字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxSearchType.SelectedItem == null)
            {
                MessageBox.Show("请选择查询字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string searchBy = comboBoxSearchType.SelectedItem?.ToString();
            string keyword = textBoxSearch.Text.Trim();

            

            if (string.IsNullOrEmpty(searchBy))
            {
                MessageBox.Show("请选择查询类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入关键词", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string conditionColumn = "";

            // 使用 switch 映射字段
            switch (currentMode)
            {
                case "course_admin":
                    switch (searchBy)
                    {
                        case "课程号":
                            conditionColumn = "course_id";
                            break;
                        case "课程名称":
                            conditionColumn = "course_name";
                            break;
                        case "是否可选":
                            conditionColumn = "is_avi";
                            break;
                        case "所属学院":
                            conditionColumn = "department";
                            break;
                        case "所属入学年份":
                            conditionColumn = "enroll_year";
                            break;
                        default:
                            MessageBox.Show("无效的查询字段", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    SearchCourseAdmin(conditionColumn, keyword);
                    break;

                case "course_studentchosen":
                    switch (searchBy)
                    {
                        case "学生学号":
                            conditionColumn = "student_id";
                            break;
                        case "学生姓名":
                            conditionColumn = "student_name";
                            break;
                        case "课程号":
                            conditionColumn = "course_id";
                            break;
                        case "课程名称":
                            conditionColumn = "course_name";
                            break;
                        case "是否结课":
                            conditionColumn = "is_completed";
                            break;
                        case "所属学院":
                            conditionColumn = "department";
                            break;
                        case "所属入学年份":
                            conditionColumn = "enroll_year";
                            break;
                        default:
                            MessageBox.Show("无效的查询字段", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    SearchCourseStudentChosen(conditionColumn, keyword);
                    break;

                default:
                    MessageBox.Show("请选择一个有效的数据表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
        }

        private void SearchCourseAdmin(string column, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = $"SELECT * FROM course_admin WHERE {column} LIKE :keyword";
                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;

                SetColumnHeaders(); // 设置课程表列名
            }
        }

        private void SearchCourseStudentChosen(string column, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = $"SELECT * FROM course_studentchosen WHERE {column} LIKE :keyword";
                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;

                SetStudentChosenHeaders(); // 设置选课记录列名
            }
        }

    }
}
