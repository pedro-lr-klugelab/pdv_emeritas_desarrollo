namespace Farmacontrol_PDV.FORMS.reportes.ventas
{
    partial class Ventas_principal
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
            this.dgv_ventas = new System.Windows.Forms.DataGridView();
            this.venta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.piezas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cbb_fechas = new System.Windows.Forms.ComboBox();
            this.btn_cargar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.save_dialog_ventas = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ventas
            // 
            this.dgv_ventas.AllowUserToAddRows = false;
            this.dgv_ventas.AllowUserToDeleteRows = false;
            this.dgv_ventas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_ventas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ventas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ventas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ventas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ventas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.venta_id,
            this.folio,
            this.terminal,
            this.fecha,
            this.empleado,
            this.articulos,
            this.piezas,
            this.total});
            this.dgv_ventas.Location = new System.Drawing.Point(12, 33);
            this.dgv_ventas.MultiSelect = false;
            this.dgv_ventas.Name = "dgv_ventas";
            this.dgv_ventas.ReadOnly = true;
            this.dgv_ventas.RowHeadersVisible = false;
            this.dgv_ventas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ventas.Size = new System.Drawing.Size(1119, 437);
            this.dgv_ventas.TabIndex = 0;
            this.dgv_ventas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ventas_CellContentDoubleClick);
            this.dgv_ventas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ventas_CellDoubleClick);
            this.dgv_ventas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_ventas_KeyDown);
            // 
            // venta_id
            // 
            this.venta_id.DataPropertyName = "venta_id";
            this.venta_id.HeaderText = "venta_id";
            this.venta_id.Name = "venta_id";
            this.venta_id.ReadOnly = true;
            this.venta_id.Visible = false;
            // 
            // folio
            // 
            this.folio.DataPropertyName = "venta_folio";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.folio.DefaultCellStyle = dataGridViewCellStyle3;
            this.folio.FillWeight = 80F;
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            this.folio.ReadOnly = true;
            // 
            // terminal
            // 
            this.terminal.DataPropertyName = "terminal";
            this.terminal.FillWeight = 80F;
            this.terminal.HeaderText = "Terminal";
            this.terminal.Name = "terminal";
            this.terminal.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha.DefaultCellStyle = dataGridViewCellStyle4;
            this.fecha.FillWeight = 90F;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.DataPropertyName = "empleado";
            this.empleado.FillWeight = 120F;
            this.empleado.HeaderText = "Empleado";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // articulos
            // 
            this.articulos.DataPropertyName = "articulos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.articulos.DefaultCellStyle = dataGridViewCellStyle5;
            this.articulos.FillWeight = 80F;
            this.articulos.HeaderText = "Articulos";
            this.articulos.Name = "articulos";
            this.articulos.ReadOnly = true;
            // 
            // piezas
            // 
            this.piezas.DataPropertyName = "piezas";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.piezas.DefaultCellStyle = dataGridViewCellStyle6;
            this.piezas.FillWeight = 80F;
            this.piezas.HeaderText = "Piezas";
            this.piezas.Name = "piezas";
            this.piezas.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total.DefaultCellStyle = dataGridViewCellStyle7;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ventas del dia";
            // 
            // cbb_fechas
            // 
            this.cbb_fechas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_fechas.FormattingEnabled = true;
            this.cbb_fechas.Location = new System.Drawing.Point(92, 6);
            this.cbb_fechas.Name = "cbb_fechas";
            this.cbb_fechas.Size = new System.Drawing.Size(145, 21);
            this.cbb_fechas.TabIndex = 2;
            // 
            // btn_cargar
            // 
            this.btn_cargar.Location = new System.Drawing.Point(243, 6);
            this.btn_cargar.Name = "btn_cargar";
            this.btn_cargar.Size = new System.Drawing.Size(75, 23);
            this.btn_cargar.TabIndex = 3;
            this.btn_cargar.Text = "Cargar";
            this.btn_cargar.UseVisualStyleBackColor = true;
            this.btn_cargar.Click += new System.EventHandler(this.btn_cargar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(968, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Generar reporte completo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Doble click a la venta para ver los detalles";
            // 
            // save_dialog_ventas
            // 
            this.save_dialog_ventas.FileName = "reporte_ventas";
            // 
            // Ventas_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 509);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_cargar);
            this.Controls.Add(this.cbb_fechas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_ventas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ventas_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventas";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ventas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_fechas;
        private System.Windows.Forms.Button btn_cargar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn venta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn terminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulos;
        private System.Windows.Forms.DataGridViewTextBoxColumn piezas;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog save_dialog_ventas;
    }
}