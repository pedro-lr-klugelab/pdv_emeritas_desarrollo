namespace Farmacontrol_PDV.FORMS.cortes.tipos_pagos
{
    partial class tipos_pagos_original
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
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.bntAceptarCambio = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFormasPago = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxPagosDisp = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Escanear el ticket :";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(168, 20);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(130, 20);
            this.txtTicket.TabIndex = 1;
            this.txtTicket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTicket_KeyDown);
            // 
            // bntAceptarCambio
            // 
            this.bntAceptarCambio.Location = new System.Drawing.Point(87, 171);
            this.bntAceptarCambio.Name = "bntAceptarCambio";
            this.bntAceptarCambio.Size = new System.Drawing.Size(75, 23);
            this.bntAceptarCambio.TabIndex = 2;
            this.bntAceptarCambio.Text = "Cambiar";
            this.bntAceptarCambio.UseVisualStyleBackColor = true;
            this.bntAceptarCambio.Visible = false;
            this.bntAceptarCambio.Click += new System.EventHandler(this.bntAceptarCambio_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(214, 171);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pago en el ticket:";
            // 
            // lblFormasPago
            // 
            this.lblFormasPago.AutoSize = true;
            this.lblFormasPago.Location = new System.Drawing.Point(165, 63);
            this.lblFormasPago.Name = "lblFormasPago";
            this.lblFormasPago.Size = new System.Drawing.Size(39, 13);
            this.lblFormasPago.TabIndex = 5;
            this.lblFormasPago.Text = "********";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pagos disponible :";
            // 
            // cbxPagosDisp
            // 
            this.cbxPagosDisp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPagosDisp.Enabled = false;
            this.cbxPagosDisp.FormattingEnabled = true;
            this.cbxPagosDisp.Location = new System.Drawing.Point(168, 100);
            this.cbxPagosDisp.Name = "cbxPagosDisp";
            this.cbxPagosDisp.Size = new System.Drawing.Size(130, 21);
            this.cbxPagosDisp.TabIndex = 22;
            // 
            // tipos_pagos_original
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 221);
            this.Controls.Add(this.cbxPagosDisp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFormasPago);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.bntAceptarCambio);
            this.Controls.Add(this.txtTicket);
            this.Controls.Add(this.label1);
            this.Name = "tipos_pagos_original";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambio de pago";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.Button bntAceptarCambio;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFormasPago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxPagosDisp;
    }
}