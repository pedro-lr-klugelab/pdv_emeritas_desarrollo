namespace Farmacontrol_PDV.FORMS.catalogos.kardex_articulos
{
	partial class Kardex_articulos_principal
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.dgv_kardex = new System.Windows.Forms.DataGridView();
            this.c_fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_existencia_anterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_existencia_posterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_importado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_inicio = new System.Windows.Forms.Button();
            this.btn_atras = new System.Windows.Forms.Button();
            this.btn_siguiente = new System.Windows.Forms.Button();
            this.btn_fin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_total_paginas = new System.Windows.Forms.Label();
            this.txt_pagina_actual = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_kardex)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amecop:";
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(12, 25);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(165, 20);
            this.txt_amecop.TabIndex = 1;
            //this.txt_amecop.TextChanged += new System.EventHandler(this.txt_amecop_TextChanged);
            this.txt_amecop.Enter += new System.EventHandler(this.txt_amecop_Enter);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // txt_nombre
            // 
            this.txt_nombre.Enabled = false;
            this.txt_nombre.Location = new System.Drawing.Point(183, 25);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(457, 20);
            this.txt_nombre.TabIndex = 2;
            //this.txt_nombre.TextChanged += new System.EventHandler(this.txt_nombre_TextChanged);
            // 
            // dgv_kardex
            // 
            this.dgv_kardex.AllowUserToAddRows = false;
            this.dgv_kardex.AllowUserToDeleteRows = false;
            this.dgv_kardex.AllowUserToResizeRows = false;
            this.dgv_kardex.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_kardex.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_kardex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_kardex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_fecha,
            this.terminal,
            this.c_tipo,
            this.c_folio,
            this.c_caducidad,
            this.c_lote,
            this.c_existencia_anterior,
            this.c_cantidad,
            this.c_existencia_posterior,
            this.es_importado});
            this.dgv_kardex.Location = new System.Drawing.Point(12, 51);
            this.dgv_kardex.MultiSelect = false;
            this.dgv_kardex.Name = "dgv_kardex";
            this.dgv_kardex.ReadOnly = true;
            this.dgv_kardex.RowHeadersVisible = false;
            this.dgv_kardex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_kardex.Size = new System.Drawing.Size(1135, 478);
            this.dgv_kardex.TabIndex = 3;
            this.dgv_kardex.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_kardex_CellFormatting);
            // 
            // c_fecha
            // 
            this.c_fecha.DataPropertyName = "fecha";
            dataGridViewCellStyle2.Format = "F";
            dataGridViewCellStyle2.NullValue = null;
            this.c_fecha.DefaultCellStyle = dataGridViewCellStyle2;
            this.c_fecha.FillWeight = 200F;
            this.c_fecha.HeaderText = "Fecha";
            this.c_fecha.Name = "c_fecha";
            this.c_fecha.ReadOnly = true;
            // 
            // terminal
            // 
            this.terminal.DataPropertyName = "terminal";
            this.terminal.HeaderText = "Terminal";
            this.terminal.Name = "terminal";
            this.terminal.ReadOnly = true;
            // 
            // c_tipo
            // 
            this.c_tipo.DataPropertyName = "tipo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_tipo.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_tipo.HeaderText = "Tipo";
            this.c_tipo.Name = "c_tipo";
            this.c_tipo.ReadOnly = true;
            // 
            // c_folio
            // 
            this.c_folio.DataPropertyName = "folio";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_folio.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_folio.FillWeight = 80F;
            this.c_folio.HeaderText = "Folio";
            this.c_folio.Name = "c_folio";
            this.c_folio.ReadOnly = true;
            // 
            // c_caducidad
            // 
            this.c_caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_caducidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_caducidad.FillWeight = 80F;
            this.c_caducidad.HeaderText = "Caducidad";
            this.c_caducidad.Name = "c_caducidad";
            this.c_caducidad.ReadOnly = true;
            // 
            // c_lote
            // 
            this.c_lote.DataPropertyName = "lote";
            this.c_lote.FillWeight = 120F;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            this.c_lote.ReadOnly = true;
            // 
            // c_existencia_anterior
            // 
            this.c_existencia_anterior.DataPropertyName = "existencia_anterior";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_existencia_anterior.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_existencia_anterior.FillWeight = 80F;
            this.c_existencia_anterior.HeaderText = "Ex. Ant.";
            this.c_existencia_anterior.Name = "c_existencia_anterior";
            this.c_existencia_anterior.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_cantidad.FillWeight = 80F;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_existencia_posterior
            // 
            this.c_existencia_posterior.DataPropertyName = "existencia_posterior";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_existencia_posterior.DefaultCellStyle = dataGridViewCellStyle8;
            this.c_existencia_posterior.FillWeight = 80F;
            this.c_existencia_posterior.HeaderText = "Ex. Pos.";
            this.c_existencia_posterior.Name = "c_existencia_posterior";
            this.c_existencia_posterior.ReadOnly = true;
            // 
            // es_importado
            // 
            this.es_importado.DataPropertyName = "es_importado";
            this.es_importado.HeaderText = "es_importado";
            this.es_importado.Name = "es_importado";
            this.es_importado.ReadOnly = true;
            this.es_importado.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Producto:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(851, 535);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 27);
            this.label3.TabIndex = 14;
            this.label3.Text = "Pagina";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_inicio
            // 
            this.btn_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_inicio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_inicio.Location = new System.Drawing.Point(786, 535);
            this.btn_inicio.Name = "btn_inicio";
            this.btn_inicio.Size = new System.Drawing.Size(30, 26);
            this.btn_inicio.TabIndex = 13;
            this.btn_inicio.Text = "<<";
            this.btn_inicio.UseVisualStyleBackColor = true;
            this.btn_inicio.Click += new System.EventHandler(this.btn_inicio_Click);
            // 
            // btn_atras
            // 
            this.btn_atras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_atras.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_atras.Location = new System.Drawing.Point(815, 535);
            this.btn_atras.Name = "btn_atras";
            this.btn_atras.Size = new System.Drawing.Size(30, 26);
            this.btn_atras.TabIndex = 12;
            this.btn_atras.Text = "<";
            this.btn_atras.UseVisualStyleBackColor = true;
            this.btn_atras.Click += new System.EventHandler(this.btn_atras_Click);
            // 
            // btn_siguiente
            // 
            this.btn_siguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_siguiente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_siguiente.Location = new System.Drawing.Point(1088, 535);
            this.btn_siguiente.Name = "btn_siguiente";
            this.btn_siguiente.Size = new System.Drawing.Size(30, 26);
            this.btn_siguiente.TabIndex = 11;
            this.btn_siguiente.Text = ">";
            this.btn_siguiente.UseVisualStyleBackColor = true;
            this.btn_siguiente.Click += new System.EventHandler(this.btn_siguiente_Click);
            // 
            // btn_fin
            // 
            this.btn_fin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_fin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_fin.Location = new System.Drawing.Point(1117, 535);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(30, 26);
            this.btn_fin.TabIndex = 10;
            this.btn_fin.Text = ">>";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(1015, 535);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 27);
            this.label4.TabIndex = 15;
            this.label4.Text = "de";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_total_paginas
            // 
            this.lbl_total_paginas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_paginas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_total_paginas.Location = new System.Drawing.Point(1038, 535);
            this.lbl_total_paginas.Name = "lbl_total_paginas";
            this.lbl_total_paginas.Size = new System.Drawing.Size(44, 27);
            this.lbl_total_paginas.TabIndex = 16;
            this.lbl_total_paginas.Text = "1";
            this.lbl_total_paginas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_pagina_actual
            // 
            this.txt_pagina_actual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pagina_actual.Location = new System.Drawing.Point(909, 539);
            this.txt_pagina_actual.Name = "txt_pagina_actual";
            this.txt_pagina_actual.Size = new System.Drawing.Size(100, 20);
            this.txt_pagina_actual.TabIndex = 17;
            this.txt_pagina_actual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_pagina_actual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pagina_actual_KeyDown);
            this.txt_pagina_actual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pagina_actual_KeyPress);
            // 
            // Kardex_articulos_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 573);
            this.Controls.Add(this.txt_pagina_actual);
            this.Controls.Add(this.lbl_total_paginas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_inicio);
            this.Controls.Add(this.btn_atras);
            this.Controls.Add(this.btn_siguiente);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv_kardex);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Kardex_articulos_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kardex de Articulos";
            this.Shown += new System.EventHandler(this.Kardex_articulos_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_kardex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.DataGridView dgv_kardex;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btn_inicio;
		private System.Windows.Forms.Button btn_atras;
		private System.Windows.Forms.Button btn_siguiente;
		private System.Windows.Forms.Button btn_fin;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lbl_total_paginas;
        private System.Windows.Forms.TextBox txt_pagina_actual;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_existencia_anterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_existencia_posterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_importado;
	}
}