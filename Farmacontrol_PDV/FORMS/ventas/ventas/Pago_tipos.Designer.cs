namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	partial class Pago_tipos
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
            this.lbl_cuenta = new System.Windows.Forms.Label();
            this.txt_cuenta = new System.Windows.Forms.TextBox();
            this.dgv_pagos = new System.Windows.Forms.DataGridView();
            this.metodo_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_terminar_venta = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.lbl_cantidad = new System.Windows.Forms.Label();
            this.txt_cantidad = new System.Windows.Forms.TextBox();
            this.lbl_total = new System.Windows.Forms.Label();
            this.lbl_total_pagar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_monto_total = new System.Windows.Forms.Label();
            this.btn_cambiar_metodo_pago = new System.Windows.Forms.Button();
            this.lbl_metodo_pago = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pagos)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_cuenta
            // 
            this.lbl_cuenta.AutoSize = true;
            this.lbl_cuenta.Location = new System.Drawing.Point(274, 36);
            this.lbl_cuenta.Name = "lbl_cuenta";
            this.lbl_cuenta.Size = new System.Drawing.Size(74, 13);
            this.lbl_cuenta.TabIndex = 2;
            this.lbl_cuenta.Text = "Cuenta/Folio: ";
            // 
            // txt_cuenta
            // 
            this.txt_cuenta.Location = new System.Drawing.Point(348, 33);
            this.txt_cuenta.Name = "txt_cuenta";
            this.txt_cuenta.Size = new System.Drawing.Size(121, 20);
            this.txt_cuenta.TabIndex = 2;
            this.txt_cuenta.TextChanged += new System.EventHandler(this.txt_cuenta_TextChanged);
            this.txt_cuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cuenta_KeyDown);
            // 
            // dgv_pagos
            // 
            this.dgv_pagos.AllowUserToAddRows = false;
            this.dgv_pagos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_pagos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_pagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_pagos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_pagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.metodo_pago,
            this.cuenta,
            this.importe,
            this.monto});
            this.dgv_pagos.Location = new System.Drawing.Point(12, 85);
            this.dgv_pagos.Name = "dgv_pagos";
            this.dgv_pagos.ReadOnly = true;
            this.dgv_pagos.RowHeadersVisible = false;
            this.dgv_pagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pagos.Size = new System.Drawing.Size(457, 181);
            this.dgv_pagos.TabIndex = 4;
            this.dgv_pagos.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_pagos_RowsRemoved);
            this.dgv_pagos.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_pagos_UserDeletedRow);
            this.dgv_pagos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_pagos_KeyDown);
            // 
            // metodo_pago
            // 
            this.metodo_pago.FillWeight = 150F;
            this.metodo_pago.HeaderText = "Metodo de Pago";
            this.metodo_pago.Name = "metodo_pago";
            this.metodo_pago.ReadOnly = true;
            // 
            // cuenta
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cuenta.DefaultCellStyle = dataGridViewCellStyle3;
            this.cuenta.FillWeight = 120F;
            this.cuenta.HeaderText = "Cuenta/Folio";
            this.cuenta.Name = "cuenta";
            this.cuenta.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.HeaderText = "importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            this.importe.Visible = false;
            // 
            // monto
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.monto.DefaultCellStyle = dataGridViewCellStyle4;
            this.monto.HeaderText = "Monto";
            this.monto.Name = "monto";
            this.monto.ReadOnly = true;
            // 
            // btn_terminar_venta
            // 
            this.btn_terminar_venta.Enabled = false;
            this.btn_terminar_venta.Location = new System.Drawing.Point(275, 310);
            this.btn_terminar_venta.Name = "btn_terminar_venta";
            this.btn_terminar_venta.Size = new System.Drawing.Size(111, 23);
            this.btn_terminar_venta.TabIndex = 4;
            this.btn_terminar_venta.Text = "Terminar Venta";
            this.btn_terminar_venta.UseVisualStyleBackColor = true;
            this.btn_terminar_venta.Click += new System.EventHandler(this.btn_terminar_venta_Click);
            this.btn_terminar_venta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_terminar_venta_KeyDown);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(394, 310);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 5;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // lbl_cantidad
            // 
            this.lbl_cantidad.AutoSize = true;
            this.lbl_cantidad.Location = new System.Drawing.Point(290, 62);
            this.lbl_cantidad.Name = "lbl_cantidad";
            this.lbl_cantidad.Size = new System.Drawing.Size(52, 13);
            this.lbl_cantidad.TabIndex = 7;
            this.lbl_cantidad.Text = "Cantidad:";
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.Location = new System.Drawing.Point(349, 59);
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(120, 20);
            this.txt_cantidad.TabIndex = 3;
            this.txt_cantidad.TextChanged += new System.EventHandler(this.txt_cantidad_TextChanged);
            this.txt_cantidad.Enter += new System.EventHandler(this.txt_cantidad_Enter);
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.Location = new System.Drawing.Point(12, 9);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(73, 13);
            this.lbl_total.TabIndex = 9;
            this.lbl_total.Text = "Total a pagar:";
            // 
            // lbl_total_pagar
            // 
            this.lbl_total_pagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_pagar.ForeColor = System.Drawing.Color.Red;
            this.lbl_total_pagar.Location = new System.Drawing.Point(15, 33);
            this.lbl_total_pagar.Name = "lbl_total_pagar";
            this.lbl_total_pagar.Size = new System.Drawing.Size(253, 42);
            this.lbl_total_pagar.TabIndex = 10;
            this.lbl_total_pagar.Text = "$9,999,999.99";
            this.lbl_total_pagar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Monto total:";
            // 
            // lbl_monto_total
            // 
            this.lbl_monto_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_monto_total.ForeColor = System.Drawing.Color.Red;
            this.lbl_monto_total.Location = new System.Drawing.Point(322, 269);
            this.lbl_monto_total.Name = "lbl_monto_total";
            this.lbl_monto_total.Size = new System.Drawing.Size(147, 23);
            this.lbl_monto_total.TabIndex = 13;
            this.lbl_monto_total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_cambiar_metodo_pago
            // 
            this.btn_cambiar_metodo_pago.Location = new System.Drawing.Point(266, 4);
            this.btn_cambiar_metodo_pago.Name = "btn_cambiar_metodo_pago";
            this.btn_cambiar_metodo_pago.Size = new System.Drawing.Size(75, 23);
            this.btn_cambiar_metodo_pago.TabIndex = 14;
            this.btn_cambiar_metodo_pago.Text = "Cambiar";
            this.btn_cambiar_metodo_pago.UseVisualStyleBackColor = true;
            this.btn_cambiar_metodo_pago.Click += new System.EventHandler(this.btn_cambiar_metodo_pago_Click);
            // 
            // lbl_metodo_pago
            // 
            this.lbl_metodo_pago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_metodo_pago.Location = new System.Drawing.Point(340, 4);
            this.lbl_metodo_pago.Name = "lbl_metodo_pago";
            this.lbl_metodo_pago.Size = new System.Drawing.Size(143, 23);
            this.lbl_metodo_pago.TabIndex = 15;
            this.lbl_metodo_pago.Text = "METODO DE PAGO";
            this.lbl_metodo_pago.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pago_tipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(485, 348);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_metodo_pago);
            this.Controls.Add(this.btn_cambiar_metodo_pago);
            this.Controls.Add(this.lbl_monto_total);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_total_pagar);
            this.Controls.Add(this.lbl_total);
            this.Controls.Add(this.txt_cantidad);
            this.Controls.Add(this.lbl_cantidad);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_terminar_venta);
            this.Controls.Add(this.dgv_pagos);
            this.Controls.Add(this.txt_cuenta);
            this.Controls.Add(this.lbl_cuenta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Pago_tipos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Pago_tipos_Load);
            this.Shown += new System.EventHandler(this.Pago_tipos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pagos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label lbl_cuenta;
		private System.Windows.Forms.TextBox txt_cuenta;
		private System.Windows.Forms.DataGridView dgv_pagos;
		private System.Windows.Forms.Button btn_terminar_venta;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.Label lbl_cantidad;
		private System.Windows.Forms.TextBox txt_cantidad;
		private System.Windows.Forms.Label lbl_total;
		private System.Windows.Forms.Label lbl_total_pagar;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbl_monto_total;
        private System.Windows.Forms.Button btn_cambiar_metodo_pago;
        private System.Windows.Forms.Label lbl_metodo_pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn metodo_pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto;

	}
}