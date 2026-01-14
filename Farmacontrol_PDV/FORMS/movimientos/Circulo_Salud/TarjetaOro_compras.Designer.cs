namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    partial class TarjetaOro_compras
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgCompras = new System.Windows.Forms.DataGridView();
            this.bntAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.Location = new System.Drawing.Point(143, 25);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(255, 22);
            this.txtTarjeta.TabIndex = 1;
            this.txtTarjeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarjeta_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tarjeta";
            // 
            // dtgCompras
            // 
            this.dtgCompras.AllowUserToAddRows = false;
            this.dtgCompras.AllowUserToDeleteRows = false;
            this.dtgCompras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCompras.Location = new System.Drawing.Point(50, 128);
            this.dtgCompras.Name = "dtgCompras";
            this.dtgCompras.ReadOnly = true;
            this.dtgCompras.RowTemplate.Height = 24;
            this.dtgCompras.Size = new System.Drawing.Size(1091, 495);
            this.dtgCompras.TabIndex = 3;
            // 
            // bntAceptar
            // 
            this.bntAceptar.Location = new System.Drawing.Point(1026, 658);
            this.bntAceptar.Name = "bntAceptar";
            this.bntAceptar.Size = new System.Drawing.Size(115, 32);
            this.bntAceptar.TabIndex = 4;
            this.bntAceptar.Text = "Aceptar";
            this.bntAceptar.UseVisualStyleBackColor = true;
            this.bntAceptar.Click += new System.EventHandler(this.bntAceptar_Click);
            // 
            // TarjetaOro_compras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 719);
            this.ControlBox = false;
            this.Controls.Add(this.bntAceptar);
            this.Controls.Add(this.dtgCompras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTarjeta);
            this.Controls.Add(this.button1);
            this.Name = "TarjetaOro_compras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras Acumuladas";
            this.Load += new System.EventHandler(this.TarjetaOro_compras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgCompras;
        private System.Windows.Forms.Button bntAceptar;
    }
}