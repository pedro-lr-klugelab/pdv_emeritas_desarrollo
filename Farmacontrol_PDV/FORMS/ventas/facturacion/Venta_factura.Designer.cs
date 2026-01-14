namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
	partial class Venta_factura
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_total = new System.Windows.Forms.Label();
            this.lbl_iva = new System.Windows.Forms.Label();
            this.lbl_ieps = new System.Windows.Forms.Label();
            this.lbl_gravado = new System.Windows.Forms.Label();
            this.lbl_excento = new System.Windows.Forms.Label();
            this.lbl_subtotal = new System.Windows.Forms.Label();
            this.lbl_piezas = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.txt_iva = new System.Windows.Forms.TextBox();
            this.txt_ieps = new System.Windows.Forms.TextBox();
            this.txt_gravado = new System.Windows.Forms.TextBox();
            this.txt_excento = new System.Windows.Forms.TextBox();
            this.txt_subtotal = new System.Windows.Forms.TextBox();
            this.txt_piezas = new System.Windows.Forms.TextBox();
            this.dgv_ventas = new System.Windows.Forms.DataGridView();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna_es_promocion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna_articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detallado_venta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pct_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pct_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe_ieps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad_sin_formato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chb_acepto = new System.Windows.Forms.CheckBox();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Location = new System.Drawing.Point(1085, 442);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(34, 13);
            this.lbl_total.TabIndex = 97;
            this.lbl_total.Text = "Total:";
            // 
            // lbl_iva
            // 
            this.lbl_iva.AutoSize = true;
            this.lbl_iva.Location = new System.Drawing.Point(1092, 389);
            this.lbl_iva.Name = "lbl_iva";
            this.lbl_iva.Size = new System.Drawing.Size(27, 13);
            this.lbl_iva.TabIndex = 96;
            this.lbl_iva.Text = "IVA:";
            // 
            // lbl_ieps
            // 
            this.lbl_ieps.AutoSize = true;
            this.lbl_ieps.Location = new System.Drawing.Point(1085, 363);
            this.lbl_ieps.Name = "lbl_ieps";
            this.lbl_ieps.Size = new System.Drawing.Size(34, 13);
            this.lbl_ieps.TabIndex = 95;
            this.lbl_ieps.Text = "IEPS:";
            // 
            // lbl_gravado
            // 
            this.lbl_gravado.AutoSize = true;
            this.lbl_gravado.Location = new System.Drawing.Point(911, 415);
            this.lbl_gravado.Name = "lbl_gravado";
            this.lbl_gravado.Size = new System.Drawing.Size(51, 13);
            this.lbl_gravado.TabIndex = 94;
            this.lbl_gravado.Text = "Gravado:";
            // 
            // lbl_excento
            // 
            this.lbl_excento.AutoSize = true;
            this.lbl_excento.Location = new System.Drawing.Point(913, 390);
            this.lbl_excento.Name = "lbl_excento";
            this.lbl_excento.Size = new System.Drawing.Size(49, 13);
            this.lbl_excento.TabIndex = 93;
            this.lbl_excento.Text = "Excento:";
            // 
            // lbl_subtotal
            // 
            this.lbl_subtotal.AutoSize = true;
            this.lbl_subtotal.Location = new System.Drawing.Point(913, 363);
            this.lbl_subtotal.Name = "lbl_subtotal";
            this.lbl_subtotal.Size = new System.Drawing.Size(49, 13);
            this.lbl_subtotal.TabIndex = 92;
            this.lbl_subtotal.Text = "Subtotal:";
            // 
            // lbl_piezas
            // 
            this.lbl_piezas.AutoSize = true;
            this.lbl_piezas.Location = new System.Drawing.Point(1078, 416);
            this.lbl_piezas.Name = "lbl_piezas";
            this.lbl_piezas.Size = new System.Drawing.Size(41, 13);
            this.lbl_piezas.TabIndex = 91;
            this.lbl_piezas.Text = "Piezas:";
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.ForeColor = System.Drawing.Color.Red;
            this.txt_total.Location = new System.Drawing.Point(1125, 438);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(100, 22);
            this.txt_total.TabIndex = 90;
            this.txt_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_iva
            // 
            this.txt_iva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_iva.ForeColor = System.Drawing.Color.Red;
            this.txt_iva.Location = new System.Drawing.Point(1125, 386);
            this.txt_iva.Name = "txt_iva";
            this.txt_iva.ReadOnly = true;
            this.txt_iva.Size = new System.Drawing.Size(100, 22);
            this.txt_iva.TabIndex = 89;
            this.txt_iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ieps
            // 
            this.txt_ieps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ieps.ForeColor = System.Drawing.Color.Red;
            this.txt_ieps.Location = new System.Drawing.Point(1125, 360);
            this.txt_ieps.Name = "txt_ieps";
            this.txt_ieps.ReadOnly = true;
            this.txt_ieps.Size = new System.Drawing.Size(100, 22);
            this.txt_ieps.TabIndex = 88;
            this.txt_ieps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_gravado
            // 
            this.txt_gravado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gravado.ForeColor = System.Drawing.Color.Red;
            this.txt_gravado.Location = new System.Drawing.Point(968, 412);
            this.txt_gravado.Name = "txt_gravado";
            this.txt_gravado.ReadOnly = true;
            this.txt_gravado.Size = new System.Drawing.Size(100, 22);
            this.txt_gravado.TabIndex = 87;
            this.txt_gravado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_excento
            // 
            this.txt_excento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_excento.ForeColor = System.Drawing.Color.Red;
            this.txt_excento.Location = new System.Drawing.Point(968, 386);
            this.txt_excento.Name = "txt_excento";
            this.txt_excento.ReadOnly = true;
            this.txt_excento.Size = new System.Drawing.Size(100, 22);
            this.txt_excento.TabIndex = 86;
            this.txt_excento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_subtotal
            // 
            this.txt_subtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_subtotal.ForeColor = System.Drawing.Color.Red;
            this.txt_subtotal.Location = new System.Drawing.Point(968, 360);
            this.txt_subtotal.Name = "txt_subtotal";
            this.txt_subtotal.ReadOnly = true;
            this.txt_subtotal.Size = new System.Drawing.Size(100, 22);
            this.txt_subtotal.TabIndex = 85;
            this.txt_subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_piezas
            // 
            this.txt_piezas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_piezas.ForeColor = System.Drawing.Color.Red;
            this.txt_piezas.Location = new System.Drawing.Point(1125, 412);
            this.txt_piezas.Name = "txt_piezas";
            this.txt_piezas.ReadOnly = true;
            this.txt_piezas.Size = new System.Drawing.Size(100, 22);
            this.txt_piezas.TabIndex = 84;
            this.txt_piezas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgv_ventas
            // 
            this.dgv_ventas.AllowUserToAddRows = false;
            this.dgv_ventas.AllowUserToDeleteRows = false;
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
            this.amecop,
            this.columna_es_promocion,
            this.columna_articulo_id,
            this.detallado_venta_id,
            this.producto,
            this.caducidad,
            this.lote,
            this.precio_publico,
            this.pct_descuento,
            this.importe_descuento,
            this.pct_iva,
            this.importe_iva,
            this.importe_ieps,
            this.importe,
            this.cantidad,
            this.subtotal,
            this.caducidad_sin_formato,
            this.total});
            this.dgv_ventas.Location = new System.Drawing.Point(12, 35);
            this.dgv_ventas.MultiSelect = false;
            this.dgv_ventas.Name = "dgv_ventas";
            this.dgv_ventas.ReadOnly = true;
            this.dgv_ventas.RowHeadersVisible = false;
            this.dgv_ventas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ventas.Size = new System.Drawing.Size(1213, 319);
            this.dgv_ventas.TabIndex = 83;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 120F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // columna_es_promocion
            // 
            this.columna_es_promocion.DataPropertyName = "es_promocion";
            this.columna_es_promocion.HeaderText = "columna_es_promocion";
            this.columna_es_promocion.Name = "columna_es_promocion";
            this.columna_es_promocion.ReadOnly = true;
            this.columna_es_promocion.Visible = false;
            // 
            // columna_articulo_id
            // 
            this.columna_articulo_id.DataPropertyName = "articulo_id";
            this.columna_articulo_id.HeaderText = "columna_articulo_id";
            this.columna_articulo_id.Name = "columna_articulo_id";
            this.columna_articulo_id.ReadOnly = true;
            this.columna_articulo_id.Visible = false;
            // 
            // detallado_venta_id
            // 
            this.detallado_venta_id.DataPropertyName = "detallado_venta_id";
            this.detallado_venta_id.HeaderText = "detallado_cotizacion_id";
            this.detallado_venta_id.Name = "detallado_venta_id";
            this.detallado_venta_id.ReadOnly = true;
            this.detallado_venta_id.Visible = false;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "nombre";
            this.producto.FillWeight = 250F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.FillWeight = 150F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            this.lote.ReadOnly = true;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle5;
            this.precio_publico.HeaderText = "P. Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            // 
            // pct_descuento
            // 
            this.pct_descuento.DataPropertyName = "pct_descuento";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pct_descuento.DefaultCellStyle = dataGridViewCellStyle6;
            this.pct_descuento.FillWeight = 80F;
            this.pct_descuento.HeaderText = "% Desc";
            this.pct_descuento.Name = "pct_descuento";
            this.pct_descuento.ReadOnly = true;
            this.pct_descuento.Visible = false;
            // 
            // importe_descuento
            // 
            this.importe_descuento.DataPropertyName = "importe_descuento";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.importe_descuento.DefaultCellStyle = dataGridViewCellStyle7;
            this.importe_descuento.HeaderText = "Imp. Desc.";
            this.importe_descuento.Name = "importe_descuento";
            this.importe_descuento.ReadOnly = true;
            // 
            // pct_iva
            // 
            this.pct_iva.DataPropertyName = "pct_iva";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pct_iva.DefaultCellStyle = dataGridViewCellStyle8;
            this.pct_iva.FillWeight = 80F;
            this.pct_iva.HeaderText = "% IVA";
            this.pct_iva.Name = "pct_iva";
            this.pct_iva.ReadOnly = true;
            this.pct_iva.Visible = false;
            // 
            // importe_iva
            // 
            this.importe_iva.DataPropertyName = "importe_iva";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "C2";
            dataGridViewCellStyle9.NullValue = null;
            this.importe_iva.DefaultCellStyle = dataGridViewCellStyle9;
            this.importe_iva.HeaderText = "Imp. IVA";
            this.importe_iva.Name = "importe_iva";
            this.importe_iva.ReadOnly = true;
            // 
            // importe_ieps
            // 
            this.importe_ieps.DataPropertyName = "importe_ieps";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "C2";
            dataGridViewCellStyle10.NullValue = null;
            this.importe_ieps.DefaultCellStyle = dataGridViewCellStyle10;
            this.importe_ieps.HeaderText = "Imp. IEPS";
            this.importe_ieps.Name = "importe_ieps";
            this.importe_ieps.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.DataPropertyName = "importe";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "C2";
            dataGridViewCellStyle11.NullValue = null;
            this.importe.DefaultCellStyle = dataGridViewCellStyle11;
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle12;
            this.cantidad.FillWeight = 70F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // subtotal
            // 
            this.subtotal.DataPropertyName = "subtotal";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "C2";
            dataGridViewCellStyle13.NullValue = null;
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle13;
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            // 
            // caducidad_sin_formato
            // 
            this.caducidad_sin_formato.DataPropertyName = "caducidad_sin_formato";
            this.caducidad_sin_formato.HeaderText = "caducidad_sin_formato";
            this.caducidad_sin_formato.Name = "caducidad_sin_formato";
            this.caducidad_sin_formato.ReadOnly = true;
            this.caducidad_sin_formato.Visible = false;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "C2";
            dataGridViewCellStyle14.NullValue = null;
            this.total.DefaultCellStyle = dataGridViewCellStyle14;
            this.total.FillWeight = 120F;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // chb_acepto
            // 
            this.chb_acepto.AutoSize = true;
            this.chb_acepto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_acepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_acepto.ForeColor = System.Drawing.SystemColors.GrayText;
            this.chb_acepto.Location = new System.Drawing.Point(450, 491);
            this.chb_acepto.Name = "chb_acepto";
            this.chb_acepto.Size = new System.Drawing.Size(484, 17);
            this.chb_acepto.TabIndex = 98;
            this.chb_acepto.Text = "He verificado que los productos aqui mostrados coinciden con el ticket de venta";
            this.chb_acepto.UseVisualStyleBackColor = true;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(951, 487);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(133, 23);
            this.btn_aceptar.TabIndex = 99;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(1090, 487);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(133, 23);
            this.btn_cancelar.TabIndex = 100;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Venta_factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 522);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.chb_acepto);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.lbl_iva);
            this.Controls.Add(this.lbl_ieps);
            this.Controls.Add(this.lbl_gravado);
            this.Controls.Add(this.lbl_excento);
            this.Controls.Add(this.lbl_subtotal);
            this.Controls.Add(this.lbl_piezas);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.txt_iva);
            this.Controls.Add(this.txt_ieps);
            this.Controls.Add(this.txt_gravado);
            this.Controls.Add(this.txt_excento);
            this.Controls.Add(this.txt_subtotal);
            this.Controls.Add(this.txt_piezas);
            this.Controls.Add(this.dgv_ventas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Venta_factura";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información de la venta";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Venta_factura_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_total;
		private System.Windows.Forms.Label lbl_iva;
		private System.Windows.Forms.Label lbl_ieps;
		private System.Windows.Forms.Label lbl_gravado;
		private System.Windows.Forms.Label lbl_excento;
		private System.Windows.Forms.Label lbl_subtotal;
		private System.Windows.Forms.Label lbl_piezas;
		private System.Windows.Forms.TextBox txt_total;
		private System.Windows.Forms.TextBox txt_iva;
		private System.Windows.Forms.TextBox txt_ieps;
		private System.Windows.Forms.TextBox txt_gravado;
		private System.Windows.Forms.TextBox txt_excento;
		private System.Windows.Forms.TextBox txt_subtotal;
		private System.Windows.Forms.TextBox txt_piezas;
		private System.Windows.Forms.DataGridView dgv_ventas;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn columna_es_promocion;
		private System.Windows.Forms.DataGridViewTextBoxColumn columna_articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn detallado_venta_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
		private System.Windows.Forms.DataGridViewTextBoxColumn pct_descuento;
		private System.Windows.Forms.DataGridViewTextBoxColumn importe_descuento;
		private System.Windows.Forms.DataGridViewTextBoxColumn pct_iva;
		private System.Windows.Forms.DataGridViewTextBoxColumn importe_iva;
		private System.Windows.Forms.DataGridViewTextBoxColumn importe_ieps;
		private System.Windows.Forms.DataGridViewTextBoxColumn importe;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad_sin_formato;
		private System.Windows.Forms.DataGridViewTextBoxColumn total;
		private System.Windows.Forms.CheckBox chb_acepto;
		private System.Windows.Forms.Button btn_aceptar;
		private System.Windows.Forms.Button btn_cancelar;
	}
}