namespace Farmacontrol_PDV.FORMS.ventas.cotizaciones
{
    partial class Reabrir_cotizaciones
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
            this.dgv_cotizaciones = new System.Windows.Forms.DataGridView();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_filtro = new System.Windows.Forms.TextBox();
            this.lbl_folio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cotizaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_cotizaciones
            // 
            this.dgv_cotizaciones.AllowUserToAddRows = false;
            this.dgv_cotizaciones.AllowUserToDeleteRows = false;
            this.dgv_cotizaciones.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_cotizaciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_cotizaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_cotizaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cotizaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.folio,
            this.empleado,
            this.cliente,
            this.fecha});
            this.dgv_cotizaciones.Location = new System.Drawing.Point(11, 32);
            this.dgv_cotizaciones.Name = "dgv_cotizaciones";
            this.dgv_cotizaciones.ReadOnly = true;
            this.dgv_cotizaciones.RowHeadersVisible = false;
            this.dgv_cotizaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_cotizaciones.Size = new System.Drawing.Size(985, 283);
            this.dgv_cotizaciones.TabIndex = 5;
            this.dgv_cotizaciones.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_cotizaciones_KeyDown);
            // 
            // folio
            // 
            this.folio.DataPropertyName = "cotizacion_id";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.folio.DefaultCellStyle = dataGridViewCellStyle2;
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            this.folio.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.DataPropertyName = "empleado";
            this.empleado.FillWeight = 200F;
            this.empleado.HeaderText = "Empleado";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.DataPropertyName = "cliente";
            this.cliente.FillWeight = 200F;
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "F";
            dataGridViewCellStyle3.NullValue = null;
            this.fecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.fecha.FillWeight = 150F;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // txt_filtro
            // 
            this.txt_filtro.Location = new System.Drawing.Point(101, 6);
            this.txt_filtro.Name = "txt_filtro";
            this.txt_filtro.Size = new System.Drawing.Size(113, 20);
            this.txt_filtro.TabIndex = 4;
            this.txt_filtro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_filtro_KeyDown);
            // 
            // lbl_folio
            // 
            this.lbl_folio.AutoSize = true;
            this.lbl_folio.Location = new System.Drawing.Point(12, 9);
            this.lbl_folio.Name = "lbl_folio";
            this.lbl_folio.Size = new System.Drawing.Size(83, 13);
            this.lbl_folio.TabIndex = 3;
            this.lbl_folio.Text = "Folio cotizacion:";
            // 
            // Reabrir_cotizaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 330);
            this.Controls.Add(this.dgv_cotizaciones);
            this.Controls.Add(this.txt_filtro);
            this.Controls.Add(this.lbl_folio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reabrir_cotizaciones";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reabrir cotizaciones";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Reabrir_cotizaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cotizaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_cotizaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.TextBox txt_filtro;
        private System.Windows.Forms.Label lbl_folio;
    }
}