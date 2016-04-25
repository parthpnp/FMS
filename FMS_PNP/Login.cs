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
    public partial class FMS_login : MetroForm
    {
        public FMS_login()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        private void FMS_login_Load(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from admin", cnn);
                int result = (int)cmd.ExecuteScalar();

                if (result == 0)
                {
                    this.Visible = false;
                    this.Hide();
                    CreateAdmin addAdmin = new CreateAdmin();
                    addAdmin.ShowDialog();
                }
                this.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Something Wrong Happen.");
            }
            finally
            {
                cnn.Close();
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from [admin] where username=@username and password=@password",cnn);

                cmd.Parameters.AddWithValue("username", txt_login_username.Text);
                cmd.Parameters.AddWithValue("password", txt_login_password.Text);

                SqlDataReader rdr = null;

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    Admin_Dashboard dashboard = new Admin_Dashboard();
                    this.Visible = false;
                   // MessageBox.Show("Hello Welcome, " + rdr[3]);
                    dashboard.ShowDialog();

                    this.Visible = true;
                }
                else
                {
                    MessageBox.Show("Wrong username or Password.");
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
    }
}
