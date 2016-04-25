namespace FMS_PNP
{
    partial class SampleWorkArea
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
            this.components = new System.ComponentModel.Container();
            this.customermasterBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.fMSDataSet2 = new FMS_PNP.FMSDataSet2();
            this.fMSDataSet = new FMS_PNP.FMSDataSet();
            this.customer_masterTableAdapter = new FMS_PNP.FMSDataSetTableAdapters.customer_masterTableAdapter();
            this.fMSDataSet1 = new FMS_PNP.FMSDataSet1();
            this.customer_masterTableAdapter1 = new FMS_PNP.FMSDataSet1TableAdapters.customer_masterTableAdapter();
            this.customer_masterTableAdapter2 = new FMS_PNP.FMSDataSet2TableAdapters.customer_masterTableAdapter();
            this.collection_grpBox = new System.Windows.Forms.GroupBox();
            this.metroTextBox8 = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.customermasterBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fMSDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fMSDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fMSDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroTextBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // customermasterBindingSource2
            // 
            this.customermasterBindingSource2.DataMember = "customer_master";
            this.customermasterBindingSource2.DataSource = this.fMSDataSet2;
            // 
            // fMSDataSet2
            // 
            this.fMSDataSet2.DataSetName = "FMSDataSet2";
            this.fMSDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fMSDataSet
            // 
            this.fMSDataSet.DataSetName = "FMSDataSet";
            this.fMSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customer_masterTableAdapter
            // 
            this.customer_masterTableAdapter.ClearBeforeFill = true;
            // 
            // fMSDataSet1
            // 
            this.fMSDataSet1.DataSetName = "FMSDataSet1";
            this.fMSDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customer_masterTableAdapter1
            // 
            this.customer_masterTableAdapter1.ClearBeforeFill = true;
            // 
            // customer_masterTableAdapter2
            // 
            this.customer_masterTableAdapter2.ClearBeforeFill = true;
            // 
            // collection_grpBox
            // 
            this.collection_grpBox.Location = new System.Drawing.Point(63, 63);
            this.collection_grpBox.Name = "collection_grpBox";
            this.collection_grpBox.Size = new System.Drawing.Size(738, 332);
            this.collection_grpBox.TabIndex = 3;
            this.collection_grpBox.TabStop = false;
            this.collection_grpBox.Text = "Report";
            // 
            // metroTextBox8
            // 
            this.metroTextBox8.Owner = null;
            // 
            // SampleWorkArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 576);
            this.Controls.Add(this.collection_grpBox);
            this.Name = "SampleWorkArea";
            this.Text = "SampleWorkArea";
            this.Load += new System.EventHandler(this.SampleWorkArea_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customermasterBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fMSDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fMSDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fMSDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroTextBox8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private FMSDataSet fMSDataSet;
        private FMSDataSetTableAdapters.customer_masterTableAdapter customer_masterTableAdapter;
        private FMSDataSet1 fMSDataSet1;
        private FMSDataSet1TableAdapters.customer_masterTableAdapter customer_masterTableAdapter1;
        private FMSDataSet2 fMSDataSet2;
        private System.Windows.Forms.BindingSource customermasterBindingSource2;
        private FMSDataSet2TableAdapters.customer_masterTableAdapter customer_masterTableAdapter2;
        private System.Windows.Forms.GroupBox collection_grpBox;
        private MetroFramework.Components.MetroStyleManager metroTextBox8;
    }
}