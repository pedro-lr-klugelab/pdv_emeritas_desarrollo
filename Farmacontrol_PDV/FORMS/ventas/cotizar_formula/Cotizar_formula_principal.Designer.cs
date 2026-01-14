namespace Farmacontrol_PDV.FORMS.ventas.cotizar_formula
{
	partial class Cotizar_formula_principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_detallado_formulas = new System.Windows.Forms.DataGridView();
            this.detallado_formula_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materia_prima_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_imprimir = new System.Windows.Forms.Button();
            this.btn_terminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_cliente = new System.Windows.Forms.TextBox();
            this.txt_doctor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_producto = new System.Windows.Forms.TextBox();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.lbl_cantidad = new System.Windows.Forms.Label();
            this.lbl_producto = new System.Windows.Forms.Label();
            this.lbl_amecop = new System.Windows.Forms.Label();
            this.lbl_total = new System.Windows.Forms.Label();
            this.lbl_iva = new System.Windows.Forms.Label();
            this.lbl_ieps = new System.Windows.Forms.Label();
            this.lbl_subtotal = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.txt_iva = new System.Windows.Forms.TextBox();
            this.txt_ieps = new System.Windows.Forms.TextBox();
            this.txt_subtotal = new System.Windows.Forms.TextBox();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_instrucciones = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cantidad = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_comentario_producto = new System.Windows.Forms.TextBox();
            this.Sucursal_id = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detallado_formulas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_detallado_formulas
            // 
            this.dgv_detallado_formulas.AllowUserToAddRows = false;
            this.dgv_detallado_formulas.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_detallado_formulas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_detallado_formulas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_detallado_formulas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_detallado_formulas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_detallado_formulas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detallado_formula_id,
            this.formula_id,
            this.materia_prima_id,
            this.articulo_id,
            this.amecop,
            this.nombre,
            this.comentarios,
            this.precio_publico,
            this.cantidad,
            this.importe,
            this.subtotal,
            this.total});
            this.dgv_detallado_formulas.Location = new System.Drawing.Point(16, 104);
            this.dgv_detallado_formulas.MultiSelect = false;
            this.dgv_detallado_formulas.Name = "dgv_detallado_formulas";
            this.dgv_detallado_formulas.ReadOnly = true;
            this.dgv_detallado_formulas.RowHeadersVisible = false;
            this.dgv_detallado_formulas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_detallado_formulas.Size = new System.Drawing.Size(960, 291);
            this.dgv_detallado_formulas.TabIndex = 0;
            this.dgv_detallado_formulas.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_detallado_formulas_UserDeletedRow);
            this.dgv_detallado_formulas.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgv_detallado_formulas_UserDeletingRow);
            this.dgv_detallado_formulas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_detallado_formulas_KeyDown);
            // 
            // detallado_formula_id
            // 
            this.detallado_formula_id.DataPropertyName = "detallado_formula_id";
            this.detallado_formula_id.HeaderText = "detallado_formula_id";
            this.detallado_formula_id.Name = "detallado_formula_id";
            this.detallado_formula_id.ReadOnly = true;
            this.detallado_formula_id.Visible = false;
            // 
            // formula_id
            // 
            this.formula_id.DataPropertyName = "formula_id";
            this.formula_id.HeaderText = "formula_id";
            this.formula_id.Name = "formula_id";
            this.formula_id.ReadOnly = true;
            this.formula_id.Visible = false;
            // 
            // materia_prima_id
            // 
            this.materia_prima_id.DataPropertyName = "materia_prima_id";
            this.materia_prima_id.HeaderText = "materia_prima_id";
            this.materia_prima_id.Name = "materia_prima_id";
            this.materia_prima_id.ReadOnly = true;
            this.materia_prima_id.Visible = false;
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
            this.amecop.FillWeight = 80F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 150F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // comentarios
            // 
            this.comentarios.DataPropertyName = "comentarios";
            this.comentarios.FillWeight = 120F;
            this.comentarios.HeaderText = "Comentarios";
            this.comentarios.Name = "comentarios";
            this.comentarios.ReadOnly = true;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "C2";
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle10;
            this.precio_publico.FillWeight = 70F;
            this.precio_publico.HeaderText = "Precio Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle11;
            this.cantidad.FillWeight = 60F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.DataPropertyName = "importe";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "C2";
            this.importe.DefaultCellStyle = dataGridViewCellStyle12;
            this.importe.FillWeight = 60F;
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            // 
            // subtotal
            // 
            this.subtotal.DataPropertyName = "subtotal";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "C2";
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle13;
            this.subtotal.FillWeight = 70F;
            this.subtotal.HeaderText = "Total";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "C2";
            this.total.DefaultCellStyle = dataGridViewCellStyle14;
            this.total.FillWeight = 70F;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Visible = false;
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(820, 535);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(75, 23);
            this.btn_imprimir.TabIndex = 1;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // btn_terminar
            // 
            this.btn_terminar.Enabled = false;
            this.btn_terminar.Location = new System.Drawing.Point(901, 535);
            this.btn_terminar.Name = "btn_terminar";
            this.btn_terminar.Size = new System.Drawing.Size(75, 23);
            this.btn_terminar.TabIndex = 2;
            this.btn_terminar.Text = "Terminar";
            this.btn_terminar.UseVisualStyleBackColor = true;
            this.btn_terminar.Click += new System.EventHandler(this.btn_terminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre del cliente:";
            // 
            // txt_cliente
            // 
            this.txt_cliente.Location = new System.Drawing.Point(113, 12);
            this.txt_cliente.Name = "txt_cliente";
            this.txt_cliente.Size = new System.Drawing.Size(487, 20);
            this.txt_cliente.TabIndex = 4;
            this.txt_cliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cliente_KeyPress);
            // 
            // txt_doctor
            // 
            this.txt_doctor.Location = new System.Drawing.Point(113, 38);
            this.txt_doctor.Name = "txt_doctor";
            this.txt_doctor.Size = new System.Drawing.Size(487, 20);
            this.txt_doctor.TabIndex = 6;
            this.txt_doctor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_doctor_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre del doctor:";
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Location = new System.Drawing.Point(162, 78);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.Size = new System.Drawing.Size(438, 20);
            this.txt_producto.TabIndex = 93;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(16, 78);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(140, 20);
            this.txt_amecop.TabIndex = 0;
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            this.txt_amecop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_amecop_KeyPress);
            this.txt_amecop.Leave += new System.EventHandler(this.txt_amecop_Leave);
            // 
            // lbl_cantidad
            // 
            this.lbl_cantidad.AutoSize = true;
            this.lbl_cantidad.Location = new System.Drawing.Point(886, 62);
            this.lbl_cantidad.Name = "lbl_cantidad";
            this.lbl_cantidad.Size = new System.Drawing.Size(49, 13);
            this.lbl_cantidad.TabIndex = 92;
            this.lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(159, 60);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(50, 13);
            this.lbl_producto.TabIndex = 88;
            this.lbl_producto.Text = "Producto";
            // 
            // lbl_amecop
            // 
            this.lbl_amecop.AutoSize = true;
            this.lbl_amecop.Location = new System.Drawing.Point(13, 61);
            this.lbl_amecop.Name = "lbl_amecop";
            this.lbl_amecop.Size = new System.Drawing.Size(46, 13);
            this.lbl_amecop.TabIndex = 87;
            this.lbl_amecop.Text = "Amecop";
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Location = new System.Drawing.Point(836, 491);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(34, 13);
            this.lbl_total.TabIndex = 111;
            this.lbl_total.Text = "Total:";
            // 
            // lbl_iva
            // 
            this.lbl_iva.AutoSize = true;
            this.lbl_iva.Location = new System.Drawing.Point(843, 434);
            this.lbl_iva.Name = "lbl_iva";
            this.lbl_iva.Size = new System.Drawing.Size(27, 13);
            this.lbl_iva.TabIndex = 110;
            this.lbl_iva.Text = "IVA:";
            // 
            // lbl_ieps
            // 
            this.lbl_ieps.AutoSize = true;
            this.lbl_ieps.Location = new System.Drawing.Point(836, 462);
            this.lbl_ieps.Name = "lbl_ieps";
            this.lbl_ieps.Size = new System.Drawing.Size(34, 13);
            this.lbl_ieps.TabIndex = 109;
            this.lbl_ieps.Text = "IEPS:";
            // 
            // lbl_subtotal
            // 
            this.lbl_subtotal.AutoSize = true;
            this.lbl_subtotal.Location = new System.Drawing.Point(821, 408);
            this.lbl_subtotal.Name = "lbl_subtotal";
            this.lbl_subtotal.Size = new System.Drawing.Size(49, 13);
            this.lbl_subtotal.TabIndex = 106;
            this.lbl_subtotal.Text = "Subtotal:";
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.ForeColor = System.Drawing.Color.Red;
            this.txt_total.Location = new System.Drawing.Point(876, 487);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(100, 22);
            this.txt_total.TabIndex = 104;
            this.txt_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_iva
            // 
            this.txt_iva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_iva.ForeColor = System.Drawing.Color.Red;
            this.txt_iva.Location = new System.Drawing.Point(876, 431);
            this.txt_iva.Name = "txt_iva";
            this.txt_iva.ReadOnly = true;
            this.txt_iva.Size = new System.Drawing.Size(100, 22);
            this.txt_iva.TabIndex = 103;
            this.txt_iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ieps
            // 
            this.txt_ieps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ieps.ForeColor = System.Drawing.Color.Red;
            this.txt_ieps.Location = new System.Drawing.Point(876, 459);
            this.txt_ieps.Name = "txt_ieps";
            this.txt_ieps.ReadOnly = true;
            this.txt_ieps.Size = new System.Drawing.Size(100, 22);
            this.txt_ieps.TabIndex = 102;
            this.txt_ieps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_subtotal
            // 
            this.txt_subtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_subtotal.ForeColor = System.Drawing.Color.Red;
            this.txt_subtotal.Location = new System.Drawing.Point(876, 405);
            this.txt_subtotal.Name = "txt_subtotal";
            this.txt_subtotal.ReadOnly = true;
            this.txt_subtotal.Size = new System.Drawing.Size(100, 22);
            this.txt_subtotal.TabIndex = 99;
            this.txt_subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.Location = new System.Drawing.Point(16, 530);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(109, 23);
            this.btn_limpiar.TabIndex = 112;
            this.btn_limpiar.Text = "Limpiar cotización";
            this.btn_limpiar.UseVisualStyleBackColor = true;
            this.btn_limpiar.Click += new System.EventHandler(this.btn_limpiar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 410);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 13);
            this.label6.TabIndex = 122;
            this.label6.Text = "Instrucciones, indicaciones, notas o comentarios:";
            // 
            // txt_instrucciones
            // 
            this.txt_instrucciones.Location = new System.Drawing.Point(14, 436);
            this.txt_instrucciones.Multiline = true;
            this.txt_instrucciones.Name = "txt_instrucciones";
            this.txt_instrucciones.Size = new System.Drawing.Size(512, 85);
            this.txt_instrucciones.TabIndex = 123;
            this.txt_instrucciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_instrucciones_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 535);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 13);
            this.label3.TabIndex = 124;
            this.label3.Text = "CTRL + M : Inserta costo de mano de obra";
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.DecimalPlaces = 2;
            this.txt_cantidad.Enabled = false;
            this.txt_cantidad.Location = new System.Drawing.Point(889, 78);
            this.txt_cantidad.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(87, 20);
            this.txt_cantidad.TabIndex = 125;
            this.txt_cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(603, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Comentarios";
            // 
            // txt_comentario_producto
            // 
            this.txt_comentario_producto.Enabled = false;
            this.txt_comentario_producto.Location = new System.Drawing.Point(606, 77);
            this.txt_comentario_producto.MaxLength = 240;
            this.txt_comentario_producto.Name = "txt_comentario_producto";
            this.txt_comentario_producto.Size = new System.Drawing.Size(277, 20);
            this.txt_comentario_producto.TabIndex = 127;
            this.txt_comentario_producto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_comentario_producto_KeyDown);
            this.txt_comentario_producto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comentario_producto_KeyPress);
            // 
            // Sucursal_id
            // 
            this.Sucursal_id.AutoSize = true;
            this.Sucursal_id.Location = new System.Drawing.Point(648, 18);
            this.Sucursal_id.Name = "Sucursal_id";
            this.Sucursal_id.Size = new System.Drawing.Size(51, 13);
            this.Sucursal_id.TabIndex = 128;
            this.Sucursal_id.Text = "Sucursal:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(705, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(165, 21);
            this.comboBox1.TabIndex = 129;
            // 
            // Cotizar_formula_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 571);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Sucursal_id);
            this.Controls.Add(this.txt_comentario_producto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_cantidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_instrucciones);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_limpiar);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.lbl_iva);
            this.Controls.Add(this.lbl_ieps);
            this.Controls.Add(this.lbl_subtotal);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.txt_iva);
            this.Controls.Add(this.txt_ieps);
            this.Controls.Add(this.txt_subtotal);
            this.Controls.Add(this.txt_producto);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.lbl_cantidad);
            this.Controls.Add(this.lbl_producto);
            this.Controls.Add(this.lbl_amecop);
            this.Controls.Add(this.txt_doctor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_cliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_terminar);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.dgv_detallado_formulas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cotizar_formula_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cotizador de formulas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cotizar_formula_principal_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detallado_formulas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_detallado_formulas;
		private System.Windows.Forms.Button btn_imprimir;
		private System.Windows.Forms.Button btn_terminar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_cliente;
		private System.Windows.Forms.TextBox txt_doctor;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_producto;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label lbl_cantidad;
		private System.Windows.Forms.Label lbl_producto;
		private System.Windows.Forms.Label lbl_amecop;
		private System.Windows.Forms.Label lbl_total;
		private System.Windows.Forms.Label lbl_iva;
		private System.Windows.Forms.Label lbl_ieps;
		private System.Windows.Forms.Label lbl_subtotal;
		private System.Windows.Forms.TextBox txt_total;
		private System.Windows.Forms.TextBox txt_iva;
		private System.Windows.Forms.TextBox txt_ieps;
		private System.Windows.Forms.TextBox txt_subtotal;
		private System.Windows.Forms.Button btn_limpiar;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txt_instrucciones;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown txt_cantidad;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_comentario_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn detallado_formula_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn formula_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn materia_prima_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn comentarios;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn importe;
		private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
		private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.Label Sucursal_id;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}