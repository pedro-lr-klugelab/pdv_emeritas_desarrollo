namespace Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad
{
    partial class Programa_lealtad_principal
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtTarjetaLealtad = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(134, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Escanear tarjeta : ";
            // 
            // txtTarjetaLealtad
            // 
            this.txtTarjetaLealtad.Location = new System.Drawing.Point(99, 132);
            this.txtTarjetaLealtad.Name = "txtTarjetaLealtad";
            this.txtTarjetaLealtad.PasswordChar = '*';
            this.txtTarjetaLealtad.Size = new System.Drawing.Size(279, 20);
            this.txtTarjetaLealtad.TabIndex = 3;
            this.txtTarjetaLealtad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTarjetaLealtad.UseSystemPasswordChar = true;
            this.txtTarjetaLealtad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTarjetaLealtad_KeyDown);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(209, 223);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(303, 223);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(75, 23);
            this.btnContinuar.TabIndex = 5;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // Programa_lealtad_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 281);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtTarjetaLealtad);
            this.Controls.Add(this.label2);
            this.Name = "Programa_lealtad_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enlace Vital ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTarjetaLealtad;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnContinuar;
    }
}