namespace Farmacontrol_PDV.FORMS.movimientos.mermas
{
	partial class Mermas_principal
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
            this.btn_buscar = new System.Windows.Forms.Button();
            this.btn_terminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_imprimir = new System.Windows.Forms.Button();
            this.txt_fecha_terminado = new System.Windows.Forms.TextBox();
            this.btn_nuevo = new System.Windows.Forms.Button();
            this.btn_inicio = new System.Windows.Forms.Button();
            this.txt_empleado_termina = new System.Windows.Forms.TextBox();
            this.btn_fin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_atras = new System.Windows.Forms.Button();
            this.txt_empleado_captura = new System.Windows.Forms.TextBox();
            this.btn_siguiente = new System.Windows.Forms.Button();
            this.txt_fecha_creado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_estado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_comentarios_entrada = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_mermas = new System.Windows.Forms.DataGridView();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_detallado_merma_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_merma_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu_principal = new System.Windows.Forms.MenuStrip();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_mensaje_bloqueo = new System.Windows.Forms.Label();
            this.cbb_lote = new System.Windows.Forms.ComboBox();
            this.cbb_caducidad = new System.Windows.Forms.ComboBox();
            this.txt_producto = new System.Windows.Forms.TextBox();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.lbl_cantidad = new System.Windows.Forms.Label();
            this.lbl_lote = new System.Windows.Forms.Label();
            this.lbl_caducidad = new System.Windows.Forms.Label();
            this.lbl_producto = new System.Windows.Forms.Label();
            this.lbl_amecop = new System.Windows.Forms.Label();
            this.txt_cantidad = new System.Windows.Forms.NumericUpDown();
            this.txt_folio_busqueda_traspaso = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_mermas)).BeginInit();
            this.menu_principal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_folio_busqueda_traspaso)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(1100, 49);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(60, 23);
            this.btn_buscar.TabIndex = 269;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            // 
            // btn_terminar
            // 
            this.btn_terminar.Location = new System.Drawing.Point(1161, 49);
            this.btn_terminar.Name = "btn_terminar";
            this.btn_terminar.Size = new System.Drawing.Size(60, 23);
            this.btn_terminar.TabIndex = 270;
            this.btn_terminar.Text = "Terminar";
            this.btn_terminar.UseVisualStyleBackColor = true;
            this.btn_terminar.Click += new System.EventHandler(this.btn_terminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 262;
            this.label1.Text = "Captura:";
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(1039, 49);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(60, 23);
            this.btn_imprimir.TabIndex = 266;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // txt_fecha_terminado
            // 
            this.txt_fecha_terminado.Enabled = false;
            this.txt_fecha_terminado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_terminado.ForeColor = System.Drawing.Color.Red;
            this.txt_fecha_terminado.Location = new System.Drawing.Point(630, 51);
            this.txt_fecha_terminado.Name = "txt_fecha_terminado";
            this.txt_fecha_terminado.Size = new System.Drawing.Size(342, 21);
            this.txt_fecha_terminado.TabIndex = 273;
            // 
            // btn_nuevo
            // 
            this.btn_nuevo.Location = new System.Drawing.Point(978, 49);
            this.btn_nuevo.Name = "btn_nuevo";
            this.btn_nuevo.Size = new System.Drawing.Size(60, 23);
            this.btn_nuevo.TabIndex = 265;
            this.btn_nuevo.Text = "Nuevo";
            this.btn_nuevo.UseVisualStyleBackColor = true;
            this.btn_nuevo.Click += new System.EventHandler(this.btn_nuevo_Click);
            // 
            // btn_inicio
            // 
            this.btn_inicio.Location = new System.Drawing.Point(978, 25);
            this.btn_inicio.Name = "btn_inicio";
            this.btn_inicio.Size = new System.Drawing.Size(40, 23);
            this.btn_inicio.TabIndex = 275;
            this.btn_inicio.Text = "<<";
            this.btn_inicio.UseVisualStyleBackColor = true;
            this.btn_inicio.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // txt_empleado_termina
            // 
            this.txt_empleado_termina.Enabled = false;
            this.txt_empleado_termina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_termina.Location = new System.Drawing.Point(79, 50);
            this.txt_empleado_termina.Name = "txt_empleado_termina";
            this.txt_empleado_termina.Size = new System.Drawing.Size(484, 21);
            this.txt_empleado_termina.TabIndex = 277;
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(1181, 25);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(40, 23);
            this.btn_fin.TabIndex = 276;
            this.btn_fin.Text = ">>";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(569, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 271;
            this.label4.Text = "Terminado:";
            // 
            // btn_atras
            // 
            this.btn_atras.Location = new System.Drawing.Point(1019, 25);
            this.btn_atras.Name = "btn_atras";
            this.btn_atras.Size = new System.Drawing.Size(40, 23);
            this.btn_atras.TabIndex = 272;
            this.btn_atras.Text = "<";
            this.btn_atras.UseVisualStyleBackColor = true;
            this.btn_atras.Click += new System.EventHandler(this.btn_atras_Click);
            // 
            // txt_empleado_captura
            // 
            this.txt_empleado_captura.Enabled = false;
            this.txt_empleado_captura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_captura.Location = new System.Drawing.Point(79, 26);
            this.txt_empleado_captura.Name = "txt_empleado_captura";
            this.txt_empleado_captura.Size = new System.Drawing.Size(484, 21);
            this.txt_empleado_captura.TabIndex = 278;
            // 
            // btn_siguiente
            // 
            this.btn_siguiente.Location = new System.Drawing.Point(1140, 25);
            this.btn_siguiente.Name = "btn_siguiente";
            this.btn_siguiente.Size = new System.Drawing.Size(40, 23);
            this.btn_siguiente.TabIndex = 274;
            this.btn_siguiente.Text = ">";
            this.btn_siguiente.UseVisualStyleBackColor = true;
            this.btn_siguiente.Click += new System.EventHandler(this.btn_siguiente_Click);
            // 
            // txt_fecha_creado
            // 
            this.txt_fecha_creado.Enabled = false;
            this.txt_fecha_creado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_creado.Location = new System.Drawing.Point(630, 27);
            this.txt_fecha_creado.Name = "txt_fecha_creado";
            this.txt_fecha_creado.Size = new System.Drawing.Size(342, 21);
            this.txt_fecha_creado.TabIndex = 268;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 267;
            this.label3.Text = "Creado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 264;
            this.label2.Text = "Termina:";
            // 
            // txt_estado
            // 
            this.txt_estado.BackColor = System.Drawing.Color.Green;
            this.txt_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_estado.ForeColor = System.Drawing.Color.White;
            this.txt_estado.Location = new System.Drawing.Point(1060, 77);
            this.txt_estado.Name = "txt_estado";
            this.txt_estado.ReadOnly = true;
            this.txt_estado.Size = new System.Drawing.Size(133, 20);
            this.txt_estado.TabIndex = 280;
            this.txt_estado.Text = "ABIERTO";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 281;
            this.label8.Text = "Comentarios:";
            // 
            // txt_comentarios_entrada
            // 
            this.txt_comentarios_entrada.Enabled = false;
            this.txt_comentarios_entrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_comentarios_entrada.Location = new System.Drawing.Point(79, 75);
            this.txt_comentarios_entrada.Name = "txt_comentarios_entrada";
            this.txt_comentarios_entrada.Size = new System.Drawing.Size(893, 21);
            this.txt_comentarios_entrada.TabIndex = 282;
            this.txt_comentarios_entrada.Leave += new System.EventHandler(this.txt_comentarios_entrada_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1011, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 279;
            this.label5.Text = "Estado:";
            // 
            // dgv_mermas
            // 
            this.dgv_mermas.AllowUserToAddRows = false;
            this.dgv_mermas.AllowUserToDeleteRows = false;
            this.dgv_mermas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_mermas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_mermas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_mermas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_mermas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_mermas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_mermas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_precio_costo,
            this.c_cantidad,
            this.c_total,
            this.c_detallado_merma_id,
            this.c_merma_id});
            this.dgv_mermas.Location = new System.Drawing.Point(8, 142);
            this.dgv_mermas.MultiSelect = false;
            this.dgv_mermas.Name = "dgv_mermas";
            this.dgv_mermas.ReadOnly = true;
            this.dgv_mermas.RowHeadersVisible = false;
            this.dgv_mermas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_mermas.Size = new System.Drawing.Size(1208, 424);
            this.dgv_mermas.TabIndex = 283;
            this.dgv_mermas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_mermas_KeyDown);
            // 
            // c_amecop
            // 
            this.c_amecop.DataPropertyName = "amecop";
            this.c_amecop.FillWeight = 90F;
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
            // c_precio_costo
            // 
            this.c_precio_costo.DataPropertyName = "precio_costo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.c_precio_costo.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_precio_costo.FillWeight = 90F;
            this.c_precio_costo.HeaderText = "Precio Costo";
            this.c_precio_costo.Name = "c_precio_costo";
            this.c_precio_costo.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_cantidad.FillWeight = 70F;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_total
            // 
            this.c_total.DataPropertyName = "total";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.c_total.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_total.HeaderText = "Total";
            this.c_total.Name = "c_total";
            this.c_total.ReadOnly = true;
            // 
            // c_detallado_merma_id
            // 
            this.c_detallado_merma_id.DataPropertyName = "detallado_merma_id";
            this.c_detallado_merma_id.HeaderText = "c_detallado_merma_id";
            this.c_detallado_merma_id.Name = "c_detallado_merma_id";
            this.c_detallado_merma_id.ReadOnly = true;
            this.c_detallado_merma_id.Visible = false;
            // 
            // c_merma_id
            // 
            this.c_merma_id.DataPropertyName = "merma_id";
            this.c_merma_id.HeaderText = "c_merma_id";
            this.c_merma_id.Name = "c_merma_id";
            this.c_merma_id.ReadOnly = true;
            this.c_merma_id.Visible = false;
            // 
            // menu_principal
            // 
            this.menu_principal.AllowMerge = false;
            this.menu_principal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarToolStripMenuItem});
            this.menu_principal.Location = new System.Drawing.Point(0, 0);
            this.menu_principal.Name = "menu_principal";
            this.menu_principal.ShowItemToolTips = true;
            this.menu_principal.Size = new System.Drawing.Size(1228, 24);
            this.menu_principal.TabIndex = 284;
            this.menu_principal.Text = "menu_principal";
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.actualizarToolStripMenuItem.Text = "Actualizar";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // lbl_mensaje_bloqueo
            // 
            this.lbl_mensaje_bloqueo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mensaje_bloqueo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mensaje_bloqueo.ForeColor = System.Drawing.Color.Red;
            this.lbl_mensaje_bloqueo.Location = new System.Drawing.Point(19, 169);
            this.lbl_mensaje_bloqueo.Name = "lbl_mensaje_bloqueo";
            this.lbl_mensaje_bloqueo.Size = new System.Drawing.Size(1178, 102);
            this.lbl_mensaje_bloqueo.TabIndex = 285;
            this.lbl_mensaje_bloqueo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbb_lote
            // 
            this.cbb_lote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_lote.Enabled = false;
            this.cbb_lote.FormattingEnabled = true;
            this.cbb_lote.Location = new System.Drawing.Point(929, 115);
            this.cbb_lote.Name = "cbb_lote";
            this.cbb_lote.Size = new System.Drawing.Size(197, 21);
            this.cbb_lote.TabIndex = 295;
            this.cbb_lote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_lote_KeyDown);
            // 
            // cbb_caducidad
            // 
            this.cbb_caducidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_caducidad.Enabled = false;
            this.cbb_caducidad.FormattingEnabled = true;
            this.cbb_caducidad.Location = new System.Drawing.Point(786, 115);
            this.cbb_caducidad.Name = "cbb_caducidad";
            this.cbb_caducidad.Size = new System.Drawing.Size(137, 21);
            this.cbb_caducidad.TabIndex = 294;
            this.cbb_caducidad.SelectedIndexChanged += new System.EventHandler(this.cbb_caducidad_SelectedIndexChanged);
            this.cbb_caducidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_caducidad_KeyDown);
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Location = new System.Drawing.Point(154, 116);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.Size = new System.Drawing.Size(626, 20);
            this.txt_producto.TabIndex = 293;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Enabled = false;
            this.txt_amecop.Location = new System.Drawing.Point(8, 116);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(140, 20);
            this.txt_amecop.TabIndex = 286;
            this.txt_amecop.Enter += new System.EventHandler(this.txt_amecop_Enter);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // lbl_cantidad
            // 
            this.lbl_cantidad.AutoSize = true;
            this.lbl_cantidad.Location = new System.Drawing.Point(1129, 99);
            this.lbl_cantidad.Name = "lbl_cantidad";
            this.lbl_cantidad.Size = new System.Drawing.Size(49, 13);
            this.lbl_cantidad.TabIndex = 292;
            this.lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_lote
            // 
            this.lbl_lote.AutoSize = true;
            this.lbl_lote.Location = new System.Drawing.Point(926, 98);
            this.lbl_lote.Name = "lbl_lote";
            this.lbl_lote.Size = new System.Drawing.Size(28, 13);
            this.lbl_lote.TabIndex = 290;
            this.lbl_lote.Text = "Lote";
            // 
            // lbl_caducidad
            // 
            this.lbl_caducidad.AutoSize = true;
            this.lbl_caducidad.Location = new System.Drawing.Point(783, 98);
            this.lbl_caducidad.Name = "lbl_caducidad";
            this.lbl_caducidad.Size = new System.Drawing.Size(58, 13);
            this.lbl_caducidad.TabIndex = 289;
            this.lbl_caducidad.Text = "Caducidad";
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(151, 98);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(50, 13);
            this.lbl_producto.TabIndex = 288;
            this.lbl_producto.Text = "Producto";
            // 
            // lbl_amecop
            // 
            this.lbl_amecop.AutoSize = true;
            this.lbl_amecop.Location = new System.Drawing.Point(5, 99);
            this.lbl_amecop.Name = "lbl_amecop";
            this.lbl_amecop.Size = new System.Drawing.Size(46, 13);
            this.lbl_amecop.TabIndex = 287;
            this.lbl_amecop.Text = "Amecop";
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Enabled = false;
            this.txt_cantidad.Location = new System.Drawing.Point(1132, 115);
            this.txt_cantidad.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
            this.txt_cantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(84, 20);
            this.txt_cantidad.TabIndex = 296;
            this.txt_cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_cantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // txt_folio_busqueda_traspaso
            // 
            this.txt_folio_busqueda_traspaso.Location = new System.Drawing.Point(1063, 26);
            this.txt_folio_busqueda_traspaso.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
            this.txt_folio_busqueda_traspaso.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_folio_busqueda_traspaso.Name = "txt_folio_busqueda_traspaso";
            this.txt_folio_busqueda_traspaso.Size = new System.Drawing.Size(75, 20);
            this.txt_folio_busqueda_traspaso.TabIndex = 297;
            this.txt_folio_busqueda_traspaso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_folio_busqueda_traspaso.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_folio_busqueda_traspaso.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_folio_busqueda_traspaso_KeyDown);
            // 
            // Mermas_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 578);
            this.Controls.Add(this.txt_folio_busqueda_traspaso);
            this.Controls.Add(this.txt_cantidad);
            this.Controls.Add(this.cbb_lote);
            this.Controls.Add(this.cbb_caducidad);
            this.Controls.Add(this.txt_producto);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.lbl_cantidad);
            this.Controls.Add(this.lbl_lote);
            this.Controls.Add(this.lbl_caducidad);
            this.Controls.Add(this.lbl_producto);
            this.Controls.Add(this.lbl_amecop);
            this.Controls.Add(this.lbl_mensaje_bloqueo);
            this.Controls.Add(this.menu_principal);
            this.Controls.Add(this.dgv_mermas);
            this.Controls.Add(this.txt_estado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_comentarios_entrada);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.btn_terminar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.txt_fecha_terminado);
            this.Controls.Add(this.btn_nuevo);
            this.Controls.Add(this.btn_inicio);
            this.Controls.Add(this.txt_empleado_termina);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_atras);
            this.Controls.Add(this.txt_empleado_captura);
            this.Controls.Add(this.btn_siguiente);
            this.Controls.Add(this.txt_fecha_creado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mermas_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mermas";
            this.Shown += new System.EventHandler(this.Mermas_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_mermas)).EndInit();
            this.menu_principal.ResumeLayout(false);
            this.menu_principal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_folio_busqueda_traspaso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_buscar;
		private System.Windows.Forms.Button btn_terminar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_imprimir;
		private System.Windows.Forms.TextBox txt_fecha_terminado;
		private System.Windows.Forms.Button btn_nuevo;
		private System.Windows.Forms.Button btn_inicio;
		private System.Windows.Forms.TextBox txt_empleado_termina;
		private System.Windows.Forms.Button btn_fin;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btn_atras;
		private System.Windows.Forms.TextBox txt_empleado_captura;
		private System.Windows.Forms.Button btn_siguiente;
        private System.Windows.Forms.TextBox txt_fecha_creado;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_estado;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txt_comentarios_entrada;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DataGridView dgv_mermas;
		private System.Windows.Forms.MenuStrip menu_principal;
		private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.Label lbl_mensaje_bloqueo;
		private System.Windows.Forms.ComboBox cbb_lote;
		private System.Windows.Forms.ComboBox cbb_caducidad;
		private System.Windows.Forms.TextBox txt_producto;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label lbl_cantidad;
		private System.Windows.Forms.Label lbl_lote;
		private System.Windows.Forms.Label lbl_caducidad;
		private System.Windows.Forms.Label lbl_producto;
		private System.Windows.Forms.Label lbl_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_precio_costo;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_total;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_detallado_merma_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_merma_id;
        private System.Windows.Forms.NumericUpDown txt_cantidad;
        private System.Windows.Forms.NumericUpDown txt_folio_busqueda_traspaso;
	}
}