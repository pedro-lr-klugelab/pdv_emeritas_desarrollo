namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Recetas
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_folio = new System.Windows.Forms.TextBox();
            this.txt_doctor = new System.Windows.Forms.TextBox();
            this.txt_cedula = new System.Windows.Forms.TextBox();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.dgv_articulos_receta = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgb_movimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_antibiotico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contiene_controlados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elemento_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos_receta)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio de la Receta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Doctor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cédula:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dirección consultorio:";
            // 
            // txt_folio
            // 
            this.txt_folio.Location = new System.Drawing.Point(127, 11);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.Size = new System.Drawing.Size(100, 20);
            this.txt_folio.TabIndex = 1;
            this.txt_folio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_folio_KeyDown);
            this.txt_folio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_folio_KeyPress);
            // 
            // txt_doctor
            // 
            this.txt_doctor.Location = new System.Drawing.Point(127, 37);
            this.txt_doctor.Name = "txt_doctor";
            this.txt_doctor.Size = new System.Drawing.Size(453, 20);
            this.txt_doctor.TabIndex = 2;
            this.txt_doctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_doctor_KeyDown);
            this.txt_doctor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_doctor_KeyPress);
            // 
            // txt_cedula
            // 
            this.txt_cedula.Location = new System.Drawing.Point(127, 63);
            this.txt_cedula.Name = "txt_cedula";
            this.txt_cedula.Size = new System.Drawing.Size(100, 20);
            this.txt_cedula.TabIndex = 3;
            this.txt_cedula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cedula_KeyDown);
            // 
            // txt_direccion
            // 
            this.txt_direccion.Location = new System.Drawing.Point(127, 89);
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(453, 20);
            this.txt_direccion.TabIndex = 4;
            this.txt_direccion.TextChanged += new System.EventHandler(this.txt_direccion_TextChanged);
            this.txt_direccion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_direccion_KeyDown);
            this.txt_direccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_direccion_KeyPress);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(505, 338);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 6;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(424, 338);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 7;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // dgv_articulos_receta
            // 
            this.dgv_articulos_receta.AllowUserToAddRows = false;
            this.dgv_articulos_receta.AllowUserToDeleteRows = false;
            this.dgv_articulos_receta.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos_receta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_articulos_receta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_articulos_receta.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_articulos_receta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_articulos_receta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos_receta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.receta_id,
            this.amecop,
            this.dgb_movimiento,
            this.es_antibiotico,
            this.contiene_controlados,
            this.check,
            this.elemento_id,
            this.producto,
            this.cantidad,
            this.caducidad,
            this.lote});
            this.dgv_articulos_receta.Location = new System.Drawing.Point(12, 182);
            this.dgv_articulos_receta.MultiSelect = false;
            this.dgv_articulos_receta.Name = "dgv_articulos_receta";
            this.dgv_articulos_receta.RowHeadersVisible = false;
            this.dgv_articulos_receta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos_receta.Size = new System.Drawing.Size(568, 150);
            this.dgv_articulos_receta.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Comentarios:";
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Location = new System.Drawing.Point(127, 115);
            this.txt_comentarios.Multiline = true;
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(453, 61);
            this.txt_comentarios.TabIndex = 5;
            this.txt_comentarios.TextChanged += new System.EventHandler(this.txt_comentarios_TextChanged);
            this.txt_comentarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_comentarios_KeyDown);
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.Visible = false;
            // 
            // receta_id
            // 
            this.receta_id.DataPropertyName = "receta_id";
            this.receta_id.HeaderText = "receta_id";
            this.receta_id.Name = "receta_id";
            this.receta_id.Visible = false;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.HeaderText = "amecop";
            this.amecop.Name = "amecop";
            this.amecop.Visible = false;
            // 
            // dgb_movimiento
            // 
            this.dgb_movimiento.DataPropertyName = "movimiento";
            this.dgb_movimiento.HeaderText = "dgv_movimiento";
            this.dgb_movimiento.Name = "dgb_movimiento";
            this.dgb_movimiento.Visible = false;
            // 
            // es_antibiotico
            // 
            this.es_antibiotico.DataPropertyName = "es_antibiotico";
            this.es_antibiotico.HeaderText = "es_antibiotico";
            this.es_antibiotico.Name = "es_antibiotico";
            this.es_antibiotico.Visible = false;
            // 
            // contiene_controlados
            // 
            this.contiene_controlados.DataPropertyName = "contiene_controlados";
            this.contiene_controlados.HeaderText = "contiene_controlados";
            this.contiene_controlados.Name = "contiene_controlados";
            this.contiene_controlados.Visible = false;
            // 
            // check
            // 
            this.check.DataPropertyName = "check";
            this.check.HeaderText = "check";
            this.check.Name = "check";
            this.check.Visible = false;
            // 
            // elemento_id
            // 
            this.elemento_id.DataPropertyName = "elemento_id";
            this.elemento_id.HeaderText = "elemento_id";
            this.elemento_id.Name = "elemento_id";
            this.elemento_id.Visible = false;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "producto";
            this.producto.FillWeight = 182.1302F;
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            this.cantidad.FillWeight = 58.17352F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            this.caducidad.FillWeight = 101.5229F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            this.lote.FillWeight = 58.17352F;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            // 
            // Recetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 370);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgv_articulos_receta);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.txt_direccion);
            this.Controls.Add(this.txt_cedula);
            this.Controls.Add(this.txt_doctor);
            this.Controls.Add(this.txt_folio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Recetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datos de la Receta";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos_receta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_folio;
        private System.Windows.Forms.TextBox txt_doctor;
        private System.Windows.Forms.TextBox txt_cedula;
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.DataGridView dgv_articulos_receta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_comentarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn receta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgb_movimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_antibiotico;
        private System.Windows.Forms.DataGridViewTextBoxColumn contiene_controlados;
        private System.Windows.Forms.DataGridViewTextBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn elemento_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn lote;
    }
}