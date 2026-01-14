namespace Farmacontrol_PDV.FORMS.inventarios.creacion_inventarios
{
	partial class Creacion_inventarios_principal
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
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_principal = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.crear_jornada_inventario = new System.Windows.Forms.ToolStripMenuItem();
            this.informacion_general_inventario = new System.Windows.Forms.ToolStripMenuItem();
            this.buscar_jornada_inventario = new System.Windows.Forms.ToolStripMenuItem();
            this.terminar_jornada_inventario = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_mensaje_bloqueo = new System.Windows.Forms.Label();
            this.dgv_inventarios_folios = new System.Windows.Forms.DataGridView();
            this.c_inventario_folio_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_nombre_terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_estado = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_fecha_terminado = new System.Windows.Forms.TextBox();
            this.btn_inicio = new System.Windows.Forms.Button();
            this.txt_empleado_termina = new System.Windows.Forms.TextBox();
            this.btn_fin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_atras = new System.Windows.Forms.Button();
            this.txt_empleado_captura = new System.Windows.Forms.TextBox();
            this.btn_siguiente = new System.Windows.Forms.Button();
            this.txt_fecha_creado = new System.Windows.Forms.TextBox();
            this.txt_folio_busqueda_traspaso = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.btn_iniciar_detener = new System.Windows.Forms.Button();
            this.menu_principal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_inventarios_folios)).BeginInit();
            this.SuspendLayout();
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.actualizarToolStripMenuItem.Text = "Actualizar";
            // 
            // menu_principal
            // 
            this.menu_principal.AllowMerge = false;
            this.menu_principal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.actualizarToolStripMenuItem});
            this.menu_principal.Location = new System.Drawing.Point(0, 0);
            this.menu_principal.Name = "menu_principal";
            this.menu_principal.ShowItemToolTips = true;
            this.menu_principal.Size = new System.Drawing.Size(987, 24);
            this.menu_principal.TabIndex = 320;
            this.menu_principal.Text = "menu_principal";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crear_jornada_inventario,
            this.informacion_general_inventario,
            this.buscar_jornada_inventario,
            this.terminar_jornada_inventario});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(72, 20);
            this.toolStripMenuItem1.Text = "Inventario";
            // 
            // crear_jornada_inventario
            // 
            this.crear_jornada_inventario.Name = "crear_jornada_inventario";
            this.crear_jornada_inventario.Size = new System.Drawing.Size(256, 22);
            this.crear_jornada_inventario.Text = "Crear jornada de inventario";
            this.crear_jornada_inventario.Click += new System.EventHandler(this.crear_jornada_inventario_Click);
            // 
            // informacion_general_inventario
            // 
            this.informacion_general_inventario.Name = "informacion_general_inventario";
            this.informacion_general_inventario.Size = new System.Drawing.Size(256, 22);
            this.informacion_general_inventario.Text = "Informacion general del inventario";
            this.informacion_general_inventario.Click += new System.EventHandler(this.informacion_general_inventario_Click);
            // 
            // buscar_jornada_inventario
            // 
            this.buscar_jornada_inventario.Name = "buscar_jornada_inventario";
            this.buscar_jornada_inventario.Size = new System.Drawing.Size(256, 22);
            this.buscar_jornada_inventario.Text = "Buscar jornada de inventario";
            this.buscar_jornada_inventario.Click += new System.EventHandler(this.buscar_jornada_inventario_Click);
            // 
            // terminar_jornada_inventario
            // 
            this.terminar_jornada_inventario.Name = "terminar_jornada_inventario";
            this.terminar_jornada_inventario.Size = new System.Drawing.Size(256, 22);
            this.terminar_jornada_inventario.Text = "Terminar jornada de inventario";
            this.terminar_jornada_inventario.Click += new System.EventHandler(this.terminar_jornada_inventario_Click);
            // 
            // lbl_mensaje_bloqueo
            // 
            this.lbl_mensaje_bloqueo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mensaje_bloqueo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mensaje_bloqueo.ForeColor = System.Drawing.Color.Red;
            this.lbl_mensaje_bloqueo.Location = new System.Drawing.Point(31, 170);
            this.lbl_mensaje_bloqueo.Name = "lbl_mensaje_bloqueo";
            this.lbl_mensaje_bloqueo.Size = new System.Drawing.Size(934, 102);
            this.lbl_mensaje_bloqueo.TabIndex = 321;
            this.lbl_mensaje_bloqueo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_inventarios_folios
            // 
            this.dgv_inventarios_folios.AllowUserToAddRows = false;
            this.dgv_inventarios_folios.AllowUserToDeleteRows = false;
            this.dgv_inventarios_folios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_inventarios_folios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_inventarios_folios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.c_inventario_folio_id,
            this.c_nombre_terminal,
            this.c_comentarios,
            this.c_estado});
            this.dgv_inventarios_folios.Location = new System.Drawing.Point(9, 135);
            this.dgv_inventarios_folios.MultiSelect = false;
            this.dgv_inventarios_folios.Name = "dgv_inventarios_folios";
            this.dgv_inventarios_folios.ReadOnly = true;
            this.dgv_inventarios_folios.RowHeadersVisible = false;
            this.dgv_inventarios_folios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_inventarios_folios.Size = new System.Drawing.Size(969, 420);
            this.dgv_inventarios_folios.TabIndex = 319;
            this.dgv_inventarios_folios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_inventarios_folios_CellFormatting);
            this.dgv_inventarios_folios.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_inventarios_folios_DataError);
            // 
            // c_inventario_folio_id
            // 
            this.c_inventario_folio_id.DataPropertyName = "inventario_folio_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_inventario_folio_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_inventario_folio_id.FillWeight = 70F;
            this.c_inventario_folio_id.HeaderText = "Folio";
            this.c_inventario_folio_id.Name = "c_inventario_folio_id";
            this.c_inventario_folio_id.ReadOnly = true;
            // 
            // c_nombre_terminal
            // 
            this.c_nombre_terminal.DataPropertyName = "terminal";
            this.c_nombre_terminal.HeaderText = "Terminal";
            this.c_nombre_terminal.Name = "c_nombre_terminal";
            this.c_nombre_terminal.ReadOnly = true;
            // 
            // c_comentarios
            // 
            this.c_comentarios.DataPropertyName = "comentarios";
            this.c_comentarios.FillWeight = 200F;
            this.c_comentarios.HeaderText = "Comentarios";
            this.c_comentarios.Name = "c_comentarios";
            this.c_comentarios.ReadOnly = true;
            // 
            // c_estado
            // 
            this.c_estado.DataPropertyName = "estado";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_estado.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_estado.FillWeight = 70F;
            this.c_estado.HeaderText = "Estado";
            this.c_estado.Name = "c_estado";
            this.c_estado.ReadOnly = true;
            // 
            // txt_estado
            // 
            this.txt_estado.BackColor = System.Drawing.Color.Green;
            this.txt_estado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_estado.ForeColor = System.Drawing.Color.White;
            this.txt_estado.Location = new System.Drawing.Point(734, 108);
            this.txt_estado.Name = "txt_estado";
            this.txt_estado.ReadOnly = true;
            this.txt_estado.Size = new System.Drawing.Size(91, 20);
            this.txt_estado.TabIndex = 316;
            this.txt_estado.Text = "ABIERTO";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 317;
            this.label8.Text = "Comentarios:";
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_comentarios.Location = new System.Drawing.Point(88, 83);
            this.txt_comentarios.Multiline = true;
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(545, 45);
            this.txt_comentarios.TabIndex = 318;
            this.txt_comentarios.Leave += new System.EventHandler(this.txt_comentarios_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(685, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 315;
            this.label5.Text = "Estado:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 298;
            this.label1.Text = "Captura:";
            // 
            // txt_fecha_terminado
            // 
            this.txt_fecha_terminado.Enabled = false;
            this.txt_fecha_terminado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_terminado.ForeColor = System.Drawing.Color.Red;
            this.txt_fecha_terminado.Location = new System.Drawing.Point(639, 57);
            this.txt_fecha_terminado.Name = "txt_fecha_terminado";
            this.txt_fecha_terminado.Size = new System.Drawing.Size(342, 21);
            this.txt_fecha_terminado.TabIndex = 309;
            // 
            // btn_inicio
            // 
            this.btn_inicio.Location = new System.Drawing.Point(757, 83);
            this.btn_inicio.Name = "btn_inicio";
            this.btn_inicio.Size = new System.Drawing.Size(35, 23);
            this.btn_inicio.TabIndex = 311;
            this.btn_inicio.Text = "<<";
            this.btn_inicio.UseVisualStyleBackColor = true;
            this.btn_inicio.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // txt_empleado_termina
            // 
            this.txt_empleado_termina.Enabled = false;
            this.txt_empleado_termina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_termina.Location = new System.Drawing.Point(88, 56);
            this.txt_empleado_termina.Name = "txt_empleado_termina";
            this.txt_empleado_termina.Size = new System.Drawing.Size(484, 21);
            this.txt_empleado_termina.TabIndex = 313;
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(946, 82);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(35, 23);
            this.btn_fin.TabIndex = 312;
            this.btn_fin.Text = ">>";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(576, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 307;
            this.label4.Text = "Finalizado:";
            // 
            // btn_atras
            // 
            this.btn_atras.Location = new System.Drawing.Point(792, 83);
            this.btn_atras.Name = "btn_atras";
            this.btn_atras.Size = new System.Drawing.Size(35, 23);
            this.btn_atras.TabIndex = 308;
            this.btn_atras.Text = "<";
            this.btn_atras.UseVisualStyleBackColor = true;
            this.btn_atras.Click += new System.EventHandler(this.btn_atras_Click);
            // 
            // txt_empleado_captura
            // 
            this.txt_empleado_captura.Enabled = false;
            this.txt_empleado_captura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_empleado_captura.Location = new System.Drawing.Point(88, 32);
            this.txt_empleado_captura.Name = "txt_empleado_captura";
            this.txt_empleado_captura.Size = new System.Drawing.Size(484, 21);
            this.txt_empleado_captura.TabIndex = 314;
            // 
            // btn_siguiente
            // 
            this.btn_siguiente.Location = new System.Drawing.Point(912, 82);
            this.btn_siguiente.Name = "btn_siguiente";
            this.btn_siguiente.Size = new System.Drawing.Size(35, 23);
            this.btn_siguiente.TabIndex = 310;
            this.btn_siguiente.Text = ">";
            this.btn_siguiente.UseVisualStyleBackColor = true;
            this.btn_siguiente.Click += new System.EventHandler(this.btn_siguiente_Click);
            // 
            // txt_fecha_creado
            // 
            this.txt_fecha_creado.Enabled = false;
            this.txt_fecha_creado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_creado.Location = new System.Drawing.Point(639, 33);
            this.txt_fecha_creado.Name = "txt_fecha_creado";
            this.txt_fecha_creado.Size = new System.Drawing.Size(342, 21);
            this.txt_fecha_creado.TabIndex = 304;
            // 
            // txt_folio_busqueda_traspaso
            // 
            this.txt_folio_busqueda_traspaso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_folio_busqueda_traspaso.ForeColor = System.Drawing.Color.Red;
            this.txt_folio_busqueda_traspaso.Location = new System.Drawing.Point(828, 83);
            this.txt_folio_busqueda_traspaso.Name = "txt_folio_busqueda_traspaso";
            this.txt_folio_busqueda_traspaso.Size = new System.Drawing.Size(83, 22);
            this.txt_folio_busqueda_traspaso.TabIndex = 299;
            this.txt_folio_busqueda_traspaso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_folio_busqueda_traspaso.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_folio_busqueda_traspaso_KeyDown);
            this.txt_folio_busqueda_traspaso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_folio_busqueda_traspaso_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(586, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 303;
            this.label3.Text = "Iniciado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 300;
            this.label2.Text = "Termina:";
            // 
            // btn_agregar
            // 
            this.btn_agregar.Location = new System.Drawing.Point(830, 106);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(151, 23);
            this.btn_agregar.TabIndex = 322;
            this.btn_agregar.Text = "Agregar Folio de Captura";
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // btn_iniciar_detener
            // 
            this.btn_iniciar_detener.Location = new System.Drawing.Point(639, 83);
            this.btn_iniciar_detener.Name = "btn_iniciar_detener";
            this.btn_iniciar_detener.Size = new System.Drawing.Size(112, 23);
            this.btn_iniciar_detener.TabIndex = 323;
            this.btn_iniciar_detener.Text = "Iniciar captura";
            this.btn_iniciar_detener.UseVisualStyleBackColor = true;
            this.btn_iniciar_detener.Click += new System.EventHandler(this.btn_iniciar_detener_Click);
            // 
            // Creacion_inventarios_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 566);
            this.Controls.Add(this.btn_iniciar_detener);
            this.Controls.Add(this.btn_agregar);
            this.Controls.Add(this.menu_principal);
            this.Controls.Add(this.lbl_mensaje_bloqueo);
            this.Controls.Add(this.dgv_inventarios_folios);
            this.Controls.Add(this.txt_estado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_fecha_terminado);
            this.Controls.Add(this.btn_inicio);
            this.Controls.Add(this.txt_empleado_termina);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_atras);
            this.Controls.Add(this.txt_empleado_captura);
            this.Controls.Add(this.btn_siguiente);
            this.Controls.Add(this.txt_fecha_creado);
            this.Controls.Add(this.txt_folio_busqueda_traspaso);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Creacion_inventarios_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de folios de inventarios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Creacion_inventarios_principal_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Creacion_inventarios_principal_FormClosed);
            this.Shown += new System.EventHandler(this.Creacion_inventarios_principal_Shown);
            this.menu_principal.ResumeLayout(false);
            this.menu_principal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_inventarios_folios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menu_principal;
		private System.Windows.Forms.Label lbl_mensaje_bloqueo;
		private System.Windows.Forms.DataGridView dgv_inventarios_folios;
		private System.Windows.Forms.TextBox txt_estado;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txt_comentarios;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_fecha_terminado;
		private System.Windows.Forms.Button btn_inicio;
		private System.Windows.Forms.TextBox txt_empleado_termina;
		private System.Windows.Forms.Button btn_fin;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btn_atras;
		private System.Windows.Forms.TextBox txt_empleado_captura;
		private System.Windows.Forms.Button btn_siguiente;
		private System.Windows.Forms.TextBox txt_fecha_creado;
		private System.Windows.Forms.TextBox txt_folio_busqueda_traspaso;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_agregar;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem crear_jornada_inventario;
		private System.Windows.Forms.ToolStripMenuItem informacion_general_inventario;
		private System.Windows.Forms.ToolStripMenuItem buscar_jornada_inventario;
		private System.Windows.Forms.ToolStripMenuItem terminar_jornada_inventario;
		private System.Windows.Forms.Button btn_iniciar_detener;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_inventario_folio_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_nombre_terminal;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_comentarios;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_estado;
	}
}