namespace Farmacontrol_PDV.FORMS.ventas.servicios_domicilio_enviar
{
    partial class Servicios_domicilio_enviar_principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_ventas = new System.Windows.Forms.DataGridView();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.venta_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enviar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione las ventas a enviar";
            // 
            // dgv_ventas
            // 
            this.dgv_ventas.AllowUserToAddRows = false;
            this.dgv_ventas.AllowUserToDeleteRows = false;
            this.dgv_ventas.AllowUserToResizeColumns = false;
            this.dgv_ventas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_ventas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ventas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ventas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ventas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.venta_id,
            this.caja,
            this.folio,
            this.empleado,
            this.cliente,
            this.total,
            this.enviar});
            this.dgv_ventas.Location = new System.Drawing.Point(12, 25);
            this.dgv_ventas.MultiSelect = false;
            this.dgv_ventas.Name = "dgv_ventas";
            this.dgv_ventas.RowHeadersVisible = false;
            this.dgv_ventas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ventas.Size = new System.Drawing.Size(1027, 449);
            this.dgv_ventas.TabIndex = 1;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(964, 480);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 2;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_enviar
            // 
            this.btn_enviar.Location = new System.Drawing.Point(883, 480);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(75, 23);
            this.btn_enviar.TabIndex = 3;
            this.btn_enviar.Text = "Enviar";
            this.btn_enviar.UseVisualStyleBackColor = true;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // venta_id
            // 
            this.venta_id.DataPropertyName = "venta_id";
            this.venta_id.HeaderText = "venta_id";
            this.venta_id.Name = "venta_id";
            this.venta_id.ReadOnly = true;
            this.venta_id.Visible = false;
            // 
            // caja
            // 
            this.caja.DataPropertyName = "caja";
            this.caja.FillWeight = 80F;
            this.caja.HeaderText = "Caja";
            this.caja.Name = "caja";
            this.caja.ReadOnly = true;
            // 
            // folio
            // 
            this.folio.DataPropertyName = "venta_folio";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.folio.DefaultCellStyle = dataGridViewCellStyle2;
            this.folio.FillWeight = 80F;
            this.folio.HeaderText = "Folio";
            this.folio.Name = "folio";
            this.folio.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.DataPropertyName = "empleado";
            this.empleado.FillWeight = 150F;
            this.empleado.HeaderText = "Empleado";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.DataPropertyName = "cliente";
            this.cliente.FillWeight = 150F;
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // total
            // 
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "C2";
            this.total.DefaultCellStyle = dataGridViewCellStyle3;
            this.total.FillWeight = 80F;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // enviar
            // 
            this.enviar.FillWeight = 30F;
            this.enviar.HeaderText = "Enviar";
            this.enviar.Name = "enviar";
            // 
            // Servicios_domicilio_enviar_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 515);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.dgv_ventas);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Servicios_domicilio_enviar_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicios a domicilio (Enviar)";
            this.Shown += new System.EventHandler(this.Servicios_domicilio_enviar_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ventas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_ventas;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.DataGridViewTextBoxColumn venta_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn caja;
        private System.Windows.Forms.DataGridViewTextBoxColumn folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enviar;
    }
}