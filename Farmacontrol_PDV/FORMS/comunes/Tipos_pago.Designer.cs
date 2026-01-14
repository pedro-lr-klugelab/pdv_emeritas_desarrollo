namespace Farmacontrol_PDV.FORMS.comunes
{
    partial class Tipos_pago
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
            this.dgv_tipos_pago = new System.Windows.Forms.DataGridView();
            this.pago_tipo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etiqueta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usa_cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entrega_cambio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_credito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_prepago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tipos_pago)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_tipos_pago
            // 
            this.dgv_tipos_pago.AllowUserToAddRows = false;
            this.dgv_tipos_pago.AllowUserToDeleteRows = false;
            this.dgv_tipos_pago.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_tipos_pago.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_tipos_pago.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tipos_pago.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_tipos_pago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tipos_pago.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pago_tipo_id,
            this.etiqueta,
            this.nombre,
            this.usa_cuenta,
            this.entrega_cambio,
            this.es_credito,
            this.es_prepago});
            this.dgv_tipos_pago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tipos_pago.Location = new System.Drawing.Point(0, 0);
            this.dgv_tipos_pago.MultiSelect = false;
            this.dgv_tipos_pago.Name = "dgv_tipos_pago";
            this.dgv_tipos_pago.ReadOnly = true;
            this.dgv_tipos_pago.RowHeadersVisible = false;
            this.dgv_tipos_pago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tipos_pago.Size = new System.Drawing.Size(284, 261);
            this.dgv_tipos_pago.TabIndex = 0;
            this.dgv_tipos_pago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_tipos_pago_KeyDown);
            // 
            // pago_tipo_id
            // 
            this.pago_tipo_id.DataPropertyName = "pago_tipo_id";
            this.pago_tipo_id.HeaderText = "pago_tipo_id";
            this.pago_tipo_id.Name = "pago_tipo_id";
            this.pago_tipo_id.ReadOnly = true;
            this.pago_tipo_id.Visible = false;
            // 
            // etiqueta
            // 
            this.etiqueta.DataPropertyName = "etiqueta";
            this.etiqueta.HeaderText = "etiqueta";
            this.etiqueta.Name = "etiqueta";
            this.etiqueta.ReadOnly = true;
            this.etiqueta.Visible = false;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Tipos de pago";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // usa_cuenta
            // 
            this.usa_cuenta.DataPropertyName = "usa_cuenta";
            this.usa_cuenta.HeaderText = "usa_cuenta";
            this.usa_cuenta.Name = "usa_cuenta";
            this.usa_cuenta.ReadOnly = true;
            this.usa_cuenta.Visible = false;
            // 
            // entrega_cambio
            // 
            this.entrega_cambio.DataPropertyName = "entrega_cambio";
            this.entrega_cambio.HeaderText = "entrega_cambio";
            this.entrega_cambio.Name = "entrega_cambio";
            this.entrega_cambio.ReadOnly = true;
            this.entrega_cambio.Visible = false;
            // 
            // es_credito
            // 
            this.es_credito.DataPropertyName = "es_credito";
            this.es_credito.HeaderText = "es_credito";
            this.es_credito.Name = "es_credito";
            this.es_credito.ReadOnly = true;
            this.es_credito.Visible = false;
            // 
            // es_prepago
            // 
            this.es_prepago.DataPropertyName = "es_prepago";
            this.es_prepago.HeaderText = "es_prepago";
            this.es_prepago.Name = "es_prepago";
            this.es_prepago.ReadOnly = true;
            this.es_prepago.Visible = false;
            // 
            // Tipos_pago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.dgv_tipos_pago);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tipos_pago";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Tipos_pago_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tipos_pago_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tipos_pago)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_tipos_pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn pago_tipo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn etiqueta;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn usa_cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn entrega_cambio;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_credito;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_prepago;
    }
}