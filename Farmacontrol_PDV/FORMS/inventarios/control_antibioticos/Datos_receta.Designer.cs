namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    partial class Datos_receta
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
            this.txt_doctor = new System.Windows.Forms.TextBox();
            this.txt_cedula = new System.Windows.Forms.TextBox();
            this.txt_receta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SuspendLayout();
            // 
            // txt_doctor
            // 
            this.txt_doctor.Location = new System.Drawing.Point(12, 37);
            this.txt_doctor.Name = "txt_doctor";
            this.txt_doctor.Size = new System.Drawing.Size(372, 20);
            this.txt_doctor.TabIndex = 0;
            this.txt_doctor.TextChanged += new System.EventHandler(this.txt_doctor_TextChanged);
            this.txt_doctor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_doctor_KeyDown);
            this.txt_doctor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_doctor_KeyPress);
            // 
            // txt_cedula
            // 
            this.txt_cedula.Location = new System.Drawing.Point(12, 80);
            this.txt_cedula.Name = "txt_cedula";
            this.txt_cedula.Size = new System.Drawing.Size(150, 20);
            this.txt_cedula.TabIndex = 1;
            this.txt_cedula.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cedula_KeyDown);
            this.txt_cedula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cedula_KeyPress);
            // 
            // txt_receta
            // 
            this.txt_receta.Location = new System.Drawing.Point(12, 126);
            this.txt_receta.Name = "txt_receta";
            this.txt_receta.Size = new System.Drawing.Size(150, 20);
            this.txt_receta.TabIndex = 2;
            this.txt_receta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_receta_KeyDown);
            this.txt_receta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_receta_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Doctor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cédula";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Receta";
            // 
            // btn_agregar
            // 
            this.btn_agregar.Location = new System.Drawing.Point(228, 171);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(75, 23);
            this.btn_agregar.TabIndex = 6;
            this.btn_agregar.Text = "Guardar";
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(309, 171);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 7;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(396, 202);
            this.shapeContainer1.TabIndex = 8;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 12;
            this.lineShape1.X2 = 389;
            this.lineShape1.Y1 = 160;
            this.lineShape1.Y2 = 160;
            // 
            // Datos_receta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 202);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_agregar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_receta);
            this.Controls.Add(this.txt_cedula);
            this.Controls.Add(this.txt_doctor);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Datos_receta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datos de la receta";
            this.Load += new System.EventHandler(this.Datos_receta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_doctor;
        private System.Windows.Forms.TextBox txt_cedula;
        private System.Windows.Forms.TextBox txt_receta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.Button btn_cancelar;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}