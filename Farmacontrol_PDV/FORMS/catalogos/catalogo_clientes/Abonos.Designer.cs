namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_clientes
{
	partial class Abonos
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
            this.dgv_abonos = new System.Windows.Forms.DataGridView();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo_anterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo_nuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_abonos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_abonos
            // 
            this.dgv_abonos.AllowUserToAddRows = false;
            this.dgv_abonos.AllowUserToDeleteRows = false;
            this.dgv_abonos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_abonos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_abonos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_abonos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_abonos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_abonos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sucursal,
            this.empleado,
            this.fecha_pago,
            this.saldo_anterior,
            this.importe,
            this.saldo_nuevo});
            this.dgv_abonos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_abonos.Location = new System.Drawing.Point(0, 0);
            this.dgv_abonos.MultiSelect = false;
            this.dgv_abonos.Name = "dgv_abonos";
            this.dgv_abonos.ReadOnly = true;
            this.dgv_abonos.RowHeadersVisible = false;
            this.dgv_abonos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_abonos.Size = new System.Drawing.Size(949, 248);
            this.dgv_abonos.TabIndex = 0;
            // 
            // sucursal
            // 
            this.sucursal.DataPropertyName = "sucursal";
            this.sucursal.FillWeight = 150F;
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.DataPropertyName = "empleado";
            this.empleado.FillWeight = 150F;
            this.empleado.HeaderText = "Atendio";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // fecha_pago
            // 
            this.fecha_pago.DataPropertyName = "fecha_pago";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MMM/yyyy h:mm:ss tt";
            this.fecha_pago.DefaultCellStyle = dataGridViewCellStyle3;
            this.fecha_pago.HeaderText = "Fecha";
            this.fecha_pago.Name = "fecha_pago";
            this.fecha_pago.ReadOnly = true;
            // 
            // saldo_anterior
            // 
            this.saldo_anterior.DataPropertyName = "saldo_anterior";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            this.saldo_anterior.DefaultCellStyle = dataGridViewCellStyle4;
            this.saldo_anterior.FillWeight = 70F;
            this.saldo_anterior.HeaderText = "Saldo Anterior";
            this.saldo_anterior.Name = "saldo_anterior";
            this.saldo_anterior.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.DataPropertyName = "importe";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            this.importe.DefaultCellStyle = dataGridViewCellStyle5;
            this.importe.FillWeight = 70F;
            this.importe.HeaderText = "Abonó";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            // 
            // saldo_nuevo
            // 
            this.saldo_nuevo.DataPropertyName = "saldo_nuevo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            this.saldo_nuevo.DefaultCellStyle = dataGridViewCellStyle6;
            this.saldo_nuevo.FillWeight = 70F;
            this.saldo_nuevo.HeaderText = "Saldo Nuevo";
            this.saldo_nuevo.Name = "saldo_nuevo";
            this.saldo_nuevo.ReadOnly = true;
            // 
            // Abonos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 248);
            this.Controls.Add(this.dgv_abonos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Abonos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abonos";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_abonos)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_abonos;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
		private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_pago;
		private System.Windows.Forms.DataGridViewTextBoxColumn saldo_anterior;
		private System.Windows.Forms.DataGridViewTextBoxColumn importe;
		private System.Windows.Forms.DataGridViewTextBoxColumn saldo_nuevo;
	}
}