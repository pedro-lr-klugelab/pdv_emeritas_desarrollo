namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc
{
	partial class Catalogo_facturas
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
            this.dgv_facturas = new System.Windows.Forms.DataGridView();
            this.venta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_reimprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_facturas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_facturas
            // 
            this.dgv_facturas.AllowUserToAddRows = false;
            this.dgv_facturas.AllowUserToDeleteRows = false;
            this.dgv_facturas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_facturas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_facturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_facturas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_facturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_facturas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.venta_id,
            this.sucursal_id,
            this.sucursal,
            this.Folio,
            this.Serie,
            this.Fecha});
            this.dgv_facturas.Location = new System.Drawing.Point(12, 12);
            this.dgv_facturas.MultiSelect = false;
            this.dgv_facturas.Name = "dgv_facturas";
            this.dgv_facturas.ReadOnly = true;
            this.dgv_facturas.RowHeadersVisible = false;
            this.dgv_facturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_facturas.Size = new System.Drawing.Size(756, 310);
            this.dgv_facturas.TabIndex = 0;
            // 
            // venta_id
            // 
            this.venta_id.DataPropertyName = "venta_id";
            this.venta_id.HeaderText = "venta_id";
            this.venta_id.Name = "venta_id";
            this.venta_id.ReadOnly = true;
            this.venta_id.Visible = false;
            // 
            // sucursal_id
            // 
            this.sucursal_id.DataPropertyName = "sucursal_id";
            this.sucursal_id.HeaderText = "sucursal_id";
            this.sucursal_id.Name = "sucursal_id";
            this.sucursal_id.ReadOnly = true;
            this.sucursal_id.Visible = false;
            // 
            // sucursal
            // 
            this.sucursal.DataPropertyName = "sucursal";
            this.sucursal.FillWeight = 130F;
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            // 
            // Folio
            // 
            this.Folio.DataPropertyName = "folio";
            this.Folio.FillWeight = 80F;
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.DataPropertyName = "serie";
            this.Serie.FillWeight = 80F;
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // btn_reimprimir
            // 
            this.btn_reimprimir.Location = new System.Drawing.Point(636, 328);
            this.btn_reimprimir.Name = "btn_reimprimir";
            this.btn_reimprimir.Size = new System.Drawing.Size(132, 23);
            this.btn_reimprimir.TabIndex = 1;
            this.btn_reimprimir.Text = "Re-imprimir factura";
            this.btn_reimprimir.UseVisualStyleBackColor = true;
            this.btn_reimprimir.Click += new System.EventHandler(this.btn_reimprimir_Click);
            // 
            // Catalogo_facturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 363);
            this.Controls.Add(this.btn_reimprimir);
            this.Controls.Add(this.dgv_facturas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Catalogo_facturas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturas";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_facturas)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_facturas;
		private System.Windows.Forms.Button btn_reimprimir;
		private System.Windows.Forms.DataGridViewTextBoxColumn venta_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
		private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
		private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
		private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
	}
}