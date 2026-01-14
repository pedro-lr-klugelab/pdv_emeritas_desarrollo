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
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.tae_diestel_new;
using PXSecurity.Datalogic.Classes;

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Pago_tipos : Form
	{
		public bool venta_terminada = false;
        public bool es_tae = false;
        public string tipo_pago;
		private bool es_traspaso_venta = false;
		DAO_Pago_tipos dao_pago_tipos = new DAO_Pago_tipos();
		private decimal total_pagar;
		private decimal monto_total;
		private long venta_id;
        public string numero_referencia;
        public string sku;
        public int numero_transaccion;
        DTO_Validacion validacion = new DTO_Validacion();
        DAO_Cancelaciones dao_cancelaciones = new DAO_Cancelaciones();
        List<Tuple<int, string, string, decimal, int>> productos_no_cancelados = new List<Tuple<int, string, string, decimal, int>>();
        bool emitir_cancelacion_factura = false;
        bool emitir_nota_credito = false;

		Dictionary<int,Tuple<string,bool>> pago_tipos = new Dictionary<int,Tuple<string,bool>>();
		DataTable detallado_traspaso_venta = new DataTable();
		DAO_Traspasos dao_traspasos = new DAO_Traspasos();
		private string hash;
        DTO_Pago_tipos pago_tipo_actual = new DTO_Pago_tipos();

		public Pago_tipos(long venta_id, decimal total_pagar, DataTable detallado_traspaso_venta, string hash)
		{
			es_traspaso_venta = true;
			this.hash = hash;
			this.detallado_traspaso_venta = detallado_traspaso_venta;
           
			this.venta_id = venta_id;
			this.total_pagar = total_pagar;
			InitializeComponent();
			lbl_total_pagar.Text = string.Format("{0,12:C2}", total_pagar);
		}

        public Pago_tipos(long venta_id, decimal total_pagar, bool es_tae = false, string sku = "", string numero_referencia = "", int numero_transaccion = 0)
		{
			es_traspaso_venta = false;
			this.venta_id = venta_id;
            this.es_tae = es_tae;
            this.numero_referencia = numero_referencia;
            this.sku = sku;
            this.numero_transaccion = numero_transaccion;
			this.total_pagar = total_pagar;
			InitializeComponent();
			lbl_total_pagar.Text = string.Format("{0,12:C2}",total_pagar);
		}

		public void get_pago_tipos()
		{
			var result_pago_tipos = dao_pago_tipos.get_pago_tipos();

			//cbb_pago_tipo.Items.Clear();

			Dictionary<string,string> pago_tipos_cbb = new Dictionary<string,string>();

			foreach(DataRow row in result_pago_tipos.Rows)
			{
				Tuple<string, bool> parametros = new Tuple<string, bool>(row["nombre"].ToString(), Convert.ToBoolean(row["usa_cuenta"]));

				pago_tipos.Add(Convert.ToInt32(row["pago_tipo_id"]), parametros);
				pago_tipos_cbb.Add(row["nombre"].ToString(), row["nombre"].ToString());
			}

			/*cbb_pago_tipo.DataSource =  new BindingSource(pago_tipos_cbb,null);
			cbb_pago_tipo.DisplayMember = "Key";
			cbb_pago_tipo.ValueMember = "Value";

			cbb_pago_tipo.DroppedDown = true;
			cbb_pago_tipo.Focus();*/
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Pago_tipos_Shown(object sender, EventArgs e)
		{
            Tipos_pago tipos_pago = new Tipos_pago(es_tae);
            tipos_pago.ShowDialog();

            pago_tipo_actual = tipos_pago.return_pago_tipos;

            if (pago_tipo_actual.pago_tipo_id == 0)
            {
                this.Close();
            }
            else
            {
                lbl_metodo_pago.Text = pago_tipo_actual.nombre;
                validar_usa_cuenta();
            }

			//get_pago_tipos();
		}

		decimal get_total_vale_efectivo(string hash_cuenta)
		{
			string[] hash = hash_cuenta.Split('$');

			if(hash.Length == 2)
			{
				DAO_Vales_efectivo dao_veles = new DAO_Vales_efectivo();
				return dao_veles.get_total_vale_efectivo(hash[1]);
			}

			return 0;
		}

		public void validar_usa_cuenta()
		{
            txt_cantidad.Text = "";
            txt_cuenta.Text = "";
            txt_cantidad.Text = Convert.ToString(total_pagar - monto_total);

            txt_cuenta.Enabled = pago_tipo_actual.usa_cuenta;

            if (pago_tipo_actual.usa_cuenta)
            {
                if (pago_tipo_actual.nombre == "VALE FARMACIA")
                {
                    txt_cuenta.UseSystemPasswordChar = false;
                    txt_cantidad.ReadOnly = true;
                    txt_cantidad.Text = "";
                }
                else
                {
                    txt_cantidad.ReadOnly = false;
                    txt_cuenta.UseSystemPasswordChar = false;
                }

                txt_cuenta.Focus();
            }
            else
            {
                txt_cantidad.ReadOnly = false;
                txt_cantidad.Focus();
                txt_cantidad.SelectAll();
            }

		    dgv_pagos.Refresh();
		}

		private void cbb_pago_tipo_KeyDown(object sender, KeyEventArgs e)
		{
            /*
            if(cbb_pago_tipo.DroppedDown == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (total_pagar > monto_total)
                    {
                        validar_usa_cuenta();
                    }

                    dgv_pagos.ClearSelection();
                }
            }
             * */
		}

		private void txt_cuenta_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
					if (txt_cantidad.ReadOnly)
					{
						DAO_Vales_efectivo dao = new DAO_Vales_efectivo();

						string temp_vale_efectivo_id = txt_cuenta.Text.Trim().ToString();
						var tmp_explode_id = temp_vale_efectivo_id.Split('$');
						if(tmp_explode_id.Length == 2)
						{
							DTO_Vale vale_data = dao.vale_data(tmp_explode_id[1]);

							if (vale_data.vale_efectivo_id != null)
							{
								if (vale_data.fecha_canje.Equals(null))
								{
									txt_cantidad.Text = vale_data.total.ToString();
									txt_cantidad.Enabled = true;
									txt_cantidad.Focus();
								}
								else
								{
									MessageBox.Show(this, "Este vale ya ha sido canjeado, retener el vale y reportar a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									txt_cuenta.Text = "";
									txt_cuenta.Focus();
								}
							}
							else
							{
								MessageBox.Show(this, "Código de vale no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								txt_cuenta.Text = "";
								txt_cuenta.Focus();
							}
						}
						else
						{
							MessageBox.Show(this, "Código inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							txt_cuenta.Text = "";
							txt_cuenta.Focus();
						}
					}
					else
					{
						txt_cantidad.Enabled = true;
						txt_cantidad.Focus();
					}
				break;
			}
		}

		private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch (keycode)
			{
				case 13:
                    if(txt_cantidad.Text.Trim().Length > 0)
                    {
                        decimal cantidad_necesaria = Convert.ToDecimal(total_pagar - monto_total);

                        if (pago_tipo_actual.nombre.Equals("VALE FARMACIA") || (bool)pago_tipo_actual.entrega_cambio == true)
                        {
                            agregar_monto();
                        }
                        else
                        {
                            decimal cantidad_actual = Convert.ToDecimal(txt_cantidad.Text.Trim());

                            if (cantidad_actual <= cantidad_necesaria)
                            {
                                agregar_monto();
                            }
                            else
                            {
                                MessageBox.Show(this,"Este metodo de pago no entrega cambio","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                txt_cantidad.Text = Convert.ToString(total_pagar - monto_total);
                                txt_cantidad.Focus();
                                txt_cantidad.SelectAll();
                            }
                        }
                    }
				break;
			}
		}

		public void limpiar_pago_tipo()
		{
			txt_cuenta.UseSystemPasswordChar = false;

			txt_cantidad.ReadOnly = false;
			txt_cantidad.Text = "";
			txt_cuenta.Text = "";
			txt_cantidad.Enabled = false;
			txt_cuenta.Enabled = true;

			/*
            if(desplegar_pago_tipo)
			{
				cbb_pago_tipo.SelectedIndex = 0;
				cbb_pago_tipo.Focus();
				cbb_pago_tipo.DroppedDown = true;
			}
            */
			dgv_pagos.ClearSelection();
		}

		public void validar_monto_pagar()
		{
			dgv_pagos.ClearSelection();
			monto_total = 0;

			foreach(DataGridViewRow row in dgv_pagos.Rows)
			{
				monto_total += Convert.ToDecimal(row.Cells["monto"].Value);
			}

            Dictionary<string, decimal> montos_pagar_actual = new Dictionary<string, decimal>();

            foreach (DataGridViewRow row in dgv_pagos.Rows)
            {
                if (!montos_pagar_actual.ContainsKey(row.Cells["metodo_pago"].Value.ToString()))
                {
                    montos_pagar_actual.Add(row.Cells["metodo_pago"].Value.ToString(), Convert.ToDecimal(row.Cells["monto"].Value));
                }
                else
                {
                    montos_pagar_actual[row.Cells["metodo_pago"].Value.ToString()] += Convert.ToDecimal(row.Cells["monto"].Value);
                }
            }
            
            decimal cantidad_tmp = 0;
            
            foreach (var tmp in montos_pagar_actual)
            {
                if (tmp.Key == "VALE FARMACIA")
                {
                    cantidad_tmp += tmp.Value;
                }
            }

            if (cantidad_tmp >= total_pagar)
            {
                decimal importe_restante = total_pagar;

                foreach (DataGridViewRow row in dgv_pagos.Rows)
                {
                    if (row.Cells["metodo_pago"].Value.ToString() != "VALE FARMACIA")
                    {
                        dgv_pagos.Rows.RemoveAt(row.Index);
                    }
                }

                foreach (DataGridViewRow row in dgv_pagos.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["monto"].Value) >= importe_restante)
                    {
                        row.Cells["importe"].Value = importe_restante;
                    }
                    else
                    {
                        row.Cells["importe"].Value = Convert.ToDecimal(row.Cells["monto"].Value);
                    }

                    importe_restante -= Convert.ToDecimal(row.Cells["monto"].Value);
                }
            }


			lbl_monto_total.Text = string.Format("{0,15:C2}",monto_total);

			if(monto_total >= total_pagar)
			{
                txt_cuenta.Text = "";
                txt_cantidad.Text = "";

                btn_cambiar_metodo_pago.Enabled = false;
                txt_cantidad.Enabled = false;
                txt_cuenta.Enabled = false;

				btn_terminar_venta.Enabled = true;
				btn_terminar_venta.Focus();
			}
			else
			{
                btn_cambiar_metodo_pago.Enabled = true;
                txt_cantidad.Enabled = true;
                txt_cuenta.Enabled = true;

                txt_cuenta.Text = "";
                txt_cantidad.Text = "";
				btn_terminar_venta.Enabled = false;
                cambiar_pago_tipo();
			}
		}

		public void agregar_monto()
		{
			if (txt_cantidad.TextLength > 0)
			{
				string nombre_pago_tipo = pago_tipo_actual.nombre;
				decimal cantidad_ingresada = Convert.ToDecimal(txt_cantidad.Text);

				bool existe = false;

				foreach(DataGridViewRow row in dgv_pagos.Rows)
				{
					if(row.Cells["metodo_pago"].Value.ToString().Equals(nombre_pago_tipo))
					{
                        if (row.Cells["cuenta"].Value.ToString().Equals(txt_cuenta.Text.Trim()) && !row.Cells["metodo_pago"].Value.ToString().Equals("VALE FARMACIA"))
						{
                            decimal importe = Convert.ToDecimal(total_pagar - monto_total);

                            if (importe > cantidad_ingresada)
                            {
                                importe = cantidad_ingresada;
                            }

                            importe += Convert.ToDecimal(row.Cells["importe"].Value);

                            row.Cells["importe"].Value = importe;
							row.Cells["monto"].Value = (Convert.ToDecimal(row.Cells["monto"].Value) + Convert.ToDecimal(txt_cantidad.Text));

							existe = true;
						}
						else
						{
							if (row.Cells["cuenta"].Value.ToString().Equals(txt_cuenta.Text) && row.Cells["metodo_pago"].Value.ToString().Equals("VALE FARMACIA"))
							{
								MessageBox.Show(this,"No puede ser aplicado el mismo VALE FARMACIA mas de una vez","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
								existe = true;
							}
						}

						break;
					}
				}

				if(!existe)
				{
                    decimal importe = Convert.ToDecimal(total_pagar - monto_total);

                    if(importe > cantidad_ingresada)
                    {
                        importe = cantidad_ingresada;
                    }

					dgv_pagos.Rows.Add(nombre_pago_tipo, txt_cuenta.Text, importe, cantidad_ingresada);
					dgv_pagos.ClearSelection();			
				}

				validar_monto_pagar();
			}
		}

		private void txt_cantidad_Enter(object sender, EventArgs e)
		{
			if(!txt_cantidad.ReadOnly)
			{
				txt_cantidad.Text = Convert.ToString(total_pagar - monto_total);
			}
		}

		private void dgv_pagos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
            validar_usa_cuenta();
			validar_monto_pagar();
			dgv_pagos.ClearSelection();
		}

		public void terminar_traspaso_venta(bool imprimir_venta)
		{
			Ticket_venta ticket_venta = new Ticket_venta();

			DataTable table_pago_tipos = new DataTable();
			table_pago_tipos.Columns.Add("metodo_pago", typeof(string));
			table_pago_tipos.Columns.Add("cuenta", typeof(string));
            table_pago_tipos.Columns.Add("importe", typeof(string));
			table_pago_tipos.Columns.Add("monto", typeof(string));

			foreach (DataGridViewRow dgvR in dgv_pagos.Rows)
			{
                table_pago_tipos.Rows.Add(dgvR.Cells["metodo_pago"].Value, dgvR.Cells["cuenta"].Value, dgvR.Cells["importe"].Value, dgvR.Cells["monto"].Value);
			}

			DAO_Ventas dao_ventas = new DAO_Ventas();
			string cambio = string.Format("{0,13:C2}", Convert.ToDecimal(monto_total - total_pagar));

			comunes.Custom_alert custom_alert = new comunes.Custom_alert("Cambio", cambio);

            if(imprimir_venta == false)
            {
                var result_importar_traspaso = dao_traspasos.importar_traspaso_para_venta(hash, true);
                var validacion = result_importar_traspaso.Item1;

                if (validacion.status)
                {
                    rellenar_detallado_venta();

                    int filas_afectadas = dao_ventas.terminar_venta(venta_id, table_pago_tipos);
                    ticket_venta.construccion_ticket(venta_id);

                    if (filas_afectadas > 0)
                    {
                        venta_terminada = true;
                        this.Hide();
                        ticket_venta.print();
                        custom_alert.ShowDialog();

                        Ticket_venta ticket = new Ticket_venta();
                        ticket.construccion_ticket(venta_id, false);
                        DAO_Impresiones dao_impresiones = new DAO_Impresiones();
                        dao_impresiones.registrar_impresion((long)Misc_helper.get_terminal_id(), ticket.ticket.ToString(), "VENTA", venta_id, "TICKET NO IMPRESO");

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un problema al intentar terminar la venta, notifique a su administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool status_impresora = HELPERS.Print_helper.status_impresora(false);

                if (status_impresora)
                {
                    var result_importar_traspaso = dao_traspasos.importar_traspaso_para_venta(hash, true);
                    var validacion = result_importar_traspaso.Item1;

                    if (validacion.status)
                    {
                        rellenar_detallado_venta();

                        int filas_afectadas = dao_ventas.terminar_venta(venta_id, table_pago_tipos);
                        ticket_venta.construccion_ticket(venta_id);

                        if (filas_afectadas > 0)
                        {
                            venta_terminada = true;
                            this.Hide();
                            ticket_venta.print();
                            custom_alert.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Ocurrio un problema al intentar terminar la venta, notifique a su administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show(this, "Impresora desconectada o Apagada, ¿Desea continuar terminando la venta?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        var result_importar_traspaso = dao_traspasos.importar_traspaso_para_venta(hash, true);
                        var validacion = result_importar_traspaso.Item1;

                        if (validacion.status)
                        {
                            rellenar_detallado_venta();

                            int filas_afectadas = dao_ventas.terminar_venta(venta_id, table_pago_tipos);
                            ticket_venta.construccion_ticket(venta_id);

                            if (filas_afectadas > 0)
                            {
                                venta_terminada = true;
                                this.Hide();
                                ticket_venta.print();
                                custom_alert.ShowDialog();

                                Ticket_venta ticket = new Ticket_venta();
                                ticket.construccion_ticket(venta_id, false);
                                DAO_Impresiones dao_impresiones = new DAO_Impresiones();
                                dao_impresiones.registrar_impresion((long)Misc_helper.get_terminal_id(), ticket.ticket.ToString(), "VENTA", venta_id, "IMPRESORA DESCONECTADA O APAGADA");

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(this, "Ocurrio un problema al intentar terminar la venta, notifique a su administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
		}

		public void rellenar_detallado_venta()
		{
			DAO_Ventas dao_ventas = new DAO_Ventas();

			foreach(DataRow row in detallado_traspaso_venta.Rows)
			{
				dao_ventas.insertar_detallado(row["amecop"].ToString(),row["caducidad"].ToString(),row["lote"].ToString(),Convert.ToInt32(row["cantidad"]),venta_id,false);	
			}
		}

		public void terminar_venta(bool imprimir_venta)
		{
            try
            {
                Ticket_venta ticket_venta = new Ticket_venta();

                DataTable table_pago_tipos = new DataTable();
                table_pago_tipos.Columns.Add("metodo_pago", typeof(string));
                table_pago_tipos.Columns.Add("cuenta", typeof(string));
                table_pago_tipos.Columns.Add("importe", typeof(string));
                table_pago_tipos.Columns.Add("monto", typeof(string));
                bool una_impresion = false;
                string s_pagos = "";
                foreach (DataGridViewRow dgvR in dgv_pagos.Rows)
                {
                    table_pago_tipos.Rows.Add(dgvR.Cells["metodo_pago"].Value, dgvR.Cells["cuenta"].Value, dgvR.Cells["importe"].Value, dgvR.Cells["monto"].Value);

                    s_pagos = s_pagos + "," + dgvR.Cells["metodo_pago"].Value.ToString();

                }

                if (!s_pagos.Contains("TARJETA"))
                    una_impresion = true;

                DAO_Ventas dao_ventas = new DAO_Ventas();
                string cambio = string.Format("{0,13:C2}", Convert.ToDecimal(monto_total - total_pagar));

                comunes.Custom_alert custom_alert = new comunes.Custom_alert("Cambio", cambio);

                if(imprimir_venta == false)
                {
                    
                    int filas_afectadas = dao_ventas.terminar_venta(venta_id, table_pago_tipos);

                    if (filas_afectadas > 0)
                    {
                        Ticket_venta ticket = new Ticket_venta();
                        ticket.construccion_ticket(venta_id, false);
                        DAO_Impresiones dao_impresiones = new DAO_Impresiones();
                        var venta_data = dao_ventas.get_venta_data(venta_id);
                        dao_impresiones.registrar_impresion((long)Misc_helper.get_terminal_id(), ticket.ticket.ToString(), "VENTA", venta_data.venta_folio, "TICKET NO IMPRESO");
                        venta_terminada = true;
                       //this.Hide();
                        custom_alert.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un problema al intentar terminar la venta, notifique a su administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool status_impresora = HELPERS.Print_helper.status_impresora(false);

                    if (Convert.ToInt32(Properties.Configuracion.Default.usar_impresora_remota_tickets) == 1 || status_impresora)
                    {
                        int filas_afectadas = dao_ventas.terminar_venta(venta_id, table_pago_tipos);

                        if (filas_afectadas > 0)
                        {
                            #region
                            // Comentar este bloque si se va a liberar una nueva version antes de finalizar el TAE
                            /*********************************************************************************************************************************************/
                            /*
                            if (es_tae)
                            {
                                Tae_helper tae_helper = new Tae_helper();

                                int? empleado_id = (int)FORMS.comunes.Principal.empleado_id;

                                //correcto 
                                long monto_tmp = (long)total_pagar;

                                //provocar_error
                                //long monto_tmp = (long)(total_pagar + 10);

                                long? venta_id_tmp = venta_id;

                                cCampo[] campos;

                                Cursor = Cursors.WaitCursor;
                                //se realiza el metodo ejecuta del ws del tae   
                                campos = tae_helper.ejecuta_tae(empleado_id, sku, numero_referencia, "EFE", monto_tmp, venta_id_tmp, numero_transaccion);
                                if (valida_ws(campos))
                                {
                                    bool ok = false;
                                    string mensaje = "";
                                    decimal saldo_tae = Convert.ToDecimal(Config_helper.get_config_local("bolsa_tae"));
                                    decimal importe_tae = Tae_helper.get_importe_tae(sku);
                                    saldo_tae = saldo_tae - importe_tae;
                                    ok = Config_helper.set_config_local("bolsa_tae", saldo_tae.ToString());

                                    mensaje += "Proveedor: " + campos[0].sValor.ToString() + "\n";
                                    mensaje += "Referencia: " + PXCryptography.PXDecryptFX(campos[1].sValor.ToString()) + "\n";
                                    mensaje += "Autorización: " + campos[2].sValor.ToString() + "\n";
                                    mensaje += "Monto: " + campos[3].sValor.ToString() + "\n";
                                    mensaje += "Mensaje: " + campos[4].sValor.ToString() + "\n";

                                    MessageBox.Show(this, mensaje, "Pago exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Ticket_venta_tae ticket_venta_tae = new Ticket_venta_tae();
                                    ticket_venta_tae.construccion_ticket(venta_id, campos);
                                    ticket_venta_tae.print();
                                    Cursor = Cursors.Default;
                                    venta_terminada = true;
                                    this.Hide();
                                    custom_alert.ShowDialog();
                                    this.Close();

                                }
                                else
                                {
                                    string error_tae = "Codigo: (" + campos[0].sValor.ToString() + "): " + campos[1].sValor.ToString();

                                    ticket_venta.construccion_ticket(venta_id);
                                    venta_terminada = true;
                                    this.Hide();
                                    ticket_venta.print();
                                    custom_alert.ShowDialog();
                                    this.Close();

                                    MessageBox.Show(this, error_tae, "Información importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    validacion = dao_cancelaciones.cancelar_venta(Convert.ToInt64(venta_id), error_tae, productos_no_cancelados, emitir_cancelacion_factura, emitir_nota_credito, es_tae);

                                    if (validacion.status)
                                    {
                                        CLASSES.PRINT.Devolucion devolucion = new CLASSES.PRINT.Devolucion();
                                        devolucion.construccion_ticket(Convert.ToInt64(venta_id), false);
                                        devolucion.print();
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            /********************************************************************************************************************************************/
                            //else 
                            //{ 
                            #endregion
                            ticket_venta.construccion_ticket(venta_id);
                                venta_terminada = true;   
                                ticket_venta.print();
                                this.Hide();

                                if(una_impresion==false)
                                    ticket_venta.print();

                                custom_alert.ShowDialog();
                               // this.Close();
                            //}
                        }
                        else
                        {
                            MessageBox.Show(this, "Ocurrio un problema al intentar terminar la venta, notifique a su administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show(this, "Impresora desconectada o Apagada, ¿Desea continuar terminando la venta?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (dr == DialogResult.Yes)
                        {
                            int filas_afectadas = dao_ventas.terminar_venta(venta_id, table_pago_tipos);

                            if (filas_afectadas > 0)
                            {
                                venta_terminada = true;

                                Ticket_venta ticket = new Ticket_venta();
                                ticket.construccion_ticket(venta_id, false);//
                                DAO_Impresiones dao_impresiones = new DAO_Impresiones();
                                dao_impresiones.registrar_impresion((long)Misc_helper.get_terminal_id(), ticket.ticket.ToString(), "VENTA", venta_id, "IMPRESORA DESCONECTADA O APAGADA");

                                this.Hide();
                                custom_alert.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(this, "Ocurrio un problema al intentar terminar la venta, notifique a su administrador!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this,"Error: "+ex.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
		}

        private bool valida_ws(cCampo[] campos)
        {
            bool ok = false;

            if(campos.Length > 2)
            {
                ok = true;
            }

            return ok;
        }

		private void btn_terminar_venta_Click(object sender, EventArgs e)
		{
            DialogResult dr = MessageBox.Show(this,"Desea imprimir el ticket de la venta","Información",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

            bool imprimir_ticket = (dr == DialogResult.Yes) ? true : false;

			if(es_traspaso_venta)
			{
				terminar_traspaso_venta(imprimir_ticket);
			}
			else
			{
				terminar_venta(imprimir_ticket);
			}
		}

		private void btn_terminar_venta_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			if(keycode == 27)
			{
				dgv_pagos.Focus();
				dgv_pagos.CurrentCell = dgv_pagos.Rows[0].Cells["metodo_pago"];
				dgv_pagos.Rows[0].Selected = true;
			}
		}

		private void dgv_pagos_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			if (keycode == 27)
			{
				
			}
		}

		private void cbb_pago_tipo_Enter(object sender, EventArgs e)
		{
			dgv_pagos.ClearSelection();
		}

		private void dgv_pagos_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			dgv_pagos.ClearSelection();
		}

		private void cbb_pago_tipo_SelectionChangeCommitted(object sender, EventArgs e)
		{
			
		}

        private void cbb_pago_tipo_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void cbb_pago_tipo_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cbb_pago_tipo_DropDownClosed(object sender, EventArgs e)
        {
            if (total_pagar > monto_total)
            {
                validar_usa_cuenta();
            }
        }

        private void btn_cambiar_metodo_pago_Click(object sender, EventArgs e)
        {
            cambiar_pago_tipo();   
        }

        void cambiar_pago_tipo()
        {
            Tipos_pago tipos_pago = new Tipos_pago(es_tae);
            tipos_pago.ShowDialog();

            if (tipos_pago.return_pago_tipos.pago_tipo_id > 0)
            {
                pago_tipo_actual = tipos_pago.return_pago_tipos;
                lbl_metodo_pago.Text = pago_tipo_actual.nombre;
                validar_usa_cuenta();
            }
        }

        private void Pago_tipos_Load(object sender, EventArgs e)
        {

        }

        private void txt_cantidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_cuenta_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
