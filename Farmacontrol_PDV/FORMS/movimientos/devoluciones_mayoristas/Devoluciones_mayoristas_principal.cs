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
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas;

namespace Farmacontrol_PDV.FORMS.movimientos.devoluciones_mayoristas
{
	public partial class Devoluciones_mayoristas_principal : Form
	{
		DAO_Devoluciones dao_devoluciones = new DAO_Devoluciones();
		DAO_Mayoristas dao_mayoristas = new DAO_Mayoristas();
		DAO_Entradas dao_entradas = new DAO_Entradas();
		
		DTO_Devoluciones dto_devoluciones = new DTO_Devoluciones();

		private long? articulo_id = null;

		public Devoluciones_mayoristas_principal()
		{
			InitializeComponent();

            foreach (DataGridViewColumn column in dgv_entradas.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
		}

		private void Devoluciones_mayoristas_principal_Load(object sender, EventArgs e)
		{
			
		}

		private void Devoluciones_mayoristas_principal_Shown(object sender, EventArgs e)
		{
			lbl_mensaje_bloqueo.Parent = dgv_entradas;
			rellenar_informacion_devolucion(dao_devoluciones.get_devolucion_fin());
		}

		private  void detallado_devoluciones()
		{
			var detallado = dao_devoluciones.get_detallado_devoluciones(dto_devoluciones.devolucion_id, dto_devoluciones.entrada_id);

			DataColumn column =  new DataColumn("cant_dev",typeof(string));
			detallado.Columns.Add(column);
			column = new DataColumn("motivo", typeof(string));
			detallado.Columns.Add(column);

			dgv_entradas.DataSource = detallado;

			if(dgv_entradas.Rows.Count > 0)
			{
				var motivo_enum = Misc_helper.get_enums("farmacontrol_local.detallado_devoluciones", "motivo");

				foreach (DataGridViewRow row in dgv_entradas.Rows)
				{
					if(dto_devoluciones.fecha_terminado.Equals(null))
					{
						int cantidad = Convert.ToInt32(row.Cells["c_cantidad_entradas"].Value) - Convert.ToInt32(row.Cells["c_cantidad_terminadas"].Value);
						int existencia_vendible = Convert.ToInt32(row.Cells["c_cantidad_vendible"].Value) + Convert.ToInt32(row.Cells["c_cantidad_actual"].Value);
						int cantidad_actual = Convert.ToInt32(row.Cells["c_cantidad_actual"].Value);
						
						/*Log_error.log("cantidad_terminadas: " + Convert.ToInt32(row.Cells["c_cantidad_terminadas"].Value));
						Log_error.log("cantidad_entradas: " + Convert.ToInt32(row.Cells["c_cantidad_entradas"].Value));
						Log_error.log("Cantidad: "+cantidad);
						Log_error.log("Existencia_vendible: " + existencia_vendible);
						Log_error.log("Cantidad_actual: " + cantidad_actual);*/

						DataGridViewComboBoxCell cant_dev = new DataGridViewComboBoxCell();
						DataGridViewComboBoxCell motivo = new DataGridViewComboBoxCell();
						motivo.Items.Add("");

						foreach (var content in motivo_enum)
						{
							if(!content.Equals("CADUCIDAD"))
							{
								motivo.Items.Add(content);
							}
						}

						if(existencia_vendible > 0)
						{
							for (int x = 0; x <= ((cantidad > existencia_vendible) ? existencia_vendible : cantidad); x++)
							{
								cant_dev.Items.Add("" + x);
							}	
						}
						else
						{
							cant_dev.Items.Add(""+0);
						}

						dgv_entradas.Rows[row.Index].Cells["c_cant_dev"] = cant_dev;
						dgv_entradas.Rows[row.Index].Cells["c_cant_dev"].Value = (existencia_vendible > 0) ? cantidad_actual : 0;
						dgv_entradas.Rows[row.Index].Cells["c_motivo"] = motivo;
						dgv_entradas.Rows[row.Index].Cells["c_motivo"].Value = row.Cells["c_motivo_actual"].Value.ToString();
					}
					else
					{
						DataGridViewComboBoxCell cant_dev = new DataGridViewComboBoxCell();
						DataGridViewComboBoxCell motivo = new DataGridViewComboBoxCell();
						motivo.Items.Add("");

						foreach (var content in motivo_enum)
						{
							motivo.Items.Add(content);
						}

						for (int x = 0; x <= Convert.ToInt32(row.Cells["c_cantidad_actual"].Value) ; x++)
						{
							cant_dev.Items.Add("" + x);
						}
						
						dgv_entradas.Rows[row.Index].Cells["c_cant_dev"] = cant_dev;
						dgv_entradas.Rows[row.Index].Cells["c_cant_dev"].Value = Convert.ToInt32(row.Cells["c_cantidad_actual"].Value);
						dgv_entradas.Rows[row.Index].Cells["c_motivo"] = motivo;
						dgv_entradas.Rows[row.Index].Cells["c_motivo"].Value = row.Cells["c_motivo_actual"].Value.ToString();
					}
				}
			}

			dgv_entradas.ClearSelection();
		}

		private void detallado_devolucion_sin_entrada()
		{
			var detallado = dao_devoluciones.get_detallado_devoluciones(dto_devoluciones.devolucion_id);

			DataColumn column = new DataColumn("cant_dev", typeof(string));
			detallado.Columns.Add(column);
			column = new DataColumn("motivo", typeof(string));
			detallado.Columns.Add(column);

			dgv_entradas.DataSource = detallado;

			var motivo_enum = Misc_helper.get_enums("farmacontrol_local.detallado_devoluciones", "motivo");

			foreach (DataGridViewRow row in dgv_entradas.Rows)
			{
				int cantidad_actual = Convert.ToInt32(row.Cells["c_cantidad_actual"].Value);

				DataGridViewComboBoxCell cant_dev = new DataGridViewComboBoxCell();
				DataGridViewComboBoxCell motivo = new DataGridViewComboBoxCell();

				motivo.Items.Add("CADUCIDAD");

				cant_dev.Items.Add("" + cantidad_actual);

				dgv_entradas.Rows[row.Index].Cells["c_cant_dev"] = cant_dev;
				dgv_entradas.Rows[row.Index].Cells["c_cant_dev"].Value = cantidad_actual;
				dgv_entradas.Rows[row.Index].Cells["c_motivo"] = motivo;
				dgv_entradas.Rows[row.Index].Cells["c_motivo"].Value = row.Cells["c_motivo_actual"].Value.ToString();
			}

			dgv_entradas.ClearSelection();
		}

		public void rellenar_informacion_devolucion(long devolucion_id = 0)
		{
			if(devolucion_id > 0)
			{
				lbl_mensaje_bloqueo.Parent = null;
				lbl_mensaje_bloqueo.Visible = false;
				dto_devoluciones = dao_devoluciones.get_informacion_devoluciones(devolucion_id);

				cbb_mayorista.DataSource = dao_mayoristas.get_all_mayoristas(true);
				cbb_mayorista.DisplayMember = "Value";
				cbb_mayorista.ValueMember = "Key";
				cbb_mayorista.SelectedValue = Convert.ToInt32(dto_devoluciones.mayorista_id);

				txt_comentarios.Text = dto_devoluciones.comentarios;

				txt_estado.Text = (dto_devoluciones.fecha_terminado.Equals(null)) ? "ABIERTO" : "CERRADO" ;
				txt_estado.BackColor = (dto_devoluciones.fecha_terminado.Equals(null)) ? Color.Green : Color.Red ;

				dtp_fecha_devolucion.Value = (dto_devoluciones.solicitud_devolucion_fecha.Equals(null)) ? Convert.ToDateTime(Misc_helper.fecha()) : Convert.ToDateTime(dto_devoluciones.solicitud_devolucion_fecha);
				dtp_fecha_devolucion.Checked = (dto_devoluciones.solicitud_devolucion_fecha.Equals(null)) ? false: true;

				if(dto_devoluciones.entrada_id > 0)
				{
					txt_amecop.Enabled = false;
					btn_asignar_desasociar_factura.Text = "Desasociar Recepción";
					var entradas = dao_entradas.get_informacion_entrada(dto_devoluciones.entrada_id);

					txt_nombre_empleado_captura_entrada.Text = entradas.nombre_empleado_captura;
					txt_nombre_empleado_termina_entrada.Text = entradas.nombre_empleado_termina;
                    txt_fecha_creado_entrada.Text = (entradas.fecha_creado != null) ? Misc_helper.fecha(entradas.fecha_creado.ToString(), "LEGIBLE") : " - ";
                    txt_fecha_terminado_entrada.Text = (entradas.fecha_terminado != null) ? Misc_helper.fecha(entradas.fecha_terminado.ToString(), "LEGIBLE") : " - ";
					txt_comentarios_entrada.Text = entradas.comentarios;

					cbb_tipo_entrada.DataSource =  new BindingSource(new Dictionary<string,string>(){
						{entradas.tipo_entrada,entradas.tipo_entrada}
					},null);

					cbb_tipo_entrada.DisplayMember = "Key";
					cbb_tipo_entrada.ValueMember = "Value";
					
					string[] split_factura = entradas.factura.Split('_');

					if(split_factura[0].Equals("SF"))
					{
						if(split_factura[1].Length == 32)
						{
							txt_factura_entrada.Text = "SIN FACTURA";
						}
						else
						{
							txt_factura_entrada.Text = entradas.factura;
						}
					}
					else
					{
						txt_factura_entrada.Text = entradas.factura;
					}
					
					detallado_devoluciones();
				}
				else
				{
					txt_amecop.Enabled = true;
					btn_asignar_desasociar_factura.Text = "Asociar Recepción";

					txt_nombre_empleado_captura_entrada.Text = "";
					txt_nombre_empleado_termina_entrada.Text = "";
					txt_fecha_creado_entrada.Text = "";
					txt_fecha_terminado_entrada.Text = "";
					txt_comentarios_entrada.Text = "";
					cbb_tipo_entrada.DataSource = null;
					txt_factura_entrada.Text = "";
					txt_comentarios.Text = dto_devoluciones.comentarios;

					DataTable DT = (DataTable)dgv_entradas.DataSource;
					if (DT != null)
					{
						DT.Clear();
					}

					detallado_devolucion_sin_entrada();
				}
				
				txt_empleado_captura.Text = dto_devoluciones.nombre_empleado_captura;
				txt_empleado_termina.Text = dto_devoluciones.nombre_empleado_termina;
                txt_fecha_creado.Text = (dto_devoluciones.fecha_creado != null) ? Misc_helper.fecha(dto_devoluciones.fecha_creado.ToString(), "LEGIBLE") : " - ";
                txt_fecha_terminado.Text = (dto_devoluciones.fecha_terminado != null) ? Misc_helper.fecha(dto_devoluciones.fecha_terminado.ToString(), "LEGIBLE") : " - ";
				txt_folio_busqueda.Value = dto_devoluciones.devolucion_id;
				txt_folio_devolucion.Text = dto_devoluciones.solicitud_devolucion_folio;

				validar_bloqueo();
			}
			else
			{
				dgv_entradas.Enabled = true;
				lbl_mensaje_bloqueo.Text = "No se encontro ninguna devolucion";
				lbl_mensaje_bloqueo.Parent = dgv_entradas;
				lbl_mensaje_bloqueo.Visible = true;

				validar_bloqueo();
			}
		}

		public void validar_bloqueo()
		{
			if(dto_devoluciones.fecha_terminado.Equals(null))
			{
				if (dto_devoluciones.terminal_id.Equals(0))
				{
					lbl_mensaje_bloqueo.Text = "Devolucion sin terminal";
					lbl_mensaje_bloqueo.Parent = dgv_entradas;
					lbl_mensaje_bloqueo.Visible = true;

					txt_folio_devolucion.Focus();
					txt_amecop.Enabled = false;
					txt_comentarios.Enabled = false;
					btn_asignar_desasociar_factura.Enabled = false;
					cbb_mayorista.Enabled = false;
					txt_folio_devolucion.Enabled = false;
					dtp_fecha_devolucion.Enabled = false;
					dgv_entradas.ReadOnly = true;
				}
				else
				{
					if(dto_devoluciones.terminal_id.Equals(Misc_helper.get_terminal_id()))
					{
						lbl_mensaje_bloqueo.Text = "";
						lbl_mensaje_bloqueo.Parent = dgv_entradas;
						lbl_mensaje_bloqueo.Visible = false;

						txt_amecop.Enabled = (dto_devoluciones.entrada_id > 0) ? false : true;
						dgv_entradas.ReadOnly = false;
						cbb_mayorista.Enabled = true;
						txt_folio_devolucion.Enabled = true;
						dtp_fecha_devolucion.Enabled = true;
						btn_asignar_desasociar_factura.Enabled = true;
						txt_comentarios.Enabled = true;

						txt_amecop.Focus();
					}
					else
					{
                        if(dto_devoluciones.devolucion_id > 0)
                        {
                            lbl_mensaje_bloqueo.Text = "Esta devolucion pertenece a " + Misc_helper.get_nombre_terminal((int)dto_devoluciones.terminal_id);
                            lbl_mensaje_bloqueo.Parent = dgv_entradas;
                            lbl_mensaje_bloqueo.Visible = true;
                        }

						txt_folio_devolucion.Focus();
						txt_amecop.Enabled = false;
						txt_comentarios.Enabled = false;
						btn_asignar_desasociar_factura.Enabled = false;
						cbb_mayorista.Enabled = false;
						txt_folio_devolucion.Enabled = false;
						dtp_fecha_devolucion.Enabled = false;
						dgv_entradas.ReadOnly = true;
					}
				}
			}
			else
			{
				if (dto_devoluciones.terminal_id.Equals(0))
				{
					lbl_mensaje_bloqueo.Text = "Devolucion sin terminal";
					lbl_mensaje_bloqueo.Parent = dgv_entradas;
					lbl_mensaje_bloqueo.Visible = true;
				}

				txt_folio_devolucion.Focus();
				txt_amecop.Enabled = false;
				txt_comentarios.Enabled = false;
				btn_asignar_desasociar_factura.Enabled = false;
				cbb_mayorista.Enabled = false;
				txt_folio_devolucion.Enabled = false;
				dtp_fecha_devolucion.Enabled = false;
				dgv_entradas.ReadOnly = true;
			}
		}

		private void btn_asignar_desasociar_factura_Click(object sender, EventArgs e)
		{
			if(btn_asignar_desasociar_factura.Text.Equals("Asociar Recepción"))
			{
				Entradas_mayorista entradas_mayoristas = new Entradas_mayorista(dto_devoluciones.mayorista_id);
				entradas_mayoristas.ShowDialog();

				if(entradas_mayoristas.entrada_id > 0)
				{
					DialogResult dr = MessageBox.Show(this,string.Format("La recepción con el folio #{0} sera asignada a esta devolucion, ¿Desea continuar?",entradas_mayoristas.entrada_id),"Asignar Recepción",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
					
					if(dr == DialogResult.Yes)
					{
						if (dao_devoluciones.set_entrada_id(dto_devoluciones.devolucion_id, Convert.ToInt64(entradas_mayoristas.entrada_id)) > 0)
						{
							try
							{
								rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
							}
							catch(Exception ex)
							{
								Log_error.log(ex);
							}
						}
						else
						{
							MessageBox.Show(this, "Ocurrio un problema al intentar asociar la recepción, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			else
			{
				DialogResult dr = MessageBox.Show(this,"Si desasocias esta recepción, todos los productos seran eliminados, ¿Deseas Continuar?","Desasociar Recepción",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if(dr == DialogResult.Yes)
				{
					dao_devoluciones.set_entrada_id(dto_devoluciones.devolucion_id,0);
					rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
				}
			}
		}

		private void cbb_mayorista_DropDownClosed(object sender, EventArgs e)
		{
			if(Convert.ToInt64(cbb_mayorista.SelectedValue) != Convert.ToInt64(dto_devoluciones.mayorista_id))
			{
				dao_devoluciones.set_mayorista_id(dto_devoluciones.devolucion_id,Convert.ToInt64(cbb_mayorista.SelectedValue));
				rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);	
			}
		}

		private void btn_nuevo_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form("crear_devolucion");
			login.ShowDialog();

			if(login.empleado_id != null)
			{
				Mayoristas mayoristas = new Mayoristas();
				mayoristas.ShowDialog();

				if(mayoristas.mayorista_id != null)
				{
					DialogResult dr = MessageBox.Show(this, "Estas a punto de crear una devolución,¿Deseas continuar?", "Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

					if (dr == DialogResult.Yes)
					{
						long insert_id = dao_devoluciones.registrar_devolucion((long)login.empleado_id, (long)mayoristas.mayorista_id);

						if (insert_id > 0)
						{
							MessageBox.Show(this, "Devolución a Mayorista registrada correctamente con el folio #" + insert_id, "Devolución Mayorista", MessageBoxButtons.OK, MessageBoxIcon.Information);
							rellenar_informacion_devolucion(insert_id);
						}
						else
						{
							MessageBox.Show(this, "Ocurrio un error al intentar registrar la devolución al mayorista, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}	
				}
			}
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			long devolucion_id = dao_devoluciones.get_devolucion_inicio();

			if(devolucion_id > 0)
			{
				rellenar_informacion_devolucion(devolucion_id);
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			long devolucion_id = dao_devoluciones.get_devolucion_atras(dto_devoluciones.devolucion_id);

			if (devolucion_id > 0)
			{
				rellenar_informacion_devolucion(devolucion_id);
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			long devolucion_id = dao_devoluciones.get_devolucion_siguiente(dto_devoluciones.devolucion_id);

			if (devolucion_id > 0)
			{
				rellenar_informacion_devolucion(devolucion_id);
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			long devolucion_id = dao_devoluciones.get_devolucion_fin();

			if (devolucion_id > 0)
			{
				rellenar_informacion_devolucion(devolucion_id);
			}
		}

		public bool existen_productos_devolver()
		{
			foreach(DataGridViewRow row in dgv_entradas.Rows)
			{
				if(Convert.ToInt32(row.Cells["c_cant_dev"].Value) > 0)
				{
					return true;
				}
			}
			
			if(dgv_entradas.Rows.Count == 0)
			{
				return true;
			}

			return false;
		}
		
		public bool validar_productos_devolver()
		{
			foreach (DataGridViewRow row in dgv_entradas.Rows)
			{
				if (Convert.ToInt32(row.Cells["c_cant_dev"].Value) > 0 && row.Cells["c_motivo"].Value.ToString().Equals(""))
				{
					MessageBox.Show(this,string.Format("Existen productos a devolver SIN MOTIVO, verifique!"),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return false;
				}

				if (Convert.ToInt32(row.Cells["c_cant_dev"].Value) == 0 && row.Cells["c_motivo"].Value.ToString().Length > 0)
				{
					MessageBox.Show(this, string.Format("Existen productos a devolver CON MOTIVO y SIN CANTIDAD, verifique!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			return true;
		}

		private void btn_cerrar_terminar_Click(object sender, EventArgs e)
		{
			if(dto_devoluciones.devolucion_id > 0 && dto_devoluciones.terminal_id == Misc_helper.get_terminal_id())
			{
				if (dto_devoluciones.fecha_terminado.Equals(null))
				{
					if (existen_productos_devolver())
					{
						if (validar_productos_devolver())
						{
							if (dto_devoluciones.entrada_id > 0)
							{
								if (txt_folio_devolucion.Text.Trim().Length > 0)
								{
									if (dtp_fecha_devolucion.Checked)
									{
										string fecha = dtp_fecha_devolucion.Value.ToString("yyyy-MM-dd");

										Login_form login = new Login_form("terminar_devolucion");
										login.ShowDialog();

										if (login.empleado_id != null)
										{
											DialogResult dr = MessageBox.Show(this, "Estas a punto de terminar la devolución y no podras hacerle ningun cambio, ¿Deseas Continuar?", "Terminar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

											if (dr == DialogResult.Yes)
											{
												var source_clone = (DataTable)dgv_entradas.DataSource;
												DataTable data_entradas = source_clone.Copy();

												foreach (DataRow row in data_entradas.Rows)
												{
													if (Convert.ToInt32(row["cant_dev"]) == 0)
													{
														row.Delete();
													}
												}

												Productos_devolucion productos_devolucion = new Productos_devolucion(dto_devoluciones.devolucion_id, dto_devoluciones.entrada_id, data_entradas, (long)login.empleado_id, fecha, txt_folio_devolucion.Text);
												productos_devolucion.ShowDialog();

                                                if (productos_devolucion.terminado)
                                                {
                                                    rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
                                                }
                                                else
                                                {
                                                    //ESTO ES NUEVO 
                                                    MessageBox.Show(this, "No se termino la devolucion, notifica al administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }

											}
										}
									}
									else
									{
										MessageBox.Show(this, "Es necesario asignar una fecha, verifique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
								}
								else
								{
									MessageBox.Show(this, "Es necesario el folio de la devolución", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									txt_folio_devolucion.Focus();
								}
							}
							else
							{
								if (dgv_entradas.Rows.Count > 0)
								{
									if (txt_folio_devolucion.Text.Trim().Length > 0)
									{
										if (dtp_fecha_devolucion.Checked)
										{
											string fecha = dtp_fecha_devolucion.Value.ToString("yyyy-MM-dd");

											Login_form login = new Login_form("terminar_devolucion");
											login.ShowDialog();

											if (login.empleado_id != null)
											{
												DialogResult dr = MessageBox.Show(this, "Estas a punto de terminar la devolución y no podras hacerle ningun cambio, ¿Deseas Continuar?", "Terminar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

												if (dr == DialogResult.Yes)
												{
													int filas_afectadas = dao_devoluciones.terminar_devolucion((long)dto_devoluciones.devolucion_id, (long)login.empleado_id, fecha, txt_folio_devolucion.Text.Trim());

													if (filas_afectadas > 0)
													{
														MessageBox.Show(this, "Devolución terminada correctamente", "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);
														rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
														Devoluciones_mayorista ticket = new Devoluciones_mayorista();
														ticket.construccion_ticket(dto_devoluciones.devolucion_id);
														ticket.print();
													}
													else
													{
														MessageBox.Show(this, "Ocurrio un error al intentar terminar vacia la devolucion, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
														rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
													}
												}
											}
										}
										else
										{
											MessageBox.Show(this, "Es necesario asignar una fecha, verifique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
										}
									}
									else
									{
										MessageBox.Show(this, "Es necesario el folio de la devolución", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
										txt_folio_devolucion.Focus();
									}
								}
								else
								{
									Login_form login = new Login_form("terminar_devolucion");
									login.ShowDialog();

									if (login.empleado_id != null)
									{
										DialogResult dr = MessageBox.Show(this, "Esta devolución se terminara vacia, y no podras hacerle ningun cambio, ¿Deseas Continuar?", "Terminar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

										if (dr == DialogResult.Yes)
										{
											int filas_afectadas = dao_devoluciones.terminar_devolucion((long)dto_devoluciones.devolucion_id, (long)login.empleado_id, "", "");

											if (filas_afectadas > 0)
											{
												MessageBox.Show(this, "Devolucion terminada correctamente", "Devolución", MessageBoxButtons.OK, MessageBoxIcon.Information);
												rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
												Devoluciones_mayorista ticket = new Devoluciones_mayorista();
												ticket.construccion_ticket(dto_devoluciones.devolucion_id);
												ticket.print();
											}
											else
											{
												MessageBox.Show(this, "Ocurrio un error al intentar terminar vacia la devolución, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
												rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
											}
										}
									}
								}
							}
						}
					}
					else
					{
						MessageBox.Show(this, "Si desea terminar la devolucion SIN AFECTAR NINGUN PRODUCTO, primero desasocie la recepción e intentelo de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}	

		private void dgv_entradas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			ComboBox combo = e.Control as ComboBox;

			//combo.SelectionChangeCommitted -= new EventHandler(cmbox_DropDownClosed_guardar_cantidad);
			//combo.SelectionChangeCommitted -= new EventHandler(cmbox_DropDownClosed_guardar_motivo);
           
                
			 if (dgv_entradas.CurrentCell.ColumnIndex == 21)
			 {
				combo.SelectionChangeCommitted -= new EventHandler(cmbox_DropDownClosed_guardar_cantidad);
				combo.SelectionChangeCommitted += new EventHandler(cmbox_DropDownClosed_guardar_cantidad);
			 }
			 else if (dgv_entradas.CurrentCell.ColumnIndex == 22)
			 {
				 combo.SelectionChangeCommitted -= new EventHandler(cmbox_DropDownClosed_guardar_motivo);
				 combo.SelectionChangeCommitted += new EventHandler(cmbox_DropDownClosed_guardar_motivo);
			 }
             else
             {
                 dgv_entradas.CurrentCell.ReadOnly = true;
             }
         
		}

		private void cmbox_DropDownClosed_guardar_cantidad(object sender, EventArgs e)
		{
            try
            {
                long detallado_devolucion_id = Convert.ToInt64(dgv_entradas.Rows[dgv_entradas.CurrentRow.Index].Cells["detallado_devolucion_id"].Value);
                var currentcell = dgv_entradas.CurrentCellAddress;
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                int cantidad = Convert.ToInt32(sendingCB.EditingControlFormattedValue.ToString());

                dao_devoluciones.update_cantidad_detallado(detallado_devolucion_id, cantidad);

            }
            catch
            {

            }


			

			//rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
			//detallado_devoluciones();
		}

		private void cmbox_DropDownClosed_guardar_motivo(object sender, EventArgs e)
		{
			long detallado_devolucion_id = Convert.ToInt64(dgv_entradas.Rows[dgv_entradas.CurrentRow.Index].Cells["detallado_devolucion_id"].Value);
			var currentcell = dgv_entradas.CurrentCellAddress;
			var sendingCB = sender as DataGridViewComboBoxEditingControl;
			string motivo = sendingCB.EditingControlFormattedValue.ToString();

			dao_devoluciones.update_motivo_detallado(detallado_devolucion_id, motivo);
			//rellenar_informacion_devolucion(dto_devoluciones.devolucion_id);
			//detallado_devoluciones();
		}

		private void txt_comentarios_Leave(object sender, EventArgs e)
		{
			dao_devoluciones.set_comentario(dto_devoluciones.devolucion_id, txt_comentarios.Text);
		}

		private void btn_imprimir_Click(object sender, EventArgs e)
		{
			if(!dto_devoluciones.fecha_terminado.Equals(null))
			{
				Devoluciones_mayorista ticket = new Devoluciones_mayorista();
				ticket.construccion_ticket(dto_devoluciones.devolucion_id, true);
				ticket.print();	
			}
		}

		private void btn_escanear_Click(object sender, EventArgs e)
		{
			Control_escaneo control_escaneo = new Control_escaneo("scans_devoluciones");
			control_escaneo.ShowDialog();

			if (control_escaneo.status)
			{
				dao_devoluciones.registrar_nombre_archivo(dto_devoluciones.devolucion_id, control_escaneo.nombre_uuid);
			}
		}

		public void limpiar_informacion()
		{
			articulo_id = null;
			txt_amecop.Enabled = true;
			txt_cantidad.Enabled = false;
			txt_cantidad.Value = 1;

			txt_amecop.Text = "";
			txt_amecop.ReadOnly = false;

			cbb_caducidad.DataSource = null;
			cbb_lote.DataSource = null;

			txt_producto.Text = "";

			cbb_caducidad.Enabled = false;
			cbb_lote.Enabled = false;

			txt_amecop.Focus();
		}

		public void busqueda_producto()
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();
			DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

			if (articulo.Articulo_id != null)
			{
				articulo_id = articulo.Articulo_id;
				rellenar_informacion_producto(articulo);
			}
			else
			{
				txt_amecop.Text = "";
				MessageBox.Show(this, "Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		public void rellenar_informacion_producto(DTO_Articulo articulo)
		{
			txt_amecop.ReadOnly = true;
			txt_producto.Text = articulo.Nombre;
			cbb_caducidad.Enabled = true;

			if (articulo.Caducidades.Rows.Count > 0)
			{
				Dictionary<string, string> caducidades = new Dictionary<string, string>();

				foreach (DataRow row in articulo.Caducidades.Rows)
				{
                    caducidades.Add(Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")", row["caducidad"].ToString());
				}

				cbb_caducidad.DataSource = new BindingSource(caducidades, null);

				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";

				cbb_caducidad.DroppedDown = true;
				cbb_caducidad.Focus();
			}
			else
			{
				MessageBox.Show(this, "Producto sin existencias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				limpiar_informacion();
			}
		}

		public void busqueda_productos_Close(object sender, FormClosedEventArgs e)
		{
			if (Busqueda_productos.articulo_articulo_id != null)
			{
				articulo_id = Busqueda_productos.articulo_articulo_id;
				txt_amecop.Text = Busqueda_productos.articulo_amecop;
				txt_producto.Text = Busqueda_productos.articulo_producto;

				cbb_caducidad.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.caducidad_item.Text,Busqueda_productos.caducidad_item.Value.ToString()}
				}, null);

				cbb_caducidad.DisplayMember = "Key";
				cbb_caducidad.ValueMember = "Value";

				cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>(){
					{Busqueda_productos.lote_item.Text,Busqueda_productos.lote_item.Value.ToString()}
				}, null);

				cbb_lote.DisplayMember = "Key";
				cbb_lote.ValueMember = "Value";

				cbb_caducidad.Enabled = false;
				cbb_lote.Enabled = false;

				txt_amecop.Enabled = false;

				txt_cantidad.Enabled = true;
				txt_cantidad.Value = 1;
				txt_cantidad.Select(0,txt_cantidad.Text.Length);
				txt_cantidad.Focus();
			}
		}

		private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 27:
					limpiar_informacion();
					break;
				case 40:
					if (dgv_entradas.Rows.Count > 0)
					{
						dgv_entradas.CurrentCell = dgv_entradas.Rows[0].Cells["c_amecop"];
						dgv_entradas.Rows[0].Selected = true;
						dgv_entradas.Focus();
					}
					break;
				case 13:
					if (txt_amecop.TextLength > 0)
						busqueda_producto();
					break;
				case 114:
					Busqueda_productos busqueda_productos = new Busqueda_productos(true);
					busqueda_productos.FormClosed += new FormClosedEventHandler(busqueda_productos_Close);
					busqueda_productos.ShowDialog();
					txt_cantidad.Focus();
					break;
			}
		}

		private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 13)
			{
				busqueda_lotes();
				cbb_caducidad.Enabled = false;
			}
			else if (Convert.ToInt32(e.KeyCode) == 27)
			{
				txt_producto.Text = "";
				txt_amecop.ReadOnly = false;
				txt_amecop.Focus();
				cbb_caducidad.DataSource = null;
				cbb_caducidad.Enabled = false;
			}
		}

		private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					txt_cantidad.Enabled = true;
					txt_cantidad.Value = 1;
					txt_cantidad.Select(0,txt_cantidad.Text.Length);
					txt_cantidad.Focus();
					cbb_lote.Enabled = false;
					break;
				case 27:
                    cbb_lote.DataSource = new BindingSource(new Dictionary<string, string>() { {"",""}}, null);
                    cbb_lote.DisplayMember = "Key";
                    cbb_lote.ValueMember = "Value";

					cbb_caducidad.Enabled = true;
					cbb_caducidad.Focus();
					cbb_caducidad.DroppedDown = true;
					cbb_lote.Enabled = false;
					break;
			}
		}

		public void busqueda_lotes()
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();
			DataTable result_lotes = dao_articulos.get_lotes((int)articulo_id, cbb_caducidad.SelectedValue.ToString());
			cbb_lote.Enabled = true;

			if (result_lotes.Rows.Count > 0)
			{
				Dictionary<string, string> lotes = new Dictionary<string, string>();

				foreach (DataRow row in result_lotes.Rows)
				{
					lotes.Add(row["lote"].ToString(), row["lote"].ToString());
				}

				cbb_lote.DataSource = new BindingSource(lotes, null);
				cbb_lote.DisplayMember = "Key";
				cbb_lote.ValueMember = "Value";

				cbb_lote.DroppedDown = true;
				cbb_lote.Focus();
			}
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			if (Convert.ToInt32(e.KeyCode) == 27)
			{
				limpiar_informacion();
			}

			if (Convert.ToInt32(e.KeyCode) == 13 && txt_cantidad.Value > 0)
			{
				insertar_producto_devolucion();
			}
		}

		private void insertar_producto_devolucion()
		{
			try
			{
				DAO_Articulos dao_articulos = new DAO_Articulos();
				int existencia_vendible = dao_articulos.get_existencia_vendible(txt_amecop.Text, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString());

				if (existencia_vendible >= Convert.ToInt32(txt_cantidad.Value))
				{
					dao_devoluciones.registrar_detallado_devolucion(dto_devoluciones.devolucion_id, (long)articulo_id, cbb_caducidad.SelectedValue.ToString(), cbb_lote.SelectedValue.ToString(), Convert.ToInt32(txt_cantidad.Value));
					limpiar_informacion();
					detallado_devolucion_sin_entrada();
				}
				else
				{
					MessageBox.Show(this, "La cantidad disponible para vender es de " + existencia_vendible, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}
		}

		private void txt_amecop_Enter(object sender, EventArgs e)
		{
			dgv_entradas.ClearSelection();
		}

		private void dgv_entradas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_entradas.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                break;
				case 46:
					if(dto_devoluciones.entrada_id == 0 && dto_devoluciones.fecha_terminado == null && dto_devoluciones.terminal_id == Misc_helper.get_terminal_id())
					{
						if(dgv_entradas.SelectedRows.Count > 0)
						{
							long detallado_id = Convert.ToInt64(dgv_entradas.SelectedRows[0].Cells["detallado_devolucion_id"].Value);
							dao_devoluciones.eliminar_detallado_devolucion(detallado_id);
							detallado_devolucion_sin_entrada();
							txt_amecop.Focus();	
						}
					}
				break;
			}
		}

		private void desasociar_terminal_Click(object sender, EventArgs e)
		{
			if(dto_devoluciones.devolucion_id > 0 && dto_devoluciones.terminal_id == Misc_helper.get_terminal_id())
			{
				if (dto_devoluciones.devolucion_id> 0)
				{
					if (dto_devoluciones.fecha_terminado.Equals(null))
					{
						if (dto_devoluciones.terminal_id > 0)
						{
							Login_form login = new Login_form();
							login.ShowDialog();

							if (login.empleado_id != null)
							{
								if ((int)login.empleado_id == dto_devoluciones.empleado_id)
								{
									if (dao_devoluciones.desasociar_terminal(dto_devoluciones.devolucion_id))
									{
										dto_devoluciones = dao_devoluciones.get_informacion_devoluciones(dto_devoluciones.devolucion_id);

										rellenar_informacion_devolucion();

										MessageBox.Show(this, "Terminal desasociada correctamente", "Desasociar Terminal", MessageBoxButtons.OK, MessageBoxIcon.Information);
									}
									else
									{
										MessageBox.Show(this, "Ocurrio un error al intentar modificar la terminal, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
								}
								else
								{
									MessageBox.Show(this, "Solo quien creo la entrada puede desasociarla de su terminal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
						}
						else
						{
							MessageBox.Show(this, "Esta entrada no tiene terminal asociada, imposible desasociar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}	
			}
		}

		private void asociar_terminal_Click(object sender, EventArgs e)
		{
			if (dto_devoluciones.devolucion_id > 0)
			{
				if (dto_devoluciones.fecha_terminado == null)
				{
					if (dto_devoluciones.terminal_id == 0)
					{
						Login_form login = new Login_form();
						login.ShowDialog();

						if (login.empleado_id != null)
						{
							if (dao_devoluciones.asociar_terminal((long)dto_devoluciones.devolucion_id, (long)login.empleado_id))
							{
								MessageBox.Show(this, "Terminal asociada correctamente", "Asociar Terminal", MessageBoxButtons.OK, MessageBoxIcon.Information);

								dto_devoluciones = dao_devoluciones.get_informacion_devoluciones(dto_devoluciones.devolucion_id);
								rellenar_informacion_devolucion();
							}
							else
							{
								MessageBox.Show(this, "Ocurrio un error al intentar asociar la terminal, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
					else
					{
						MessageBox.Show(this, "Esta sucursal ya cuenta con una terminal asignada!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void txt_folio_busqueda_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_folio_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_folio_busqueda.Value > 0)
					{
						long tmp_devolucion_id = Convert.ToInt64(txt_folio_busqueda.Value);
						var dev_temp = dao_devoluciones.get_informacion_devoluciones(tmp_devolucion_id);

						if(dev_temp.devolucion_id > 0)
						{
							rellenar_informacion_devolucion(dev_temp.devolucion_id);
						}

					}
				break;
			}
		}

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dto_devoluciones.devolucion_id > 0)
            {
                rellenar_informacion_devolucion();
            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgv_entradas_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            
        }

        private void txt_folio_devolucion_Leave(object sender, EventArgs e)
        {
            dao_devoluciones.set_folio_devolucion((long)dto_devoluciones.devolucion_id, txt_folio_devolucion.Text);
        }

        private void dtp_fecha_devolucion_Leave(object sender, EventArgs e)
        {
            if(dtp_fecha_devolucion.Checked)
            {
                string fecha = dtp_fecha_devolucion.Value.ToString("yyyy-MM-dd");
                dao_devoluciones.set_fecha_devolucion(dto_devoluciones.devolucion_id,fecha);
            }
        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
