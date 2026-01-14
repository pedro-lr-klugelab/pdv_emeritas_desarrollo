namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Busqueda_cliente_mayoreo
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
            this.dgv_clientes = new System.Windows.Forms.DataGridView();
            this.c_cliente_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_busqueda = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_clientes
            // 
            this.dgv_clientes.AllowUserToAddRows = false;
            this.dgv_clientes.AllowUserToDeleteRows = false;
            this.dgv_clientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_clientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_clientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_clientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_cliente_id,
            this.nombre,
            this.comentarios});
            this.dgv_clientes.Location = new System.Drawing.Point(12, 32);
            this.dgv_clientes.MultiSelect = false;
            this.dgv_clientes.Name = "dgv_clientes";
            this.dgv_clientes.ReadOnly = true;
            this.dgv_clientes.RowHeadersVisible = false;
            this.dgv_clientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_clientes.Size = new System.Drawing.Size(654, 314);
            this.dgv_clientes.TabIndex = 0;
            this.dgv_clientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_clientes_KeyDown);
            // 
            // c_cliente_id
            // 
            this.c_cliente_id.DataPropertyName = "cliente_id";
            this.c_cliente_id.HeaderText = "cliente_id";
            this.c_cliente_id.Name = "c_cliente_id";
            this.c_cliente_id.ReadOnly = true;
            this.c_cliente_id.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 200F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // comentarios
            // 
            this.comentarios.DataPropertyName = "comentarios";
            this.comentarios.FillWeight = 120F;
            this.comentarios.HeaderText = "Comentarios";
            this.comentarios.Name = "comentarios";
            this.comentarios.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // txt_busqueda
            // 
            this.txt_busqueda.Location = new System.Drawing.Point(65, 6);
            this.txt_busqueda.Name = "txt_busqueda";
            this.txt_busqueda.Size = new System.Drawing.Size(404, 20);
            this.txt_busqueda.TabIndex = 2;
            this.txt_busqueda.Enter += new System.EventHandler(this.txt_busqueda_Enter);
            this.txt_busqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_busqueda_KeyDown);
            // 
            // Busqueda_cliente_mayoreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 358);
            this.Controls.Add(this.txt_busqueda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_clientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Busqueda_cliente_mayoreo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Busqueda_cliente_mayoreo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_clientes;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_busqueda;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cliente_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn comentarios;
	}
}