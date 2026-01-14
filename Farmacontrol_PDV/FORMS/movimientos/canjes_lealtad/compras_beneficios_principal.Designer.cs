namespace Farmacontrol_PDV.FORMS.movimientos.canjes_lealtad
{
    partial class compras_beneficios_principal
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
            this.txtBFolio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgvProductos = new System.Windows.Forms.DataGridView();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bntCancelar = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.btnConsultarfolio = new System.Windows.Forms.Button();
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.txtBTransaccion = new System.Windows.Forms.TextBox();
            this.txtNombreEmpleado = new System.Windows.Forms.TextBox();
            this.ProBarTermina = new System.Windows.Forms.ProgressBar();
            this.backGWorkerBeneficios = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "TICKET:";
            // 
            // txtBFolio
            // 
            this.txtBFolio.Location = new System.Drawing.Point(304, 13);
            this.txtBFolio.Name = "txtBFolio";
            this.txtBFolio.Size = new System.Drawing.Size(206, 20);
            this.txtBFolio.TabIndex = 1;
            this.txtBFolio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBFolio_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "TRANSACCION :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "TARJETA :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "EMPLEADO :";
            // 
            // dtgvProductos
            // 
            this.dtgvProductos.AllowUserToAddRows = false;
            this.dtgvProductos.AllowUserToDeleteRows = false;
            this.dtgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.codigo,
            this.descripcion,
            this.precio_unitario,
            this.cantidad,
            this.importe});
            this.dtgvProductos.Location = new System.Drawing.Point(12, 132);
            this.dtgvProductos.Name = "dtgvProductos";
            this.dtgvProductos.ReadOnly = true;
            this.dtgvProductos.Size = new System.Drawing.Size(740, 202);
            this.dtgvProductos.TabIndex = 5;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.ReadOnly = true;
            this.articulo_id.Visible = false;
            // 
            // codigo
            // 
            this.codigo.DataPropertyName = "codigo";
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 140;
            // 
            // descripcion
            // 
            this.descripcion.DataPropertyName = "descripcion";
            this.descripcion.HeaderText = "Descripcion";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 250;
            // 
            // precio_unitario
            // 
            this.precio_unitario.DataPropertyName = "precio_unitario";
            this.precio_unitario.HeaderText = "Precio unitario";
            this.precio_unitario.Name = "precio_unitario";
            this.precio_unitario.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            this.cantidad.HeaderText = "Piezas";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // importe
            // 
            this.importe.DataPropertyName = "importe";
            this.importe.HeaderText = "Importe";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            // 
            // bntCancelar
            // 
            this.bntCancelar.Location = new System.Drawing.Point(538, 362);
            this.bntCancelar.Name = "bntCancelar";
            this.bntCancelar.Size = new System.Drawing.Size(75, 23);
            this.bntCancelar.TabIndex = 6;
            this.bntCancelar.Text = "Cancelar";
            this.bntCancelar.UseVisualStyleBackColor = true;
            this.bntCancelar.Click += new System.EventHandler(this.bntCancelar_Click);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(634, 362);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(75, 23);
            this.btnFinalizar.TabIndex = 8;
            this.btnFinalizar.Text = "Terminar ";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // btnConsultarfolio
            // 
            this.btnConsultarfolio.Location = new System.Drawing.Point(529, 10);
            this.btnConsultarfolio.Name = "btnConsultarfolio";
            this.btnConsultarfolio.Size = new System.Drawing.Size(75, 23);
            this.btnConsultarfolio.TabIndex = 9;
            this.btnConsultarfolio.Text = "Consultar";
            this.btnConsultarfolio.UseVisualStyleBackColor = true;
            this.btnConsultarfolio.Click += new System.EventHandler(this.btnConsultarfolio_Click);
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTarjeta.Enabled = false;
            this.txtTarjeta.Location = new System.Drawing.Point(138, 58);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.ReadOnly = true;
            this.txtTarjeta.Size = new System.Drawing.Size(231, 13);
            this.txtTarjeta.TabIndex = 10;
            // 
            // txtBTransaccion
            // 
            this.txtBTransaccion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBTransaccion.Enabled = false;
            this.txtBTransaccion.Location = new System.Drawing.Point(138, 84);
            this.txtBTransaccion.Name = "txtBTransaccion";
            this.txtBTransaccion.ReadOnly = true;
            this.txtBTransaccion.Size = new System.Drawing.Size(231, 13);
            this.txtBTransaccion.TabIndex = 11;
            // 
            // txtNombreEmpleado
            // 
            this.txtNombreEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombreEmpleado.Enabled = false;
            this.txtNombreEmpleado.Location = new System.Drawing.Point(138, 113);
            this.txtNombreEmpleado.Name = "txtNombreEmpleado";
            this.txtNombreEmpleado.ReadOnly = true;
            this.txtNombreEmpleado.Size = new System.Drawing.Size(337, 13);
            this.txtNombreEmpleado.TabIndex = 12;
            // 
            // ProBarTermina
            // 
            this.ProBarTermina.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ProBarTermina.Location = new System.Drawing.Point(190, 184);
            this.ProBarTermina.Name = "ProBarTermina";
            this.ProBarTermina.Size = new System.Drawing.Size(401, 23);
            this.ProBarTermina.TabIndex = 13;
            this.ProBarTermina.Value = 1;
            this.ProBarTermina.Visible = false;
            // 
            // backGWorkerBeneficios
            // 
            this.backGWorkerBeneficios.WorkerReportsProgress = true;
            this.backGWorkerBeneficios.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backGWorkerBeneficios_DoWork);
            this.backGWorkerBeneficios.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backGWorkerBeneficios_ProgressChanged);
            this.backGWorkerBeneficios.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backGWorkerBeneficios_RunWorkerCompleted);
            // 
            // compras_beneficios_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 404);
            this.Controls.Add(this.ProBarTermina);
            this.Controls.Add(this.txtNombreEmpleado);
            this.Controls.Add(this.txtBTransaccion);
            this.Controls.Add(this.txtTarjeta);
            this.Controls.Add(this.btnConsultarfolio);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.bntCancelar);
            this.Controls.Add(this.dtgvProductos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBFolio);
            this.Controls.Add(this.label1);
            this.Name = "compras_beneficios_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras - beneficios";
            this.Load += new System.EventHandler(this.compras_beneficios_principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBFolio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dtgvProductos;
        private System.Windows.Forms.Button bntCancelar;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Button btnConsultarfolio;
        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.TextBox txtBTransaccion;
        private System.Windows.Forms.TextBox txtNombreEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe;
        private System.Windows.Forms.ProgressBar ProBarTermina;
        private System.ComponentModel.BackgroundWorker backGWorkerBeneficios;
    }
}