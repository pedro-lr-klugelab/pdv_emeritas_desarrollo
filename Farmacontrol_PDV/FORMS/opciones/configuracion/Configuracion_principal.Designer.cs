namespace Farmacontrol_PDV.FORMS.opciones.configuracion
{
	partial class Configuracion_principal
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
            this.tp_impresoras = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chb_permitir_impresion_remota = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chb_usar_impresora_remota_etiquetas = new System.Windows.Forms.CheckBox();
            this.cbb_impresora_remota_etiquetas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbb_etiquetas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbb_impresora_remota_tickets = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chb_usar_impresora_remota_tickets = new System.Windows.Forms.CheckBox();
            this.cbb_tickets = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_escaner = new System.Windows.Forms.ComboBox();
            this.lbl_escaner = new System.Windows.Forms.Label();
            this.btn_guardar_cambios = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fbd_facturas = new System.Windows.Forms.FolderBrowserDialog();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tp_impresoras.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tp_impresoras
            // 
            this.tp_impresoras.BackColor = System.Drawing.SystemColors.Control;
            this.tp_impresoras.Controls.Add(this.lblVersion);
            this.tp_impresoras.Controls.Add(this.groupBox3);
            this.tp_impresoras.Controls.Add(this.groupBox2);
            this.tp_impresoras.Controls.Add(this.groupBox1);
            this.tp_impresoras.Controls.Add(this.cbb_escaner);
            this.tp_impresoras.Controls.Add(this.lbl_escaner);
            this.tp_impresoras.Controls.Add(this.btn_guardar_cambios);
            this.tp_impresoras.Location = new System.Drawing.Point(4, 22);
            this.tp_impresoras.Name = "tp_impresoras";
            this.tp_impresoras.Padding = new System.Windows.Forms.Padding(3);
            this.tp_impresoras.Size = new System.Drawing.Size(437, 372);
            this.tp_impresoras.TabIndex = 0;
            this.tp_impresoras.Text = "Impresoras";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chb_permitir_impresion_remota);
            this.groupBox3.Location = new System.Drawing.Point(10, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 82);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Servidores";
            // 
            // chb_permitir_impresion_remota
            // 
            this.chb_permitir_impresion_remota.AutoSize = true;
            this.chb_permitir_impresion_remota.Location = new System.Drawing.Point(75, 19);
            this.chb_permitir_impresion_remota.Name = "chb_permitir_impresion_remota";
            this.chb_permitir_impresion_remota.Size = new System.Drawing.Size(142, 17);
            this.chb_permitir_impresion_remota.TabIndex = 0;
            this.chb_permitir_impresion_remota.Text = "Permitir impresion remota";
            this.chb_permitir_impresion_remota.UseVisualStyleBackColor = true;
            this.chb_permitir_impresion_remota.CheckedChanged += new System.EventHandler(this.chb_permitir_impresion_remota_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chb_usar_impresora_remota_etiquetas);
            this.groupBox2.Controls.Add(this.cbb_impresora_remota_etiquetas);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbb_etiquetas);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(10, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 98);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Impresora de Etiquetas";
            // 
            // chb_usar_impresora_remota_etiquetas
            // 
            this.chb_usar_impresora_remota_etiquetas.AutoSize = true;
            this.chb_usar_impresora_remota_etiquetas.Location = new System.Drawing.Point(114, 73);
            this.chb_usar_impresora_remota_etiquetas.Name = "chb_usar_impresora_remota_etiquetas";
            this.chb_usar_impresora_remota_etiquetas.Size = new System.Drawing.Size(131, 17);
            this.chb_usar_impresora_remota_etiquetas.TabIndex = 12;
            this.chb_usar_impresora_remota_etiquetas.Text = "Usar impresora remota";
            this.chb_usar_impresora_remota_etiquetas.UseVisualStyleBackColor = true;
            // 
            // cbb_impresora_remota_etiquetas
            // 
            this.cbb_impresora_remota_etiquetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_impresora_remota_etiquetas.FormattingEnabled = true;
            this.cbb_impresora_remota_etiquetas.Location = new System.Drawing.Point(114, 46);
            this.cbb_impresora_remota_etiquetas.Name = "cbb_impresora_remota_etiquetas";
            this.cbb_impresora_remota_etiquetas.Size = new System.Drawing.Size(281, 21);
            this.cbb_impresora_remota_etiquetas.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Local:";
            // 
            // cbb_etiquetas
            // 
            this.cbb_etiquetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_etiquetas.FormattingEnabled = true;
            this.cbb_etiquetas.Location = new System.Drawing.Point(114, 19);
            this.cbb_etiquetas.Name = "cbb_etiquetas";
            this.cbb_etiquetas.Size = new System.Drawing.Size(281, 21);
            this.cbb_etiquetas.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Remota:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbb_impresora_remota_tickets);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chb_usar_impresora_remota_tickets);
            this.groupBox1.Controls.Add(this.cbb_tickets);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(10, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 103);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impresora de Tickets";
            // 
            // cbb_impresora_remota_tickets
            // 
            this.cbb_impresora_remota_tickets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_impresora_remota_tickets.FormattingEnabled = true;
            this.cbb_impresora_remota_tickets.Location = new System.Drawing.Point(114, 48);
            this.cbb_impresora_remota_tickets.Name = "cbb_impresora_remota_tickets";
            this.cbb_impresora_remota_tickets.Size = new System.Drawing.Size(281, 21);
            this.cbb_impresora_remota_tickets.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local:";
            // 
            // chb_usar_impresora_remota_tickets
            // 
            this.chb_usar_impresora_remota_tickets.AutoSize = true;
            this.chb_usar_impresora_remota_tickets.Location = new System.Drawing.Point(114, 75);
            this.chb_usar_impresora_remota_tickets.Name = "chb_usar_impresora_remota_tickets";
            this.chb_usar_impresora_remota_tickets.Size = new System.Drawing.Size(131, 17);
            this.chb_usar_impresora_remota_tickets.TabIndex = 11;
            this.chb_usar_impresora_remota_tickets.Text = "Usar impresora remota";
            this.chb_usar_impresora_remota_tickets.UseVisualStyleBackColor = true;
            // 
            // cbb_tickets
            // 
            this.cbb_tickets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_tickets.FormattingEnabled = true;
            this.cbb_tickets.Location = new System.Drawing.Point(114, 21);
            this.cbb_tickets.Name = "cbb_tickets";
            this.cbb_tickets.Size = new System.Drawing.Size(281, 21);
            this.cbb_tickets.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Remota:";
            // 
            // cbb_escaner
            // 
            this.cbb_escaner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_escaner.FormattingEnabled = true;
            this.cbb_escaner.Location = new System.Drawing.Point(124, 307);
            this.cbb_escaner.Name = "cbb_escaner";
            this.cbb_escaner.Size = new System.Drawing.Size(281, 21);
            this.cbb_escaner.TabIndex = 7;
            // 
            // lbl_escaner
            // 
            this.lbl_escaner.AutoSize = true;
            this.lbl_escaner.Location = new System.Drawing.Point(69, 310);
            this.lbl_escaner.Name = "lbl_escaner";
            this.lbl_escaner.Size = new System.Drawing.Size(49, 13);
            this.lbl_escaner.TabIndex = 6;
            this.lbl_escaner.Text = "Escaner:";
            // 
            // btn_guardar_cambios
            // 
            this.btn_guardar_cambios.Location = new System.Drawing.Point(322, 343);
            this.btn_guardar_cambios.Name = "btn_guardar_cambios";
            this.btn_guardar_cambios.Size = new System.Drawing.Size(107, 23);
            this.btn_guardar_cambios.TabIndex = 4;
            this.btn_guardar_cambios.Text = "Guardar cambios";
            this.btn_guardar_cambios.UseVisualStyleBackColor = true;
            this.btn_guardar_cambios.Click += new System.EventHandler(this.btn_guardar_cambios_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_impresoras);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(445, 398);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // fbd_facturas
            // 
            this.fbd_facturas.Description = "Selecciona el lugar donde se guardaran las facturas";
            this.fbd_facturas.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(17, 348);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 17;
            this.lblVersion.Text = "Version";
            // 
            // Configuracion_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 398);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuracion_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion principal";
            this.tp_impresoras.ResumeLayout(false);
            this.tp_impresoras.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabPage tp_impresoras;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ComboBox cbb_etiquetas;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbb_tickets;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_guardar_cambios;
		private System.Windows.Forms.ComboBox cbb_escaner;
		private System.Windows.Forms.Label lbl_escaner;
		private System.Windows.Forms.FolderBrowserDialog fbd_facturas;
		private System.Windows.Forms.ComboBox cbb_impresora_remota_tickets;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chb_usar_impresora_remota_tickets;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chb_usar_impresora_remota_etiquetas;
		private System.Windows.Forms.ComboBox cbb_impresora_remota_etiquetas;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chb_permitir_impresion_remota;
        private System.Windows.Forms.Label lblVersion;
	}
}