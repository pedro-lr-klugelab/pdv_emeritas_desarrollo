namespace Farmacontrol_PDV.FORMS.reportes.entradas_ab
{
    partial class Entradas_AB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_reporte = new System.Windows.Forms.DataGridView();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mayorista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.termino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_terminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_reporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reporte)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_reporte
            // 
            this.dgv_reporte.AllowUserToAddRows = false;
            this.dgv_reporte.AllowUserToDeleteRows = false;
            this.dgv_reporte.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_reporte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_reporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_reporte.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_reporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_reporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_reporte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.folio,
            this.mayorista,
            this.termino,
            this.comentarios,
            this.fecha_terminado});
            this.dgv_reporte.Location = new System.Drawing.Point(12, 40);
            this.dgv_reporte.Name = "dgv_reporte";
            this.dgv_reporte.ReadOnly = true;
            this.dgv_reporte.RowHeadersVisible = false;
            this.dgv_reporte.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_reporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_reporte.Size = new System.Drawing.Size(732, 400);
            this.dgv_reporte.StandardTab = true;
            this.dgv_reporte.TabIndex = 0;
            // 
            // folio
            // 
            this.folio.DataPropertyName = "folio";
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            this.folio.ReadOnly = true;
            // 
            // mayorista
            // 
            this.mayorista.DataPropertyName = "mayorista";
            this.mayorista.HeaderText = "Mayorista";
            this.mayorista.Name = "mayorista";
            this.mayorista.ReadOnly = true;
            // 
            // termino
            // 
            this.termino.DataPropertyName = "termino";
            this.termino.HeaderText = "Empleado";
            this.termino.Name = "termino";
            this.termino.ReadOnly = true;
            // 
            // comentarios
            // 
            this.comentarios.DataPropertyName = "comentarios";
            this.comentarios.HeaderText = "Comentarios";
            this.comentarios.Name = "comentarios";
            this.comentarios.ReadOnly = true;
            // 
            // fecha_terminado
            // 
            this.fecha_terminado.DataPropertyName = "fecha_terminado";
            this.fecha_terminado.HeaderText = "Fecha Terminado";
            this.fecha_terminado.Name = "fecha_terminado";
            this.fecha_terminado.ReadOnly = true;
            // 
            // btn_reporte
            // 
            this.btn_reporte.Location = new System.Drawing.Point(669, 11);
            this.btn_reporte.Name = "btn_reporte";
            this.btn_reporte.Size = new System.Drawing.Size(75, 23);
            this.btn_reporte.TabIndex = 1;
            this.btn_reporte.Text = "Generar Reporte";
            this.btn_reporte.UseVisualStyleBackColor = true;
            this.btn_reporte.Click += new System.EventHandler(this.btn_reporte_Click);
            // 
            // Entradas_AB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 452);
            this.Controls.Add(this.btn_reporte);
            this.Controls.Add(this.dgv_reporte);
            this.Name = "Entradas_AB";
            this.Text = "Entradas AB";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_reporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_reporte;
        private System.Windows.Forms.Button btn_reporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn mayorista;
        private System.Windows.Forms.DataGridViewTextBoxColumn termino;
        private System.Windows.Forms.DataGridViewTextBoxColumn comentarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_terminado;
    }
}