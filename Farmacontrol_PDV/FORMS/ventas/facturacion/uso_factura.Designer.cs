namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
    partial class uso_factura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uso_factura));
            this.label1 = new System.Windows.Forms.Label();
            this.cbb_tipo_cfdi = new System.Windows.Forms.ComboBox();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_regimen_fiscal = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbb_tipo_cfdi
            // 
            this.cbb_tipo_cfdi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_tipo_cfdi.FormattingEnabled = true;
            resources.ApplyResources(this.cbb_tipo_cfdi, "cbb_tipo_cfdi");
            this.cbb_tipo_cfdi.Name = "cbb_tipo_cfdi";
            // 
            // btn_aceptar
            // 
            resources.ApplyResources(this.btn_aceptar, "btn_aceptar");
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            resources.ApplyResources(this.btn_cancelar, "btn_cancelar");
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbx_regimen_fiscal
            // 
            this.cbx_regimen_fiscal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_regimen_fiscal.FormattingEnabled = true;
            resources.ApplyResources(this.cbx_regimen_fiscal, "cbx_regimen_fiscal");
            this.cbx_regimen_fiscal.Name = "cbx_regimen_fiscal";
            // 
            // uso_factura
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.cbx_regimen_fiscal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.cbb_tipo_cfdi);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "uso_factura";
            this.ShowIcon = false;
            this.Shown += new System.EventHandler(this.uso_factura_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbb_tipo_cfdi;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_regimen_fiscal;
    }
}