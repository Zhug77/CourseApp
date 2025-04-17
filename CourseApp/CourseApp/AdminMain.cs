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


        public AdminMain()
        {
            InitializeComponent();
        }

        private void AdminMain_Load(object sender, EventArgs e)
        {
            LoadCourseAdmin();  // 默认加载课程信息
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("切换了表格视图");
            //if (comboBoxTable.SelectedIndex == 0)
            //LoadCourseAdmin();
            //else
            //LoadCourseStudentChosen();
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
            }
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
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            // 如果正在添加，则禁用修改功能
            if (isAdding)
            {
                buttonEdit.Enabled = false;
                return;
            }

            // 如果已经在编辑，也不重复进入
            if (isEditing) return;

            // 如果没有选择行，提示用户
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的行。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isEditing = true;
            saveButton.Enabled = true;
            cancelButton.Enabled = true;

            // 设置 DataGridView 可编辑
            dataGridView.ReadOnly = false;
            dataGridView.Columns["course_id"].ReadOnly = true;


            // 自动选中当前行第一列
            //var selectedRow = dataGridView.SelectedRows[0];
            //dataGridView.CurrentCell = selectedRow.Cells[0];
            buttonEdit.Enabled = false;
            buttonAdd.Enabled = false;
            buttonDelete.Enabled = false;
                
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一行要删除的课程记录！");
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
                                //MessageBox.Show("删除成功！");
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
            LoadCourseAdmin();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
            
            if (!isAdding && !isEditing) return;

            DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;
            using (OracleConnection checkConn = new OracleConnection(connString))
            {
                checkConn.Open(); // ⭐ 加上这一行！

                string checkQuery = "SELECT COUNT(*) FROM course_admin WHERE course_id = :course_id";
                using (OracleCommand checkCmd = new OracleCommand(checkQuery, checkConn))
                {
                    checkCmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"]));
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("课程编号已存在，请使用唯一的课程编号！", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }



            try
            {
                dataGridView.EndEdit();  // 应用用户编辑的更改
                //DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;

                using (OracleConnection conn = new OracleConnection(connString))
                {
                    conn.Open();
                    //string checkQuery = "SELECT COUNT(*) FROM course_admin WHERE course_id = :course_id";

                    if (isAdding)
                    {
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
                            cmd.Parameters.Add(new OracleParameter(":course_id", row["course_id"])); // where

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("修改成功！1");
                    }
                    isAdding = false;
                    isEditing = false;

                    dataGridView.ReadOnly = true;
                    saveButton.Enabled = false;
                    cancelButton.Enabled = false;


                    buttonEdit.Enabled = true;
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = true;


                    LoadCourseAdmin(); // 重新加载数据，刷新视图
                }

                isAdding = false;
                isEditing = false;
                saveButton.Enabled = false;
                cancelButton.Enabled = false;
                dataGridView.ReadOnly = true;

                LoadCourseAdmin(); // 重新加载
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
        }


    }
}
