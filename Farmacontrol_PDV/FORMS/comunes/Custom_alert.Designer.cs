namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Custom_alert
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
            this.lbl_titulo = new System.Windows.Forms.Label();
            this.lbl_texto = new System.Windows.Forms.Label();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_titulo
            // 
            this.lbl_titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titulo.Location = new System.Drawing.Point(13, 13);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Size = new System.Drawing.Size(431, 29);
            this.lbl_titulo.TabIndex = 0;
            this.lbl_titulo.Text = "label1";
            // 
            // lbl_texto
            // 
            this.lbl_texto.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_texto.ForeColor = System.Drawing.Color.Red;
            this.lbl_texto.Location = new System.Drawing.Point(12, 42);
            this.lbl_texto.Name = "lbl_texto";
            this.lbl_texto.Size = new System.Drawing.Size(432, 89);
            this.lbl_texto.TabIndex = 1;
            this.lbl_texto.Text = "label2";
            this.lbl_texto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_texto.TextChanged += new System.EventHandler(this.lbl_texto_TextChanged);
            this.lbl_texto.VisibleChanged += new System.EventHandler(this.lbl_texto_VisibleChanged);
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(190, 140);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 2;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // Custom_alert
            // 
            this.AcceptButton = this.btn_aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 175);
            this.ControlBox = false;
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.lbl_texto);
            this.Controls.Add(this.lbl_titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Custom_alert";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.Custom_alert_Activated);
            this.Load += new System.EventHandler(this.Custom_alert_Load);
            this.Shown += new System.EventHandler(this.Custom_alert_Shown);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_aceptar;
		public System.Windows.Forms.Label lbl_titulo;
		private System.Windows.Forms.Label lbl_texto;


	}
}