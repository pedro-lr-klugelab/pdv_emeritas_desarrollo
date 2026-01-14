namespace Farmacontrol_PDV.FORMS.reportes.reimpresiones
{
    partial class Reimpresiones_principal
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbb_kardex_tipo = new System.Windows.Forms.ComboBox();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.dgv_impresiones = new System.Windows.Forms.DataGridView();
            this.impresion_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminal_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Impresora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_reimprimir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_fecha = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_impresiones)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtrar por";
            // 
            // cbb_kardex_tipo
            // 
            this.cbb_kardex_tipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_kardex_tipo.FormattingEnabled = true;
            this.cbb_kardex_tipo.Location = new System.Drawing.Point(68, 6);
            this.cbb_kardex_tipo.Name = "cbb_kardex_tipo";
            this.cbb_kardex_tipo.Size = new System.Drawing.Size(239, 21);
            this.cbb_kardex_tipo.TabIndex = 1;
            this.cbb_kardex_tipo.SelectedIndexChanged += new System.EventHandler(this.cbb_kardex_tipo_SelectedIndexChanged);
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(532, 4);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 6;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // dgv_impresiones
            // 
            this.dgv_impresiones.AllowUserToAddRows = false;
            this.dgv_impresiones.AllowUserToDeleteRows = false;
            this.dgv_impresiones.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_impresiones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_impresiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_impresiones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_impresiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_impresiones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.impresion_id,
            this.terminal_id,
            this.Terminal,
            this.empleado_id,
            this.Empleado,
            this.Tipo,
            this.Folio,
            this.Impresora,
            this.Fecha});
            this.dgv_impresiones.Location = new System.Drawing.Point(12, 33);
            this.dgv_impresiones.MultiSelect = false;
            this.dgv_impresiones.Name = "dgv_impresiones";
            this.dgv_impresiones.ReadOnly = true;
            this.dgv_impresiones.RowHeadersVisible = false;
            this.dgv_impresiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_impresiones.Size = new System.Drawing.Size(915, 528);
            this.dgv_impresiones.TabIndex = 7;
            // 
            // impresion_id
            // 
            this.impresion_id.DataPropertyName = "impresion_id";
            this.impresion_id.HeaderText = "impresion_id";
            this.impresion_id.Name = "impresion_id";
            this.impresion_id.ReadOnly = true;
            this.impresion_id.Visible = false;
            // 
            // terminal_id
            // 
            this.terminal_id.DataPropertyName = "terminal_id";
            this.terminal_id.HeaderText = "terminal_id";
            this.terminal_id.Name = "terminal_id";
            this.terminal_id.ReadOnly = true;
            this.terminal_id.Visible = false;
            // 
            // Terminal
            // 
            this.Terminal.DataPropertyName = "nombre_terminal";
            this.Terminal.HeaderText = "Terminal";
            this.Terminal.Name = "Terminal";
            this.Terminal.ReadOnly = true;
            // 
            // empleado_id
            // 
            this.empleado_id.DataPropertyName = "empleado_id";
            this.empleado_id.HeaderText = "empleado_id";
            this.empleado_id.Name = "empleado_id";
            this.empleado_id.ReadOnly = true;
            this.empleado_id.Visible = false;
            // 
            // Empleado
            // 
            this.Empleado.DataPropertyName = "nombre_empleado";
            this.Empleado.HeaderText = "Empleado";
            this.Empleado.Name = "Empleado";
            this.Empleado.ReadOnly = true;
            // 
            // Tipo
            // 
            this.Tipo.DataPropertyName = "tipo";
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            // 
            // Folio
            // 
            this.Folio.DataPropertyName = "folio";
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            // 
            // Impresora
            // 
            this.Impresora.DataPropertyName = "impresora";
            this.Impresora.HeaderText = "Impresora";
            this.Impresora.Name = "Impresora";
            this.Impresora.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // btn_reimprimir
            // 
            this.btn_reimprimir.Location = new System.Drawing.Point(851, 568);
            this.btn_reimprimir.Name = "btn_reimprimir";
            this.btn_reimprimir.Size = new System.Drawing.Size(75, 23);
            this.btn_reimprimir.TabIndex = 8;
            this.btn_reimprimir.Text = "Reimprimir";
            this.btn_reimprimir.UseVisualStyleBackColor = true;
            this.btn_reimprimir.Click += new System.EventHandler(this.btn_reimprimir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha:";
            // 
            // cbb_fecha
            // 
            this.cbb_fecha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_fecha.FormattingEnabled = true;
            this.cbb_fecha.Location = new System.Drawing.Point(359, 6);
            this.cbb_fecha.Name = "cbb_fecha";
            this.cbb_fecha.Size = new System.Drawing.Size(167, 21);
            this.cbb_fecha.TabIndex = 10;
            // 
            // Reimpresiones_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 603);
            this.Controls.Add(this.cbb_fecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_reimprimir);
            this.Controls.Add(this.dgv_impresiones);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.cbb_kardex_tipo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reimpresiones_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reimpresiones";
            this.Load += new System.EventHandler(this.Reimpresiones_principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_impresiones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_kardex_tipo;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.DataGridView dgv_impresiones;
        private System.Windows.Forms.Button btn_reimprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn impresion_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn terminal_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn empleado_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Impresora;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbb_fecha;
    }
}