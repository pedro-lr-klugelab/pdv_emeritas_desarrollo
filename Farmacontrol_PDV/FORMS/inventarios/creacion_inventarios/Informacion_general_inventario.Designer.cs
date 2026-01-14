namespace Farmacontrol_PDV.FORMS.inventarios.creacion_inventarios
{
	partial class Informacion_general_inventario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbc_principal = new System.Windows.Forms.TabControl();
            this.tbp_no_inventariados = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_total_no_inventariados = new System.Windows.Forms.TextBox();
            this.btn_imprimir_no_inventariados = new System.Windows.Forms.Button();
            this.chb_no_inventariados = new System.Windows.Forms.CheckBox();
            this.dgv_no_inventariados = new System.Windows.Forms.DataGridView();
            this.c_articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_precio_costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbp_diferencias = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_sobrante = new System.Windows.Forms.TextBox();
            this.txt_faltante = new System.Windows.Forms.TextBox();
            this.btn_imprimir_diferencias = new System.Windows.Forms.Button();
            this.chb_diferencias = new System.Windows.Forms.CheckBox();
            this.dgv_diferencias = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_sobrante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_faltante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbp_finalizar = new System.Windows.Forms.TabPage();
            this.btn_terminar_inventario = new System.Windows.Forms.Button();
            this.lbl_inventario_actual = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_inventario_previo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbc_principal.SuspendLayout();
            this.tbp_no_inventariados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_no_inventariados)).BeginInit();
            this.tbp_diferencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_diferencias)).BeginInit();
            this.tbp_finalizar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbc_principal
            // 
            this.tbc_principal.Controls.Add(this.tbp_no_inventariados);
            this.tbc_principal.Controls.Add(this.tbp_diferencias);
            this.tbc_principal.Controls.Add(this.tbp_finalizar);
            this.tbc_principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_principal.Location = new System.Drawing.Point(0, 0);
            this.tbc_principal.Name = "tbc_principal";
            this.tbc_principal.SelectedIndex = 0;
            this.tbc_principal.Size = new System.Drawing.Size(918, 552);
            this.tbc_principal.TabIndex = 0;
            this.tbc_principal.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbc_principal_Selected);
            // 
            // tbp_no_inventariados
            // 
            this.tbp_no_inventariados.BackColor = System.Drawing.SystemColors.Control;
            this.tbp_no_inventariados.Controls.Add(this.label2);
            this.tbp_no_inventariados.Controls.Add(this.txt_total_no_inventariados);
            this.tbp_no_inventariados.Controls.Add(this.btn_imprimir_no_inventariados);
            this.tbp_no_inventariados.Controls.Add(this.chb_no_inventariados);
            this.tbp_no_inventariados.Controls.Add(this.dgv_no_inventariados);
            this.tbp_no_inventariados.Location = new System.Drawing.Point(4, 22);
            this.tbp_no_inventariados.Name = "tbp_no_inventariados";
            this.tbp_no_inventariados.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_no_inventariados.Size = new System.Drawing.Size(910, 526);
            this.tbp_no_inventariados.TabIndex = 0;
            this.tbp_no_inventariados.Text = "No Inventariados";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(727, 501);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Total:";
            // 
            // txt_total_no_inventariados
            // 
            this.txt_total_no_inventariados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_no_inventariados.ForeColor = System.Drawing.Color.Red;
            this.txt_total_no_inventariados.Location = new System.Drawing.Point(767, 498);
            this.txt_total_no_inventariados.Name = "txt_total_no_inventariados";
            this.txt_total_no_inventariados.ReadOnly = true;
            this.txt_total_no_inventariados.Size = new System.Drawing.Size(135, 21);
            this.txt_total_no_inventariados.TabIndex = 8;
            this.txt_total_no_inventariados.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_imprimir_no_inventariados
            // 
            this.btn_imprimir_no_inventariados.Location = new System.Drawing.Point(702, 2);
            this.btn_imprimir_no_inventariados.Name = "btn_imprimir_no_inventariados";
            this.btn_imprimir_no_inventariados.Size = new System.Drawing.Size(200, 23);
            this.btn_imprimir_no_inventariados.TabIndex = 5;
            this.btn_imprimir_no_inventariados.Text = "Imprimir Productos no inventariados";
            this.btn_imprimir_no_inventariados.UseVisualStyleBackColor = true;
            this.btn_imprimir_no_inventariados.Click += new System.EventHandler(this.btn_imprimir_no_inventariados_Click);
            // 
            // chb_no_inventariados
            // 
            this.chb_no_inventariados.AutoSize = true;
            this.chb_no_inventariados.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_no_inventariados.Location = new System.Drawing.Point(8, 6);
            this.chb_no_inventariados.Name = "chb_no_inventariados";
            this.chb_no_inventariados.Size = new System.Drawing.Size(428, 17);
            this.chb_no_inventariados.TabIndex = 2;
            this.chb_no_inventariados.Text = "Me doy por enterado de la informacion aqui presentada (Productos NO inventariados" +
    ")";
            this.chb_no_inventariados.UseVisualStyleBackColor = true;
            this.chb_no_inventariados.CheckedChanged += new System.EventHandler(this.chb_no_inventariados_CheckedChanged);
            // 
            // dgv_no_inventariados
            // 
            this.dgv_no_inventariados.AllowUserToAddRows = false;
            this.dgv_no_inventariados.AllowUserToDeleteRows = false;
            this.dgv_no_inventariados.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_no_inventariados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_no_inventariados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_no_inventariados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_no_inventariados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_no_inventariados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_articulo_id,
            this.c_amecop,
            this.c_producto,
            this.c_caducidad,
            this.c_lote,
            this.c_cantidad,
            this.c_precio_costo,
            this.c_total});
            this.dgv_no_inventariados.Location = new System.Drawing.Point(8, 29);
            this.dgv_no_inventariados.MultiSelect = false;
            this.dgv_no_inventariados.Name = "dgv_no_inventariados";
            this.dgv_no_inventariados.ReadOnly = true;
            this.dgv_no_inventariados.RowHeadersVisible = false;
            this.dgv_no_inventariados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_no_inventariados.Size = new System.Drawing.Size(894, 463);
            this.dgv_no_inventariados.TabIndex = 0;
            // 
            // c_articulo_id
            // 
            this.c_articulo_id.DataPropertyName = "articulo_id";
            this.c_articulo_id.HeaderText = "c_articulo_id";
            this.c_articulo_id.Name = "c_articulo_id";
            this.c_articulo_id.ReadOnly = true;
            this.c_articulo_id.Visible = false;
            // 
            // c_amecop
            // 
            this.c_amecop.DataPropertyName = "amecop";
            this.c_amecop.FillWeight = 70F;
            this.c_amecop.HeaderText = "Amecop";
            this.c_amecop.Name = "c_amecop";
            this.c_amecop.ReadOnly = true;
            // 
            // c_producto
            // 
            this.c_producto.DataPropertyName = "producto";
            this.c_producto.FillWeight = 150F;
            this.c_producto.HeaderText = "Producto";
            this.c_producto.Name = "c_producto";
            this.c_producto.ReadOnly = true;
            // 
            // c_caducidad
            // 
            this.c_caducidad.DataPropertyName = "caducidad";
            this.c_caducidad.FillWeight = 60F;
            this.c_caducidad.HeaderText = "Caducidad";
            this.c_caducidad.Name = "c_caducidad";
            this.c_caducidad.ReadOnly = true;
            // 
            // c_lote
            // 
            this.c_lote.DataPropertyName = "lote";
            this.c_lote.FillWeight = 110F;
            this.c_lote.HeaderText = "Lote";
            this.c_lote.Name = "c_lote";
            this.c_lote.ReadOnly = true;
            // 
            // c_cantidad
            // 
            this.c_cantidad.DataPropertyName = "existencia";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_cantidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_cantidad.FillWeight = 60F;
            this.c_cantidad.HeaderText = "Cantidad";
            this.c_cantidad.Name = "c_cantidad";
            this.c_cantidad.ReadOnly = true;
            // 
            // c_precio_costo
            // 
            this.c_precio_costo.DataPropertyName = "precio_costo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.c_precio_costo.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_precio_costo.FillWeight = 70F;
            this.c_precio_costo.HeaderText = "Precio Costo";
            this.c_precio_costo.Name = "c_precio_costo";
            this.c_precio_costo.ReadOnly = true;
            // 
            // c_total
            // 
            this.c_total.DataPropertyName = "total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.c_total.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_total.FillWeight = 70F;
            this.c_total.HeaderText = "Total";
            this.c_total.Name = "c_total";
            this.c_total.ReadOnly = true;
            // 
            // tbp_diferencias
            // 
            this.tbp_diferencias.BackColor = System.Drawing.SystemColors.Control;
            this.tbp_diferencias.Controls.Add(this.label3);
            this.tbp_diferencias.Controls.Add(this.label1);
            this.tbp_diferencias.Controls.Add(this.txt_sobrante);
            this.tbp_diferencias.Controls.Add(this.txt_faltante);
            this.tbp_diferencias.Controls.Add(this.btn_imprimir_diferencias);
            this.tbp_diferencias.Controls.Add(this.chb_diferencias);
            this.tbp_diferencias.Controls.Add(this.dgv_diferencias);
            this.tbp_diferencias.Location = new System.Drawing.Point(4, 22);
            this.tbp_diferencias.Name = "tbp_diferencias";
            this.tbp_diferencias.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_diferencias.Size = new System.Drawing.Size(910, 526);
            this.tbp_diferencias.TabIndex = 1;
            this.tbp_diferencias.Text = "Diferencias";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(764, 482);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Total Faltante:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(623, 482);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total Sobrante:";
            // 
            // txt_sobrante
            // 
            this.txt_sobrante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sobrante.ForeColor = System.Drawing.Color.Red;
            this.txt_sobrante.Location = new System.Drawing.Point(626, 498);
            this.txt_sobrante.Name = "txt_sobrante";
            this.txt_sobrante.ReadOnly = true;
            this.txt_sobrante.Size = new System.Drawing.Size(135, 21);
            this.txt_sobrante.TabIndex = 6;
            this.txt_sobrante.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_faltante
            // 
            this.txt_faltante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_faltante.ForeColor = System.Drawing.Color.Red;
            this.txt_faltante.Location = new System.Drawing.Point(767, 498);
            this.txt_faltante.Name = "txt_faltante";
            this.txt_faltante.ReadOnly = true;
            this.txt_faltante.Size = new System.Drawing.Size(135, 21);
            this.txt_faltante.TabIndex = 5;
            this.txt_faltante.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_imprimir_diferencias
            // 
            this.btn_imprimir_diferencias.Location = new System.Drawing.Point(777, 2);
            this.btn_imprimir_diferencias.Name = "btn_imprimir_diferencias";
            this.btn_imprimir_diferencias.Size = new System.Drawing.Size(115, 23);
            this.btn_imprimir_diferencias.TabIndex = 4;
            this.btn_imprimir_diferencias.Text = "Imprimir Diferencias";
            this.btn_imprimir_diferencias.UseVisualStyleBackColor = true;
            this.btn_imprimir_diferencias.Click += new System.EventHandler(this.btn_imprimir_diferencias_Click);
            // 
            // chb_diferencias
            // 
            this.chb_diferencias.AutoSize = true;
            this.chb_diferencias.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_diferencias.Location = new System.Drawing.Point(8, 6);
            this.chb_diferencias.Name = "chb_diferencias";
            this.chb_diferencias.Size = new System.Drawing.Size(418, 17);
            this.chb_diferencias.TabIndex = 3;
            this.chb_diferencias.Text = "Me doy por enterado de la informacion aqui presentada (Productos con diferencias)" +
    "";
            this.chb_diferencias.UseVisualStyleBackColor = true;
            this.chb_diferencias.CheckedChanged += new System.EventHandler(this.chb_diferencias_CheckedChanged);
            // 
            // dgv_diferencias
            // 
            this.dgv_diferencias.AllowUserToAddRows = false;
            this.dgv_diferencias.AllowUserToDeleteRows = false;
            this.dgv_diferencias.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_diferencias.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_diferencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_diferencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_diferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_diferencias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.c_sobrante,
            this.c_faltante});
            this.dgv_diferencias.Location = new System.Drawing.Point(8, 29);
            this.dgv_diferencias.MultiSelect = false;
            this.dgv_diferencias.Name = "dgv_diferencias";
            this.dgv_diferencias.ReadOnly = true;
            this.dgv_diferencias.RowHeadersVisible = false;
            this.dgv_diferencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_diferencias.Size = new System.Drawing.Size(894, 450);
            this.dgv_diferencias.TabIndex = 1;
            this.dgv_diferencias.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_diferencias_CellFormatting);
            this.dgv_diferencias.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_diferencias_DataBindingComplete);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "amecop";
            this.dataGridViewTextBoxColumn1.FillWeight = 70F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Amecop";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "producto";
            this.dataGridViewTextBoxColumn2.FillWeight = 150F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Producto";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "cantidad";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn5.FillWeight = 60F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Diferencia";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // c_sobrante
            // 
            this.c_sobrante.DataPropertyName = "sobrante";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "C2";
            dataGridViewCellStyle9.NullValue = null;
            this.c_sobrante.DefaultCellStyle = dataGridViewCellStyle9;
            this.c_sobrante.FillWeight = 70F;
            this.c_sobrante.HeaderText = "Sobrante";
            this.c_sobrante.Name = "c_sobrante";
            this.c_sobrante.ReadOnly = true;
            // 
            // c_faltante
            // 
            this.c_faltante.DataPropertyName = "faltante";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "C2";
            dataGridViewCellStyle10.NullValue = null;
            this.c_faltante.DefaultCellStyle = dataGridViewCellStyle10;
            this.c_faltante.FillWeight = 70F;
            this.c_faltante.HeaderText = "Faltante";
            this.c_faltante.Name = "c_faltante";
            this.c_faltante.ReadOnly = true;
            // 
            // tbp_finalizar
            // 
            this.tbp_finalizar.BackColor = System.Drawing.SystemColors.Control;
            this.tbp_finalizar.Controls.Add(this.btn_terminar_inventario);
            this.tbp_finalizar.Controls.Add(this.lbl_inventario_actual);
            this.tbp_finalizar.Controls.Add(this.label6);
            this.tbp_finalizar.Controls.Add(this.lbl_inventario_previo);
            this.tbp_finalizar.Controls.Add(this.label4);
            this.tbp_finalizar.Location = new System.Drawing.Point(4, 22);
            this.tbp_finalizar.Name = "tbp_finalizar";
            this.tbp_finalizar.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_finalizar.Size = new System.Drawing.Size(910, 526);
            this.tbp_finalizar.TabIndex = 2;
            this.tbp_finalizar.Text = "Finalizar";
            // 
            // btn_terminar_inventario
            // 
            this.btn_terminar_inventario.Location = new System.Drawing.Point(707, 489);
            this.btn_terminar_inventario.Name = "btn_terminar_inventario";
            this.btn_terminar_inventario.Size = new System.Drawing.Size(195, 29);
            this.btn_terminar_inventario.TabIndex = 4;
            this.btn_terminar_inventario.Text = "Terminar jornada de inventario";
            this.btn_terminar_inventario.UseVisualStyleBackColor = true;
            this.btn_terminar_inventario.Click += new System.EventHandler(this.btn_terminar_inventario_Click);
            // 
            // lbl_inventario_actual
            // 
            this.lbl_inventario_actual.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_inventario_actual.ForeColor = System.Drawing.Color.Red;
            this.lbl_inventario_actual.Location = new System.Drawing.Point(371, 28);
            this.lbl_inventario_actual.Name = "lbl_inventario_actual";
            this.lbl_inventario_actual.Size = new System.Drawing.Size(311, 53);
            this.lbl_inventario_actual.TabIndex = 3;
            this.lbl_inventario_actual.Text = "-$999,999,999.99";
            this.lbl_inventario_actual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(375, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Inventario ACTUAL:";
            // 
            // lbl_inventario_previo
            // 
            this.lbl_inventario_previo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_inventario_previo.ForeColor = System.Drawing.Color.Red;
            this.lbl_inventario_previo.Location = new System.Drawing.Point(4, 28);
            this.lbl_inventario_previo.Name = "lbl_inventario_previo";
            this.lbl_inventario_previo.Size = new System.Drawing.Size(311, 53);
            this.lbl_inventario_previo.TabIndex = 1;
            this.lbl_inventario_previo.Text = "-$999,999,999.99";
            this.lbl_inventario_previo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Inventario PREVIO:";
            // 
            // Informacion_general_inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 552);
            this.Controls.Add(this.tbc_principal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Informacion_general_inventario";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informacion general de la Jornada del  inventario";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Informacion_general_inventario_Load);
            this.Shown += new System.EventHandler(this.Informacion_general_inventario_Shown);
            this.tbc_principal.ResumeLayout(false);
            this.tbp_no_inventariados.ResumeLayout(false);
            this.tbp_no_inventariados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_no_inventariados)).EndInit();
            this.tbp_diferencias.ResumeLayout(false);
            this.tbp_diferencias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_diferencias)).EndInit();
            this.tbp_finalizar.ResumeLayout(false);
            this.tbp_finalizar.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tbc_principal;
		private System.Windows.Forms.TabPage tbp_diferencias;
		private System.Windows.Forms.DataGridView dgv_diferencias;
		private System.Windows.Forms.CheckBox chb_diferencias;
		private System.Windows.Forms.TabPage tbp_no_inventariados;
		private System.Windows.Forms.Button btn_imprimir_no_inventariados;
		private System.Windows.Forms.CheckBox chb_no_inventariados;
		private System.Windows.Forms.DataGridView dgv_no_inventariados;
		private System.Windows.Forms.Button btn_imprimir_diferencias;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_producto;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_cantidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_precio_costo;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_total;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_sobrante;
		private System.Windows.Forms.DataGridViewTextBoxColumn c_faltante;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_total_no_inventariados;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_sobrante;
		private System.Windows.Forms.TextBox txt_faltante;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabPage tbp_finalizar;
		private System.Windows.Forms.Label lbl_inventario_previo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lbl_inventario_actual;
		private System.Windows.Forms.Button btn_terminar_inventario;
		private System.Windows.Forms.Label label6;
	}
}