namespace Farmacontrol_PDV.FORMS.reportes.valuacion_inventario
{
	partial class Valuacion_inventario_principal
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btn_abrir = new System.Windows.Forms.Button();
			this.btn_cancelar = new System.Windows.Forms.Button();
			this.folder_dialog = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Reporte guardado en:";
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(12, 25);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(392, 34);
			this.textBox1.TabIndex = 1;
			// 
			// btn_abrir
			// 
			this.btn_abrir.Location = new System.Drawing.Point(223, 65);
			this.btn_abrir.Name = "btn_abrir";
			this.btn_abrir.Size = new System.Drawing.Size(106, 23);
			this.btn_abrir.TabIndex = 2;
			this.btn_abrir.Text = "Abrir Reporte";
			this.btn_abrir.UseVisualStyleBackColor = true;
			this.btn_abrir.Click += new System.EventHandler(this.btn_abrir_Click);
			// 
			// btn_cancelar
			// 
			this.btn_cancelar.Location = new System.Drawing.Point(335, 65);
			this.btn_cancelar.Name = "btn_cancelar";
			this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelar.TabIndex = 3;
			this.btn_cancelar.Text = "Cancelar";
			this.btn_cancelar.UseVisualStyleBackColor = true;
			this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
			// 
			// folder_dialog
			// 
			this.folder_dialog.Description = "Seleccionar la carpeta donde se guardará el reporte";
			// 
			// Valuacion_inventario_principal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(422, 100);
			this.ControlBox = false;
			this.Controls.Add(this.btn_cancelar);
			this.Controls.Add(this.btn_abrir);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Valuacion_inventario_principal";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Shown += new System.EventHandler(this.Valuacion_inventario_principal_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btn_abrir;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.FolderBrowserDialog folder_dialog;
	}
}