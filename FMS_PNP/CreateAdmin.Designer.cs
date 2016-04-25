namespace FMS_PNP
{
    partial class CreateAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.txt_Create_Admin_name = new MetroFramework.Controls.MetroTextBox();
            this.txt_Create_Admin_username = new MetroFramework.Controls.MetroTextBox();
            this.txt_Create_Admin_password = new MetroFramework.Controls.MetroTextBox();
            this.txt_Create_Admin_Cpassword = new MetroFramework.Controls.MetroTextBox();
            this.txt_Create_Admin_email = new MetroFramework.Controls.MetroTextBox();
            this.btn_Create_Admin = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(255, 124);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(98, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "New Username";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(255, 156);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(93, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "New Passowrd";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(255, 190);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(115, 19);
            this.metroLabel3.TabIndex = 2;
            this.metroLabel3.Text = "Confirm Password";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(255, 222);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(56, 19);
            this.metroLabel4.TabIndex = 3;
            this.metroLabel4.Text = "Email Id";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(255, 93);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(88, 19);
            this.metroLabel5.TabIndex = 4;
            this.metroLabel5.Text = "Admin Name";
            // 
            // txt_Create_Admin_name
            // 
            this.txt_Create_Admin_name.Location = new System.Drawing.Point(388, 93);
            this.txt_Create_Admin_name.Name = "txt_Create_Admin_name";
            this.txt_Create_Admin_name.Size = new System.Drawing.Size(133, 23);
            this.txt_Create_Admin_name.TabIndex = 5;
            // 
            // txt_Create_Admin_username
            // 
            this.txt_Create_Admin_username.Location = new System.Drawing.Point(388, 124);
            this.txt_Create_Admin_username.Name = "txt_Create_Admin_username";
            this.txt_Create_Admin_username.Size = new System.Drawing.Size(133, 23);
            this.txt_Create_Admin_username.TabIndex = 6;
            // 
            // txt_Create_Admin_password
            // 
            this.txt_Create_Admin_password.Location = new System.Drawing.Point(388, 156);
            this.txt_Create_Admin_password.Name = "txt_Create_Admin_password";
            this.txt_Create_Admin_password.PasswordChar = '*';
            this.txt_Create_Admin_password.Size = new System.Drawing.Size(133, 23);
            this.txt_Create_Admin_password.TabIndex = 7;
            // 
            // txt_Create_Admin_Cpassword
            // 
            this.txt_Create_Admin_Cpassword.Location = new System.Drawing.Point(388, 190);
            this.txt_Create_Admin_Cpassword.Name = "txt_Create_Admin_Cpassword";
            this.txt_Create_Admin_Cpassword.PasswordChar = '*';
            this.txt_Create_Admin_Cpassword.Size = new System.Drawing.Size(133, 23);
            this.txt_Create_Admin_Cpassword.TabIndex = 8;
            // 
            // txt_Create_Admin_email
            // 
            this.txt_Create_Admin_email.Location = new System.Drawing.Point(388, 222);
            this.txt_Create_Admin_email.Name = "txt_Create_Admin_email";
            this.txt_Create_Admin_email.Size = new System.Drawing.Size(133, 23);
            this.txt_Create_Admin_email.TabIndex = 9;
            // 
            // btn_Create_Admin
            // 
            this.btn_Create_Admin.Location = new System.Drawing.Point(388, 276);
            this.btn_Create_Admin.Name = "btn_Create_Admin";
            this.btn_Create_Admin.Size = new System.Drawing.Size(133, 32);
            this.btn_Create_Admin.TabIndex = 10;
            this.btn_Create_Admin.Text = "Create Account";
            this.btn_Create_Admin.Click += new System.EventHandler(this.btn_Create_Admin_Click);
            // 
            // CreateAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 392);
            this.Controls.Add(this.btn_Create_Admin);
            this.Controls.Add(this.txt_Create_Admin_email);
            this.Controls.Add(this.txt_Create_Admin_Cpassword);
            this.Controls.Add(this.txt_Create_Admin_password);
            this.Controls.Add(this.txt_Create_Admin_username);
            this.Controls.Add(this.txt_Create_Admin_name);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "CreateAdmin";
            this.Text = "Create New Admin Account";
            this.Load += new System.EventHandler(this.CreateAdmin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroTextBox txt_Create_Admin_name;
        private MetroFramework.Controls.MetroTextBox txt_Create_Admin_username;
        private MetroFramework.Controls.MetroTextBox txt_Create_Admin_password;
        private MetroFramework.Controls.MetroTextBox txt_Create_Admin_Cpassword;
        private MetroFramework.Controls.MetroTextBox txt_Create_Admin_email;
        private MetroFramework.Controls.MetroButton btn_Create_Admin;
    }
}