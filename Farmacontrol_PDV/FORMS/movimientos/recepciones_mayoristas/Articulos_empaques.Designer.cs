namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
	partial class Articulos_empaques
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
            this.label1 = new System.Windows.Forms.Label();
            this.rdb_empaque = new System.Windows.Forms.RadioButton();
            this.rdb_contenido = new System.Windows.Forms.RadioButton();
            this.dgv_productos = new System.Windows.Forms.DataGridView();
            this.c_articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Este producto forma parte de un empaque, ¿Que desea hacer?";
            // 
            // rdb_empaque
            // 
            this.rdb_empaque.AutoSize = true;
            this.rdb_empaque.Location = new System.Drawing.Point(15, 35);
            this.rdb_empaque.Name = "rdb_empaque";
            this.rdb_empaque.Size = new System.Drawing.Size(125, 17);
            this.rdb_empaque.TabIndex = 1;
            this.rdb_empaque.Text = "Registrar el empaque";
            this.rdb_empaque.UseVisualStyleBackColor = true;
            // 
            // rdb_contenido
            // 
            this.rdb_contenido.AutoSize = true;
            this.rdb_contenido.Location = new System.Drawing.Point(146, 35);
            this.rdb_contenido.Name = "rdb_contenido";
            this.rdb_contenido.Size = new System.Drawing.Size(382, 17);
            this.rdb_contenido.TabIndex = 2;
            this.rdb_contenido.Text = "Dar entrada a todos los productos dentro del empaque de manera individual";
            this.rdb_contenido.UseVisualStyleBackColor = true;
            // 
            // dgv_productos
            // 
            this.dgv_productos.AllowUserToAddRows = false;
            this.dgv_productos.AllowUserToDeleteRows = false;
            this.dgv_productos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_productos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_productos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_productos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_productos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_productos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_articulo_id,
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_cantidad});
            this.dgv_productos.Enabled = false;
            this.dgv_productos.Location = new System.Drawing.Point(12, 83);
            this.dgv_productos.Name = "dgv_productos";
            this.dgv_productos.ReadOnly = true;
            this.dgv_productos.RowHeadersVisible = false;
            this.dgv_productos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_productos.Size = new System.Drawing.Size(920, 304);
            this.dgv_productos.TabIndex = 3;
            // 
            // c_articulo_id
            // 
            this.c_articulo_id.DataPropertyName = "articulo_id";
            this.c_articulo_id.HeaderText = "c_articulo_id";
            this.c_articulo_id.Name = "c_articulo_id";
            this.c_articulo_id.ReadOnly = true;
            this.c_articulo_id.Visible = false;
            // 
            // c_amecop
            // 
            this.c_amecop.DataPropertyName = "amecop";
            this.c_amecop.FillWeight = 80F;
            this.c_amecop.HeaderText = "Amecop";
            this.c_amecop.Name = "c_amecop";
            this.c_amecop.ReadOnly = true;
            // 
            // c_producto
            // 
            this.c_producto.DataPropertyName = "producto";
            this.c_producto.FillWeight = 200F;
            this.c_producto.HeaderText = "Producto";
            this.c_producto.Name = "c_producto";
            this.c_producto.ReadOnly = true;
            // 
            // c_caducidad
            // 
            this.c_caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_caducidad.FillWeight = 80F;
            this.c_caducidad.HeaderText = "Caducidad";
            this.c_caducidad.Name = "c_caducidad";
            this.c_caducidad.ReadOnly = true;
            // 
            // c_lote
            // 
            this.c_lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            this.c_lote.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_cantidad.FillWeight = 70F;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(796, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Registrar Producto(s)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Productos dentro del empaque:";
            // 
            // Articulos_empaques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 428);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv_productos);
            this.Controls.Add(this.rdb_contenido);
            this.Controls.Add(this.rdb_empaque);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Articulos_empaques";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información Empaques";
            this.Shown += new System.EventHandler(this.Articulos_empaques_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rdb_empaque;
		private System.Windows.Forms.RadioButton rdb_contenido;
		private System.Windows.Forms.DataGridView dgv_productos;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
	}
}