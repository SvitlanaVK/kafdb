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
    public partial class Form5 : Form
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["kusrovikdb.Properties.Settings.KAFEDRAConnectionString"].ConnectionString;
        SqlConnection cn = new SqlConnection(connectionString);
        SqlDataAdapter semestrAdapter = null;
        DataTable semestrTable = null;
        BindingSource semestrBS = null;
        SqlCommandBuilder semestrCB = null;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Shown(object sender, EventArgs e)
        {
            string semestrSQL = @"SELECT * FROM SemesterPlan";
            semestrAdapter = new SqlDataAdapter(semestrSQL, cn);
            semestrCB = new SqlCommandBuilder(semestrAdapter);

            semestrTable = new DataTable();
            semestrAdapter.Fill(semestrTable);

            semestrBS = new BindingSource();
            semestrBS.DataSource = semestrTable;

            dataGridView1.DataSource = semestrBS;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                semestrBS.EndEdit();
                dataGridView1.Refresh();
                dataGridView1.Parent.Refresh();
                semestrTable.GetChanges();
                dataGridView1.Invalidate();
                dataGridView1.EndEdit();
                semestrAdapter.Update(semestrTable);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                semestrAdapter.Update(semestrTable);
            }
            catch (Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message.ToString());
            }
        }
    }
}
