namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Upload_file
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
            this.pgb_upload = new System.Windows.Forms.ProgressBar();
            this.lbl_progress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgb_upload
            // 
            this.pgb_upload.Location = new System.Drawing.Point(12, 12);
            this.pgb_upload.Name = "pgb_upload";
            this.pgb_upload.Size = new System.Drawing.Size(257, 23);
            this.pgb_upload.TabIndex = 0;
            // 
            // lbl_progress
            // 
            this.lbl_progress.AutoSize = true;
            this.lbl_progress.Location = new System.Drawing.Point(275, 17);
            this.lbl_progress.Name = "lbl_progress";
            this.lbl_progress.Size = new System.Drawing.Size(21, 13);
            this.lbl_progress.TabIndex = 1;
            this.lbl_progress.Text = "0%";
            // 
            // Upload_file
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 50);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_progress);
            this.Controls.Add(this.pgb_upload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Upload_file";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guardando Archivos";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Upload_file_FormClosing);
            this.Shown += new System.EventHandler(this.Upload_file_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar pgb_upload;
		private System.Windows.Forms.Label lbl_progress;
	}
}