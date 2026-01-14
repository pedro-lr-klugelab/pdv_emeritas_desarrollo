namespace Farmacontrol_PDV.FORMS.consultas.vademecum
{
    partial class Vademecum
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(1324, 778);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("https://www.actuamed.com.mx/search/pa/?f[0]=im_field_tipo_de_producto:81", System.UriKind.Absolute);
            // 
            // Vademecum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 778);
            this.Controls.Add(this.webBrowser1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Vademecum";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Vademecum";
            this.Load += new System.EventHandler(this.Vademecum_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;


    }
}