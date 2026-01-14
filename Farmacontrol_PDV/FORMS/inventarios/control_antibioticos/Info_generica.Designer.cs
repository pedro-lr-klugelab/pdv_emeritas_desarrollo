namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Info_generica
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_fecha = new System.Windows.Forms.TextBox();
            this.txt_folio = new System.Windows.Forms.TextBox();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.dgv_info_generica = new System.Windows.Forms.DataGridView();
            this.es_antibiotico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_elemento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contiene_controlados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cbb_tipos_movimiento = new System.Windows.Forms.ComboBox();
            this.lbl_folio = new System.Windows.Forms.Label();
            this.txt_busqueda = new System.Windows.Forms.TextBox();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.btn_receta = new System.Windows.Forms.Button();
            this.btn_anadir_receta = new System.Windows.Forms.Button();
            this.btn_desasociar_receta = new System.Windows.Forms.Button();
            this.lbl_mensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_info_generica)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Folio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Comentarios:";
            // 
            // txt_fecha
            // 
            this.txt_fecha.Location = new System.Drawing.Point(87, 38);
            this.txt_fecha.Name = "txt_fecha";
            this.txt_fecha.ReadOnly = true;
            this.txt_fecha.Size = new System.Drawing.Size(169, 20);
            this.txt_fecha.TabIndex = 0;
            this.txt_fecha.TabStop = false;
            // 
            // txt_folio
            // 
            this.txt_folio.Location = new System.Drawing.Point(87, 64);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.ReadOnly = true;
            this.txt_folio.Size = new System.Drawing.Size(169, 20);
            this.txt_folio.TabIndex = 0;
            this.txt_folio.TabStop = false;
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Location = new System.Drawing.Point(87, 90);
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.ReadOnly = true;
            this.txt_comentarios.Size = new System.Drawing.Size(578, 20);
            this.txt_comentarios.TabIndex = 0;
            this.txt_comentarios.TabStop = false;
            // 
            // dgv_info_generica
            // 
            this.dgv_info_generica.AllowUserToAddRows = false;
            this.dgv_info_generica.AllowUserToDeleteRows = false;
            this.dgv_info_generica.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_info_generica.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_info_generica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_info_generica.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_info_generica.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_info_generica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_info_generica.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.es_antibiotico,
            this.receta_id,
            this.dgv_movimiento,
            this.dgv_elemento_id,
            this.articulo_id,
            this.contiene_controlados,
            this.amecop,
            this.producto,
            this.caducidad,
            this.lote,
            this.cantidad,
            this.check});
            this.dgv_info_generica.Location = new System.Drawing.Point(12, 116);
            this.dgv_info_generica.MultiSelect = false;
            this.dgv_info_generica.Name = "dgv_info_generica";
            this.dgv_info_generica.RowHeadersVisible = false;
            this.dgv_info_generica.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_info_generica.Size = new System.Drawing.Size(653, 325);
            this.dgv_info_generica.TabIndex = 4;
            // 
            // es_antibiotico
            // 
            this.es_antibiotico.DataPropertyName = "es_antibiotico";
            this.es_antibiotico.HeaderText = "es_antibiotico";
            this.es_antibiotico.Name = "es_antibiotico";
            this.es_antibiotico.Visible = false;
            // 
            // receta_id
            // 
            this.receta_id.DataPropertyName = "receta_id";
            this.receta_id.HeaderText = "receta_id";
            this.receta_id.Name = "receta_id";
            this.receta_id.Visible = false;
            // 
            // dgv_movimiento
            // 
            this.dgv_movimiento.DataPropertyName = "movimiento";
            this.dgv_movimiento.HeaderText = "dgv_movimiento";
            this.dgv_movimiento.Name = "dgv_movimiento";
            this.dgv_movimiento.Visible = false;
            // 
            // dgv_elemento_id
            // 
            this.dgv_elemento_id.DataPropertyName = "elemento_id";
            this.dgv_elemento_id.HeaderText = "dgv_elemento_id";
            this.dgv_elemento_id.Name = "dgv_elemento_id";
            this.dgv_elemento_id.Visible = false;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.Visible = false;
            // 
            // contiene_controlados
            // 
            this.contiene_controlados.DataPropertyName = "contiene_controlados";
            this.contiene_controlados.HeaderText = "contiene_controlados";
            this.contiene_controlados.Name = "contiene_controlados";
            this.contiene_controlados.Visible = false;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 65F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 170F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            this.caducidad.FillWeight = 60F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            this.lote.FillWeight = 70F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            this.cantidad.FillWeight = 40F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // check
            // 
            this.check.DataPropertyName = "check";
            this.check.FillWeight = 50F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.check.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cbb_tipos_movimiento
            // 
            this.cbb_tipos_movimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_tipos_movimiento.Enabled = false;
            this.cbb_tipos_movimiento.FormattingEnabled = true;
            this.cbb_tipos_movimiento.Location = new System.Drawing.Point(87, 11);
            this.cbb_tipos_movimiento.MaxDropDownItems = 15;
            this.cbb_tipos_movimiento.MaxLength = 1;
            this.cbb_tipos_movimiento.Name = "cbb_tipos_movimiento";
            this.cbb_tipos_movimiento.Size = new System.Drawing.Size(169, 21);
            this.cbb_tipos_movimiento.TabIndex = 1;
            this.cbb_tipos_movimiento.SelectedIndexChanged += new System.EventHandler(this.cbb_tipos_movimiento_SelectedIndexChanged);
            // 
            // lbl_folio
            // 
            this.lbl_folio.AutoSize = true;
            this.lbl_folio.Location = new System.Drawing.Point(272, 13);
            this.lbl_folio.Name = "lbl_folio";
            this.lbl_folio.Size = new System.Drawing.Size(32, 13);
            this.lbl_folio.TabIndex = 6;
            this.lbl_folio.Text = "Folio:";
            this.lbl_folio.Visible = false;
            // 
            // txt_busqueda
            // 
            this.txt_busqueda.Enabled = false;
            this.txt_busqueda.Location = new System.Drawing.Point(310, 11);
            this.txt_busqueda.Name = "txt_busqueda";
            this.txt_busqueda.Size = new System.Drawing.Size(92, 20);
            this.txt_busqueda.TabIndex = 7;
            this.txt_busqueda.Visible = false;
            this.txt_busqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_busqueda_KeyDown);
            // 
            // btn_buscar
            // 
            this.btn_buscar.Enabled = false;
            this.btn_buscar.Location = new System.Drawing.Point(408, 10);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 8;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Visible = false;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.Location = new System.Drawing.Point(12, 447);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(75, 23);
            this.btn_cerrar.TabIndex = 9;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = true;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Enabled = false;
            this.btn_guardar.Location = new System.Drawing.Point(590, 447);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 10;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Visible = false;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // btn_receta
            // 
            this.btn_receta.Enabled = false;
            this.btn_receta.Location = new System.Drawing.Point(489, 10);
            this.btn_receta.Name = "btn_receta";
            this.btn_receta.Size = new System.Drawing.Size(75, 23);
            this.btn_receta.TabIndex = 11;
            this.btn_receta.Text = "Ver Receta";
            this.btn_receta.UseVisualStyleBackColor = true;
            this.btn_receta.Visible = false;
            this.btn_receta.Click += new System.EventHandler(this.btn_receta_Click);
            // 
            // btn_anadir_receta
            // 
            this.btn_anadir_receta.Enabled = false;
            this.btn_anadir_receta.Location = new System.Drawing.Point(489, 447);
            this.btn_anadir_receta.Name = "btn_anadir_receta";
            this.btn_anadir_receta.Size = new System.Drawing.Size(95, 23);
            this.btn_anadir_receta.TabIndex = 12;
            this.btn_anadir_receta.Text = "Añadir Receta";
            this.btn_anadir_receta.UseVisualStyleBackColor = true;
            this.btn_anadir_receta.Visible = false;
            this.btn_anadir_receta.Click += new System.EventHandler(this.btn_anadir_receta_Click);
            // 
            // btn_desasociar_receta
            // 
            this.btn_desasociar_receta.Enabled = false;
            this.btn_desasociar_receta.Location = new System.Drawing.Point(374, 447);
            this.btn_desasociar_receta.Name = "btn_desasociar_receta";
            this.btn_desasociar_receta.Size = new System.Drawing.Size(109, 23);
            this.btn_desasociar_receta.TabIndex = 13;
            this.btn_desasociar_receta.Text = "Reiniciar Proceso";
            this.btn_desasociar_receta.UseVisualStyleBackColor = true;
            this.btn_desasociar_receta.Visible = false;
            this.btn_desasociar_receta.Click += new System.EventHandler(this.btn_desasociar_receta_Click);
            // 
            // lbl_mensaje
            // 
            this.lbl_mensaje.AutoSize = true;
            this.lbl_mensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mensaje.ForeColor = System.Drawing.Color.DarkRed;
            this.lbl_mensaje.Location = new System.Drawing.Point(272, 45);
            this.lbl_mensaje.Name = "lbl_mensaje";
            this.lbl_mensaje.Size = new System.Drawing.Size(199, 13);
            this.lbl_mensaje.TabIndex = 14;
            this.lbl_mensaje.Text = "Escanear ticket de venta o capturar folio";
            // 
            // Info_generica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 476);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_mensaje);
            this.Controls.Add(this.btn_desasociar_receta);
            this.Controls.Add(this.btn_anadir_receta);
            this.Controls.Add(this.btn_receta);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.txt_busqueda);
            this.Controls.Add(this.lbl_folio);
            this.Controls.Add(this.cbb_tipos_movimiento);
            this.Controls.Add(this.dgv_info_generica);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.txt_folio);
            this.Controls.Add(this.txt_fecha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Info_generica";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información Complementaria";
            this.Load += new System.EventHandler(this.Info_generica_Load);
            this.Shown += new System.EventHandler(this.Info_generica_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_info_generica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_fecha;
        private System.Windows.Forms.TextBox txt_folio;
        private System.Windows.Forms.TextBox txt_comentarios;
        private System.Windows.Forms.DataGridView dgv_info_generica;
        private System.Windows.Forms.ComboBox cbb_tipos_movimiento;
        private System.Windows.Forms.Label lbl_folio;
        private System.Windows.Forms.TextBox txt_busqueda;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Button btn_cerrar;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Button btn_receta;
        private System.Windows.Forms.Button btn_anadir_receta;
        private System.Windows.Forms.Button btn_desasociar_receta;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_antibiotico;
        private System.Windows.Forms.DataGridViewTextBoxColumn receta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_movimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_elemento_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn contiene_controlados;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.Label lbl_mensaje;
    }
}