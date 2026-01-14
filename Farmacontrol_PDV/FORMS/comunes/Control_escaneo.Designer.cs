namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
	partial class Control_escaneo
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel_contenedor_imagenes = new System.Windows.Forms.FlowLayoutPanel();
            this.pic_image_complete = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_brillo = new System.Windows.Forms.TextBox();
            this.btn_mas_brillo = new System.Windows.Forms.Button();
            this.btn_menos_brillo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_contraste = new System.Windows.Forms.TextBox();
            this.btn_mas_contraste = new System.Windows.Forms.Button();
            this.btn_menos_contraste = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.btn_escanear = new System.Windows.Forms.Button();
            this.btn_zoom_in = new System.Windows.Forms.Button();
            this.btn_zoom_out = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_rotate_left = new System.Windows.Forms.Button();
            this.lbl_porcentaje_zoom = new System.Windows.Forms.Label();
            this.btn_rotate_right = new System.Windows.Forms.Button();
            this.tcb_zoom = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_image_complete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcb_zoom)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.txt_brillo);
            this.splitContainer1.Panel2.Controls.Add(this.btn_mas_brillo);
            this.splitContainer1.Panel2.Controls.Add(this.btn_menos_brillo);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txt_contraste);
            this.splitContainer1.Panel2.Controls.Add(this.btn_mas_contraste);
            this.splitContainer1.Panel2.Controls.Add(this.btn_menos_contraste);
            this.splitContainer1.Panel2.Controls.Add(this.btn_cancelar);
            this.splitContainer1.Panel2.Controls.Add(this.btn_guardar);
            this.splitContainer1.Panel2.Controls.Add(this.btn_escanear);
            this.splitContainer1.Panel2.Controls.Add(this.btn_zoom_in);
            this.splitContainer1.Panel2.Controls.Add(this.btn_zoom_out);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.btn_rotate_left);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_porcentaje_zoom);
            this.splitContainer1.Panel2.Controls.Add(this.btn_rotate_right);
            this.splitContainer1.Panel2.Controls.Add(this.tcb_zoom);
            this.splitContainer1.Size = new System.Drawing.Size(966, 537);
            this.splitContainer1.SplitterDistance = 494;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.panel_contenedor_imagenes);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pic_image_complete);
            this.splitContainer2.Size = new System.Drawing.Size(966, 494);
            this.splitContainer2.SplitterDistance = 178;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel_contenedor_imagenes
            // 
            this.panel_contenedor_imagenes.AutoScroll = true;
            this.panel_contenedor_imagenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_contenedor_imagenes.Location = new System.Drawing.Point(0, 0);
            this.panel_contenedor_imagenes.Name = "panel_contenedor_imagenes";
            this.panel_contenedor_imagenes.Size = new System.Drawing.Size(178, 494);
            this.panel_contenedor_imagenes.TabIndex = 0;
            // 
            // pic_image_complete
            // 
            this.pic_image_complete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_image_complete.Location = new System.Drawing.Point(0, 0);
            this.pic_image_complete.Name = "pic_image_complete";
            this.pic_image_complete.Size = new System.Drawing.Size(784, 494);
            this.pic_image_complete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_image_complete.TabIndex = 0;
            this.pic_image_complete.TabStop = false;
            this.pic_image_complete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_image_complete_MouseDown);
            this.pic_image_complete.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_image_complete_MouseMove);
            this.pic_image_complete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_image_complete_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(555, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Brillo:";
            // 
            // txt_brillo
            // 
            this.txt_brillo.Location = new System.Drawing.Point(591, 10);
            this.txt_brillo.MaxLength = 3;
            this.txt_brillo.Name = "txt_brillo";
            this.txt_brillo.ReadOnly = true;
            this.txt_brillo.Size = new System.Drawing.Size(32, 20);
            this.txt_brillo.TabIndex = 36;
            // 
            // btn_mas_brillo
            // 
            this.btn_mas_brillo.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.brillo_mas_icon;
            this.btn_mas_brillo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_mas_brillo.Location = new System.Drawing.Point(665, 7);
            this.btn_mas_brillo.Name = "btn_mas_brillo";
            this.btn_mas_brillo.Size = new System.Drawing.Size(36, 27);
            this.btn_mas_brillo.TabIndex = 35;
            this.btn_mas_brillo.UseVisualStyleBackColor = true;
            this.btn_mas_brillo.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_menos_brillo
            // 
            this.btn_menos_brillo.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.brillo_menos_icon;
            this.btn_menos_brillo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_menos_brillo.Location = new System.Drawing.Point(628, 7);
            this.btn_menos_brillo.Name = "btn_menos_brillo";
            this.btn_menos_brillo.Size = new System.Drawing.Size(36, 27);
            this.btn_menos_brillo.TabIndex = 34;
            this.btn_menos_brillo.UseVisualStyleBackColor = true;
            this.btn_menos_brillo.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Contraste:";
            // 
            // txt_contraste
            // 
            this.txt_contraste.Location = new System.Drawing.Point(427, 10);
            this.txt_contraste.MaxLength = 3;
            this.txt_contraste.Name = "txt_contraste";
            this.txt_contraste.ReadOnly = true;
            this.txt_contraste.Size = new System.Drawing.Size(33, 20);
            this.txt_contraste.TabIndex = 32;
            // 
            // btn_mas_contraste
            // 
            this.btn_mas_contraste.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.contrast__positivo;
            this.btn_mas_contraste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_mas_contraste.Location = new System.Drawing.Point(505, 6);
            this.btn_mas_contraste.Name = "btn_mas_contraste";
            this.btn_mas_contraste.Size = new System.Drawing.Size(36, 27);
            this.btn_mas_contraste.TabIndex = 31;
            this.btn_mas_contraste.UseVisualStyleBackColor = true;
            this.btn_mas_contraste.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_menos_contraste
            // 
            this.btn_menos_contraste.BackgroundImage = global::Farmacontrol_PDV.Properties.Resources.contrast_menos;
            this.btn_menos_contraste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_menos_contraste.Location = new System.Drawing.Point(465, 6);
            this.btn_menos_contraste.Name = "btn_menos_contraste";
            this.btn_menos_contraste.Size = new System.Drawing.Size(36, 27);
            this.btn_menos_contraste.TabIndex = 30;
            this.btn_menos_contraste.UseVisualStyleBackColor = true;
            this.btn_menos_contraste.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Location = new System.Drawing.Point(883, 9);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(75, 23);
            this.btn_cancelar.TabIndex = 26;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(802, 9);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 25;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // btn_escanear
            // 
            this.btn_escanear.Location = new System.Drawing.Point(721, 9);
            this.btn_escanear.Name = "btn_escanear";
            this.btn_escanear.Size = new System.Drawing.Size(75, 23);
            this.btn_escanear.TabIndex = 24;
            this.btn_escanear.Text = "Escanear";
            this.btn_escanear.UseVisualStyleBackColor = true;
            this.btn_escanear.Click += new System.EventHandler(this.btn_escanear_Click);
            // 
            // btn_zoom_in
            // 
            this.btn_zoom_in.Image = global::Farmacontrol_PDV.Properties.Resources.zoom_in;
            this.btn_zoom_in.Location = new System.Drawing.Point(197, 7);
            this.btn_zoom_in.Name = "btn_zoom_in";
            this.btn_zoom_in.Size = new System.Drawing.Size(36, 27);
            this.btn_zoom_in.TabIndex = 23;
            this.btn_zoom_in.UseVisualStyleBackColor = true;
            this.btn_zoom_in.Click += new System.EventHandler(this.btn_zoom_in_Click);
            // 
            // btn_zoom_out
            // 
            this.btn_zoom_out.Image = global::Farmacontrol_PDV.Properties.Resources.zoom_out;
            this.btn_zoom_out.Location = new System.Drawing.Point(237, 7);
            this.btn_zoom_out.Name = "btn_zoom_out";
            this.btn_zoom_out.Size = new System.Drawing.Size(36, 27);
            this.btn_zoom_out.TabIndex = 22;
            this.btn_zoom_out.UseVisualStyleBackColor = true;
            this.btn_zoom_out.Click += new System.EventHandler(this.btn_zoom_out_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Zoom:";
            // 
            // btn_rotate_left
            // 
            this.btn_rotate_left.Image = global::Farmacontrol_PDV.Properties.Resources.left_black;
            this.btn_rotate_left.Location = new System.Drawing.Point(287, 7);
            this.btn_rotate_left.Name = "btn_rotate_left";
            this.btn_rotate_left.Size = new System.Drawing.Size(36, 27);
            this.btn_rotate_left.TabIndex = 21;
            this.btn_rotate_left.UseVisualStyleBackColor = true;
            this.btn_rotate_left.Click += new System.EventHandler(this.btn_rotate_left_Click);
            // 
            // lbl_porcentaje_zoom
            // 
            this.lbl_porcentaje_zoom.AutoSize = true;
            this.lbl_porcentaje_zoom.Location = new System.Drawing.Point(165, 14);
            this.lbl_porcentaje_zoom.Name = "lbl_porcentaje_zoom";
            this.lbl_porcentaje_zoom.Size = new System.Drawing.Size(33, 13);
            this.lbl_porcentaje_zoom.TabIndex = 19;
            this.lbl_porcentaje_zoom.Text = "100%";
            // 
            // btn_rotate_right
            // 
            this.btn_rotate_right.Image = global::Farmacontrol_PDV.Properties.Resources.right_black;
            this.btn_rotate_right.Location = new System.Drawing.Point(327, 7);
            this.btn_rotate_right.Name = "btn_rotate_right";
            this.btn_rotate_right.Size = new System.Drawing.Size(36, 27);
            this.btn_rotate_right.TabIndex = 20;
            this.btn_rotate_right.UseVisualStyleBackColor = true;
            this.btn_rotate_right.Click += new System.EventHandler(this.btn_rotate_right_Click);
            // 
            // tcb_zoom
            // 
            this.tcb_zoom.AutoSize = false;
            this.tcb_zoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tcb_zoom.Enabled = false;
            this.tcb_zoom.LargeChange = 50;
            this.tcb_zoom.Location = new System.Drawing.Point(42, 12);
            this.tcb_zoom.Maximum = 200;
            this.tcb_zoom.Minimum = 100;
            this.tcb_zoom.Name = "tcb_zoom";
            this.tcb_zoom.Size = new System.Drawing.Size(125, 22);
            this.tcb_zoom.SmallChange = 5;
            this.tcb_zoom.TabIndex = 17;
            this.tcb_zoom.TickFrequency = 50;
            this.tcb_zoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tcb_zoom.Value = 100;
            // 
            // Control_escaneo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 537);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Control_escaneo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de escaneo";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_image_complete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcb_zoom)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btn_guardar;
		private System.Windows.Forms.Button btn_escanear;
		private System.Windows.Forms.Button btn_zoom_in;
		private System.Windows.Forms.Button btn_zoom_out;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btn_rotate_left;
		private System.Windows.Forms.Label lbl_porcentaje_zoom;
		private System.Windows.Forms.Button btn_rotate_right;
		private System.Windows.Forms.TrackBar tcb_zoom;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.FlowLayoutPanel panel_contenedor_imagenes;
		private System.Windows.Forms.PictureBox pic_image_complete;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_mas_contraste;
        private System.Windows.Forms.Button btn_menos_contraste;
        private System.Windows.Forms.TextBox txt_contraste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_mas_brillo;
        private System.Windows.Forms.Button btn_menos_brillo;
        private System.Windows.Forms.TextBox txt_brillo;
        private System.Windows.Forms.Label label2;

	}
}