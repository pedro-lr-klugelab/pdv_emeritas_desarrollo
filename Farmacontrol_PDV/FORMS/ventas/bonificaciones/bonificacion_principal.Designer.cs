namespace Farmacontrol_PDV.FORMS.ventas.bonificaciones
{
    partial class bonificacion_principal
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxTicket = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txttarjeta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransaccion = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dgwbonificacion = new System.Windows.Forms.DataGridView();
            this.idproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nmbCantidad = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgwbonificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(487, 346);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(189, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Escanear Ticket  :";
            // 
            // txtbxTicket
            // 
            this.txtbxTicket.Location = new System.Drawing.Point(289, 22);
            this.txtbxTicket.Name = "txtbxTicket";
            this.txtbxTicket.Size = new System.Drawing.Size(200, 20);
            this.txtbxTicket.TabIndex = 2;
            this.txtbxTicket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbxTicket_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Escanear tarjeta :";
            // 
            // txttarjeta
            // 
            this.txttarjeta.Location = new System.Drawing.Point(141, 90);
            this.txttarjeta.MaxLength = 15;
            this.txttarjeta.Name = "txttarjeta";
            this.txttarjeta.Size = new System.Drawing.Size(217, 20);
            this.txttarjeta.TabIndex = 4;
            this.txttarjeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Transaccion : ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtTransaccion
            // 
            this.txtTransaccion.Location = new System.Drawing.Point(487, 90);
            this.txtTransaccion.MaxLength = 15;
            this.txtTransaccion.Name = "txtTransaccion";
            this.txtTransaccion.Size = new System.Drawing.Size(195, 20);
            this.txtTransaccion.TabIndex = 6;
            this.txtTransaccion.TextChanged += new System.EventHandler(this.txtTransaccion_TextChanged);
            this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransaccion_KeyPress);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(607, 346);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // dgwbonificacion
            // 
            this.dgwbonificacion.AllowUserToAddRows = false;
            this.dgwbonificacion.AllowUserToDeleteRows = false;
            this.dgwbonificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwbonificacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idproducto,
            this.Amecop,
            this.Descripcion,
            this.Cantidad});
            this.dgwbonificacion.Location = new System.Drawing.Point(48, 180);
            this.dgwbonificacion.Name = "dgwbonificacion";
            this.dgwbonificacion.ReadOnly = true;
            this.dgwbonificacion.Size = new System.Drawing.Size(634, 150);
            this.dgwbonificacion.TabIndex = 8;
            this.dgwbonificacion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgwbonificacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwbonificacion_KeyDown);
            // 
            // idproducto
            // 
            this.idproducto.DataPropertyName = "idproducto";
            this.idproducto.HeaderText = "idproducto";
            this.idproducto.Name = "idproducto";
            this.idproducto.ReadOnly = true;
            this.idproducto.Visible = false;
            this.idproducto.Width = 200;
            // 
            // Amecop
            // 
            this.Amecop.DataPropertyName = "amecop";
            this.Amecop.HeaderText = "Amecop";
            this.Amecop.Name = "Amecop";
            this.Amecop.ReadOnly = true;
            this.Amecop.Width = 200;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "descripcion";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 300;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "cantidad";
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 90;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Amecop";
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(48, 146);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(161, 20);
            this.txt_amecop.TabIndex = 10;
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtamecop_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(255, 146);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(273, 20);
            this.txtDescripcion.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(579, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Cantidad";
            // 
            // nmbCantidad
            // 
            this.nmbCantidad.Location = new System.Drawing.Point(582, 147);
            this.nmbCantidad.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbCantidad.Name = "nmbCantidad";
            this.nmbCantidad.Size = new System.Drawing.Size(100, 20);
            this.nmbCantidad.TabIndex = 15;
            this.nmbCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmbCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nmbCantidad_KeyDown);
            // 
            // bonificacion_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 388);
            this.Controls.Add(this.nmbCantidad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgwbonificacion);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtTransaccion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txttarjeta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbxTicket);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAceptar);
            this.MaximizeBox = false;
            this.Name = "bonificacion_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bonificaciones";
            this.Activated += new System.EventHandler(this.bonificacion_principal_Activated);
            this.Load += new System.EventHandler(this.bonificacion_principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwbonificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxTicket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txttarjeta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTransaccion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dgwbonificacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_amecop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nmbCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn idproducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
    }
}