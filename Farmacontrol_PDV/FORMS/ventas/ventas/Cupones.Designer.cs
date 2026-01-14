namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	partial class Cupones
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
			this.btn_cancelar = new System.Windows.Forms.Button();
			this.txt_codigo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btn_cancelar
			// 
			this.btn_cancelar.Location = new System.Drawing.Point(395, 4);
			this.btn_cancelar.Name = "btn_cancelar";
			this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
			this.btn_cancelar.TabIndex = 5;
			this.btn_cancelar.Text = "Cancelar";
			this.btn_cancelar.UseVisualStyleBackColor = true;
			this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
			// 
			// txt_codigo
			// 
			this.txt_codigo.Location = new System.Drawing.Point(58, 6);
			this.txt_codigo.Name = "txt_codigo";
			this.txt_codigo.Size = new System.Drawing.Size(331, 20);
			this.txt_codigo.TabIndex = 4;
			this.txt_codigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_codigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_codigo_KeyDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Codigo:";
			// 
			// Cupones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(482, 31);
			this.ControlBox = false;
			this.Controls.Add(this.btn_cancelar);
			this.Controls.Add(this.txt_codigo);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Cupones";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.TextBox txt_codigo;
		private System.Windows.Forms.Label label1;
	}
}