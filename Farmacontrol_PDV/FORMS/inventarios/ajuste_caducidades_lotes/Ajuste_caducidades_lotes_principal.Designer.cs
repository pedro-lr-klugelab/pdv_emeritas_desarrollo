namespace Farmacontrol_PDV.FORMS.inventarios.ajuste_caducidades_lotes
{
	partial class Ajuste_caducidades_lotes_principal
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
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_afectar_existencias = new System.Windows.Forms.Button();
            this.txt_producto = new System.Windows.Forms.TextBox();
            this.cbb_lote_de = new System.Windows.Forms.ComboBox();
            this.cbb_caducidad_de = new System.Windows.Forms.ComboBox();
            this.lbl_lote = new System.Windows.Forms.Label();
            this.lbl_caducidad = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_otro_lote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbb_anio = new System.Windows.Forms.ComboBox();
            this.cbb_mes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbb_lote_a = new System.Windows.Forms.ComboBox();
            this.cbb_caducidad_a = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nup_cantidad_de = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_amecop = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_cantidad_de)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(655, 177);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 173;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_afectar_existencias
            // 
            this.btn_afectar_existencias.Location = new System.Drawing.Point(562, 177);
            this.btn_afectar_existencias.Name = "btn_afectar_existencias";
            this.btn_afectar_existencias.Size = new System.Drawing.Size(87, 23);
            this.btn_afectar_existencias.TabIndex = 171;
            this.btn_afectar_existencias.Text = "Afectar";
            this.btn_afectar_existencias.UseVisualStyleBackColor = true;
            this.btn_afectar_existencias.Click += new System.EventHandler(this.btn_afectar_existencias_Click);
            // 
            // txt_producto
            // 
            this.txt_producto.Enabled = false;
            this.txt_producto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_producto.Location = new System.Drawing.Point(244, 6);
            this.txt_producto.Name = "txt_producto";
            this.txt_producto.Size = new System.Drawing.Size(486, 21);
            this.txt_producto.TabIndex = 152;
            // 
            // cbb_lote_de
            // 
            this.cbb_lote_de.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_lote_de.Enabled = false;
            this.cbb_lote_de.FormattingEnabled = true;
            this.cbb_lote_de.Location = new System.Drawing.Point(369, 31);
            this.cbb_lote_de.Name = "cbb_lote_de";
            this.cbb_lote_de.Size = new System.Drawing.Size(156, 21);
            this.cbb_lote_de.TabIndex = 158;
            this.cbb_lote_de.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_lote_KeyDown_de);
            this.cbb_lote_de.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbb_lote_de_KeyPress);
            // 
            // cbb_caducidad_de
            // 
            this.cbb_caducidad_de.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_caducidad_de.Enabled = false;
            this.cbb_caducidad_de.FormattingEnabled = true;
            this.cbb_caducidad_de.Location = new System.Drawing.Point(32, 31);
            this.cbb_caducidad_de.Name = "cbb_caducidad_de";
            this.cbb_caducidad_de.Size = new System.Drawing.Size(106, 21);
            this.cbb_caducidad_de.TabIndex = 157;
            this.cbb_caducidad_de.Enter += new System.EventHandler(this.cbb_caducidad_Enter);
            this.cbb_caducidad_de.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_caducidad_KeyDown_de);
            // 
            // lbl_lote
            // 
            this.lbl_lote.AutoSize = true;
            this.lbl_lote.Location = new System.Drawing.Point(366, 16);
            this.lbl_lote.Name = "lbl_lote";
            this.lbl_lote.Size = new System.Drawing.Size(28, 13);
            this.lbl_lote.TabIndex = 156;
            this.lbl_lote.Text = "Lote";
            // 
            // lbl_caducidad
            // 
            this.lbl_caducidad.AutoSize = true;
            this.lbl_caducidad.Location = new System.Drawing.Point(29, 16);
            this.lbl_caducidad.Name = "lbl_caducidad";
            this.lbl_caducidad.Size = new System.Drawing.Size(58, 13);
            this.lbl_caducidad.TabIndex = 155;
            this.lbl_caducidad.Text = "Caducidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 151;
            this.label2.Text = "Producto:";
            // 
            // txt_otro_lote
            // 
            this.txt_otro_lote.Enabled = false;
            this.txt_otro_lote.Location = new System.Drawing.Point(587, 31);
            this.txt_otro_lote.Name = "txt_otro_lote";
            this.txt_otro_lote.Size = new System.Drawing.Size(122, 20);
            this.txt_otro_lote.TabIndex = 182;
            this.txt_otro_lote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_otro_lote_KeyDown);
            this.txt_otro_lote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_otro_lote_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(587, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 183;
            this.label1.Text = "Otro Lote";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 181;
            this.label6.Text = "Año";
            // 
            // cbb_anio
            // 
            this.cbb_anio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_anio.Enabled = false;
            this.cbb_anio.FormattingEnabled = true;
            this.cbb_anio.Items.AddRange(new object[] {
            "2013",
            "2014",
            "2015",
            "2016",
            "2017"});
            this.cbb_anio.Location = new System.Drawing.Point(244, 31);
            this.cbb_anio.Name = "cbb_anio";
            this.cbb_anio.Size = new System.Drawing.Size(60, 21);
            this.cbb_anio.TabIndex = 180;
            this.cbb_anio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_anio_KeyDown);
            // 
            // cbb_mes
            // 
            this.cbb_mes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_mes.Enabled = false;
            this.cbb_mes.FormattingEnabled = true;
            this.cbb_mes.Location = new System.Drawing.Point(188, 31);
            this.cbb_mes.Name = "cbb_mes";
            this.cbb_mes.Size = new System.Drawing.Size(50, 21);
            this.cbb_mes.TabIndex = 179;
            this.cbb_mes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_mes_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(186, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 178;
            this.label7.Text = "Mes";
            // 
            // cbb_lote_a
            // 
            this.cbb_lote_a.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_lote_a.Enabled = false;
            this.cbb_lote_a.FormattingEnabled = true;
            this.cbb_lote_a.Location = new System.Drawing.Point(369, 31);
            this.cbb_lote_a.Name = "cbb_lote_a";
            this.cbb_lote_a.Size = new System.Drawing.Size(156, 21);
            this.cbb_lote_a.TabIndex = 177;
            this.cbb_lote_a.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_lote_KeyDown_a);
            // 
            // cbb_caducidad_a
            // 
            this.cbb_caducidad_a.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_caducidad_a.Enabled = false;
            this.cbb_caducidad_a.FormattingEnabled = true;
            this.cbb_caducidad_a.Location = new System.Drawing.Point(32, 31);
            this.cbb_caducidad_a.Name = "cbb_caducidad_a";
            this.cbb_caducidad_a.Size = new System.Drawing.Size(106, 21);
            this.cbb_caducidad_a.TabIndex = 176;
            this.cbb_caducidad_a.Enter += new System.EventHandler(this.cbb_caducidad_Enter);
            this.cbb_caducidad_a.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbb_caducidad_KeyDown_a);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(366, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 175;
            this.label11.Text = "Lote";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(29, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 174;
            this.label12.Text = "Caducidad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(587, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 184;
            this.label4.Text = "Cantidad";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nup_cantidad_de);
            this.groupBox1.Controls.Add(this.lbl_caducidad);
            this.groupBox1.Controls.Add(this.lbl_lote);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbb_caducidad_de);
            this.groupBox1.Controls.Add(this.cbb_lote_de);
            this.groupBox1.Location = new System.Drawing.Point(15, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 63);
            this.groupBox1.TabIndex = 186;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "De:";
            // 
            // nup_cantidad_de
            // 
            this.nup_cantidad_de.Enabled = false;
            this.nup_cantidad_de.Location = new System.Drawing.Point(590, 32);
            this.nup_cantidad_de.Name = "nup_cantidad_de";
            this.nup_cantidad_de.Size = new System.Drawing.Size(119, 20);
            this.nup_cantidad_de.TabIndex = 186;
            this.nup_cantidad_de.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nup_cantidad_de_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txt_otro_lote);
            this.groupBox2.Controls.Add(this.cbb_caducidad_a);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbb_lote_a);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbb_anio);
            this.groupBox2.Controls.Add(this.cbb_mes);
            this.groupBox2.Location = new System.Drawing.Point(15, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 66);
            this.groupBox2.TabIndex = 187;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "A:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Amecop:";
            // 
            // txt_amecop
            // 
            this.txt_amecop.Location = new System.Drawing.Point(67, 7);
            this.txt_amecop.Name = "txt_amecop";
            this.txt_amecop.Size = new System.Drawing.Size(112, 20);
            this.txt_amecop.TabIndex = 189;
            this.txt_amecop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_amecop_KeyDown);
            // 
            // Ajuste_caducidades_lotes_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 212);
            this.Controls.Add(this.txt_amecop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_afectar_existencias);
            this.Controls.Add(this.txt_producto);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ajuste_caducidades_lotes_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajuste de Caducidades y Lotes";
            this.Shown += new System.EventHandler(this.Ajuste_caducidades_lotes_principal_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_cantidad_de)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.Button btn_afectar_existencias;
		private System.Windows.Forms.TextBox txt_producto;
		private System.Windows.Forms.ComboBox cbb_lote_de;
		private System.Windows.Forms.ComboBox cbb_caducidad_de;
		private System.Windows.Forms.Label lbl_lote;
		private System.Windows.Forms.Label lbl_caducidad;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_otro_lote;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbb_anio;
		private System.Windows.Forms.ComboBox cbb_mes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox cbb_lote_a;
		private System.Windows.Forms.ComboBox cbb_caducidad_a;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_amecop;
		private System.Windows.Forms.NumericUpDown nup_cantidad_de;


	}
}