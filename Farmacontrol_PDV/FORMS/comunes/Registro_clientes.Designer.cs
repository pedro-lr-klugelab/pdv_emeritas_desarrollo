namespace Farmacontrol_PDV.FORMS.comunes
{
	partial class Registro_clientes
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
            this.label2 = new System.Windows.Forms.Label();
            this.txt_nombre_cliente = new System.Windows.Forms.TextBox();
            this.txt_tipo = new System.Windows.Forms.TextBox();
            this.btn_guardar_cliente = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.txt_codigo_postal = new System.Windows.Forms.TextBox();
            this.txt_pais = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_ciudad = new System.Windows.Forms.TextBox();
            this.txt_municipio = new System.Windows.Forms.TextBox();
            this.txt_estado = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_numero_interior = new System.Windows.Forms.TextBox();
            this.txt_numero_exterior = new System.Windows.Forms.TextBox();
            this.txt_calle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_colonia = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mtb_telefono = new System.Windows.Forms.TextBox();
            this.txt_Apellido = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombres:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo:";
            // 
            // txt_nombre_cliente
            // 
            this.txt_nombre_cliente.Enabled = false;
            this.txt_nombre_cliente.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_nombre_cliente.Location = new System.Drawing.Point(115, 40);
            this.txt_nombre_cliente.Name = "txt_nombre_cliente";
            this.txt_nombre_cliente.ShortcutsEnabled = false;
            this.txt_nombre_cliente.Size = new System.Drawing.Size(106, 20);
            this.txt_nombre_cliente.TabIndex = 1;
            this.txt_nombre_cliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_cliente_KeyDown);
            this.txt_nombre_cliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nombre_cliente_KeyPress);
            // 
            // txt_tipo
            // 
            this.txt_tipo.Enabled = false;
            this.txt_tipo.Location = new System.Drawing.Point(115, 66);
            this.txt_tipo.Name = "txt_tipo";
            this.txt_tipo.Size = new System.Drawing.Size(216, 20);
            this.txt_tipo.TabIndex = 2;
            this.txt_tipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_tipo_KeyDown);
            this.txt_tipo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_tipo_KeyPress);
            // 
            // btn_guardar_cliente
            // 
            this.btn_guardar_cliente.Enabled = false;
            this.btn_guardar_cliente.Location = new System.Drawing.Point(243, 429);
            this.btn_guardar_cliente.Name = "btn_guardar_cliente";
            this.btn_guardar_cliente.Size = new System.Drawing.Size(123, 23);
            this.btn_guardar_cliente.TabIndex = 13;
            this.btn_guardar_cliente.Text = "Guardar Cliente";
            this.btn_guardar_cliente.UseVisualStyleBackColor = true;
            this.btn_guardar_cliente.Click += new System.EventHandler(this.btn_guardar_cliente_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(391, 429);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 14;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // txt_codigo_postal
            // 
            this.txt_codigo_postal.Enabled = false;
            this.txt_codigo_postal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txt_codigo_postal.Location = new System.Drawing.Point(116, 226);
            this.txt_codigo_postal.MaxLength = 5;
            this.txt_codigo_postal.Name = "txt_codigo_postal";
            this.txt_codigo_postal.Size = new System.Drawing.Size(61, 20);
            this.txt_codigo_postal.TabIndex = 7;
            this.txt_codigo_postal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codigo_postal_KeyPress);
            // 
            // txt_pais
            // 
            this.txt_pais.Enabled = false;
            this.txt_pais.Location = new System.Drawing.Point(116, 330);
            this.txt_pais.Name = "txt_pais";
            this.txt_pais.Size = new System.Drawing.Size(137, 20);
            this.txt_pais.TabIndex = 11;
            this.txt_pais.Text = "MEXICO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 180;
            this.label3.Text = "Pais:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 179;
            this.label4.Text = "Codigo Postal:";
            // 
            // txt_ciudad
            // 
            this.txt_ciudad.Enabled = false;
            this.txt_ciudad.Location = new System.Drawing.Point(116, 252);
            this.txt_ciudad.Name = "txt_ciudad";
            this.txt_ciudad.Size = new System.Drawing.Size(137, 20);
            this.txt_ciudad.TabIndex = 8;
            this.txt_ciudad.Text = "MERIDA";
            this.txt_ciudad.Enter += new System.EventHandler(this.txt_ciudad_Enter);
            this.txt_ciudad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ciudad_KeyPress);
            // 
            // txt_municipio
            // 
            this.txt_municipio.Enabled = false;
            this.txt_municipio.Location = new System.Drawing.Point(116, 278);
            this.txt_municipio.Name = "txt_municipio";
            this.txt_municipio.Size = new System.Drawing.Size(137, 20);
            this.txt_municipio.TabIndex = 9;
            this.txt_municipio.Text = "MERIDA";
            this.txt_municipio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_municipio_KeyPress);
            // 
            // txt_estado
            // 
            this.txt_estado.Enabled = false;
            this.txt_estado.Location = new System.Drawing.Point(115, 304);
            this.txt_estado.Name = "txt_estado";
            this.txt_estado.Size = new System.Drawing.Size(138, 20);
            this.txt_estado.TabIndex = 10;
            this.txt_estado.Text = "YUCATAN";
            this.txt_estado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_estado_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(228, 178);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(183, 13);
            this.label21.TabIndex = 178;
            this.label21.Text = "Ejemplo: \"DEPTO 101\", \"LOCAL 10\"";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(228, 151);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(88, 13);
            this.label20.TabIndex = 177;
            this.label20.Text = "Ejemplo: \"591-A\"";
            // 
            // txt_numero_interior
            // 
            this.txt_numero_interior.Enabled = false;
            this.txt_numero_interior.Location = new System.Drawing.Point(115, 174);
            this.txt_numero_interior.Name = "txt_numero_interior";
            this.txt_numero_interior.Size = new System.Drawing.Size(107, 20);
            this.txt_numero_interior.TabIndex = 5;
            this.txt_numero_interior.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_numero_interior_KeyPress);
            // 
            // txt_numero_exterior
            // 
            this.txt_numero_exterior.Enabled = false;
            this.txt_numero_exterior.Location = new System.Drawing.Point(115, 148);
            this.txt_numero_exterior.Name = "txt_numero_exterior";
            this.txt_numero_exterior.Size = new System.Drawing.Size(107, 20);
            this.txt_numero_exterior.TabIndex = 4;
            this.txt_numero_exterior.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_numero_exterior_KeyPress);
            // 
            // txt_calle
            // 
            this.txt_calle.Enabled = false;
            this.txt_calle.Location = new System.Drawing.Point(115, 92);
            this.txt_calle.Name = "txt_calle";
            this.txt_calle.Size = new System.Drawing.Size(351, 20);
            this.txt_calle.TabIndex = 3;
            this.txt_calle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_calle_KeyPress);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(112, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(418, 30);
            this.label15.TabIndex = 176;
            this.label15.Text = "Calles, cruzamientos, número, nombres de avenidas, plazas o locales. Ejemplos: \"2" +
    "0 X 13 X 15\", \"AVENIDA COLON X 19\", \"PLAZA LAS AMERICAS, CALLE 21 X 51 Y 19\"";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(25, 181);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(62, 13);
            this.label25.TabIndex = 175;
            this.label25.Text = "No. Interior:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(25, 151);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 13);
            this.label26.TabIndex = 174;
            this.label26.Text = "No. Exterior:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(25, 95);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(33, 13);
            this.label27.TabIndex = 173;
            this.label27.Text = "Calle:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 207);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 172;
            this.label13.Text = "Colonia:";
            // 
            // txt_colonia
            // 
            this.txt_colonia.Enabled = false;
            this.txt_colonia.Location = new System.Drawing.Point(115, 200);
            this.txt_colonia.Name = "txt_colonia";
            this.txt_colonia.Size = new System.Drawing.Size(351, 20);
            this.txt_colonia.TabIndex = 6;
            this.txt_colonia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_colonia_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 259);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 170;
            this.label12.Text = "Ciudad:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 285);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 169;
            this.label11.Text = "Municipio:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 311);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 168;
            this.label10.Text = "Estado:";
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Enabled = false;
            this.txt_comentarios.Location = new System.Drawing.Point(116, 356);
            this.txt_comentarios.Multiline = true;
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(350, 63);
            this.txt_comentarios.TabIndex = 12;
            this.txt_comentarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comentarios_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(25, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 34);
            this.label5.TabIndex = 167;
            this.label5.Text = "Comentarios y/o Referencias:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(337, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 181;
            this.label7.Text = "Ejemplo: CASA, OFICINA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 183;
            this.label6.Text = "Telefono:";
            // 
            // mtb_telefono
            // 
            this.mtb_telefono.Location = new System.Drawing.Point(115, 11);
            this.mtb_telefono.MaxLength = 10;
            this.mtb_telefono.Name = "mtb_telefono";
            this.mtb_telefono.Size = new System.Drawing.Size(106, 20);
            this.mtb_telefono.TabIndex = 0;
            this.mtb_telefono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtb_telefono_KeyDown_1);
            this.mtb_telefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtb_telefono_KeyPress);
            this.mtb_telefono.Leave += new System.EventHandler(this.mtb_telefono_Leave);
            // 
            // txt_Apellido
            // 
            this.txt_Apellido.Enabled = false;
            this.txt_Apellido.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Apellido.Location = new System.Drawing.Point(317, 40);
            this.txt_Apellido.Name = "txt_Apellido";
            this.txt_Apellido.ShortcutsEnabled = false;
            this.txt_Apellido.Size = new System.Drawing.Size(106, 20);
            this.txt_Apellido.TabIndex = 184;
            this.txt_Apellido.Enter += new System.EventHandler(this.txt_Apellido_Enter);
            this.txt_Apellido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Apellido_KeyDown);
            this.txt_Apellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Apellido_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 185;
            this.label8.Text = "Apellidos:";
            // 
            // Registro_clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 462);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_Apellido);
            this.Controls.Add(this.mtb_telefono);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_codigo_postal);
            this.Controls.Add(this.txt_pais);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_ciudad);
            this.Controls.Add(this.txt_municipio);
            this.Controls.Add(this.txt_estado);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txt_numero_interior);
            this.Controls.Add(this.txt_numero_exterior);
            this.Controls.Add(this.txt_calle);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_colonia);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_guardar_cliente);
            this.Controls.Add(this.txt_tipo);
            this.Controls.Add(this.txt_nombre_cliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Registro_clientes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Clientes";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_nombre_cliente;
		private System.Windows.Forms.TextBox txt_tipo;
		private System.Windows.Forms.Button btn_guardar_cliente;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.TextBox txt_codigo_postal;
		private System.Windows.Forms.TextBox txt_pais;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_ciudad;
		private System.Windows.Forms.TextBox txt_municipio;
		private System.Windows.Forms.TextBox txt_estado;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txt_numero_interior;
		private System.Windows.Forms.TextBox txt_numero_exterior;
		private System.Windows.Forms.TextBox txt_calle;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txt_colonia;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txt_comentarios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mtb_telefono;
        private System.Windows.Forms.TextBox txt_Apellido;
        private System.Windows.Forms.Label label8;

	}
}