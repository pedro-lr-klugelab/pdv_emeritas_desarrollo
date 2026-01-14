namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    partial class Beneficios_Oro
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.bntCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSesion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFolioVenta = new System.Windows.Forms.Label();
            this.dtgview = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(635, 405);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(92, 31);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // bntCancelar
            // 
            this.bntCancelar.Location = new System.Drawing.Point(751, 405);
            this.bntCancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bntCancelar.Name = "bntCancelar";
            this.bntCancelar.Size = new System.Drawing.Size(89, 31);
            this.bntCancelar.TabIndex = 1;
            this.bntCancelar.Text = "Cancelar";
            this.bntCancelar.UseVisualStyleBackColor = true;
            this.bntCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "SESION : ";
            // 
            // lblSesion
            // 
            this.lblSesion.AutoSize = true;
            this.lblSesion.Location = new System.Drawing.Point(118, 24);
            this.lblSesion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSesion.Name = "lblSesion";
            this.lblSesion.Size = new System.Drawing.Size(35, 13);
            this.lblSesion.TabIndex = 3;
            this.lblSesion.Text = "label2";
            this.lblSesion.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(442, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "VENTA :";
            // 
            // lblFolioVenta
            // 
            this.lblFolioVenta.AutoSize = true;
            this.lblFolioVenta.Location = new System.Drawing.Point(532, 24);
            this.lblFolioVenta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFolioVenta.Name = "lblFolioVenta";
            this.lblFolioVenta.Size = new System.Drawing.Size(13, 13);
            this.lblFolioVenta.TabIndex = 5;
            this.lblFolioVenta.Text = "0";
            this.lblFolioVenta.Visible = false;
            this.lblFolioVenta.Click += new System.EventHandler(this.lblFolioVenta_Click);
            // 
            // dtgview
            // 
            this.dtgview.AllowUserToDeleteRows = false;
            this.dtgview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgview.Location = new System.Drawing.Point(38, 96);
            this.dtgview.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtgview.Name = "dtgview";
            this.dtgview.ReadOnly = true;
            this.dtgview.RowTemplate.Height = 24;
            this.dtgview.Size = new System.Drawing.Size(802, 249);
            this.dtgview.TabIndex = 6;
            // 
            // Beneficios_Oro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(861, 446);
            this.Controls.Add(this.dtgview);
            this.Controls.Add(this.lblFolioVenta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSesion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bntCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Beneficios_Oro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beneficios :";
            this.Load += new System.EventHandler(this.Beneficios_Oro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button bntCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSesion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFolioVenta;
        private System.Windows.Forms.DataGridView dtgview;
    }
}