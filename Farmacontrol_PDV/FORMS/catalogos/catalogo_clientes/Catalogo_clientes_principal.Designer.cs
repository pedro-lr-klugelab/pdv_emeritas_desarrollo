namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_clientes
{
	partial class Catalogo_clientes_principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_busqueda_nombre = new System.Windows.Forms.Label();
            this.txt_busqueda_cliente = new System.Windows.Forms.TextBox();
            this.dgv_clientes = new System.Windows.Forms.DataGridView();
            this.cliente_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_registrar_cliente = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_busqueda_nombre
            // 
            this.lbl_busqueda_nombre.AutoSize = true;
            this.lbl_busqueda_nombre.Location = new System.Drawing.Point(13, 13);
            this.lbl_busqueda_nombre.Name = "lbl_busqueda_nombre";
            this.lbl_busqueda_nombre.Size = new System.Drawing.Size(100, 13);
            this.lbl_busqueda_nombre.TabIndex = 0;
            this.lbl_busqueda_nombre.Text = "Nombre o telefono :";
            // 
            // txt_busqueda_cliente
            // 
            this.txt_busqueda_cliente.Location = new System.Drawing.Point(133, 10);
            this.txt_busqueda_cliente.Name = "txt_busqueda_cliente";
            this.txt_busqueda_cliente.Size = new System.Drawing.Size(505, 20);
            this.txt_busqueda_cliente.TabIndex = 1;
            this.txt_busqueda_cliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_busqueda_cliente_KeyDown);
            // 
            // dgv_clientes
            // 
            this.dgv_clientes.AllowUserToAddRows = false;
            this.dgv_clientes.AllowUserToDeleteRows = false;
            this.dgv_clientes.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_clientes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_clientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_clientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgv_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cliente_id,
            this.nombre,
            this.tipo,
            this.direccion,
            this.telefono});
            this.dgv_clientes.Location = new System.Drawing.Point(12, 36);
            this.dgv_clientes.MultiSelect = false;
            this.dgv_clientes.Name = "dgv_clientes";
            this.dgv_clientes.ReadOnly = true;
            this.dgv_clientes.RowHeadersVisible = false;
            this.dgv_clientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_clientes.Size = new System.Drawing.Size(998, 471);
            this.dgv_clientes.TabIndex = 2;
            this.dgv_clientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientes_CellContentClick);
            this.dgv_clientes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientes_CellContentDoubleClick);
            this.dgv_clientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientes_CellDoubleClick);
            this.dgv_clientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_clientes_KeyDown);
            // 
            // cliente_id
            // 
            this.cliente_id.DataPropertyName = "cliente_id";
            this.cliente_id.HeaderText = "cliente_id";
            this.cliente_id.Name = "cliente_id";
            this.cliente_id.ReadOnly = true;
            this.cliente_id.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 250F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // tipo
            // 
            this.tipo.DataPropertyName = "tipo";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tipo.DefaultCellStyle = dataGridViewCellStyle15;
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            // 
            // direccion
            // 
            this.direccion.DataPropertyName = "direccion";
            this.direccion.FillWeight = 300F;
            this.direccion.HeaderText = "Direccion";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            // 
            // telefono
            // 
            this.telefono.DataPropertyName = "telefono";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.telefono.DefaultCellStyle = dataGridViewCellStyle16;
            this.telefono.HeaderText = "Telefono";
            this.telefono.Name = "telefono";
            this.telefono.ReadOnly = true;
            // 
            // btn_registrar_cliente
            // 
            this.btn_registrar_cliente.Location = new System.Drawing.Point(814, 513);
            this.btn_registrar_cliente.Name = "btn_registrar_cliente";
            this.btn_registrar_cliente.Size = new System.Drawing.Size(115, 23);
            this.btn_registrar_cliente.TabIndex = 3;
            this.btn_registrar_cliente.Text = "Registrar Cliente";
            this.btn_registrar_cliente.UseVisualStyleBackColor = true;
            this.btn_registrar_cliente.Click += new System.EventHandler(this.btn_registrar_cliente_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(935, 513);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 4;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(293, 121);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(417, 39);
            this.progBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(16, 523);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Doble click sobre el cliente que requieras editar";
            // 
            // Catalogo_clientes_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 548);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_registrar_cliente);
            this.Controls.Add(this.dgv_clientes);
            this.Controls.Add(this.txt_busqueda_cliente);
            this.Controls.Add(this.lbl_busqueda_nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Catalogo_clientes_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catalogo de Clientes";
            this.Shown += new System.EventHandler(this.Catalogo_clientes_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_busqueda_nombre;
		private System.Windows.Forms.TextBox txt_busqueda_cliente;
		private System.Windows.Forms.DataGridView dgv_clientes;
		private System.Windows.Forms.Button btn_registrar_cliente;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.DataGridViewTextBoxColumn cliente_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
		private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
		private System.Windows.Forms.DataGridViewTextBoxColumn telefono;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.Label label1;
	}
}