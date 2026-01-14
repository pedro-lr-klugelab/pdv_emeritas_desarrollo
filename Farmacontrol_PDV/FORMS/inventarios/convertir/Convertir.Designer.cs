using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.inventarios.convertir
{
    partial class Convertir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Convertir));
            this.comboBox_Resultado = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1_Resultado = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_Origen = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2_Resultado = new System.Windows.Forms.Label();
            this.codigo_Resultado = new System.Windows.Forms.TextBox();
            this.lstSugerencias_Resultado = new System.Windows.Forms.ListBox();
            this.lstSugerencias_Origen = new System.Windows.Forms.ListBox();
            this.codigo_Origen = new System.Windows.Forms.TextBox();
            this.label1_Origen = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_Terminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_Resultado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_Resultado
            // 
            this.comboBox_Resultado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Resultado.FormattingEnabled = true;
            this.comboBox_Resultado.Items.AddRange(new object[] {
            "1L/K",
            "500",
            "250",
            "125",
            "100",
            "50",
            "25",
            "20",
            "10",
            "5",
            "1"});
            this.comboBox_Resultado.Location = new System.Drawing.Point(827, 196);
            this.comboBox_Resultado.Name = "comboBox_Resultado";
            this.comboBox_Resultado.Size = new System.Drawing.Size(50, 21);
            this.comboBox_Resultado.TabIndex = 3;
            this.comboBox_Resultado.SelectionChangeCommitted += new System.EventHandler(this.comboBox4_SelectionChangeCommitted);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "AGREGAR";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(489, 156);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 88);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(828, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "UNIDAD";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(700, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "CODIGO";
            // 
            // numericUpDown1_Resultado
            // 
            this.numericUpDown1_Resultado.Location = new System.Drawing.Point(910, 196);
            this.numericUpDown1_Resultado.Name = "numericUpDown1_Resultado";
            this.numericUpDown1_Resultado.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown1_Resultado.TabIndex = 12;
            this.numericUpDown1_Resultado.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(907, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "CANTIDAD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "UNIDAD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "CODIGO";
            // 
            // comboBox_Origen
            // 
            this.comboBox_Origen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Origen.FormattingEnabled = true;
            this.comboBox_Origen.Items.AddRange(new object[] {
            "1L/K",
            "500",
            "250",
            "125",
            "100",
            "50",
            "25",
            "20",
            "10",
            "5",
            "1"});
            this.comboBox_Origen.Location = new System.Drawing.Point(250, 194);
            this.comboBox_Origen.Name = "comboBox_Origen";
            this.comboBox_Origen.Size = new System.Drawing.Size(50, 21);
            this.comboBox_Origen.TabIndex = 15;
            this.comboBox_Origen.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(205, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 31);
            this.label9.TabIndex = 20;
            this.label9.Text = "ORIGEN";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(707, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(181, 31);
            this.label10.TabIndex = 21;
            this.label10.Text = "RESULTADO";
            // 
            // label2_Resultado
            // 
            this.label2_Resultado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2_Resultado.AutoSize = true;
            this.label2_Resultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2_Resultado.Location = new System.Drawing.Point(663, 241);
            this.label2_Resultado.Name = "label2_Resultado";
            this.label2_Resultado.Size = new System.Drawing.Size(68, 13);
            this.label2_Resultado.TabIndex = 27;
            this.label2_Resultado.Text = "ARTICULOS";
            // 
            // codigo_Resultado
            // 
            this.codigo_Resultado.Location = new System.Drawing.Point(666, 195);
            this.codigo_Resultado.Name = "codigo_Resultado";
            this.codigo_Resultado.Size = new System.Drawing.Size(120, 20);
            this.codigo_Resultado.TabIndex = 23;
            // 
            // lstSugerencias_Resultado
            // 
            this.lstSugerencias_Resultado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSugerencias_Resultado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSugerencias_Resultado.ColumnWidth = 10;
            this.lstSugerencias_Resultado.FormattingEnabled = true;
            this.lstSugerencias_Resultado.HorizontalScrollbar = true;
            this.lstSugerencias_Resultado.Location = new System.Drawing.Point(666, 221);
            this.lstSugerencias_Resultado.Name = "lstSugerencias_Resultado";
            this.lstSugerencias_Resultado.Size = new System.Drawing.Size(417, 132);
            this.lstSugerencias_Resultado.TabIndex = 24;
            this.lstSugerencias_Resultado.Visible = false;
            // 
            // lstSugerencias_Origen
            // 
            this.lstSugerencias_Origen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSugerencias_Origen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSugerencias_Origen.ColumnWidth = 10;
            this.lstSugerencias_Origen.FormattingEnabled = true;
            this.lstSugerencias_Origen.HorizontalScrollbar = true;
            this.lstSugerencias_Origen.Location = new System.Drawing.Point(91, 221);
            this.lstSugerencias_Origen.Name = "lstSugerencias_Origen";
            this.lstSugerencias_Origen.Size = new System.Drawing.Size(406, 132);
            this.lstSugerencias_Origen.TabIndex = 25;
            this.lstSugerencias_Origen.Visible = false;
            // 
            // codigo_Origen
            // 
            this.codigo_Origen.Location = new System.Drawing.Point(91, 194);
            this.codigo_Origen.Name = "codigo_Origen";
            this.codigo_Origen.Size = new System.Drawing.Size(120, 20);
            this.codigo_Origen.TabIndex = 26;
            // 
            // label1_Origen
            // 
            this.label1_Origen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1_Origen.AutoSize = true;
            this.label1_Origen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1_Origen.Location = new System.Drawing.Point(88, 241);
            this.label1_Origen.Name = "label1_Origen";
            this.label1_Origen.Size = new System.Drawing.Size(68, 13);
            this.label1_Origen.TabIndex = 22;
            this.label1_Origen.Text = "ARTICULOS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(91, 328);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(878, 242);
            this.dataGridView1.TabIndex = 28;
            // 
            // button_Terminar
            // 
            this.button_Terminar.Location = new System.Drawing.Point(489, 605);
            this.button_Terminar.Name = "button_Terminar";
            this.button_Terminar.Size = new System.Drawing.Size(92, 23);
            this.button_Terminar.TabIndex = 29;
            this.button_Terminar.Text = "TERMINAR";
            this.button_Terminar.UseVisualStyleBackColor = true;
            this.button_Terminar.Click += new System.EventHandler(this.btn_terminar_Click);
            // 
            // Convertir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 692);
            this.Controls.Add(this.button_Terminar);
            this.Controls.Add(this.lstSugerencias_Resultado);
            this.Controls.Add(this.lstSugerencias_Origen);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.codigo_Origen);
            this.Controls.Add(this.codigo_Resultado);
            this.Controls.Add(this.label2_Resultado);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox_Origen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown1_Resultado);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_Resultado);
            this.Controls.Add(this.label1_Origen);
            this.Name = "Convertir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convertir";
            this.Load += new System.EventHandler(this.Convertir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1_Resultado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_Resultado;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1_Resultado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_Origen;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2_Resultado;
        private TextBox codigo_Resultado;
        private ListBox lstSugerencias_Resultado;
        private ListBox lstSugerencias_Origen;
        private TextBox codigo_Origen;
        private Label label1_Origen;
        private DataGridView dataGridView1;
        private Button button_Terminar;
    }
}