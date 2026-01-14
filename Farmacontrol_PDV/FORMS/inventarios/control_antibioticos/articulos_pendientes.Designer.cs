namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class articulos_pendientes
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
            this.dgv_control_AB = new System.Windows.Forms.DataGridView();
            this.por_ajustar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clase_antibiotico_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cedula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control_antibiotico_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_control_AB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_control_AB
            // 
            this.dgv_control_AB.AllowUserToAddRows = false;
            this.dgv_control_AB.AllowUserToDeleteRows = false;
            this.dgv_control_AB.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_control_AB.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_control_AB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_control_AB.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_control_AB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_control_AB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_control_AB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.por_ajustar,
            this.clase_antibiotico_id,
            this.cantidad,
            this.doctor,
            this.cedula,
            this.receta,
            this.control_antibiotico_id,
            this.articulo_id,
            this.amecop,
            this.nombre});
            this.dgv_control_AB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_control_AB.Location = new System.Drawing.Point(0, 0);
            this.dgv_control_AB.MultiSelect = false;
            this.dgv_control_AB.Name = "dgv_control_AB";
            this.dgv_control_AB.ReadOnly = true;
            this.dgv_control_AB.RowHeadersVisible = false;
            this.dgv_control_AB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_control_AB.Size = new System.Drawing.Size(359, 483);
            this.dgv_control_AB.TabIndex = 163;
            this.dgv_control_AB.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_control_AB_CellContentClick);
            this.dgv_control_AB.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_control_AB_CellContentDoubleClick);
            this.dgv_control_AB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_control_AB_KeyDown);
            // 
            // por_ajustar
            // 
            this.por_ajustar.DataPropertyName = "por_ajustar";
            this.por_ajustar.HeaderText = "por_ajustar";
            this.por_ajustar.Name = "por_ajustar";
            this.por_ajustar.ReadOnly = true;
            this.por_ajustar.Visible = false;
            // 
            // clase_antibiotico_id
            // 
            this.clase_antibiotico_id.DataPropertyName = "clase_antibiotico_id";
            this.clase_antibiotico_id.HeaderText = "clase_antibiotico_id";
            this.clase_antibiotico_id.Name = "clase_antibiotico_id";
            this.clase_antibiotico_id.ReadOnly = true;
            this.clase_antibiotico_id.Visible = false;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            this.cantidad.HeaderText = "cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Visible = false;
            // 
            // doctor
            // 
            this.doctor.DataPropertyName = "doctor";
            this.doctor.HeaderText = "doctor";
            this.doctor.Name = "doctor";
            this.doctor.ReadOnly = true;
            this.doctor.Visible = false;
            // 
            // cedula
            // 
            this.cedula.DataPropertyName = "cedula";
            this.cedula.HeaderText = "cedula";
            this.cedula.Name = "cedula";
            this.cedula.ReadOnly = true;
            this.cedula.Visible = false;
            // 
            // receta
            // 
            this.receta.DataPropertyName = "receta";
            this.receta.HeaderText = "receta";
            this.receta.Name = "receta";
            this.receta.ReadOnly = true;
            this.receta.Visible = false;
            // 
            // control_antibiotico_id
            // 
            this.control_antibiotico_id.DataPropertyName = "control_antibiotico_id";
            this.control_antibiotico_id.HeaderText = "control_antibiotico_id";
            this.control_antibiotico_id.Name = "control_antibiotico_id";
            this.control_antibiotico_id.ReadOnly = true;
            this.control_antibiotico_id.Visible = false;
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
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            this.amecop.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 180F;
            this.nombre.HeaderText = "Producto";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // articulos_pendientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 483);
            this.Controls.Add(this.dgv_control_AB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "articulos_pendientes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Articulos Pendientes";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_control_AB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_control_AB;
        private System.Windows.Forms.DataGridViewTextBoxColumn por_ajustar;
        private System.Windows.Forms.DataGridViewTextBoxColumn clase_antibiotico_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn doctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cedula;
        private System.Windows.Forms.DataGridViewTextBoxColumn receta;
        private System.Windows.Forms.DataGridViewTextBoxColumn control_antibiotico_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;

    }
}