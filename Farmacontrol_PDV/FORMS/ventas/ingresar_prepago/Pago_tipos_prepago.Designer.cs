namespace Farmacontrol_PDV.FORMS.ventas.ingresar_prepago
{
	partial class Pago_tipos_prepago
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
			this.lbl_total_pagar = new System.Windows.Forms.Label();
			this.lbl_total = new System.Windows.Forms.Label();
			this.btn_cancelar = new System.Windows.Forms.Button();
			this.btn_generar_prepago = new System.Windows.Forms.Button();
			this.chb_confirmacion = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lbl_total_pagar
			// 
			this.lbl_total_pagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_total_pagar.ForeColor = System.Drawing.Color.Red;
			this.lbl_total_pagar.Location = new System.Drawing.Point(65, 33);
			this.lbl_total_pagar.Name = "lbl_total_pagar";
			this.lbl_total_pagar.Size = new System.Drawing.Size(253, 42);
			this.lbl_total_pagar.TabIndex = 24;
			this.lbl_total_pagar.Text = "$9,999,999.99";
			this.lbl_total_pagar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_total
			// 
			this.lbl_total.AutoSize = true;
			this.lbl_total.Location = new System.Drawing.Point(12, 9);
			this.lbl_total.Name = "lbl_total";
			this.lbl_total.Size = new System.Drawing.Size(73, 13);
			this.lbl_total.TabIndex = 23;
			this.lbl_total.Text = "Total a pagar:";
			// 
			// btn_cancelar
			// 
			this.btn_cancelar.Location = new System.Drawing.Point(301, 110);
			this.btn_cancelar.Name = "btn_cancelar";
			this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelar.TabIndex = 21;
			this.btn_cancelar.Text = "Cancelar";
			this.btn_cancelar.UseVisualStyleBackColor = true;
			this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
			// 
			// btn_generar_prepago
			// 
			this.btn_generar_prepago.Enabled = false;
			this.btn_generar_prepago.Location = new System.Drawing.Point(182, 110);
			this.btn_generar_prepago.Name = "btn_generar_prepago";
			this.btn_generar_prepago.Size = new System.Drawing.Size(111, 23);
			this.btn_generar_prepago.TabIndex = 19;
			this.btn_generar_prepago.Text = "Generar prepago";
			this.btn_generar_prepago.UseVisualStyleBackColor = true;
			this.btn_generar_prepago.Click += new System.EventHandler(this.btn_generar_prepago_Click);
			// 
			// chb_confirmacion
			// 
			this.chb_confirmacion.AutoSize = true;
			this.chb_confirmacion.Location = new System.Drawing.Point(15, 81);
			this.chb_confirmacion.Name = "chb_confirmacion";
			this.chb_confirmacion.Size = new System.Drawing.Size(169, 17);
			this.chb_confirmacion.TabIndex = 25;
			this.chb_confirmacion.Text = "He recibido el pago del cliente";
			this.chb_confirmacion.UseVisualStyleBackColor = true;
			this.chb_confirmacion.CheckedChanged += new System.EventHandler(this.chb_confirmacion_CheckedChanged);
			// 
			// Pago_tipos_prepago
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 145);
			this.ControlBox = false;
			this.Controls.Add(this.chb_confirmacion);
			this.Controls.Add(this.lbl_total_pagar);
			this.Controls.Add(this.lbl_total);
			this.Controls.Add(this.btn_cancelar);
			this.Controls.Add(this.btn_generar_prepago);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MinimizeBox = false;
			this.Name = "Pago_tipos_prepago";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_total_pagar;
		private System.Windows.Forms.Label lbl_total;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.Button btn_generar_prepago;
		private System.Windows.Forms.CheckBox chb_confirmacion;
	}
}