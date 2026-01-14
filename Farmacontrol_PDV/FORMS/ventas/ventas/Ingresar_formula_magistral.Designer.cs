namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	partial class Ingresar_formula_magistral
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
			this.txt_formula = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Escanea el ticket de la formula:";
			// 
			// txt_formula
			// 
			this.txt_formula.Location = new System.Drawing.Point(173, 8);
			this.txt_formula.Name = "txt_formula";
			this.txt_formula.Size = new System.Drawing.Size(187, 20);
			this.txt_formula.TabIndex = 1;
			this.txt_formula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_formula_KeyDown);
			this.txt_formula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_formula_KeyPress);
			// 
			// Ingresar_formula_magistral
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(372, 36);
			this.ControlBox = false;
			this.Controls.Add(this.txt_formula);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Ingresar_formula_magistral";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_formula;
	}
}