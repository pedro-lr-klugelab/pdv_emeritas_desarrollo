namespace Farmacontrol_PDV.FORMS.movimientos.devoluciones_mayoristas
{
	partial class Productos_devolucion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_continuar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.dgv_entradas = new System.Windows.Forms.DataGridView();
            this.detallado_devolucion_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad_entradas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_motivo_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad_terminadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad_devoluciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad_vendible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_pct_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_pct_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_importe_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cant_dev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_importe_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_motivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_entradas)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_continuar
            // 
            this.btn_continuar.Location = new System.Drawing.Point(1112, 389);
            this.btn_continuar.Name = "btn_continuar";
            this.btn_continuar.Size = new System.Drawing.Size(87, 23);
            this.btn_continuar.TabIndex = 0;
            this.btn_continuar.Text = "Continuar";
            this.btn_continuar.UseVisualStyleBackColor = true;
            this.btn_continuar.Click += new System.EventHandler(this.btn_continuar_Click);
            this.btn_continuar.Enter += new System.EventHandler(this.btn_continuar_Enter);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(1205, 389);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 1;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // dgv_entradas
            // 
            this.dgv_entradas.AllowUserToAddRows = false;
            this.dgv_entradas.AllowUserToDeleteRows = false;
            this.dgv_entradas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_entradas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_entradas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_entradas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_entradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_entradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_entradas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detallado_devolucion_id,
            this.c_cantidad_entradas,
            this.c_motivo_actual,
            this.c_cantidad_actual,
            this.c_cantidad_terminadas,
            this.c_cantidad_devoluciones,
            this.c_cantidad_vendible,
            this.c_pct_descuento,
            this.c_articulo_id,
            this.c_pct_iva,
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_precio_costo,
            this.c_importe_descuento,
            this.c_importe,
            this.c_cantidad,
            this.c_cant_dev,
            this.c_subtotal,
            this.c_importe_iva,
            this.c_total,
            this.c_motivo});
            this.dgv_entradas.Enabled = false;
            this.dgv_entradas.Location = new System.Drawing.Point(12, 12);
            this.dgv_entradas.MultiSelect = false;
            this.dgv_entradas.Name = "dgv_entradas";
            this.dgv_entradas.RowHeadersVisible = false;
            this.dgv_entradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_entradas.Size = new System.Drawing.Size(1268, 371);
            this.dgv_entradas.TabIndex = 220;
            this.dgv_entradas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_entradas_KeyDown);
            // 
            // detallado_devolucion_id
            // 
            this.detallado_devolucion_id.DataPropertyName = "detallado_devolucion_id";
            this.detallado_devolucion_id.HeaderText = "detallado_devolucion_id";
            this.detallado_devolucion_id.Name = "detallado_devolucion_id";
            this.detallado_devolucion_id.Visible = false;
            // 
            // c_cantidad_entradas
            // 
            this.c_cantidad_entradas.DataPropertyName = "cantidad_entradas";
            this.c_cantidad_entradas.HeaderText = "c_cantidad_entradas";
            this.c_cantidad_entradas.Name = "c_cantidad_entradas";
            this.c_cantidad_entradas.Visible = false;
            // 
            // c_motivo_actual
            // 
            this.c_motivo_actual.DataPropertyName = "motivo_actual";
            this.c_motivo_actual.HeaderText = "c_motivo_actual";
            this.c_motivo_actual.Name = "c_motivo_actual";
            this.c_motivo_actual.Visible = false;
            // 
            // c_cantidad_actual
            // 
            this.c_cantidad_actual.DataPropertyName = "cantidad_actual";
            this.c_cantidad_actual.HeaderText = "c_cantidad_actual";
            this.c_cantidad_actual.Name = "c_cantidad_actual";
            this.c_cantidad_actual.Visible = false;
            // 
            // c_cantidad_terminadas
            // 
            this.c_cantidad_terminadas.DataPropertyName = "cantidad_terminadas";
            this.c_cantidad_terminadas.HeaderText = "c_cantidad_terminadas";
            this.c_cantidad_terminadas.Name = "c_cantidad_terminadas";
            this.c_cantidad_terminadas.Visible = false;
            // 
            // c_cantidad_devoluciones
            // 
            this.c_cantidad_devoluciones.DataPropertyName = "cantidad_devoluciones";
            this.c_cantidad_devoluciones.HeaderText = "c_cantidad_devoluciones";
            this.c_cantidad_devoluciones.Name = "c_cantidad_devoluciones";
            this.c_cantidad_devoluciones.Visible = false;
            // 
            // c_cantidad_vendible
            // 
            this.c_cantidad_vendible.DataPropertyName = "cantidad_vendible";
            this.c_cantidad_vendible.HeaderText = "c_cantidad_vendible";
            this.c_cantidad_vendible.Name = "c_cantidad_vendible";
            this.c_cantidad_vendible.Visible = false;
            // 
            // c_pct_descuento
            // 
            this.c_pct_descuento.DataPropertyName = "pct_descuento";
            this.c_pct_descuento.HeaderText = "c_pct_descuento";
            this.c_pct_descuento.Name = "c_pct_descuento";
            this.c_pct_descuento.Visible = false;
            // 
            // c_articulo_id
            // 
            this.c_articulo_id.DataPropertyName = "articulo_id";
            this.c_articulo_id.HeaderText = "c_articulo_id";
            this.c_articulo_id.Name = "c_articulo_id";
            this.c_articulo_id.Visible = false;
            // 
            // c_pct_iva
            // 
            this.c_pct_iva.DataPropertyName = "pct_iva";
            this.c_pct_iva.HeaderText = "c_pct_iva";
            this.c_pct_iva.Name = "c_pct_iva";
            this.c_pct_iva.Visible = false;
            // 
            // c_amecop
            // 
            this.c_amecop.DataPropertyName = "amecop";
            this.c_amecop.HeaderText = "Amecop";
            this.c_amecop.Name = "c_amecop";
            // 
            // c_producto
            // 
            this.c_producto.DataPropertyName = "producto";
            this.c_producto.FillWeight = 180F;
            this.c_producto.HeaderText = "Producto";
            this.c_producto.Name = "c_producto";
            // 
            // c_caducidad
            // 
            this.c_caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_caducidad.FillWeight = 70F;
            this.c_caducidad.HeaderText = "Caducidad";
            this.c_caducidad.Name = "c_caducidad";
            // 
            // c_lote
            // 
            this.c_lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_lote.FillWeight = 130F;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            // 
            // c_precio_costo
            // 
            this.c_precio_costo.DataPropertyName = "precio_costo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.c_precio_costo.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_precio_costo.FillWeight = 80F;
            this.c_precio_costo.HeaderText = "Precio Costo";
            this.c_precio_costo.Name = "c_precio_costo";
            // 
            // c_importe_descuento
            // 
            this.c_importe_descuento.DataPropertyName = "importe_descuento";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.c_importe_descuento.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_importe_descuento.FillWeight = 70F;
            this.c_importe_descuento.HeaderText = "Imp. Desc.";
            this.c_importe_descuento.Name = "c_importe_descuento";
            this.c_importe_descuento.Visible = false;
            // 
            // c_importe
            // 
            this.c_importe.DataPropertyName = "importe";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.c_importe.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_importe.FillWeight = 80F;
            this.c_importe.HeaderText = "Importe";
            this.c_importe.Name = "c_importe";
            this.c_importe.Visible = false;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle8;
            this.c_cantidad.FillWeight = 70F;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.Visible = false;
            // 
            // c_cant_dev
            // 
            this.c_cant_dev.DataPropertyName = "cant_dev";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Red;
            this.c_cant_dev.DefaultCellStyle = dataGridViewCellStyle9;
            this.c_cant_dev.FillWeight = 70F;
            this.c_cant_dev.HeaderText = "Cant. Dev.";
            this.c_cant_dev.Name = "c_cant_dev";
            this.c_cant_dev.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // c_subtotal
            // 
            this.c_subtotal.DataPropertyName = "subtotal";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "C2";
            dataGridViewCellStyle10.NullValue = null;
            this.c_subtotal.DefaultCellStyle = dataGridViewCellStyle10;
            this.c_subtotal.FillWeight = 90F;
            this.c_subtotal.HeaderText = "Subtotal";
            this.c_subtotal.Name = "c_subtotal";
            this.c_subtotal.Visible = false;
            // 
            // c_importe_iva
            // 
            this.c_importe_iva.DataPropertyName = "importe_iva";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "C2";
            dataGridViewCellStyle11.NullValue = null;
            this.c_importe_iva.DefaultCellStyle = dataGridViewCellStyle11;
            this.c_importe_iva.FillWeight = 70F;
            this.c_importe_iva.HeaderText = "Imp. IVA";
            this.c_importe_iva.Name = "c_importe_iva";
            this.c_importe_iva.Visible = false;
            // 
            // c_total
            // 
            this.c_total.DataPropertyName = "total";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "C2";
            dataGridViewCellStyle12.NullValue = null;
            this.c_total.DefaultCellStyle = dataGridViewCellStyle12;
            this.c_total.HeaderText = "Total";
            this.c_total.Name = "c_total";
            this.c_total.Visible = false;
            // 
            // c_motivo
            // 
            this.c_motivo.DataPropertyName = "motivo";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_motivo.DefaultCellStyle = dataGridViewCellStyle13;
            this.c_motivo.HeaderText = "Motivo";
            this.c_motivo.Name = "c_motivo";
            this.c_motivo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_motivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Productos_devolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 425);
            this.ControlBox = false;
            this.Controls.Add(this.dgv_entradas);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_continuar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Productos_devolucion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos a devolver";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Productos_devolucion_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_entradas)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_continuar;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.DataGridView dgv_entradas;
		private System.Windows.Forms.DataGridViewTextBoxColumn detallado_devolucion_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad_entradas;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_motivo_actual;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad_actual;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad_terminadas;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad_devoluciones;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad_vendible;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_pct_descuento;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_pct_iva;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_precio_costo;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_importe_descuento;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_importe;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cant_dev;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_subtotal;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_importe_iva;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_total;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_motivo;
	}
}