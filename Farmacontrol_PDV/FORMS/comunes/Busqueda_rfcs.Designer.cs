namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Busqueda_rfcs
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
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.txt_rfc = new System.Windows.Forms.TextBox();
            this.dgv_rfc = new System.Windows.Forms.DataGridView();
            this.rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razon_social = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correo_electronico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columna_rfc_registro_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_registrar_nuevo_rfc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rfc)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_nombre
            // 
            this.lbl_nombre.AutoSize = true;
            this.lbl_nombre.Location = new System.Drawing.Point(13, 13);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(47, 13);
            this.lbl_nombre.TabIndex = 0;
            this.lbl_nombre.Text = "Nombre:";
            // 
            // txt_rfc
            // 
            this.txt_rfc.Location = new System.Drawing.Point(66, 10);
            this.txt_rfc.Name = "txt_rfc";
            this.txt_rfc.Size = new System.Drawing.Size(608, 20);
            this.txt_rfc.TabIndex = 1;
            this.txt_rfc.Enter += new System.EventHandler(this.txt_rfc_Enter);
            this.txt_rfc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_rfc_KeyDown);
            // 
            // dgv_rfc
            // 
            this.dgv_rfc.AllowUserToAddRows = false;
            this.dgv_rfc.AllowUserToDeleteRows = false;
            this.dgv_rfc.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_rfc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_rfc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_rfc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rfc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rfc,
            this.razon_social,
            this.direccion,
            this.correo_electronico,
            this.columna_rfc_registro_id});
            this.dgv_rfc.Location = new System.Drawing.Point(12, 36);
            this.dgv_rfc.Name = "dgv_rfc";
            this.dgv_rfc.ReadOnly = true;
            this.dgv_rfc.RowHeadersVisible = false;
            this.dgv_rfc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rfc.Size = new System.Drawing.Size(904, 310);
            this.dgv_rfc.TabIndex = 2;
            this.dgv_rfc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_rfc_KeyDown);
            // 
            // rfc
            // 
            this.rfc.DataPropertyName = "rfc";
            this.rfc.HeaderText = "RFC";
            this.rfc.Name = "rfc";
            this.rfc.ReadOnly = true;
            // 
            // razon_social
            // 
            this.razon_social.DataPropertyName = "razon_social";
            this.razon_social.FillWeight = 250F;
            this.razon_social.HeaderText = "Razon social";
            this.razon_social.Name = "razon_social";
            this.razon_social.ReadOnly = true;
            // 
            // direccion
            // 
            this.direccion.DataPropertyName = "direccion";
            this.direccion.FillWeight = 200F;
            this.direccion.HeaderText = "Direccion";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            // 
            // correo_electronico
            // 
            this.correo_electronico.DataPropertyName = "correo_electronico";
            this.correo_electronico.FillWeight = 150F;
            this.correo_electronico.HeaderText = "Correo electronico";
            this.correo_electronico.Name = "correo_electronico";
            this.correo_electronico.ReadOnly = true;
            // 
            // columna_rfc_registro_id
            // 
            this.columna_rfc_registro_id.DataPropertyName = "rfc_registro_id";
            this.columna_rfc_registro_id.HeaderText = "columna_rfc_registro_id";
            this.columna_rfc_registro_id.Name = "columna_rfc_registro_id";
            this.columna_rfc_registro_id.ReadOnly = true;
            this.columna_rfc_registro_id.Visible = false;
            // 
            // btn_registrar_nuevo_rfc
            // 
            this.btn_registrar_nuevo_rfc.Location = new System.Drawing.Point(785, 8);
            this.btn_registrar_nuevo_rfc.Name = "btn_registrar_nuevo_rfc";
            this.btn_registrar_nuevo_rfc.Size = new System.Drawing.Size(131, 23);
            this.btn_registrar_nuevo_rfc.TabIndex = 3;
            this.btn_registrar_nuevo_rfc.Text = "Registrar Nuevo RFC";
            this.btn_registrar_nuevo_rfc.UseVisualStyleBackColor = true;
            this.btn_registrar_nuevo_rfc.Click += new System.EventHandler(this.btn_registrar_nuevo_rfc_Click);
            // 
            // Busqueda_rfcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 358);
            this.ControlBox = false;
            this.Controls.Add(this.btn_registrar_nuevo_rfc);
            this.Controls.Add(this.dgv_rfc);
            this.Controls.Add(this.txt_rfc);
            this.Controls.Add(this.lbl_nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Busqueda_rfcs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Busqueda RFC";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rfc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_nombre;
		private System.Windows.Forms.TextBox txt_rfc;
		private System.Windows.Forms.DataGridView dgv_rfc;
		private System.Windows.Forms.DataGridViewTextBoxColumn rfc;
		private System.Windows.Forms.DataGridViewTextBoxColumn razon_social;
		private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
		private System.Windows.Forms.DataGridViewTextBoxColumn correo_electronico;
		private System.Windows.Forms.DataGridViewTextBoxColumn columna_rfc_registro_id;
        private System.Windows.Forms.Button btn_registrar_nuevo_rfc;
	}
}