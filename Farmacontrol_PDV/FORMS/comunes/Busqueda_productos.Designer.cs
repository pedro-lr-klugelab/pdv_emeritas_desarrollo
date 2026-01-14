namespace Farmacontrol_PDV.FORMS.comunes
{
    partial class Busqueda_productos
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_producto = new System.Windows.Forms.Label();
            this.txt_nombre_producto = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progBarBusqueda = new System.Windows.Forms.ProgressBar();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_pct_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad_sin_formato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_ventas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_devoluciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_mermas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_cambio_fisico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_apartados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_traspasos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_mayoreo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_parcial_prepago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_vendible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_antibiotico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submenu_dgv_productos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rastrear_piezas_uso = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_time = new System.Windows.Forms.Label();
            this.chb_inactivos = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.submenu_dgv_productos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_producto
            // 
            this.lbl_producto.AutoSize = true;
            this.lbl_producto.Location = new System.Drawing.Point(13, 13);
            this.lbl_producto.Name = "lbl_producto";
            this.lbl_producto.Size = new System.Drawing.Size(53, 13);
            this.lbl_producto.TabIndex = 0;
            this.lbl_producto.Text = "Producto:";
            // 
            // txt_nombre_producto
            // 
            this.txt_nombre_producto.Location = new System.Drawing.Point(67, 10);
            this.txt_nombre_producto.Name = "txt_nombre_producto";
            this.txt_nombre_producto.Size = new System.Drawing.Size(389, 20);
            this.txt_nombre_producto.TabIndex = 1;
            this.txt_nombre_producto.TextChanged += new System.EventHandler(this.txt_nombre_producto_TextChanged);
            this.txt_nombre_producto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_producto_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progBarBusqueda);
            this.panel1.Controls.Add(this.dgv_articulos);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1223, 404);
            this.panel1.TabIndex = 5;
            // 
            // progBarBusqueda
            // 
            this.progBarBusqueda.AccessibleDescription = "";
            this.progBarBusqueda.AccessibleName = "espere";
            this.progBarBusqueda.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.progBarBusqueda.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.progBarBusqueda.Location = new System.Drawing.Point(359, 179);
            this.progBarBusqueda.Name = "progBarBusqueda";
            this.progBarBusqueda.Size = new System.Drawing.Size(511, 23);
            this.progBarBusqueda.TabIndex = 3;
            this.progBarBusqueda.Value = 10;
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AllowUserToResizeColumns = false;
            this.dgv_articulos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_articulos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_articulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.amecop,
            this.col_pct_descuento,
            this.c_activo,
            this.caducidad_sin_formato,
            this.articulo_id,
            this.producto,
            this.caducidad,
            this.lote,
            this.total,
            this.existencia_ventas,
            this.existencia_devoluciones,
            this.existencia_mermas,
            this.existencia_cambio_fisico,
            this.existencia_apartados,
            this.existencia_traspasos,
            this.existencia_mayoreo,
            this.existencia_parcial_prepago,
            this.existencia_vendible,
            this.es_antibiotico,
            this.precio_publico});
            this.dgv_articulos.ContextMenuStrip = this.submenu_dgv_productos;
            this.dgv_articulos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_articulos.Location = new System.Drawing.Point(0, 0);
            this.dgv_articulos.MultiSelect = false;
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(1223, 404);
            this.dgv_articulos.StandardTab = true;
            this.dgv_articulos.TabIndex = 2;
            this.dgv_articulos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_articulos_CellContentClick);
            this.dgv_articulos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_articulos_CellFormatting);
            this.dgv_articulos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dgv_articulos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_articulos_KeyDown);
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 115F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            // 
            // col_pct_descuento
            // 
            this.col_pct_descuento.DataPropertyName = "pct_descuento";
            this.col_pct_descuento.HeaderText = "col_pct_descuento";
            this.col_pct_descuento.Name = "col_pct_descuento";
            this.col_pct_descuento.ReadOnly = true;
            this.col_pct_descuento.Visible = false;
            // 
            // c_activo
            // 
            this.c_activo.DataPropertyName = "activo";
            this.c_activo.HeaderText = "c_activo";
            this.c_activo.Name = "c_activo";
            this.c_activo.ReadOnly = true;
            this.c_activo.Visible = false;
            // 
            // caducidad_sin_formato
            // 
            this.caducidad_sin_formato.DataPropertyName = "caducidad_sin_formato";
            this.caducidad_sin_formato.HeaderText = "caducidad_sin_formato";
            this.caducidad_sin_formato.Name = "caducidad_sin_formato";
            this.caducidad_sin_formato.ReadOnly = true;
            this.caducidad_sin_formato.Visible = false;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "nombre";
            this.producto.FillWeight = 300F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.FillWeight = 135F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            this.lote.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "existencia_total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.total.DefaultCellStyle = dataGridViewCellStyle5;
            this.total.FillWeight = 65F;
            this.total.HeaderText = "TOT";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // existencia_ventas
            // 
            this.existencia_ventas.DataPropertyName = "existencia_ventas";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_ventas.DefaultCellStyle = dataGridViewCellStyle6;
            this.existencia_ventas.FillWeight = 65F;
            this.existencia_ventas.HeaderText = "VTA";
            this.existencia_ventas.Name = "existencia_ventas";
            this.existencia_ventas.ReadOnly = true;
            // 
            // existencia_devoluciones
            // 
            this.existencia_devoluciones.DataPropertyName = "existencia_devoluciones";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_devoluciones.DefaultCellStyle = dataGridViewCellStyle7;
            this.existencia_devoluciones.FillWeight = 65F;
            this.existencia_devoluciones.HeaderText = "DEV";
            this.existencia_devoluciones.Name = "existencia_devoluciones";
            this.existencia_devoluciones.ReadOnly = true;
            // 
            // existencia_mermas
            // 
            this.existencia_mermas.DataPropertyName = "existencia_mermas";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_mermas.DefaultCellStyle = dataGridViewCellStyle8;
            this.existencia_mermas.FillWeight = 65F;
            this.existencia_mermas.HeaderText = "MER";
            this.existencia_mermas.Name = "existencia_mermas";
            this.existencia_mermas.ReadOnly = true;
            // 
            // existencia_cambio_fisico
            // 
            this.existencia_cambio_fisico.DataPropertyName = "existencia_cambio_fisico";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_cambio_fisico.DefaultCellStyle = dataGridViewCellStyle9;
            this.existencia_cambio_fisico.FillWeight = 65F;
            this.existencia_cambio_fisico.HeaderText = "CBF";
            this.existencia_cambio_fisico.Name = "existencia_cambio_fisico";
            this.existencia_cambio_fisico.ReadOnly = true;
            // 
            // existencia_apartados
            // 
            this.existencia_apartados.DataPropertyName = "existencia_apartados";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_apartados.DefaultCellStyle = dataGridViewCellStyle10;
            this.existencia_apartados.FillWeight = 65F;
            this.existencia_apartados.HeaderText = "APT";
            this.existencia_apartados.Name = "existencia_apartados";
            this.existencia_apartados.ReadOnly = true;
            // 
            // existencia_traspasos
            // 
            this.existencia_traspasos.DataPropertyName = "existencia_traspasos";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_traspasos.DefaultCellStyle = dataGridViewCellStyle11;
            this.existencia_traspasos.FillWeight = 65F;
            this.existencia_traspasos.HeaderText = "TRA";
            this.existencia_traspasos.Name = "existencia_traspasos";
            this.existencia_traspasos.ReadOnly = true;
            // 
            // existencia_mayoreo
            // 
            this.existencia_mayoreo.DataPropertyName = "existencia_mayoreo";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_mayoreo.DefaultCellStyle = dataGridViewCellStyle12;
            this.existencia_mayoreo.FillWeight = 65F;
            this.existencia_mayoreo.HeaderText = "MAY";
            this.existencia_mayoreo.Name = "existencia_mayoreo";
            this.existencia_mayoreo.ReadOnly = true;
            // 
            // existencia_parcial_prepago
            // 
            this.existencia_parcial_prepago.DataPropertyName = "existencia_prepago";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_parcial_prepago.DefaultCellStyle = dataGridViewCellStyle13;
            this.existencia_parcial_prepago.FillWeight = 65F;
            this.existencia_parcial_prepago.HeaderText = "EPP";
            this.existencia_parcial_prepago.Name = "existencia_parcial_prepago";
            this.existencia_parcial_prepago.ReadOnly = true;
            // 
            // existencia_vendible
            // 
            this.existencia_vendible.DataPropertyName = "existencia_vendible";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_vendible.DefaultCellStyle = dataGridViewCellStyle14;
            this.existencia_vendible.FillWeight = 65F;
            this.existencia_vendible.HeaderText = "VEN";
            this.existencia_vendible.Name = "existencia_vendible";
            this.existencia_vendible.ReadOnly = true;
            // 
            // es_antibiotico
            // 
            this.es_antibiotico.DataPropertyName = "es_antibiotico";
            this.es_antibiotico.HeaderText = "es_antibiotico";
            this.es_antibiotico.Name = "es_antibiotico";
            this.es_antibiotico.ReadOnly = true;
            this.es_antibiotico.Visible = false;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "C2";
            dataGridViewCellStyle15.NullValue = null;
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle15;
            this.precio_publico.HeaderText = "Precio Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            // 
            // submenu_dgv_productos
            // 
            this.submenu_dgv_productos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rastrear_piezas_uso});
            this.submenu_dgv_productos.Name = "submenu_dgv_productos";
            this.submenu_dgv_productos.Size = new System.Drawing.Size(215, 26);
            // 
            // rastrear_piezas_uso
            // 
            this.rastrear_piezas_uso.Name = "rastrear_piezas_uso";
            this.rastrear_piezas_uso.Size = new System.Drawing.Size(214, 22);
            this.rastrear_piezas_uso.Text = "RASTREAR PIEZAS EN USO";
            this.rastrear_piezas_uso.Click += new System.EventHandler(this.rastrear_piezas_uso_Click);
            // 
            // lbl_time
            // 
            this.lbl_time.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl_time.Location = new System.Drawing.Point(14, 456);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(256, 13);
            this.lbl_time.TabIndex = 6;
            // 
            // chb_inactivos
            // 
            this.chb_inactivos.AutoSize = true;
            this.chb_inactivos.Enabled = false;
            this.chb_inactivos.Location = new System.Drawing.Point(462, 12);
            this.chb_inactivos.Name = "chb_inactivos";
            this.chb_inactivos.Size = new System.Drawing.Size(69, 17);
            this.chb_inactivos.TabIndex = 7;
            this.chb_inactivos.Text = "Inactivos";
            this.chb_inactivos.UseVisualStyleBackColor = true;
            this.chb_inactivos.CheckedChanged += new System.EventHandler(this.chb_inactivos_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // Busqueda_productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1247, 481);
            this.Controls.Add(this.chb_inactivos);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt_nombre_producto);
            this.Controls.Add(this.lbl_producto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Busqueda_productos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Busqueda de Productos";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Busqueda_productos_FormClosing);
            this.Load += new System.EventHandler(this.Busqueda_productos_Load);
            this.Shown += new System.EventHandler(this.Busqueda_productos_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.submenu_dgv_productos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_producto;
		private System.Windows.Forms.TextBox txt_nombre_producto;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.Label lbl_time;
		private System.Windows.Forms.CheckBox chb_inactivos;
		private System.Windows.Forms.ContextMenuStrip submenu_dgv_productos;
        private System.Windows.Forms.ToolStripMenuItem rastrear_piezas_uso;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_pct_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad_sin_formato;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_ventas;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_devoluciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_mermas;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_cambio_fisico;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_apartados;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_traspasos;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_mayoreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_parcial_prepago;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_vendible;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_antibiotico;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
        private System.Windows.Forms.ProgressBar progBarBusqueda;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}