namespace Farmacontrol_PDV.FORMS.inventarios.ajuste_existencias
{
	partial class Ajuste_existencias_principal
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
            this.btn_buscar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_otro_lote = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbb_anio = new System.Windows.Forms.ComboBox();
            this.cbb_mes = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_terminar = new System.Windows.Forms.Button();
            this.txt_estado = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_editar_guardar_comentario = new System.Windows.Forms.Button();
            this.lbl_mensaje_bloqueo = new System.Windows.Forms.Label();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_producto = new System.Windows.Forms.Label();
            this.txt_fecha_terminado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_fecha_creado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_imprimir = new System.Windows.Forms.Button();
            this.btn_nuevo = new System.Windows.Forms.Button();
            this.btn_inicio = new System.Windows.Forms.Button();
            this.btn_fin = new System.Windows.Forms.Button();
            this.btn_atras = new System.Windows.Forms.Button();
            this.btn_siguiente = new System.Windows.Forms.Button();
            this.dgv_ajuste_existencias = new System.Windows.Forms.DataGridView();
            this.detallado_ajuste_existencia_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_existencia_anterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_diferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_empleado_captura = new System.Windows.Forms.TextBox();
            this.txt_empleado_termina = new System.Windows.Forms.TextBox();
            this.cbb_lote = new System.Windows.Forms.ComboBox();
            this.cbb_caducidad = new System.Windows.Forms.ComboBox();
            this.txt_producto = new System.Windows.Forms.TextBox();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.lbl_cantidad = new System.Windows.Forms.Label();
            this.lbl_lote = new System.Windows.Forms.Label();
            this.lbl_caducidad = new System.Windows.Forms.Label();
            this.lbl_amecop = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_folio_busqueda_ajuste = new System.Windows.Forms.NumericUpDown();
            this.txt_cantidad = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ajuste_existencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_folio_busqueda_ajuste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(1083, 33);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(60, 23);
            this.btn_buscar.TabIndex = 139;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(995, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 176;
            this.label10.Text = "Otro Lote";
            // 
            // txt_otro_lote
            // 
            this.txt_otro_lote.Enabled = false;
            this.txt_otro_lote.Location = new System.Drawing.Point(995, 97);
            this.txt_otro_lote.Name = "txt_otro_lote";
            this.txt_otro_lote.Size = new System.Drawing.Size(136, 20);
            this.txt_otro_lote.TabIndex = 175;
            this.txt_otro_lote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_otro_lote_KeyDown);
            this.txt_otro_lote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_otro_lote_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(774, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 174;
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
            this.cbb_anio.Location = new System.Drawing.Point(770, 97);
            this.cbb_anio.Name = "cbb_anio";
            this.cbb_anio.Size = new System.Drawing.Size(60, 21);
            this.cbb_anio.TabIndex = 173;
            this.cbb_anio.DropDown += new System.EventHandler(this.cbb_anio_DropDown);
            this.cbb_anio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_anio_KeyDown);
            // 
            // cbb_mes
            // 
            this.cbb_mes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_mes.Enabled = false;
            this.cbb_mes.FormattingEnabled = true;
            this.cbb_mes.Location = new System.Drawing.Point(716, 97);
            this.cbb_mes.Name = "cbb_mes";
            this.cbb_mes.Size = new System.Drawing.Size(50, 21);
            this.cbb_mes.TabIndex = 172;
            this.cbb_mes.DropDown += new System.EventHandler(this.cbb_mes_DropDown);
            this.cbb_mes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_mes_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(714, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 171;
            this.label8.Text = "Mes";
            // 
            // btn_terminar
            // 
            this.btn_terminar.Location = new System.Drawing.Point(1144, 33);
            this.btn_terminar.Name = "btn_terminar";
            this.btn_terminar.Size = new System.Drawing.Size(60, 23);
            this.btn_terminar.TabIndex = 140;
            this.btn_terminar.Text = "Terminar";
            this.btn_terminar.UseVisualStyleBackColor = true;
            this.btn_terminar.Click += new System.EventHandler(this.btn_terminar_Click);
            // 
            // txt_estado
            // 
            this.txt_estado.BackColor = System.Drawing.Color.Green;
            this.txt_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_estado.ForeColor = System.Drawing.Color.White;
            this.txt_estado.Location = new System.Drawing.Point(1009, 59);
            this.txt_estado.Name = "txt_estado";
            this.txt_estado.ReadOnly = true;
            this.txt_estado.Size = new System.Drawing.Size(104, 20);
            this.txt_estado.TabIndex = 169;
            this.txt_estado.Text = "ABIERTO";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(960, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 168;
            this.label5.Text = "Estado:";
            // 
            // btn_editar_guardar_comentario
            // 
            this.btn_editar_guardar_comentario.Location = new System.Drawing.Point(879, 56);
            this.btn_editar_guardar_comentario.Name = "btn_editar_guardar_comentario";
            this.btn_editar_guardar_comentario.Size = new System.Drawing.Size(75, 23);
            this.btn_editar_guardar_comentario.TabIndex = 167;
            this.btn_editar_guardar_comentario.Text = "Editar";
            this.btn_editar_guardar_comentario.UseVisualStyleBackColor = true;
            this.btn_editar_guardar_comentario.Click += new System.EventHandler(this.btn_editar_guardar_comentario_Click);
            // 
            // lbl_mensaje_bloqueo
            // 
            this.lbl_mensaje_bloqueo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mensaje_bloqueo.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mensaje_bloqueo.ForeColor = System.Drawing.Color.Red;
            this.lbl_mensaje_bloqueo.Location = new System.Drawing.Point(27, 152);
            this.lbl_mensaje_bloqueo.Name = "lbl_mensaje_bloqueo";
            this.lbl_mensaje_bloqueo.Size = new System.Drawing.Size(1160, 60);
            this.lbl_mensaje_bloqueo.TabIndex = 164;
            this.lbl_mensaje_bloqueo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_mensaje_bloqueo.Visible = false;
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Enabled = false;
            this.txt_comentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_comentarios.Location = new System.Drawing.Point(78, 57);
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(795, 21);
            this.txt_comentarios.TabIndex = 161;
            this.txt_comentarios.TextChanged += new System.EventHandler(this.txt_comentarios_TextChanged);
            this.txt_comentarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_comentarios_KeyDown);
            this.txt_comentarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comentarios_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 160;
            this.label6.Text = "Comentarios:";
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(133, 80);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(50, 13);
            this.lbl_producto.TabIndex = 149;
            this.lbl_producto.Text = "Producto";
            // 
            // txt_fecha_terminado
            // 
            this.txt_fecha_terminado.Enabled = false;
            this.txt_fecha_terminado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_terminado.ForeColor = System.Drawing.Color.Red;
            this.txt_fecha_terminado.Location = new System.Drawing.Point(627, 31);
            this.txt_fecha_terminado.Name = "txt_fecha_terminado";
            this.txt_fecha_terminado.Size = new System.Drawing.Size(328, 21);
            this.txt_fecha_terminado.TabIndex = 143;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 141;
            this.label4.Text = "Terminado:";
            // 
            // txt_fecha_creado
            // 
            this.txt_fecha_creado.Enabled = false;
            this.txt_fecha_creado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_creado.Location = new System.Drawing.Point(627, 5);
            this.txt_fecha_creado.Name = "txt_fecha_creado";
            this.txt_fecha_creado.Size = new System.Drawing.Size(328, 21);
            this.txt_fecha_creado.TabIndex = 138;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(577, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 137;
            this.label3.Text = "Creado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 134;
            this.label2.Text = "Termina:";
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(1022, 33);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(60, 23);
            this.btn_imprimir.TabIndex = 136;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // btn_nuevo
            // 
            this.btn_nuevo.Location = new System.Drawing.Point(961, 33);
            this.btn_nuevo.Name = "btn_nuevo";
            this.btn_nuevo.Size = new System.Drawing.Size(60, 23);
            this.btn_nuevo.TabIndex = 135;
            this.btn_nuevo.Text = "Nuevo";
            this.btn_nuevo.UseVisualStyleBackColor = true;
            this.btn_nuevo.Click += new System.EventHandler(this.btn_nuevo_Click);
            // 
            // btn_inicio
            // 
            this.btn_inicio.Location = new System.Drawing.Point(961, 4);
            this.btn_inicio.Name = "btn_inicio";
            this.btn_inicio.Size = new System.Drawing.Size(40, 23);
            this.btn_inicio.TabIndex = 145;
            this.btn_inicio.Text = "<<";
            this.btn_inicio.UseVisualStyleBackColor = true;
            this.btn_inicio.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(1164, 4);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(40, 23);
            this.btn_fin.TabIndex = 146;
            this.btn_fin.Text = ">>";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // btn_atras
            // 
            this.btn_atras.Location = new System.Drawing.Point(1002, 4);
            this.btn_atras.Name = "btn_atras";
            this.btn_atras.Size = new System.Drawing.Size(40, 23);
            this.btn_atras.TabIndex = 142;
            this.btn_atras.Text = "<";
            this.btn_atras.UseVisualStyleBackColor = true;
            this.btn_atras.Click += new System.EventHandler(this.btn_atras_Click);
            // 
            // btn_siguiente
            // 
            this.btn_siguiente.Location = new System.Drawing.Point(1123, 4);
            this.btn_siguiente.Name = "btn_siguiente";
            this.btn_siguiente.Size = new System.Drawing.Size(40, 23);
            this.btn_siguiente.TabIndex = 144;
            this.btn_siguiente.Text = ">";
            this.btn_siguiente.UseVisualStyleBackColor = true;
            this.btn_siguiente.Click += new System.EventHandler(this.btn_siguiente_Click);
            // 
            // dgv_ajuste_existencias
            // 
            this.dgv_ajuste_existencias.AllowUserToAddRows = false;
            this.dgv_ajuste_existencias.AllowUserToDeleteRows = false;
            this.dgv_ajuste_existencias.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_ajuste_existencias.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ajuste_existencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ajuste_existencias.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ajuste_existencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ajuste_existencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ajuste_existencias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detallado_ajuste_existencia_id,
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_existencia_anterior,
            this.c_cantidad,
            this.c_diferencia});
            this.dgv_ajuste_existencias.Enabled = false;
            this.dgv_ajuste_existencias.Location = new System.Drawing.Point(10, 124);
            this.dgv_ajuste_existencias.MultiSelect = false;
            this.dgv_ajuste_existencias.Name = "dgv_ajuste_existencias";
            this.dgv_ajuste_existencias.ReadOnly = true;
            this.dgv_ajuste_existencias.RowHeadersVisible = false;
            this.dgv_ajuste_existencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ajuste_existencias.Size = new System.Drawing.Size(1194, 408);
            this.dgv_ajuste_existencias.TabIndex = 159;
            this.dgv_ajuste_existencias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_ajuste_existencias_KeyDown);
            this.dgv_ajuste_existencias.Leave += new System.EventHandler(this.dgv_ajuste_existencias_Leave);
            // 
            // detallado_ajuste_existencia_id
            // 
            this.detallado_ajuste_existencia_id.DataPropertyName = "detallado_ajuste_existencia_id";
            this.detallado_ajuste_existencia_id.HeaderText = "detallado_ajuste_existencia_id";
            this.detallado_ajuste_existencia_id.Name = "detallado_ajuste_existencia_id";
            this.detallado_ajuste_existencia_id.ReadOnly = true;
            this.detallado_ajuste_existencia_id.Visible = false;
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
            this.c_producto.FillWeight = 180F;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_lote.FillWeight = 120F;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            this.c_lote.ReadOnly = true;
            // 
            // c_existencia_anterior
            // 
            this.c_existencia_anterior.DataPropertyName = "existencia_anterior";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_existencia_anterior.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_existencia_anterior.HeaderText = "Existencia Anterior";
            this.c_existencia_anterior.Name = "c_existencia_anterior";
            this.c_existencia_anterior.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_cantidad.HeaderText = "Existencia Nueva";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_diferencia
            // 
            this.c_diferencia.DataPropertyName = "diferencia";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_diferencia.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_diferencia.FillWeight = 70F;
            this.c_diferencia.HeaderText = "Diferencia";
            this.c_diferencia.Name = "c_diferencia";
            this.c_diferencia.ReadOnly = true;
            // 
            // txt_empleado_captura
            // 
            this.txt_empleado_captura.Enabled = false;
            this.txt_empleado_captura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_captura.Location = new System.Drawing.Point(78, 5);
            this.txt_empleado_captura.Name = "txt_empleado_captura";
            this.txt_empleado_captura.Size = new System.Drawing.Size(477, 21);
            this.txt_empleado_captura.TabIndex = 158;
            // 
            // txt_empleado_termina
            // 
            this.txt_empleado_termina.Enabled = false;
            this.txt_empleado_termina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_termina.Location = new System.Drawing.Point(78, 31);
            this.txt_empleado_termina.Name = "txt_empleado_termina";
            this.txt_empleado_termina.Size = new System.Drawing.Size(477, 21);
            this.txt_empleado_termina.TabIndex = 157;
            // 
            // cbb_lote
            // 
            this.cbb_lote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_lote.Enabled = false;
            this.cbb_lote.FormattingEnabled = true;
            this.cbb_lote.Location = new System.Drawing.Point(835, 97);
            this.cbb_lote.Name = "cbb_lote";
            this.cbb_lote.Size = new System.Drawing.Size(156, 21);
            this.cbb_lote.TabIndex = 155;
            this.cbb_lote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_lote_KeyDown);
            // 
            // cbb_caducidad
            // 
            this.cbb_caducidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_caducidad.Enabled = false;
            this.cbb_caducidad.FormattingEnabled = true;
            this.cbb_caducidad.Location = new System.Drawing.Point(604, 97);
            this.cbb_caducidad.Name = "cbb_caducidad";
            this.cbb_caducidad.Size = new System.Drawing.Size(108, 21);
            this.cbb_caducidad.TabIndex = 154;
            this.cbb_caducidad.SelectedIndexChanged += new System.EventHandler(this.cbb_caducidad_SelectedIndexChanged);
            this.cbb_caducidad.Enter += new System.EventHandler(this.cbb_caducidad_Enter);
            this.cbb_caducidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_caducidad_KeyDown);
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Location = new System.Drawing.Point(136, 97);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.Size = new System.Drawing.Size(462, 20);
            this.txt_producto.TabIndex = 153;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(10, 97);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(122, 20);
            this.txt_amecop.TabIndex = 132;
            this.txt_amecop.Enter += new System.EventHandler(this.txt_amecop_Enter);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // lbl_cantidad
            // 
            this.lbl_cantidad.AutoSize = true;
            this.lbl_cantidad.Location = new System.Drawing.Point(1134, 81);
            this.lbl_cantidad.Name = "lbl_cantidad";
            this.lbl_cantidad.Size = new System.Drawing.Size(49, 13);
            this.lbl_cantidad.TabIndex = 152;
            this.lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_lote
            // 
            this.lbl_lote.AutoSize = true;
            this.lbl_lote.Location = new System.Drawing.Point(832, 82);
            this.lbl_lote.Name = "lbl_lote";
            this.lbl_lote.Size = new System.Drawing.Size(28, 13);
            this.lbl_lote.TabIndex = 151;
            this.lbl_lote.Text = "Lote";
            // 
            // lbl_caducidad
            // 
            this.lbl_caducidad.AutoSize = true;
            this.lbl_caducidad.Location = new System.Drawing.Point(609, 82);
            this.lbl_caducidad.Name = "lbl_caducidad";
            this.lbl_caducidad.Size = new System.Drawing.Size(58, 13);
            this.lbl_caducidad.TabIndex = 150;
            this.lbl_caducidad.Text = "Caducidad";
            // 
            // lbl_amecop
            // 
            this.lbl_amecop.AutoSize = true;
            this.lbl_amecop.Location = new System.Drawing.Point(12, 80);
            this.lbl_amecop.Name = "lbl_amecop";
            this.lbl_amecop.Size = new System.Drawing.Size(46, 13);
            this.lbl_amecop.TabIndex = 148;
            this.lbl_amecop.Text = "Amecop";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 131;
            this.label1.Text = "Captura:";
            // 
            // txt_folio_busqueda_ajuste
            // 
            this.txt_folio_busqueda_ajuste.Location = new System.Drawing.Point(1043, 6);
            this.txt_folio_busqueda_ajuste.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
            this.txt_folio_busqueda_ajuste.Name = "txt_folio_busqueda_ajuste";
            this.txt_folio_busqueda_ajuste.Size = new System.Drawing.Size(80, 20);
            this.txt_folio_busqueda_ajuste.TabIndex = 177;
            this.txt_folio_busqueda_ajuste.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_folio_busqueda_ajuste.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_folio_busqueda_ajuste_KeyDown);
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Enabled = false;
            this.txt_cantidad.Location = new System.Drawing.Point(1137, 98);
            this.txt_cantidad.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(67, 20);
            this.txt_cantidad.TabIndex = 178;
            this.txt_cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_cantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_cantidad.ValueChanged += new System.EventHandler(this.txt_cantidad_ValueChanged);
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // Ajuste_existencias_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 539);
            this.Controls.Add(this.txt_cantidad);
            this.Controls.Add(this.txt_folio_busqueda_ajuste);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_otro_lote);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbb_anio);
            this.Controls.Add(this.cbb_mes);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_terminar);
            this.Controls.Add(this.txt_estado);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_editar_guardar_comentario);
            this.Controls.Add(this.lbl_mensaje_bloqueo);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_producto);
            this.Controls.Add(this.txt_fecha_terminado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_fecha_creado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.btn_nuevo);
            this.Controls.Add(this.btn_inicio);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.btn_atras);
            this.Controls.Add(this.btn_siguiente);
            this.Controls.Add(this.dgv_ajuste_existencias);
            this.Controls.Add(this.txt_empleado_captura);
            this.Controls.Add(this.txt_empleado_termina);
            this.Controls.Add(this.cbb_lote);
            this.Controls.Add(this.cbb_caducidad);
            this.Controls.Add(this.txt_producto);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.lbl_cantidad);
            this.Controls.Add(this.lbl_lote);
            this.Controls.Add(this.lbl_caducidad);
            this.Controls.Add(this.lbl_amecop);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ajuste_existencias_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajuste de existencias";
            this.Load += new System.EventHandler(this.Ajuste_existencias_principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ajuste_existencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_folio_busqueda_ajuste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_buscar;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txt_otro_lote;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cbb_anio;
		private System.Windows.Forms.ComboBox cbb_mes;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btn_terminar;
		private System.Windows.Forms.TextBox txt_estado;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btn_editar_guardar_comentario;
		private System.Windows.Forms.Label lbl_mensaje_bloqueo;
		private System.Windows.Forms.TextBox txt_comentarios;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lbl_producto;
		private System.Windows.Forms.TextBox txt_fecha_terminado;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_fecha_creado;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_imprimir;
		private System.Windows.Forms.Button btn_nuevo;
		private System.Windows.Forms.Button btn_inicio;
		private System.Windows.Forms.Button btn_fin;
		private System.Windows.Forms.Button btn_atras;
        private System.Windows.Forms.Button btn_siguiente;
		private System.Windows.Forms.DataGridView dgv_ajuste_existencias;
		private System.Windows.Forms.TextBox txt_empleado_captura;
        private System.Windows.Forms.TextBox txt_empleado_termina;
		private System.Windows.Forms.ComboBox cbb_lote;
		private System.Windows.Forms.ComboBox cbb_caducidad;
		private System.Windows.Forms.TextBox txt_producto;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label lbl_cantidad;
		private System.Windows.Forms.Label lbl_lote;
		private System.Windows.Forms.Label lbl_caducidad;
		private System.Windows.Forms.Label lbl_amecop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn detallado_ajuste_existencia_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_existencia_anterior;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_diferencia;
        private System.Windows.Forms.NumericUpDown txt_folio_busqueda_ajuste;
        private System.Windows.Forms.NumericUpDown txt_cantidad;

	}
}