namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	partial class Importar_cotizacion
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
            this.lbl_filtro = new System.Windows.Forms.Label();
            this.txt_filtro = new System.Windows.Forms.TextBox();
            this.dgv_cotizaciones_ventas = new System.Windows.Forms.DataGridView();
            this.cotizacion_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_creado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cotizaciones_ventas)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_filtro
            // 
            this.lbl_filtro.AutoSize = true;
            this.lbl_filtro.Location = new System.Drawing.Point(13, 13);
            this.lbl_filtro.Name = "lbl_filtro";
            this.lbl_filtro.Size = new System.Drawing.Size(32, 13);
            this.lbl_filtro.TabIndex = 0;
            this.lbl_filtro.Text = "Folio:";
            // 
            // txt_filtro
            // 
            this.txt_filtro.Location = new System.Drawing.Point(51, 10);
            this.txt_filtro.Name = "txt_filtro";
            this.txt_filtro.Size = new System.Drawing.Size(166, 20);
            this.txt_filtro.TabIndex = 1;
            this.txt_filtro.Enter += new System.EventHandler(this.txt_filtro_Enter);
            this.txt_filtro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_filtro_KeyDown);
            this.txt_filtro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_filtro_KeyUp);
            // 
            // dgv_cotizaciones_ventas
            // 
            this.dgv_cotizaciones_ventas.AllowUserToAddRows = false;
            this.dgv_cotizaciones_ventas.AllowUserToDeleteRows = false;
            this.dgv_cotizaciones_ventas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_cotizaciones_ventas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_cotizaciones_ventas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_cotizaciones_ventas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_cotizaciones_ventas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_cotizaciones_ventas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cotizacion_id,
            this.empleado,
            this.fecha_creado,
            this.cliente,
            this.total});
            this.dgv_cotizaciones_ventas.Location = new System.Drawing.Point(12, 36);
            this.dgv_cotizaciones_ventas.MultiSelect = false;
            this.dgv_cotizaciones_ventas.Name = "dgv_cotizaciones_ventas";
            this.dgv_cotizaciones_ventas.ReadOnly = true;
            this.dgv_cotizaciones_ventas.RowHeadersVisible = false;
            this.dgv_cotizaciones_ventas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_cotizaciones_ventas.Size = new System.Drawing.Size(966, 306);
            this.dgv_cotizaciones_ventas.TabIndex = 2;
            this.dgv_cotizaciones_ventas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_cotizaciones_ventas_KeyDown);
            // 
            // cotizacion_id
            // 
            this.cotizacion_id.DataPropertyName = "cotizacion_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cotizacion_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.cotizacion_id.HeaderText = "Folio";
            this.cotizacion_id.Name = "cotizacion_id";
            this.cotizacion_id.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.DataPropertyName = "empleado";
            this.empleado.FillWeight = 120F;
            this.empleado.HeaderText = "Empleado";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // fecha_creado
            // 
            this.fecha_creado.DataPropertyName = "fecha";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MMM/yyyy H:mm:ss tt";
            dataGridViewCellStyle4.NullValue = null;
            this.fecha_creado.DefaultCellStyle = dataGridViewCellStyle4;
            this.fecha_creado.FillWeight = 90F;
            this.fecha_creado.HeaderText = "Fecha";
            this.fecha_creado.Name = "fecha_creado";
            this.fecha_creado.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.DataPropertyName = "cliente";
            this.cliente.FillWeight = 120F;
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            this.total.DefaultCellStyle = dataGridViewCellStyle5;
            this.total.FillWeight = 80F;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // Importar_cotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(990, 354);
            this.Controls.Add(this.dgv_cotizaciones_ventas);
            this.Controls.Add(this.txt_filtro);
            this.Controls.Add(this.lbl_filtro);
            this.MaximizeBox = false;
            this.Name = "Importar_cotizacion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Cotizacion";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Importar_cotizacion_Load);
            this.Shown += new System.EventHandler(this.Importar_cotizacion_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cotizaciones_ventas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_filtro;
		private System.Windows.Forms.TextBox txt_filtro;
        private System.Windows.Forms.DataGridView dgv_cotizaciones_ventas;
        private System.Windows.Forms.DataGridViewTextBoxColumn cotizacion_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_creado;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
	}
}