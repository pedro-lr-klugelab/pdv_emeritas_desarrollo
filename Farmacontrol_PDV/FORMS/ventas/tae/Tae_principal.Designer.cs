namespace Farmacontrol_PDV.FORMS.ventas.tae
{
    partial class Tae_principal
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
            this.dgv_servicios_tae = new System.Windows.Forms.DataGridView();
            this.dgv_serv_articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nom_fabricante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_serv_fabricante_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_serv_denominacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_serv_sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_prov_tae = new System.Windows.Forms.DataGridView();
            this.dgv_prov_fabricante_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_prov_nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_servicio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_proveedor = new System.Windows.Forms.Label();
            this.txt_confirma_numero = new System.Windows.Forms.TextBox();
            this.txt_numero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_cobrar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_importe = new System.Windows.Forms.TextBox();
            this.txt_comision = new System.Windows.Forms.TextBox();
            this.txt_referencia = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_servicios_tae)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_prov_tae)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_servicios_tae
            // 
            this.dgv_servicios_tae.AllowUserToAddRows = false;
            this.dgv_servicios_tae.AllowUserToDeleteRows = false;
            this.dgv_servicios_tae.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_servicios_tae.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_servicios_tae.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_servicios_tae.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_servicios_tae.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_servicios_tae.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_servicios_tae.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_serv_articulo_id,
            this.nom_fabricante,
            this.precio_publico,
            this.dgv_serv_fabricante_id,
            this.dgv_serv_denominacion,
            this.dgv_serv_sku});
            this.dgv_servicios_tae.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_servicios_tae.Location = new System.Drawing.Point(0, 0);
            this.dgv_servicios_tae.MultiSelect = false;
            this.dgv_servicios_tae.Name = "dgv_servicios_tae";
            this.dgv_servicios_tae.ReadOnly = true;
            this.dgv_servicios_tae.RowHeadersVisible = false;
            this.dgv_servicios_tae.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_servicios_tae.Size = new System.Drawing.Size(386, 293);
            this.dgv_servicios_tae.TabIndex = 0;
            this.dgv_servicios_tae.TabStop = false;
            this.dgv_servicios_tae.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_servicios_tae_CellContentClick);
            this.dgv_servicios_tae.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_servicios_tae_KeyDown);
            // 
            // dgv_serv_articulo_id
            // 
            this.dgv_serv_articulo_id.DataPropertyName = "articulo_id";
            this.dgv_serv_articulo_id.HeaderText = "articulo_id";
            this.dgv_serv_articulo_id.Name = "dgv_serv_articulo_id";
            this.dgv_serv_articulo_id.ReadOnly = true;
            this.dgv_serv_articulo_id.Visible = false;
            // 
            // nom_fabricante
            // 
            this.nom_fabricante.DataPropertyName = "nombre_fabricante";
            this.nom_fabricante.HeaderText = "nom_fabricante";
            this.nom_fabricante.Name = "nom_fabricante";
            this.nom_fabricante.ReadOnly = true;
            this.nom_fabricante.Visible = false;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            this.precio_publico.HeaderText = "precio_publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            this.precio_publico.Visible = false;
            // 
            // dgv_serv_fabricante_id
            // 
            this.dgv_serv_fabricante_id.DataPropertyName = "fabricante_id";
            this.dgv_serv_fabricante_id.HeaderText = "fabricante_id";
            this.dgv_serv_fabricante_id.Name = "dgv_serv_fabricante_id";
            this.dgv_serv_fabricante_id.ReadOnly = true;
            this.dgv_serv_fabricante_id.Visible = false;
            // 
            // dgv_serv_denominacion
            // 
            this.dgv_serv_denominacion.DataPropertyName = "nombre";
            this.dgv_serv_denominacion.HeaderText = "Servicio";
            this.dgv_serv_denominacion.Name = "dgv_serv_denominacion";
            this.dgv_serv_denominacion.ReadOnly = true;
            // 
            // dgv_serv_sku
            // 
            this.dgv_serv_sku.DataPropertyName = "sku";
            this.dgv_serv_sku.HeaderText = "sku";
            this.dgv_serv_sku.Name = "dgv_serv_sku";
            this.dgv_serv_sku.ReadOnly = true;
            this.dgv_serv_sku.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_prov_tae);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 293);
            this.panel1.TabIndex = 99;
            this.panel1.Tag = "panel1";
            // 
            // dgv_prov_tae
            // 
            this.dgv_prov_tae.AllowUserToAddRows = false;
            this.dgv_prov_tae.AllowUserToDeleteRows = false;
            this.dgv_prov_tae.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_prov_tae.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_prov_tae.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_prov_tae.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_prov_tae.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_prov_tae.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_prov_fabricante_id,
            this.dgv_prov_nombre});
            this.dgv_prov_tae.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_prov_tae.Location = new System.Drawing.Point(0, 0);
            this.dgv_prov_tae.MultiSelect = false;
            this.dgv_prov_tae.Name = "dgv_prov_tae";
            this.dgv_prov_tae.ReadOnly = true;
            this.dgv_prov_tae.RowHeadersVisible = false;
            this.dgv_prov_tae.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_prov_tae.Size = new System.Drawing.Size(386, 293);
            this.dgv_prov_tae.TabIndex = 1;
            this.dgv_prov_tae.TabStop = false;
            this.dgv_prov_tae.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_prov_tae_KeyDown);
            // 
            // dgv_prov_fabricante_id
            // 
            this.dgv_prov_fabricante_id.DataPropertyName = "fabricante_id";
            this.dgv_prov_fabricante_id.HeaderText = "fabricante_id";
            this.dgv_prov_fabricante_id.Name = "dgv_prov_fabricante_id";
            this.dgv_prov_fabricante_id.ReadOnly = true;
            this.dgv_prov_fabricante_id.Visible = false;
            // 
            // dgv_prov_nombre
            // 
            this.dgv_prov_nombre.DataPropertyName = "nombre";
            this.dgv_prov_nombre.HeaderText = "Nombre";
            this.dgv_prov_nombre.Name = "dgv_prov_nombre";
            this.dgv_prov_nombre.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbl_servicio);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lbl_proveedor);
            this.panel3.Controls.Add(this.txt_confirma_numero);
            this.panel3.Controls.Add(this.txt_numero);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(386, 293);
            this.panel3.TabIndex = 99;
            this.panel3.Tag = "panel3";
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // lbl_servicio
            // 
            this.lbl_servicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_servicio.Location = new System.Drawing.Point(125, 118);
            this.lbl_servicio.Name = "lbl_servicio";
            this.lbl_servicio.Size = new System.Drawing.Size(241, 23);
            this.lbl_servicio.TabIndex = 7;
            this.lbl_servicio.Text = "DATOS 200MB 7 DÍAS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Servicio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Proveedor:";
            // 
            // lbl_proveedor
            // 
            this.lbl_proveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_proveedor.Location = new System.Drawing.Point(125, 90);
            this.lbl_proveedor.Name = "lbl_proveedor";
            this.lbl_proveedor.Size = new System.Drawing.Size(241, 23);
            this.lbl_proveedor.TabIndex = 4;
            this.lbl_proveedor.Text = "VIRGIN MOBILE";
            // 
            // txt_confirma_numero
            // 
            this.txt_confirma_numero.Enabled = false;
            this.txt_confirma_numero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_confirma_numero.Location = new System.Drawing.Point(129, 176);
            this.txt_confirma_numero.MaxLength = 10;
            this.txt_confirma_numero.Name = "txt_confirma_numero";
            this.txt_confirma_numero.ShortcutsEnabled = false;
            this.txt_confirma_numero.Size = new System.Drawing.Size(131, 26);
            this.txt_confirma_numero.TabIndex = 3;
            this.txt_confirma_numero.Text = "8888888888";
            this.txt_confirma_numero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_confirma_numero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_confirma_numero_KeyDown);
            this.txt_confirma_numero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_confirma_numero_KeyPress);
            // 
            // txt_numero
            // 
            this.txt_numero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_numero.Location = new System.Drawing.Point(129, 144);
            this.txt_numero.MaxLength = 10;
            this.txt_numero.Name = "txt_numero";
            this.txt_numero.ShortcutsEnabled = false;
            this.txt_numero.Size = new System.Drawing.Size(131, 26);
            this.txt_numero.TabIndex = 2;
            this.txt_numero.Text = "9999999999";
            this.txt_numero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_numero.TextChanged += new System.EventHandler(this.txt_numero_TextChanged);
            this.txt_numero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_numero_KeyDown);
            this.txt_numero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_numero_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirmar número:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de teléfono:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_servicios_tae);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 293);
            this.panel2.TabIndex = 99;
            this.panel2.Tag = "panel2";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.btn_cancelar);
            this.panel4.Controls.Add(this.btn_cobrar);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.txt_importe);
            this.panel4.Controls.Add(this.txt_comision);
            this.panel4.Controls.Add(this.txt_referencia);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(386, 293);
            this.panel4.TabIndex = 99;
            this.panel4.Tag = "panel4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(129, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Confirmación";
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(208, 258);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 8;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_cobrar
            // 
            this.btn_cobrar.Location = new System.Drawing.Point(299, 258);
            this.btn_cobrar.Name = "btn_cobrar";
            this.btn_cobrar.Size = new System.Drawing.Size(75, 23);
            this.btn_cobrar.TabIndex = 1;
            this.btn_cobrar.Text = "Cobrar";
            this.btn_cobrar.UseVisualStyleBackColor = true;
            this.btn_cobrar.Click += new System.EventHandler(this.btn_cobrar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(126, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Importe:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Comisión:";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Número Cel:";
            // 
            // txt_importe
            // 
            this.txt_importe.Location = new System.Drawing.Point(177, 179);
            this.txt_importe.Name = "txt_importe";
            this.txt_importe.ReadOnly = true;
            this.txt_importe.Size = new System.Drawing.Size(100, 20);
            this.txt_importe.TabIndex = 2;
            this.txt_importe.TabStop = false;
            // 
            // txt_comision
            // 
            this.txt_comision.Location = new System.Drawing.Point(71, 232);
            this.txt_comision.Name = "txt_comision";
            this.txt_comision.ReadOnly = true;
            this.txt_comision.Size = new System.Drawing.Size(100, 20);
            this.txt_comision.TabIndex = 1;
            this.txt_comision.TabStop = false;
            this.txt_comision.Visible = false;
            // 
            // txt_referencia
            // 
            this.txt_referencia.Location = new System.Drawing.Point(177, 141);
            this.txt_referencia.Name = "txt_referencia";
            this.txt_referencia.ReadOnly = true;
            this.txt_referencia.Size = new System.Drawing.Size(100, 20);
            this.txt_referencia.TabIndex = 0;
            this.txt_referencia.TabStop = false;
            // 
            // Tae_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 293);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tae_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Tae_principal_Load);
            this.Shown += new System.EventHandler(this.Tae_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_servicios_tae)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_prov_tae)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_servicios_tae;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txt_confirma_numero;
        private System.Windows.Forms.TextBox txt_numero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_prov_tae;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_prov_fabricante_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_prov_nombre;
        private System.Windows.Forms.Label lbl_servicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_proveedor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_importe;
        private System.Windows.Forms.TextBox txt_comision;
        private System.Windows.Forms.TextBox txt_referencia;
        private System.Windows.Forms.Button btn_cobrar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_serv_articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nom_fabricante;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_serv_fabricante_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_serv_denominacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_serv_sku;
    }
}