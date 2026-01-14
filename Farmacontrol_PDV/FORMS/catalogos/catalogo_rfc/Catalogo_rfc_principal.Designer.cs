namespace Farmacontrol_PDV.FORMS.catalogos.catalogo_rfc
{
	partial class Catalogo_rfc_principal
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_busqueda = new System.Windows.Forms.TextBox();
            this.dgv_rfc = new System.Windows.Forms.DataGridView();
            this.c_rfc_registro_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_razon_social = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_correo_electronico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_registrar_rfc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rfc)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RFC:";
            // 
            // txt_busqueda
            // 
            this.txt_busqueda.Location = new System.Drawing.Point(49, 6);
            this.txt_busqueda.Name = "txt_busqueda";
            this.txt_busqueda.Size = new System.Drawing.Size(302, 20);
            this.txt_busqueda.TabIndex = 1;
            this.txt_busqueda.Enter += new System.EventHandler(this.txt_busqueda_Enter);
            this.txt_busqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_busqueda_KeyDown);
            // 
            // dgv_rfc
            // 
            this.dgv_rfc.AllowUserToAddRows = false;
            this.dgv_rfc.AllowUserToDeleteRows = false;
            this.dgv_rfc.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_rfc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_rfc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_rfc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_rfc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rfc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_rfc_registro_id,
            this.c_rfc,
            this.c_razon_social,
            this.c_direccion,
            this.c_correo_electronico});
            this.dgv_rfc.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_rfc.Location = new System.Drawing.Point(12, 32);
            this.dgv_rfc.MultiSelect = false;
            this.dgv_rfc.Name = "dgv_rfc";
            this.dgv_rfc.ReadOnly = true;
            this.dgv_rfc.RowHeadersVisible = false;
            this.dgv_rfc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rfc.Size = new System.Drawing.Size(1085, 476);
            this.dgv_rfc.TabIndex = 2;
            this.dgv_rfc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_rfc_CellDoubleClick);
            this.dgv_rfc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_rfc_KeyDown);
            // 
            // c_rfc_registro_id
            // 
            this.c_rfc_registro_id.DataPropertyName = "rfc_registro_id";
            this.c_rfc_registro_id.HeaderText = "c_rfc_registro_id";
            this.c_rfc_registro_id.Name = "c_rfc_registro_id";
            this.c_rfc_registro_id.ReadOnly = true;
            this.c_rfc_registro_id.Visible = false;
            // 
            // c_rfc
            // 
            this.c_rfc.DataPropertyName = "rfc";
            this.c_rfc.FillWeight = 80F;
            this.c_rfc.HeaderText = "RFC";
            this.c_rfc.Name = "c_rfc";
            this.c_rfc.ReadOnly = true;
            // 
            // c_razon_social
            // 
            this.c_razon_social.DataPropertyName = "razon_social";
            this.c_razon_social.FillWeight = 250F;
            this.c_razon_social.HeaderText = "Razon Social";
            this.c_razon_social.Name = "c_razon_social";
            this.c_razon_social.ReadOnly = true;
            // 
            // c_direccion
            // 
            this.c_direccion.DataPropertyName = "direccion";
            this.c_direccion.FillWeight = 150F;
            this.c_direccion.HeaderText = "Direccion";
            this.c_direccion.Name = "c_direccion";
            this.c_direccion.ReadOnly = true;
            // 
            // c_correo_electronico
            // 
            this.c_correo_electronico.DataPropertyName = "correo_electronico";
            this.c_correo_electronico.FillWeight = 150F;
            this.c_correo_electronico.HeaderText = "Correo Electronico";
            this.c_correo_electronico.Name = "c_correo_electronico";
            this.c_correo_electronico.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verFacturasToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
            // 
            // verFacturasToolStripMenuItem
            // 
            this.verFacturasToolStripMenuItem.Name = "verFacturasToolStripMenuItem";
            this.verFacturasToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.verFacturasToolStripMenuItem.Text = "Ver facturas";
            this.verFacturasToolStripMenuItem.Click += new System.EventHandler(this.verFacturasToolStripMenuItem_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(1022, 514);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_registrar_rfc
            // 
            this.btn_registrar_rfc.Location = new System.Drawing.Point(912, 514);
            this.btn_registrar_rfc.Name = "btn_registrar_rfc";
            this.btn_registrar_rfc.Size = new System.Drawing.Size(104, 23);
            this.btn_registrar_rfc.TabIndex = 4;
            this.btn_registrar_rfc.Text = "Registrar RFC";
            this.btn_registrar_rfc.UseVisualStyleBackColor = true;
            this.btn_registrar_rfc.Click += new System.EventHandler(this.btn_registrar_rfc_Click);
            // 
            // Catalogo_rfc_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 549);
            this.Controls.Add(this.btn_registrar_rfc);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.dgv_rfc);
            this.Controls.Add(this.txt_busqueda);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Catalogo_rfc_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catalogo RFC";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rfc)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_busqueda;
		private System.Windows.Forms.DataGridView dgv_rfc;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.Button btn_registrar_rfc;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_rfc_registro_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_rfc;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_razon_social;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_direccion;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_correo_electronico;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem verFacturasToolStripMenuItem;
	}
}