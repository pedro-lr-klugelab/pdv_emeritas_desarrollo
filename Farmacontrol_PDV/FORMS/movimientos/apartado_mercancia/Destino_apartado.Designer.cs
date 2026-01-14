namespace Farmacontrol_PDV.FORMS.movimientos.apartado_mercancia
{
	partial class Destino_apartado
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
			this.cbb_destino = new System.Windows.Forms.ComboBox();
			this.btn_aceptar = new System.Windows.Forms.Button();
			this.btn_cancelar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Destino:";
			// 
			// cbb_destino
			// 
			this.cbb_destino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbb_destino.FormattingEnabled = true;
			this.cbb_destino.Location = new System.Drawing.Point(64, 8);
			this.cbb_destino.Name = "cbb_destino";
			this.cbb_destino.Size = new System.Drawing.Size(434, 21);
			this.cbb_destino.TabIndex = 1;
			this.cbb_destino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_destino_KeyDown);
			// 
			// btn_aceptar
			// 
			this.btn_aceptar.Location = new System.Drawing.Point(504, 6);
			this.btn_aceptar.Name = "btn_aceptar";
			this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
			this.btn_aceptar.TabIndex = 2;
			this.btn_aceptar.Text = "Aceptar";
			this.btn_aceptar.UseVisualStyleBackColor = true;
			this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
			// 
			// btn_cancelar
			// 
			this.btn_cancelar.Location = new System.Drawing.Point(585, 6);
			this.btn_cancelar.Name = "btn_cancelar";
			this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelar.TabIndex = 3;
			this.btn_cancelar.Text = "Cancelar";
			this.btn_cancelar.UseVisualStyleBackColor = true;
			this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
			// 
			// Destino_apartado
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(675, 37);
			this.ControlBox = false;
			this.Controls.Add(this.btn_cancelar);
			this.Controls.Add(this.btn_aceptar);
			this.Controls.Add(this.cbb_destino);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Destino_apartado";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.Destino_apartado_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbb_destino;
		private System.Windows.Forms.Button btn_aceptar;
		private System.Windows.Forms.Button btn_cancelar;
	}
}