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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void signin_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = textBox1.Text;
                string Password = textBox2.Text;



                string Constr = @"Data Source= ;User ID=sa;Password= ";
                SqlConnection con = new SqlConnection(Constr);

                string Sql = String.Format(@"SELECT [user_id]
      ,[username]
      
      ,[password]
,[privilege_id]
  FROM [HR].[dbo].[users]  where [username] = '{0}' and [password] = '{1}'", Username, Password);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(Sql, con);


                DataTable table1 = new DataTable();

                adapter.Fill(table1);

                //TableUsrالتاكد من كون اليوزر موجود او لا حسب عدد الحقول الموجودة في التيبل 
                //    if (table1.Rows.Count != 0)
                //    {
                //        MessageBox.Show("username OR Password is Exist");

                //    }
                //    else
                //    {
                //        MessageBox.Show("username OR Password is incorrect");

                //    }
                //    con.Close();
                //}

                //string firstname = "";
                //int UserID = 0;
                if (table1.Rows.Count != 0)
                {
                    foreach (DataRow row in table1.Rows)
                    {

                        //string privilege_type = row["privilege_"].ToString();
                      int  privilege_id = Convert.ToInt32(row["privilege_id"].ToString());
                        //}

                        Form1 Main = new Form1(privilege_id);
                        Main.Show();
                        this.Hide();
                    }
                    MessageBox.Show("you log in succssfully ");
                }
                else
                {
                    MessageBox.Show("username OR Password is incorrect");

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
