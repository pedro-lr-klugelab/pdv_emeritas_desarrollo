namespace Farmacontrol_PDV.FORMS.consultas.faltantes
{
	partial class Faltantes_principal
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
            this.dgv_reportes_faltantes = new System.Windows.Forms.DataGridView();
            this.reporte_faltantes_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_creado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_terminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reportes_faltantes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_reportes_faltantes
            // 
            this.dgv_reportes_faltantes.AllowUserToAddRows = false;
            this.dgv_reportes_faltantes.AllowUserToDeleteRows = false;
            this.dgv_reportes_faltantes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_reportes_faltantes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_reportes_faltantes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_reportes_faltantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_reportes_faltantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_reportes_faltantes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reporte_faltantes_id,
            this.fecha_creado,
            this.fecha_terminado,
            this.empleado,
            this.almacen,
            this.sucursales});
            this.dgv_reportes_faltantes.Location = new System.Drawing.Point(12, 12);
            this.dgv_reportes_faltantes.MultiSelect = false;
            this.dgv_reportes_faltantes.Name = "dgv_reportes_faltantes";
            this.dgv_reportes_faltantes.ReadOnly = true;
            this.dgv_reportes_faltantes.RowHeadersVisible = false;
            this.dgv_reportes_faltantes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_reportes_faltantes.Size = new System.Drawing.Size(981, 409);
            this.dgv_reportes_faltantes.TabIndex = 0;
            this.dgv_reportes_faltantes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_reportes_faltantes_CellDoubleClick);
            // 
            // reporte_faltantes_id
            // 
            this.reporte_faltantes_id.DataPropertyName = "reporte_faltantes_id";
            this.reporte_faltantes_id.HeaderText = "reporte_faltantes_id";
            this.reporte_faltantes_id.Name = "reporte_faltantes_id";
            this.reporte_faltantes_id.ReadOnly = true;
            this.reporte_faltantes_id.Visible = false;
            // 
            // fecha_creado
            // 
            this.fecha_creado.DataPropertyName = "fecha_creado";
            dataGridViewCellStyle3.Format = "dd/MMM/yy hh:mm:ss tt";
            this.fecha_creado.DefaultCellStyle = dataGridViewCellStyle3;
            this.fecha_creado.FillWeight = 70F;
            this.fecha_creado.HeaderText = "Fecha Creado";
            this.fecha_creado.Name = "fecha_creado";
            this.fecha_creado.ReadOnly = true;
            // 
            // fecha_terminado
            // 
            this.fecha_terminado.DataPropertyName = "fecha_terminado";
            dataGridViewCellStyle4.Format = "dd/MMM/yy hh:mm:ss tt";
            this.fecha_terminado.DefaultCellStyle = dataGridViewCellStyle4;
            this.fecha_terminado.FillWeight = 70F;
            this.fecha_terminado.HeaderText = "Fecha Terminado";
            this.fecha_terminado.Name = "fecha_terminado";
            this.fecha_terminado.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.DataPropertyName = "nombre_empleado";
            this.empleado.FillWeight = 120F;
            this.empleado.HeaderText = "Empleado";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // almacen
            // 
            this.almacen.DataPropertyName = "almacen";
            this.almacen.HeaderText = "Almacen";
            this.almacen.Name = "almacen";
            this.almacen.ReadOnly = true;
            // 
            // sucursales
            // 
            this.sucursales.DataPropertyName = "nombre_sucursales";
            this.sucursales.FillWeight = 150F;
            this.sucursales.HeaderText = "Sucursales";
            this.sucursales.Name = "sucursales";
            this.sucursales.ReadOnly = true;
            // 
            // Faltantes_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 433);
            this.Controls.Add(this.dgv_reportes_faltantes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Faltantes_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Faltantes";
            this.Shown += new System.EventHandler(this.Faltantes_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reportes_faltantes)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_reportes_faltantes;
		private System.Windows.Forms.DataGridViewTextBoxColumn reporte_faltantes_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_creado;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_terminado;
		private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
		private System.Windows.Forms.DataGridViewTextBoxColumn almacen;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursales;
	}
}