namespace Farmacontrol_PDV.FORMS.movimientos.recibir_bultos
{
    partial class Recibir_bultos_principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.boton_aceptar = new System.Windows.Forms.Button();
            this.boton_cancelar = new System.Windows.Forms.Button();
            this.recibir_bultos_grid = new System.Windows.Forms.DataGridView();
            this.row_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capturado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_bultos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sucursal_origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.worker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.recibir_bultos_grid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo:";
            // 
            // txt_codigo
            // 
            this.txt_codigo.Location = new System.Drawing.Point(57, 9);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.PasswordChar = '*';
            this.txt_codigo.Size = new System.Drawing.Size(166, 20);
            this.txt_codigo.TabIndex = 1;
            this.txt_codigo.Enter += new System.EventHandler(this.txt_codigo_Enter);
            this.txt_codigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codigo_KeyDown);
            // 
            // boton_aceptar
            // 
            this.boton_aceptar.Enabled = false;
            this.boton_aceptar.Location = new System.Drawing.Point(442, 437);
            this.boton_aceptar.Name = "boton_aceptar";
            this.boton_aceptar.Size = new System.Drawing.Size(75, 23);
            this.boton_aceptar.TabIndex = 3;
            this.boton_aceptar.Text = "Aceptar";
            this.boton_aceptar.UseVisualStyleBackColor = true;
            this.boton_aceptar.Click += new System.EventHandler(this.boton_aceptar_Click);
            // 
            // boton_cancelar
            // 
            this.boton_cancelar.Location = new System.Drawing.Point(523, 437);
            this.boton_cancelar.Name = "boton_cancelar";
            this.boton_cancelar.Size = new System.Drawing.Size(75, 23);
            this.boton_cancelar.TabIndex = 4;
            this.boton_cancelar.Text = "Cancelar";
            this.boton_cancelar.UseVisualStyleBackColor = true;
            this.boton_cancelar.Click += new System.EventHandler(this.boton_cancelar_Click);
            // 
            // recibir_bultos_grid
            // 
            this.recibir_bultos_grid.AllowUserToAddRows = false;
            this.recibir_bultos_grid.AllowUserToDeleteRows = false;
            this.recibir_bultos_grid.AllowUserToResizeColumns = false;
            this.recibir_bultos_grid.AllowUserToResizeRows = false;
            this.recibir_bultos_grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.recibir_bultos_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recibir_bultos_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.row_id,
            this.capturado,
            this.numero_bultos,
            this.sucursal_origen,
            this.folio,
            this.bulto});
            this.recibir_bultos_grid.Location = new System.Drawing.Point(12, 35);
            this.recibir_bultos_grid.Name = "recibir_bultos_grid";
            this.recibir_bultos_grid.ReadOnly = true;
            this.recibir_bultos_grid.RowHeadersVisible = false;
            this.recibir_bultos_grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.recibir_bultos_grid.Size = new System.Drawing.Size(586, 353);
            this.recibir_bultos_grid.TabIndex = 2;
            this.recibir_bultos_grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.recibir_bultos_grid_KeyDown);
            // 
            // row_id
            // 
            this.row_id.HeaderText = "row_id";
            this.row_id.Name = "row_id";
            this.row_id.ReadOnly = true;
            this.row_id.Visible = false;
            // 
            // capturado
            // 
            this.capturado.HeaderText = "capturado";
            this.capturado.Name = "capturado";
            this.capturado.ReadOnly = true;
            this.capturado.Visible = false;
            // 
            // numero_bultos
            // 
            this.numero_bultos.HeaderText = "numero_bultos";
            this.numero_bultos.Name = "numero_bultos";
            this.numero_bultos.ReadOnly = true;
            this.numero_bultos.Visible = false;
            // 
            // sucursal_origen
            // 
            this.sucursal_origen.HeaderText = "Sucursal Origen";
            this.sucursal_origen.Name = "sucursal_origen";
            this.sucursal_origen.ReadOnly = true;
            this.sucursal_origen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // folio
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.folio.DefaultCellStyle = dataGridViewCellStyle1;
            this.folio.FillWeight = 40F;
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            this.folio.ReadOnly = true;
            this.folio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bulto
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.bulto.DefaultCellStyle = dataGridViewCellStyle2;
            this.bulto.FillWeight = 40F;
            this.bulto.HeaderText = "Bulto";
            this.bulto.Name = "bulto";
            this.bulto.ReadOnly = true;
            this.bulto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.worker_ProgressChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // Recibir_bultos_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 472);
            this.Controls.Add(this.recibir_bultos_grid);
            this.Controls.Add(this.boton_cancelar);
            this.Controls.Add(this.boton_aceptar);
            this.Controls.Add(this.txt_codigo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Recibir_bultos_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recepcion de Bultos de Traspaso";
            ((System.ComponentModel.ISupportInitialize)(this.recibir_bultos_grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.Button boton_aceptar;
		private System.Windows.Forms.Button boton_cancelar;
		private System.Windows.Forms.DataGridView recibir_bultos_grid;
		public System.ComponentModel.BackgroundWorker worker;
		private System.Windows.Forms.DataGridViewTextBoxColumn row_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn capturado;
		private System.Windows.Forms.DataGridViewTextBoxColumn numero_bultos;
		private System.Windows.Forms.DataGridViewTextBoxColumn sucursal_origen;
		private System.Windows.Forms.DataGridViewTextBoxColumn folio;
		private System.Windows.Forms.DataGridViewTextBoxColumn bulto;
    }
}