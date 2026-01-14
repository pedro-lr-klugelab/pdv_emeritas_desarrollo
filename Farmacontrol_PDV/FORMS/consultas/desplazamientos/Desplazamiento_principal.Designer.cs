namespace Farmacontrol_PDV.FORMS.consultas.desplazamientos
{
	partial class Desplazamiento_principal
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
            this.dtp_inicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_final = new System.Windows.Forms.DateTimePicker();
            this.cbb_sucursales = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.dgv_desplazamientos = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ventas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prox_cad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_listado = new System.Windows.Forms.Button();
            this.cbb_sectores = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_limpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_desplazamientos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp_inicial
            // 
            this.dtp_inicial.Location = new System.Drawing.Point(96, 12);
            this.dtp_inicial.Name = "dtp_inicial";
            this.dtp_inicial.Size = new System.Drawing.Size(200, 20);
            this.dtp_inicial.TabIndex = 0;
            this.dtp_inicial.CloseUp += new System.EventHandler(this.dtp_inicial_CloseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha de Inicio:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha de fin:";
            // 
            // dtp_final
            // 
            this.dtp_final.Location = new System.Drawing.Point(397, 12);
            this.dtp_final.Name = "dtp_final";
            this.dtp_final.Size = new System.Drawing.Size(200, 20);
            this.dtp_final.TabIndex = 2;
            this.dtp_final.CloseUp += new System.EventHandler(this.dtp_final_CloseUp);
            // 
            // cbb_sucursales
            // 
            this.cbb_sucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_sucursales.FormattingEnabled = true;
            this.cbb_sucursales.Location = new System.Drawing.Point(660, 11);
            this.cbb_sucursales.Name = "cbb_sucursales";
            this.cbb_sucursales.Size = new System.Drawing.Size(338, 21);
            this.cbb_sucursales.TabIndex = 4;
            this.cbb_sucursales.SelectionChangeCommitted += new System.EventHandler(this.cbb_sucursales_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(603, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sucursal:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Amecop:";
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(96, 38);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(146, 20);
            this.txt_amecop.TabIndex = 7;
            //this.txt_amecop.TextChanged += new System.EventHandler(this.txt_amecop_TextChanged);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            this.txt_amecop.MouseEnter += new System.EventHandler(this.txt_amecop_MouseEnter);
            // 
            // dgv_desplazamientos
            // 
            this.dgv_desplazamientos.AllowUserToAddRows = false;
            this.dgv_desplazamientos.AllowUserToDeleteRows = false;
            this.dgv_desplazamientos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_desplazamientos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_desplazamientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_desplazamientos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_desplazamientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_desplazamientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_desplazamientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.amecop,
            this.producto,
            this.existencia,
            this.ventas,
            this.prox_cad});
            this.dgv_desplazamientos.Location = new System.Drawing.Point(12, 64);
            this.dgv_desplazamientos.MultiSelect = false;
            this.dgv_desplazamientos.Name = "dgv_desplazamientos";
            this.dgv_desplazamientos.ReadOnly = true;
            this.dgv_desplazamientos.RowHeadersVisible = false;
            this.dgv_desplazamientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_desplazamientos.Size = new System.Drawing.Size(986, 448);
            this.dgv_desplazamientos.TabIndex = 8;
            this.dgv_desplazamientos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_desplazamientos_KeyDown);
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
            this.amecop.FillWeight = 80F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 300F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // existencia
            // 
            this.existencia.DataPropertyName = "existencia";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia.DefaultCellStyle = dataGridViewCellStyle3;
            this.existencia.HeaderText = "Existencia";
            this.existencia.Name = "existencia";
            this.existencia.ReadOnly = true;
            // 
            // ventas
            // 
            this.ventas.DataPropertyName = "ventas";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ventas.DefaultCellStyle = dataGridViewCellStyle4;
            this.ventas.HeaderText = "Ventas";
            this.ventas.Name = "ventas";
            this.ventas.ReadOnly = true;
            // 
            // prox_cad
            // 
            this.prox_cad.DataPropertyName = "prox_cd";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.prox_cad.DefaultCellStyle = dataGridViewCellStyle5;
            this.prox_cad.HeaderText = "Prox. Cad.";
            this.prox_cad.Name = "prox_cad";
            this.prox_cad.ReadOnly = true;
            // 
            // btn_listado
            // 
            this.btn_listado.Location = new System.Drawing.Point(606, 36);
            this.btn_listado.Name = "btn_listado";
            this.btn_listado.Size = new System.Drawing.Size(108, 23);
            this.btn_listado.TabIndex = 9;
            this.btn_listado.Text = "Cargar sector";
            this.btn_listado.UseVisualStyleBackColor = true;
            this.btn_listado.Click += new System.EventHandler(this.btn_listado_Click);
            // 
            // cbb_sectores
            // 
            this.cbb_sectores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_sectores.FormattingEnabled = true;
            this.cbb_sectores.Location = new System.Drawing.Point(306, 38);
            this.cbb_sectores.Name = "cbb_sectores";
            this.cbb_sectores.Size = new System.Drawing.Size(291, 21);
            this.cbb_sectores.TabIndex = 11;
            this.cbb_sectores.SelectionChangeCommitted += new System.EventHandler(this.cbb_sectores_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Sectores:";
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.Location = new System.Drawing.Point(720, 36);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(75, 23);
            this.btn_limpiar.TabIndex = 13;
            this.btn_limpiar.Text = "Limpiar";
            this.btn_limpiar.UseVisualStyleBackColor = true;
            this.btn_limpiar.Click += new System.EventHandler(this.btn_limpiar_Click);
            // 
            // Desplazamiento_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 524);
            this.Controls.Add(this.btn_limpiar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbb_sectores);
            this.Controls.Add(this.btn_listado);
            this.Controls.Add(this.dgv_desplazamientos);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbb_sucursales);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtp_final);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_inicial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Desplazamiento_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desplazamientos";
            this.Shown += new System.EventHandler(this.Desplazamiento_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_desplazamientos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dtp_inicial;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtp_final;
		private System.Windows.Forms.ComboBox cbb_sucursales;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.DataGridView dgv_desplazamientos;
		private System.Windows.Forms.Button btn_listado;
		private System.Windows.Forms.ComboBox cbb_sectores;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btn_limpiar;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia;
		private System.Windows.Forms.DataGridViewTextBoxColumn ventas;
		private System.Windows.Forms.DataGridViewTextBoxColumn prox_cad;
	}
}