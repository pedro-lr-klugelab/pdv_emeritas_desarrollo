namespace Farmacontrol_PDV.FORMS.ventas.consulta_ventas
{
	partial class Folio_consulta
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
            this.txt_venta_id = new System.Windows.Forms.TextBox();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Escanear ticket de venta o capturar folio";
            // 
            // txt_venta_id
            // 
            this.txt_venta_id.Location = new System.Drawing.Point(78, 21);
            this.txt_venta_id.Name = "txt_venta_id";
            this.txt_venta_id.Size = new System.Drawing.Size(164, 20);
            this.txt_venta_id.TabIndex = 1;
            this.txt_venta_id.UseSystemPasswordChar = true;
            this.txt_venta_id.TextChanged += new System.EventHandler(this.txt_venta_id_TextChanged);
            this.txt_venta_id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_venta_id_KeyDown);
            this.txt_venta_id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_venta_id_KeyPress);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(248, 19);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 2;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Folio_consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 49);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.txt_venta_id);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Folio_consulta";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_venta_id;
		private System.Windows.Forms.Button btn_cancelar;
	}
}