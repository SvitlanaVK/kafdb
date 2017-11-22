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
//AAAAA
namespace kusrovikdb
{
    public partial class Form3 : Form
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["kusrovikdb.Properties.Settings.KAFEDRAConnectionString"].ConnectionString;
        SqlConnection cn = new SqlConnection(connectionString);

        public Form3()
        {
            InitializeComponent();
        }

        public void fillDataGrid()
        {
            string sql = "";
            string where = "";
            string from = @"
            FROM Fixation f
            JOIN Teachers t ON f.TeacherID = t.ID
            JOIN Groups g ON f.GroupID = g.ID
            JOIN Subjects s ON f.SubjectID = s.ID
            JOIN Works w ON f.WorkID = w.ID
            JOIN SemesterPlan sp ON f.SemesterPlanID = sp.ID
            ";

            //FILTER
            if (toolStripComboBox1.ComboBox.SelectedIndex!=-1)
            {
                switch (toolStripComboBox1.ComboBox.SelectedItem.ToString())
                {
                    case "Имя преподавателя":
                        where = " WHERE t.Name LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                    case "Группа":
                        where = " WHERE g.Name LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                    case "Предмет":
                        where = " WHERE s.Name LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                    case "Вид работы":
                        where = " WHERE w.Name LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                    case "Семестр":
                        where = " WHERE sp.Semester LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                    case "Часы по плану":
                        where = " WHERE f.PlanHours LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                    case "Затраченные часы":
                        where = " WHERE f.ActualHours LIKE '%" + toolStripTextBox1.Text + "%'";
                        break;
                }   
            }
            //GROUPING

            if (toolStripComboBox2.ComboBox.SelectedIndex != -1 && toolStripComboBox2.ComboBox.SelectedIndex != 5)
            {
                switch (toolStripComboBox2.ComboBox.SelectedItem.ToString())
                {
                    case "Преподавателю":
                        sql = @"
                            SELECT t.Name AS 'Имя преподавателя', SUM(f.PlanHours) AS 'Часы по плану',
                            COUNT(f.ActualHours) AS 'Затраченные часы'
                            " + from + @"
                            " + where + @"
                            GROUP BY t.Name
                            ";
                        break;
                    case "Группе":
                        sql = @"
                            SELECT g.Name AS 'Группа', SUM(f.PlanHours) AS 'Часы по плану',
                            COUNT(f.ActualHours) AS 'Затраченные часы'
                            " + from + @"
                            " + where +@"
                            GROUP BY g.Name
                            ";
                        break;
                    case "Предмету":
                        sql = @"
                            SELECT s.Name AS 'Предмет', SUM(f.PlanHours) AS 'Часы по плану',
                            COUNT(f.ActualHours) AS 'Затраченные часы'
                            " + from + @"
                            " + where + @"
                            GROUP BY s.Name
                            ";
                        break;
                    case "Виду работы":
                        sql = @"
                            SELECT w.Name AS 'Вид работы', SUM(f.PlanHours) AS 'Часы по плану',
                            COUNT(f.ActualHours) AS 'Затраченные часы'
                            " + from + @"
                            " + where + @"
                            GROUP BY w.Name
                            ";
                        break;
                    case "Семестру":
                        sql = @"
                            SELECT sp.Semester AS 'Семестр', SUM(f.PlanHours) AS 'Часы по плану',
                            COUNT(f.ActualHours) AS 'Затраченные часы'
                            " + from + @"
                            " + where + @"
                            GROUP BY sp.Semester
                            ";
                        break;
                }
            } else {
                sql = @"
            SELECT t.Name AS 'Имя преподавателя', g.Name AS 'Группа', s.Name AS 'Предмет', w.Name AS 'Вид работы', 
            sp.Semester AS 'Семестр', f.PlanHours AS 'Часы по плану', f.ActualHours AS 'Затраченные часы'  
            " + from + @"
            " + where;
            }

            try
            {
                SqlCommand cm;
                cm = new SqlCommand(sql, cn);
                SqlDataAdapter fixationAdapter = new SqlDataAdapter(cm);
                DataTable fixation = new DataTable();
                fixationAdapter.Fill(fixation);
                dataGridView1.DataSource = fixation;


                foreach (var column in fixation.Columns)
                {
                    if (!toolStripComboBox1.ComboBox.Items.Contains(column.ToString()))
                        toolStripComboBox1.ComboBox.Items.Add(column.ToString());
                }
            }
            catch (SqlException ex)
            {

            }
                
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            fillDataGrid();
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDataGrid();
        }
    }
}
