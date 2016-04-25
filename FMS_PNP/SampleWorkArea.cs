using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMS_PNP
{
    public partial class SampleWorkArea : MetroForm
    {
        public SampleWorkArea()
        {
            InitializeComponent();
        }

        private void SampleWorkArea_Load(object sender, EventArgs e)
        {
           

        }

        private void loan_profile_cname_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
        }
    }
}
