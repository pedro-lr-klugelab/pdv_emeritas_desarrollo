namespace Farmacontrol_PDV.FORMS.consultas.consulta_existencias
{
	partial class Consulta_existencias_caducidades
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
            this.dgv_caducidades = new System.Windows.Forms.DataGridView();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_caducidades)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_caducidades
            // 
            this.dgv_caducidades.AllowUserToAddRows = false;
            this.dgv_caducidades.AllowUserToDeleteRows = false;
            this.dgv_caducidades.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_caducidades.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_caducidades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_caducidades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_caducidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_caducidades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.caducidad,
            this.existencia});
            this.dgv_caducidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_caducidades.Location = new System.Drawing.Point(0, 0);
            this.dgv_caducidades.MultiSelect = false;
            this.dgv_caducidades.Name = "dgv_caducidades";
            this.dgv_caducidades.ReadOnly = true;
            this.dgv_caducidades.RowHeadersVisible = false;
            this.dgv_caducidades.Size = new System.Drawing.Size(227, 217);
            this.dgv_caducidades.TabIndex = 1;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // existencia
            // 
            this.existencia.DataPropertyName = "existencia";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia.DefaultCellStyle = dataGridViewCellStyle4;
            this.existencia.HeaderText = "Existencia";
            this.existencia.Name = "existencia";
            this.existencia.ReadOnly = true;
            // 
            // Consulta_existencias_caducidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 217);
            this.Controls.Add(this.dgv_caducidades);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Consulta_existencias_caducidades";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caducidades";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Consulta_existencias_caducidades_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_caducidades)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_caducidades;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn existencia;
	}
}