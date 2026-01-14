namespace Farmacontrol_PDV.FORMS.ventas.cancelar_prepago
{
    partial class Cancelar_prepago_principal
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
            this.txt_codigo_prepago = new System.Windows.Forms.TextBox();
            this.btn_caeptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo del prepago:";
            // 
            // txt_codigo_prepago
            // 
            this.txt_codigo_prepago.Location = new System.Drawing.Point(123, 15);
            this.txt_codigo_prepago.Name = "txt_codigo_prepago";
            this.txt_codigo_prepago.Size = new System.Drawing.Size(335, 20);
            this.txt_codigo_prepago.TabIndex = 1;
            this.txt_codigo_prepago.UseSystemPasswordChar = true;
            this.txt_codigo_prepago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ttx_codigo_prepago_KeyDown);
            // 
            // btn_caeptar
            // 
            this.btn_caeptar.Location = new System.Drawing.Point(464, 13);
            this.btn_caeptar.Name = "btn_caeptar";
            this.btn_caeptar.Size = new System.Drawing.Size(75, 23);
            this.btn_caeptar.TabIndex = 2;
            this.btn_caeptar.Text = "Aceptar";
            this.btn_caeptar.UseVisualStyleBackColor = true;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(545, 13);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            // 
            // Cancelar_prepago_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 49);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_caeptar);
            this.Controls.Add(this.txt_codigo_prepago);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cancelar_prepago_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelar Prepago";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_codigo_prepago;
        private System.Windows.Forms.Button btn_caeptar;
        private System.Windows.Forms.Button btn_cancelar;
    }
}