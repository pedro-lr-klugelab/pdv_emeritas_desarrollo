namespace Farmacontrol_PDV.FORMS.catalogos.asignacion_ca
{
    partial class Asignacion_ca_principal
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
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_antibiotico = new System.Windows.Forms.ComboBox();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amecop:";
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(75, 12);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(142, 20);
            this.txt_amecop.TabIndex = 0;
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // txt_nombre
            // 
            this.txt_nombre.Enabled = false;
            this.txt_nombre.Location = new System.Drawing.Point(75, 38);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(519, 20);
            this.txt_nombre.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Producto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Antibiotico:";
            // 
            // cbb_antibiotico
            // 
            this.cbb_antibiotico.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbb_antibiotico.FormattingEnabled = true;
            this.cbb_antibiotico.Location = new System.Drawing.Point(75, 64);
            this.cbb_antibiotico.Name = "cbb_antibiotico";
            this.cbb_antibiotico.Size = new System.Drawing.Size(519, 21);
            this.cbb_antibiotico.TabIndex = 2;
            this.cbb_antibiotico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_antibiotico_KeyDown);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(519, 95);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 6;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(438, 95);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 3;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // Asignacion_ca_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 130);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.cbb_antibiotico);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Asignacion_ca_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignacion de Catalogo Antibioticos";
            this.Load += new System.EventHandler(this.Asignacion_ca_principal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_amecop;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_antibiotico;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_aceptar;
    }
}