using MetroFramework.Forms;
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

namespace FMS_PNP
{
    public partial class CreateAdmin : MetroForm
    {
        public CreateAdmin()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        private void btn_Create_Admin_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_Create_Admin_password.Text.Equals(txt_Create_Admin_Cpassword.Text))
                { 
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into [admin] (name,username,password,emailid) values (@name,@username,@password,@emailid)", cnn);
                cmd.Parameters.AddWithValue("username", txt_Create_Admin_username.Text);
                cmd.Parameters.AddWithValue("name", txt_Create_Admin_name.Text);
                cmd.Parameters.AddWithValue("password", txt_Create_Admin_password.Text);
                cmd.Parameters.AddWithValue("emailid", txt_Create_Admin_email.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Admin Account Created Succesfully.");

                this.Close();
       
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something Wrong Happen.");
            }
            finally
            {
                cnn.Close();
            }
        }

        private void CreateAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
