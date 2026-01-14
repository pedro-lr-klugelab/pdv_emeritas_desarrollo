namespace Farmacontrol_PDV.FORMS.consultas.faltantes
{
	partial class Faltantes_sucursal
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
            this.btn_imprimir_ticket = new System.Windows.Forms.Button();
            this.btn_reporte_completo = new System.Windows.Forms.Button();
            this.dgv_sucursales = new System.Windows.Forms.DataGridView();
            this.sucursal_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.save_dialog_faltantes = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sucursales)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_imprimir_ticket
            // 
            this.btn_imprimir_ticket.Location = new System.Drawing.Point(373, 12);
            this.btn_imprimir_ticket.Name = "btn_imprimir_ticket";
            this.btn_imprimir_ticket.Size = new System.Drawing.Size(145, 23);
            this.btn_imprimir_ticket.TabIndex = 1;
            this.btn_imprimir_ticket.Text = "Imprimir ticket de surtido";
            this.btn_imprimir_ticket.UseVisualStyleBackColor = true;
            this.btn_imprimir_ticket.Click += new System.EventHandler(this.btn_imprimir_ticket_Click);
            // 
            // btn_reporte_completo
            // 
            this.btn_reporte_completo.Location = new System.Drawing.Point(373, 41);
            this.btn_reporte_completo.Name = "btn_reporte_completo";
            this.btn_reporte_completo.Size = new System.Drawing.Size(145, 23);
            this.btn_reporte_completo.TabIndex = 2;
            this.btn_reporte_completo.Text = "Generar Reporte Completo";
            this.btn_reporte_completo.UseVisualStyleBackColor = true;
            this.btn_reporte_completo.Click += new System.EventHandler(this.btn_reporte_completo_Click);
            // 
            // dgv_sucursales
            // 
            this.dgv_sucursales.AllowUserToAddRows = false;
            this.dgv_sucursales.AllowUserToDeleteRows = false;
            this.dgv_sucursales.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_sucursales.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_sucursales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_sucursales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_sucursales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_sucursales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sucursal_id,
            this.sucursal});
            this.dgv_sucursales.Location = new System.Drawing.Point(12, 12);
            this.dgv_sucursales.MultiSelect = false;
            this.dgv_sucursales.Name = "dgv_sucursales";
            this.dgv_sucursales.ReadOnly = true;
            this.dgv_sucursales.RowHeadersVisible = false;
            this.dgv_sucursales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_sucursales.Size = new System.Drawing.Size(355, 431);
            this.dgv_sucursales.TabIndex = 3;
            // 
            // sucursal_id
            // 
            this.sucursal_id.DataPropertyName = "sucursal_id";
            this.sucursal_id.HeaderText = "sucursal_id";
            this.sucursal_id.Name = "sucursal_id";
            this.sucursal_id.ReadOnly = true;
            this.sucursal_id.Visible = false;
            // 
            // sucursal
            // 
            this.sucursal.DataPropertyName = "nombre";
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            // 
            // save_dialog_faltantes
            // 
            this.save_dialog_faltantes.FileName = "faltantes";
            // 
            // Faltantes_sucursal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 455);
            this.Controls.Add(this.dgv_sucursales);
            this.Controls.Add(this.btn_reporte_completo);
            this.Controls.Add(this.btn_imprimir_ticket);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Faltantes_sucursal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sucursales";
            this.Shown += new System.EventHandler(this.Faltantes_sucursal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sucursales)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_imprimir_ticket;
		private System.Windows.Forms.Button btn_reporte_completo;
		private System.Windows.Forms.DataGridView dgv_sucursales;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
		private System.Windows.Forms.SaveFileDialog save_dialog_faltantes;
	}
}