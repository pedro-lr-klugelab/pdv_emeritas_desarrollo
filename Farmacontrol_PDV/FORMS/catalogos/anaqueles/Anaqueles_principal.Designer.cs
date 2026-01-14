namespace Farmacontrol_PDV.FORMS.catalogos.anaqueles
{
	partial class Anaqueles_principal
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.dgv_anaqueles = new System.Windows.Forms.DataGridView();
            this.anaquel_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_productos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_editar = new System.Windows.Forms.Button();
            this.btn_eliminar = new System.Windows.Forms.Button();
            this.btn_agregar_productos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_anaqueles)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(65, 6);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(327, 20);
            this.txt_nombre.TabIndex = 1;
            this.txt_nombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_KeyUp);
            // 
            // btn_agregar
            // 
            this.btn_agregar.Location = new System.Drawing.Point(398, 4);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(75, 23);
            this.btn_agregar.TabIndex = 2;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // dgv_anaqueles
            // 
            this.dgv_anaqueles.AllowUserToAddRows = false;
            this.dgv_anaqueles.AllowUserToDeleteRows = false;
            this.dgv_anaqueles.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_anaqueles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_anaqueles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_anaqueles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_anaqueles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_anaqueles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.anaquel_id,
            this.nombre,
            this.posicion,
            this.numero_productos});
            this.dgv_anaqueles.Location = new System.Drawing.Point(12, 32);
            this.dgv_anaqueles.Name = "dgv_anaqueles";
            this.dgv_anaqueles.ReadOnly = true;
            this.dgv_anaqueles.RowHeadersVisible = false;
            this.dgv_anaqueles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_anaqueles.Size = new System.Drawing.Size(461, 524);
            this.dgv_anaqueles.TabIndex = 3;
            // 
            // anaquel_id
            // 
            this.anaquel_id.DataPropertyName = "anaquel_id";
            this.anaquel_id.HeaderText = "anaquel_id";
            this.anaquel_id.Name = "anaquel_id";
            this.anaquel_id.ReadOnly = true;
            this.anaquel_id.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // posicion
            // 
            this.posicion.DataPropertyName = "posicion";
            this.posicion.HeaderText = "posicion";
            this.posicion.Name = "posicion";
            this.posicion.ReadOnly = true;
            this.posicion.Visible = false;
            // 
            // numero_productos
            // 
            this.numero_productos.DataPropertyName = "numero_productos";
            this.numero_productos.HeaderText = "numero_productos";
            this.numero_productos.Name = "numero_productos";
            this.numero_productos.ReadOnly = true;
            this.numero_productos.Visible = false;
            // 
            // btn_editar
            // 
            this.btn_editar.Location = new System.Drawing.Point(479, 32);
            this.btn_editar.Name = "btn_editar";
            this.btn_editar.Size = new System.Drawing.Size(129, 23);
            this.btn_editar.TabIndex = 4;
            this.btn_editar.Text = "Editar Nombre Anaquel";
            this.btn_editar.UseVisualStyleBackColor = true;
            this.btn_editar.Click += new System.EventHandler(this.btn_editar_Click);
            // 
            // btn_eliminar
            // 
            this.btn_eliminar.Location = new System.Drawing.Point(479, 61);
            this.btn_eliminar.Name = "btn_eliminar";
            this.btn_eliminar.Size = new System.Drawing.Size(129, 23);
            this.btn_eliminar.TabIndex = 5;
            this.btn_eliminar.Text = "Eliminar Anaquel";
            this.btn_eliminar.UseVisualStyleBackColor = true;
            this.btn_eliminar.Click += new System.EventHandler(this.btn_eliminar_Click);
            // 
            // btn_agregar_productos
            // 
            this.btn_agregar_productos.Location = new System.Drawing.Point(479, 90);
            this.btn_agregar_productos.Name = "btn_agregar_productos";
            this.btn_agregar_productos.Size = new System.Drawing.Size(129, 23);
            this.btn_agregar_productos.TabIndex = 6;
            this.btn_agregar_productos.Text = "Agregar Productos";
            this.btn_agregar_productos.UseVisualStyleBackColor = true;
            this.btn_agregar_productos.Click += new System.EventHandler(this.btn_agregar_productos_Click);
            // 
            // Anaqueles_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 568);
            this.Controls.Add(this.btn_agregar_productos);
            this.Controls.Add(this.btn_eliminar);
            this.Controls.Add(this.btn_editar);
            this.Controls.Add(this.dgv_anaqueles);
            this.Controls.Add(this.btn_agregar);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Anaqueles_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anaqueles";
            this.Load += new System.EventHandler(this.Anaqueles_principal_Load);
            this.Shown += new System.EventHandler(this.Anaqueles_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_anaqueles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_nombre;
		private System.Windows.Forms.Button btn_agregar;
		private System.Windows.Forms.DataGridView dgv_anaqueles;
		private System.Windows.Forms.Button btn_editar;
		private System.Windows.Forms.Button btn_eliminar;
		private System.Windows.Forms.Button btn_agregar_productos;
		private System.Windows.Forms.DataGridViewTextBoxColumn anaquel_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn posicion;
		private System.Windows.Forms.DataGridViewTextBoxColumn numero_productos;
	}
}