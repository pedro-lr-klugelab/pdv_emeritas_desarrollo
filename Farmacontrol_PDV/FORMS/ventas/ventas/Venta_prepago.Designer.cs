namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	partial class Venta_prepago
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
            this.dgv_venta_previa = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chb_confirm = new System.Windows.Forms.CheckBox();
            this.btn_procesar_prepago = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_venta_previa)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_venta_previa
            // 
            this.dgv_venta_previa.AllowUserToAddRows = false;
            this.dgv_venta_previa.AllowUserToDeleteRows = false;
            this.dgv_venta_previa.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_venta_previa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_venta_previa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_venta_previa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_venta_previa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_venta_previa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.amecop,
            this.producto,
            this.caducidad,
            this.lote,
            this.cantidad});
            this.dgv_venta_previa.Enabled = false;
            this.dgv_venta_previa.Location = new System.Drawing.Point(12, 12);
            this.dgv_venta_previa.MultiSelect = false;
            this.dgv_venta_previa.Name = "dgv_venta_previa";
            this.dgv_venta_previa.ReadOnly = true;
            this.dgv_venta_previa.RowHeadersVisible = false;
            this.dgv_venta_previa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_venta_previa.Size = new System.Drawing.Size(999, 308);
            this.dgv_venta_previa.TabIndex = 0;
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
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 200F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 50F;
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
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // chb_confirm
            // 
            this.chb_confirm.AutoSize = true;
            this.chb_confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_confirm.ForeColor = System.Drawing.Color.Red;
            this.chb_confirm.Location = new System.Drawing.Point(12, 326);
            this.chb_confirm.Name = "chb_confirm";
            this.chb_confirm.Size = new System.Drawing.Size(560, 17);
            this.chb_confirm.TabIndex = 1;
            this.chb_confirm.Text = "He verificado que cuento con todos los productos y deseo continuar con el canje d" +
    "el prepago";
            this.chb_confirm.UseVisualStyleBackColor = true;
            this.chb_confirm.CheckedChanged += new System.EventHandler(this.chb_confirm_CheckedChanged);
            // 
            // btn_procesar_prepago
            // 
            this.btn_procesar_prepago.Location = new System.Drawing.Point(896, 339);
            this.btn_procesar_prepago.Name = "btn_procesar_prepago";
            this.btn_procesar_prepago.Size = new System.Drawing.Size(115, 23);
            this.btn_procesar_prepago.TabIndex = 2;
            this.btn_procesar_prepago.Text = "Procesar Prepago";
            this.btn_procesar_prepago.UseVisualStyleBackColor = true;
            this.btn_procesar_prepago.Click += new System.EventHandler(this.btn_procesar_prepago_Click);
            // 
            // Venta_prepago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 374);
            this.Controls.Add(this.btn_procesar_prepago);
            this.Controls.Add(this.chb_confirm);
            this.Controls.Add(this.dgv_venta_previa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Venta_prepago";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información productos";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Venta_prepago_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_venta_previa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_venta_previa;
		private System.Windows.Forms.CheckBox chb_confirm;
		private System.Windows.Forms.Button btn_procesar_prepago;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
	}
}