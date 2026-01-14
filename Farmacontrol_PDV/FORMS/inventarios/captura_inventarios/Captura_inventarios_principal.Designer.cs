namespace Farmacontrol_PDV.FORMS.inventarios.captura_inventarios
{
	partial class Captura_inventarios_principal
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
            this.label8 = new System.Windows.Forms.Label();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_empleado_captura = new System.Windows.Forms.TextBox();
            this.txt_fecha_creado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cantidad = new System.Windows.Forms.TextBox();
            this.txt_producto = new System.Windows.Forms.TextBox();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.lbl_cantidad = new System.Windows.Forms.Label();
            this.lbl_lote = new System.Windows.Forms.Label();
            this.lbl_producto = new System.Windows.Forms.Label();
            this.lbl_amecop = new System.Windows.Forms.Label();
            this.btn_inicio = new System.Windows.Forms.Button();
            this.btn_fin = new System.Windows.Forms.Button();
            this.btn_atras = new System.Windows.Forms.Button();
            this.btn_siguiente = new System.Windows.Forms.Button();
            this.txt_folio_busqueda_traspaso = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_inventario_id = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_nombre_terminal = new System.Windows.Forms.TextBox();
            this.dgv_inventarios_folios = new System.Windows.Forms.DataGridView();
            this.c_detallado_inventario_folio_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_existencia_anterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_existencia_posterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_diferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_mensaje_bloqueo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbb_anio = new System.Windows.Forms.ComboBox();
            this.cbb_mes = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_lote = new System.Windows.Forms.TextBox();
            this.chb_lote = new System.Windows.Forms.CheckBox();
            this.menu_principal = new System.Windows.Forms.MenuStrip();
            this.Herramientas = new System.Windows.Forms.ToolStripMenuItem();
            this.terminal = new System.Windows.Forms.ToolStripMenuItem();
            this.desasociar_terminal = new System.Windows.Forms.ToolStripMenuItem();
            this.asociar_terminal = new System.Windows.Forms.ToolStripMenuItem();
            this.terninarCapturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chb_sin_caducidad = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_inventarios_folios)).BeginInit();
            this.menu_principal.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 336;
            this.label8.Text = "Comentarios:";
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_comentarios.Location = new System.Drawing.Point(86, 54);
            this.txt_comentarios.Multiline = true;
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(619, 45);
            this.txt_comentarios.TabIndex = 337;
            this.txt_comentarios.Leave += new System.EventHandler(this.txt_comentarios_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 323;
            this.label1.Text = "Captura:";
            // 
            // txt_empleado_captura
            // 
            this.txt_empleado_captura.Enabled = false;
            this.txt_empleado_captura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_captura.Location = new System.Drawing.Point(86, 29);
            this.txt_empleado_captura.Name = "txt_empleado_captura";
            this.txt_empleado_captura.Size = new System.Drawing.Size(619, 21);
            this.txt_empleado_captura.TabIndex = 335;
            // 
            // txt_fecha_creado
            // 
            this.txt_fecha_creado.Enabled = false;
            this.txt_fecha_creado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_creado.Location = new System.Drawing.Point(761, 29);
            this.txt_fecha_creado.Name = "txt_fecha_creado";
            this.txt_fecha_creado.Size = new System.Drawing.Size(342, 21);
            this.txt_fecha_creado.TabIndex = 327;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(711, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 326;
            this.label3.Text = "Creado:";
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Enabled = false;
            this.txt_cantidad.Location = new System.Drawing.Point(1009, 119);
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(84, 20);
            this.txt_cantidad.TabIndex = 350;
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            this.txt_cantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cantidad_KeyPress);
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Location = new System.Drawing.Point(155, 119);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.Size = new System.Drawing.Size(392, 20);
            this.txt_producto.TabIndex = 346;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(13, 119);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(140, 20);
            this.txt_amecop.TabIndex = 339;
            this.txt_amecop.Enter += new System.EventHandler(this.txt_amecop_Enter);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // lbl_cantidad
            // 
            this.lbl_cantidad.AutoSize = true;
            this.lbl_cantidad.Location = new System.Drawing.Point(1006, 103);
            this.lbl_cantidad.Name = "lbl_cantidad";
            this.lbl_cantidad.Size = new System.Drawing.Size(49, 13);
            this.lbl_cantidad.TabIndex = 345;
            this.lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_lote
            // 
            this.lbl_lote.AutoSize = true;
            this.lbl_lote.Location = new System.Drawing.Point(772, 101);
            this.lbl_lote.Name = "lbl_lote";
            this.lbl_lote.Size = new System.Drawing.Size(28, 13);
            this.lbl_lote.TabIndex = 343;
            this.lbl_lote.Text = "Lote";
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(152, 101);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(50, 13);
            this.lbl_producto.TabIndex = 341;
            this.lbl_producto.Text = "Producto";
            // 
            // lbl_amecop
            // 
            this.lbl_amecop.AutoSize = true;
            this.lbl_amecop.Location = new System.Drawing.Point(10, 102);
            this.lbl_amecop.Name = "lbl_amecop";
            this.lbl_amecop.Size = new System.Drawing.Size(46, 13);
            this.lbl_amecop.TabIndex = 340;
            this.lbl_amecop.Text = "Amecop";
            // 
            // btn_inicio
            // 
            this.btn_inicio.Location = new System.Drawing.Point(883, 55);
            this.btn_inicio.Name = "btn_inicio";
            this.btn_inicio.Size = new System.Drawing.Size(35, 23);
            this.btn_inicio.TabIndex = 354;
            this.btn_inicio.Text = "<<";
            this.btn_inicio.UseVisualStyleBackColor = true;
            this.btn_inicio.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(1068, 55);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(35, 23);
            this.btn_fin.TabIndex = 355;
            this.btn_fin.Text = ">>";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // btn_atras
            // 
            this.btn_atras.Location = new System.Drawing.Point(918, 55);
            this.btn_atras.Name = "btn_atras";
            this.btn_atras.Size = new System.Drawing.Size(35, 23);
            this.btn_atras.TabIndex = 352;
            this.btn_atras.Text = "<";
            this.btn_atras.UseVisualStyleBackColor = true;
            this.btn_atras.Click += new System.EventHandler(this.btn_atras_Click);
            // 
            // btn_siguiente
            // 
            this.btn_siguiente.Location = new System.Drawing.Point(1033, 55);
            this.btn_siguiente.Name = "btn_siguiente";
            this.btn_siguiente.Size = new System.Drawing.Size(35, 23);
            this.btn_siguiente.TabIndex = 353;
            this.btn_siguiente.Text = ">";
            this.btn_siguiente.UseVisualStyleBackColor = true;
            this.btn_siguiente.Click += new System.EventHandler(this.btn_siguiente_Click);
            // 
            // txt_folio_busqueda_traspaso
            // 
            this.txt_folio_busqueda_traspaso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_folio_busqueda_traspaso.ForeColor = System.Drawing.Color.Red;
            this.txt_folio_busqueda_traspaso.Location = new System.Drawing.Point(954, 55);
            this.txt_folio_busqueda_traspaso.Name = "txt_folio_busqueda_traspaso";
            this.txt_folio_busqueda_traspaso.Size = new System.Drawing.Size(78, 22);
            this.txt_folio_busqueda_traspaso.TabIndex = 351;
            this.txt_folio_busqueda_traspaso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_folio_busqueda_traspaso.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_folio_busqueda_traspaso_KeyDown);
            this.txt_folio_busqueda_traspaso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_folio_busqueda_traspaso_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(707, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 356;
            this.label5.Text = "Jornada:";
            // 
            // txt_inventario_id
            // 
            this.txt_inventario_id.Enabled = false;
            this.txt_inventario_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_inventario_id.Location = new System.Drawing.Point(761, 55);
            this.txt_inventario_id.Name = "txt_inventario_id";
            this.txt_inventario_id.Size = new System.Drawing.Size(116, 21);
            this.txt_inventario_id.TabIndex = 357;
            this.txt_inventario_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(827, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 358;
            this.label6.Text = "Terminal:";
            // 
            // txt_nombre_terminal
            // 
            this.txt_nombre_terminal.Enabled = false;
            this.txt_nombre_terminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombre_terminal.Location = new System.Drawing.Point(883, 79);
            this.txt_nombre_terminal.Name = "txt_nombre_terminal";
            this.txt_nombre_terminal.Size = new System.Drawing.Size(220, 21);
            this.txt_nombre_terminal.TabIndex = 359;
            // 
            // dgv_inventarios_folios
            // 
            this.dgv_inventarios_folios.AllowUserToAddRows = false;
            this.dgv_inventarios_folios.AllowUserToDeleteRows = false;
            this.dgv_inventarios_folios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgv_inventarios_folios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_inventarios_folios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_inventarios_folios.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_inventarios_folios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_inventarios_folios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_inventarios_folios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_detallado_inventario_folio_id,
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_existencia_anterior,
            this.c_cantidad,
            this.c_existencia_posterior,
            this.c_diferencia,
            this.c_precio_costo,
            this.c_total});
            this.dgv_inventarios_folios.Location = new System.Drawing.Point(12, 145);
            this.dgv_inventarios_folios.MultiSelect = false;
            this.dgv_inventarios_folios.Name = "dgv_inventarios_folios";
            this.dgv_inventarios_folios.ReadOnly = true;
            this.dgv_inventarios_folios.RowHeadersVisible = false;
            this.dgv_inventarios_folios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_inventarios_folios.Size = new System.Drawing.Size(1091, 394);
            this.dgv_inventarios_folios.TabIndex = 362;
            this.dgv_inventarios_folios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_inventarios_folios_CellFormatting);
            this.dgv_inventarios_folios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_inventarios_folios_KeyDown);
            // 
            // c_detallado_inventario_folio_id
            // 
            this.c_detallado_inventario_folio_id.DataPropertyName = "detallado_inventario_folio_id";
            this.c_detallado_inventario_folio_id.HeaderText = "c_detallado_inventario_folio_id";
            this.c_detallado_inventario_folio_id.Name = "c_detallado_inventario_folio_id";
            this.c_detallado_inventario_folio_id.ReadOnly = true;
            this.c_detallado_inventario_folio_id.Visible = false;
            // 
            // c_amecop
            // 
            this.c_amecop.DataPropertyName = "amecop";
            this.c_amecop.HeaderText = "Amecop";
            this.c_amecop.Name = "c_amecop";
            this.c_amecop.ReadOnly = true;
            // 
            // c_producto
            // 
            this.c_producto.DataPropertyName = "producto";
            this.c_producto.FillWeight = 200F;
            this.c_producto.HeaderText = "Producto";
            this.c_producto.Name = "c_producto";
            this.c_producto.ReadOnly = true;
            // 
            // c_caducidad
            // 
            this.c_caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_caducidad.FillWeight = 70F;
            this.c_caducidad.HeaderText = "Caducidad";
            this.c_caducidad.Name = "c_caducidad";
            this.c_caducidad.ReadOnly = true;
            // 
            // c_lote
            // 
            this.c_lote.DataPropertyName = "lote";
            this.c_lote.FillWeight = 150F;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            this.c_lote.ReadOnly = true;
            // 
            // c_existencia_anterior
            // 
            this.c_existencia_anterior.DataPropertyName = "existencia_anterior";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_existencia_anterior.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_existencia_anterior.FillWeight = 70F;
            this.c_existencia_anterior.HeaderText = "Ex. Ant.";
            this.c_existencia_anterior.Name = "c_existencia_anterior";
            this.c_existencia_anterior.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_cantidad.FillWeight = 70F;
            this.c_cantidad.HeaderText = "Cant.";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_existencia_posterior
            // 
            this.c_existencia_posterior.DataPropertyName = "existencia_posterior";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_existencia_posterior.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_existencia_posterior.FillWeight = 70F;
            this.c_existencia_posterior.HeaderText = "Ex. Pos.";
            this.c_existencia_posterior.Name = "c_existencia_posterior";
            this.c_existencia_posterior.ReadOnly = true;
            // 
            // c_diferencia
            // 
            this.c_diferencia.DataPropertyName = "diferencia";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_diferencia.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_diferencia.FillWeight = 70F;
            this.c_diferencia.HeaderText = "Dif.";
            this.c_diferencia.Name = "c_diferencia";
            this.c_diferencia.ReadOnly = true;
            // 
            // c_precio_costo
            // 
            this.c_precio_costo.DataPropertyName = "precio_costo";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "C2";
            dataGridViewCellStyle8.NullValue = null;
            this.c_precio_costo.DefaultCellStyle = dataGridViewCellStyle8;
            this.c_precio_costo.FillWeight = 90F;
            this.c_precio_costo.HeaderText = "Precio Costo";
            this.c_precio_costo.Name = "c_precio_costo";
            this.c_precio_costo.ReadOnly = true;
            // 
            // c_total
            // 
            this.c_total.DataPropertyName = "total";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "C2";
            dataGridViewCellStyle9.NullValue = null;
            this.c_total.DefaultCellStyle = dataGridViewCellStyle9;
            this.c_total.FillWeight = 80F;
            this.c_total.HeaderText = "Total";
            this.c_total.Name = "c_total";
            this.c_total.ReadOnly = true;
            // 
            // lbl_mensaje_bloqueo
            // 
            this.lbl_mensaje_bloqueo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mensaje_bloqueo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mensaje_bloqueo.ForeColor = System.Drawing.Color.Red;
            this.lbl_mensaje_bloqueo.Location = new System.Drawing.Point(21, 173);
            this.lbl_mensaje_bloqueo.Name = "lbl_mensaje_bloqueo";
            this.lbl_mensaje_bloqueo.Size = new System.Drawing.Size(1072, 60);
            this.lbl_mensaje_bloqueo.TabIndex = 363;
            this.lbl_mensaje_bloqueo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(606, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 367;
            this.label9.Text = "Año";
            // 
            // cbb_anio
            // 
            this.cbb_anio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_anio.Enabled = false;
            this.cbb_anio.FormattingEnabled = true;
            this.cbb_anio.Items.AddRange(new object[] {
            "2013",
            "2014",
            "2015",
            "2016",
            "2017"});
            this.cbb_anio.Location = new System.Drawing.Point(608, 119);
            this.cbb_anio.Name = "cbb_anio";
            this.cbb_anio.Size = new System.Drawing.Size(60, 21);
            this.cbb_anio.TabIndex = 366;
            this.cbb_anio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_anio_KeyDown);
            // 
            // cbb_mes
            // 
            this.cbb_mes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_mes.Enabled = false;
            this.cbb_mes.FormattingEnabled = true;
            this.cbb_mes.Location = new System.Drawing.Point(553, 119);
            this.cbb_mes.Name = "cbb_mes";
            this.cbb_mes.Size = new System.Drawing.Size(50, 21);
            this.cbb_mes.TabIndex = 365;
            this.cbb_mes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_mes_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(553, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 364;
            this.label10.Text = "Mes";
            // 
            // txt_lote
            // 
            this.txt_lote.Enabled = false;
            this.txt_lote.Location = new System.Drawing.Point(775, 118);
            this.txt_lote.Name = "txt_lote";
            this.txt_lote.Size = new System.Drawing.Size(156, 20);
            this.txt_lote.TabIndex = 368;
            this.txt_lote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_lote_KeyDown);
            this.txt_lote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_lote_KeyPress);
            // 
            // chb_lote
            // 
            this.chb_lote.AutoSize = true;
            this.chb_lote.Enabled = false;
            this.chb_lote.Location = new System.Drawing.Point(938, 120);
            this.chb_lote.Name = "chb_lote";
            this.chb_lote.Size = new System.Drawing.Size(65, 17);
            this.chb_lote.TabIndex = 369;
            this.chb_lote.Text = "Sin Lote";
            this.chb_lote.UseVisualStyleBackColor = true;
            this.chb_lote.CheckedChanged += new System.EventHandler(this.chb_lote_CheckedChanged);
            // 
            // menu_principal
            // 
            this.menu_principal.AllowMerge = false;
            this.menu_principal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Herramientas,
            this.actualizarToolStripMenuItem});
            this.menu_principal.Location = new System.Drawing.Point(0, 0);
            this.menu_principal.Name = "menu_principal";
            this.menu_principal.ShowItemToolTips = true;
            this.menu_principal.Size = new System.Drawing.Size(1115, 24);
            this.menu_principal.TabIndex = 370;
            this.menu_principal.Text = "menu_principal";
            // 
            // Herramientas
            // 
            this.Herramientas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminal,
            this.terninarCapturaToolStripMenuItem});
            this.Herramientas.Name = "Herramientas";
            this.Herramientas.Size = new System.Drawing.Size(90, 20);
            this.Herramientas.Text = "Herramientas";
            // 
            // terminal
            // 
            this.terminal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desasociar_terminal,
            this.asociar_terminal});
            this.terminal.Name = "terminal";
            this.terminal.Size = new System.Drawing.Size(163, 22);
            this.terminal.Text = "Terminal";
            // 
            // desasociar_terminal
            // 
            this.desasociar_terminal.Name = "desasociar_terminal";
            this.desasociar_terminal.Size = new System.Drawing.Size(180, 22);
            this.desasociar_terminal.Text = "Desasociar Terminal";
            this.desasociar_terminal.Click += new System.EventHandler(this.desasociar_terminal_Click);
            // 
            // asociar_terminal
            // 
            this.asociar_terminal.Name = "asociar_terminal";
            this.asociar_terminal.Size = new System.Drawing.Size(180, 22);
            this.asociar_terminal.Text = "Asociar Terminal";
            this.asociar_terminal.Click += new System.EventHandler(this.asociar_terminal_Click);
            // 
            // terninarCapturaToolStripMenuItem
            // 
            this.terninarCapturaToolStripMenuItem.Name = "terninarCapturaToolStripMenuItem";
            this.terninarCapturaToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.terninarCapturaToolStripMenuItem.Text = "Terninar Captura";
            this.terninarCapturaToolStripMenuItem.Click += new System.EventHandler(this.terninarCapturaToolStripMenuItem_Click);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.actualizarToolStripMenuItem.Text = "Actualizar";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click_1);
            // 
            // chb_sin_caducidad
            // 
            this.chb_sin_caducidad.AutoSize = true;
            this.chb_sin_caducidad.Enabled = false;
            this.chb_sin_caducidad.Location = new System.Drawing.Point(674, 121);
            this.chb_sin_caducidad.Name = "chb_sin_caducidad";
            this.chb_sin_caducidad.Size = new System.Drawing.Size(95, 17);
            this.chb_sin_caducidad.TabIndex = 371;
            this.chb_sin_caducidad.Text = "Sin Caducidad";
            this.chb_sin_caducidad.UseVisualStyleBackColor = true;
            this.chb_sin_caducidad.CheckedChanged += new System.EventHandler(this.chb_sin_caducidad_CheckedChanged);
            // 
            // Captura_inventarios_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 548);
            this.Controls.Add(this.chb_sin_caducidad);
            this.Controls.Add(this.menu_principal);
            this.Controls.Add(this.chb_lote);
            this.Controls.Add(this.txt_lote);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbb_anio);
            this.Controls.Add(this.cbb_mes);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbl_mensaje_bloqueo);
            this.Controls.Add(this.dgv_inventarios_folios);
            this.Controls.Add(this.txt_nombre_terminal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_inventario_id);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_inicio);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.btn_atras);
            this.Controls.Add(this.btn_siguiente);
            this.Controls.Add(this.txt_folio_busqueda_traspaso);
            this.Controls.Add(this.txt_cantidad);
            this.Controls.Add(this.txt_producto);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.lbl_cantidad);
            this.Controls.Add(this.lbl_lote);
            this.Controls.Add(this.lbl_producto);
            this.Controls.Add(this.lbl_amecop);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_empleado_captura);
            this.Controls.Add(this.txt_fecha_creado);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Captura_inventarios_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folios de captura de inventario";
            this.Shown += new System.EventHandler(this.Captura_inventarios_principal_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Captura_inventarios_principal_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_inventarios_folios)).EndInit();
            this.menu_principal.ResumeLayout(false);
            this.menu_principal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txt_comentarios;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_empleado_captura;
		private System.Windows.Forms.TextBox txt_fecha_creado;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_cantidad;
		private System.Windows.Forms.TextBox txt_producto;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label lbl_cantidad;
		private System.Windows.Forms.Label lbl_lote;
		private System.Windows.Forms.Label lbl_producto;
		private System.Windows.Forms.Label lbl_amecop;
		private System.Windows.Forms.Button btn_inicio;
		private System.Windows.Forms.Button btn_fin;
		private System.Windows.Forms.Button btn_atras;
		private System.Windows.Forms.Button btn_siguiente;
		private System.Windows.Forms.TextBox txt_folio_busqueda_traspaso;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txt_inventario_id;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txt_nombre_terminal;
		private System.Windows.Forms.DataGridView dgv_inventarios_folios;
		private System.Windows.Forms.Label lbl_mensaje_bloqueo;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cbb_anio;
		private System.Windows.Forms.ComboBox cbb_mes;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txt_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_detallado_inventario_folio_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_existencia_anterior;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_existencia_posterior;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_diferencia;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_precio_costo;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_total;
		private System.Windows.Forms.CheckBox chb_lote;
		private System.Windows.Forms.MenuStrip menu_principal;
		private System.Windows.Forms.ToolStripMenuItem Herramientas;
		private System.Windows.Forms.ToolStripMenuItem terminal;
		private System.Windows.Forms.ToolStripMenuItem desasociar_terminal;
		private System.Windows.Forms.ToolStripMenuItem asociar_terminal;
		private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem terninarCapturaToolStripMenuItem;
		private System.Windows.Forms.CheckBox chb_sin_caducidad;
	}
}