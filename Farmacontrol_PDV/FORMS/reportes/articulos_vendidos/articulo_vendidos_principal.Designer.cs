namespace Farmacontrol_PDV.FORMS.reportes.articulos_vendidos
{
    partial class articulo_vendidos_principal
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
            this.dtInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFinal = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dgv_prod_vendidos = new System.Windows.Forms.DataGridView();
            this.proBarr = new System.Windows.Forms.ProgressBar();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amecop_original = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_prod_vendidos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtInicial
            // 
            this.dtInicial.Location = new System.Drawing.Point(97, 22);
            this.dtInicial.Name = "dtInicial";
            this.dtInicial.Size = new System.Drawing.Size(200, 20);
            this.dtInicial.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Fecha final";
            // 
            // dtFinal
            // 
            this.dtFinal.Location = new System.Drawing.Point(390, 22);
            this.dtFinal.Name = "dtFinal";
            this.dtFinal.Size = new System.Drawing.Size(200, 20);
            this.dtFinal.TabIndex = 4;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(618, 19);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 5;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dgv_prod_vendidos
            // 
            this.dgv_prod_vendidos.AllowUserToAddRows = false;
            this.dgv_prod_vendidos.AllowUserToDeleteRows = false;
            this.dgv_prod_vendidos.AllowUserToResizeRows = false;
            this.dgv_prod_vendidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_prod_vendidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_prod_vendidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articulo_id,
            this.amecop_original,
            this.nombre,
            this.vendido,
            this.fecha_date});
            this.dgv_prod_vendidos.Location = new System.Drawing.Point(28, 70);
            this.dgv_prod_vendidos.Name = "dgv_prod_vendidos";
            this.dgv_prod_vendidos.Size = new System.Drawing.Size(894, 482);
            this.dgv_prod_vendidos.TabIndex = 6;
            // 
            // proBarr
            // 
            this.proBarr.Location = new System.Drawing.Point(339, 307);
            this.proBarr.Name = "proBarr";
            this.proBarr.Size = new System.Drawing.Size(240, 35);
            this.proBarr.TabIndex = 7;
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.Visible = false;
            // 
            // amecop_original
            // 
            this.amecop_original.DataPropertyName = "amecop_original";
            this.amecop_original.HeaderText = "Amecop";
            this.amecop_original.Name = "amecop_original";
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Descripcion";
            this.nombre.Name = "nombre";
            // 
            // vendido
            // 
            this.vendido.DataPropertyName = "vendido";
            this.vendido.HeaderText = "Piezas vendidas";
            this.vendido.Name = "vendido";
            // 
            // fecha_date
            // 
            this.fecha_date.DataPropertyName = "fecha_date";
            this.fecha_date.HeaderText = "Fecha de venta";
            this.fecha_date.Name = "fecha_date";
            // 
            // articulo_vendidos_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 588);
            this.Controls.Add(this.proBarr);
            this.Controls.Add(this.dgv_prod_vendidos);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dtFinal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtInicial);
            this.Name = "articulo_vendidos_principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos vendidos";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_prod_vendidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFinal;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView dgv_prod_vendidos;
        private System.Windows.Forms.ProgressBar proBarr;
        private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amecop_original;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendido;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_date;
    }
}