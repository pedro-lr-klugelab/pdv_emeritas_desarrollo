namespace Farmacontrol_PDV.FORMS.reportes.reporte_letra
{
    partial class Reporte_letra_principal
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
            this.cbb_letras = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pct_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.row_grid_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad_sin_formato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.existencia_prepago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.existencia_vendible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_publico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_antibiotico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_imprimir = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.smi_imprimir = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_resultados = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.cms_imprimir.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbb_letras
            // 
            this.cbb_letras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_letras.FormattingEnabled = true;
            this.cbb_letras.Location = new System.Drawing.Point(129, 11);
            this.cbb_letras.MaxDropDownItems = 15;
            this.cbb_letras.MaxLength = 1;
            this.cbb_letras.Name = "cbb_letras";
            this.cbb_letras.Size = new System.Drawing.Size(100, 21);
            this.cbb_letras.TabIndex = 0;
            this.cbb_letras.SelectedIndexChanged += new System.EventHandler(this.cbb_letras_SelectedIndexChanged_1);
            this.cbb_letras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_letras_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione una letra:";
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(244, 11);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 2;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            this.btn_buscar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_buscar_MouseDown);
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
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
            this.activo,
            this.pct_descuento,
            this.precio_costo,
            this.row_grid_id,
            this.caducidad_sin_formato,
            this.articulo_id,
            this.amecop,
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
            this.existencia_prepago,
            this.existencia_vendible,
            this.precio_publico,
            this.es_antibiotico});
            this.dgv_articulos.ContextMenuStrip = this.cms_imprimir;
            this.dgv_articulos.Location = new System.Drawing.Point(12, 38);
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.ReadOnly = true;
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_articulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos.Size = new System.Drawing.Size(1072, 428);
            this.dgv_articulos.StandardTab = true;
            this.dgv_articulos.TabIndex = 13;
            this.dgv_articulos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_articulos_CellContentClick);
            // 
            // activo
            // 
            this.activo.DataPropertyName = "activo";
            this.activo.HeaderText = "activo";
            this.activo.Name = "activo";
            this.activo.ReadOnly = true;
            this.activo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.activo.Visible = false;
            // 
            // pct_descuento
            // 
            this.pct_descuento.DataPropertyName = "pct_descuento";
            this.pct_descuento.HeaderText = "pct_descuento";
            this.pct_descuento.Name = "pct_descuento";
            this.pct_descuento.ReadOnly = true;
            this.pct_descuento.Visible = false;
            // 
            // precio_costo
            // 
            this.precio_costo.DataPropertyName = "precio_costo";
            this.precio_costo.HeaderText = "precio_costo";
            this.precio_costo.Name = "precio_costo";
            this.precio_costo.ReadOnly = true;
            this.precio_costo.Visible = false;
            // 
            // row_grid_id
            // 
            this.row_grid_id.DataPropertyName = "row_grid_id";
            this.row_grid_id.HeaderText = "row_grid_id";
            this.row_grid_id.Name = "row_grid_id";
            this.row_grid_id.ReadOnly = true;
            this.row_grid_id.Visible = false;
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
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 36.39032F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            this.amecop.ReadOnly = true;
            this.amecop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "nombre";
            this.producto.FillWeight = 85.12419F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            this.producto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 31.64375F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            this.caducidad.ReadOnly = true;
            this.caducidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.FillWeight = 42.71907F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            this.lote.ReadOnly = true;
            this.lote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // total
            // 
            this.total.DataPropertyName = "existencia_total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.total.DefaultCellStyle = dataGridViewCellStyle5;
            this.total.FillWeight = 33.95935F;
            this.total.HeaderText = "Existencia";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_ventas
            // 
            this.existencia_ventas.DataPropertyName = "existencia_ventas";
            this.existencia_ventas.FillWeight = 20.36948F;
            this.existencia_ventas.HeaderText = "VTA";
            this.existencia_ventas.Name = "existencia_ventas";
            this.existencia_ventas.ReadOnly = true;
            this.existencia_ventas.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.existencia_ventas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // existencia_devoluciones
            // 
            this.existencia_devoluciones.DataPropertyName = "existencia_devoluciones";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "-";
            this.existencia_devoluciones.DefaultCellStyle = dataGridViewCellStyle6;
            this.existencia_devoluciones.FillWeight = 20.56844F;
            this.existencia_devoluciones.HeaderText = "DEV";
            this.existencia_devoluciones.Name = "existencia_devoluciones";
            this.existencia_devoluciones.ReadOnly = true;
            this.existencia_devoluciones.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_mermas
            // 
            this.existencia_mermas.DataPropertyName = "existencia_mermas";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = "-";
            this.existencia_mermas.DefaultCellStyle = dataGridViewCellStyle7;
            this.existencia_mermas.FillWeight = 20.56844F;
            this.existencia_mermas.HeaderText = "MER";
            this.existencia_mermas.Name = "existencia_mermas";
            this.existencia_mermas.ReadOnly = true;
            this.existencia_mermas.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_cambio_fisico
            // 
            this.existencia_cambio_fisico.DataPropertyName = "existencia_cambio_fisico";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "-";
            this.existencia_cambio_fisico.DefaultCellStyle = dataGridViewCellStyle8;
            this.existencia_cambio_fisico.FillWeight = 20.56844F;
            this.existencia_cambio_fisico.HeaderText = "CBF";
            this.existencia_cambio_fisico.Name = "existencia_cambio_fisico";
            this.existencia_cambio_fisico.ReadOnly = true;
            this.existencia_cambio_fisico.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_apartados
            // 
            this.existencia_apartados.DataPropertyName = "existencia_apartados";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = "-";
            this.existencia_apartados.DefaultCellStyle = dataGridViewCellStyle9;
            this.existencia_apartados.FillWeight = 20.56844F;
            this.existencia_apartados.HeaderText = "APT";
            this.existencia_apartados.Name = "existencia_apartados";
            this.existencia_apartados.ReadOnly = true;
            this.existencia_apartados.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_traspasos
            // 
            this.existencia_traspasos.DataPropertyName = "existencia_traspasos";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.NullValue = "-";
            this.existencia_traspasos.DefaultCellStyle = dataGridViewCellStyle10;
            this.existencia_traspasos.FillWeight = 20.56844F;
            this.existencia_traspasos.HeaderText = "TRA";
            this.existencia_traspasos.Name = "existencia_traspasos";
            this.existencia_traspasos.ReadOnly = true;
            this.existencia_traspasos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_mayoreo
            // 
            this.existencia_mayoreo.DataPropertyName = "existencia_mayoreo";
            this.existencia_mayoreo.FillWeight = 20.36948F;
            this.existencia_mayoreo.HeaderText = "MAY";
            this.existencia_mayoreo.Name = "existencia_mayoreo";
            this.existencia_mayoreo.ReadOnly = true;
            this.existencia_mayoreo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_prepago
            // 
            this.existencia_prepago.DataPropertyName = "existencia_prepago";
            this.existencia_prepago.FillWeight = 20.36948F;
            this.existencia_prepago.HeaderText = "EPP";
            this.existencia_prepago.Name = "existencia_prepago";
            this.existencia_prepago.ReadOnly = true;
            this.existencia_prepago.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // existencia_vendible
            // 
            this.existencia_vendible.DataPropertyName = "existencia_vendible";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.existencia_vendible.DefaultCellStyle = dataGridViewCellStyle11;
            this.existencia_vendible.FillWeight = 20.56844F;
            this.existencia_vendible.HeaderText = "VEN";
            this.existencia_vendible.Name = "existencia_vendible";
            this.existencia_vendible.ReadOnly = true;
            this.existencia_vendible.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // precio_publico
            // 
            this.precio_publico.DataPropertyName = "precio_publico";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "C2";
            dataGridViewCellStyle12.NullValue = null;
            this.precio_publico.DefaultCellStyle = dataGridViewCellStyle12;
            this.precio_publico.FillWeight = 31.64375F;
            this.precio_publico.HeaderText = "Precio Publico";
            this.precio_publico.Name = "precio_publico";
            this.precio_publico.ReadOnly = true;
            this.precio_publico.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // es_antibiotico
            // 
            this.es_antibiotico.DataPropertyName = "es_antibiotico";
            this.es_antibiotico.HeaderText = "es_antibiotico";
            this.es_antibiotico.Name = "es_antibiotico";
            this.es_antibiotico.ReadOnly = true;
            this.es_antibiotico.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.es_antibiotico.Visible = false;
            // 
            // cms_imprimir
            // 
            this.cms_imprimir.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smi_imprimir});
            this.cms_imprimir.Name = "cms_imprimir";
            this.cms_imprimir.Size = new System.Drawing.Size(121, 26);
            // 
            // smi_imprimir
            // 
            this.smi_imprimir.Name = "smi_imprimir";
            this.smi_imprimir.Size = new System.Drawing.Size(120, 22);
            this.smi_imprimir.Text = "Imprimir";
            this.smi_imprimir.Click += new System.EventHandler(this.smi_imprimir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 477);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Resultados encontrados:";
            // 
            // txt_resultados
            // 
            this.txt_resultados.Location = new System.Drawing.Point(136, 474);
            this.txt_resultados.Name = "txt_resultados";
            this.txt_resultados.ReadOnly = true;
            this.txt_resultados.Size = new System.Drawing.Size(74, 20);
            this.txt_resultados.TabIndex = 15;
            // 
            // Reporte_letra_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 499);
            this.Controls.Add(this.txt_resultados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv_articulos);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbb_letras);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reporte_letra_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte por letra";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Reporte_letra_principal_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.cms_imprimir.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbb_letras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.DataGridView dgv_articulos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_resultados;
        private System.Windows.Forms.ContextMenuStrip cms_imprimir;
        private System.Windows.Forms.ToolStripMenuItem smi_imprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn activo;
        private System.Windows.Forms.DataGridViewTextBoxColumn pct_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_costo;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_grid_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad_sin_formato;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_prepago;
        private System.Windows.Forms.DataGridViewTextBoxColumn existencia_vendible;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_publico;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_antibiotico;
    }
}