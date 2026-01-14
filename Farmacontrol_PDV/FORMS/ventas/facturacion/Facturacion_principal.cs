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
using Farmacontrol_PDV.CLASSES;
using System.Text.RegularExpressions;
using Farmacontrol_PDV.HELPERS;
using System.Threading;

namespace Farmacontrol_PDV.FORMS.ventas.facturacion
{
	public partial class Facturacion_principal : Form
	{
		DAO_Ventas dao_ventas = new DAO_Ventas();
		DAO_Rfcs dao_rfc = new DAO_Rfcs();
		DTO_Rfc dto_rfc = new DTO_Rfc();
		string rfc_registro_id = "";

		/*Atributos facturación*/

		FacturaWSP importar_factura = new FacturaWSP();
		DTO_Validacion enviar_factura = new DTO_Validacion();
		//private bool facturacion_terminado = false;
		private bool importacion_terminado = false;
		private bool enviar_terminado = false;

		/*Fin atributos facturación*/

		private long venta_id_facturacion;

		public Facturacion_principal()
		{
			InitializeComponent();
			txt_folio_busqueda.Focus();
			dto_rfc.correos_electronicos = new List<string>();
			dto_rfc.rfc_registro_id = "";
			//dto_rfc.asentamiento_id = null;
		}

		private void btn_p1_siguiente_Click(object sender, EventArgs e)
		{
			if(dgv_ventas.SelectedRows.Count > 0)
			{
				venta_id_facturacion = Convert.ToInt64(dgv_ventas.SelectedRows[0].Cells["venta_id"].Value);
				rfc_registro_id = dgv_ventas.SelectedRows[0].Cells["c_rfc_registro_id"].Value.ToString();

				if (rfc_registro_id.Equals(""))
				{
					DialogResult dr = MessageBox.Show(this, "Esta venta no tiene ningun RFC asociado para poder facturarla, ¿desea asignarle un RFC?", "Sin RFC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if (dr == DialogResult.Yes)
					{
						Busqueda_rfcs busqueda_rfcs = new Busqueda_rfcs();
						busqueda_rfcs.ShowDialog();

						if (busqueda_rfcs.rfc_registro_id != "")
						{
							rfc_registro_id = busqueda_rfcs.rfc_registro_id;						
							abrir_segundo_panel();
						}
					}
				}
				else
				{
					abrir_segundo_panel();
				}
			}
		}

		public void abrir_segundo_panel()
		{
			tbp_editar_informacion.Parent = tbc_principal;
			tbp_busqueda.Parent = null;
			var venta_data = dao_ventas.get_venta_data(venta_id_facturacion);
			dto_rfc = dao_rfc.get_data_rfc(rfc_registro_id);

			lbl_tipo_persona.Visible = true;
			rdb_fisica.Visible = true;
			rdb_moral.Visible = true;
			txt_razon_social.Text = dto_rfc.razon_social;
			txt_calle.Text = dto_rfc.calle;
			txt_numero_exterior.Text = dto_rfc.numero_exterior;
			txt_numero_interior.Text = dto_rfc.numero_interior;
			ltb_correos.DataSource = dto_rfc.correos_electronicos;
			ltb_correos.ClearSelected();
			/*
			var codigo_postal_data = dao_codigos_postales.get_codigo_postal_data((long)dto_rfc.asentamiento_id);

			if (codigo_postal_data.Rows.Count > 0)
			{
				txt_codigo_postal.Text = codigo_postal_data.Rows[0]["codigo_postal"].ToString();
				txt_nombre_cp.Text = codigo_postal_data.Rows[0]["nombre"].ToString();
				txt_municipio_cp.Text = codigo_postal_data.Rows[0]["municipio"].ToString();
				txt_ciudad_cp.Text = codigo_postal_data.Rows[0]["ciudad"].ToString();
				txt_estado_cp.Text = codigo_postal_data.Rows[0]["estado"].ToString();
			}

			 * */
			string rfc = dto_rfc.rfc;
			string rfc_nombre = "";
			string rfc_fecha = "";
			string rfc_homoclave = "";

			try
			{
				if (rfc.Trim().Length == 12)
				{
					rfc_nombre = rfc.Substring(0, 3);
					rfc_fecha = rfc.Substring(3, 6);
					rfc_homoclave = rfc.Substring(9, 3);
					rdb_fisica.Checked = false;
					rdb_moral.Checked = true;
				}
				else
				{
					rfc_nombre = rfc.Substring(0, 4);
					rfc_fecha = rfc.Substring(4, 6);
					rfc_homoclave = rfc.Substring(10, 3);
					rdb_fisica.Checked = true;
					rdb_moral.Checked = false;
				}
			}
			catch (Exception ex)
			{
				Log_error.log(ex);
			}

			txt_rfc_nombre.Text = rfc_nombre;
			txt_rfc_fecha.Text = rfc_fecha;
			txt_rfc_homoclave.Text = rfc_homoclave;
		}

		private void txt_folio_busqueda_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					
					string[] codigo = txt_folio_busqueda.Text.Split('$');

					if(codigo.Length == 2)
					{
						bool todos_numeros = true;

						foreach(string items in codigo)
						{
							int n;
							bool isNumeric = int.TryParse(items, out n);	

							if(isNumeric == false)
							{
								todos_numeros = false;
								break;
							}
						}

						if(todos_numeros)
						{
							if(Convert.ToInt64(codigo[0]).Equals(Convert.ToInt64(Config_helper.get_config_local("sucursal_id"))))
							{
								if (dao_ventas.es_venta_facturada(Convert.ToInt64(codigo[1])) == false)
								{
									dgv_ventas.DataSource = dao_ventas.ventas_por_facturar(Convert.ToInt64(codigo[1]));
									txt_folio_busqueda.Text = "";
									dgv_ventas.ClearSelection();	
								}
								else
								{
									MessageBox.Show(this, "Esta venta ya ha sido facturada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
							else
							{
								MessageBox.Show(this, "Este codigo de venta pertenece a otra sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							MessageBox.Show(this,"Codigo de venta invalido","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
						}
					}
					else
					{
						MessageBox.Show(this,"Codigo de venta invalido","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;
				case 27:
					txt_folio_busqueda.Text = "";
				break;
				case 40:
					dgv_ventas.CurrentCell = dgv_ventas.Rows[0].Cells["venta_id"];
					dgv_ventas.Rows[0].Selected = true;
					dgv_ventas.Focus();
				break;
			}
		}

		private void txt_folio_busqueda_Enter(object sender, EventArgs e)
		{
			dgv_ventas.ClearSelection();
		}

		private void dgv_ventas_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					btn_p1_siguiente.Focus();
				break;
				case 27:
					txt_folio_busqueda.Focus();
				break;
			}
		}

		private void Facturacion_principal_Load(object sender, EventArgs e)
		{
			dgv_ventas.ClearSelection();
			txt_folio_busqueda.Focus();
		}

		private void Facturacion_principal_Shown(object sender, EventArgs e)
		{
			txt_folio_busqueda.Focus();
			tbp_editar_informacion.Parent = null;
		}

		private void btn_p2_atras_Click(object sender, EventArgs e)
		{
			tbp_busqueda.Parent = tbc_principal;
			tbp_editar_informacion.Parent = null;
		}

		private Boolean valida_email(string email)
		{
			string expresion;
			expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, string.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public void facturar()
		{
			Facturacion lib = new Facturacion();

			string correos = "";

			for (int i = 0; i < ltb_correos.Items.Count; i++)
			{
				if (correos.Equals(""))
				{
					correos = ltb_correos.Items[i].ToString();
				}
				else
				{
					correos += "," + ltb_correos.Items[i].ToString();
				}
			}

			DTO_Validacion validacion = lib.importar(Convert.ToInt64(venta_id_facturacion), dto_rfc);

			if (validacion.status)
			{
				dao_ventas.registrar_rfc(venta_id_facturacion,dto_rfc.rfc_registro_id);
				imprimir_factura();
				MessageBox.Show(this, validacion.informacion, "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information);
				DTO_Validacion validacion_correo = lib.enviar_correo(Convert.ToInt64(venta_id_facturacion), correos);
				MessageBox.Show(this, validacion_correo.informacion, "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			}
			else
			{
				MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void imprimir_factura()
		{
			CLASSES.PRINT.Facturacion ticket = new CLASSES.PRINT.Facturacion();
			//ticket.construccion_ticket(venta_id_facturacion,false);
			ticket.print();
		}

		private void btn_atras_editar_informacion_Click(object sender, EventArgs e)
		{
			tbp_busqueda.Parent = tbc_principal;
			tbp_editar_informacion.Parent = null;
		}

		private void btn_buscar_cp_Click(object sender, EventArgs e)
		{
			/*
			Busqueda_codigos_postales busqueda_codigos_postales = new Busqueda_codigos_postales();
			busqueda_codigos_postales.ShowDialog();
			if (busqueda_codigos_postales.asentamiento_id_g != null)
			{
				dto_rfc.asentamiento_id = busqueda_codigos_postales.asentamiento_id_g;
				txt_codigo_postal.Text = busqueda_codigos_postales.codigo_postal_cp;
				txt_nombre_cp.Text = busqueda_codigos_postales.nombre_cp;
				txt_municipio_cp.Text = busqueda_codigos_postales.municipio_cp;
				txt_ciudad_cp.Text = busqueda_codigos_postales.ciudad_cp;
				txt_estado_cp.Text = busqueda_codigos_postales.estado_cp;
			}
			 * */
		}

		private void btn_sin_cp_Click(object sender, EventArgs e)
		{
			/*
			Sin_codigo_postal sin_codigo_postal = new Sin_codigo_postal();
			sin_codigo_postal.ShowDialog();
			if (sin_codigo_postal.asentamiento_id != null)
			{
				dto_rfc.asentamiento_id = sin_codigo_postal.asentamiento_id;
				var codigo_postal_data = dao_codigos_postales.get_codigo_postal_data((long)dto_rfc.asentamiento_id);

				txt_codigo_postal.Text = codigo_postal_data.Rows[0]["codigo_postal"].ToString();
				txt_nombre_cp.Text = codigo_postal_data.Rows[0]["nombre"].ToString();
				txt_municipio_cp.Text = codigo_postal_data.Rows[0]["municipio"].ToString();
				txt_ciudad_cp.Text = codigo_postal_data.Rows[0]["ciudad"].ToString();
				txt_estado_cp.Text = codigo_postal_data.Rows[0]["estado"].ToString();
			}
			 * */
		}

		private void btn_agregar_correo_Click(object sender, EventArgs e)
		{
			if (txt_agregar_correo.Text.Trim().Length > 0)
			{
				if (validar_email(txt_agregar_correo.Text))
				{
					dto_rfc.correos_electronicos.Add(txt_agregar_correo.Text);
					ltb_correos.DataSource = null;
					ltb_correos.DataSource = dto_rfc.correos_electronicos;
					ltb_correos.ClearSelected();
					txt_agregar_correo.Text = "";
					txt_agregar_correo.Focus();
				}
				else
				{
					MessageBox.Show(this, "El correo electronico no tiene un formato valido, verifique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					txt_agregar_correo.Focus();
				}
			}
			else
			{
				MessageBox.Show(this, "Es necesario escribir un correo para poder añadirlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txt_agregar_correo.Focus();
			}
		}

		public Boolean validar_email(String email)
		{
			String expresion;
			expresion = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, String.Empty).Length == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void btn_siguiente_editar_informacion_Click(object sender, EventArgs e)
		{
			tbp_editar_informacion.Parent =null;
			tbp_facturacion.Parent = this;
			progress_bar_facturacion.Value = 0;
			worker_facturacion.RunWorkerAsync();

			int progress = 0;

			while(importacion_terminado == false)
			{
				if(importar_factura.status == false)
				{
					if (progress < 50)
					{
						worker_facturacion.ReportProgress(progress);
						Thread.Sleep(20);
						progress += 5;
					}	
				}
				else
				{
					worker_facturacion.ReportProgress(50);
					break;
				}
			}

			while(enviar_terminado == false)
			{
				if(enviar_factura.status == false)
				{
					if(progress < 100)
					{
						worker_facturacion.ReportProgress(progress);
						Thread.Sleep(20);
						progress += 5;
					}
				}
				else
				{
					worker_facturacion.ReportProgress(100);
					break;
				}
			}
		}

		private void ltb_correos_KeyDown_1(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 46:
					if (ltb_correos.SelectedItems.Count > 0)
					{
						int index = ltb_correos.SelectedIndex;
						dto_rfc.correos_electronicos.RemoveAt(index);
						ltb_correos.DataSource = null;
						ltb_correos.DataSource = dto_rfc.correos_electronicos;
					}
					break;
			}
		}

		private void btn_cambiar_rfc_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this,"¿Esta seguro de querer cambiar el RFC asignado a esta venta?","Cambiar RFC",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

			if(dr == DialogResult.Yes)
			{
				Busqueda_rfcs busqueda_rfcs = new Busqueda_rfcs();
				busqueda_rfcs.ShowDialog();

				if (busqueda_rfcs.rfc_registro_id != "")
				{
					rfc_registro_id = busqueda_rfcs.rfc_registro_id;
					abrir_segundo_panel();
				}
			}
		}

		private void btn_enviarcorreo_Click(object sender, EventArgs e)
		{
			Facturacion fac = new Facturacion();
			var result = fac.enviar_correo(Convert.ToInt64(txt_folio_busqueda.Text),"mijael.3.21@gmail.com");
		}

		private void txt_folio_busqueda_KeyPress(object sender, KeyPressEventArgs e)
		{

		}

		private void worker_facturacion_DoWork(object sender, DoWorkEventArgs e)
		{	
			lbl_proceso_factura.Text = "Obteniendo información de la venta ...";
			dto_rfc.razon_social = txt_razon_social.Text;
			dto_rfc.calle = txt_calle.Text;
			dto_rfc.numero_exterior = txt_numero_exterior.Text;
			dto_rfc.numero_interior = txt_numero_interior.Text;

			string usuario = Config_helper.get_config_global("facturacion_usuario");
			string password = Config_helper.get_config_global("facturacion_password");

			//var conector_txt = dao_ventas.get_informacion_factura(venta_id_facturacion, "PRUEBAS", string.Join(",",dto_rfc.correos_electronicos.ToArray()));
			var conector_txt = "";
			var txt_encode = Convert.FromBase64String(Misc_helper.EncodeTo64(conector_txt));

			lbl_proceso_factura.Text = "Procesando factura ...";
			
			//importar_factura = WebServicePac_helper.importar(txt_encode,false);

			if(importar_factura.status)
			{
				lbl_proceso_factura.Text = "Enviando factura por correo ...";
				DAO_Sucursales dao_sucursales = new DAO_Sucursales();
				var sucursal_data = dao_sucursales.get_sucursal_data(Convert.ToInt32(Config_helper.get_config_local("sucursal_id")));

				string[] correos = new string[ltb_correos.Items.Count];
				int count = 0;

				foreach(var items in ltb_correos.Items)
				{
					correos[count] = items.ToString();
					count++;
				}

				//enviar_factura = WebServicePac_helper.enviar(importar_factura.serie,importar_factura.uuid,importar_factura.folio,sucursal_data.pdf_id,correos);

				if(enviar_factura.status)
				{
					//facturacion_terminado = true;

					//SE ELIMINO LA VARIABLE facturacion_terminado DEBIDO A QUE YA NO SE USA ESTE MODULO, PERO SE TIENE DE REFERENCIA EN CASO DE FALLO
				}
				else
				{
					//facturacion_terminado = false;
					MessageBox.Show(this, importar_factura.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show(this,importar_factura.mensaje,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

	}
}
