namespace FMS_PNP
{
    partial class FMS_login
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
            this.Username = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txt_login_username = new MetroFramework.Controls.MetroTextBox();
            this.txt_login_password = new MetroFramework.Controls.MetroTextBox();
            this.btn_login = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(76, 92);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(68, 19);
            this.Username.TabIndex = 0;
            this.Username.Text = "Username";
            this.Username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(76, 137);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(63, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Password";
            // 
            // txt_login_username
            // 
            this.txt_login_username.Location = new System.Drawing.Point(175, 92);
            this.txt_login_username.Name = "txt_login_username";
            this.txt_login_username.Size = new System.Drawing.Size(127, 23);
            this.txt_login_username.TabIndex = 2;
            // 
            // txt_login_password
            // 
            this.txt_login_password.Location = new System.Drawing.Point(175, 137);
            this.txt_login_password.Name = "txt_login_password";
            this.txt_login_password.PasswordChar = '*';
            this.txt_login_password.Size = new System.Drawing.Size(127, 23);
            this.txt_login_password.TabIndex = 3;
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(175, 184);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(127, 23);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "Login";
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // FMS_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 250);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_login_password);
            this.Controls.Add(this.txt_login_username);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.Username);
            this.Name = "FMS_login";
            this.Text = "Finance Managment System Login";
            this.Load += new System.EventHandler(this.FMS_login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel Username;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txt_login_username;
        private MetroFramework.Controls.MetroTextBox txt_login_password;
        private MetroFramework.Controls.MetroButton btn_login;
    }
}

