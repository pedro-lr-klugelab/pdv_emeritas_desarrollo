namespace Farmacontrol_PDV.FORMS.cortes.vales
{
    partial class vales_farmacia
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
            this.btnGenerar = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.NumericUpDown();
            this.lblComentarios = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtImporte)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(341, 206);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(30, 38);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(45, 13);
            this.lblCliente.TabIndex = 1;
            this.lblCliente.Text = "Cliente :";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(135, 31);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(297, 20);
            this.txtCliente.TabIndex = 2;
            // 
            // txtImporte
            // 
            this.txtImporte.DecimalPlaces = 2;
            this.txtImporte.Location = new System.Drawing.Point(12, 206);
            this.txtImporte.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtImporte.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(158, 20);
            this.txtImporte.TabIndex = 4;
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtImporte.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtImporte.Visible = false;
            // 
            // lblComentarios
            // 
            this.lblComentarios.AutoSize = true;
            this.lblComentarios.Location = new System.Drawing.Point(30, 88);
            this.lblComentarios.Name = "lblComentarios";
            this.lblComentarios.Size = new System.Drawing.Size(71, 13);
            this.lblComentarios.TabIndex = 5;
            this.lblComentarios.Text = "Comentarios :";
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(128, 88);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(297, 97);
            this.txtComentarios.TabIndex = 6;
            // 
            // vales_farmacia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 247);
            this.Controls.Add(this.txtComentarios);
            this.Controls.Add(this.lblComentarios);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.btnGenerar);
            this.Name = "vales_farmacia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar vales";
            ((System.ComponentModel.ISupportInitialize)(this.txtImporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.NumericUpDown txtImporte;
        private System.Windows.Forms.Label lblComentarios;
        private System.Windows.Forms.TextBox txtComentarios;
    }
}