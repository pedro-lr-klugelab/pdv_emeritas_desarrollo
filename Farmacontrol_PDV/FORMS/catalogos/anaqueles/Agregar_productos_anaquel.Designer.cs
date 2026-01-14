namespace Farmacontrol_PDV.FORMS.catalogos.anaqueles
{
	partial class Agregar_productos_anaquel
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
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_cerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(64, 6);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(184, 20);
            this.txt_amecop.TabIndex = 0;
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            this.txt_amecop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_amecop_KeyPress);
            this.txt_amecop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Amecop";
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_articulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.amecop,
            this.nombre});
            this.dgv_articulos.Location = new System.Drawing.Point(8, 32);
            this.dgv_articulos.MultiSelect = false;
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(447, 421);
            this.dgv_articulos.TabIndex = 2;
            this.dgv_articulos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgv_articulos_KeyUp);
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 60F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 150F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.Location = new System.Drawing.Point(380, 459);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(75, 23);
            this.btn_cerrar.TabIndex = 3;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = true;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // Agregar_productos_anaquel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 494);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.dgv_articulos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_amecop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Agregar_productos_anaquel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos en el anaquel";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.Button btn_cerrar;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
	}
}