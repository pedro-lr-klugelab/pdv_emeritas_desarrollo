namespace Farmacontrol_PDV.FORMS.reportes.ceros
{
    partial class Ceros_principal
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
            this.dgv_reporte_ceros = new System.Windows.Forms.DataGridView();
            this.existencia_almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.venta_60dias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ultima_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reporte_ceros)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_reporte_ceros
            // 
            this.dgv_reporte_ceros.AllowUserToAddRows = false;
            this.dgv_reporte_ceros.AllowUserToDeleteRows = false;
            this.dgv_reporte_ceros.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_reporte_ceros.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_reporte_ceros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_reporte_ceros.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_reporte_ceros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_reporte_ceros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_reporte_ceros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.amecop,
            this.producto,
            this.existencia,
            this.ultima_venta,
            this.venta_60dias,
            this.existencia_almacen});
            this.dgv_reporte_ceros.Location = new System.Drawing.Point(12, 24);
            this.dgv_reporte_ceros.Name = "dgv_reporte_ceros";
            this.dgv_reporte_ceros.ReadOnly = true;
            this.dgv_reporte_ceros.RowHeadersVisible = false;
            this.dgv_reporte_ceros.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_reporte_ceros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_reporte_ceros.Size = new System.Drawing.Size(821, 653);
            this.dgv_reporte_ceros.StandardTab = true;
            this.dgv_reporte_ceros.TabIndex = 15;
            // 
            // existencia_almacen
            // 
            this.existencia_almacen.DataPropertyName = "existencia_almacen";
            this.existencia_almacen.FillWeight = 59.0863F;
            this.existencia_almacen.HeaderText = "Exis. Alm.";
            this.existencia_almacen.Name = "existencia_almacen";
            this.existencia_almacen.ReadOnly = true;
            // 
            // venta_60dias
            // 
            this.venta_60dias.DataPropertyName = "venta_60dias";
            this.venta_60dias.FillWeight = 59.0863F;
            this.venta_60dias.HeaderText = "Venta 60 dias";
            this.venta_60dias.Name = "venta_60dias";
            this.venta_60dias.ReadOnly = true;
            // 
            // ultima_venta
            // 
            this.ultima_venta.DataPropertyName = "ultima_venta";
            this.ultima_venta.FillWeight = 59.0863F;
            this.ultima_venta.HeaderText = "Ult. Venta";
            this.ultima_venta.Name = "ultima_venta";
            this.ultima_venta.ReadOnly = true;
            // 
            // existencia
            // 
            this.existencia.DataPropertyName = "existencia";
            this.existencia.FillWeight = 59.0863F;
            this.existencia.HeaderText = "Existencia";
            this.existencia.Name = "existencia";
            this.existencia.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 59.0863F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 304.5685F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "Articulo";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // Ceros_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 703);
            this.Controls.Add(this.dgv_reporte_ceros);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ceros_principal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Ceros";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reporte_ceros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_reporte_ceros;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ultima_venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn venta_60dias;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_almacen;
    }
}