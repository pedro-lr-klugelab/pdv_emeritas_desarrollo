namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Control_AB_principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_control_AB = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control_antibiotico_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control_antibioticos_receta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elemento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.piezas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_producto = new System.Windows.Forms.TextBox();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.lbl_producto = new System.Windows.Forms.Label();
            this.lbl_amecop = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_existencia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_tiempo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_reg_mov = new System.Windows.Forms.Button();
            this.btn_reporte = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFechaEntradaInicial = new System.Windows.Forms.Label();
            this.lblExistenciaInicial = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSustancia = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_control_AB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_control_AB
            // 
            this.dgv_control_AB.AllowUserToAddRows = false;
            this.dgv_control_AB.AllowUserToDeleteRows = false;
            this.dgv_control_AB.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_control_AB.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgv_control_AB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_control_AB.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_control_AB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgv_control_AB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_control_AB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.fecha,
            this.control_antibiotico_id,
            this.amecop,
            this.control_antibioticos_receta_id,
            this.movimiento,
            this.elemento_id,
            this.piezas,
            this.caducidad,
            this.lote});
            this.dgv_control_AB.Location = new System.Drawing.Point(9, 74);
            this.dgv_control_AB.MultiSelect = false;
            this.dgv_control_AB.Name = "dgv_control_AB";
            this.dgv_control_AB.ReadOnly = true;
            this.dgv_control_AB.RowHeadersVisible = false;
            this.dgv_control_AB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_control_AB.Size = new System.Drawing.Size(983, 464);
            this.dgv_control_AB.TabIndex = 2;
            this.dgv_control_AB.Click += new System.EventHandler(this.dgv_control_AB_Click);
            this.dgv_control_AB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_control_AB_KeyDown);
            this.dgv_control_AB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_control_AB_MouseDoubleClick);
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // control_antibiotico_id
            // 
            this.control_antibiotico_id.DataPropertyName = "control_antibiotico_id";
            this.control_antibiotico_id.HeaderText = "control_antibiotico_id";
            this.control_antibiotico_id.Name = "control_antibiotico_id";
            this.control_antibiotico_id.ReadOnly = true;
            this.control_antibiotico_id.Visible = false;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.HeaderText = "amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            this.amecop.Visible = false;
            // 
            // control_antibioticos_receta_id
            // 
            this.control_antibioticos_receta_id.DataPropertyName = "control_antibioticos_receta_id";
            this.control_antibioticos_receta_id.HeaderText = "control_antibioticos_receta_id";
            this.control_antibioticos_receta_id.Name = "control_antibioticos_receta_id";
            this.control_antibioticos_receta_id.ReadOnly = true;
            this.control_antibioticos_receta_id.Visible = false;
            // 
            // movimiento
            // 
            this.movimiento.DataPropertyName = "movimiento";
            this.movimiento.HeaderText = "Movimiento";
            this.movimiento.Name = "movimiento";
            this.movimiento.ReadOnly = true;
            // 
            // elemento_id
            // 
            this.elemento_id.DataPropertyName = "elemento_id";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.elemento_id.DefaultCellStyle = dataGridViewCellStyle15;
            this.elemento_id.HeaderText = "Folio";
            this.elemento_id.Name = "elemento_id";
            this.elemento_id.ReadOnly = true;
            // 
            // piezas
            // 
            this.piezas.DataPropertyName = "cantidad";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.piezas.DefaultCellStyle = dataGridViewCellStyle16;
            this.piezas.HeaderText = "Piezas";
            this.piezas.Name = "piezas";
            this.piezas.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle17;
            this.caducidad.FillWeight = 70F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle18;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            this.lote.ReadOnly = true;
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Location = new System.Drawing.Point(248, 10);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.ReadOnly = true;
            this.txt_producto.Size = new System.Drawing.Size(462, 20);
            this.txt_producto.TabIndex = 161;
            this.txt_producto.TabStop = false;
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(64, 10);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(122, 20);
            this.txt_amecop.TabIndex = 160;
            this.txt_amecop.Click += new System.EventHandler(this.txt_amecop_Click);
            this.txt_amecop.TextChanged += new System.EventHandler(this.txt_amecop_TextChanged);
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            this.txt_amecop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_amecop_KeyPress);
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(195, 13);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(47, 13);
            this.lbl_producto.TabIndex = 164;
            this.lbl_producto.Text = "Nombre:";
            // 
            // lbl_amecop
            // 
            this.lbl_amecop.AutoSize = true;
            this.lbl_amecop.Location = new System.Drawing.Point(9, 13);
            this.lbl_amecop.Name = "lbl_amecop";
            this.lbl_amecop.Size = new System.Drawing.Size(49, 13);
            this.lbl_amecop.TabIndex = 163;
            this.lbl_amecop.Text = "Amecop:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(729, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 166;
            this.label2.Text = "Existencia:";
            // 
            // txt_existencia
            // 
            this.txt_existencia.Location = new System.Drawing.Point(793, 10);
            this.txt_existencia.Name = "txt_existencia";
            this.txt_existencia.ReadOnly = true;
            this.txt_existencia.Size = new System.Drawing.Size(50, 20);
            this.txt_existencia.TabIndex = 167;
            this.txt_existencia.TabStop = false;
            this.txt_existencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(796, 557);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 168;
            this.label1.Text = "Generado en:";
            // 
            // lbl_tiempo
            // 
            this.lbl_tiempo.AutoSize = true;
            this.lbl_tiempo.Location = new System.Drawing.Point(874, 557);
            this.lbl_tiempo.Name = "lbl_tiempo";
            this.lbl_tiempo.Size = new System.Drawing.Size(40, 13);
            this.lbl_tiempo.TabIndex = 169;
            this.lbl_tiempo.Text = "0.0000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(939, 554);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 170;
            this.label3.Text = "segundos";
            // 
            // btn_reg_mov
            // 
            this.btn_reg_mov.Location = new System.Drawing.Point(863, 7);
            this.btn_reg_mov.Name = "btn_reg_mov";
            this.btn_reg_mov.Size = new System.Drawing.Size(130, 23);
            this.btn_reg_mov.TabIndex = 171;
            this.btn_reg_mov.Text = "Registrar Movimiento";
            this.btn_reg_mov.UseVisualStyleBackColor = true;
            this.btn_reg_mov.Click += new System.EventHandler(this.btn_reg_mov_Click);
            // 
            // btn_reporte
            // 
            this.btn_reporte.Location = new System.Drawing.Point(10, 544);
            this.btn_reporte.Name = "btn_reporte";
            this.btn_reporte.Size = new System.Drawing.Size(135, 23);
            this.btn_reporte.TabIndex = 172;
            this.btn_reporte.Text = "Consulta de Movimientos";
            this.btn_reporte.UseVisualStyleBackColor = true;
            this.btn_reporte.Click += new System.EventHandler(this.btn_reporte_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 173;
            this.label4.Text = "Fecha :";
            // 
            // lblFechaEntradaInicial
            // 
            this.lblFechaEntradaInicial.AutoSize = true;
            this.lblFechaEntradaInicial.Location = new System.Drawing.Point(61, 48);
            this.lblFechaEntradaInicial.Name = "lblFechaEntradaInicial";
            this.lblFechaEntradaInicial.Size = new System.Drawing.Size(65, 13);
            this.lblFechaEntradaInicial.TabIndex = 174;
            this.lblFechaEntradaInicial.Text = "16/03/1991";
            // 
            // lblExistenciaInicial
            // 
            this.lblExistenciaInicial.AutoSize = true;
            this.lblExistenciaInicial.Location = new System.Drawing.Point(920, 557);
            this.lblExistenciaInicial.Name = "lblExistenciaInicial";
            this.lblExistenciaInicial.Size = new System.Drawing.Size(13, 13);
            this.lblExistenciaInicial.TabIndex = 176;
            this.lblExistenciaInicial.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 178;
            this.label5.Text = "Sustancia :";
            // 
            // lblSustancia
            // 
            this.lblSustancia.AutoSize = true;
            this.lblSustancia.Location = new System.Drawing.Point(261, 48);
            this.lblSustancia.Name = "lblSustancia";
            this.lblSustancia.Size = new System.Drawing.Size(0, 13);
            this.lblSustancia.TabIndex = 179;
            // 
            // Control_AB_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 579);
            this.Controls.Add(this.lblSustancia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblExistenciaInicial);
            this.Controls.Add(this.lblFechaEntradaInicial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_reporte);
            this.Controls.Add(this.btn_reg_mov);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_tiempo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_existencia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_producto);
            this.Controls.Add(this.lbl_amecop);
            this.Controls.Add(this.dgv_control_AB);
            this.Controls.Add(this.txt_producto);
            this.Controls.Add(this.txt_amecop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Control_AB_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control Antibióticos";
            this.Load += new System.EventHandler(this.Control_AB_principal_Load);
            this.Shown += new System.EventHandler(this.Control_AB_principal_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_AB_principal_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_control_AB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_control_AB;
        private System.Windows.Forms.TextBox txt_producto;
        private System.Windows.Forms.TextBox txt_amecop;
        private System.Windows.Forms.Label lbl_producto;
        private System.Windows.Forms.Label lbl_amecop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_existencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_tiempo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_reg_mov;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn control_antibiotico_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn control_antibioticos_receta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn movimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn elemento_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn piezas;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn lote;
        private System.Windows.Forms.Button btn_reporte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFechaEntradaInicial;
        private System.Windows.Forms.Label lblExistenciaInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSustancia;
    }
}