namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Busqueda_creditos
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
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.txt_nombre_cliente = new System.Windows.Forms.TextBox();
            this.dgv_clientes_creditos = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna_cliente_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo_disponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente_activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progBarCredito = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes_creditos)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_nombre
            // 
            this.lbl_nombre.AutoSize = true;
            this.lbl_nombre.Location = new System.Drawing.Point(13, 13);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(47, 13);
            this.lbl_nombre.TabIndex = 0;
            this.lbl_nombre.Text = "Nombre:";
            // 
            // txt_nombre_cliente
            // 
            this.txt_nombre_cliente.Location = new System.Drawing.Point(66, 10);
            this.txt_nombre_cliente.Name = "txt_nombre_cliente";
            this.txt_nombre_cliente.Size = new System.Drawing.Size(633, 20);
            this.txt_nombre_cliente.TabIndex = 1;
            this.txt_nombre_cliente.Enter += new System.EventHandler(this.txt_nombre_cliente_Enter);
            this.txt_nombre_cliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_cliente_KeyDown);
            // 
            // dgv_clientes_creditos
            // 
            this.dgv_clientes_creditos.AllowUserToAddRows = false;
            this.dgv_clientes_creditos.AllowUserToDeleteRows = false;
            this.dgv_clientes_creditos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_clientes_creditos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_clientes_creditos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_clientes_creditos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_clientes_creditos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes_creditos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.columna_cliente_id,
            this.saldo_disponible,
            this.saldo_total,
            this.cliente_activo});
            this.dgv_clientes_creditos.Location = new System.Drawing.Point(12, 36);
            this.dgv_clientes_creditos.Name = "dgv_clientes_creditos";
            this.dgv_clientes_creditos.ReadOnly = true;
            this.dgv_clientes_creditos.RowHeadersVisible = false;
            this.dgv_clientes_creditos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_clientes_creditos.Size = new System.Drawing.Size(687, 271);
            this.dgv_clientes_creditos.TabIndex = 2;
            this.dgv_clientes_creditos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_clientes_creditos_CellFormatting);
            this.dgv_clientes_creditos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_clientes_creditos_KeyDown);
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 250F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // columna_cliente_id
            // 
            this.columna_cliente_id.DataPropertyName = "cliente_id";
            this.columna_cliente_id.HeaderText = "columna_cliente_id";
            this.columna_cliente_id.Name = "columna_cliente_id";
            this.columna_cliente_id.ReadOnly = true;
            this.columna_cliente_id.Visible = false;
            // 
            // saldo_disponible
            // 
            this.saldo_disponible.DataPropertyName = "credito_disponible";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.saldo_disponible.DefaultCellStyle = dataGridViewCellStyle3;
            this.saldo_disponible.HeaderText = "Saldo disponible";
            this.saldo_disponible.Name = "saldo_disponible";
            this.saldo_disponible.ReadOnly = true;
            // 
            // saldo_total
            // 
            this.saldo_total.DataPropertyName = "credito_total";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.saldo_total.DefaultCellStyle = dataGridViewCellStyle4;
            this.saldo_total.HeaderText = "Total";
            this.saldo_total.Name = "saldo_total";
            this.saldo_total.ReadOnly = true;
            // 
            // cliente_activo
            // 
            this.cliente_activo.DataPropertyName = "cliente_activo";
            this.cliente_activo.HeaderText = "cliente_activo";
            this.cliente_activo.Name = "cliente_activo";
            this.cliente_activo.ReadOnly = true;
            this.cliente_activo.Visible = false;
            // 
            // progBarCredito
            // 
            this.progBarCredito.Location = new System.Drawing.Point(184, 136);
            this.progBarCredito.Name = "progBarCredito";
            this.progBarCredito.Size = new System.Drawing.Size(357, 23);
            this.progBarCredito.TabIndex = 3;
            // 
            // Busqueda_creditos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 319);
            this.Controls.Add(this.progBarCredito);
            this.Controls.Add(this.dgv_clientes_creditos);
            this.Controls.Add(this.txt_nombre_cliente);
            this.Controls.Add(this.lbl_nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Busqueda_creditos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes con credito";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes_creditos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_nombre;
		private System.Windows.Forms.TextBox txt_nombre_cliente;
        private System.Windows.Forms.DataGridView dgv_clientes_creditos;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn columna_cliente_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo_disponible;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente_activo;
        private System.Windows.Forms.ProgressBar progBarCredito;
	}
}