namespace Farmacontrol_PDV.FORMS.movimientos.devoluciones_mayoristas
{
	partial class Entradas_mayorista
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_factura = new System.Windows.Forms.TextBox();
            this.dgv_entradas = new System.Windows.Forms.DataGridView();
            this.c_entrada_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_tipo_entrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_fecha_creado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_fecha_terminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.radio_folio = new System.Windows.Forms.RadioButton();
            this.radio_factura = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_entradas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Busqueda por:";
            // 
            // txt_factura
            // 
            this.txt_factura.Location = new System.Drawing.Point(305, 6);
            this.txt_factura.Name = "txt_factura";
            this.txt_factura.Size = new System.Drawing.Size(328, 20);
            this.txt_factura.TabIndex = 1;
            this.txt_factura.Enter += new System.EventHandler(this.txt_factura_Enter);
            this.txt_factura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_factura_KeyDown);
            this.txt_factura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_factura_KeyPress);
            // 
            // dgv_entradas
            // 
            this.dgv_entradas.AllowUserToAddRows = false;
            this.dgv_entradas.AllowUserToDeleteRows = false;
            this.dgv_entradas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_entradas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_entradas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_entradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_entradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_entradas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_entrada_id,
            this.c_tipo_entrada,
            this.c_factura,
            this.c_fecha_creado,
            this.c_fecha_terminado,
            this.c_comentarios});
            this.dgv_entradas.Location = new System.Drawing.Point(12, 32);
            this.dgv_entradas.MultiSelect = false;
            this.dgv_entradas.Name = "dgv_entradas";
            this.dgv_entradas.ReadOnly = true;
            this.dgv_entradas.RowHeadersVisible = false;
            this.dgv_entradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_entradas.Size = new System.Drawing.Size(1017, 386);
            this.dgv_entradas.TabIndex = 2;
            this.dgv_entradas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_entradas_KeyDown);
            // 
            // c_entrada_id
            // 
            this.c_entrada_id.DataPropertyName = "entrada_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_entrada_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_entrada_id.FillWeight = 80F;
            this.c_entrada_id.HeaderText = "Folio";
            this.c_entrada_id.Name = "c_entrada_id";
            this.c_entrada_id.ReadOnly = true;
            // 
            // c_tipo_entrada
            // 
            this.c_tipo_entrada.DataPropertyName = "tipo_entrada";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_tipo_entrada.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_tipo_entrada.HeaderText = "Tipo de Entrada";
            this.c_tipo_entrada.Name = "c_tipo_entrada";
            this.c_tipo_entrada.ReadOnly = true;
            // 
            // c_factura
            // 
            this.c_factura.DataPropertyName = "factura";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_factura.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_factura.FillWeight = 120F;
            this.c_factura.HeaderText = "# Factura";
            this.c_factura.Name = "c_factura";
            this.c_factura.ReadOnly = true;
            // 
            // c_fecha_creado
            // 
            this.c_fecha_creado.DataPropertyName = "fecha_creado";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_fecha_creado.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_fecha_creado.FillWeight = 180F;
            this.c_fecha_creado.HeaderText = "Fecha Creado";
            this.c_fecha_creado.Name = "c_fecha_creado";
            this.c_fecha_creado.ReadOnly = true;
            // 
            // c_fecha_terminado
            // 
            this.c_fecha_terminado.DataPropertyName = "fecha_terminado";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_fecha_terminado.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_fecha_terminado.FillWeight = 180F;
            this.c_fecha_terminado.HeaderText = "Fecha Terminado";
            this.c_fecha_terminado.Name = "c_fecha_terminado";
            this.c_fecha_terminado.ReadOnly = true;
            // 
            // c_comentarios
            // 
            this.c_comentarios.DataPropertyName = "comentarios";
            this.c_comentarios.HeaderText = "Comentarios";
            this.c_comentarios.Name = "c_comentarios";
            this.c_comentarios.ReadOnly = true;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(873, 424);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 3;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(954, 424);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 4;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // radio_folio
            // 
            this.radio_folio.AutoSize = true;
            this.radio_folio.Checked = true;
            this.radio_folio.Location = new System.Drawing.Point(94, 7);
            this.radio_folio.Name = "radio_folio";
            this.radio_folio.Size = new System.Drawing.Size(86, 17);
            this.radio_folio.TabIndex = 5;
            this.radio_folio.TabStop = true;
            this.radio_folio.Text = "Folio entrada";
            this.radio_folio.UseVisualStyleBackColor = true;
            this.radio_folio.Click += new System.EventHandler(this.radio_folio_Click);
            // 
            // radio_factura
            // 
            this.radio_factura.AutoSize = true;
            this.radio_factura.Location = new System.Drawing.Point(186, 7);
            this.radio_factura.Name = "radio_factura";
            this.radio_factura.Size = new System.Drawing.Size(113, 17);
            this.radio_factura.TabIndex = 6;
            this.radio_factura.Text = "Numero de factura";
            this.radio_factura.UseVisualStyleBackColor = true;
            this.radio_factura.Click += new System.EventHandler(this.radio_factura_Click);
            // 
            // Entradas_mayorista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 459);
            this.Controls.Add(this.radio_factura);
            this.Controls.Add(this.radio_folio);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.dgv_entradas);
            this.Controls.Add(this.txt_factura);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Entradas_mayorista";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recepciones de mayorista";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Entradas_mayorista_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_entradas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_factura;
		private System.Windows.Forms.DataGridView dgv_entradas;
		private System.Windows.Forms.Button btn_aceptar;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_entrada_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_tipo_entrada;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_factura;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_fecha_creado;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_fecha_terminado;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_comentarios;
		private System.Windows.Forms.RadioButton radio_folio;
		private System.Windows.Forms.RadioButton radio_factura;
	}
}