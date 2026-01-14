namespace Farmacontrol_PDV.FORMS.movimientos.fanasa_fan
{
    partial class venta_beneficios_soyfan
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolioVenta = new System.Windows.Forms.TextBox();
            this.dvgVentas = new System.Windows.Forms.DataGridView();
            this.btnAtras = new System.Windows.Forms.Button();
            this.btnTerminar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAutorizacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dvgVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio : ";
            // 
            // txtFolioVenta
            // 
            this.txtFolioVenta.Enabled = false;
            this.txtFolioVenta.Location = new System.Drawing.Point(101, 42);
            this.txtFolioVenta.Name = "txtFolioVenta";
            this.txtFolioVenta.ReadOnly = true;
            this.txtFolioVenta.Size = new System.Drawing.Size(144, 20);
            this.txtFolioVenta.TabIndex = 1;
            // 
            // dvgVentas
            // 
            this.dvgVentas.AllowUserToAddRows = false;
            this.dvgVentas.AllowUserToDeleteRows = false;
            this.dvgVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.codigo,
            this.descripcion,
            this.precio_unitario,
            this.cantidad,
            this.importe});
            this.dvgVentas.Location = new System.Drawing.Point(12, 130);
            this.dvgVentas.Name = "dvgVentas";
            this.dvgVentas.ReadOnly = true;
            this.dvgVentas.Size = new System.Drawing.Size(1045, 251);
            this.dvgVentas.TabIndex = 2;
            this.dvgVentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgVentas_CellContentClick);
            // 
            // btnAtras
            // 
            this.btnAtras.Location = new System.Drawing.Point(820, 415);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(75, 23);
            this.btnAtras.TabIndex = 3;
            this.btnAtras.Text = "Atras";
            this.btnAtras.UseVisualStyleBackColor = true;
            this.btnAtras.Click += new System.EventHandler(this.btnAtras_Click);
            // 
            // btnTerminar
            // 
            this.btnTerminar.Location = new System.Drawing.Point(901, 415);
            this.btnTerminar.Name = "btnTerminar";
            this.btnTerminar.Size = new System.Drawing.Size(75, 23);
            this.btnTerminar.TabIndex = 4;
            this.btnTerminar.Text = "Terminar";
            this.btnTerminar.UseVisualStyleBackColor = true;
            this.btnTerminar.Click += new System.EventHandler(this.btnTerminar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(982, 415);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(276, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Autorización";
            // 
            // txtAutorizacion
            // 
            this.txtAutorizacion.Enabled = false;
            this.txtAutorizacion.Location = new System.Drawing.Point(394, 42);
            this.txtAutorizacion.Name = "txtAutorizacion";
            this.txtAutorizacion.ReadOnly = true;
            this.txtAutorizacion.Size = new System.Drawing.Size(144, 20);
            this.txtAutorizacion.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(559, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tarjeta :";
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.Enabled = false;
            this.txtTarjeta.Location = new System.Drawing.Point(631, 42);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.ReadOnly = true;
            this.txtTarjeta.Size = new System.Drawing.Size(159, 20);
            this.txtTarjeta.TabIndex = 9;
            // 
            // articulo_id
            // 
            this.articulo_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // codigo
            // 
            this.codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.codigo.DataPropertyName = "codigo";
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 180;
            // 
            // descripcion
            // 
            this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.descripcion.DataPropertyName = "descripcion";
            this.descripcion.HeaderText = "Descripcion";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 300;
            // 
            // precio_unitario
            // 
            this.precio_unitario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.precio_unitario.DataPropertyName = "precio_unitario";
            this.precio_unitario.HeaderText = "Precio";
            this.precio_unitario.Name = "precio_unitario";
            this.precio_unitario.ReadOnly = true;
            this.precio_unitario.Width = 160;
            // 
            // cantidad
            // 
            this.cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cantidad.DataPropertyName = "cantidad";
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 150;
            // 
            // importe
            // 
            this.importe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.importe.DataPropertyName = "importe";
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            this.importe.Width = 210;
            // 
            // venta_beneficios_soyfan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 477);
            this.ControlBox = false;
            this.Controls.Add(this.txtTarjeta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAutorizacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnTerminar);
            this.Controls.Add(this.btnAtras);
            this.Controls.Add(this.dvgVentas);
            this.Controls.Add(this.txtFolioVenta);
            this.Controls.Add(this.label1);
            this.Name = "venta_beneficios_soyfan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dvgVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFolioVenta;
        private System.Windows.Forms.DataGridView dvgVentas;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Button btnTerminar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAutorizacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe;
    }
}