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

namespace Farmacontrol_PDV.FORMS.ventas.ingresar_prepago
{
	public partial class Entrega_parcial_prepago : Form
	{
		List<DTO_Detallado_prepago> detallado_prepago = new List<DTO_Detallado_prepago>();
		public List<DTO_Detallado_ventas_vista_previa> detallado_ventas_vista_previa = new List<DTO_Detallado_ventas_vista_previa>();
		public bool para_prepago = false;

		public Entrega_parcial_prepago(List<DTO_Detallado_prepago> detallado_prepago)
		{
			this.detallado_prepago = detallado_prepago;
			InitializeComponent();

			get_productos_en_existencia();
			dgv_entrega_parcial.ClearSelection();
		}


		void get_productos_en_existencia()
		{
			DAO_Existencias dao_existencias = new DAO_Existencias();

			List<DTO_Existencia> existencia_articulos_prepago = new List<DTO_Existencia>();

			string[] articulos_ids = new string[detallado_prepago.Count];

			int count = 0;

			foreach(DTO_Detallado_prepago item in detallado_prepago)
			{
				articulos_ids[count] = item.articulo_id.ToString();
				count++;
			}

			existencia_articulos_prepago = dao_existencias.get_articulos_existencias_prepago(articulos_ids);

			detallado_ventas_vista_previa = new List<DTO_Detallado_ventas_vista_previa>();

			foreach (DTO_Detallado_prepago existencia_prepago in detallado_prepago)
			{
				long existencia_necesaria = existencia_prepago.cantidad;
				long existencia_acumulada = 0;

				foreach (DTO_Existencia existencia_vendible in existencia_articulos_prepago)
				{
					if (existencia_vendible.existencia > 0)
					{
						if (existencia_prepago.articulo_id == existencia_vendible.articulo_id)
						{
							if ((existencia_necesaria - existencia_acumulada) > 0)
							{
								if (existencia_acumulada < existencia_necesaria)
								{
									DTO_Detallado_ventas_vista_previa vista_previa = new DTO_Detallado_ventas_vista_previa();
									vista_previa.amecop = existencia_prepago.amecop;
									vista_previa.articulo_id = existencia_prepago.articulo_id;
									vista_previa.caducidad = (existencia_vendible.caducidad.Equals("0000-00-00")) ? "SIN CAD" : Misc_helper.fecha(existencia_vendible.caducidad,"CADUCIDAD");
									vista_previa.lote = existencia_vendible.lote;
									vista_previa.producto = existencia_prepago.producto;

									if (existencia_vendible.existencia < (existencia_necesaria - existencia_acumulada))
									{
										vista_previa.cantidad = existencia_vendible.existencia;
										detallado_ventas_vista_previa.Add(vista_previa);

										existencia_acumulada += existencia_vendible.existencia;
									}
									else
									{
										if (existencia_acumulada == 0)
										{
											vista_previa.cantidad = existencia_necesaria;
										}
										else
										{
											vista_previa.cantidad = (existencia_necesaria - existencia_acumulada);
										}
										detallado_ventas_vista_previa.Add(vista_previa);

										break;
									}
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

			dgv_entrega_parcial.DataSource = detallado_ventas_vista_previa;
		}

		private void Entrega_parcial_prepago_Shown(object sender, EventArgs e)
		{
			dgv_entrega_parcial.ClearSelection();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_procesar_Click(object sender, EventArgs e)
		{
			bool all_check_fisico = true;

			foreach(DataGridViewRow row in dgv_entrega_parcial.Rows)
			{
				if(!Convert.ToBoolean(row.Cells["confirma_fisico"].Value))
				{
					all_check_fisico = false;
					break;
				}
			}

			if(all_check_fisico)
			{
				para_prepago = true;
				this.Close();		
			}
			else
			{
				MessageBox.Show(this,"Es necesario confirmar que tienes fisicamente los productos que se le entregaran parcialmente al cliente","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
	}
}
