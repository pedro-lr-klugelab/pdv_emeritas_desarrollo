namespace Farmacontrol_PDV.FORMS.ventas.servicios_domicilio_saldar
{
    partial class Servicios_domicilio_saldar_principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_ventas = new System.Windows.Forms.DataGridView();
            this.btn_ticket = new System.Windows.Forms.Button();
            this.btn_saldar = new System.Windows.Forms.Button();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_envio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diligenciero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ventas
            // 
            this.dgv_ventas.AllowUserToAddRows = false;
            this.dgv_ventas.AllowUserToDeleteRows = false;
            this.dgv_ventas.AllowUserToResizeColumns = false;
            this.dgv_ventas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_ventas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ventas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ventas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ventas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ventas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.folio,
            this.fecha_envio,
            this.diligenciero,
            this.importe,
            this.total});
            this.dgv_ventas.Location = new System.Drawing.Point(12, 12);
            this.dgv_ventas.MultiSelect = false;
            this.dgv_ventas.Name = "dgv_ventas";
            this.dgv_ventas.ReadOnly = true;
            this.dgv_ventas.RowHeadersVisible = false;
            this.dgv_ventas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ventas.Size = new System.Drawing.Size(756, 372);
            this.dgv_ventas.TabIndex = 0;
            // 
            // btn_ticket
            // 
            this.btn_ticket.Location = new System.Drawing.Point(774, 12);
            this.btn_ticket.Name = "btn_ticket";
            this.btn_ticket.Size = new System.Drawing.Size(115, 23);
            this.btn_ticket.TabIndex = 1;
            this.btn_ticket.Text = "Reimprimir Ticket";
            this.btn_ticket.UseVisualStyleBackColor = true;
            this.btn_ticket.Click += new System.EventHandler(this.btn_ticket_Click);
            // 
            // btn_saldar
            // 
            this.btn_saldar.Location = new System.Drawing.Point(774, 41);
            this.btn_saldar.Name = "btn_saldar";
            this.btn_saldar.Size = new System.Drawing.Size(115, 23);
            this.btn_saldar.TabIndex = 2;
            this.btn_saldar.Text = "Saldar";
            this.btn_saldar.UseVisualStyleBackColor = true;
            this.btn_saldar.Click += new System.EventHandler(this.btn_saldar_Click);
            // 
            // folio
            // 
            this.folio.DataPropertyName = "venta_envio_folio";
            this.folio.FillWeight = 80F;
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            this.folio.ReadOnly = true;
            // 
            // fecha_envio
            // 
            this.fecha_envio.DataPropertyName = "fecha_envio";
            this.fecha_envio.HeaderText = "Fecha Enviado";
            this.fecha_envio.Name = "fecha_envio";
            this.fecha_envio.ReadOnly = true;
            // 
            // diligenciero
            // 
            this.diligenciero.DataPropertyName = "diligenciero";
            this.diligenciero.FillWeight = 120F;
            this.diligenciero.HeaderText = "Diligenciero";
            this.diligenciero.Name = "diligenciero";
            this.diligenciero.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.DataPropertyName = "importe";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.NullValue = "C2";
            this.importe.DefaultCellStyle = dataGridViewCellStyle3;
            this.importe.FillWeight = 80F;
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            this.total.DefaultCellStyle = dataGridViewCellStyle4;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // Servicios_domicilio_saldar_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 396);
            this.Controls.Add(this.btn_saldar);
            this.Controls.Add(this.btn_ticket);
            this.Controls.Add(this.dgv_ventas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Servicios_domicilio_saldar_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicios a domicilio (SALDAR)";
            this.Shown += new System.EventHandler(this.Servicios_domicilio_saldar_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ventas;
        private System.Windows.Forms.Button btn_ticket;
        private System.Windows.Forms.Button btn_saldar;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_envio;
        private System.Windows.Forms.DataGridViewTextBoxColumn diligenciero;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
    }
}