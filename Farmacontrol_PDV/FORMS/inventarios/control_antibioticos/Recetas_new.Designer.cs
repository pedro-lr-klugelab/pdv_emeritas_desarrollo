namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Recetas_new
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgv_articulos_receta = new System.Windows.Forms.DataGridView();
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
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.txt_cedula = new System.Windows.Forms.TextBox();
            this.txt_doctor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.chk_receta_fisica = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.lbl_folio = new System.Windows.Forms.Label();
            this.txt_folio = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos_receta)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Enabled = false;
            this.txt_comentarios.Location = new System.Drawing.Point(253, 142);
            this.txt_comentarios.Multiline = true;
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(453, 61);
            this.txt_comentarios.TabIndex = 20;
            this.txt_comentarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_comentarios_KeyDown);
            this.txt_comentarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comentarios_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Comentarios:";
            // 
            // dgv_articulos_receta
            // 
            this.dgv_articulos_receta.AllowUserToAddRows = false;
            this.dgv_articulos_receta.AllowUserToDeleteRows = false;
            this.dgv_articulos_receta.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos_receta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_articulos_receta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_articulos_receta.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_articulos_receta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
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
            this.dgv_articulos_receta.Enabled = false;
            this.dgv_articulos_receta.Location = new System.Drawing.Point(12, 209);
            this.dgv_articulos_receta.MultiSelect = false;
            this.dgv_articulos_receta.Name = "dgv_articulos_receta";
            this.dgv_articulos_receta.RowHeadersVisible = false;
            this.dgv_articulos_receta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articulos_receta.Size = new System.Drawing.Size(694, 150);
            this.dgv_articulos_receta.TabIndex = 21;
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
            // txt_direccion
            // 
            this.txt_direccion.Enabled = false;
            this.txt_direccion.Location = new System.Drawing.Point(253, 103);
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(453, 20);
            this.txt_direccion.TabIndex = 19;
            this.txt_direccion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_direccion_KeyDown);
            this.txt_direccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_direccion_KeyPress);
            // 
            // txt_cedula
            // 
            this.txt_cedula.Enabled = false;
            this.txt_cedula.Location = new System.Drawing.Point(253, 25);
            this.txt_cedula.Name = "txt_cedula";
            this.txt_cedula.Size = new System.Drawing.Size(120, 20);
            this.txt_cedula.TabIndex = 17;
            this.txt_cedula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cedula_KeyDown);
            this.txt_cedula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cedula_KeyPress);
            // 
            // txt_doctor
            // 
            this.txt_doctor.Enabled = false;
            this.txt_doctor.Location = new System.Drawing.Point(253, 64);
            this.txt_doctor.Name = "txt_doctor";
            this.txt_doctor.Size = new System.Drawing.Size(453, 20);
            this.txt_doctor.TabIndex = 15;
            this.txt_doctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_doctor_KeyDown);
            this.txt_doctor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_doctor_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Dirección consultorio:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Cédula:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Doctor:";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(709, 392);
            this.shapeContainer1.TabIndex = 23;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 245;
            this.lineShape1.X2 = 245;
            this.lineShape1.Y1 = 8;
            this.lineShape1.Y2 = 202;
            // 
            // chk_receta_fisica
            // 
            this.chk_receta_fisica.AutoSize = true;
            this.chk_receta_fisica.Location = new System.Drawing.Point(12, 25);
            this.chk_receta_fisica.Name = "chk_receta_fisica";
            this.chk_receta_fisica.Size = new System.Drawing.Size(97, 17);
            this.chk_receta_fisica.TabIndex = 24;
            this.chk_receta_fisica.Text = "Retener receta";
            this.chk_receta_fisica.UseVisualStyleBackColor = true;
            this.chk_receta_fisica.CheckedChanged += new System.EventHandler(this.chk_receta_fisica_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Comentarios:";
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(550, 365);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 28;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(631, 365);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 27;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // lbl_folio
            // 
            this.lbl_folio.AutoSize = true;
            this.lbl_folio.Location = new System.Drawing.Point(397, 9);
            this.lbl_folio.Name = "lbl_folio";
            this.lbl_folio.Size = new System.Drawing.Size(32, 13);
            this.lbl_folio.TabIndex = 29;
            this.lbl_folio.Text = "Folio:";
            // 
            // txt_folio
            // 
            this.txt_folio.Enabled = false;
            this.txt_folio.Location = new System.Drawing.Point(400, 25);
            this.txt_folio.Name = "txt_folio";
            this.txt_folio.Size = new System.Drawing.Size(100, 20);
            this.txt_folio.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Verificar que la receta sea válida";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Vigencia de la receta";
            // 
            // Recetas_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 392);
            this.ControlBox = false;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_folio);
            this.Controls.Add(this.lbl_folio);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chk_receta_fisica);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgv_articulos_receta);
            this.Controls.Add(this.txt_direccion);
            this.Controls.Add(this.txt_cedula);
            this.Controls.Add(this.txt_doctor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Recetas_new";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datos de la receta";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos_receta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_comentarios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgv_articulos_receta;
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
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.TextBox txt_cedula;
        private System.Windows.Forms.TextBox txt_doctor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.CheckBox chk_receta_fisica;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Label lbl_folio;
        private System.Windows.Forms.TextBox txt_folio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}