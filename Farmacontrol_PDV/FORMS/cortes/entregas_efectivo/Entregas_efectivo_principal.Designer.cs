namespace Farmacontrol_PDV.FORMS.cortes.entregas_efectivo
{
    partial class Entregas_efectivo_principal
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_procesar = new System.Windows.Forms.Button();
            this.txt_quien_recibe = new System.Windows.Forms.TextBox();
            this.txt_cantidad = new System.Windows.Forms.NumericUpDown();
            this.txt_comentarios = new System.Windows.Forms.TextBox();
            this.txt_confirmar = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_confirmar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quien recibe:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Importe:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Comentarios:";
            // 
            // btn_procesar
            // 
            this.btn_procesar.Location = new System.Drawing.Point(286, 188);
            this.btn_procesar.Name = "btn_procesar";
            this.btn_procesar.Size = new System.Drawing.Size(108, 23);
            this.btn_procesar.TabIndex = 4;
            this.btn_procesar.Text = "Procesar";
            this.btn_procesar.UseVisualStyleBackColor = true;
            this.btn_procesar.Click += new System.EventHandler(this.btn_procesar_Click);
            // 
            // txt_quien_recibe
            // 
            this.txt_quien_recibe.Location = new System.Drawing.Point(88, 6);
            this.txt_quien_recibe.Name = "txt_quien_recibe";
            this.txt_quien_recibe.Size = new System.Drawing.Size(306, 20);
            this.txt_quien_recibe.TabIndex = 0;
            this.txt_quien_recibe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_quien_recibe_KeyDown);
            this.txt_quien_recibe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_quien_recibe_KeyPress);
            // 
            // txt_cantidad
            // 
            this.txt_cantidad.DecimalPlaces = 2;
            this.txt_cantidad.Location = new System.Drawing.Point(88, 33);
            this.txt_cantidad.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txt_cantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_cantidad.Name = "txt_cantidad";
            this.txt_cantidad.Size = new System.Drawing.Size(120, 20);
            this.txt_cantidad.TabIndex = 1;
            this.txt_cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_cantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_cantidad.Enter += new System.EventHandler(this.txt_cantidad_Enter);
            this.txt_cantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cantidad_KeyDown);
            // 
            // txt_comentarios
            // 
            this.txt_comentarios.Location = new System.Drawing.Point(88, 85);
            this.txt_comentarios.Multiline = true;
            this.txt_comentarios.Name = "txt_comentarios";
            this.txt_comentarios.Size = new System.Drawing.Size(306, 97);
            this.txt_comentarios.TabIndex = 3;
            this.txt_comentarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comentarios_KeyPress);
            // 
            // txt_confirmar
            // 
            this.txt_confirmar.DecimalPlaces = 2;
            this.txt_confirmar.Location = new System.Drawing.Point(88, 59);
            this.txt_confirmar.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txt_confirmar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_confirmar.Name = "txt_confirmar";
            this.txt_confirmar.Size = new System.Drawing.Size(120, 20);
            this.txt_confirmar.TabIndex = 2;
            this.txt_confirmar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_confirmar.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_confirmar.Enter += new System.EventHandler(this.txt_confirmar_Enter);
            this.txt_confirmar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_confirmar_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Confirmar:";
            // 
            // Entregas_efectivo_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 223);
            this.Controls.Add(this.txt_confirmar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_comentarios);
            this.Controls.Add(this.txt_cantidad);
            this.Controls.Add(this.txt_quien_recibe);
            this.Controls.Add(this.btn_procesar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Entregas_efectivo_principal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entregas de Efectivo";
            this.Shown += new System.EventHandler(this.Entregas_efectivo_principal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.txt_cantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_confirmar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_procesar;
        private System.Windows.Forms.TextBox txt_quien_recibe;
        private System.Windows.Forms.NumericUpDown txt_cantidad;
        private System.Windows.Forms.TextBox txt_comentarios;
        private System.Windows.Forms.NumericUpDown txt_confirmar;
        private System.Windows.Forms.Label label4;
    }
}