using iTextSharp.text;
using iTextSharp.text.pdf;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMS_PNP
{
    public partial class Admin_Dashboard : MetroForm
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        String imglocation = null;
        String g_imglocation = null;

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fMSDataSet6.loan' table. You can move, or remove it, as needed.
            this.loanTableAdapter.Fill(this.fMSDataSet6.loan);
            // TODO: This line of code loads data into the 'fMSDataSet5.customer_master' table. You can move, or remove it, as needed.
            this.customer_masterTableAdapter2.Fill(this.fMSDataSet5.customer_master);
            // TODO: This line of code loads data into the 'trancation_new.transaction' table. You can move, or remove it, as needed.
            this.transactionTableAdapter.Fill(this.trancation_new.transaction);
            // TODO: This line of code loads data into the 'fMSDataSet11.customer_master' table. You can move, or remove it, as needed.
            this.customer_masterTableAdapter1.Fill(this.fMSDataSet11.customer_master);
            try
            {
                Loan_Profile_grp.Visible = false;
                collection_grpBox.Visible = false;
                AddExpenceGrpBox.Visible = false;
                AddAmountGrpBox.Visible = false;
            // TODO: This line of code loads data into the 'fMSDataSet3.customer_master' table. You can move, or remove it, as needed.
            this.customer_masterTableAdapter.Fill(this.fMSDataSet3.customer_master);
            AddExpenceGrpBox.Visible = false;
            AddAmountGrpBox.Visible = false;
            issue_loan.Visible = false;
            

            
                    cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from statics", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                if(rdr[0].ToString()=="0")
                {
                    optPanel_grpBox.Visible = false;
                    DisplayPanel.Visible = false;
                }
                else
                {
                    Amount_RemianLbl.Text = rdr[0].ToString();
                    Amount_givenLbl.Text = rdr[1].ToString();
                    ProfitLbl.Text = rdr[2].ToString();
                    Expence_LBL.Text = rdr[3].ToString();
                    Net_profit.Text =Convert.ToString(Convert.ToDouble(rdr[2].ToString()) - Convert.ToDouble(rdr[3].ToString()));
                    optPanel_grpBox.Visible = true;
                    DisplayPanel.Visible = true;
                }
                rdr.Close();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            String tabledate=" ";
            //cheacking update_date latest date-------DAILY------------------------------>
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select date from update_date where table_type='daily'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                tabledate = rdr[0].ToString();
                rdr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

           
            if(tabledate!=DateTime.Today.ToString("dd/MM/yyyy"))
            { 
            //Creating Daily Outstad Table ------------------------------------------------------>
            
            try
            {
                //cheaking data in daily_outstand-------------------------------------------------- >

                //Cheacking kona baki -------------------------------->
                cnn.Open();
                SqlCommand cmd2 = new SqlCommand("select * from daily_outstand", cnn);
                SqlDataReader rdr2 = cmd2.ExecuteReader();
                List<string> baki = new List<string>();
                int i1 = 0;
                String date = "00/00/0000";
                while (rdr2.Read())
                {
                    baki.Add(rdr2[0].ToString());
                    date = rdr2[1].ToString();
                    i1++;
                }
                
                rdr2.Close();
                String today = DateTime.Today.ToString("dd/MM/yyyy");

                //cheacking toady date is not equal tabel date then & only then procude new table---------------------------------->
                //if (date != today)
                //{

                    //filling penalty ------------------jemna baki hoy ----------------------------------->
                    if (i1 != 0)
                    {
                        int n1 = 0;
                        while (n1 < i1)
                        {

                            //caculating new penalty & new remaing days----------------------------->

                            SqlCommand cmd3 = new SqlCommand("select * from loan where loan_id='" + baki[n1].ToString() + "'", cnn);
                            SqlDataReader rdr3 = null;
                            rdr3 = cmd3.ExecuteReader();
                            rdr3.Read();
                            int pr = Convert.ToInt32(rdr3[3].ToString());
                            int old_penalty = Convert.ToInt32(rdr3[8].ToString());
                            int old_days = Convert.ToInt32(rdr3[9].ToString());
                            int emi = Convert.ToInt32(rdr3[2].ToString());
                            int new_penalty = ((emi * pr) / 100) + old_penalty + emi;
                            int new_days = old_days - 1;
                            rdr3.Close();


                            //writng back the penalty & new remaining days-------------------------->

                            SqlCommand cmd4 = new SqlCommand("update loan SET penalty_amount=@penalty , remain_days=@days where loan_id=@lid", cnn);
                            cmd4.Parameters.AddWithValue("penalty", new_penalty);
                            cmd4.Parameters.AddWithValue("days", new_days);
                            cmd4.Parameters.AddWithValue("lid", baki[n1]);
              
                            cmd4.ExecuteNonQuery();


                            //deleting lid entry from daily_outstad-------------------------------->

                            SqlCommand cmd5 = new SqlCommand("delete from daily_outstand where lid='" + baki[n1] +"'", cnn);
                            cmd5.ExecuteNonQuery();

                            //moving forward to next recoerd
                            n1++;
                        }
                    }

                    //filling NEW data------------------------------------------------------------------->

                    SqlCommand cmd = new SqlCommand("select loan_id,cid from [loan] where type='daily' and status='running'", cnn);
                    SqlDataReader rdr = null;
                    //SqlDataAdapter dt = null;
                    rdr = cmd.ExecuteReader();

                    List<string> lid_list = new List<string>();
                    List<string> cid_list = new List<string>();

                    int i = 0, n = 0;
                    while (rdr.Read())
                    {
                        lid_list.Add(rdr[0].ToString());
                        cid_list.Add(rdr[1].ToString());
                        i++;
                    }

                    rdr.Close();
                    while (n < i)
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into daily_outstand (lid,date,cid) values (@lid,@date,@cid)", cnn);
                        cmd1.Parameters.AddWithValue("lid", lid_list[n].ToString());
                        cmd1.Parameters.AddWithValue("date", DateTime.Today.ToString("dd/MM/yyyy"));
                        cmd1.Parameters.AddWithValue("cid", cid_list[n].ToString());
                        cmd1.ExecuteNonQuery();
                        n++;
                    }

                        //putting entry into update_date table

                        SqlCommand cmd6 = new SqlCommand("update update_date SET date='"+DateTime.Today.ToString("dd/MM/yyyy")+"' where table_type='daily'", cnn);
                        cmd6.ExecuteNonQuery();
                }   // date!=today condition closes here
            //}
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + " daily_outstand");
            }
            finally
            {
                cnn.Close();
            }
            }

            // monthly table creation--------------------------------------------------------------------->

            String tabledate1 = " ";
            //cheacking update_date latest date-------DAILY------------------------------>
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select date from update_date where table_type='monthly'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                tabledate1 = rdr[0].ToString();
                
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

          

            if (tabledate1.Substring(3,2) != DateTime.Today.ToString("dd/MM/yyyy").Substring(3,2))
            {
                //Creating Daily Outstad Table ------------------------------------------------------>

                try
                {
                    //cheaking data in daily_outstand-------------------------------------------------- >

                    //Cheacking kona baki -------------------------------->
                    cnn.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from monthly_outstand", cnn);
                    SqlDataReader rdr2 = cmd2.ExecuteReader();
                    List<string> baki = new List<string>();
                    int i1 = 0;
                    String date = "00/00/0000";
                    while (rdr2.Read())
                    {
                        baki.Add(rdr2[0].ToString());
                        date = rdr2[1].ToString();
                        i1++;
                    }

                    rdr2.Close();
                    String today = DateTime.Today.ToString("dd/MM/yyyy");

                    //cheacking toady date is not equal tabel date then & only then procude new table---------------------------------->
                   // if (date != today)
                   // {

                        //filling penalty ------------------jemna baki hoy ----------------------------------->
                        if (i1 != 0)
                        {
                            int n1 = 0;
                            while (n1 < i1)
                            {

                                //caculating new penalty & new remaing days----------------------------->

                                SqlCommand cmd3 = new SqlCommand("select * from loan where loan_id='" + baki[n1].ToString() + "'", cnn);
                                SqlDataReader rdr3 = null;
                                rdr3 = cmd3.ExecuteReader();
                                rdr3.Read();
                                int pr = Convert.ToInt32(rdr3[3].ToString());
                                int old_penalty = Convert.ToInt32(rdr3[9].ToString());
                                int old_days = Convert.ToInt32(rdr3[10].ToString());
                                int emi = Convert.ToInt32(rdr3[2].ToString());
                                int new_penalty = (emi * pr) / 100 + old_penalty + emi;
                                int new_days = old_days - 1;
                                rdr3.Close();


                                //writng back the penalty & new remaining days-------------------------->

                                SqlCommand cmd4 = new SqlCommand("update loan SET penalty_amount=@penalty , remain_days=@days where loan_id=@lid", cnn);
                                cmd4.Parameters.AddWithValue("penalty", new_penalty);
                                cmd4.Parameters.AddWithValue("days", new_days);
                                cmd4.Parameters.AddWithValue("lid", baki[n1]);

                                cmd4.ExecuteNonQuery();


                                //deleting lid entry from daily_outstad-------------------------------->

                                SqlCommand cmd5 = new SqlCommand("delete from monthly_outstand where lid='" + baki[n1] + "'", cnn);
                                cmd5.ExecuteNonQuery();

                                //moving forward to next recoerd
                                n1++;
                            }
                        }

                        //filling NEW data------------------------------------------------------------------->

                        SqlCommand cmd = new SqlCommand("select loan_id,cid from [loan] where type='monthly' and status='running'", cnn);
                        SqlDataReader rdr = null;
                        //SqlDataAdapter dt = null;
                        rdr = cmd.ExecuteReader();

                        List<string> lid_list = new List<string>();
                        List<string> cid_list = new List<string>();

                        int i = 0, n = 0;
                        while (rdr.Read())
                        {
                            lid_list.Add(rdr[0].ToString());
                            cid_list.Add(rdr[1].ToString());
                            i++;
                        }

                        rdr.Close();
                        while (n < i)
                        {
                            SqlCommand cmd1 = new SqlCommand("insert into monthly_outstand (lid,date,cid) values (@lid,@date,@cid)", cnn);
                            cmd1.Parameters.AddWithValue("lid", lid_list[n].ToString());
                            cmd1.Parameters.AddWithValue("date", DateTime.Today.ToString("dd/MM/yyyy"));
                            cmd1.Parameters.AddWithValue("cid", cid_list[n].ToString());
                            cmd1.ExecuteNonQuery();
                            n++;
                        }

                        //putting entry into update_date table

                        SqlCommand cmd6 = new SqlCommand("update update_date SET date='" + DateTime.Today.ToString("dd/MM/yyyy") + "' where table_type='monthly'", cnn);
                        cmd6.ExecuteNonQuery();
                    }   // date!=today condition closes here
              //  }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " monthly_outstand");
                }
                finally
                {
                    cnn.Close();
                }
            }


        }

        private void btn_add_customer_Click(object sender, EventArgs e)
        {
            customer_profile_grpBox.Visible = false;
            report_grpBox.Visible = false;
            Loan_Profile_grp.Visible = false;
            group_add_customer.Visible = true;
            issue_loan.Visible = false;
            AddAmountGrpBox.Visible = false;
            collection_grpBox.Visible = false;
        }


        private void btn_Create_customer_Click_1(object sender, EventArgs e)
        {
            byte[] img = null;
            FileStream fs = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);


            byte[] g_img = null;
            FileStream g_fs = new FileStream(g_imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader g_br = new BinaryReader(g_fs);
            g_img = g_br.ReadBytes((int)g_fs.Length);


            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("insert into [customer_master] (customer_name,gender,mobile,address,emailid,occupation,id_proof_name,id_proof_img,g_name,g_mobile,g_address,g_id_proof_name,g_id_proof_img) values (@customer_name,@gender,@mobile,@address,@emailid,@occupation,@id_proof_name,@id_proof_img,@g_name,@g_mobile,@g_address,@g_id_proof_name,@g_id_proof_img)", cnn);
                cmd.Parameters.AddWithValue("customer_name", new_customer_name.Text);
                cmd.Parameters.AddWithValue("gender", gender_combo.Text);
                cmd.Parameters.AddWithValue("mobile", new_customer_mobile.Text);
                cmd.Parameters.AddWithValue("address", new_customer_address.Text);
                cmd.Parameters.AddWithValue("emailid", new_customer_emailid.Text);
                cmd.Parameters.AddWithValue("occupation", new_customer_occupation.Text);
                cmd.Parameters.AddWithValue("id_proof_name", new_customer_id_proof_combo.Text);
                cmd.Parameters.AddWithValue("id_proof_img", img);
                cmd.Parameters.AddWithValue("g_name", new_g_name.Text);
                cmd.Parameters.AddWithValue("g_mobile", new_g_mobile.Text);
                cmd.Parameters.AddWithValue("g_address", new_g_address.Text);
                cmd.Parameters.AddWithValue("g_id_proof_name", new_g_id_name.Text);
                cmd.Parameters.AddWithValue("g_id_proof_img", g_img);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Account Created Successfull.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void customer_upload_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Files(*.jpg) | *.jpg |All Files (*.*) |*.*";
            openFileDialog1.Title = "Selecte your profile picture";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imglocation = openFileDialog1.FileName.ToString();
                new_customer_img.Text = imglocation;
            }
        }

        private void g_upload_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Files(*.jpg) | *.jpg |All Files (*.*) |*.*";
            openFileDialog1.Title = "Selecte your profile picture";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                g_imglocation = openFileDialog1.FileName.ToString();
                new_g_img.Text = g_imglocation;
            }
        }

        private void statastic_controler_CheckedChanged(object sender, EventArgs e)
        {
            if (statics.Visible == false)
            {
                statics.Visible = true;
            }
            else
            {
                statics.Visible = false;
            }
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void collection_Btn_Click(object sender, EventArgs e)
        {
            customer_profile_grpBox.Visible = false;
            report_grpBox.Visible = false;
            Loan_Profile_grp.Visible = false;
            group_add_customer.Visible = false;
            collection_grpBox.Visible = true;
            issue_loan.Visible = false;
            AddAmountGrpBox.Visible = false;
            customer_id_collection.Text = customer_name_collection.SelectedValue.ToString();


        }



        private void metroButton3_Click(object sender, EventArgs e)
        {
            AddAmountGrpBox.Visible = true;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            AddExpenceGrpBox.Visible = true;
        }

        private void AddAmount_Btn_Click(object sender, EventArgs e)
        {
            try {
                cnn.Open();
                SqlCommand cmd1 = new SqlCommand("select amount_remain from statics",cnn);
                SqlDataReader rdr = null;
                rdr = cmd1.ExecuteReader();
                //rdr.Read();
                double master_amount;
                    rdr.Read();
                double amount = Convert.ToDouble(rdr[0].ToString()) ;
                rdr.Close();
                master_amount = amount + Convert.ToDouble(AddAmountText.Text);
                SqlCommand cmd2 = new SqlCommand("update statics SET amount_remain ='" + master_amount + "' where id='1';", cnn);
                cmd2.ExecuteNonQuery();
                Amount_RemianLbl.Text = master_amount.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void optPanel_grpBox_Enter(object sender, EventArgs e)
        {

        }

        private void AddExpence_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd1 = new SqlCommand("select expence from statics", cnn);
                SqlDataReader rdr = null;
                rdr = cmd1.ExecuteReader();
                //rdr.Read();
                double master_expence;
                rdr.Read();
                double expence = Convert.ToDouble(rdr[0].ToString());
                master_expence = expence + Convert.ToDouble(AddExpenceText.Text);
                SqlCommand cmd = new SqlCommand("update statics SET expence ='" + master_expence + "' where id='1';", cnn);
                cmd.ExecuteNonQuery();
                Expence_LBL.Text = master_expence.ToString();
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void DisplayPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void issue_loan_Enter(object sender, EventArgs e)
        {

        }

        private void customer_name_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer_id.Text = customer_name_combo.SelectedValue.ToString();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            customer_profile_grpBox.Visible = false;
            report_grpBox.Visible = false;
            Loan_Profile_grp.Visible = false;
            issue_loan.Visible = true;
            AddAmountGrpBox.Visible = false;
            collection_grpBox.Visible = false;
            group_add_customer.Visible = false;
            customer_id.Text = customer_name_combo.SelectedValue.ToString();
        }

        private void daily_calculate_Click(object sender, EventArgs e)
        {
            daily_emi.Text = (((Convert.ToInt32(daily_amount.Text) * Convert.ToInt32(daily_ir.Text)) / 100 + Convert.ToInt32(daily_amount.Text)) / Convert.ToInt32(daily_inst.Text)).ToString();
        }

        private void daily_issue_loan_Click(object sender, EventArgs e)
        {
            try
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("insert into [loan] (ir,inst,emi,pr,cid,type,status,penalty_amount,remain_days,loan_amount) values (@ir,@inst,@emi,@pr,@cid,@type,@status,@penalty_amount,@remain_days,@loan_amount)", cnn);
                cmd.Parameters.AddWithValue("ir", daily_ir.Text);
                cmd.Parameters.AddWithValue("inst", daily_inst.Text);
                cmd.Parameters.AddWithValue("emi", daily_emi.Text);
                cmd.Parameters.AddWithValue("pr", daily_pr.Text);
                cmd.Parameters.AddWithValue("cid", customer_id.Text);
                cmd.Parameters.AddWithValue("type", "daily");
                cmd.Parameters.AddWithValue("status", "running");
                cmd.Parameters.AddWithValue("penalty_amount", "0");
                cmd.Parameters.AddWithValue("remain_days", daily_inst.Text);
                cmd.Parameters.AddWithValue("loan_amount", daily_amount.Text);
                //cmd.Parameters.AddWithValue("");
                cmd.ExecuteNonQuery();

                MessageBox.Show("Loan Added");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void monthly_issue_loan_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("insert into [loan] (ir,inst,emi,pr,cid,type,status,penalty_amount,remain_days,loan_amount) values (@ir,@inst,@emi,@pr,@cid,@type,@status,@penalty_amount,@remain_days,@loan_amount)", cnn);
                cmd.Parameters.AddWithValue("ir", monthly_ir.Text);
                cmd.Parameters.AddWithValue("inst", monthly_inst.Text);
                cmd.Parameters.AddWithValue("emi", monthly_emi.Text);
                cmd.Parameters.AddWithValue("pr", monthly_pr.Text);
                cmd.Parameters.AddWithValue("cid", customer_id.Text);
                cmd.Parameters.AddWithValue("type", "monthly");
                cmd.Parameters.AddWithValue("status", "running");
                cmd.Parameters.AddWithValue("penalty_amount", "0");
                cmd.Parameters.AddWithValue("remain_days",monthly_inst.Text);
                cmd.Parameters.AddWithValue("loan_amount", monthly_amount.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Loan Added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }



        private void btnCollect_collection_Click(object sender, EventArgs e)
        {
            if (loan_type_collection.SelectedItem.ToString() == "Daily")
            {
                try
                {
                    cnn.Open();

                    //inserting Entry into Trancation table ------------------------------------>

                    SqlCommand cmd = new SqlCommand("insert into [transaction] (lid,date,paid_amount) values (@lid,@date,@paid_amount)", cnn);
                    cmd.Parameters.AddWithValue("lid", select_load_collection.SelectedItem.ToString());

                    cmd.Parameters.AddWithValue("date", DateTime.Today.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("paid_amount", collecting_amount_collection.Text);

                    cmd.ExecuteNonQuery();

                    // deleting record from the daily_outstand --------------------------------------------->

                    SqlCommand cmd1 = new SqlCommand("delete from daily_outstand where lid='" + select_load_collection.SelectedItem.ToString() + "'", cnn);
                    cmd1.ExecuteNonQuery();


                    //updating values in loan table
                    SqlCommand cmd2 = new SqlCommand("select penalty_amount,remain_days,emi,pr from loan where loan_id='" + select_load_collection.SelectedItem.ToString() + "'", cnn);
                    SqlDataReader rdr = null;

                    rdr = cmd2.ExecuteReader();
                    rdr.Read();

                    int old_penalty = Convert.ToInt32(rdr[0].ToString());
                    int old_days = Convert.ToInt32(rdr[1].ToString());
                    int emi = Convert.ToInt32(rdr[2].ToString());
                    int pr = Convert.ToInt32(rdr[3].ToString());
                    int new_penalty = 0;
                    int new_days = old_days - 1;
                    rdr.Close();

                    if (emi == Convert.ToInt32(collecting_amount_collection.Text))
                    {
                        new_penalty = old_penalty;
                    }
                    else if (emi < Convert.ToInt32(collecting_amount_collection.Text))
                    {
                        new_penalty = old_penalty - (Convert.ToInt32(collecting_amount_collection.Text)-emi);
                    }
                    else if (emi > Convert.ToInt32(collecting_amount_collection.Text))
                    {
                        int remain = (emi - Convert.ToInt32(collecting_amount_collection.Text));
                        new_penalty = old_penalty + ((remain * pr) / 100) + remain;
                    }


                    SqlCommand cmd3 = new SqlCommand("update loan SET remain_days=@new_days, penalty_amount=@new_penalty where loan_id=@lid ", cnn);
                    cmd3.Parameters.AddWithValue("new_days", new_days);
                    cmd3.Parameters.AddWithValue("new_penalty", new_penalty);
                    cmd3.Parameters.AddWithValue("lid", select_load_collection.SelectedItem.ToString());
                    cmd3.ExecuteNonQuery();
                    rdr.Close();
                    MessageBox.Show("Installment Collected..!!");


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }
            }
            else
            {
                try { 
                cnn.Open();

                //inserting Entry into Trancation table ------------------------------------>

                SqlCommand cmd = new SqlCommand("insert into [transaction] (lid,date,paid_amount) values (@lid,@date,@paid_amount)", cnn);
                cmd.Parameters.AddWithValue("lid", select_load_collection.SelectedItem.ToString());

                cmd.Parameters.AddWithValue("date", DateTime.Today.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("paid_amount", collecting_amount_collection.Text);

                cmd.ExecuteNonQuery();

                // deleting record from the daily_outstand --------------------------------------------->

                SqlCommand cmd1 = new SqlCommand("delete from monthly_outstand where lid='" + select_load_collection.SelectedItem.ToString() + "'", cnn);
                cmd1.ExecuteNonQuery();


                //updating values in loan table
                SqlCommand cmd2 = new SqlCommand("select penalty_amount,remain_days,emi,pr from loan where loan_id='" + select_load_collection.SelectedItem.ToString() + "'", cnn);
                SqlDataReader rdr = null;

                rdr = cmd2.ExecuteReader();
                rdr.Read();

                int old_penalty = Convert.ToInt32(rdr[0].ToString());
                int old_days = Convert.ToInt32(rdr[1].ToString());
                int emi = Convert.ToInt32(rdr[2].ToString());
                int pr = Convert.ToInt32(rdr[3].ToString());
                int new_penalty = 0;
                int new_days = old_days - 1;
                rdr.Close();

                if (emi == Convert.ToInt32(collecting_amount_collection.Text))
                {
                    new_penalty = old_penalty;
                }
                else if (emi < Convert.ToInt32(collecting_amount_collection.Text))
                {
                    new_penalty = old_penalty - Convert.ToInt32(collecting_amount_collection.Text);
                }
                else if (emi > Convert.ToInt32(collecting_amount_collection.Text))
                {
                    int remain = (emi - Convert.ToInt32(collecting_amount_collection.Text));
                    new_penalty = old_penalty + ((remain * pr) / 100) + remain;
                }


                SqlCommand cmd3 = new SqlCommand("update loan SET remain_days=@new_days, penalty_amount=@new_penalty where loan_id=@lid ", cnn);
                cmd3.Parameters.AddWithValue("new_days", new_days);
                cmd3.Parameters.AddWithValue("new_penalty", new_penalty);
                cmd3.Parameters.AddWithValue("lid", select_load_collection.SelectedItem.ToString());
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Installment Collected..!!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }
        }



        private void loan_type_collection_SelectedIndexChanged(object sender, EventArgs e)
        {
            String loan_type = loan_type_collection.SelectedItem.ToString();
            select_load_collection.Items.Clear();
            if (loan_type == "Daily")
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("select * from daily_outstand where cid='" + customer_id_collection.Text + "'", cnn);
                    SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        select_load_collection.Items.Add(rdr[0]);
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }

            }
            else if (loan_type == "Monthly")
            {
                try { 

                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from monthly_outstand where cid='" + customer_id_collection.Text + "'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    select_load_collection.Items.Add(rdr[0]);
                }
                rdr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cnn.Close();
                }
            
            }
        }

        private void select_load_collection_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                String str = select_load_collection.SelectedItem.ToString();

                SqlCommand cmd = new SqlCommand("select * from loan where loan_id='" + str + "'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                outstand_amount_collection.Text = (Convert.ToInt32(rdr[2].ToString()) + Convert.ToInt32(rdr[8].ToString())).ToString();
                collecting_amount_collection.Text = outstand_amount_collection.Text;
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void loan_profile_cname_SelectedIndexChanged(object sender, EventArgs e)
        {
            loan_profile_loan.Items.Clear();
            loan_profile_cid.Text = loan_profile_cname.SelectedValue.ToString();

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select loan_id from loan where cid='" + loan_profile_cid.Text + "'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    loan_profile_loan.Items.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            report_lbl.Visible = false;
            customer_profile_grpBox.Visible = false;
            report_grpBox.Visible = true;
            Loan_Profile_grp.Visible = false;
            group_add_customer.Visible = false;
            issue_loan.Visible = false;
            AddAmountGrpBox.Visible = false;
            collection_grpBox.Visible = false;
            try {
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void metroTile1_Click_1(object sender, EventArgs e)
        {
            customer_profile_grpBox.Visible = false;
            report_grpBox.Visible = false;
            loan_profile_loan.Items.Clear();
            Loan_Profile_grp.Visible = true;
            issue_loan.Visible = false;
            AddAmountGrpBox.Visible = false;
            group_add_customer.Visible = false;
            collection_grpBox.Visible = false;
        }

        private void loan_profile_loan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from loan where loan_id=@lid", cnn);
                cmd.Parameters.AddWithValue("lid", loan_profile_loan.SelectedItem.ToString());

                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();

                loan_profile_ir.Text = rdr[0].ToString();
                loan_profile_inst.Text = rdr[1].ToString();
                loan_profile_emi.Text = rdr[2].ToString();
                loan_profile_pr.Text = rdr[3].ToString();
                loan_profile_type.Text = rdr[6].ToString();
                loan_profile_status.Text = rdr[7].ToString();
                loan_profile_penalty.Text = rdr[8].ToString();
                loan_profile_remain_days.Text = rdr[9].ToString();
                loan_profile_amount.Text = rdr[10].ToString();

                loan_profile_remain_amount.Text = (Convert.ToInt32(rdr[9].ToString()) * Convert.ToInt32(rdr[2].ToString())).ToString();
                rdr.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

        }

        private void customer_name_collection_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer_id_collection.Text = customer_name_collection.SelectedValue.ToString();

        }

        private void monthly_calculate_Click(object sender, EventArgs e)
        {
            monthly_emi.Text = ((((Convert.ToInt32(monthly_amount.Text) * Convert.ToInt32(monthly_ir.Text)) / 100) + Convert.ToInt32(monthly_amount.Text)) / Convert.ToInt32(monthly_inst.Text)).ToString();
        }

   
        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer_profile_loans_combo.Items.Clear();
            customer_profile_cid.Text = customer_profile_cnmae_combo.SelectedValue.ToString();

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select loan_id from loan where cid='" + customer_profile_cid.Text + "'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customer_profile_loans_combo.Items.Add(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from customer_master where user_id=@cid", cnn);
                cmd.Parameters.AddWithValue("cid", customer_profile_cid.Text);

                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();

                customer_profile_gender.Text = rdr[2].ToString();
                customer_profile_mobile.Text = rdr[3].ToString();
                customer_profile_address.Text = rdr[4].ToString();
                customer_profile_emailid.Text = rdr[5].ToString();
                customer_profile_occupation.Text = rdr[6].ToString();
                customer_profile_idName.Text = rdr[7].ToString();

                byte[] img = (byte[])(rdr[8]);
                MemoryStream ms = new MemoryStream(img);
                customer_profile_idImg.Image = System.Drawing.Image.FromStream(ms);

                customer_profile_g_name.Text = rdr[9].ToString();
                customer_profile_g_mobile.Text = rdr[10].ToString();
                customer_profile_g_address.Text = rdr[11].ToString();
                customer_profile_g_idName.Text = rdr[12].ToString();

                byte[] img1 = (byte[])(rdr[13]);
                MemoryStream ms1 = new MemoryStream(img1);
                customer_profile_g_idImg.Image = System.Drawing.Image.FromStream(ms1);
                rdr.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            
        }

        private void btn_updateCustomer_Info_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("update customer_master set gender=@gender, mobile=@mobile, address=@address, emailid=@emailid, occupation=@occupation, g_name=@g_name, g_mobile=@g_mobile, g_address=@g_address where user_id=@cid", cnn);
                cmd.Parameters.AddWithValue("gender", customer_profile_gender.Text);
                cmd.Parameters.AddWithValue("mobile", customer_profile_mobile.Text);
                cmd.Parameters.AddWithValue("address", customer_profile_address.Text);
                cmd.Parameters.AddWithValue("emailid", customer_profile_emailid.Text);
                cmd.Parameters.AddWithValue("occupation", customer_profile_occupation.Text);
                //cmd.Parameters.AddWithValue("id_proof_name", customer_profile_idName.Text);

                //byte[] img = null;
                //FileStream fs = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                //BinaryReader br = new BinaryReader(fs);
                //img = br.ReadBytes((int)fs.Length);


                //byte[] g_img = null;
                //FileStream g_fs = new FileStream(g_imglocation, FileMode.Open, FileAccess.Read);
                //BinaryReader g_br = new BinaryReader(g_fs);
                //g_img = g_br.ReadBytes((int)g_fs.Length);

                //cmd.Parameters.AddWithValue("id_proof_img", img);
                //cmd.Parameters.AddWithValue("g_id_proof_img", g_img);

                //cmd.Parameters.AddWithValue("g_id_proof_name", customer_profile_g_idName.Text);
                cmd.Parameters.AddWithValue("g_name", customer_profile_g_name.Text);
                cmd.Parameters.AddWithValue("g_mobile", customer_profile_g_mobile.Text);
                cmd.Parameters.AddWithValue("g_address", customer_profile_g_address.Text);

                cmd.Parameters.AddWithValue("cid", customer_profile_cid.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Information Updated Successfully..!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            customer_profile_grpBox.Visible = true;
            report_grpBox.Visible = false;
            Loan_Profile_grp.Visible = false;
            group_add_customer.Visible = false;
            issue_loan.Visible = false;
            AddAmountGrpBox.Visible = false;
            collection_grpBox.Visible = false;
        }

        private void group_add_customer_Enter(object sender, EventArgs e)
        {

        }


        private void full_payment_loan_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand ("select remain_days,emi,penalty_amount from loan where loan_id=@loan_id",cnn);
                cmd.Parameters.AddWithValue("loan_id", full_payment_loan_combo.SelectedItem.ToString());
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                rdr.Read();
                full_payment_amount.Text = (Convert.ToInt32(rdr[0].ToString()) * Convert.ToInt32(rdr[1].ToString())+Convert.ToInt32(rdr[2].ToString())).ToString();
                rdr.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void full_payment_cname_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            full_payment_loan_combo.Items.Clear();
            full_payment_cid.Text = full_payment_cname.SelectedValue.ToString();

            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select loan_id from loan where cid='" + full_payment_cid.Text + "'", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    full_payment_loan_combo.Items.Add(rdr[0].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void btn_collect_full_payment_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                //SqlCommand cmd = new SqlCommand ("select * ",cnn)
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void report_transacation_Click(object sender, EventArgs e)
        {
            report_lbl.Visible = true;
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from [transaction]", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                FileStream fs = new FileStream(@"C:\Users\Parth\Desktop\Transaction.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                PdfPTable table = new PdfPTable(3);
                Document doc = new Document();

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.AddTitle("Transaction Summery");
                doc.Open();
                PdfPCell cell = new PdfPCell(new Phrase("Transaction Summery"));
                cell.Colspan = 3;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);
                //  var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                String date1 = dateTimePicker1.Value.Date.ToString();
                String date2 = dateTimePicker2.Value.ToShortDateString();
                rdr.Read();
                while (rdr.Read())
                {

                    if (DateTime.Parse(rdr[1].ToString()) >= DateTime.Parse(date1) && DateTime.Parse(rdr[1].ToString()) <= DateTime.Parse(date2))
                    {
                        table.AddCell(rdr[0].ToString());
                        table.AddCell(rdr[1].ToString());
                        table.AddCell(rdr[2].ToString());
                       
                    }

                }
                doc.Add(table);
                doc.Close();
                rdr.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void loan_summery_Click(object sender, EventArgs e)
        {
            try {
                String loanid = loan_list.SelectedValue.ToString();
            cnn.Open();
            SqlCommand cmd = new SqlCommand("select * from [transaction] where lid=@lid", cnn);
            cmd.Parameters.AddWithValue("lid", loanid);
                MessageBox.Show(loanid);
            SqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();
            FileStream fs = new FileStream(@"C:\Users\Parth\Desktop\Loan_transaction2.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            PdfPTable table = new PdfPTable(3);
            Document doc = new Document();

            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.AddTitle("Transaction Summery");
            doc.Open();
            PdfPCell cell = new PdfPCell(new Phrase("Transaction Summery"));
            cell.Colspan = 3;

            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            table.AddCell(cell);
            //  var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            table.AddCell("Loan ID");
            table.AddCell("Date of Transaction");
            table.AddCell("Paid Amount");
            while (rdr.Read())
            {
                    table.AddCell(rdr[0].ToString());
                    table.AddCell(rdr[1].ToString());
                    table.AddCell(rdr[2].ToString());
                    
                }
                doc.Add(table);
                doc.Close();
            rdr.Close();

        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void loan_sum_Click(object sender, EventArgs e)
        {
           String lid= loan_list.SelectedValue.ToString();
            try { 
            cnn.Open();
            SqlCommand cmd = new SqlCommand("select * from [loan] where loan_id=@lid", cnn);
            cmd.Parameters.AddWithValue("lid", lid);
            MessageBox.Show(lid);
            SqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();
            FileStream fs = new FileStream(@"C:\Users\Parth\Desktop\Loan_summry3.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            PdfPTable table = new PdfPTable(11);
            Document doc = new Document();

            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.AddTitle("Transaction Summery");
            doc.Open();
            PdfPCell cell = new PdfPCell(new Phrase("Loan Summery"));
            cell.Colspan = 11;

            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

            table.AddCell(cell);
            //  var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            table.AddCell("Interest Rate");
            table.AddCell("Installment");
            table.AddCell("EMI");
            table.AddCell("Penalty Rate");
            table.AddCell("Customer ID");
                table.AddCell("Loan Type");
                table.AddCell("Loan Status");
                table.AddCell("Penalty Amount");
                table.AddCell("Remaining Days");
                table.AddCell("Loan Amount");
                table.AddCell("Loan ID");
                while (rdr.Read())
            {
                table.AddCell(rdr[0].ToString());
                table.AddCell(rdr[1].ToString());
                table.AddCell(rdr[2].ToString());
                    table.AddCell(rdr[3].ToString());
                    table.AddCell(rdr[4].ToString());
                    table.AddCell(rdr[6].ToString());
                    table.AddCell(rdr[7].ToString());
                    table.AddCell(rdr[8].ToString());
                    table.AddCell(rdr[9].ToString());
                    table.AddCell(rdr[10].ToString());
                    table.AddCell(rdr[11].ToString());

            }
                doc.Add(table);
            doc.Close();
            rdr.Close();

        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void Cutomer_Summry_Click(object sender, EventArgs e)
        {
            string cid = customer_list.SelectedValue.ToString();
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from [customer_master] where user_id=@cid", cnn);
                cmd.Parameters.AddWithValue("cid", cid);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                FileStream fs = new FileStream(@"C:\Users\Parth\Desktop\customer_summry.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                PdfPTable table = new PdfPTable(7);
                Document doc = new Document();

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.AddTitle("Transaction Summery");
                doc.Open();
                PdfPCell cell = new PdfPCell(new Phrase("Transaction Summery"));
                cell.Colspan = 7;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);
                //  var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                table.AddCell("Customer Name");
                table.AddCell("Gender");
                table.AddCell("Mobile");
                table.AddCell("Address");
                table.AddCell("Email Id");
                table.AddCell("Occupation");
                table.AddCell("Customer ID");
                while (rdr.Read())
                {
                    table.AddCell(rdr[1].ToString());
                    table.AddCell(rdr[2].ToString());
                    table.AddCell(rdr[3].ToString());
                    table.AddCell(rdr[4].ToString());
                    table.AddCell(rdr[5].ToString());
                    table.AddCell(rdr[6].ToString());
                    table.AddCell(rdr[14].ToString());              
                }
                doc.Add(table);
                doc.Close();
                rdr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }
        List<String> lid = new List<String>();
        List<String> cid = new List<String>();

        private void collection_list_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from [daily_outstand]", cnn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                FileStream fs = new FileStream(@"C:\Users\Parth\Desktop\collection_list.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                PdfPTable table = new PdfPTable(7);
                Document doc = new Document();
                
               
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.AddTitle("Today Collection List");
                doc.Open();
                PdfPCell cell = new PdfPCell(new Phrase("Transaction Summery"));
                cell.Colspan = 7;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                
                table.AddCell(cell);
                //  var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                table.AddCell("Loan ID");
                table.AddCell("Customer ID");
                table.AddCell("Customer Name");
                table.AddCell("Customer Mobile");
                table.AddCell("EMI");
                table.AddCell("Penalty");
                table.AddCell("Remaining Days");
                while(rdr.Read())
                {
                    lid.Add(rdr[0].ToString());
                    cid.Add(rdr[2].ToString());
                }
                rdr.Close();

                int n = lid.Count;
                int i = 0;
                while (n>0)
                {

                    table.AddCell(lid[i].ToString());            
                    table.AddCell(cid[i].ToString());
                    try {
                        SqlCommand cmd2 = new SqlCommand("select customer_name,mobile from customer_master where user_id=@cid", cnn);
                        cmd2.Parameters.AddWithValue("cid", cid[i].ToString());                    
                        SqlDataReader rdr2 = null;
                        rdr2 = cmd2.ExecuteReader();
                        rdr2.Read();
                        table.AddCell(rdr2[0].ToString());
                        table.AddCell(rdr2[1].ToString());
                        rdr2.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    try
                    {
                        SqlCommand cmd3 = new SqlCommand("select emi,penalty_amount,remain_days from loan where loan_id=@lid", cnn);
                        cmd3.Parameters.AddWithValue("lid", lid[i].ToString());
                        SqlDataReader rdr3 = null;
                        rdr3 = cmd3.ExecuteReader();
                        rdr3.Read();
                        table.AddCell(rdr3[0].ToString());
                        table.AddCell(rdr3[1].ToString());
                        table.AddCell(rdr3[2].ToString());
                        rdr3.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    i++;
                    n--;
                }
               

                SqlCommand cmd1 = new SqlCommand("select * from [monthly_outstand]",cnn);
                SqlDataReader rdr1 = null;
                rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    table.AddCell(rdr1[0].ToString());
                    table.AddCell(rdr1[2].ToString());
                }
                doc.Add(table);
                doc.Close();
                rdr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
 