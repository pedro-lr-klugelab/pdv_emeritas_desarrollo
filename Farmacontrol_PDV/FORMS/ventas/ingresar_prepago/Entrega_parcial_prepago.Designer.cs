namespace Farmacontrol_PDV.FORMS.ventas.ingresar_prepago
{
	partial class Entrega_parcial_prepago
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
            this.dgv_entrega_parcial = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirma_fisico = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_procesar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_entrega_parcial)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_entrega_parcial
            // 
            this.dgv_entrega_parcial.AllowUserToAddRows = false;
            this.dgv_entrega_parcial.AllowUserToDeleteRows = false;
            this.dgv_entrega_parcial.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_entrega_parcial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_entrega_parcial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_entrega_parcial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_entrega_parcial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_entrega_parcial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.amecop,
            this.producto,
            this.caducidad,
            this.lote,
            this.cantidad,
            this.confirma_fisico});
            this.dgv_entrega_parcial.Location = new System.Drawing.Point(12, 12);
            this.dgv_entrega_parcial.MultiSelect = false;
            this.dgv_entrega_parcial.Name = "dgv_entrega_parcial";
            this.dgv_entrega_parcial.RowHeadersVisible = false;
            this.dgv_entrega_parcial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_entrega_parcial.Size = new System.Drawing.Size(1111, 410);
            this.dgv_entrega_parcial.TabIndex = 1;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.Visible = false;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 60F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 200F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 50F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.FillWeight = 120F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad.FillWeight = 70F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // confirma_fisico
            // 
            this.confirma_fisico.FillWeight = 50F;
            this.confirma_fisico.HeaderText = "Confirmo Fisico";
            this.confirma_fisico.Name = "confirma_fisico";
            this.confirma_fisico.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.confirma_fisico.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btn_procesar
            // 
            this.btn_procesar.Location = new System.Drawing.Point(943, 428);
            this.btn_procesar.Name = "btn_procesar";
            this.btn_procesar.Size = new System.Drawing.Size(99, 23);
            this.btn_procesar.TabIndex = 2;
            this.btn_procesar.Text = "Procesar prepago";
            this.btn_procesar.UseVisualStyleBackColor = true;
            this.btn_procesar.Click += new System.EventHandler(this.btn_procesar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(1048, 428);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Entrega_parcial_prepago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 463);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_procesar);
            this.Controls.Add(this.dgv_entrega_parcial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Entrega_parcial_prepago";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrega parcial de productos";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Entrega_parcial_prepago_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_entrega_parcial)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_entrega_parcial;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
		private System.Windows.Forms.DataGridViewCheckBoxColumn confirma_fisico;
		private System.Windows.Forms.Button btn_procesar;
		private System.Windows.Forms.Button btn_cancelar;
	}
}