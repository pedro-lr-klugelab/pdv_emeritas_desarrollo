namespace Farmacontrol_PDV.FORMS.consultas.consulta_existencias
{
	partial class Consulta_existencias_sucursales
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pr_cd = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.existencia_vendible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.sucursal_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_devoluciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_mermas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_cambio_fisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_apartados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_traspasos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_total_global = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // pr_cd
            // 
            this.pr_cd.DataPropertyName = "pr_cd";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pr_cd.DefaultCellStyle = dataGridViewCellStyle1;
            this.pr_cd.HeaderText = "PR CD";
            this.pr_cd.Name = "pr_cd";
            this.pr_cd.ReadOnly = true;
            this.pr_cd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pr_cd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.pr_cd.TrackVisitedState = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Total global:";
            // 
            // txt_nombre
            // 
            this.txt_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombre.Location = new System.Drawing.Point(251, 7);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.ReadOnly = true;
            this.txt_nombre.Size = new System.Drawing.Size(543, 21);
            this.txt_nombre.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nombre:";
            // 
            // existencia_vendible
            // 
            this.existencia_vendible.DataPropertyName = "existencia_vendible";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "-";
            this.existencia_vendible.DefaultCellStyle = dataGridViewCellStyle2;
            this.existencia_vendible.FillWeight = 65F;
            this.existencia_vendible.HeaderText = "VEN";
            this.existencia_vendible.Name = "existencia_vendible";
            this.existencia_vendible.ReadOnly = true;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_amecop.Location = new System.Drawing.Point(67, 7);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.ReadOnly = true;
            this.txt_amecop.Size = new System.Drawing.Size(125, 21);
            this.txt_amecop.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Amecop:";
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_articulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sucursal_id,
            this.sucursal,
            this.total,
            this.existencia_devoluciones,
            this.existencia_mermas,
            this.existencia_cambio_fisico,
            this.existencia_apartados,
            this.existencia_traspasos,
            this.existencia_vendible,
            this.pr_cd});
            this.dgv_articulos.Location = new System.Drawing.Point(12, 34);
            this.dgv_articulos.MultiSelect = false;
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(782, 405);
            this.dgv_articulos.StandardTab = true;
            this.dgv_articulos.TabIndex = 10;
            this.dgv_articulos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_articulos_CellContentClick);
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
            this.sucursal.DataPropertyName = "sucursal";
            this.sucursal.FillWeight = 250F;
            this.sucursal.HeaderText = "Sucursal";
            this.sucursal.Name = "sucursal";
            this.sucursal.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "existencia_total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.total.DefaultCellStyle = dataGridViewCellStyle5;
            this.total.FillWeight = 65F;
            this.total.HeaderText = "TOT";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // existencia_devoluciones
            // 
            this.existencia_devoluciones.DataPropertyName = "existencia_devoluciones";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "-";
            this.existencia_devoluciones.DefaultCellStyle = dataGridViewCellStyle6;
            this.existencia_devoluciones.FillWeight = 65F;
            this.existencia_devoluciones.HeaderText = "DEV";
            this.existencia_devoluciones.Name = "existencia_devoluciones";
            this.existencia_devoluciones.ReadOnly = true;
            // 
            // existencia_mermas
            // 
            this.existencia_mermas.DataPropertyName = "existencia_mermas";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = "-";
            this.existencia_mermas.DefaultCellStyle = dataGridViewCellStyle7;
            this.existencia_mermas.FillWeight = 65F;
            this.existencia_mermas.HeaderText = "MER";
            this.existencia_mermas.Name = "existencia_mermas";
            this.existencia_mermas.ReadOnly = true;
            // 
            // existencia_cambio_fisico
            // 
            this.existencia_cambio_fisico.DataPropertyName = "existencia_cambio_fisico";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "-";
            this.existencia_cambio_fisico.DefaultCellStyle = dataGridViewCellStyle8;
            this.existencia_cambio_fisico.FillWeight = 65F;
            this.existencia_cambio_fisico.HeaderText = "CBF";
            this.existencia_cambio_fisico.Name = "existencia_cambio_fisico";
            this.existencia_cambio_fisico.ReadOnly = true;
            // 
            // existencia_apartados
            // 
            this.existencia_apartados.DataPropertyName = "existencia_apartados";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = "-";
            this.existencia_apartados.DefaultCellStyle = dataGridViewCellStyle9;
            this.existencia_apartados.FillWeight = 65F;
            this.existencia_apartados.HeaderText = "APT";
            this.existencia_apartados.Name = "existencia_apartados";
            this.existencia_apartados.ReadOnly = true;
            // 
            // existencia_traspasos
            // 
            this.existencia_traspasos.DataPropertyName = "existencia_traspasos";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.NullValue = "-";
            this.existencia_traspasos.DefaultCellStyle = dataGridViewCellStyle10;
            this.existencia_traspasos.FillWeight = 65F;
            this.existencia_traspasos.HeaderText = "TRA";
            this.existencia_traspasos.Name = "existencia_traspasos";
            this.existencia_traspasos.ReadOnly = true;
            // 
            // lbl_total_global
            // 
            this.lbl_total_global.AutoSize = true;
            this.lbl_total_global.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total_global.ForeColor = System.Drawing.Color.Red;
            this.lbl_total_global.Location = new System.Drawing.Point(83, 442);
            this.lbl_total_global.Name = "lbl_total_global";
            this.lbl_total_global.Size = new System.Drawing.Size(0, 13);
            this.lbl_total_global.TabIndex = 16;
            // 
            // Consulta_existencias_sucursales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 463);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_articulos);
            this.Controls.Add(this.lbl_total_global);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Consulta_existencias_sucursales";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Consulta_existencias_sucursales_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridViewLinkColumn pr_cd;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_nombre;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia_vendible;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal;
		private System.Windows.Forms.DataGridViewTextBoxColumn total;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia_devoluciones;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia_mermas;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia_cambio_fisico;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia_apartados;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia_traspasos;
		private System.Windows.Forms.Label lbl_total_global;
	}
}