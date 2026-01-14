namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	partial class Importar_traspaso_venta
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
			this.label1 = new System.Windows.Forms.Label();
			this.txt_folio = new System.Windows.Forms.TextBox();
			this.btn_cancelar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Folio:";
			// 
			// txt_folio
			// 
			this.txt_folio.Location = new System.Drawing.Point(50, 6);
			this.txt_folio.Name = "txt_folio";
			this.txt_folio.Size = new System.Drawing.Size(331, 20);
			this.txt_folio.TabIndex = 1;
			this.txt_folio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_folio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_folio_KeyDown);
			// 
			// btn_cancelar
			// 
			this.btn_cancelar.Location = new System.Drawing.Point(387, 4);
			this.btn_cancelar.Name = "btn_cancelar";
			this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelar.TabIndex = 2;
			this.btn_cancelar.Text = "Cancelar";
			this.btn_cancelar.UseVisualStyleBackColor = true;
			this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
			// 
			// Importar_traspaso_venta
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(474, 31);
			this.ControlBox = false;
			this.Controls.Add(this.btn_cancelar);
			this.Controls.Add(this.txt_folio);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Importar_traspaso_venta";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_folio;
		private System.Windows.Forms.Button btn_cancelar;
	}
}