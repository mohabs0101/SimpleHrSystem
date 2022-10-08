using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace user_control
{
    public partial class profile : Form
    {
        public static string Constr = @"Data Source= ;User ID=sa;Password= ";

        //public static string Constr = @"Data Source=DESKTOP-QPN7OMM\SQLEXPRESS;Initial Catalog=HR;User ID=sa;Password=2872222";

        int EmpID = 0;

        int g;
        public profile(int IDS  , int ff)
        {
            EmpID = IDS;
            g = ff;

            InitializeComponent();
          
            
        }

        private void profile_Load(object sender, EventArgs e)
        {
            int imgId = 0;
            SqlConnection con = new SqlConnection(Constr);

            con.Open();

            string SqlDept = @"select  * from [HR].[dbo].[Departments]";
            SqlDataAdapter adapter1 = new SqlDataAdapter(SqlDept, con);
            DataTable tablec = new DataTable();
            adapter1.Fill(tablec);

            if (tablec.Rows.Count > 0)
            {
                foreach (DataRow row in tablec.Rows)
                {
                    DeptCombo.Items.Add(row["department_name"].ToString());
                    DeptComboID.Items.Add(row["department_id"].ToString());
                }
            }



 
            string Sql = String.Format(@"select t1.[employee_id]
      ,t1.[first_name]
      ,t1.[last_name]
      ,t1.[email]
      ,t1.[phone_number]
      ,t1.[salary]
      ,t1.[commission_pct]
      ,t1.[department_id]
     , t1.[job_id]
      ,t1.[street_address]
      ,t1.[postal_code]
      ,t1.[state_province]
      ,t1.images_id
	  ,t2.department_name
	  ,t3.job_title
	  from [HR].[dbo].[employees] T1
	  join [HR].[dbo].[departments] T2 On (T1.department_id = T2.department_id)
	  Join [HR].[dbo].[jobs] T3 on (t1.job_id = t3.job_id)  Where T1.[employee_id] ={0}", EmpID);

 
            SqlDataAdapter adapter = new SqlDataAdapter(Sql, con);
            DataTable table1 = new DataTable();

            adapter.Fill(table1);

            //dataGridView1.DataSource = table1;

            if (table1.Rows.Count != 0)
            {
                foreach (DataRow row in table1.Rows)
                {

                    FirstnameTxt.Text = row["first_name"].ToString();
                    LastnameTxt.Text = row["last_name"].ToString();
                    Email_txtt.Text = row["email"].ToString();
                    Phone_txt.Text = row["phone_number"].ToString();
                    salarTxt.Text = row["salary"].ToString();
                    CommissionTxt.Text = row["commission_pct"].ToString();
                    str_addressTxt.Text = row["street_address"].ToString();
                    postalTxt.Text = row["postal_code"].ToString();
                    stateTxt.Text = row["state_province"].ToString();
                    DeptCombo.Text = row["department_name"].ToString();
                    JobCombo.Text = row["job_title"].ToString();
                    imgId =Convert.ToInt32( row["images_id"].ToString());


                }
            }

            string selectimg = String.Format(@"select t7.image_name
from [HR].[dbo].[employees] T1
join [HR].[dbo].[images] T7
On (T1.images_id = T7.images_id) where t1.images_id={0}", imgId);


            SqlCommand command = new SqlCommand(selectimg, con);
            byte[] img = (byte[])command.ExecuteScalar();
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(g);
            f.Show();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            FirstnameTxt.Enabled = true;
            LastnameTxt.Enabled = true;
            Email_txtt.Enabled = true;
            Phone_txt.Enabled = true;
            salarTxt.Enabled = true;
            CommissionTxt.Enabled = true;
            DeptCombo.Enabled = true;
            JobCombo.Enabled = true;
          
            str_addressTxt.Enabled = true;
            stateProviderTxt.Enabled = true;
         
            postalTxt.Enabled = true;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
