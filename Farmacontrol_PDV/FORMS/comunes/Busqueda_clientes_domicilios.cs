using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;

namespace Farmacontrol_PDV.FORMS.comunes
{
	public partial class Busqueda_clientes_domicilios : Form
	{
		public string cliente_domicilio_id = null;

		public Busqueda_clientes_domicilios()
		{
			InitializeComponent();
		}

		private void txt_nombre_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode =  Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 27:
					if(txt_nombre.TextLength > 0){ txt_nombre.Text = ""; } else { this.Close(); }
				break;
				case 13:
                    buscar_clientes();	
				break;
				case 40:
					if(dgv_domicilios.Rows.Count > 0)
                    {
                        dgv_domicilios.CurrentCell = dgv_domicilios.Rows[0].Cells[0];
                        dgv_domicilios.Rows[0].Selected = true;
                        dgv_domicilios.Focus();
                    }
				break;
			}
		}

        void buscar_clientes()
        {
          

            if (txt_nombre.TextLength > 3)
            {
                DAO.DAO_Clientes dao_clientes = new DAO.DAO_Clientes();

                //VALIDA SI ES UNA CADENA O UN NUMERO LO QUE SE ESCRIBIO 
                string cadena = txt_nombre.Text.ToString().Trim();
                Int64 numero = 0;
                bool es_nombre = true;
                if (!Int64.TryParse(cadena, out numero))
                {

                    es_nombre = true;

                }
                else
                {
                    es_nombre = false;
                }

                dgv_domicilios.DataSource = dao_clientes.get_clientes_domicilios(txt_nombre.Text, es_nombre);
                dgv_domicilios.ClearSelection();

                txt_nombre.Focus();
            }
            else
            {
                MessageBox.Show(this, "Error,la busqueda debe de tener al menos 4 elementos para poder realizarla ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
        }

		private void dgv_domicilios_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode =  Convert.ToInt32(e.KeyCode);
			
			switch(keycode)
			{
				case 27:
					dgv_domicilios.ClearSelection();
					txt_nombre.Focus();
				break;
				case 13:
					if(dgv_domicilios.SelectedRows[0].Cells["tipo"].Value.ToString().Equals("VARIOS")){
						Busqueda_domicilios domicilios = new Busqueda_domicilios(dgv_domicilios.SelectedRows[0].Cells["nombre"].Value.ToString(), dgv_domicilios.SelectedRows[0].Cells["cliente_id"].Value.ToString());
						domicilios.ShowDialog();
							if (domicilios.cliente_domicilio_id != ""){ cliente_domicilio_id = domicilios.cliente_domicilio_id; this.Close(); }
					}else{
						cliente_domicilio_id = 	dgv_domicilios.SelectedRows[0].Cells["columna_cliente_domicilio_id"].Value.ToString(); this.Close();
					}
				break;
			}
		}

		private void txt_nombre_Enter(object sender, EventArgs e)
		{
			dgv_domicilios.ClearSelection();
		}

        private void btn_registrar_cliente_Click(object sender, EventArgs e)
        {
            Registro_clientes registro = new Registro_clientes(true);
            registro.ShowDialog();

            if(registro.usar_cliente_domicilio_id)
            {
                cliente_domicilio_id = registro.cliente_domicilio_id;
                this.Close();
            }
        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
