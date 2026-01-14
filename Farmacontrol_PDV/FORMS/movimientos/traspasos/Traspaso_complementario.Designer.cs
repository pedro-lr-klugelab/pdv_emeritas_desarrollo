namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
	partial class Traspaso_complementario
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
            this.dgv_traspaso_complementario = new System.Windows.Forms.DataGridView();
            this.detallado_traspaso_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad_origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_problema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_solucion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_terminar_traspaso = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_traspaso_complementario)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_traspaso_complementario
            // 
            this.dgv_traspaso_complementario.AllowUserToAddRows = false;
            this.dgv_traspaso_complementario.AllowUserToDeleteRows = false;
            this.dgv_traspaso_complementario.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_traspaso_complementario.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_traspaso_complementario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_traspaso_complementario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_traspaso_complementario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_traspaso_complementario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detallado_traspaso_id,
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_cantidad_origen,
            this.c_cantidad,
            this.c_problema,
            this.c_solucion});
            this.dgv_traspaso_complementario.Enabled = false;
            this.dgv_traspaso_complementario.Location = new System.Drawing.Point(12, 12);
            this.dgv_traspaso_complementario.MultiSelect = false;
            this.dgv_traspaso_complementario.Name = "dgv_traspaso_complementario";
            this.dgv_traspaso_complementario.ReadOnly = true;
            this.dgv_traspaso_complementario.RowHeadersVisible = false;
            this.dgv_traspaso_complementario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_traspaso_complementario.Size = new System.Drawing.Size(1176, 385);
            this.dgv_traspaso_complementario.TabIndex = 4;
            this.dgv_traspaso_complementario.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_traspaso_complementario_CellFormatting);
            // 
            // detallado_traspaso_id
            // 
            this.detallado_traspaso_id.DataPropertyName = "detallado_traspaso_id";
            this.detallado_traspaso_id.HeaderText = "detallado_traspaso_id";
            this.detallado_traspaso_id.Name = "detallado_traspaso_id";
            this.detallado_traspaso_id.ReadOnly = true;
            this.detallado_traspaso_id.Visible = false;
            // 
            // c_amecop
            // 
            this.c_amecop.DataPropertyName = "amecop";
            this.c_amecop.FillWeight = 80F;
            this.c_amecop.HeaderText = "Amecop";
            this.c_amecop.Name = "c_amecop";
            this.c_amecop.ReadOnly = true;
            // 
            // c_producto
            // 
            this.c_producto.DataPropertyName = "producto";
            this.c_producto.FillWeight = 150F;
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
            // c_cantidad_origen
            // 
            this.c_cantidad_origen.DataPropertyName = "cantidad_origen";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad_origen.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_cantidad_origen.HeaderText = "Cant. Origen";
            this.c_cantidad_origen.Name = "c_cantidad_origen";
            this.c_cantidad_origen.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_problema
            // 
            this.c_problema.DataPropertyName = "problema";
            this.c_problema.FillWeight = 150F;
            this.c_problema.HeaderText = "Problema";
            this.c_problema.Name = "c_problema";
            this.c_problema.ReadOnly = true;
            // 
            // c_solucion
            // 
            this.c_solucion.DataPropertyName = "solucion";
            this.c_solucion.FillWeight = 150F;
            this.c_solucion.HeaderText = "Solucion";
            this.c_solucion.Name = "c_solucion";
            this.c_solucion.ReadOnly = true;
            this.c_solucion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(1113, 403);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 6;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_terminar_traspaso
            // 
            this.btn_terminar_traspaso.Location = new System.Drawing.Point(912, 403);
            this.btn_terminar_traspaso.Name = "btn_terminar_traspaso";
            this.btn_terminar_traspaso.Size = new System.Drawing.Size(195, 23);
            this.btn_terminar_traspaso.TabIndex = 5;
            this.btn_terminar_traspaso.Text = "Enviar productos virtuales";
            this.btn_terminar_traspaso.UseVisualStyleBackColor = true;
            this.btn_terminar_traspaso.Click += new System.EventHandler(this.btn_terminar_traspaso_Click);
            // 
            // Traspaso_complementario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 438);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_terminar_traspaso);
            this.Controls.Add(this.dgv_traspaso_complementario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Traspaso_complementario";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traspaso Complementario";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Traspaso_complementario_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_traspaso_complementario)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_traspaso_complementario;
		private System.Windows.Forms.DataGridViewTextBoxColumn detallado_traspaso_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad_origen;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_problema;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_solucion;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.Button btn_terminar_traspaso;

	}
}