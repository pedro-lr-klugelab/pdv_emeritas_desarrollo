namespace Farmacontrol_PDV.FORMS.ventas.elaborar_formula
{
	partial class Elaborar_formula_principal
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
            this.dgv_formulas_pendientes = new System.Windows.Forms.DataGridView();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formula_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_creado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_elaborado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_formulas_pendientes)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_formulas_pendientes
            // 
            this.dgv_formulas_pendientes.AllowUserToAddRows = false;
            this.dgv_formulas_pendientes.AllowUserToDeleteRows = false;
            this.dgv_formulas_pendientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_formulas_pendientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_formulas_pendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_formulas_pendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_formulas_pendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_formulas_pendientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sucursal,
            this.sucursal_id,
            this.formula_id,
            this.sucursal_folio,
            this.fecha_creado,
            this.fecha_elaborado,
            this.status});
            this.dgv_formulas_pendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_formulas_pendientes.Location = new System.Drawing.Point(3, 3);
            this.dgv_formulas_pendientes.MultiSelect = false;
            this.dgv_formulas_pendientes.Name = "dgv_formulas_pendientes";
            this.dgv_formulas_pendientes.ReadOnly = true;
            this.dgv_formulas_pendientes.RowHeadersVisible = false;
            this.dgv_formulas_pendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_formulas_pendientes.Size = new System.Drawing.Size(764, 340);
            this.dgv_formulas_pendientes.TabIndex = 0;
            this.dgv_formulas_pendientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_formulas_pendientes_CellDoubleClick);
            this.dgv_formulas_pendientes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_formulas_pendientes_CellFormatting);
            // 
            // sucursal
            // 
            this.sucursal.DataPropertyName = "sucursal";
            this.sucursal.FillWeight = 150F;
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            // 
            // sucursal_id
            // 
            this.sucursal_id.DataPropertyName = "sucursal_id";
            this.sucursal_id.HeaderText = "sucursal_id";
            this.sucursal_id.Name = "sucursal_id";
            this.sucursal_id.ReadOnly = true;
            this.sucursal_id.Visible = false;
            // 
            // formula_id
            // 
            this.formula_id.DataPropertyName = "formula_id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.formula_id.DefaultCellStyle = dataGridViewCellStyle3;
            this.formula_id.FillWeight = 50F;
            this.formula_id.HeaderText = "formula_id";
            this.formula_id.Name = "formula_id";
            this.formula_id.ReadOnly = true;
            this.formula_id.Visible = false;
            // 
            // sucursal_folio
            // 
            this.sucursal_folio.DataPropertyName = "sucursal_folio";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.sucursal_folio.DefaultCellStyle = dataGridViewCellStyle4;
            this.sucursal_folio.FillWeight = 50F;
            this.sucursal_folio.HeaderText = "Folio";
            this.sucursal_folio.Name = "sucursal_folio";
            this.sucursal_folio.ReadOnly = true;
            // 
            // fecha_creado
            // 
            this.fecha_creado.DataPropertyName = "fecha_creado";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "dd/MMM/yyyy hh:mm:ss tt";
            this.fecha_creado.DefaultCellStyle = dataGridViewCellStyle5;
            this.fecha_creado.FillWeight = 80F;
            this.fecha_creado.HeaderText = "Fecha Creado";
            this.fecha_creado.Name = "fecha_creado";
            this.fecha_creado.ReadOnly = true;
            this.fecha_creado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // fecha_elaborado
            // 
            this.fecha_elaborado.DataPropertyName = "fecha_elaborado";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "dd/MMM/yyyy hh:mm:ss tt";
            this.fecha_elaborado.DefaultCellStyle = dataGridViewCellStyle6;
            this.fecha_elaborado.FillWeight = 80F;
            this.fecha_elaborado.HeaderText = "Fecha Elaborado";
            this.fecha_elaborado.Name = "fecha_elaborado";
            this.fecha_elaborado.ReadOnly = true;
            this.fecha_elaborado.Visible = false;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.status.DefaultCellStyle = dataGridViewCellStyle7;
            this.status.FillWeight = 80F;
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(778, 372);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.dgv_formulas_pendientes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(770, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Formulas Pendientes";
            // 
            // Elaborar_formula_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 372);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Elaborar_formula_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elaborar Formula";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Elaborar_formula_principal_FormClosing);
            this.Shown += new System.EventHandler(this.Elaborar_formula_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_formulas_pendientes)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_formulas_pendientes;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn formula_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal_folio;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_creado;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha_elaborado;
		private System.Windows.Forms.DataGridViewTextBoxColumn status;
	}
}