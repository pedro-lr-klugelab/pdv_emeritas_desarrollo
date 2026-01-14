namespace Farmacontrol_PDV.FORMS.ventas.elaborar_formula
{
	partial class Formula_articulos_local
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.amecop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materia_prima_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caducidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articulo_usado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_finalizar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_articulos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_articulos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.amecop,
            this.nombre,
            this.articulo_id,
            this.materia_prima_id,
            this.caducidad,
            this.lote,
            this.cantidad,
            this.articulo_usado});
            this.dgv_articulos.Location = new System.Drawing.Point(12, 25);
            this.dgv_articulos.MultiSelect = false;
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.Size = new System.Drawing.Size(843, 262);
            this.dgv_articulos.TabIndex = 0;
            // 
            // amecop
            // 
            this.amecop.DataPropertyName = "amecop";
            this.amecop.FillWeight = 60F;
            this.amecop.HeaderText = "Amecop";
            this.amecop.Name = "amecop";
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.FillWeight = 150F;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            // 
            // articulo_id
            // 
            this.articulo_id.DataPropertyName = "articulo_id";
            this.articulo_id.HeaderText = "articulo_id";
            this.articulo_id.Name = "articulo_id";
            this.articulo_id.Visible = false;
            // 
            // materia_prima_id
            // 
            this.materia_prima_id.DataPropertyName = "materia_prima_id";
            this.materia_prima_id.HeaderText = "materia_prima_id";
            this.materia_prima_id.Name = "materia_prima_id";
            this.materia_prima_id.Visible = false;
            // 
            // caducidad
            // 
            this.caducidad.DataPropertyName = "caducidad";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.caducidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.caducidad.FillWeight = 80F;
            this.caducidad.HeaderText = "Caducidad";
            this.caducidad.Name = "caducidad";
            // 
            // lote
            // 
            this.lote.DataPropertyName = "lote";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.lote.DefaultCellStyle = dataGridViewCellStyle4;
            this.lote.HeaderText = "Lote";
            this.lote.Name = "lote";
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad.FillWeight = 70F;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // articulo_usado
            // 
            this.articulo_usado.FillWeight = 20F;
            this.articulo_usado.HeaderText = "";
            this.articulo_usado.Name = "articulo_usado";
            this.articulo_usado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.articulo_usado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Los siguientes productos fueron usados durante la elaboraciónde la formula, confi" +
    "rme:";
            // 
            // btn_finalizar
            // 
            this.btn_finalizar.Location = new System.Drawing.Point(780, 293);
            this.btn_finalizar.Name = "btn_finalizar";
            this.btn_finalizar.Size = new System.Drawing.Size(75, 23);
            this.btn_finalizar.TabIndex = 2;
            this.btn_finalizar.Text = "Finalizar";
            this.btn_finalizar.UseVisualStyleBackColor = true;
            this.btn_finalizar.Click += new System.EventHandler(this.btn_finalizar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(699, 293);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Formula_articulos_local
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 328);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_finalizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_articulos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Formula_articulos_local";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Formula_articulos_local_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgv_articulos;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_finalizar;
		private System.Windows.Forms.Button btn_cancelar;
		private System.Windows.Forms.DataGridViewTextBoxColumn amecop;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn articulo_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn materia_prima_id;
		private System.Windows.Forms.DataGridViewTextBoxColumn caducidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn lote;
		private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
		private System.Windows.Forms.DataGridViewCheckBoxColumn articulo_usado;
	}
}