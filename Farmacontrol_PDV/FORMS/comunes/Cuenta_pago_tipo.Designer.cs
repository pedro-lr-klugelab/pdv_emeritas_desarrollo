namespace Farmacontrol_PDV.FORMS.comunes
{
    partial class Cuenta_pago_tipo
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
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cuenta = new System.Windows.Forms.TextBox();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.cbb_tipos_pago = new System.Windows.Forms.ComboBox();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuenta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Metodo de pago:";
            // 
            // txt_cuenta
            // 
            this.txt_cuenta.Location = new System.Drawing.Point(106, 43);
            this.txt_cuenta.Name = "txt_cuenta";
            this.txt_cuenta.Size = new System.Drawing.Size(122, 20);
            this.txt_cuenta.TabIndex = 3;
            this.txt_cuenta.Enter += new System.EventHandler(this.txt_cuenta_Enter);
            this.txt_cuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cuenta_KeyDown);
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(184, 78);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 4;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            this.btn_aceptar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_aceptar_KeyDown);
            // 
            // cbb_tipos_pago
            // 
            this.cbb_tipos_pago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_tipos_pago.FormattingEnabled = true;
            this.cbb_tipos_pago.Location = new System.Drawing.Point(106, 16);
            this.cbb_tipos_pago.Name = "cbb_tipos_pago";
            this.cbb_tipos_pago.Size = new System.Drawing.Size(232, 21);
            this.cbb_tipos_pago.TabIndex = 5;
            this.cbb_tipos_pago.DropDownClosed += new System.EventHandler(this.cbb_tipos_pago_DropDownClosed);
            this.cbb_tipos_pago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_tipos_pago_KeyDown);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(265, 78);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 6;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Cuenta_pago_tipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 113);
            this.ControlBox = false;
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.cbb_tipos_pago);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.txt_cuenta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Cuenta_pago_tipo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Cuenta_pago_tipo_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_cuenta;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.ComboBox cbb_tipos_pago;
        private System.Windows.Forms.Button btn_cancelar;
    }
}