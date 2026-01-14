namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Reporte_Movimientos
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
            this.dgv_reporte = new System.Windows.Forms.DataGridView();
            this.dt_fecha_inicial = new System.Windows.Forms.DateTimePicker();
            this.dt_fecha_final = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.codigos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.venta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control_antibiotico_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control_ab_receta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion_consultorio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cedula_profesional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.venta_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folio_receta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_receta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reporte)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_reporte
            // 
            this.dgv_reporte.AllowUserToAddRows = false;
            this.dgv_reporte.AllowUserToDeleteRows = false;
            this.dgv_reporte.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_reporte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_reporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_reporte.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_reporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_reporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_reporte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fecha_receta,
            this.folio_receta,
            this.fecha_venta,
            this.venta_folio,
            this.doctor,
            this.cedula_profesional,
            this.direccion_consultorio,
            this.control_ab_receta_id,
            this.control_antibiotico_id,
            this.venta_id,
            this.comentarios,
            this.codigos});
            this.dgv_reporte.Location = new System.Drawing.Point(12, 38);
            this.dgv_reporte.MultiSelect = false;
            this.dgv_reporte.Name = "dgv_reporte";
            this.dgv_reporte.ReadOnly = true;
            this.dgv_reporte.RowHeadersVisible = false;
            this.dgv_reporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_reporte.Size = new System.Drawing.Size(1212, 478);
            this.dgv_reporte.TabIndex = 0;
            this.dgv_reporte.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_reporte_MouseDoubleClick);
            // 
            // dt_fecha_inicial
            // 
            this.dt_fecha_inicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_fecha_inicial.Location = new System.Drawing.Point(88, 9);
            this.dt_fecha_inicial.Name = "dt_fecha_inicial";
            this.dt_fecha_inicial.Size = new System.Drawing.Size(134, 20);
            this.dt_fecha_inicial.TabIndex = 1;
            this.dt_fecha_inicial.ValueChanged += new System.EventHandler(this.dt_fecha_inicial_ValueChanged);
            // 
            // dt_fecha_final
            // 
            this.dt_fecha_final.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_fecha_final.Location = new System.Drawing.Point(311, 9);
            this.dt_fecha_final.Name = "dt_fecha_final";
            this.dt_fecha_final.Size = new System.Drawing.Size(135, 20);
            this.dt_fecha_final.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha Inicial:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fecha Final:";
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(474, 8);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 5;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // codigos
            // 
            this.codigos.DataPropertyName = "codigos";
            this.codigos.HeaderText = "Productos";
            this.codigos.Name = "codigos";
            this.codigos.ReadOnly = true;
            // 
            // comentarios
            // 
            this.comentarios.DataPropertyName = "comentarios";
            this.comentarios.HeaderText = "comentarios";
            this.comentarios.Name = "comentarios";
            this.comentarios.ReadOnly = true;
            this.comentarios.Visible = false;
            // 
            // venta_id
            // 
            this.venta_id.DataPropertyName = "elemento_id";
            this.venta_id.HeaderText = "venta_id";
            this.venta_id.Name = "venta_id";
            this.venta_id.ReadOnly = true;
            this.venta_id.Visible = false;
            // 
            // control_antibiotico_id
            // 
            this.control_antibiotico_id.DataPropertyName = "control_antibiotico_id";
            this.control_antibiotico_id.HeaderText = "control_antibiotico_id";
            this.control_antibiotico_id.Name = "control_antibiotico_id";
            this.control_antibiotico_id.ReadOnly = true;
            this.control_antibiotico_id.Visible = false;
            // 
            // control_ab_receta_id
            // 
            this.control_ab_receta_id.DataPropertyName = "control_ab_receta_id";
            this.control_ab_receta_id.HeaderText = "control_ab_receta_id";
            this.control_ab_receta_id.Name = "control_ab_receta_id";
            this.control_ab_receta_id.ReadOnly = true;
            this.control_ab_receta_id.Visible = false;
            // 
            // direccion_consultorio
            // 
            this.direccion_consultorio.DataPropertyName = "direccion_consultorio";
            this.direccion_consultorio.HeaderText = "Dirección";
            this.direccion_consultorio.Name = "direccion_consultorio";
            this.direccion_consultorio.ReadOnly = true;
            // 
            // cedula_profesional
            // 
            this.cedula_profesional.DataPropertyName = "cedula_profesional";
            this.cedula_profesional.HeaderText = "Cédula";
            this.cedula_profesional.Name = "cedula_profesional";
            this.cedula_profesional.ReadOnly = true;
            // 
            // doctor
            // 
            this.doctor.DataPropertyName = "doctor";
            this.doctor.HeaderText = "Doctor";
            this.doctor.Name = "doctor";
            this.doctor.ReadOnly = true;
            // 
            // venta_folio
            // 
            this.venta_folio.DataPropertyName = "venta_folio";
            this.venta_folio.HeaderText = "Folio Venta";
            this.venta_folio.Name = "venta_folio";
            this.venta_folio.ReadOnly = true;
            // 
            // fecha_venta
            // 
            this.fecha_venta.DataPropertyName = "fecha_venta";
            this.fecha_venta.HeaderText = "Fecha Venta";
            this.fecha_venta.Name = "fecha_venta";
            this.fecha_venta.ReadOnly = true;
            // 
            // folio_receta
            // 
            this.folio_receta.DataPropertyName = "folio_receta";
            this.folio_receta.HeaderText = "Folio Receta";
            this.folio_receta.Name = "folio_receta";
            this.folio_receta.ReadOnly = true;
            // 
            // fecha_receta
            // 
            this.fecha_receta.DataPropertyName = "fecha_receta";
            this.fecha_receta.HeaderText = "Fecha Receta";
            this.fecha_receta.Name = "fecha_receta";
            this.fecha_receta.ReadOnly = true;
            this.fecha_receta.Visible = false;
            // 
            // Reporte_Movimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 526);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dt_fecha_final);
            this.Controls.Add(this.dt_fecha_inicial);
            this.Controls.Add(this.dgv_reporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reporte_Movimientos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Movimientos";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_reporte;
        private System.Windows.Forms.DateTimePicker dt_fecha_inicial;
        private System.Windows.Forms.DateTimePicker dt_fecha_final;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_receta;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio_receta;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn venta_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn doctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cedula_profesional;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccion_consultorio;
        private System.Windows.Forms.DataGridViewTextBoxColumn control_ab_receta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn control_antibiotico_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn venta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn comentarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigos;
    }
}