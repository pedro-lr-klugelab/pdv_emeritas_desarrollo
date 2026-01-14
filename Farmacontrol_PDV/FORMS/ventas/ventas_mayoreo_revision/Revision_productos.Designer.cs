namespace Farmacontrol_PDV.FORMS.ventas.ventas_mayoreo_revision
{
	partial class Revision_productos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chb_acepto = new System.Windows.Forms.CheckBox();
            this.btn_finalizar = new System.Windows.Forms.Button();
            this.dgv_productos = new System.Windows.Forms.DataGridView();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad_revision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_conflicto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productos)).BeginInit();
            this.SuspendLayout();
            // 
            // chb_acepto
            // 
            this.chb_acepto.AutoSize = true;
            this.chb_acepto.Location = new System.Drawing.Point(12, 12);
            this.chb_acepto.Name = "chb_acepto";
            this.chb_acepto.Size = new System.Drawing.Size(417, 17);
            this.chb_acepto.TabIndex = 0;
            this.chb_acepto.Text = "He verificado los productos con conflictos y deseo continuar finalizando la revis" +
    "ion.";
            this.chb_acepto.UseVisualStyleBackColor = true;
            // 
            // btn_finalizar
            // 
            this.btn_finalizar.Location = new System.Drawing.Point(949, 380);
            this.btn_finalizar.Name = "btn_finalizar";
            this.btn_finalizar.Size = new System.Drawing.Size(108, 23);
            this.btn_finalizar.TabIndex = 1;
            this.btn_finalizar.Text = "Finalizar revision";
            this.btn_finalizar.UseVisualStyleBackColor = true;
            this.btn_finalizar.Click += new System.EventHandler(this.btn_finalizar_Click);
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
            this.amecop,
            this.producto,
            this.caducidad,
            this.lote,
            this.cantidad,
            this.cantidad_revision,
            this.diferencia,
            this.c_conflicto});
            this.dgv_productos.Enabled = false;
            this.dgv_productos.Location = new System.Drawing.Point(12, 35);
            this.dgv_productos.Name = "dgv_productos";
            this.dgv_productos.ReadOnly = true;
            this.dgv_productos.RowHeadersVisible = false;
            this.dgv_productos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_productos.Size = new System.Drawing.Size(1045, 339);
            this.dgv_productos.TabIndex = 2;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 70F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 180F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 70F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.FillWeight = 120F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            this.lote.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad.FillWeight = 70F;
            this.cantidad.HeaderText = "Cant. Cap.";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // cantidad_revision
            // 
            this.cantidad_revision.DataPropertyName = "cantidad_revision";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad_revision.DefaultCellStyle = dataGridViewCellStyle6;
            this.cantidad_revision.FillWeight = 70F;
            this.cantidad_revision.HeaderText = "Cant. Rev.";
            this.cantidad_revision.Name = "cantidad_revision";
            this.cantidad_revision.ReadOnly = true;
            // 
            // diferencia
            // 
            this.diferencia.DataPropertyName = "diferencia";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Red;
            this.diferencia.DefaultCellStyle = dataGridViewCellStyle7;
            this.diferencia.FillWeight = 70F;
            this.diferencia.HeaderText = "Dif.";
            this.diferencia.Name = "diferencia";
            this.diferencia.ReadOnly = true;
            // 
            // c_conflicto
            // 
            this.c_conflicto.FillWeight = 150F;
            this.c_conflicto.HeaderText = "Conflicto";
            this.c_conflicto.Name = "c_conflicto";
            this.c_conflicto.ReadOnly = true;
            // 
            // Revision_productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 415);
            this.Controls.Add(this.dgv_productos);
            this.Controls.Add(this.btn_finalizar);
            this.Controls.Add(this.chb_acepto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Revision_productos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos con conflicto";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Revision_productos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chb_acepto;
		private System.Windows.Forms.Button btn_finalizar;
		private System.Windows.Forms.DataGridView dgv_productos;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad_revision;
		private System.Windows.Forms.DataGridViewTextBoxColumn diferencia;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_conflicto;
	}
}