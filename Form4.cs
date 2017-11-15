using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace kusrovikdb
{
    public partial class Form4 : Form
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["kusrovikdb.Properties.Settings.KAFEDRAConnectionString"].ConnectionString;
        SqlConnection cn = new SqlConnection(connectionString);
        SqlDataAdapter fixationAdapter = null;
        DataTable fixationTable = null;
        BindingSource fixationBS = null;
        SqlCommandBuilder fixationCB = null;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_Shown(object sender, EventArgs e)
        {
            //select Teacher.Name;
            string teacherSQL = @"SELECT ID, Name FROM Teachers";
            
            SqlDataAdapter teacherAdapter = new SqlDataAdapter(teacherSQL, cn);
            SqlCommandBuilder teacherCmBuilder = new SqlCommandBuilder(teacherAdapter);
            
            DataTable teacherTable = new DataTable();
            teacherAdapter.Fill(teacherTable);
            
            BindingSource teacherBS = new BindingSource();
            teacherBS.DataSource = teacherTable;
            //select Group.Name;
            string groupSQL = @"SELECT ID, Name FROM Groups";

            SqlDataAdapter groupAdapter = new SqlDataAdapter(groupSQL, cn);
            SqlCommandBuilder groupCmBuilder = new SqlCommandBuilder(groupAdapter);

            DataTable groupTable = new DataTable();
            groupAdapter.Fill(groupTable);

            BindingSource groupBS = new BindingSource();
            groupBS.DataSource = groupTable;

            //select subject.name
            string subjectSQL = @"SELECT ID, Name FROM Subjects";

            SqlDataAdapter subjectAdapter = new SqlDataAdapter(subjectSQL, cn);
            SqlCommandBuilder subjectCmBuilder = new SqlCommandBuilder(subjectAdapter);

            DataTable subjectTable = new DataTable();
            subjectAdapter.Fill(subjectTable);

            BindingSource subjectBS = new BindingSource();
            subjectBS.DataSource = subjectTable;

            //select work.Name;
            string workSQL = @"SELECT ID, Name FROM Works";

            SqlDataAdapter workAdapter = new SqlDataAdapter(workSQL, cn);
            SqlCommandBuilder workCmBuilder = new SqlCommandBuilder(workAdapter);

            DataTable workTable = new DataTable();
            workAdapter.Fill(workTable);

            BindingSource workBS = new BindingSource();
            workBS.DataSource = workTable;


            //add ID data
            DataGridViewTextBoxColumn IDColumn = new DataGridViewTextBoxColumn();

            IDColumn.DataPropertyName = "ID";
            IDColumn.HeaderText = "ID";
            IDColumn.Width = 160;
            IDColumn.Visible = false;

            dataGridView1.Columns.Add(IDColumn);

            //add SemesterPlanID data
            DataGridViewTextBoxColumn SemesterPlanIDColumn = new DataGridViewTextBoxColumn();

            SemesterPlanIDColumn.DataPropertyName = "SemesterPlanID";
            SemesterPlanIDColumn.HeaderText = "Номер записи семестра";
            SemesterPlanIDColumn.Width = 160;
            SemesterPlanIDColumn.Visible = true;

            dataGridView1.Columns.Add(SemesterPlanIDColumn);


            //add teacher combo
            DataGridViewComboBoxColumn teacherColumn = new DataGridViewComboBoxColumn();

            teacherColumn.DataPropertyName = "TeacherID";
            teacherColumn.HeaderText = "Имя преподавателя";
            teacherColumn.Width = 260;

            teacherColumn.DataSource = teacherBS;
            teacherColumn.ValueMember = "ID";
            teacherColumn.DisplayMember = "Name";

            dataGridView1.Columns.Add(teacherColumn);

            //add group Combo
            DataGridViewComboBoxColumn groupColumn = new DataGridViewComboBoxColumn();

            groupColumn.DataPropertyName = "GroupID";
            groupColumn.HeaderText = "Группа";
            groupColumn.Width = 160;

            groupColumn.DataSource = groupBS;
            groupColumn.ValueMember = "ID";
            groupColumn.DisplayMember = "Name";

            dataGridView1.Columns.Add(groupColumn);

            //add subject Combo
            DataGridViewComboBoxColumn subjectColumn = new DataGridViewComboBoxColumn();

            subjectColumn.DataPropertyName = "SubjectID";
            subjectColumn.HeaderText = "Предмет";
            subjectColumn.Width = 160;

            subjectColumn.DataSource = subjectBS;
            subjectColumn.ValueMember = "ID";
            subjectColumn.DisplayMember = "Name";

            dataGridView1.Columns.Add(subjectColumn);

            //add work Combo
            DataGridViewComboBoxColumn workColumn = new DataGridViewComboBoxColumn();

            workColumn.DataPropertyName = "WorkID";
            workColumn.HeaderText = "Вид работы";
            workColumn.Width = 160;

            workColumn.DataSource = workBS;
            workColumn.ValueMember = "ID";
            workColumn.DisplayMember = "Name";

            dataGridView1.Columns.Add(workColumn);

            //add PlanHours data
            DataGridViewTextBoxColumn PlanHoursColumn = new DataGridViewTextBoxColumn();

            PlanHoursColumn.DataPropertyName = "PlanHours";
            PlanHoursColumn.HeaderText = "Часы по плану";
            PlanHoursColumn.Width = 160;

            dataGridView1.Columns.Add(PlanHoursColumn);

            //add ActualHours data
            DataGridViewTextBoxColumn ActualHoursColumn = new DataGridViewTextBoxColumn();

            ActualHoursColumn.DataPropertyName = "ActualHours";
            ActualHoursColumn.HeaderText = "Затраченные часы";
            ActualHoursColumn.Width = 160;

            dataGridView1.Columns.Add(ActualHoursColumn);



            //Select Fixation
            //string fixationSQL = @"SELECT f.ID, f.SemesterPlanID, f.TeacherID, f.GroupID, f.SubjectID, f.WorkID, sp.Semester, f.PlanHours, f.ActualHours FROM Fixation f JOIN SemesterPlan sp ON f.SemesterPlanID = sp.ID";
            fillDataGrid();


        }

        public void fillDataGrid()
        {
            string fixationSQL = @"SELECT f.ID, f.SemesterPlanID, f.TeacherID, f.GroupID, f.SubjectID, f.WorkID, f.PlanHours, f.ActualHours FROM Fixation f";
            fixationAdapter = new SqlDataAdapter(fixationSQL, cn);
            fixationCB = new SqlCommandBuilder(fixationAdapter);

            fixationTable = new DataTable();
            fixationAdapter.Fill(fixationTable);

            fixationBS = new BindingSource();
            fixationBS.DataSource = fixationTable;

            dataGridView1.DataSource = fixationBS;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (var row in dataGridView1.Rows)
                //{
                //    string updateSQL = String.Format( @"update Fixation set ");
                //   // fixationAdapter.UpdateCommand = new SqlCommand(@"update Fixation set ; 
                //     ///                                               update SemesterPlan set Semester=4 where ID=1");
                //    fixationAdapter.Update(fixationTable);
                //}
                //MessageBox.Show(dataGridView1.Rows[1].Cells[4].Value.ToString());
                fillDataGrid();
                updateData();
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }

        public void updateData()
        {
            fixationBS.EndEdit();
            dataGridView1.Refresh();
            dataGridView1.Parent.Refresh();
            fixationTable.GetChanges();
            dataGridView1.Invalidate();
            dataGridView1.EndEdit();
            fixationAdapter.Update(fixationTable);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Form f = Application.OpenForms["Form3"];
                if (f != null)
                {
                    (f as Form3).fillDataGrid();
                }
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                fixationAdapter.Update(fixationTable);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }
    }
}
