namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Busqueda_codigos_postales
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
            this.txt_codigo_postal = new System.Windows.Forms.TextBox();
            this.dgv_codigos_postales = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_postal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asentamiento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.municipio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ciudad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_codigos_postales)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo Postal:";
            // 
            // txt_codigo_postal
            // 
            this.txt_codigo_postal.Location = new System.Drawing.Point(94, 10);
            this.txt_codigo_postal.Name = "txt_codigo_postal";
            this.txt_codigo_postal.Size = new System.Drawing.Size(141, 20);
            this.txt_codigo_postal.TabIndex = 1;
            this.txt_codigo_postal.Enter += new System.EventHandler(this.txt_codigo_postal_Enter);
            this.txt_codigo_postal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_codigo_postal_KeyDown);
            // 
            // dgv_codigos_postales
            // 
            this.dgv_codigos_postales.AllowUserToAddRows = false;
            this.dgv_codigos_postales.AllowUserToDeleteRows = false;
            this.dgv_codigos_postales.AllowUserToResizeRows = false;
            this.dgv_codigos_postales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_codigos_postales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_codigos_postales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_codigos_postales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.codigo_postal,
            this.asentamiento_id,
            this.municipio,
            this.ciudad,
            this.estado});
            this.dgv_codigos_postales.Location = new System.Drawing.Point(12, 36);
            this.dgv_codigos_postales.MultiSelect = false;
            this.dgv_codigos_postales.Name = "dgv_codigos_postales";
            this.dgv_codigos_postales.ReadOnly = true;
            this.dgv_codigos_postales.RowHeadersVisible = false;
            this.dgv_codigos_postales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_codigos_postales.Size = new System.Drawing.Size(965, 387);
            this.dgv_codigos_postales.TabIndex = 2;
            this.dgv_codigos_postales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_codigos_postales_KeyDown);
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 250F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // codigo_postal
            // 
            this.codigo_postal.DataPropertyName = "codigo_postal";
            this.codigo_postal.HeaderText = "codigo_postal";
            this.codigo_postal.Name = "codigo_postal";
            this.codigo_postal.ReadOnly = true;
            this.codigo_postal.Visible = false;
            // 
            // asentamiento_id
            // 
            this.asentamiento_id.DataPropertyName = "asentamiento_id";
            this.asentamiento_id.HeaderText = "asentamiento_id";
            this.asentamiento_id.Name = "asentamiento_id";
            this.asentamiento_id.ReadOnly = true;
            this.asentamiento_id.Visible = false;
            // 
            // municipio
            // 
            this.municipio.DataPropertyName = "municipio";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.municipio.DefaultCellStyle = dataGridViewCellStyle2;
            this.municipio.FillWeight = 200F;
            this.municipio.HeaderText = "Municipio";
            this.municipio.Name = "municipio";
            this.municipio.ReadOnly = true;
            // 
            // ciudad
            // 
            this.ciudad.DataPropertyName = "ciudad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ciudad.DefaultCellStyle = dataGridViewCellStyle3;
            this.ciudad.FillWeight = 150F;
            this.ciudad.HeaderText = "Ciudad";
            this.ciudad.Name = "ciudad";
            this.ciudad.ReadOnly = true;
            // 
            // estado
            // 
            this.estado.DataPropertyName = "estado";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle4;
            this.estado.FillWeight = 150F;
            this.estado.HeaderText = "Estado";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // Busqueda_codigos_postales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 435);
            this.Controls.Add(this.dgv_codigos_postales);
            this.Controls.Add(this.txt_codigo_postal);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Busqueda_codigos_postales";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bsqueda de Codigos Postales";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_codigos_postales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_codigo_postal;
		private System.Windows.Forms.DataGridView dgv_codigos_postales;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigo_postal;
		private System.Windows.Forms.DataGridViewTextBoxColumn asentamiento_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn municipio;
		private System.Windows.Forms.DataGridViewTextBoxColumn ciudad;
		private System.Windows.Forms.DataGridViewTextBoxColumn estado;
	}
}