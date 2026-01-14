namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Cambiar_cantidad
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
			this.lbl_cantidad = new System.Windows.Forms.Label();
			this.txt_cantidad = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lbl_cantidad
			// 
			this.lbl_cantidad.AutoSize = true;
			this.lbl_cantidad.Location = new System.Drawing.Point(12, 9);
			this.lbl_cantidad.Name = "lbl_cantidad";
			this.lbl_cantidad.Size = new System.Drawing.Size(52, 13);
			this.lbl_cantidad.TabIndex = 0;
			this.lbl_cantidad.Text = "Cantidad:";
			// 
			// txt_cantidad
			// 
			this.txt_cantidad.Location = new System.Drawing.Point(70, 6);
			this.txt_cantidad.Name = "txt_cantidad";
			this.txt_cantidad.Size = new System.Drawing.Size(64, 20);
			this.txt_cantidad.TabIndex = 1;
			this.txt_cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
			this.txt_cantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cantidad_KeyPress);
			// 
			// Cambiar_cantidad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(147, 30);
			this.ControlBox = false;
			this.Controls.Add(this.txt_cantidad);
			this.Controls.Add(this.lbl_cantidad);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Cambiar_cantidad";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Cambiar Cantidad";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt_cantidad;
		public System.Windows.Forms.Label lbl_cantidad;
	}
}