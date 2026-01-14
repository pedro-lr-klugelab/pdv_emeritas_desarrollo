namespace Farmacontrol_PDV.FORMS.ventas.elaborar_formula
{
	partial class Formula_sucursal
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
            this.txt_instrucciones = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_total = new System.Windows.Forms.Label();
            this.lbl_iva = new System.Windows.Forms.Label();
            this.lbl_ieps = new System.Windows.Forms.Label();
            this.lbl_subtotal = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.txt_iva = new System.Windows.Forms.TextBox();
            this.txt_ieps = new System.Windows.Forms.TextBox();
            this.txt_subtotal = new System.Windows.Forms.TextBox();
            this.txt_doctor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_cliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_terminar = new System.Windows.Forms.Button();
            this.btn_imprimir = new System.Windows.Forms.Button();
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
            this.label3 = new System.Windows.Forms.Label();
            this.txt_fecha_creado = new System.Windows.Forms.TextBox();
            this.txt_comentarios_elaboracion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detallado_formulas)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_instrucciones
            // 
            this.txt_instrucciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_instrucciones.Location = new System.Drawing.Point(10, 384);
            this.txt_instrucciones.Multiline = true;
            this.txt_instrucciones.Name = "txt_instrucciones";
            this.txt_instrucciones.ReadOnly = true;
            this.txt_instrucciones.Size = new System.Drawing.Size(586, 72);
            this.txt_instrucciones.TabIndex = 148;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 13);
            this.label6.TabIndex = 147;
            this.label6.Text = "Instrucciones, indicaciones, notas o comentarios:";
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Location = new System.Drawing.Point(841, 438);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(34, 13);
            this.lbl_total.TabIndex = 145;
            this.lbl_total.Text = "Total:";
            // 
            // lbl_iva
            // 
            this.lbl_iva.AutoSize = true;
            this.lbl_iva.Location = new System.Drawing.Point(848, 381);
            this.lbl_iva.Name = "lbl_iva";
            this.lbl_iva.Size = new System.Drawing.Size(27, 13);
            this.lbl_iva.TabIndex = 144;
            this.lbl_iva.Text = "IVA:";
            // 
            // lbl_ieps
            // 
            this.lbl_ieps.AutoSize = true;
            this.lbl_ieps.Location = new System.Drawing.Point(841, 409);
            this.lbl_ieps.Name = "lbl_ieps";
            this.lbl_ieps.Size = new System.Drawing.Size(34, 13);
            this.lbl_ieps.TabIndex = 143;
            this.lbl_ieps.Text = "IEPS:";
            // 
            // lbl_subtotal
            // 
            this.lbl_subtotal.AutoSize = true;
            this.lbl_subtotal.Location = new System.Drawing.Point(826, 355);
            this.lbl_subtotal.Name = "lbl_subtotal";
            this.lbl_subtotal.Size = new System.Drawing.Size(49, 13);
            this.lbl_subtotal.TabIndex = 142;
            this.lbl_subtotal.Text = "Subtotal:";
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.ForeColor = System.Drawing.Color.Red;
            this.txt_total.Location = new System.Drawing.Point(881, 434);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(100, 22);
            this.txt_total.TabIndex = 141;
            this.txt_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_iva
            // 
            this.txt_iva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_iva.ForeColor = System.Drawing.Color.Red;
            this.txt_iva.Location = new System.Drawing.Point(881, 378);
            this.txt_iva.Name = "txt_iva";
            this.txt_iva.ReadOnly = true;
            this.txt_iva.Size = new System.Drawing.Size(100, 22);
            this.txt_iva.TabIndex = 140;
            this.txt_iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ieps
            // 
            this.txt_ieps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ieps.ForeColor = System.Drawing.Color.Red;
            this.txt_ieps.Location = new System.Drawing.Point(881, 406);
            this.txt_ieps.Name = "txt_ieps";
            this.txt_ieps.ReadOnly = true;
            this.txt_ieps.Size = new System.Drawing.Size(100, 22);
            this.txt_ieps.TabIndex = 139;
            this.txt_ieps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_subtotal
            // 
            this.txt_subtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_subtotal.ForeColor = System.Drawing.Color.Red;
            this.txt_subtotal.Location = new System.Drawing.Point(881, 352);
            this.txt_subtotal.Name = "txt_subtotal";
            this.txt_subtotal.ReadOnly = true;
            this.txt_subtotal.Size = new System.Drawing.Size(100, 22);
            this.txt_subtotal.TabIndex = 138;
            this.txt_subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_doctor
            // 
            this.txt_doctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_doctor.Location = new System.Drawing.Point(114, 38);
            this.txt_doctor.Name = "txt_doctor";
            this.txt_doctor.ReadOnly = true;
            this.txt_doctor.Size = new System.Drawing.Size(418, 20);
            this.txt_doctor.TabIndex = 133;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 132;
            this.label2.Text = "Nombre del doctor:";
            // 
            // txt_cliente
            // 
            this.txt_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cliente.Location = new System.Drawing.Point(114, 12);
            this.txt_cliente.Name = "txt_cliente";
            this.txt_cliente.ReadOnly = true;
            this.txt_cliente.Size = new System.Drawing.Size(418, 20);
            this.txt_cliente.TabIndex = 131;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 130;
            this.label1.Text = "Nombre del cliente:";
            // 
            // btn_terminar
            // 
            this.btn_terminar.Enabled = false;
            this.btn_terminar.Location = new System.Drawing.Point(906, 514);
            this.btn_terminar.Name = "btn_terminar";
            this.btn_terminar.Size = new System.Drawing.Size(75, 23);
            this.btn_terminar.TabIndex = 129;
            this.btn_terminar.Text = "Terminar";
            this.btn_terminar.UseVisualStyleBackColor = true;
            this.btn_terminar.Click += new System.EventHandler(this.btn_terminar_Click);
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(763, 514);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(137, 23);
            this.btn_imprimir.TabIndex = 128;
            this.btn_imprimir.Text = "Imprimir lista de surtido";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // dgv_detallado_formulas
            // 
            this.dgv_detallado_formulas.AllowUserToAddRows = false;
            this.dgv_detallado_formulas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_detallado_formulas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_detallado_formulas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_detallado_formulas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            this.dgv_detallado_formulas.Location = new System.Drawing.Point(12, 64);
            this.dgv_detallado_formulas.MultiSelect = false;
            this.dgv_detallado_formulas.Name = "dgv_detallado_formulas";
            this.dgv_detallado_formulas.ReadOnly = true;
            this.dgv_detallado_formulas.RowHeadersVisible = false;
            this.dgv_detallado_formulas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_detallado_formulas.Size = new System.Drawing.Size(969, 279);
            this.dgv_detallado_formulas.TabIndex = 127;
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
            this.comentarios.HeaderText = "Comentarios";
            this.comentarios.Name = "comentarios";
            this.comentarios.ReadOnly = true;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle3;
            this.precio_publico.FillWeight = 70F;
            this.precio_publico.HeaderText = "Precio Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "#.##";
            dataGridViewCellStyle4.NullValue = null;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.cantidad.FillWeight = 60F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.DataPropertyName = "importe";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            this.importe.DefaultCellStyle = dataGridViewCellStyle5;
            this.importe.FillWeight = 60F;
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            // 
            // subtotal
            // 
            this.subtotal.DataPropertyName = "subtotal";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.subtotal.FillWeight = 70F;
            this.subtotal.HeaderText = "Total";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            this.total.DefaultCellStyle = dataGridViewCellStyle7;
            this.total.FillWeight = 70F;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(552, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 149;
            this.label3.Text = "Creado:";
            // 
            // txt_fecha_creado
            // 
            this.txt_fecha_creado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fecha_creado.Location = new System.Drawing.Point(602, 12);
            this.txt_fecha_creado.Name = "txt_fecha_creado";
            this.txt_fecha_creado.ReadOnly = true;
            this.txt_fecha_creado.Size = new System.Drawing.Size(254, 20);
            this.txt_fecha_creado.TabIndex = 150;
            // 
            // txt_comentarios_elaboracion
            // 
            this.txt_comentarios_elaboracion.Location = new System.Drawing.Point(10, 462);
            this.txt_comentarios_elaboracion.Multiline = true;
            this.txt_comentarios_elaboracion.Name = "txt_comentarios_elaboracion";
            this.txt_comentarios_elaboracion.Size = new System.Drawing.Size(586, 72);
            this.txt_comentarios_elaboracion.TabIndex = 151;
            this.txt_comentarios_elaboracion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comentarios_elaboracion_KeyPress);
            // 
            // Formula_sucursal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 549);
            this.Controls.Add(this.txt_comentarios_elaboracion);
            this.Controls.Add(this.txt_fecha_creado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_instrucciones);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.lbl_iva);
            this.Controls.Add(this.lbl_ieps);
            this.Controls.Add(this.lbl_subtotal);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.txt_iva);
            this.Controls.Add(this.txt_ieps);
            this.Controls.Add(this.txt_subtotal);
            this.Controls.Add(this.txt_doctor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_cliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_terminar);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.dgv_detallado_formulas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Formula_sucursal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elaborar Formula";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formula_sucursal_FormClosing);
            this.Shown += new System.EventHandler(this.Formula_sucursal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detallado_formulas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt_instrucciones;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lbl_total;
		private System.Windows.Forms.Label lbl_iva;
		private System.Windows.Forms.Label lbl_ieps;
		private System.Windows.Forms.Label lbl_subtotal;
		private System.Windows.Forms.TextBox txt_total;
		private System.Windows.Forms.TextBox txt_iva;
		private System.Windows.Forms.TextBox txt_ieps;
		private System.Windows.Forms.TextBox txt_subtotal;
		private System.Windows.Forms.TextBox txt_doctor;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_cliente;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_terminar;
		private System.Windows.Forms.Button btn_imprimir;
		private System.Windows.Forms.DataGridView dgv_detallado_formulas;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_fecha_creado;
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
		private System.Windows.Forms.TextBox txt_comentarios_elaboracion;
	}
}