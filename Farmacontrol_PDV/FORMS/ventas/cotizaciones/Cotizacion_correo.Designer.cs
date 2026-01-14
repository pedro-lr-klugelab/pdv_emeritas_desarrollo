namespace Farmacontrol_PDV.FORMS.ventas.cotizaciones
{
	partial class Cotizacion_correo
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbp_busqueda = new System.Windows.Forms.TabPage();
            this.dgv_clientes = new System.Windows.Forms.DataGridView();
            this.elemento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_tbp_busqueda_siguiente = new System.Windows.Forms.Button();
            this.txt_busqueda = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbp_direccion_cliente = new System.Windows.Forms.TabPage();
            this.btn_tbp_direccion_cliente_atras = new System.Windows.Forms.Button();
            this.txt_tbp_direccion_cliente_nombre_cliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_tbp_direccion_cliente_siguiente = new System.Windows.Forms.Button();
            this.dgv_direcciones = new System.Windows.Forms.DataGridView();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente_domicilio_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.tbp_correos = new System.Windows.Forms.TabPage();
            this.txt_mensaje_personalizado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_tbp_correos_atras = new System.Windows.Forms.Button();
            this.btn_enviar_correo = new System.Windows.Forms.Button();
            this.txt_correo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_tbp_correo_nombre_cliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.tabControl1.SuspendLayout();
            this.tbp_busqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).BeginInit();
            this.tbp_direccion_cliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_direcciones)).BeginInit();
            this.tbp_correos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbp_busqueda);
            this.tabControl1.Controls.Add(this.tbp_direccion_cliente);
            this.tabControl1.Controls.Add(this.tbp_correos);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(668, 379);
            this.tabControl1.TabIndex = 16;
            // 
            // tbp_busqueda
            // 
            this.tbp_busqueda.BackColor = System.Drawing.SystemColors.Control;
            this.tbp_busqueda.Controls.Add(this.dgv_clientes);
            this.tbp_busqueda.Controls.Add(this.btn_tbp_busqueda_siguiente);
            this.tbp_busqueda.Controls.Add(this.txt_busqueda);
            this.tbp_busqueda.Controls.Add(this.label7);
            this.tbp_busqueda.Location = new System.Drawing.Point(4, 22);
            this.tbp_busqueda.Name = "tbp_busqueda";
            this.tbp_busqueda.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_busqueda.Size = new System.Drawing.Size(660, 353);
            this.tbp_busqueda.TabIndex = 0;
            this.tbp_busqueda.Text = "Seleccionar cliente";
            // 
            // dgv_clientes
            // 
            this.dgv_clientes.AllowUserToAddRows = false;
            this.dgv_clientes.AllowUserToDeleteRows = false;
            this.dgv_clientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_clientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_clientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_clientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.elemento_id,
            this.tipo_cliente,
            this.nombre});
            this.dgv_clientes.Location = new System.Drawing.Point(6, 32);
            this.dgv_clientes.MultiSelect = false;
            this.dgv_clientes.Name = "dgv_clientes";
            this.dgv_clientes.ReadOnly = true;
            this.dgv_clientes.RowHeadersVisible = false;
            this.dgv_clientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_clientes.Size = new System.Drawing.Size(646, 283);
            this.dgv_clientes.TabIndex = 3;
            this.dgv_clientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientes_CellDoubleClick);
            this.dgv_clientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_clientes_KeyDown);
            // 
            // elemento_id
            // 
            this.elemento_id.DataPropertyName = "elemento_id";
            this.elemento_id.HeaderText = "elemento_id";
            this.elemento_id.Name = "elemento_id";
            this.elemento_id.ReadOnly = true;
            this.elemento_id.Visible = false;
            // 
            // tipo_cliente
            // 
            this.tipo_cliente.DataPropertyName = "tipo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tipo_cliente.DefaultCellStyle = dataGridViewCellStyle3;
            this.tipo_cliente.FillWeight = 70F;
            this.tipo_cliente.HeaderText = "Tipo";
            this.tipo_cliente.Name = "tipo_cliente";
            this.tipo_cliente.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 300F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // btn_tbp_busqueda_siguiente
            // 
            this.btn_tbp_busqueda_siguiente.Location = new System.Drawing.Point(577, 322);
            this.btn_tbp_busqueda_siguiente.Name = "btn_tbp_busqueda_siguiente";
            this.btn_tbp_busqueda_siguiente.Size = new System.Drawing.Size(75, 23);
            this.btn_tbp_busqueda_siguiente.TabIndex = 2;
            this.btn_tbp_busqueda_siguiente.Text = "Siguiente";
            this.btn_tbp_busqueda_siguiente.UseVisualStyleBackColor = true;
            this.btn_tbp_busqueda_siguiente.Click += new System.EventHandler(this.btn_tbp_busqueda_siguiente_Click);
            // 
            // txt_busqueda
            // 
            this.txt_busqueda.Location = new System.Drawing.Point(57, 6);
            this.txt_busqueda.Name = "txt_busqueda";
            this.txt_busqueda.Size = new System.Drawing.Size(399, 20);
            this.txt_busqueda.TabIndex = 1;
            this.txt_busqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_busqueda_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Buscar:";
            // 
            // tbp_direccion_cliente
            // 
            this.tbp_direccion_cliente.BackColor = System.Drawing.SystemColors.Control;
            this.tbp_direccion_cliente.Controls.Add(this.btn_tbp_direccion_cliente_atras);
            this.tbp_direccion_cliente.Controls.Add(this.txt_tbp_direccion_cliente_nombre_cliente);
            this.tbp_direccion_cliente.Controls.Add(this.label1);
            this.tbp_direccion_cliente.Controls.Add(this.btn_tbp_direccion_cliente_siguiente);
            this.tbp_direccion_cliente.Controls.Add(this.dgv_direcciones);
            this.tbp_direccion_cliente.Controls.Add(this.label2);
            this.tbp_direccion_cliente.Location = new System.Drawing.Point(4, 22);
            this.tbp_direccion_cliente.Name = "tbp_direccion_cliente";
            this.tbp_direccion_cliente.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_direccion_cliente.Size = new System.Drawing.Size(660, 353);
            this.tbp_direccion_cliente.TabIndex = 1;
            this.tbp_direccion_cliente.Text = "Direncción del cliente";
            // 
            // btn_tbp_direccion_cliente_atras
            // 
            this.btn_tbp_direccion_cliente_atras.Location = new System.Drawing.Point(496, 322);
            this.btn_tbp_direccion_cliente_atras.Name = "btn_tbp_direccion_cliente_atras";
            this.btn_tbp_direccion_cliente_atras.Size = new System.Drawing.Size(75, 23);
            this.btn_tbp_direccion_cliente_atras.TabIndex = 22;
            this.btn_tbp_direccion_cliente_atras.Text = "Atras";
            this.btn_tbp_direccion_cliente_atras.UseVisualStyleBackColor = true;
            this.btn_tbp_direccion_cliente_atras.Click += new System.EventHandler(this.btn_tbp_direccion_cliente_atras_Click);
            // 
            // txt_tbp_direccion_cliente_nombre_cliente
            // 
            this.txt_tbp_direccion_cliente_nombre_cliente.Enabled = false;
            this.txt_tbp_direccion_cliente_nombre_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tbp_direccion_cliente_nombre_cliente.Location = new System.Drawing.Point(69, 6);
            this.txt_tbp_direccion_cliente_nombre_cliente.Multiline = true;
            this.txt_tbp_direccion_cliente_nombre_cliente.Name = "txt_tbp_direccion_cliente_nombre_cliente";
            this.txt_tbp_direccion_cliente_nombre_cliente.Size = new System.Drawing.Size(583, 44);
            this.txt_tbp_direccion_cliente_nombre_cliente.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Cliente:";
            // 
            // btn_tbp_direccion_cliente_siguiente
            // 
            this.btn_tbp_direccion_cliente_siguiente.Location = new System.Drawing.Point(577, 322);
            this.btn_tbp_direccion_cliente_siguiente.Name = "btn_tbp_direccion_cliente_siguiente";
            this.btn_tbp_direccion_cliente_siguiente.Size = new System.Drawing.Size(75, 23);
            this.btn_tbp_direccion_cliente_siguiente.TabIndex = 19;
            this.btn_tbp_direccion_cliente_siguiente.Text = "Siguiente";
            this.btn_tbp_direccion_cliente_siguiente.UseVisualStyleBackColor = true;
            this.btn_tbp_direccion_cliente_siguiente.Click += new System.EventHandler(this.btn_tbp_direccion_cliente_siguiente_Click);
            // 
            // dgv_direcciones
            // 
            this.dgv_direcciones.AllowUserToAddRows = false;
            this.dgv_direcciones.AllowUserToDeleteRows = false;
            this.dgv_direcciones.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_direcciones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_direcciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_direcciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_direcciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_direcciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tipo,
            this.direccion,
            this.cliente_domicilio_id,
            this.telefono});
            this.dgv_direcciones.Location = new System.Drawing.Point(69, 56);
            this.dgv_direcciones.MultiSelect = false;
            this.dgv_direcciones.Name = "dgv_direcciones";
            this.dgv_direcciones.ReadOnly = true;
            this.dgv_direcciones.RowHeadersVisible = false;
            this.dgv_direcciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_direcciones.Size = new System.Drawing.Size(583, 260);
            this.dgv_direcciones.TabIndex = 17;
            // 
            // tipo
            // 
            this.tipo.DataPropertyName = "tipo";
            this.tipo.FillWeight = 60F;
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            // 
            // direccion
            // 
            this.direccion.DataPropertyName = "direccion";
            this.direccion.FillWeight = 300F;
            this.direccion.HeaderText = "Direccion";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            this.direccion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.direccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cliente_domicilio_id
            // 
            this.cliente_domicilio_id.DataPropertyName = "cliente_domicilio_id";
            this.cliente_domicilio_id.HeaderText = "cliente_domicilio_id";
            this.cliente_domicilio_id.Name = "cliente_domicilio_id";
            this.cliente_domicilio_id.ReadOnly = true;
            this.cliente_domicilio_id.Visible = false;
            // 
            // telefono
            // 
            this.telefono.DataPropertyName = "telefono";
            this.telefono.HeaderText = "telefono";
            this.telefono.Name = "telefono";
            this.telefono.ReadOnly = true;
            this.telefono.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Dirección:";
            // 
            // tbp_correos
            // 
            this.tbp_correos.BackColor = System.Drawing.SystemColors.Control;
            this.tbp_correos.Controls.Add(this.txt_mensaje_personalizado);
            this.tbp_correos.Controls.Add(this.label4);
            this.tbp_correos.Controls.Add(this.btn_tbp_correos_atras);
            this.tbp_correos.Controls.Add(this.btn_enviar_correo);
            this.tbp_correos.Controls.Add(this.txt_correo);
            this.tbp_correos.Controls.Add(this.label9);
            this.tbp_correos.Controls.Add(this.txt_direccion);
            this.tbp_correos.Controls.Add(this.label5);
            this.tbp_correos.Controls.Add(this.txt_tbp_correo_nombre_cliente);
            this.tbp_correos.Controls.Add(this.label3);
            this.tbp_correos.Location = new System.Drawing.Point(4, 22);
            this.tbp_correos.Name = "tbp_correos";
            this.tbp_correos.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_correos.Size = new System.Drawing.Size(660, 353);
            this.tbp_correos.TabIndex = 2;
            this.tbp_correos.Text = "Asignacion de correos";
            // 
            // txt_mensaje_personalizado
            // 
            this.txt_mensaje_personalizado.Location = new System.Drawing.Point(20, 236);
            this.txt_mensaje_personalizado.Multiline = true;
            this.txt_mensaje_personalizado.Name = "txt_mensaje_personalizado";
            this.txt_mensaje_personalizado.Size = new System.Drawing.Size(387, 69);
            this.txt_mensaje_personalizado.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Mensaje personalizado:";
            // 
            // btn_tbp_correos_atras
            // 
            this.btn_tbp_correos_atras.Location = new System.Drawing.Point(465, 322);
            this.btn_tbp_correos_atras.Name = "btn_tbp_correos_atras";
            this.btn_tbp_correos_atras.Size = new System.Drawing.Size(75, 23);
            this.btn_tbp_correos_atras.TabIndex = 5;
            this.btn_tbp_correos_atras.Text = "Atras";
            this.btn_tbp_correos_atras.UseVisualStyleBackColor = true;
            this.btn_tbp_correos_atras.Click += new System.EventHandler(this.btn_tbp_correos_atras_Click);
            // 
            // btn_enviar_correo
            // 
            this.btn_enviar_correo.Location = new System.Drawing.Point(546, 322);
            this.btn_enviar_correo.Name = "btn_enviar_correo";
            this.btn_enviar_correo.Size = new System.Drawing.Size(106, 23);
            this.btn_enviar_correo.TabIndex = 4;
            this.btn_enviar_correo.Text = "Enviar cotización";
            this.btn_enviar_correo.UseVisualStyleBackColor = true;
            this.btn_enviar_correo.Click += new System.EventHandler(this.btn_enviar_correo_Click);
            // 
            // txt_correo
            // 
            this.txt_correo.Location = new System.Drawing.Point(20, 120);
            this.txt_correo.Multiline = true;
            this.txt_correo.Name = "txt_correo";
            this.txt_correo.Size = new System.Drawing.Size(387, 97);
            this.txt_correo.TabIndex = 2;
            this.txt_correo.TextChanged += new System.EventHandler(this.txt_correo_TextChanged);
            this.txt_correo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_correo_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Correos (una por linea):";
            // 
            // txt_direccion
            // 
            this.txt_direccion.Enabled = false;
            this.txt_direccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_direccion.Location = new System.Drawing.Point(20, 74);
            this.txt_direccion.Multiline = true;
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(583, 27);
            this.txt_direccion.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Dirección:";
            // 
            // txt_tbp_correo_nombre_cliente
            // 
            this.txt_tbp_correo_nombre_cliente.Enabled = false;
            this.txt_tbp_correo_nombre_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tbp_correo_nombre_cliente.Location = new System.Drawing.Point(22, 26);
            this.txt_tbp_correo_nombre_cliente.Multiline = true;
            this.txt_tbp_correo_nombre_cliente.Name = "txt_tbp_correo_nombre_cliente";
            this.txt_tbp_correo_nombre_cliente.Size = new System.Drawing.Size(583, 29);
            this.txt_tbp_correo_nombre_cliente.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Cliente:";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(668, 379);
            this.shapeContainer1.TabIndex = 17;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Location = new System.Drawing.Point(405, -15);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(75, 23);
            // 
            // Cotizacion_correo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 379);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cotizacion_correo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cotización por correo";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Cotizacion_correo_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tbp_busqueda.ResumeLayout(false);
            this.tbp_busqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).EndInit();
            this.tbp_direccion_cliente.ResumeLayout(false);
            this.tbp_direccion_cliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_direcciones)).EndInit();
            this.tbp_correos.ResumeLayout(false);
            this.tbp_correos.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tbp_busqueda;
		private System.Windows.Forms.TabPage tbp_direccion_cliente;
		private System.Windows.Forms.DataGridView dgv_direcciones;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dgv_clientes;
		private System.Windows.Forms.Button btn_tbp_busqueda_siguiente;
		private System.Windows.Forms.TextBox txt_busqueda;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DataGridViewTextBoxColumn elemento_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo_cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.TextBox txt_tbp_direccion_cliente_nombre_cliente;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_tbp_direccion_cliente_siguiente;
		private System.Windows.Forms.TabPage tbp_correos;
		private System.Windows.Forms.TextBox txt_tbp_correo_nombre_cliente;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txt_correo;
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btn_enviar_correo;
		private System.Windows.Forms.Button btn_tbp_direccion_cliente_atras;
		private System.Windows.Forms.Button btn_tbp_correos_atras;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
		private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
		private System.Windows.Forms.DataGridViewTextBoxColumn cliente_domicilio_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn telefono;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.TextBox txt_mensaje_personalizado;
        private System.Windows.Forms.Label label4;
	}
}