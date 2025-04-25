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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CourseApp
{
    public partial class StudentMain : Form
    {
        public StudentMain(string studentId, string department, string enrollYear)
        {
            InitializeComponent();

            this.studentId = studentId;
            this.studentDepartment = department;
            this.studentEnrollYear = enrollYear;
            LoadSelectableCourses();

            // 设置标签值
            labelStudentId.Text = studentId;
            labelDepartment.Text = department;
            labelEnrollYear.Text = enrollYear;

            // 查询学生姓名
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT name FROM student WHERE student_id = :id";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = studentId;
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            labelStudentName.Text = reader.GetString(0);
                        }
                    }
                }
            }
            comboBoxTable.SelectedIndex = 0;
            comboBoxSearchType.SelectedIndex = 0;
        }


        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxTable.SelectedItem.ToString();
            comboBoxSearchType.Items.Clear();
            comboBoxSearchType.Text = "";
            if (selected == "可选课表")
            {
                LoadSelectableCourses();
                comboBoxSearchType.Items.AddRange(new object[] { "课程ID", "课程名称", "是否可选", "所属学院", "所属入学年份", "所属学期" });
                comboBoxSearchType.SelectedIndex = 0;
            }
            else if (selected == "已选课表")
            {
                LoadSelectedCourses();
                comboBoxSearchType.Items.AddRange(new object[] { "课程ID", "课程名称", "所属学院", "所属学期" });
                comboBoxSearchType.SelectedIndex = 0;
            }
            else if (selected == "已选未结课课表")
            {
                LoadUncompletedCourses();
                comboBoxSearchType.Items.AddRange(new object[] { "课程ID", "课程名称", "所属学院", "所属学期" });
                comboBoxSearchType.SelectedIndex = 0;
            }
            else if (selected == "已选已结课课表")
            {
                LoadCompletedCourses();
                comboBoxSearchType.Items.AddRange(new object[] { "课程ID", "课程名称", "所属学院", "是否通过", "所属学期" });
                comboBoxSearchType.SelectedIndex = 0;
            }
            else if (selected == "所有课程")
            {
                LoadAllCourse();
                comboBoxSearchType.Items.AddRange(new object[] { "课程ID", "课程名称", "是否可选", "所属学院", "所属入学年份", "所属学期" });
                comboBoxSearchType.SelectedIndex = 0;
            }
        }

        private void LoadSelectableCourses()
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = @"SELECT 
                            course_id, course_name, teacher, capacity, count, semester, schedule_time, location
                       FROM course_admin
                       WHERE  department = :department 
                             AND enroll_year = :enrollYear";
                //is_avi = 'Y'  AND
                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":department", OracleDbType.Varchar2).Value = studentDepartment;
                adapter.SelectCommand.Parameters.Add(":enrollYear", OracleDbType.Varchar2).Value = studentEnrollYear;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;
                if (dataGridView.Columns["course_id"] != null) dataGridView.Columns["course_id"].HeaderText = "课程ID";
                if (dataGridView.Columns["course_name"] != null) dataGridView.Columns["course_name"].HeaderText = "课程名称";
                if (dataGridView.Columns["teacher"] != null) dataGridView.Columns["teacher"].HeaderText = "授课老师";
                if (dataGridView.Columns["capacity"] != null) dataGridView.Columns["capacity"].HeaderText = "容量";
                if (dataGridView.Columns["count"] != null) dataGridView.Columns["count"].HeaderText = "已选人数";
                if (dataGridView.Columns["semester"] != null) dataGridView.Columns["semester"].HeaderText = "学期";
                if (dataGridView.Columns["schedule_time"] != null) dataGridView.Columns["schedule_time"].HeaderText = "上课时间";
                if (dataGridView.Columns["location"] != null) dataGridView.Columns["location"].HeaderText = "上课地点";
            }

            buttonCancelCourse.Enabled = false;
            buttonSelectCourse.Enabled = true;
        }

        private void LoadAllCourse()
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = @"SELECT 
                        course_id, 
                        course_name, 
                        teacher, 
                        capacity, 
                        count, 
                        semester, 
                        schedule_time, 
                        location,
                        department,
                        enroll_year,
                        is_avi
                       FROM course_admin";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 设置中文列头
                dt.Columns["course_id"].ColumnName = "课程ID";
                dt.Columns["course_name"].ColumnName = "课程名称";
                dt.Columns["teacher"].ColumnName = "教师";
                dt.Columns["capacity"].ColumnName = "容量";
                dt.Columns["count"].ColumnName = "已选人数";
                dt.Columns["semester"].ColumnName = "学期";
                dt.Columns["schedule_time"].ColumnName = "上课时间";
                dt.Columns["location"].ColumnName = "上课地点";
                dt.Columns["department"].ColumnName = "学院";
                dt.Columns["enroll_year"].ColumnName = "入学年份";
                dt.Columns["is_avi"].ColumnName = "是否可选";

                dataGridView.DataSource = dt;
            }
            buttonCancelCourse.Enabled = false;
            buttonSelectCourse.Enabled = false;
        }


        private void LoadSelectedCourses()
        {
            string connectionString = "data source=localhost:1521/orcl;user id=course;password=123456;"; // 替换为你实际的连接字符串

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string sql = @"
                SELECT course_id, course_name, teacher, department, semester, score, is_completed, is_passed
                FROM course_studentchosen
                WHERE student_id = :sid";

                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId; // studentId 是当前学生的学号

                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView.DataSource = dt;

                            // 可选：设置列头名称
                            dataGridView.Columns["course_id"].HeaderText = "课程ID";
                            dataGridView.Columns["course_name"].HeaderText = "课程名称";
                            dataGridView.Columns["teacher"].HeaderText = "授课老师";
                            dataGridView.Columns["department"].HeaderText = "学院";
                            dataGridView.Columns["semester"].HeaderText = "学期";
                            dataGridView.Columns["score"].HeaderText = "成绩";
                            dataGridView.Columns["is_completed"].HeaderText = "是否结课";
                            dataGridView.Columns["is_passed"].HeaderText = "是否通过";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载已选课程失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            buttonCancelCourse.Enabled = false;
            buttonSelectCourse.Enabled = false;
        }



        private void LoadUncompletedCourses()
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = @"SELECT 
                            course_id, 
                            course_name, 
                            teacher, 
                            department, 
                            semester, 
                            score, 
                            is_completed, 
                            is_passed
                       FROM course_studentchosen
                       WHERE student_id = :sid AND is_completed = 'N'";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;

                // 设置中文列头
                if (dataGridView.Columns["course_id"] != null) dataGridView.Columns["course_id"].HeaderText = "课程ID";
                if (dataGridView.Columns["course_name"] != null) dataGridView.Columns["course_name"].HeaderText = "课程名称";
                if (dataGridView.Columns["teacher"] != null) dataGridView.Columns["teacher"].HeaderText = "授课老师";
                if (dataGridView.Columns["department"] != null) dataGridView.Columns["department"].HeaderText = "学院";
                if (dataGridView.Columns["semester"] != null) dataGridView.Columns["semester"].HeaderText = "学期";
                if (dataGridView.Columns["score"] != null) dataGridView.Columns["score"].HeaderText = "成绩";
                if (dataGridView.Columns["is_completed"] != null) dataGridView.Columns["is_completed"].HeaderText = "是否结课";
                if (dataGridView.Columns["is_passed"] != null) dataGridView.Columns["is_passed"].HeaderText = "是否通过";
            }

            buttonCancelCourse.Enabled = true;
            buttonSelectCourse.Enabled = false;
        }

        private void LoadCompletedCourses()
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = @"SELECT 
                            course_id, 
                            course_name, 
                            teacher, 
                            department, 
                            semester, 
                            score, 
                            is_completed, 
                            is_passed
                       FROM course_studentchosen
                       WHERE student_id = :sid AND is_completed = 'Y'";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;

                // 设置中文列头
                if (dataGridView.Columns["course_id"] != null) dataGridView.Columns["course_id"].HeaderText = "课程ID";
                if (dataGridView.Columns["course_name"] != null) dataGridView.Columns["course_name"].HeaderText = "课程名称";
                if (dataGridView.Columns["teacher"] != null) dataGridView.Columns["teacher"].HeaderText = "授课老师";
                if (dataGridView.Columns["department"] != null) dataGridView.Columns["department"].HeaderText = "学院";
                if (dataGridView.Columns["semester"] != null) dataGridView.Columns["semester"].HeaderText = "学期";
                if (dataGridView.Columns["score"] != null) dataGridView.Columns["score"].HeaderText = "成绩";
                if (dataGridView.Columns["is_completed"] != null) dataGridView.Columns["is_completed"].HeaderText = "是否结课";
                if (dataGridView.Columns["is_passed"] != null) dataGridView.Columns["is_passed"].HeaderText = "是否通过";
            }

            buttonCancelCourse.Enabled = false;
            buttonSelectCourse.Enabled = false;
        }



        private void buttonSelectCourse_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一门课程进行选课！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridView.SelectedRows[0];

            string courseId = selectedRow.Cells["course_id"].Value.ToString();
            string courseName = selectedRow.Cells["course_name"].Value.ToString();
            string teacher = selectedRow.Cells["teacher"].Value.ToString();
            string department = studentDepartment;
            string semester = selectedRow.Cells["semester"].Value.ToString();

            DialogResult confirm = MessageBox.Show($"确定要选择课程 {courseName} 吗？", "确认选课", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();

                // 检查课程是否仍可选
                string checkSql = @"
            SELECT capacity, count, is_avi 
            FROM course_admin 
            WHERE course_id = :cid";

                using (OracleCommand checkCmd = new OracleCommand(checkSql, conn))
                {
                    checkCmd.Parameters.Add(":cid", OracleDbType.Varchar2).Value = courseId;

                    using (OracleDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int capacity = reader.GetInt32(0);
                            int count = reader.GetInt32(1);
                            string isAvi = reader.GetString(2);

                            if (isAvi != "Y")
                            {
                                MessageBox.Show("该课程当前不可选！", "选课失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (count >= capacity)
                            {
                                MessageBox.Show("该课程已满，无法选课！", "选课失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("课程不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // 获取学生姓名
                string studentName = "";
                string nameSql = "SELECT name FROM student WHERE student_id = :sid";
                using (OracleCommand nameCmd = new OracleCommand(nameSql, conn))
                {
                    nameCmd.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId;
                    object result = nameCmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("无法获取学生信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    studentName = result.ToString();
                }

                // 检查是否已经选过该课程
                string checkDuplicateSql = @"
    SELECT COUNT(*) 
    FROM course_studentchosen 
    WHERE student_id = :sid AND course_id = :cid";

                using (OracleCommand dupCmd = new OracleCommand(checkDuplicateSql, conn))
                {
                    dupCmd.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId;
                    dupCmd.Parameters.Add(":cid", OracleDbType.Varchar2).Value = courseId;

                    int count = Convert.ToInt32(dupCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("你已经选过这门课程，不能重复选课！", "选课失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                // 插入选课记录
                string insertSql = @"
            INSERT INTO course_studentchosen 
            (student_id, student_name, course_id, course_name, teacher, department, semester, score, is_completed, is_passed)
            VALUES 
            (:sid, :sname, :cid, :cname, :teacher, :dept, :sem, 0, 'N', '-')";

                using (OracleCommand insertCmd = new OracleCommand(insertSql, conn))
                {
                    insertCmd.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId;
                    insertCmd.Parameters.Add(":sname", OracleDbType.Varchar2).Value = studentName;
                    insertCmd.Parameters.Add(":cid", OracleDbType.Varchar2).Value = courseId;
                    insertCmd.Parameters.Add(":cname", OracleDbType.Varchar2).Value = courseName;
                    insertCmd.Parameters.Add(":teacher", OracleDbType.Varchar2).Value = teacher;
                    insertCmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = department;
                    insertCmd.Parameters.Add(":sem", OracleDbType.Varchar2).Value = semester;

                    try
                    {
                        insertCmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show("选课失败，请咨询管理员。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 更新课程已选人数
                string updateSql = "UPDATE course_admin SET count = count + 1 WHERE course_id = :cid";
                using (OracleCommand updateCmd = new OracleCommand(updateSql, conn))
                {
                    updateCmd.Parameters.Add(":cid", OracleDbType.Varchar2).Value = courseId;
                    updateCmd.ExecuteNonQuery();
                }

                MessageBox.Show("选课成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSelectableCourses(); // 重新加载数据
            }
        }

        private void buttonCancelCourse_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要退选的课程。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 假设课程编号在 dataGridView 的第一列
            string courseId = dataGridView.SelectedRows[0].Cells["course_id"].Value.ToString();

            DialogResult result = MessageBox.Show("确定要退选这门课程吗？", "确认退课", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction(); // 使用事务

                try
                {
                    // 1. 删除选课记录
                    string deleteSql = @"DELETE FROM course_studentchosen 
                                 WHERE student_id = :sid AND course_id = :cid";
                    using (OracleCommand deleteCmd = new OracleCommand(deleteSql, conn))
                    {
                        deleteCmd.Transaction = transaction;
                        deleteCmd.Parameters.Add(":sid", OracleDbType.Varchar2).Value = studentId;
                        deleteCmd.Parameters.Add(":cid", OracleDbType.Varchar2).Value = courseId;
                        deleteCmd.ExecuteNonQuery();
                    }

                    // 2. 更新课程容量
                    string updateSql = @"UPDATE course_admin 
                                 SET count = count - 1 
                                 WHERE course_id = :cid";
                    using (OracleCommand updateCmd = new OracleCommand(updateSql, conn))
                    {
                        updateCmd.Transaction = transaction;
                        updateCmd.Parameters.Add(":cid", OracleDbType.Varchar2).Value = courseId;
                        updateCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("退课成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadUncompletedCourses(); // 刷新已选课表
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("退课失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            string selected = comboBoxTable.SelectedItem.ToString();
            string keyword = textBoxSearch.Text.Trim();
            string searchBy = comboBoxSearchType.SelectedItem?.ToString(); // 课程号 或 课程名称

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入要搜索的关键词", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            switch (selected)
            {
                case "可选课表":
                    SearchSelectableCourses(searchBy, keyword);
                    break;
                case "已选课表":
                    SearchSelectedCourses(searchBy, keyword);
                    break;
                case "已选未结课课表":
                    SearchUncompletedCourses(searchBy, keyword);
                    break;
                case "已选已结课课表":
                    SearchCompletedCourses(searchBy, keyword);
                    break;
                case "所有课程":
                    SearchAllCourses(searchBy, keyword);
                    break;
                default:
                    MessageBox.Show("请选择查询类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void SearchSelectableCourses(string searchBy, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string condition = "";
                switch (searchBy)
                {
                    case "课程ID":
                        condition = "course_id";
                        break;
                    case "课程名称":
                        condition = "course_name";
                        break;
                    case "是否可选":
                        condition = "is_avi";
                        break;
                    case "所属学院":
                        condition = "department";
                        break;
                    case "所属入学年份":
                        condition = "enroll_year";
                        break;
                    case "所属学期":
                        condition = "semester";
                        break;
                    default:
                        MessageBox.Show("请选择有效的查询字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                string sql = $@"SELECT course_id, course_name, teacher, capacity, count, semester, 
                        schedule_time, location
                        FROM course_admin
                        WHERE department = :department AND enroll_year = :enrollYear
                        AND {condition} LIKE :keyword";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":department", OracleDbType.Varchar2).Value = studentDepartment;
                adapter.SelectCommand.Parameters.Add(":enrollYear", OracleDbType.Varchar2).Value = studentEnrollYear;
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 设置中文列头（可提取成公共方法）
                dt.Columns["course_id"].ColumnName = "课程ID";
                dt.Columns["course_name"].ColumnName = "课程名称";
                dt.Columns["teacher"].ColumnName = "教师";
                dt.Columns["capacity"].ColumnName = "容量";
                dt.Columns["count"].ColumnName = "已选人数";
                dt.Columns["semester"].ColumnName = "学期";
                dt.Columns["schedule_time"].ColumnName = "上课时间";
                dt.Columns["location"].ColumnName = "上课地点";

                dataGridView.DataSource = dt;
            }
        }

        private void SearchSelectedCourses(string searchBy, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string condition = "";
                switch (searchBy)
                {
                    case "课程ID":
                        condition = "course_id";
                        break;
                    case "课程名称":
                        condition = "course_name";
                        break;
                    case "所属学院":
                        condition = "department";
                        break;
                    case "所属学期":
                        condition = "semester";
                        break;
                    default:
                        MessageBox.Show("请选择有效的查询字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                string sql = $@"SELECT course_id, course_name, teacher, department, semester, score, is_completed, is_passed
                        FROM course_studentchosen
                        WHERE student_id = :studentId AND {condition} LIKE :keyword";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":studentId", OracleDbType.Varchar2).Value = studentId;
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dt.Columns["course_id"].ColumnName = "课程ID";
                dt.Columns["course_name"].ColumnName = "课程名称";
                dt.Columns["teacher"].ColumnName = "教师";
                dt.Columns["department"].ColumnName = "学院";
                dt.Columns["semester"].ColumnName = "学期";
                dt.Columns["score"].ColumnName = "成绩";
                dt.Columns["is_completed"].ColumnName = "是否结课";
                dt.Columns["is_passed"].ColumnName = "是否通过";

                dataGridView.DataSource = dt;
            }
        }

        private void SearchUncompletedCourses(string searchBy, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string condition = "";
                switch (searchBy)
                {
                    case "课程ID":
                        condition = "course_id";
                        break;
                    case "课程名称":
                        condition = "course_name";
                        break;
                    case "所属学院":
                        condition = "department";
                        break;
                    case "所属学期":
                        condition = "semester";
                        break;
                    default:
                        MessageBox.Show("请选择有效的查询字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                string sql = $@"SELECT course_id, course_name, teacher, department, semester, score, is_completed, is_passed
                        FROM course_studentchosen
                        WHERE student_id = :studentId AND is_completed = 'N' AND {condition} LIKE :keyword";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":studentId", OracleDbType.Varchar2).Value = studentId;
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dt.Columns["course_id"].ColumnName = "课程ID";
                dt.Columns["course_name"].ColumnName = "课程名称";
                dt.Columns["teacher"].ColumnName = "教师";
                dt.Columns["department"].ColumnName = "学院";
                dt.Columns["semester"].ColumnName = "学期";
                dt.Columns["score"].ColumnName = "成绩";
                dt.Columns["is_completed"].ColumnName = "是否结课";
                dt.Columns["is_passed"].ColumnName = "是否通过";

                dataGridView.DataSource = dt;
            }
        }

        private void SearchCompletedCourses(string searchBy, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string condition = "";
                switch (searchBy)
                {
                    case "课程ID":
                        condition = "course_id";
                        break;
                    case "课程名称":
                        condition = "course_name";
                        break;
                    case "是否通过":
                        condition = "is_passed";
                        break;
                    case "所属学院":
                        condition = "department";
                        break;
                    case "所属学期":
                        condition = "semester";
                        break;
                    default:
                        MessageBox.Show("请选择有效的查询字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                string sql = $@"SELECT course_id, course_name, teacher, department, semester, score, is_completed, is_passed
                        FROM course_studentchosen
                        WHERE student_id = :studentId AND is_completed = 'Y' AND {condition} LIKE :keyword";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":studentId", OracleDbType.Varchar2).Value = studentId;
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dt.Columns["course_id"].ColumnName = "课程ID";
                dt.Columns["course_name"].ColumnName = "课程名称";
                dt.Columns["teacher"].ColumnName = "教师";
                dt.Columns["department"].ColumnName = "学院";
                dt.Columns["semester"].ColumnName = "学期";
                dt.Columns["score"].ColumnName = "成绩";
                dt.Columns["is_completed"].ColumnName = "是否结课";
                dt.Columns["is_passed"].ColumnName = "是否通过";

                dataGridView.DataSource = dt;
            }
        }

        private void SearchAllCourses(string searchBy, string keyword)
        {
            string connStr = "data source=localhost:1521/orcl;user id=course;password=123456;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string condition = "";
                switch (searchBy)
                {
                    case "课程ID":
                        condition = "course_id";
                        break;
                    case "课程名称":
                        condition = "course_name";
                        break;
                    case "是否可选":
                        condition = "is_avi";
                        break;
                    case "所属学院":
                        condition = "department";
                        break;
                    case "所属入学年份":
                        condition = "enroll_year";
                        break;
                    case "所属学期":
                        condition = "semester";
                        break;
                    default:
                        MessageBox.Show("请选择有效的查询字段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                string sql = $@"SELECT course_id, course_name, teacher, capacity, count, semester, 
                        schedule_time, location, department, enroll_year, is_avi
                        FROM course_admin
                        WHERE {condition} LIKE :keyword";

                OracleDataAdapter adapter = new OracleDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(":keyword", OracleDbType.Varchar2).Value = "%" + keyword + "%";

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dt.Columns["course_id"].ColumnName = "课程ID";
                dt.Columns["course_name"].ColumnName = "课程名称";
                dt.Columns["teacher"].ColumnName = "教师";
                dt.Columns["capacity"].ColumnName = "容量";
                dt.Columns["count"].ColumnName = "已选人数";
                dt.Columns["semester"].ColumnName = "学期";
                dt.Columns["schedule_time"].ColumnName = "上课时间";
                dt.Columns["location"].ColumnName = "上课地点";
                dt.Columns["department"].ColumnName = "学院";
                dt.Columns["enroll_year"].ColumnName = "入学年份";
                dt.Columns["is_avi"].ColumnName = "是否可选";

                dataGridView.DataSource = dt;
            }
        }

        
    }
}
