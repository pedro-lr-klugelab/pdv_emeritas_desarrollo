using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.ventas.devolucion
{
	public partial class Tipo_devolucion : Form
	{
		public string tipo_devolucion = "";
        long venta_id;

		public Tipo_devolucion(long venta_id)
		{
            this.venta_id = venta_id;
			InitializeComponent();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Tipo_devolucion_Shown(object sender, EventArgs e)
		{
            DAO_Ventas dao_ventas = new DAO_Ventas();
            var venta_data = dao_ventas.get_venta_data(venta_id);

            //if (venta_data.cliente_domicilio_id != "")
            //{
                //cbb_tipo_devolucion.DataSource = new BindingSource(new Dictionary<string, string>(){
              //  {"EFECTIVO", "EFECTIVO"}
			//}, null);
           // }
            //else
            //{
                cbb_tipo_devolucion.DataSource = new BindingSource(new Dictionary<string, string>(){
                {"EFECTIVO", "EFECTIVO"}
			}, null);
            //}

			cbb_tipo_devolucion.DisplayMember = "Key";
			cbb_tipo_devolucion.ValueMember = "Value";

			cbb_tipo_devolucion.DroppedDown = true;
			cbb_tipo_devolucion.Focus();
		}

        void asignar_devolucion()
        {
            string tipo_seleccion = cbb_tipo_devolucion.SelectedValue.ToString();

            if (tipo_seleccion.Equals("EFECTIVO"))
            {
                Login_form login = new Login_form();
                login.ShowDialog();

                if (login.empleado_id != null)
                {
                    DAO_Login dao_login = new DAO_Login();

                    if (dao_login.empleado_es_encargado((long)login.empleado_id))
                    {
                        DialogResult dr = MessageBox.Show(this, "Se afectara la devolucion como " + tipo_seleccion + ", ¿Deseas continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            tipo_devolucion = tipo_seleccion.ToString();
                            this.Close();
                        }
                        else
                        {
                            cbb_tipo_devolucion.DroppedDown = true;
                            cbb_tipo_devolucion.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Solo el encargado puede afectar las devoluciones por efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show(this, "Se afectara la devolucion como " + tipo_seleccion + ", ¿Deseas continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    tipo_devolucion = tipo_seleccion.ToString();
                    this.Close();
                }
                else
                {
                    cbb_tipo_devolucion.DroppedDown = true;
                    cbb_tipo_devolucion.Focus();
                }
            }
        }

		private void cbb_tipo_devolucion_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
                    asignar_devolucion();
				break;
				case 27:
					tipo_devolucion = "";
					this.Close();
				break;
			}
		}

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            asignar_devolucion();
        }
	}
}
