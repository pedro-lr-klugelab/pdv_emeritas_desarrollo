namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Busqueda_domicilios
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
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.dgv_domicilios = new System.Windows.Forms.DataGridView();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna_cliente_domicilio_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_nombre_cliente = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_domicilios)).BeginInit();
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
            // dgv_domicilios
            // 
            this.dgv_domicilios.AllowUserToAddRows = false;
            this.dgv_domicilios.AllowUserToDeleteRows = false;
            this.dgv_domicilios.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_domicilios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_domicilios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_domicilios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_domicilios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_domicilios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tipo,
            this.direccion,
            this.columna_cliente_domicilio_id});
            this.dgv_domicilios.Location = new System.Drawing.Point(12, 36);
            this.dgv_domicilios.MultiSelect = false;
            this.dgv_domicilios.Name = "dgv_domicilios";
            this.dgv_domicilios.ReadOnly = true;
            this.dgv_domicilios.RowHeadersVisible = false;
            this.dgv_domicilios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_domicilios.Size = new System.Drawing.Size(664, 174);
            this.dgv_domicilios.TabIndex = 2;
            this.dgv_domicilios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // tipo
            // 
            this.tipo.DataPropertyName = "tipo";
            this.tipo.FillWeight = 80F;
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            // 
            // direccion
            // 
            this.direccion.DataPropertyName = "direccion";
            this.direccion.FillWeight = 250F;
            this.direccion.HeaderText = "Direccion";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            // 
            // columna_cliente_domicilio_id
            // 
            this.columna_cliente_domicilio_id.DataPropertyName = "cliente_domicilio_id";
            this.columna_cliente_domicilio_id.HeaderText = "cliente_domicilio_id";
            this.columna_cliente_domicilio_id.Name = "columna_cliente_domicilio_id";
            this.columna_cliente_domicilio_id.ReadOnly = true;
            this.columna_cliente_domicilio_id.Visible = false;
            // 
            // txt_nombre_cliente
            // 
            this.txt_nombre_cliente.Enabled = false;
            this.txt_nombre_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombre_cliente.Location = new System.Drawing.Point(66, 10);
            this.txt_nombre_cliente.Name = "txt_nombre_cliente";
            this.txt_nombre_cliente.Size = new System.Drawing.Size(610, 21);
            this.txt_nombre_cliente.TabIndex = 1;
            this.txt_nombre_cliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Busqueda_domicilios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 222);
            this.ControlBox = false;
            this.Controls.Add(this.dgv_domicilios);
            this.Controls.Add(this.txt_nombre_cliente);
            this.Controls.Add(this.lbl_nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Busqueda_domicilios";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Domicilios";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.domicilios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_domicilios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_nombre;
		private System.Windows.Forms.DataGridView dgv_domicilios;
		private System.Windows.Forms.TextBox txt_nombre_cliente;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
		private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
		private System.Windows.Forms.DataGridViewTextBoxColumn columna_cliente_domicilio_id;
	}
}