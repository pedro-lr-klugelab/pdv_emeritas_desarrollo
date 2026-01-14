namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Busqueda_clientes_domicilios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.dgv_domicilios = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna_cliente_domicilio_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.domicilio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_registrar_cliente = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_domicilios)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_nombre
            // 
            this.lbl_nombre.AutoSize = true;
            this.lbl_nombre.Location = new System.Drawing.Point(13, 13);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(137, 13);
            this.lbl_nombre.TabIndex = 0;
            this.lbl_nombre.Text = "Buscar Nombre o Telefono:";
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(156, 10);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(620, 20);
            this.txt_nombre.TabIndex = 1;
            this.txt_nombre.TextChanged += new System.EventHandler(this.txt_nombre_TextChanged);
            this.txt_nombre.Enter += new System.EventHandler(this.txt_nombre_Enter);
            this.txt_nombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_KeyDown);
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
            this.nombre,
            this.cliente_id,
            this.columna_cliente_domicilio_id,
            this.tipo,
            this.domicilio,
            this.telefono});
            this.dgv_domicilios.Location = new System.Drawing.Point(12, 36);
            this.dgv_domicilios.Name = "dgv_domicilios";
            this.dgv_domicilios.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_domicilios.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_domicilios.RowHeadersVisible = false;
            this.dgv_domicilios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_domicilios.Size = new System.Drawing.Size(938, 383);
            this.dgv_domicilios.TabIndex = 2;
            this.dgv_domicilios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_domicilios_KeyDown);
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nombre.DefaultCellStyle = dataGridViewCellStyle3;
            this.nombre.FillWeight = 250F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // cliente_id
            // 
            this.cliente_id.DataPropertyName = "cliente_id";
            this.cliente_id.HeaderText = "cliente_id";
            this.cliente_id.Name = "cliente_id";
            this.cliente_id.ReadOnly = true;
            this.cliente_id.Visible = false;
            // 
            // columna_cliente_domicilio_id
            // 
            this.columna_cliente_domicilio_id.DataPropertyName = "cliente_domicilio_id";
            this.columna_cliente_domicilio_id.HeaderText = "columna_cliente_domicilio_id";
            this.columna_cliente_domicilio_id.Name = "columna_cliente_domicilio_id";
            this.columna_cliente_domicilio_id.ReadOnly = true;
            this.columna_cliente_domicilio_id.Visible = false;
            // 
            // tipo
            // 
            this.tipo.DataPropertyName = "tipo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.tipo.DefaultCellStyle = dataGridViewCellStyle4;
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            // 
            // domicilio
            // 
            this.domicilio.DataPropertyName = "direccion";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.domicilio.DefaultCellStyle = dataGridViewCellStyle5;
            this.domicilio.FillWeight = 250F;
            this.domicilio.HeaderText = "Domicilio";
            this.domicilio.Name = "domicilio";
            this.domicilio.ReadOnly = true;
            // 
            // telefono
            // 
            this.telefono.DataPropertyName = "telefono";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "(###) ###-####";
            dataGridViewCellStyle6.NullValue = null;
            this.telefono.DefaultCellStyle = dataGridViewCellStyle6;
            this.telefono.HeaderText = "Telefono";
            this.telefono.Name = "telefono";
            this.telefono.ReadOnly = true;
            // 
            // btn_registrar_cliente
            // 
            this.btn_registrar_cliente.Location = new System.Drawing.Point(831, 7);
            this.btn_registrar_cliente.Name = "btn_registrar_cliente";
            this.btn_registrar_cliente.Size = new System.Drawing.Size(119, 23);
            this.btn_registrar_cliente.TabIndex = 3;
            this.btn_registrar_cliente.Text = "Registrar Cliente";
            this.btn_registrar_cliente.UseVisualStyleBackColor = true;
            this.btn_registrar_cliente.Click += new System.EventHandler(this.btn_registrar_cliente_Click);
            // 
            // Busqueda_clientes_domicilios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 431);
            this.ControlBox = false;
            this.Controls.Add(this.btn_registrar_cliente);
            this.Controls.Add(this.dgv_domicilios);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.lbl_nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Busqueda_clientes_domicilios";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicio a Domicilio";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_domicilios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_nombre;
		private System.Windows.Forms.TextBox txt_nombre;
		private System.Windows.Forms.DataGridView dgv_domicilios;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn cliente_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn columna_cliente_domicilio_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
		private System.Windows.Forms.DataGridViewTextBoxColumn domicilio;
		private System.Windows.Forms.DataGridViewTextBoxColumn telefono;
        private System.Windows.Forms.Button btn_registrar_cliente;
	}
}