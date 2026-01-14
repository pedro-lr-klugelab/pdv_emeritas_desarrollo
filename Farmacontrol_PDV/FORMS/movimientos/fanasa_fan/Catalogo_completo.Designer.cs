namespace Farmacontrol_PDV.FORMS.movimientos.fanasa_fan
{
    partial class Catalogo_completo
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
            this.dtCatalogoSoyFan = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Regla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigoBusqueda = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtCatalogoSoyFan)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(152, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(538, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "CATALOGO DE PROMOCIONES";
            // 
            // dtCatalogoSoyFan
            // 
            this.dtCatalogoSoyFan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtCatalogoSoyFan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Descripcion,
            this.Regla});
            this.dtCatalogoSoyFan.Location = new System.Drawing.Point(35, 121);
            this.dtCatalogoSoyFan.Name = "dtCatalogoSoyFan";
            this.dtCatalogoSoyFan.Size = new System.Drawing.Size(847, 427);
            this.dtCatalogoSoyFan.TabIndex = 1;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(777, 554);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(105, 30);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "sku";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 400;
            // 
            // Regla
            // 
            this.Regla.DataPropertyName = "detalle";
            this.Regla.HeaderText = "Regla";
            this.Regla.Name = "Regla";
            this.Regla.Width = 300;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Codigo";
            // 
            // txtCodigoBusqueda
            // 
            this.txtCodigoBusqueda.Location = new System.Drawing.Point(78, 79);
            this.txtCodigoBusqueda.Name = "txtCodigoBusqueda";
            this.txtCodigoBusqueda.Size = new System.Drawing.Size(145, 20);
            this.txtCodigoBusqueda.TabIndex = 4;
            this.txtCodigoBusqueda.TextChanged += new System.EventHandler(this.txtCodigoBusqueda_TextChanged);
            // 
            // Catalogo_completo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 605);
            this.Controls.Add(this.txtCodigoBusqueda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dtCatalogoSoyFan);
            this.Controls.Add(this.label1);
            this.Name = "Catalogo_completo";
            ((System.ComponentModel.ISupportInitialize)(this.dtCatalogoSoyFan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtCatalogoSoyFan;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regla;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigoBusqueda;
    }
}