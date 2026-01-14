namespace Farmacontrol_PDV.FORMS.movimientos.apartado_mercancia
{
	partial class Apartado_mercancia_principal
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
            this.dgv_apartados = new System.Windows.Forms.DataGridView();
            this.apartado_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_destino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_destino_sin_formato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_fecha_apartado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_fecha_expiracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_apartados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_apartados
            // 
            this.dgv_apartados.AllowUserToAddRows = false;
            this.dgv_apartados.AllowUserToDeleteRows = false;
            this.dgv_apartados.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_apartados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_apartados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_apartados.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_apartados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_apartados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_apartados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.apartado_id,
            this.c_amecop,
            this.c_producto,
            this.c_destino,
            this.c_destino_sin_formato,
            this.c_caducidad,
            this.c_lote,
            this.c_cantidad,
            this.c_fecha_apartado,
            this.c_fecha_expiracion});
            this.dgv_apartados.Location = new System.Drawing.Point(12, 50);
            this.dgv_apartados.MultiSelect = false;
            this.dgv_apartados.Name = "dgv_apartados";
            this.dgv_apartados.ReadOnly = true;
            this.dgv_apartados.RowHeadersVisible = false;
            this.dgv_apartados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_apartados.Size = new System.Drawing.Size(1208, 507);
            this.dgv_apartados.TabIndex = 0;
            this.dgv_apartados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_apartados_KeyDown);
            // 
            // apartado_id
            // 
            this.apartado_id.DataPropertyName = "apartado_id";
            this.apartado_id.HeaderText = "apartado_id";
            this.apartado_id.Name = "apartado_id";
            this.apartado_id.ReadOnly = true;
            this.apartado_id.Visible = false;
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
            this.c_producto.FillWeight = 200F;
            this.c_producto.HeaderText = "Producto";
            this.c_producto.Name = "c_producto";
            this.c_producto.ReadOnly = true;
            // 
            // c_destino
            // 
            this.c_destino.DataPropertyName = "destino";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_destino.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_destino.FillWeight = 200F;
            this.c_destino.HeaderText = "Destino";
            this.c_destino.Name = "c_destino";
            this.c_destino.ReadOnly = true;
            // 
            // c_destino_sin_formato
            // 
            this.c_destino_sin_formato.DataPropertyName = "destino_sin_formato";
            this.c_destino_sin_formato.HeaderText = "c_destino_sin_formato";
            this.c_destino_sin_formato.Name = "c_destino_sin_formato";
            this.c_destino_sin_formato.ReadOnly = true;
            this.c_destino_sin_formato.Visible = false;
            // 
            // c_caducidad
            // 
            this.c_caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_caducidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_caducidad.HeaderText = "Caducidad";
            this.c_caducidad.Name = "c_caducidad";
            this.c_caducidad.ReadOnly = true;
            // 
            // c_lote
            // 
            this.c_lote.DataPropertyName = "lote";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_lote.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_lote.FillWeight = 150F;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            this.c_lote.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_fecha_apartado
            // 
            this.c_fecha_apartado.DataPropertyName = "fecha_apartado";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "dd/MMM/yyyy h:mm:ss tt";
            this.c_fecha_apartado.DefaultCellStyle = dataGridViewCellStyle7;
            this.c_fecha_apartado.FillWeight = 150F;
            this.c_fecha_apartado.HeaderText = "Fecha Apartado";
            this.c_fecha_apartado.Name = "c_fecha_apartado";
            this.c_fecha_apartado.ReadOnly = true;
            // 
            // c_fecha_expiracion
            // 
            this.c_fecha_expiracion.DataPropertyName = "fecha_expiracion";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "dd/MMM/yyyy h:mm:ss tt";
            this.c_fecha_expiracion.DefaultCellStyle = dataGridViewCellStyle8;
            this.c_fecha_expiracion.FillWeight = 150F;
            this.c_fecha_expiracion.HeaderText = "Fecha Expiracion";
            this.c_fecha_expiracion.Name = "c_fecha_expiracion";
            this.c_fecha_expiracion.ReadOnly = true;
            // 
            // cbb_lote
            // 
            this.cbb_lote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_lote.Enabled = false;
            this.cbb_lote.FormattingEnabled = true;
            this.cbb_lote.Location = new System.Drawing.Point(887, 23);
            this.cbb_lote.Name = "cbb_lote";
            this.cbb_lote.Size = new System.Drawing.Size(156, 21);
            this.cbb_lote.TabIndex = 95;
            this.cbb_lote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_lote_KeyDown);
            // 
            // cbb_caducidad
            // 
            this.cbb_caducidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_caducidad.Enabled = false;
            this.cbb_caducidad.FormattingEnabled = true;
            this.cbb_caducidad.Location = new System.Drawing.Point(760, 23);
            this.cbb_caducidad.Name = "cbb_caducidad";
            this.cbb_caducidad.Size = new System.Drawing.Size(121, 21);
            this.cbb_caducidad.TabIndex = 94;
            this.cbb_caducidad.SelectedIndexChanged += new System.EventHandler(this.cbb_caducidad_SelectedIndexChanged);
            this.cbb_caducidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_caducidad_KeyDown);
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Location = new System.Drawing.Point(156, 24);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.Size = new System.Drawing.Size(598, 20);
            this.txt_producto.TabIndex = 93;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(10, 24);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(140, 20);
            this.txt_amecop.TabIndex = 86;
            this.txt_amecop.Enter += new System.EventHandler(this.txt_amecop_Enter);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // lbl_cantidad
            // 
            this.lbl_cantidad.AutoSize = true;
            this.lbl_cantidad.Location = new System.Drawing.Point(1046, 8);
            this.lbl_cantidad.Name = "lbl_cantidad";
            this.lbl_cantidad.Size = new System.Drawing.Size(49, 13);
            this.lbl_cantidad.TabIndex = 92;
            this.lbl_cantidad.Text = "Cantidad";
            // 
            // lbl_lote
            // 
            this.lbl_lote.AutoSize = true;
            this.lbl_lote.Location = new System.Drawing.Point(884, 6);
            this.lbl_lote.Name = "lbl_lote";
            this.lbl_lote.Size = new System.Drawing.Size(28, 13);
            this.lbl_lote.TabIndex = 90;
            this.lbl_lote.Text = "Lote";
            // 
            // lbl_caducidad
            // 
            this.lbl_caducidad.AutoSize = true;
            this.lbl_caducidad.Location = new System.Drawing.Point(757, 6);
            this.lbl_caducidad.Name = "lbl_caducidad";
            this.lbl_caducidad.Size = new System.Drawing.Size(58, 13);
            this.lbl_caducidad.TabIndex = 89;
            this.lbl_caducidad.Text = "Caducidad";
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(153, 6);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(50, 13);
            this.lbl_producto.TabIndex = 88;
            this.lbl_producto.Text = "Producto";
            // 
            // lbl_amecop
            // 
            this.lbl_amecop.AutoSize = true;
            this.lbl_amecop.Location = new System.Drawing.Point(7, 7);
            this.lbl_amecop.Name = "lbl_amecop";
            this.lbl_amecop.Size = new System.Drawing.Size(46, 13);
            this.lbl_amecop.TabIndex = 87;
            this.lbl_amecop.Text = "Amecop";
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Enabled = false;
            this.txt_cantidad.Location = new System.Drawing.Point(1049, 24);
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(93, 20);
            this.txt_cantidad.TabIndex = 96;
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // Apartado_mercancia_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 571);
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
            this.Controls.Add(this.dgv_apartados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Apartado_mercancia_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apartado de Mercancia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Apartado_mercancia_principal_FormClosing);
            this.Load += new System.EventHandler(this.Apartado_mercancia_principal_Load);
            this.Shown += new System.EventHandler(this.Apartado_mercancia_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_apartados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.DataGridView dgv_apartados;
		private System.Windows.Forms.ComboBox cbb_lote;
		private System.Windows.Forms.ComboBox cbb_caducidad;
		private System.Windows.Forms.TextBox txt_producto;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label lbl_cantidad;
		private System.Windows.Forms.Label lbl_lote;
		private System.Windows.Forms.Label lbl_caducidad;
		private System.Windows.Forms.Label lbl_producto;
		private System.Windows.Forms.Label lbl_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn apartado_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_destino;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_destino_sin_formato;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_fecha_apartado;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_fecha_expiracion;
        private System.Windows.Forms.NumericUpDown txt_cantidad;
	}
}