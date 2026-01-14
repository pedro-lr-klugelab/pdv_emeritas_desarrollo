using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Custom_alert : Form
	{
        public string titulo_mensaje = "";
        public string mensaje = "";
		public Custom_alert(string titulo_mensaje, string mensaje)
		{
			InitializeComponent();
			//lbl_titulo.Text = titulo_mensaje;
			//lbl_texto.Text = mensaje;
            this.titulo_mensaje = titulo_mensaje;
            this.mensaje = mensaje;
            Control.CheckForIllegalCrossThreadCalls = false;
		}

		private void btn_aceptar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void Custom_alert_Shown(object sender, EventArgs e)
        {
            lbl_titulo.Text = titulo_mensaje;
            lbl_texto.Text = mensaje;
        }

        private void lbl_texto_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void lbl_texto_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void Custom_alert_Load(object sender, EventArgs e)
        {
            Thread hilo = new Thread(new ThreadStart(MyCallbackFunction));
            hilo.Start();  
        }

        private void Custom_alert_Activated(object sender, EventArgs e)
        {
            
            
        }

        static void MyCallbackFunction()
        {
           
            try
            { 
                Thread.Sleep(3000);
            
                if (Custom_alert.ActiveForm != null && Custom_alert.ActiveForm.Name.Equals("Custom_alert"))
                    Custom_alert.ActiveForm.Close();
            }
            catch(Exception ex)
			{
				Log_error.log(ex);
            }

        }

	}
}
