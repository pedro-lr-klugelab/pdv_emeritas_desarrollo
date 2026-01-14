namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Sin_codigo_postal
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
			this.cbb_estado = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cbb_ciudad = new System.Windows.Forms.ComboBox();
			this.cbb_nombre = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btn_aceptar = new System.Windows.Forms.Button();
			this.btn_cancelar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Estado:";
			// 
			// cbb_estado
			// 
			this.cbb_estado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbb_estado.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cbb_estado.FormattingEnabled = true;
			this.cbb_estado.Location = new System.Drawing.Point(73, 6);
			this.cbb_estado.Name = "cbb_estado";
			this.cbb_estado.Size = new System.Drawing.Size(323, 21);
			this.cbb_estado.TabIndex = 1;
			this.cbb_estado.SelectionChangeCommitted += new System.EventHandler(this.cbb_estado_SelectionChangeCommitted);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Ciudad:";
			// 
			// cbb_ciudad
			// 
			this.cbb_ciudad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbb_ciudad.Enabled = false;
			this.cbb_ciudad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cbb_ciudad.FormattingEnabled = true;
			this.cbb_ciudad.Location = new System.Drawing.Point(73, 33);
			this.cbb_ciudad.Name = "cbb_ciudad";
			this.cbb_ciudad.Size = new System.Drawing.Size(323, 21);
			this.cbb_ciudad.TabIndex = 3;
			this.cbb_ciudad.SelectionChangeCommitted += new System.EventHandler(this.cbb_ciudad_SelectionChangeCommitted);
			this.cbb_ciudad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_ciudad_KeyDown);
			// 
			// cbb_nombre
			// 
			this.cbb_nombre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbb_nombre.Enabled = false;
			this.cbb_nombre.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cbb_nombre.FormattingEnabled = true;
			this.cbb_nombre.Location = new System.Drawing.Point(73, 60);
			this.cbb_nombre.Name = "cbb_nombre";
			this.cbb_nombre.Size = new System.Drawing.Size(323, 21);
			this.cbb_nombre.TabIndex = 5;
			this.cbb_nombre.SelectionChangeCommitted += new System.EventHandler(this.cbb_nombre_SelectionChangeCommitted);
			this.cbb_nombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_nombre_KeyDown);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(22, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Colonia:";
			// 
			// btn_aceptar
			// 
			this.btn_aceptar.Location = new System.Drawing.Point(240, 94);
			this.btn_aceptar.Name = "btn_aceptar";
			this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
			this.btn_aceptar.TabIndex = 8;
			this.btn_aceptar.Text = "Aceptar";
			this.btn_aceptar.UseVisualStyleBackColor = true;
			this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
			// 
			// btn_cancelar
			// 
			this.btn_cancelar.Location = new System.Drawing.Point(321, 94);
			this.btn_cancelar.Name = "btn_cancelar";
			this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelar.TabIndex = 9;
			this.btn_cancelar.Text = "Cancelar";
			this.btn_cancelar.UseVisualStyleBackColor = true;
			this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
			// 
			// Sin_codigo_postal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(408, 129);
			this.Controls.Add(this.btn_cancelar);
			this.Controls.Add(this.btn_aceptar);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbb_nombre);
			this.Controls.Add(this.cbb_ciudad);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cbb_estado);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Sin_codigo_postal";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sin codigo Postal";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.Sin_codigo_postal_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbb_estado;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbb_ciudad;
		private System.Windows.Forms.ComboBox cbb_nombre;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btn_aceptar;
		private System.Windows.Forms.Button btn_cancelar;
	}
}