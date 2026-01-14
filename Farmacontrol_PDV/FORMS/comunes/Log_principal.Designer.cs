namespace Farmacontrol_PDV.FORMS.comunes
{
    partial class Log_principal
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
            this.txt_log = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_salida = new System.Windows.Forms.RichTextBox();
            this.txt_folio_devolucion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(12, 12);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_log.Size = new System.Drawing.Size(848, 267);
            this.txt_log.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(202, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Imprimir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_salida
            // 
            this.txt_salida.Location = new System.Drawing.Point(12, 314);
            this.txt_salida.Name = "txt_salida";
            this.txt_salida.Size = new System.Drawing.Size(848, 263);
            this.txt_salida.TabIndex = 2;
            this.txt_salida.Text = "";
            // 
            // txt_folio_devolucion
            // 
            this.txt_folio_devolucion.Location = new System.Drawing.Point(12, 288);
            this.txt_folio_devolucion.Name = "txt_folio_devolucion";
            this.txt_folio_devolucion.Size = new System.Drawing.Size(184, 20);
            this.txt_folio_devolucion.TabIndex = 3;
            // 
            // Log_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 589);
            this.Controls.Add(this.txt_folio_devolucion);
            this.Controls.Add(this.txt_salida);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_log);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Log_principal";
            this.Text = "Registro de eventos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txt_salida;
        private System.Windows.Forms.TextBox txt_folio_devolucion;
    }
}