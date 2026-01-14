namespace Farmacontrol_PDV.FORMS.movimientos.Circulo_Salud
{
    partial class Circulo_Oro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Circulo_Oro));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTarjetaSalud = new System.Windows.Forms.TextBox();
            this.txtFolioVenta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LblSesion = new System.Windows.Forms.Label();
            this.bntTarjetaConsulta = new System.Windows.Forms.Button();
            this.btnCatalogo = new System.Windows.Forms.Button();
            this.btnValidaTarjeta = new System.Windows.Forms.Button();
            this.btnConfiguracion = new System.Windows.Forms.Button();
            this.btnRegClientes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(615, 320);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(144, 38);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(780, 320);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(120, 37);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 183);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "TARJETA DE LA SALUD :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(69, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(362, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "ESCANEA TICKET VENTA :";
            // 
            // txtTarjetaSalud
            // 
            this.txtTarjetaSalud.Location = new System.Drawing.Point(448, 190);
            this.txtTarjetaSalud.Margin = new System.Windows.Forms.Padding(4);
            this.txtTarjetaSalud.Name = "txtTarjetaSalud";
            this.txtTarjetaSalud.Size = new System.Drawing.Size(452, 22);
            this.txtTarjetaSalud.TabIndex = 4;
            this.txtTarjetaSalud.TextChanged += new System.EventHandler(this.txtbox_TextChanged);
            this.txtTarjetaSalud.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTarjetaSalud_KeyDown);
            this.txtTarjetaSalud.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarjetaSalud_KeyPress);
            this.txtTarjetaSalud.Leave += new System.EventHandler(this.txtTarjetaSalud_Leave);
            // 
            // txtFolioVenta
            // 
            this.txtFolioVenta.Location = new System.Drawing.Point(448, 258);
            this.txtFolioVenta.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioVenta.Name = "txtFolioVenta";
            this.txtFolioVenta.Size = new System.Drawing.Size(452, 22);
            this.txtFolioVenta.TabIndex = 5;
            this.txtFolioVenta.TextChanged += new System.EventHandler(this.txtFolioVenta_TextChanged);
            this.txtFolioVenta.Enter += new System.EventHandler(this.txtFolioVenta_Enter);
            this.txtFolioVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFolioVenta_KeyPress);
            this.txtFolioVenta.Leave += new System.EventHandler(this.txtFolioVenta_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 389);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sesion : ";
            // 
            // LblSesion
            // 
            this.LblSesion.AutoSize = true;
            this.LblSesion.Location = new System.Drawing.Point(103, 389);
            this.LblSesion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblSesion.Name = "LblSesion";
            this.LblSesion.Size = new System.Drawing.Size(0, 17);
            this.LblSesion.TabIndex = 9;
            this.LblSesion.Visible = false;
            // 
            // bntTarjetaConsulta
            // 
            this.bntTarjetaConsulta.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.compras;
            this.bntTarjetaConsulta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bntTarjetaConsulta.Location = new System.Drawing.Point(559, 15);
            this.bntTarjetaConsulta.Name = "bntTarjetaConsulta";
            this.bntTarjetaConsulta.Size = new System.Drawing.Size(147, 127);
            this.bntTarjetaConsulta.TabIndex = 12;
            this.bntTarjetaConsulta.UseVisualStyleBackColor = true;
            this.bntTarjetaConsulta.Click += new System.EventHandler(this.bntTarjetaConsulta_Click);
            // 
            // btnCatalogo
            // 
            this.btnCatalogo.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.catalogo;
            this.btnCatalogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCatalogo.Location = new System.Drawing.Point(411, 15);
            this.btnCatalogo.Margin = new System.Windows.Forms.Padding(4);
            this.btnCatalogo.Name = "btnCatalogo";
            this.btnCatalogo.Size = new System.Drawing.Size(128, 127);
            this.btnCatalogo.TabIndex = 11;
            this.btnCatalogo.UseVisualStyleBackColor = true;
            this.btnCatalogo.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnValidaTarjeta
            // 
            this.btnValidaTarjeta.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.validatarjeta_logo;
            this.btnValidaTarjeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnValidaTarjeta.Location = new System.Drawing.Point(248, 15);
            this.btnValidaTarjeta.Margin = new System.Windows.Forms.Padding(4);
            this.btnValidaTarjeta.Name = "btnValidaTarjeta";
            this.btnValidaTarjeta.Size = new System.Drawing.Size(144, 127);
            this.btnValidaTarjeta.TabIndex = 10;
            this.btnValidaTarjeta.UseVisualStyleBackColor = true;
            this.btnValidaTarjeta.Click += new System.EventHandler(this.btnValidaTarjeta_Click);
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.Image = global::Farmacontrol_PDV.Properties.Resources._5720427;
            this.btnConfiguracion.Location = new System.Drawing.Point(783, 13);
            this.btnConfiguracion.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(121, 103);
            this.btnConfiguracion.TabIndex = 7;
            this.btnConfiguracion.UseVisualStyleBackColor = true;
            this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // btnRegClientes
            // 
            this.btnRegClientes.Image = global::Farmacontrol_PDV.Properties.Resources._5146927___copia;
            this.btnRegClientes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRegClientes.Location = new System.Drawing.Point(76, 15);
            this.btnRegClientes.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegClientes.Name = "btnRegClientes";
            this.btnRegClientes.Size = new System.Drawing.Size(155, 127);
            this.btnRegClientes.TabIndex = 6;
            this.btnRegClientes.UseVisualStyleBackColor = true;
            this.btnRegClientes.Click += new System.EventHandler(this.btnRegClientes_Click);
            // 
            // Circulo_Oro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 412);
            this.ControlBox = false;
            this.Controls.Add(this.bntTarjetaConsulta);
            this.Controls.Add(this.btnCatalogo);
            this.Controls.Add(this.btnValidaTarjeta);
            this.Controls.Add(this.LblSesion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnConfiguracion);
            this.Controls.Add(this.btnRegClientes);
            this.Controls.Add(this.txtFolioVenta);
            this.Controls.Add(this.txtTarjetaSalud);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Circulo_Oro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circulo Salud Oro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Circulo_Oro_FormClosing);
            this.Load += new System.EventHandler(this.Circulo_Oro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTarjetaSalud;
        private System.Windows.Forms.TextBox txtFolioVenta;
        private System.Windows.Forms.Button btnRegClientes;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblSesion;
        private System.Windows.Forms.Button btnValidaTarjeta;
        private System.Windows.Forms.Button btnCatalogo;
        private System.Windows.Forms.Button bntTarjetaConsulta;
    }
}