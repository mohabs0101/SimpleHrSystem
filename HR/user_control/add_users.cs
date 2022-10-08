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
    public partial class add_users : Form
    {
        public static string Constr = @"Data Source= ;User ID=sa;Password= ";

        //public static string Constr = @"Data Source=DESKTOP-QPN7OMM\SQLEXPRESS;Initial Catalog=HR;User ID=sa;Password=2872222";
        string imgloc = "";
        int f;
        public add_users(int privilege_id_recive)
        {
            f = privilege_id_recive;
            InitializeComponent();
        }

        private void add_users_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Constr);


            //string image_idd = @"select MAX(images_id) from [HR].[dbo].[images]";

            //SqlDataAdapter adapter20 = new SqlDataAdapter(image_idd, con);

            //DataTable table20 = new DataTable();
            //adapter20.Fill(table20);



            //    if (table20.Rows.Count >0)
            //{
            //    foreach (DataRow row in table20.Rows)
            //    {
            //        comboold.Items.Add(Convert.ToInt32(row["column1"].ToString()) + 1);
            //    }
            //    comboold.SelectedIndex = 0;
            //}
            //else {

            //    comboold.Items.Add(Convert.ToInt32(('1')).ToString());


            //}










            string SqlDept = @"select  * from [HR].[dbo].[Departments]";
            SqlDataAdapter adapter1 = new SqlDataAdapter(SqlDept, con);
            DataTable table1 = new DataTable();
            adapter1.Fill(table1);

            if (table1.Rows.Count > 0)
            {
                foreach (DataRow row in table1.Rows)
                {
                    DeptCombo.Items.Add(row["department_name"].ToString());
                    DeptComboID.Items.Add(row["department_id"].ToString());
                }
                DeptCombo.SelectedIndex = 0;
                DeptComboID.SelectedIndex = 0;
            }







            string Sqljobs = @"select * from [HR].[dbo].[jobs]";
            SqlDataAdapter adapter2 = new SqlDataAdapter(Sqljobs, con);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);

            if (table2.Rows.Count != 0)
            {
                foreach (DataRow row in table2.Rows)
                {
                    jobcombo.Items.Add(row["job_title"].ToString());
                    jobcombo_id.Items.Add(row["job_id"].ToString());
                }
                jobcombo.SelectedIndex = 0;
                jobcombo_id.SelectedIndex = 0;
            }
         





            








        }



        private void DeptCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeptComboID.SelectedIndex = DeptCombo.SelectedIndex;
        }
        private void jobcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobcombo.SelectedIndex = jobcombo_id.SelectedIndex;
        }



        private void AddEmpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                

                SqlConnection con = new SqlConnection(Constr);
                con.Open();

                byte[] images = null;

                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);

                BinaryReader brs = new BinaryReader(fs);

                images = brs.ReadBytes((int)fs.Length);

                string sqlquerry = "insert into [HR].[dbo].[images]  ([image_name]) values(@images)";
                SqlCommand addimages = new SqlCommand(sqlquerry, con);

                addimages.Parameters.Add(new SqlParameter("@images", images));

                int n = addimages.ExecuteNonQuery();
                //MessageBox.Show(n.ToString() + "image saves succeefully");


              


                    string Firstname = "", Lastname = "", Email = "", phonenumber = "", salary = "", commission_pct = "", streetadress = "", stateprove = "";

                int DeptID = 0, jobID = 0, pastalcode = 0,imgid=0;
                string GetLastImage = "select max (images_id) as idimg from [HR].[dbo].[images]";
                SqlDataAdapter cmdimg = new SqlDataAdapter(GetLastImage, con);
                DataTable imgtable = new DataTable();
                cmdimg.Fill(imgtable);
                foreach (DataRow row in imgtable.Rows)
                {
                    imgid = Convert.ToInt32( row["idimg"].ToString());
                }

                Firstname = FirstnameTxt.Text;
                Lastname = LastnameTxt.Text;
                phonenumber = phonenTxt.Text;
                Email = emaillTxt.Text;
                salary = salartTxt.Text;
                commission_pct = commissionTxt.Text;

                streetadress = stradrTxt.Text;
                pastalcode = Convert.ToInt32(postalcodeTxt.Text);
                stateprove = stateTxt.Text;

                DeptID = Convert.ToInt32(DeptComboID.SelectedItem.ToString());
                jobID = Convert.ToInt32(jobcombo_id.SelectedItem.ToString());
               
                string SqlAddEmp = String.Format(@"INSERT INTO [HR].[dbo].[employees] 
                                                  ([first_name]
                                                 ,[last_name]
                                                  ,[email]       
                                                  ,[phone_number]
                                                  ,[salary]
                                                  ,[commission_pct]
                                                  ,[street_address]
                                                  ,[postal_code]
                                                  ,[state_province]
                                   

                                                  ,[department_id]
                                                  ,[job_id]
                                                
                                                   ,[images_id]
                                                   )
                 
                                                  VALUES
                                                  ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}',{9},{10},{11})", Firstname, Lastname, Email, phonenumber, salary, commission_pct,streetadress, pastalcode,stateprove, DeptID, jobID,imgid);

               


         
                 SqlCommand AddEmpCmd = new SqlCommand(SqlAddEmp, con);
                int x = AddEmpCmd.ExecuteNonQuery();

                if (x == 1)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show("Do You want To Go Back To main form?", "Employee Added Scsassfully", buttons);
                    if (result == DialogResult.Yes)
                    {
                     
                    }
                    else
                    {
                         


                    }

                }
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }







          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f2 = new Form1(f);
            f2.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.PNG";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                //pictureBox1.Image = new Bitmap(opnfd.FileName);
                imgloc = opnfd.FileName.ToString();
                pictureBox1.ImageLocation = imgloc;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
