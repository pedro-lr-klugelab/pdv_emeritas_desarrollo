namespace Farmacontrol_PDV.FORMS.consultas.cambios_precio
{
	partial class Cambios_precio_informacion
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
            this.chb_sin_existencia = new System.Windows.Forms.CheckBox();
            this.chb_sin_cambio = new System.Windows.Forms.CheckBox();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico_anterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico_nuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.incr_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_costo_anterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_costo_nuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.incr_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_pdf = new System.Windows.Forms.Button();
            this.btn_imprimir_ticket = new System.Windows.Forms.Button();
            this.save_dialog_cambios_precio = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // chb_sin_existencia
            // 
            this.chb_sin_existencia.AutoSize = true;
            this.chb_sin_existencia.Location = new System.Drawing.Point(12, 12);
            this.chb_sin_existencia.Name = "chb_sin_existencia";
            this.chb_sin_existencia.Size = new System.Drawing.Size(176, 17);
            this.chb_sin_existencia.TabIndex = 0;
            this.chb_sin_existencia.Text = "Ocultar productos sin existencia";
            this.chb_sin_existencia.UseVisualStyleBackColor = true;
            this.chb_sin_existencia.CheckedChanged += new System.EventHandler(this.chb_sin_existencia_CheckedChanged);
            // 
            // chb_sin_cambio
            // 
            this.chb_sin_cambio.AutoSize = true;
            this.chb_sin_cambio.Location = new System.Drawing.Point(194, 12);
            this.chb_sin_cambio.Name = "chb_sin_cambio";
            this.chb_sin_cambio.Size = new System.Drawing.Size(285, 17);
            this.chb_sin_cambio.TabIndex = 1;
            this.chb_sin_cambio.Text = "Ocultar productos sin cambio de precio costo o público";
            this.chb_sin_cambio.UseVisualStyleBackColor = true;
            this.chb_sin_cambio.CheckedChanged += new System.EventHandler(this.chb_sin_cambio_CheckedChanged);
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_articulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.amecop,
            this.producto,
            this.precio_publico_anterior,
            this.precio_publico_nuevo,
            this.incr_publico,
            this.precio_costo_anterior,
            this.precio_costo_nuevo,
            this.incr_costo,
            this.existencia});
            this.dgv_articulos.Location = new System.Drawing.Point(12, 35);
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(1053, 580);
            this.dgv_articulos.TabIndex = 2;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 80F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 150F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // precio_publico_anterior
            // 
            this.precio_publico_anterior.DataPropertyName = "precio_publico_anterior";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.precio_publico_anterior.DefaultCellStyle = dataGridViewCellStyle3;
            this.precio_publico_anterior.FillWeight = 70F;
            this.precio_publico_anterior.HeaderText = "P. Publico. Ant.";
            this.precio_publico_anterior.Name = "precio_publico_anterior";
            this.precio_publico_anterior.ReadOnly = true;
            // 
            // precio_publico_nuevo
            // 
            this.precio_publico_nuevo.DataPropertyName = "precio_publico_nuevo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            this.precio_publico_nuevo.DefaultCellStyle = dataGridViewCellStyle4;
            this.precio_publico_nuevo.FillWeight = 70F;
            this.precio_publico_nuevo.HeaderText = "P. Publico Nvo.";
            this.precio_publico_nuevo.Name = "precio_publico_nuevo";
            this.precio_publico_nuevo.ReadOnly = true;
            // 
            // incr_publico
            // 
            this.incr_publico.DataPropertyName = "incr_publico";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "P2";
            this.incr_publico.DefaultCellStyle = dataGridViewCellStyle5;
            this.incr_publico.FillWeight = 40F;
            this.incr_publico.HeaderText = "Incr. P.";
            this.incr_publico.Name = "incr_publico";
            this.incr_publico.ReadOnly = true;
            // 
            // precio_costo_anterior
            // 
            this.precio_costo_anterior.DataPropertyName = "precio_costo_anterior";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            this.precio_costo_anterior.DefaultCellStyle = dataGridViewCellStyle6;
            this.precio_costo_anterior.FillWeight = 70F;
            this.precio_costo_anterior.HeaderText = "P. Costo Ant.";
            this.precio_costo_anterior.Name = "precio_costo_anterior";
            this.precio_costo_anterior.ReadOnly = true;
            // 
            // precio_costo_nuevo
            // 
            this.precio_costo_nuevo.DataPropertyName = "precio_costo_nuevo";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C2";
            this.precio_costo_nuevo.DefaultCellStyle = dataGridViewCellStyle7;
            this.precio_costo_nuevo.FillWeight = 70F;
            this.precio_costo_nuevo.HeaderText = "P. Costo Nvo.";
            this.precio_costo_nuevo.Name = "precio_costo_nuevo";
            this.precio_costo_nuevo.ReadOnly = true;
            // 
            // incr_costo
            // 
            this.incr_costo.DataPropertyName = "incr_costo";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "P2";
            this.incr_costo.DefaultCellStyle = dataGridViewCellStyle8;
            this.incr_costo.FillWeight = 40F;
            this.incr_costo.HeaderText = "Incr. C.";
            this.incr_costo.Name = "incr_costo";
            this.incr_costo.ReadOnly = true;
            // 
            // existencia
            // 
            this.existencia.DataPropertyName = "existencia";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia.DefaultCellStyle = dataGridViewCellStyle9;
            this.existencia.FillWeight = 60F;
            this.existencia.HeaderText = "Existencia";
            this.existencia.Name = "existencia";
            this.existencia.ReadOnly = true;
            // 
            // btn_pdf
            // 
            this.btn_pdf.Location = new System.Drawing.Point(906, 621);
            this.btn_pdf.Name = "btn_pdf";
            this.btn_pdf.Size = new System.Drawing.Size(159, 23);
            this.btn_pdf.TabIndex = 3;
            this.btn_pdf.Text = "Imprimir reporte completo";
            this.btn_pdf.UseVisualStyleBackColor = true;
            this.btn_pdf.Click += new System.EventHandler(this.btn_pdf_Click);
            // 
            // btn_imprimir_ticket
            // 
            this.btn_imprimir_ticket.Location = new System.Drawing.Point(726, 621);
            this.btn_imprimir_ticket.Name = "btn_imprimir_ticket";
            this.btn_imprimir_ticket.Size = new System.Drawing.Size(174, 23);
            this.btn_imprimir_ticket.TabIndex = 4;
            this.btn_imprimir_ticket.Text = "Imprimir tira de reetiquetado";
            this.btn_imprimir_ticket.UseVisualStyleBackColor = true;
            this.btn_imprimir_ticket.Click += new System.EventHandler(this.btn_imprimir_ticket_Click);
            // 
            // save_dialog_cambios_precio
            // 
            this.save_dialog_cambios_precio.FileName = "cambio_precios";
            // 
            // Cambios_precio_informacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 656);
            this.Controls.Add(this.btn_imprimir_ticket);
            this.Controls.Add(this.btn_pdf);
            this.Controls.Add(this.dgv_articulos);
            this.Controls.Add(this.chb_sin_cambio);
            this.Controls.Add(this.chb_sin_existencia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cambios_precio_informacion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Cambios_precio_informacion_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chb_sin_existencia;
		private System.Windows.Forms.CheckBox chb_sin_cambio;
		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.Button btn_pdf;
		private System.Windows.Forms.Button btn_imprimir_ticket;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico_anterior;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico_nuevo;
		private System.Windows.Forms.DataGridViewTextBoxColumn incr_publico;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_costo_anterior;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_costo_nuevo;
		private System.Windows.Forms.DataGridViewTextBoxColumn incr_costo;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia;
		private System.Windows.Forms.SaveFileDialog save_dialog_cambios_precio;
	}
}