using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace user_control
{
    public partial class Form1 : Form
    {
        public static string Constr = @"Data Source= ;User ID=sa;Password= ";
        //public static string Constr = @"Data Source=DESKTOP-QPN7OMM\SQLEXPRESS;Initial Catalog=HR;User ID=sa;Password=2872222";
        int privilege_id_container = 0;

        public Form1( int privilege_id_reciver)
        {

            privilege_id_container = privilege_id_reciver;

            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            //do search when form1 loaded
            SearchBtn.PerformClick();
            //dataGridView1.BackgroundColor = Color.LightGray;
            if( privilege_id_container == 1)
            {



            }




        }

     

    

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //send index of dgv to profile 
            // indexes of dgv matching the database table
            int EmpID = 0;
            int EmpIndex = e.RowIndex;

            EmpID = Convert.ToInt32(dataGridView1.Rows[EmpIndex].Cells[1].Value.ToString());
          
            profile profileForm = new profile(EmpID, privilege_id_container);
            profileForm.Show();
            this.Hide();
        }

        private void AddEmpBtn_Click(object sender, EventArgs e)
        {//go to adding form
            add_users addUserForm = new add_users(privilege_id_container);
            addUserForm.Show();
            this.Hide();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {//select colomins from employees table 
            dataGridView1.Rows.Clear();
            

            SqlConnection con = new SqlConnection(Constr);



            string Filter = "";
            if (UserFirstnameTxt.Text != "")
            {
                Filter = String.Format("Where [first_name] ='{0}'", UserFirstnameTxt.Text);

            }
            else
            {
                Filter = "";

            }

            string selectall = @"SELECT [employee_id]
      ,[first_name]
      ,[last_name]
      ,[email]
      ,[phone_number]
      ,[salary]
     
  FROM [HR].[dbo].[employees]";


            //Environment.Exit(0);



            string SQL = selectall + Filter;

            SqlDataAdapter adapter = new SqlDataAdapter(SQL, con);
            DataTable table1 = new DataTable();

               adapter.Fill(table1);

            //dataGridView1.DataSource = table1;
            //display it in datagridveiw
            if (table1.Rows.Count != 0)
            {
                foreach (DataRow row in table1.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[1].Value = row["employee_id"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = row["first_name"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = row["last_name"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = row["email"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = row["phone_number"].ToString();
                     dataGridView1.Rows[n].Cells[6].Value = row["salary"].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {   
        }

         

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string idies = ""; // متغيير لجمع الارقام السترنكز
                foreach (DataGridViewRow roow in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell chkchecking = roow.Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chkchecking.Value) == true)
                    {
                        idies = idies + "'" + roow.Cells[1].Value.ToString() + "'" + ",";








                    }






                }
                if (string.IsNullOrEmpty(idies))
              
                {
                    MessageBox.Show("please select employees to delete");
                }
                else {


                    SqlConnection con = new SqlConnection(Constr);
                    con.Open();
                    idies = idies.Substring(0, idies.Length - 1);
                    string Sql = String.Format("Delete From [HR].[dbo].[employees] Where employee_id IN ({0})", idies);
                    SqlCommand DeleteEmpCmd = new SqlCommand(Sql, con);

                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show("Are you sure?", "Employee Will Be Deleted", buttons);
                    if (result == DialogResult.Yes)
                    {
                        int x = DeleteEmpCmd.ExecuteNonQuery();
                        
                            dataGridView1.Rows.Clear();
                            SearchBtn.PerformClick();
                            MessageBox.Show("Employee deleted succesfully");
                      
                    }

                    con.Close();
                   


                }
                //idies = idies.Substring(0, idies.Length - 1);

            }
            catch(Exception EX) {



                MessageBox.Show("error happened");
              
            }

        }
    }
}
