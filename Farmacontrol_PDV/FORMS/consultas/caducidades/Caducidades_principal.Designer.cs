namespace Farmacontrol_PDV.FORMS.consultas.caducidades
{
	partial class Caducidades_principal
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
            this.label1 = new System.Windows.Forms.Label();
            this.chb_meses = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_generar = new System.Windows.Forms.Button();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_reporte = new System.Windows.Forms.Button();
            this.btn_ticket = new System.Windows.Forms.Button();
            this.save_dialog_caducidades = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Generar productos a caducar durante los proximos";
            // 
            // chb_meses
            // 
            this.chb_meses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chb_meses.FormattingEnabled = true;
            this.chb_meses.Location = new System.Drawing.Point(263, 6);
            this.chb_meses.Name = "chb_meses";
            this.chb_meses.Size = new System.Drawing.Size(42, 21);
            this.chb_meses.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "meses.";
            // 
            // btn_generar
            // 
            this.btn_generar.Location = new System.Drawing.Point(357, 4);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(75, 23);
            this.btn_generar.TabIndex = 3;
            this.btn_generar.Text = "Generar";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
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
            this.articulo_id,
            this.amecop,
            this.producto,
            this.caducidad,
            this.existencia});
            this.dgv_articulos.Location = new System.Drawing.Point(12, 33);
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(877, 513);
            this.dgv_articulos.TabIndex = 4;
            this.dgv_articulos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_articulos_CellFormatting);
            this.dgv_articulos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_articulos_CellPainting);
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
            this.amecop.FillWeight = 60F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 200F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 50F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // existencia
            // 
            this.existencia.DataPropertyName = "existencia";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.existencia.FillWeight = 40F;
            this.existencia.HeaderText = "Existencia";
            this.existencia.Name = "existencia";
            this.existencia.ReadOnly = true;
            // 
            // btn_reporte
            // 
            this.btn_reporte.Location = new System.Drawing.Point(725, 552);
            this.btn_reporte.Name = "btn_reporte";
            this.btn_reporte.Size = new System.Drawing.Size(164, 23);
            this.btn_reporte.TabIndex = 5;
            this.btn_reporte.Text = "Generar reporte completo";
            this.btn_reporte.UseVisualStyleBackColor = true;
            this.btn_reporte.Click += new System.EventHandler(this.btn_reporte_Click);
            // 
            // btn_ticket
            // 
            this.btn_ticket.Location = new System.Drawing.Point(567, 552);
            this.btn_ticket.Name = "btn_ticket";
            this.btn_ticket.Size = new System.Drawing.Size(152, 23);
            this.btn_ticket.TabIndex = 6;
            this.btn_ticket.Text = "Imprimir tira de chequeo";
            this.btn_ticket.UseVisualStyleBackColor = true;
            this.btn_ticket.Click += new System.EventHandler(this.btn_ticket_Click);
            // 
            // save_dialog_caducidades
            // 
            this.save_dialog_caducidades.FileName = "caducidades";
            // 
            // Caducidades_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 587);
            this.Controls.Add(this.btn_ticket);
            this.Controls.Add(this.btn_reporte);
            this.Controls.Add(this.dgv_articulos);
            this.Controls.Add(this.btn_generar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chb_meses);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Caducidades_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caducidades";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox chb_meses;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_generar;
		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.Button btn_reporte;
		private System.Windows.Forms.Button btn_ticket;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia;
		private System.Windows.Forms.SaveFileDialog save_dialog_caducidades;
	}
}