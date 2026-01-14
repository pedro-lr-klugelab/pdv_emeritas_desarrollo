namespace Farmacontrol_PDV.FORMS.consultas.cambios_precio
{
	partial class Cambios_precio_principal
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
            this.dgv_cambio_precios = new System.Windows.Forms.DataGridView();
            this.cambio_precio_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mayorista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_creado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_productos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cambio_precios)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_cambio_precios
            // 
            this.dgv_cambio_precios.AllowUserToAddRows = false;
            this.dgv_cambio_precios.AllowUserToDeleteRows = false;
            this.dgv_cambio_precios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_cambio_precios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_cambio_precios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_cambio_precios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_cambio_precios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cambio_precios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cambio_precio_id,
            this.mayorista,
            this.fecha_creado,
            this.numero_productos});
            this.dgv_cambio_precios.Location = new System.Drawing.Point(12, 12);
            this.dgv_cambio_precios.MultiSelect = false;
            this.dgv_cambio_precios.Name = "dgv_cambio_precios";
            this.dgv_cambio_precios.ReadOnly = true;
            this.dgv_cambio_precios.RowHeadersVisible = false;
            this.dgv_cambio_precios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_cambio_precios.Size = new System.Drawing.Size(764, 474);
            this.dgv_cambio_precios.TabIndex = 0;
            this.dgv_cambio_precios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_cambio_precios_CellDoubleClick);
            // 
            // cambio_precio_id
            // 
            this.cambio_precio_id.DataPropertyName = "cambio_precio_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cambio_precio_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.cambio_precio_id.FillWeight = 70F;
            this.cambio_precio_id.HeaderText = "Folio";
            this.cambio_precio_id.Name = "cambio_precio_id";
            this.cambio_precio_id.ReadOnly = true;
            // 
            // mayorista
            // 
            this.mayorista.DataPropertyName = "mayorista";
            this.mayorista.FillWeight = 130F;
            this.mayorista.HeaderText = "Mayorista";
            this.mayorista.Name = "mayorista";
            this.mayorista.ReadOnly = true;
            // 
            // fecha_creado
            // 
            this.fecha_creado.DataPropertyName = "fecha_creado";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MMM/yyyy h:m:s";
            this.fecha_creado.DefaultCellStyle = dataGridViewCellStyle4;
            this.fecha_creado.FillWeight = 80F;
            this.fecha_creado.HeaderText = "Fecha";
            this.fecha_creado.Name = "fecha_creado";
            this.fecha_creado.ReadOnly = true;
            // 
            // numero_productos
            // 
            this.numero_productos.DataPropertyName = "numero_productos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numero_productos.DefaultCellStyle = dataGridViewCellStyle5;
            this.numero_productos.FillWeight = 70F;
            this.numero_productos.HeaderText = "Numero de Productos";
            this.numero_productos.Name = "numero_productos";
            this.numero_productos.ReadOnly = true;
            // 
            // Cambios_precio_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 498);
            this.Controls.Add(this.dgv_cambio_precios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cambios_precio_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambios de precio";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cambio_precios)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_cambio_precios;
		private System.Windows.Forms.DataGridViewTextBoxColumn cambio_precio_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn mayorista;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_creado;
		private System.Windows.Forms.DataGridViewTextBoxColumn numero_productos;
	}
}