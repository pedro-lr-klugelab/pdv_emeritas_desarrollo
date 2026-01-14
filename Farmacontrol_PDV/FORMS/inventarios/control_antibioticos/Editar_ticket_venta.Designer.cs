namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Editar_ticket_venta
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
            this.btn_guardar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.txt_ticket_anterior = new System.Windows.Forms.TextBox();
            this.txt_nuevo_ticket = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_receta = new System.Windows.Forms.TextBox();
            this.txt_folio_venta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_guardar
            // 
            this.btn_guardar.Enabled = false;
            this.btn_guardar.Location = new System.Drawing.Point(379, 104);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 0;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(298, 104);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 1;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // txt_ticket_anterior
            // 
            this.txt_ticket_anterior.Location = new System.Drawing.Point(96, 17);
            this.txt_ticket_anterior.Name = "txt_ticket_anterior";
            this.txt_ticket_anterior.ReadOnly = true;
            this.txt_ticket_anterior.Size = new System.Drawing.Size(139, 20);
            this.txt_ticket_anterior.TabIndex = 2;
            // 
            // txt_nuevo_ticket
            // 
            this.txt_nuevo_ticket.Location = new System.Drawing.Point(96, 56);
            this.txt_nuevo_ticket.Name = "txt_nuevo_ticket";
            this.txt_nuevo_ticket.Size = new System.Drawing.Size(139, 20);
            this.txt_nuevo_ticket.TabIndex = 3;
            this.txt_nuevo_ticket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nuevo_ticket_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ticket anterior:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ticket nuevo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Folio receta:";
            // 
            // txt_receta
            // 
            this.txt_receta.Location = new System.Drawing.Point(354, 17);
            this.txt_receta.Name = "txt_receta";
            this.txt_receta.ReadOnly = true;
            this.txt_receta.Size = new System.Drawing.Size(100, 20);
            this.txt_receta.TabIndex = 7;
            // 
            // txt_folio_venta
            // 
            this.txt_folio_venta.Location = new System.Drawing.Point(354, 56);
            this.txt_folio_venta.Name = "txt_folio_venta";
            this.txt_folio_venta.ReadOnly = true;
            this.txt_folio_venta.Size = new System.Drawing.Size(100, 20);
            this.txt_folio_venta.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Folio de venta:";
            // 
            // Editar_ticket_venta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 139);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_folio_venta);
            this.Controls.Add(this.txt_receta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_nuevo_ticket);
            this.Controls.Add(this.txt_ticket_anterior);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_guardar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Editar_ticket_venta";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar Ticket de Venta";
            this.Load += new System.EventHandler(this.Editar_ticket_venta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.TextBox txt_ticket_anterior;
        private System.Windows.Forms.TextBox txt_nuevo_ticket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_receta;
        private System.Windows.Forms.TextBox txt_folio_venta;
        private System.Windows.Forms.Label label4;
    }
}