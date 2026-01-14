namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Registro_terminal
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
			this.txt_uuid = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txt_uuid
			// 
			this.txt_uuid.Location = new System.Drawing.Point(37, 30);
			this.txt_uuid.Name = "txt_uuid";
			this.txt_uuid.Size = new System.Drawing.Size(358, 20);
			this.txt_uuid.TabIndex = 0;
			this.txt_uuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_uuid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_uuid_KeyDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(71, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(280, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Acceso DENEGADO, terminal NO REGISTRADA";
			// 
			// Registro_terminal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(425, 62);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txt_uuid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Registro_terminal";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt_uuid;
		private System.Windows.Forms.Label label2;
	}
}