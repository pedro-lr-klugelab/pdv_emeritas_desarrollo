namespace Farmacontrol_PDV.FORMS.comunes
{
    partial class Login_form
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
            this.txt_usuario = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.boton_aceptar = new System.Windows.Forms.Button();
            this.boton_cancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_usuario
            // 
            this.txt_usuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_usuario.Location = new System.Drawing.Point(88, 15);
            this.txt_usuario.Name = "txt_usuario";
            this.txt_usuario.PasswordChar = '*';
            this.txt_usuario.Size = new System.Drawing.Size(253, 20);
            this.txt_usuario.TabIndex = 0;
            this.txt_usuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_usuario.TextChanged += new System.EventHandler(this.txt_usuario_TextChanged);
            this.txt_usuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_usuario_KeyDown);
            // 
            // txt_password
            // 
            this.txt_password.Enabled = false;
            this.txt_password.Location = new System.Drawing.Point(88, 44);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(253, 20);
            this.txt_password.TabIndex = 1;
            this.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_password_KeyDown);
            // 
            // boton_aceptar
            // 
            this.boton_aceptar.Enabled = false;
            this.boton_aceptar.Location = new System.Drawing.Point(101, 74);
            this.boton_aceptar.Name = "boton_aceptar";
            this.boton_aceptar.Size = new System.Drawing.Size(75, 23);
            this.boton_aceptar.TabIndex = 2;
            this.boton_aceptar.Text = "Aceptar";
            this.boton_aceptar.UseVisualStyleBackColor = true;
            this.boton_aceptar.Click += new System.EventHandler(this.boton_aceptar_Click);
            // 
            // boton_cancelar
            // 
            this.boton_cancelar.Location = new System.Drawing.Point(182, 74);
            this.boton_cancelar.Name = "boton_cancelar";
            this.boton_cancelar.Size = new System.Drawing.Size(75, 23);
            this.boton_cancelar.TabIndex = 3;
            this.boton_cancelar.Text = "Cancelar";
            this.boton_cancelar.UseVisualStyleBackColor = true;
            this.boton_cancelar.Click += new System.EventHandler(this.boton_cancelar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FCID/Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            // 
            // Login_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 108);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boton_cancelar);
            this.Controls.Add(this.boton_aceptar);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_usuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Login_form";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.login_form_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_aplicacion_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_usuario;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button boton_aceptar;
        private System.Windows.Forms.Button boton_cancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}