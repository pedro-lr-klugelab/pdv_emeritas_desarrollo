using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Ingresar_prepago : Form
	{
		public bool prepago_afectado = false;
		decimal monto = 0;
		long prepago_id;
		long venta_id;
		public bool cancelar_prepago = false;

		public Ingresar_prepago(long venta_id)
		{
			this.venta_id = venta_id;
			InitializeComponent();
		}

		private void txt_codigo_prepago_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					generar_venta_prepago();
				break;
				case 27:
					if(txt_codigo_prepago.Text.Trim().Length > 0)
					{
						txt_codigo_prepago.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
			}
		}

		void generar_venta_prepago()
		{
            try
            {
			    string[] ingreso_prepago = txt_codigo_prepago.Text.Trim().Split('$');

			    if(ingreso_prepago.Length > 0 && ingreso_prepago.Length == 2)
			    {
				    long sucursal_id = 0;
				    bool conversion_sucursal = long.TryParse(ingreso_prepago[0], out sucursal_id);

				    if(conversion_sucursal)
				    {
					    if(Convert.ToInt64(Config_helper.get_config_local("sucursal_id")) == sucursal_id)
					    {
						    DAO_Prepago dao_prepago = new DAO_Prepago();
						    DTO_Prepago prepago = new DTO_Prepago();

						    prepago = dao_prepago.get_informacion_prepago(ingreso_prepago[1]);

						    if (prepago.prepago_id > 0)
						    {
							    if (prepago.fecha_canje == null)
							    {
								    if (prepago.fecha_cancelado == null)
								    {
									    monto = prepago.monto;
									    prepago_id = prepago.prepago_id;
									    DAO_Existencias dao_existencias = new DAO_Existencias();

									    List<DTO_Detallado_prepago> detallado_prepago = new List<DTO_Detallado_prepago>();
									    detallado_prepago = dao_prepago.get_detallado_prepago(prepago.prepago_id);

									    List<DTO_Existencia> existencia_articulos_prepago = new List<DTO_Existencia>();
									    existencia_articulos_prepago = dao_existencias.get_articulos_existencias_prepago(prepago.codigo);

									    bool generar_venta = true;

									    foreach (DTO_Detallado_prepago det_prep in detallado_prepago)
									    {
										    long cantidad_necesaria = (det_prep.cantidad - det_prep.cantidad_entregada);
										    long cantidad_disponible = 0;

										    foreach (DTO_Existencia det_ex in existencia_articulos_prepago)
										    {
											    if (det_ex.articulo_id == det_prep.articulo_id)
											    {
												    cantidad_disponible += det_ex.existencia;
											    }
										    }

										    if (cantidad_disponible < cantidad_necesaria)
										    {
											    generar_venta = false;
											    txt_codigo_prepago.Text = "";
											    MessageBox.Show(this, string.Format("No tienes suficientes existencias de * {0} *, EXISTENCIA VENDIBLE = {1}, EXISTENCIA NECESARIA = {2}", det_prep.producto, cantidad_disponible, cantidad_necesaria), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
											    break;
										    }
									    }

									    if (generar_venta)
									    {
										    generar_productos_venta(existencia_articulos_prepago, detallado_prepago);
									    }
								    }
								    else
								    {
									    DialogResult dr = MessageBox.Show(
										    this,
                                            string.Format("Este encargo ha sido cancelado por {0} el {1}, se entregara al cliente {2} {3} equivalente a {4}, ¿desea proceder con la devolucion? ", prepago.nombre_empleado_cancela.ToUpper(), Convert.ToDateTime(prepago.fecha_cancelado).ToString("dd-MMM-yyy h:mm:ss tt").ToUpper().Replace(".", ""), (prepago.tipo_devolucion.Equals("VALE")) ? "un" : "el", prepago.tipo_devolucion, prepago.monto.ToString("C2")),
										    "Prepago Cancelado", 
										    MessageBoxButtons.YesNoCancel, 
										    MessageBoxIcon.Question,
										    MessageBoxDefaultButton.Button2
									    );

									    if(dr == DialogResult.Yes)
									    {
										    var detallado_entrega_parcial = dao_prepago.get_productos_entrega_parcial(prepago.prepago_id);

										    if(detallado_entrega_parcial.Count > 0)
										    {
											    Cancelar_prepago cancelacion_prepago = new Cancelar_prepago(prepago.prepago_id,detallado_entrega_parcial);
											    cancelacion_prepago.ShowDialog();

											    if(cancelacion_prepago.prepago_afectado)
											    {
												    prepago_afectado = cancelacion_prepago.prepago_afectado;
											    }
										    }
										    else
										    {
											    DialogResult dr2 = MessageBox.Show(this, "Al iniciar el proceso de devolución de prepago no podra cancelar ni modificar nada, ¿Desea continuar?", "Devolución prepago", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

											    if (dr2 == DialogResult.Yes)
											    {
                                                    if (dao_prepago.cancelar_prepago(prepago.prepago_id, detallado_entrega_parcial))
												    {
													    MessageBox.Show(this, "Prepago cancelado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
													    prepago_afectado = true;
												    }
												    else
												    {
													    MessageBox.Show(this, "Ocurrio un error al intentar cancelar el prepago, notifique a su adminstrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
												    }
											    }
										    }
									    }
									
									    if(prepago_afectado)
									    {
										    DAO_Vales_efectivo dao_vales = new DAO_Vales_efectivo();
										    decimal total_devolucion = dao_vales.get_total_devolucion_prepago(prepago.prepago_id);

										    /*if(prepago.tipo_devolucion.Equals("VALE"))
										    {
											    if(total_devolucion > 0)
											    {
												    string vale_id = dao_vales.generar_vale_efectivo_prepago(prepago.prepago_id);
												    Vale_efectivo ticket_vale = new Vale_efectivo();
												    ticket_vale.construccion_ticket(vale_id,true);
												    ticket_vale.print();
											    }
										    }
										    else
										    {*/
											    if (total_devolucion > 0)
											    {
												    Recibo_efectivo_prepago ticket_efectivo = new Recibo_efectivo_prepago();
												    ticket_efectivo.construccion_ticket(prepago.prepago_id);
												    ticket_efectivo.print();
											    }
										    //}

										    this.Close();	
									    }
								    }
							    }
							    else
							    {
								    MessageBox.Show(this, "Este prepago ya fue canjeado, retenga el prepago y reportelo a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							    }
						    }
						    else
						    {
							    MessageBox.Show(this, "Codigo de prepago no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						    }	
					    }
					    else
					    {
						    DAO_Sucursales dao_sucursales = new DAO_Sucursales();
						    var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(sucursal_id));

						    if(sucursal_data.sucursal_id > 0)
						    {
							    MessageBox.Show(this, string.Format("Este prepago solo es canjeable en la sucursal *{0}*", sucursal_data.nombre.ToUpper()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);	
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
		}

		void generar_productos_venta(List<DTO_Existencia> existencias_productos_prepago, List<DTO_Detallado_prepago> detallado_prepago)
		{
			List<DTO_Detallado_ventas_vista_previa> detallado_ventas_vista_previa = new List<DTO_Detallado_ventas_vista_previa>();

			foreach(DTO_Detallado_prepago existencia_prepago in detallado_prepago)
			{
				long existencia_necesaria = (existencia_prepago.cantidad - existencia_prepago.cantidad_entregada);
				long existencia_acumulada = 0;

				foreach(DTO_Existencia existencia_vendible in existencias_productos_prepago)
				{
					if(existencia_vendible.existencia > 0)
					{
						if (existencia_prepago.articulo_id == existencia_vendible.articulo_id)
						{
							if ((existencia_necesaria - existencia_acumulada) > 0)
							{
								if (existencia_acumulada < existencia_necesaria)
								{
									DTO_Detallado_ventas_vista_previa vista_previa = new DTO_Detallado_ventas_vista_previa();
									vista_previa.amecop = existencia_prepago.amecop_completo;
									vista_previa.articulo_id = existencia_prepago.articulo_id;
                                    vista_previa.caducidad = (existencia_vendible.caducidad.Equals("0000-00-00")) ? "SIN CAD" : Misc_helper.fecha(existencia_vendible.caducidad, "CADUCIDAD");
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

			DAO_Apartado_mercancia dao_apartado = new DAO_Apartado_mercancia();
			var productos_entregados = dao_apartado.get_productos_prepago_parcial(prepago_id);

			foreach(var item in  productos_entregados)
			{
				detallado_ventas_vista_previa.Add(item);
			}

			Venta_prepago venta_prepago = new Venta_prepago(venta_id,detallado_ventas_vista_previa,monto,prepago_id);
			venta_prepago.ShowDialog();

			if(venta_prepago.prepago_afectado)
			{
				DAO_Prepago dao_prepago = new DAO_Prepago();
				dao_prepago.set_prepago_canje(prepago_id);
				prepago_afectado = venta_prepago.prepago_afectado;
				this.Close();
			}
		}

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (txt_codigo_prepago.Text.Trim().Length > 0)
            {
                generar_venta_prepago();
            }
            else
            {
                MessageBox.Show(this,"Es necesario el codigo del prepago","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txt_codigo_prepago.Focus();
            }
        }
	}
}
