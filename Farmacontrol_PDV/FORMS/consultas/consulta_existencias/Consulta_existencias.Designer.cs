namespace Farmacontrol_PDV.FORMS.consultas.consulta_existencias
{
	partial class Consulta_existencias
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
            this.txt_buscar = new System.Windows.Forms.TextBox();
            this.cbb_sucursales = new System.Windows.Forms.ComboBox();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pct_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.row_grid_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad_sin_formato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_ventas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_devoluciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_mermas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_cambio_fisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_apartados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_traspasos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_mayoreo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_prepago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_vendible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_antibiotico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_buscar
            // 
            this.txt_buscar.Location = new System.Drawing.Point(61, 9);
            this.txt_buscar.Name = "txt_buscar";
            this.txt_buscar.Size = new System.Drawing.Size(405, 20);
            this.txt_buscar.TabIndex = 11;
            this.txt_buscar.TextChanged += new System.EventHandler(this.txt_buscar_TextChanged);
            this.txt_buscar.Enter += new System.EventHandler(this.txt_buscar_Enter);
            this.txt_buscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_buscar_KeyDown);
            // 
            // cbb_sucursales
            // 
            this.cbb_sucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_sucursales.FormattingEnabled = true;
            this.cbb_sucursales.Location = new System.Drawing.Point(529, 9);
            this.cbb_sucursales.Name = "cbb_sucursales";
            this.cbb_sucursales.Size = new System.Drawing.Size(417, 21);
            this.cbb_sucursales.TabIndex = 13;
            this.cbb_sucursales.SelectedIndexChanged += new System.EventHandler(this.cbb_sucursales_SelectedIndexChanged);
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_articulos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_articulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.activo,
            this.pct_descuento,
            this.precio_costo,
            this.row_grid_id,
            this.caducidad_sin_formato,
            this.articulo_id,
            this.amecop,
            this.producto,
            this.caducidad,
            this.lote,
            this.total,
            this.existencia_ventas,
            this.existencia_devoluciones,
            this.existencia_mermas,
            this.existencia_cambio_fisico,
            this.existencia_apartados,
            this.existencia_traspasos,
            this.existencia_mayoreo,
            this.existencia_prepago,
            this.existencia_vendible,
            this.precio_publico,
            this.es_antibiotico});
            this.dgv_articulos.Location = new System.Drawing.Point(13, 35);
            this.dgv_articulos.MultiSelect = false;
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(1199, 403);
            this.dgv_articulos.StandardTab = true;
            this.dgv_articulos.TabIndex = 12;
            this.dgv_articulos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_articulos_CellFormatting);
            this.dgv_articulos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_articulos_KeyDown);
            // 
            // activo
            // 
            this.activo.DataPropertyName = "activo";
            this.activo.HeaderText = "activo";
            this.activo.Name = "activo";
            this.activo.ReadOnly = true;
            this.activo.Visible = false;
            // 
            // pct_descuento
            // 
            this.pct_descuento.DataPropertyName = "pct_descuento";
            this.pct_descuento.HeaderText = "pct_descuento";
            this.pct_descuento.Name = "pct_descuento";
            this.pct_descuento.ReadOnly = true;
            this.pct_descuento.Visible = false;
            // 
            // precio_costo
            // 
            this.precio_costo.DataPropertyName = "precio_costo";
            this.precio_costo.HeaderText = "precio_costo";
            this.precio_costo.Name = "precio_costo";
            this.precio_costo.ReadOnly = true;
            this.precio_costo.Visible = false;
            // 
            // row_grid_id
            // 
            this.row_grid_id.DataPropertyName = "row_grid_id";
            this.row_grid_id.HeaderText = "row_grid_id";
            this.row_grid_id.Name = "row_grid_id";
            this.row_grid_id.ReadOnly = true;
            this.row_grid_id.Visible = false;
            // 
            // caducidad_sin_formato
            // 
            this.caducidad_sin_formato.DataPropertyName = "caducidad_sin_formato";
            this.caducidad_sin_formato.HeaderText = "caducidad_sin_formato";
            this.caducidad_sin_formato.Name = "caducidad_sin_formato";
            this.caducidad_sin_formato.ReadOnly = true;
            this.caducidad_sin_formato.Visible = false;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 37.51674F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "nombre";
            this.producto.FillWeight = 87.75912F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 32.62325F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.FillWeight = 44.04139F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            this.lote.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "existencia_total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.total.DefaultCellStyle = dataGridViewCellStyle5;
            this.total.FillWeight = 21.20511F;
            this.total.HeaderText = "TOT";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // existencia_ventas
            // 
            this.existencia_ventas.DataPropertyName = "existencia_ventas";
            this.existencia_ventas.FillWeight = 21F;
            this.existencia_ventas.HeaderText = "VTA";
            this.existencia_ventas.Name = "existencia_ventas";
            this.existencia_ventas.ReadOnly = true;
            // 
            // existencia_devoluciones
            // 
            this.existencia_devoluciones.DataPropertyName = "existencia_devoluciones";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "-";
            this.existencia_devoluciones.DefaultCellStyle = dataGridViewCellStyle6;
            this.existencia_devoluciones.FillWeight = 21.20511F;
            this.existencia_devoluciones.HeaderText = "DEV";
            this.existencia_devoluciones.Name = "existencia_devoluciones";
            this.existencia_devoluciones.ReadOnly = true;
            // 
            // existencia_mermas
            // 
            this.existencia_mermas.DataPropertyName = "existencia_mermas";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = "-";
            this.existencia_mermas.DefaultCellStyle = dataGridViewCellStyle7;
            this.existencia_mermas.FillWeight = 21.20511F;
            this.existencia_mermas.HeaderText = "MER";
            this.existencia_mermas.Name = "existencia_mermas";
            this.existencia_mermas.ReadOnly = true;
            // 
            // existencia_cambio_fisico
            // 
            this.existencia_cambio_fisico.DataPropertyName = "existencia_cambio_fisico";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "-";
            this.existencia_cambio_fisico.DefaultCellStyle = dataGridViewCellStyle8;
            this.existencia_cambio_fisico.FillWeight = 21.20511F;
            this.existencia_cambio_fisico.HeaderText = "CBF";
            this.existencia_cambio_fisico.Name = "existencia_cambio_fisico";
            this.existencia_cambio_fisico.ReadOnly = true;
            // 
            // existencia_apartados
            // 
            this.existencia_apartados.DataPropertyName = "existencia_apartados";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = "-";
            this.existencia_apartados.DefaultCellStyle = dataGridViewCellStyle9;
            this.existencia_apartados.FillWeight = 21.20511F;
            this.existencia_apartados.HeaderText = "APT";
            this.existencia_apartados.Name = "existencia_apartados";
            this.existencia_apartados.ReadOnly = true;
            // 
            // existencia_traspasos
            // 
            this.existencia_traspasos.DataPropertyName = "existencia_traspasos";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.NullValue = "-";
            this.existencia_traspasos.DefaultCellStyle = dataGridViewCellStyle10;
            this.existencia_traspasos.FillWeight = 21.20511F;
            this.existencia_traspasos.HeaderText = "TRA";
            this.existencia_traspasos.Name = "existencia_traspasos";
            this.existencia_traspasos.ReadOnly = true;
            // 
            // existencia_mayoreo
            // 
            this.existencia_mayoreo.DataPropertyName = "existencia_mayoreo";
            this.existencia_mayoreo.FillWeight = 21F;
            this.existencia_mayoreo.HeaderText = "MAY";
            this.existencia_mayoreo.Name = "existencia_mayoreo";
            this.existencia_mayoreo.ReadOnly = true;
            // 
            // existencia_prepago
            // 
            this.existencia_prepago.DataPropertyName = "existencia_prepago";
            this.existencia_prepago.FillWeight = 21F;
            this.existencia_prepago.HeaderText = "EPP";
            this.existencia_prepago.Name = "existencia_prepago";
            this.existencia_prepago.ReadOnly = true;
            // 
            // existencia_vendible
            // 
            this.existencia_vendible.DataPropertyName = "existencia_vendible";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_vendible.DefaultCellStyle = dataGridViewCellStyle11;
            this.existencia_vendible.FillWeight = 21.20511F;
            this.existencia_vendible.HeaderText = "VEN";
            this.existencia_vendible.Name = "existencia_vendible";
            this.existencia_vendible.ReadOnly = true;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "C2";
            dataGridViewCellStyle12.NullValue = null;
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle12;
            this.precio_publico.FillWeight = 32.62325F;
            this.precio_publico.HeaderText = "Precio Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            // 
            // es_antibiotico
            // 
            this.es_antibiotico.DataPropertyName = "es_antibiotico";
            this.es_antibiotico.HeaderText = "es_antibiotico";
            this.es_antibiotico.Name = "es_antibiotico";
            this.es_antibiotico.ReadOnly = true;
            this.es_antibiotico.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Buscar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Sucursal:";
            // 
            // Consulta_existencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 447);
            this.Controls.Add(this.txt_buscar);
            this.Controls.Add(this.cbb_sucursales);
            this.Controls.Add(this.dgv_articulos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Consulta_existencias";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de existencias";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt_buscar;
		private System.Windows.Forms.ComboBox cbb_sucursales;
		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pct_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_costo;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_grid_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad_sin_formato;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_ventas;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_devoluciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_mermas;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_cambio_fisico;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_apartados;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_traspasos;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_mayoreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_prepago;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_vendible;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_antibiotico;
	}
}