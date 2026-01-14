namespace Farmacontrol_PDV.FORMS.reportes.reimpresiones
{
    partial class Motivo_reimpresiones
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtMotivoImpresion = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnreimprimir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Motivo de impresion  : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tengo un codigo : ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(176, 130);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(98, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Obtener un codigo ";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtMotivoImpresion
            // 
            this.txtMotivoImpresion.Location = new System.Drawing.Point(139, 40);
            this.txtMotivoImpresion.Name = "txtMotivoImpresion";
            this.txtMotivoImpresion.Size = new System.Drawing.Size(307, 20);
            this.txtMotivoImpresion.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(139, 90);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(162, 20);
            this.txtCodigo.TabIndex = 4;
            this.txtCodigo.UseSystemPasswordChar = true;
            // 
            // btnreimprimir
            // 
            this.btnreimprimir.Location = new System.Drawing.Point(351, 173);
            this.btnreimprimir.Name = "btnreimprimir";
            this.btnreimprimir.Size = new System.Drawing.Size(75, 23);
            this.btnreimprimir.TabIndex = 5;
            this.btnreimprimir.Text = "Reimprimir";
            this.btnreimprimir.UseVisualStyleBackColor = true;
            this.btnreimprimir.Click += new System.EventHandler(this.btnreimprimir_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(348, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "mostrar";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(327, 93);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Motivo_reimpresiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 220);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnreimprimir);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.txtMotivoImpresion);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Motivo_reimpresiones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Motivo_reimpresiones_Activated);
            this.Load += new System.EventHandler(this.Motivo_reimpresiones_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtMotivoImpresion;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnreimprimir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}