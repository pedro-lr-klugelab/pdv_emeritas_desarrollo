namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
    partial class Lote_caducidad
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblCaducidades = new System.Windows.Forms.Label();
            this.lblLotes = new System.Windows.Forms.Label();
            this.txtLotes = new System.Windows.Forms.TextBox();
            this.cmbxMeses = new System.Windows.Forms.ComboBox();
            this.cbxAnio = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(172, 83);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.btnAceptar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnAceptar_KeyDown);
            this.btnAceptar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnAceptar_KeyPress);
            this.btnAceptar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnAceptar_KeyUp);
            // 
            // lblCaducidades
            // 
            this.lblCaducidades.AutoSize = true;
            this.lblCaducidades.Location = new System.Drawing.Point(22, 23);
            this.lblCaducidades.Name = "lblCaducidades";
            this.lblCaducidades.Size = new System.Drawing.Size(64, 13);
            this.lblCaducidades.TabIndex = 2;
            this.lblCaducidades.Text = "Caducidad :";
            // 
            // lblLotes
            // 
            this.lblLotes.AutoSize = true;
            this.lblLotes.Location = new System.Drawing.Point(169, 23);
            this.lblLotes.Name = "lblLotes";
            this.lblLotes.Size = new System.Drawing.Size(34, 13);
            this.lblLotes.TabIndex = 3;
            this.lblLotes.Text = "Lote :";
            // 
            // txtLotes
            // 
            this.txtLotes.Enabled = false;
            this.txtLotes.Location = new System.Drawing.Point(172, 39);
            this.txtLotes.Name = "txtLotes";
            this.txtLotes.Size = new System.Drawing.Size(100, 20);
            this.txtLotes.TabIndex = 4;
            this.txtLotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLotes_KeyDown);
            this.txtLotes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLotes_KeyPress);
            // 
            // cmbxMeses
            // 
            this.cmbxMeses.Enabled = false;
            this.cmbxMeses.FormattingEnabled = true;
            this.cmbxMeses.Location = new System.Drawing.Point(25, 39);
            this.cmbxMeses.Name = "cmbxMeses";
            this.cmbxMeses.Size = new System.Drawing.Size(48, 21);
            this.cmbxMeses.TabIndex = 5;
            this.cmbxMeses.Enter += new System.EventHandler(this.cmbxMeses_Enter);
            this.cmbxMeses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbxMeses_KeyDown);
            // 
            // cbxAnio
            // 
            this.cbxAnio.Enabled = false;
            this.cbxAnio.FormattingEnabled = true;
            this.cbxAnio.Location = new System.Drawing.Point(89, 39);
            this.cbxAnio.Name = "cbxAnio";
            this.cbxAnio.Size = new System.Drawing.Size(45, 21);
            this.cbxAnio.TabIndex = 6;
            this.cbxAnio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxAnio_KeyDown);
            // 
            // Lote_caducidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 119);
            this.Controls.Add(this.cbxAnio);
            this.Controls.Add(this.cmbxMeses);
            this.Controls.Add(this.txtLotes);
            this.Controls.Add(this.lblLotes);
            this.Controls.Add(this.lblCaducidades);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Lote_caducidad";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lote y caducidad";
            this.Load += new System.EventHandler(this.Lote_caducidad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblCaducidades;
        private System.Windows.Forms.Label lblLotes;
        private System.Windows.Forms.TextBox txtLotes;
        private System.Windows.Forms.ComboBox cmbxMeses;
        private System.Windows.Forms.ComboBox cbxAnio;

    }
}