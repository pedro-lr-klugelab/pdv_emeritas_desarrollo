using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Ingresar_formula_magistral : Form
	{
		private long venta_id;

		public Ingresar_formula_magistral(long venta_id)
		{
			this.venta_id = venta_id;
			InitializeComponent();
		}

		private void txt_formula_KeyPress(object sender, KeyPressEventArgs e)
		{
			
		}

		private void txt_formula_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					validar_folio();		
				break;
				case 27:
					if(txt_formula.Text.Trim().Length > 0)
					{
						txt_formula.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
			}
		}

		void validar_folio()
		{
			string[] ingreso_formula = txt_formula.Text.Split('$');

			if (ingreso_formula.Length > 0 && ingreso_formula.Length == 2)
			{
				long sucursal_id = 0;
				bool conversion_sucursal = long.TryParse(ingreso_formula[0], out sucursal_id);

				if (conversion_sucursal)
				{
					if (Convert.ToInt64(Config_helper.get_config_local("sucursal_id")) == sucursal_id)
					{
						validar_formula(ingreso_formula[1].ToString());
					}
					else
					{
						DAO_Sucursales dao_sucursales = new DAO_Sucursales();
						var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

						if (sucursal_data.sucursal_id > 0)
						{
							MessageBox.Show(this, string.Format("Esta fórmula magistral solo es canjeable en la sucursal *{0}*", sucursal_data.nombre.ToUpper()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							MessageBox.Show(this, "Código Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
				else
				{
					MessageBox.Show(this, "Código Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this, "Código Inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        void validar_formula(string formula_id)
        {
            DAO_Formulas dao_formulas = new DAO_Formulas();
            var response = dao_formulas.formula_disponible(formula_id);

            if (response.status)
            {
                DAO_Ventas dao_ventas = new DAO_Ventas();

                try
                {
                    long articuloId = Convert.ToInt64(Config_helper.get_config_global("formula_magistral_articulo_id"));
                    bool exito = dao_ventas.insertar_formula_detallado(venta_id, articuloId, formula_id);

                    if (exito)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this,
                            $"La fórmula no pudo ser registrada en la venta. Verifique si el artículo con ID '{articuloId}' existe en la tabla 'articulos'.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    long articuloId = Convert.ToInt64(Config_helper.get_config_global("formula_magistral_articulo_id"));
                    MessageBox.Show(this,
                        $"Error al insertar artículo. Puede que el artículo con ID '{articuloId}' no exista en 'articulos'.\n\nDetalles: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show(this, response.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
