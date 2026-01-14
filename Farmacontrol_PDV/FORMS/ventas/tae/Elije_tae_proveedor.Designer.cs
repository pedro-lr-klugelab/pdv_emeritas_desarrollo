namespace Farmacontrol_PDV.FORMS.ventas.tae
{
    partial class Elije_tae_proveedor
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
            this.dgv_prov_tae = new System.Windows.Forms.DataGridView();
            this.fabricante_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_prov_tae)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_prov_tae
            // 
            this.dgv_prov_tae.AllowUserToAddRows = false;
            this.dgv_prov_tae.AllowUserToDeleteRows = false;
            this.dgv_prov_tae.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_prov_tae.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_prov_tae.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_prov_tae.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_prov_tae.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_prov_tae.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fabricante_id,
            this.nombre});
            this.dgv_prov_tae.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_prov_tae.Location = new System.Drawing.Point(0, 0);
            this.dgv_prov_tae.MultiSelect = false;
            this.dgv_prov_tae.Name = "dgv_prov_tae";
            this.dgv_prov_tae.ReadOnly = true;
            this.dgv_prov_tae.RowHeadersVisible = false;
            this.dgv_prov_tae.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_prov_tae.Size = new System.Drawing.Size(265, 293);
            this.dgv_prov_tae.TabIndex = 0;
            this.dgv_prov_tae.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgv_prov_tae.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_prov_tae_KeyDown);
            // 
            // fabricante_id
            // 
            this.fabricante_id.DataPropertyName = "fabricante_id";
            this.fabricante_id.HeaderText = "fabricante_id";
            this.fabricante_id.Name = "fabricante_id";
            this.fabricante_id.ReadOnly = true;
            this.fabricante_id.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // Elije_tae_proveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 293);
            this.ControlBox = false;
            this.Controls.Add(this.dgv_prov_tae);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Elije_tae_proveedor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.elije_tae_proveedor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Elije_tae_proveedor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_prov_tae)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_prov_tae;
        private System.Windows.Forms.DataGridViewTextBoxColumn fabricante_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
    }
}