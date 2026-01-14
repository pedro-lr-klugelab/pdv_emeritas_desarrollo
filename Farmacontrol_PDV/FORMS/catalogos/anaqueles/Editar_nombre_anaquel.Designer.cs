namespace Farmacontrol_PDV.FORMS.catalogos.anaqueles
{
	partial class Editar_nombre_anaquel
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
			this.txt_nombre = new System.Windows.Forms.TextBox();
			this.btn_guardar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nombre:";
			// 
			// txt_nombre
			// 
			this.txt_nombre.Location = new System.Drawing.Point(62, 11);
			this.txt_nombre.Name = "txt_nombre";
			this.txt_nombre.Size = new System.Drawing.Size(355, 20);
			this.txt_nombre.TabIndex = 1;
			this.txt_nombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_KeyUp);
			// 
			// btn_guardar
			// 
			this.btn_guardar.Location = new System.Drawing.Point(423, 9);
			this.btn_guardar.Name = "btn_guardar";
			this.btn_guardar.Size = new System.Drawing.Size(75, 23);
			this.btn_guardar.TabIndex = 2;
			this.btn_guardar.Text = "Guardar";
			this.btn_guardar.UseVisualStyleBackColor = true;
			this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
			// 
			// Editar_nombre_anaquel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(510, 43);
			this.ControlBox = false;
			this.Controls.Add(this.btn_guardar);
			this.Controls.Add(this.txt_nombre);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Editar_nombre_anaquel";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.Editar_nombre_anaquel_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_nombre;
		private System.Windows.Forms.Button btn_guardar;
	}
}