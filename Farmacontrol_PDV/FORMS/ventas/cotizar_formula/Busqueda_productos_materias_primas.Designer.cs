namespace Farmacontrol_PDV.FORMS.ventas.cotizar_formula
{
	partial class Busqueda_productos_materias_primas
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_busqueda = new System.Windows.Forms.TextBox();
            this.dgv_productos_materias = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materia_prima_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_materia_prima = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productos_materias)).BeginInit();
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
            // txt_busqueda
            // 
            this.txt_busqueda.Location = new System.Drawing.Point(65, 6);
            this.txt_busqueda.Name = "txt_busqueda";
            this.txt_busqueda.Size = new System.Drawing.Size(545, 20);
            this.txt_busqueda.TabIndex = 0;
            this.txt_busqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_busqueda_KeyDown);
            this.txt_busqueda.Leave += new System.EventHandler(this.txt_busqueda_Leave);
            // 
            // dgv_productos_materias
            // 
            this.dgv_productos_materias.AllowUserToAddRows = false;
            this.dgv_productos_materias.AllowUserToDeleteRows = false;
            this.dgv_productos_materias.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_productos_materias.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_productos_materias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_productos_materias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_productos_materias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_productos_materias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.materia_prima_id,
            this.nombre,
            this.precio_costo,
            this.precio_publico,
            this.es_materia_prima});
            this.dgv_productos_materias.Location = new System.Drawing.Point(12, 32);
            this.dgv_productos_materias.MultiSelect = false;
            this.dgv_productos_materias.Name = "dgv_productos_materias";
            this.dgv_productos_materias.ReadOnly = true;
            this.dgv_productos_materias.RowHeadersVisible = false;
            this.dgv_productos_materias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_productos_materias.Size = new System.Drawing.Size(672, 344);
            this.dgv_productos_materias.TabIndex = 1;
            this.dgv_productos_materias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_productos_materias_KeyDown);
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // materia_prima_id
            // 
            this.materia_prima_id.DataPropertyName = "materia_prima_id";
            this.materia_prima_id.HeaderText = "materia_prima_id";
            this.materia_prima_id.Name = "materia_prima_id";
            this.materia_prima_id.ReadOnly = true;
            this.materia_prima_id.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 200F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // precio_costo
            // 
            this.precio_costo.DataPropertyName = "precio_costo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            this.precio_costo.DefaultCellStyle = dataGridViewCellStyle3;
            this.precio_costo.HeaderText = "Precio Costo";
            this.precio_costo.Name = "precio_costo";
            this.precio_costo.ReadOnly = true;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle4;
            this.precio_publico.HeaderText = "Precio Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            // 
            // es_materia_prima
            // 
            this.es_materia_prima.DataPropertyName = "es_materia_prima";
            this.es_materia_prima.FillWeight = 30F;
            this.es_materia_prima.HeaderText = "MP";
            this.es_materia_prima.Name = "es_materia_prima";
            this.es_materia_prima.ReadOnly = true;
            this.es_materia_prima.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.es_materia_prima.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(609, 382);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 2;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(528, 382);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Busqueda_productos_materias_primas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 417);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.dgv_productos_materias);
            this.Controls.Add(this.txt_busqueda);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Busqueda_productos_materias_primas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Busqueda";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productos_materias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_busqueda;
		private System.Windows.Forms.DataGridView dgv_productos_materias;
		private System.Windows.Forms.Button btn_aceptar;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn materia_prima_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_costo;
		private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
		private System.Windows.Forms.DataGridViewCheckBoxColumn es_materia_prima;
	}
}