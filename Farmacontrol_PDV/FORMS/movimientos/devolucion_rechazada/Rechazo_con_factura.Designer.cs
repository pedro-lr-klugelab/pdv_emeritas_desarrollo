namespace Farmacontrol_PDV.FORMS.movimientos.devolucion_rechazada
{
	partial class Rechazo_con_factura
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
			this.radio_auditoria = new System.Windows.Forms.RadioButton();
			this.radio_apartado_mercancia = new System.Windows.Forms.RadioButton();
			this.radio_conservarlos = new System.Windows.Forms.RadioButton();
			this.btn_aceptar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(433, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "¿Que deseas hacer con los productos de la devolucion que ahora  estan en tu inven" +
				"tario?";
			// 
			// radio_auditoria
			// 
			this.radio_auditoria.AutoSize = true;
			this.radio_auditoria.Location = new System.Drawing.Point(15, 35);
			this.radio_auditoria.Name = "radio_auditoria";
			this.radio_auditoria.Size = new System.Drawing.Size(150, 17);
			this.radio_auditoria.TabIndex = 1;
			this.radio_auditoria.TabStop = true;
			this.radio_auditoria.Text = "Enviar todo a AUDITORIA";
			this.radio_auditoria.UseVisualStyleBackColor = true;
			// 
			// radio_apartado_mercancia
			// 
			this.radio_apartado_mercancia.AutoSize = true;
			this.radio_apartado_mercancia.Location = new System.Drawing.Point(15, 58);
			this.radio_apartado_mercancia.Name = "radio_apartado_mercancia";
			this.radio_apartado_mercancia.Size = new System.Drawing.Size(359, 17);
			this.radio_apartado_mercancia.TabIndex = 2;
			this.radio_apartado_mercancia.TabStop = true;
			this.radio_apartado_mercancia.Text = "Enviar todo a APARTADO DE MERCANCIA con el tipo DEVOLUCION";
			this.radio_apartado_mercancia.UseVisualStyleBackColor = true;
			// 
			// radio_conservarlos
			// 
			this.radio_conservarlos.AutoSize = true;
			this.radio_conservarlos.Location = new System.Drawing.Point(15, 81);
			this.radio_conservarlos.Name = "radio_conservarlos";
			this.radio_conservarlos.Size = new System.Drawing.Size(379, 17);
			this.radio_conservarlos.TabIndex = 3;
			this.radio_conservarlos.TabStop = true;
			this.radio_conservarlos.Text = "No hacer nada conservando el stock en la sucursal de todos los productos";
			this.radio_conservarlos.UseVisualStyleBackColor = true;
			// 
			// btn_aceptar
			// 
			this.btn_aceptar.Location = new System.Drawing.Point(190, 104);
			this.btn_aceptar.Name = "btn_aceptar";
			this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
			this.btn_aceptar.TabIndex = 4;
			this.btn_aceptar.Text = "Aceptar";
			this.btn_aceptar.UseVisualStyleBackColor = true;
			this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
			// 
			// Rechazo_con_factura
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(459, 139);
			this.ControlBox = false;
			this.Controls.Add(this.btn_aceptar);
			this.Controls.Add(this.radio_conservarlos);
			this.Controls.Add(this.radio_apartado_mercancia);
			this.Controls.Add(this.radio_auditoria);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Rechazo_con_factura";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Productos rechazados";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radio_auditoria;
		private System.Windows.Forms.RadioButton radio_apartado_mercancia;
		private System.Windows.Forms.RadioButton radio_conservarlos;
		private System.Windows.Forms.Button btn_aceptar;
	}
}