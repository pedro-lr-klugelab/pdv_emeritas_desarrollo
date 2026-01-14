using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.ventas.elaborar_formula
{
	public partial class Formula_sucursal : Form
	{
		long sucursal_id;
		string formula_id_rest;
		DTO_Formula formula = new DTO_Formula();
		List<DTO_Detallado_formula_elaborada> articulos_usados_localmente = new List<DTO_Detallado_formula_elaborada>();
		List<DTO_Detallado_formulas> detallado = new List<DTO_Detallado_formulas>();
		bool existencias_correctas = true;
		string articulos_ids = "";

		public Formula_sucursal(long sucursal_id, string formula_id)
		{
			this.sucursal_id = sucursal_id;
			this.formula_id_rest = formula_id;
			InitializeComponent();
			get_informacion_formula();
		}

		void get_informacion_formula()
		{
			DAO_Formulas dao_formulas = new DAO_Formulas();
			formula = dao_formulas.get_informacion_formula(formula_id_rest);
			rellenar_informacion();
		}

		void rellenar_informacion()
		{
			txt_cliente.Text = formula.nombre_cliente;
			txt_doctor.Text = formula.nombre_doctor;
			txt_instrucciones.Text = formula.comentarios;
            txt_fecha_creado.Text = formula.fecha_creado.ToString("dd/MMM/yyyy h:mm:ss tt").ToUpper().Replace(".", "");

			DAO_Formulas dao_formulas = new DAO_Formulas();
			detallado = dao_formulas.get_detallado_formulas(formula_id_rest);
			dgv_detallado_formulas.DataSource = detallado; 
			get_totales();
		}

        void validar_existencia_articulos()
        {
            existencias_correctas = true;
            articulos_ids = "";

            foreach (DTO_Detallado_formulas det in detallado)
            {
                if (det.articulo_id != null)
                {
                    articulos_ids += (articulos_ids.Equals("")) ? det.articulo_id.ToString() : ", " + det.articulo_id;
                }
            }

            if (!articulos_ids.Equals(""))
            {
                DAO_Existencias dao_existencias = new DAO_Existencias();
                List<DTO_Existencia> lista_existencias_local = dao_existencias.get_existencias_articulos_formula(articulos_ids);

                foreach (DataGridViewRow row in dgv_detallado_formulas.Rows)
                {
                    if (row.Cells["articulo_id"].Value != null)
                    {
                        bool encontrado = false;

                        foreach (DTO_Existencia existencia in lista_existencias_local)
                        {
                            if (existencia.articulo_id == Convert.ToInt64(row.Cells["articulo_id"].Value))
                            {
                                encontrado = true;

                                decimal requerido = Convert.ToDecimal(row.Cells["cantidad"].Value);

                                if (existencia.volumen < requerido)
                                {
                                    existencias_correctas = false;
                                    dgv_detallado_formulas.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                                }
                            }
                        }

                        if (!encontrado)
                        {
                            existencias_correctas = false;
                            dgv_detallado_formulas.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
                        }
                    }
                }

                dgv_detallado_formulas.Refresh();
            }
        }


        void get_totales()
		{
			decimal subtotal = 0;
			decimal importe_iva = 0;
			decimal importe_ieps = 0;
			decimal total = 0;

			foreach (DTO_Detallado_formulas item in detallado)
			{
				subtotal += item.subtotal;
				total += item.total;
			}

			importe_iva = Convert.ToDecimal(subtotal * Convert.ToDecimal(0.16));

			txt_iva.Text = importe_iva.ToString("C2");
			txt_ieps.Text = importe_ieps.ToString("C2");
			txt_total.Text = total.ToString("C2");
			txt_subtotal.Text = subtotal.ToString("C2");
		}

		private void Formula_sucursal_Shown(object sender, EventArgs e)
		{
			dgv_detallado_formulas.ClearSelection();
		}

		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			validar_existencia_articulos();

			if(existencias_correctas)
			{
				imprimir_formula();
				btn_terminar.Enabled = true;
			}
			else
			{
                btn_terminar.Enabled = false;

                MessageBox.Show(this,"Los productos marcados en rojo indican que no cuentas con la existencia necesaria para la creación de la formula","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

        void imprimir_formula()
        {
            DAO_Existencias dao_existencias = new DAO_Existencias();

            // Ahora consulta desde la tabla materias_primas
            List<DTO_Existencia> existencia_articulos_formulas = dao_existencias.get_existencias_articulos_formula(articulos_ids);

            List<DTO_Detallado_formula_elaborada> detallado_formula_local = new List<DTO_Detallado_formula_elaborada>();

            foreach (DTO_Detallado_formulas existencia_detallado_formulas in detallado)
            {
                if (existencia_detallado_formulas.articulo_id != null)
                {
                    decimal existencia_necesaria = existencia_detallado_formulas.cantidad;
                    decimal existencia_acumulada = 0;

                    foreach (DTO_Existencia existencia_vendible in existencia_articulos_formulas)
                    {
                        if (existencia_vendible.existencia > 0)
                        {
                            if (existencia_detallado_formulas.articulo_id == existencia_vendible.articulo_id)
                            {
                                if ((existencia_necesaria - existencia_acumulada) > 0)
                                {
                                    if (existencia_acumulada < existencia_necesaria)
                                    {
                                        DTO_Detallado_formula_elaborada articulo_local = new DTO_Detallado_formula_elaborada();
                                        articulo_local.amecop = existencia_detallado_formulas.amecop;
                                        articulo_local.nombre = existencia_detallado_formulas.nombre;
                                        articulo_local.articulo_id = (long)existencia_detallado_formulas.articulo_id;
                                        articulo_local.caducidad = ""; // No hay caducidad en materias_primas
                                        articulo_local.lote = "";      // No hay lote en materias_primas

                                        decimal disponible = existencia_vendible.existencia;
                                        decimal faltante = existencia_necesaria - existencia_acumulada;

                                        if (disponible < faltante)
                                        {
                                            articulo_local.cantidad = disponible;
                                            existencia_acumulada += disponible;
                                        }
                                        else
                                        {
                                            articulo_local.cantidad = faltante;
                                            existencia_acumulada += faltante;
                                        }

                                        detallado_formula_local.Add(articulo_local);

                                        if (existencia_acumulada >= existencia_necesaria)
                                            break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            articulos_usados_localmente = detallado_formula_local;

            Elaborar_formula ticket = new Elaborar_formula();
            ticket.construccion_ticket(formula, articulos_usados_localmente);
            ticket.print();
            btn_terminar.Enabled = true;
        }


        private void btn_terminar_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form();
			login.ShowDialog();

			if(login.empleado_id != null)
			{
				Formula_articulos_local local = new Formula_articulos_local(articulos_usados_localmente);
				local.ShowDialog();

				if (local.terminar_formula)
				{
					DAO_Formulas dao_formulas = new DAO_Formulas();

					if (dao_formulas.registrar_formula_elaborada((long)login.empleado_id, formula_id_rest, articulos_usados_localmente, txt_comentarios_elaboracion.Text) == true)
					{
						Elaborar_formula ticket = new Elaborar_formula();
						ticket.construccion_ticket(formula ,articulos_usados_localmente,true);
						ticket.print();
						MessageBox.Show(this, "Fórmula elaborada y registrada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.Close();
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al intentar registrar la elaboración de la fórmula, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}	
			}
		}

		private void Formula_sucursal_FormClosing(object sender, FormClosingEventArgs e)
		{
			
		}

        private void txt_comentarios_elaboracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
	}
}
