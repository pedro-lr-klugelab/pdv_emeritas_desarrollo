using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using RestSharp;
using RestSharp.Deserializers;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;

namespace Farmacontrol_PDV.FORMS.movimientos.recibir_bultos
{
    public partial class Recibir_bultos_principal : Form
    {
		Progressbar_form progressbar_form;

        public Recibir_bultos_principal()
        {
			InitializeComponent();
           // txt_codigo.ContextMenu = new ContextMenu();
        }

        private void boton_aceptar_Click(object sender, EventArgs e)
        {
			if (MessageBox.Show("Desea importar los traspasos?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				boton_aceptar.Enabled = false;

				progressbar_form = new Progressbar_form();
				progressbar_form.TopMost = false;
				worker.RunWorkerAsync();
				progressbar_form.ShowDialog();
			}
        }

        private void boton_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void codigo_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            txt_codigo.ContextMenu = new ContextMenu();
            if (e.Control == true)
            {
                MessageBox.Show(this, "Es necesario escanear el codigo del bulto");
            }
            */

            if (Convert.ToInt32(e.KeyCode) == 13 && txt_codigo.TextLength > 0)
            {

                string hash_txt_codigo = txt_codigo.Text.ToString();
                hash_txt_codigo = hash_txt_codigo + "$1$1";
                string[] split_hash = hash_txt_codigo.Split('$');
                string bultos = "";

                if (split_hash.Length > 7)
                {
                    MessageBox.Show(this, "FORMATO INCOMPATIBLE, NOTIFICA A INFORMATICA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    bultos = "$1$1";

                    hash_txt_codigo = split_hash[0] + "$" + split_hash[1] +  "$" + split_hash[2] + "$" +split_hash[3] + "$" + split_hash[4];

                }
                
                //PRIMERO PONERLO ALA REVERSA
                hash_txt_codigo = reversa(hash_txt_codigo);

                hash_txt_codigo = Decode(hash_txt_codigo);

                if (hash_txt_codigo.Equals("0"))
                {
                    MessageBox.Show(this, "FORMATO INCOMPATIBLE, NOTIFICA A INFORMATICA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (hash_txt_codigo.Equals("-1"))
                {
                    MessageBox.Show(this, "FORMATO DE TRASPASO ALTERADO, NOTIFICA A INFORMATICA ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


				DTO_Codigo_barras codigo_barras = new DTO_Codigo_barras();

				try
				{
                    codigo_barras.Valida_codigo((hash_txt_codigo + bultos), "BULTO_TRASPASO");
				}
				catch(Exception exception)
				{
					Log_error.log(exception);
				}

				if (codigo_barras.Codigo_valido == false)
				{
					MessageBox.Show(this, codigo_barras.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				DAO_Sucursales sucursal = new DAO_Sucursales();
				DTO_Sucursal sucursal_data_origen = new DTO_Sucursal();
				DTO_Sucursal sucursal_data_destino = new DTO_Sucursal();
				
				sucursal_data_origen = sucursal.get_sucursal_data(codigo_barras.origen_sucursal_id);
				sucursal_data_destino = sucursal.get_sucursal_data(codigo_barras.destino_sucursal_id);

				if (sucursal_data_origen.sucursal_id == 0)
				{
					MessageBox.Show(this, "El número de sucursal origen no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (sucursal_data_destino.sucursal_id == 0)
				{
					MessageBox.Show(this, "El número de sucursal destino no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (Convert.ToBoolean(sucursal_data_destino.es_local) == false)
				{
					MessageBox.Show(this, "Este traspaso no es para esta sucursal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				for (int i = 1; i <= codigo_barras.total_bultos; i++)
				{
					string row_id = String.Format("{0}_{1}_{2}", sucursal_data_origen.sucursal_id, codigo_barras.folio, i);
					int capturado = Convert.ToInt16(i == codigo_barras.numero_bulto);

					if(checa_existe(row_id))
					{
						if(capturado == 1)
						{
							marca_capturado(row_id);
						}
					}
					else
					{
						recibir_bultos_grid.Rows.Add(
							row_id,
							capturado,
							codigo_barras.total_bultos,
							sucursal_data_origen.nombre, 
							codigo_barras.folio, 
							String.Format("{0} de {1}", i, codigo_barras.total_bultos)
						);
					}
				}

				txt_codigo.Text = "";

				recibir_bultos_grid.ClearSelection();
				
				colorea_capturados();

				checa_todos_capturados();
			}
        }

		private void checa_todos_capturados()
		{
			DataGridViewRow row;
			bool todos_capturados = true;
			
			boton_aceptar.Enabled = false;

			if (recibir_bultos_grid.Rows.Count > 0)
			{
				for (int i = 0; i < recibir_bultos_grid.Rows.Count; i++)
				{
					row = recibir_bultos_grid.Rows[i];

					if (row.Cells["capturado"].Value.ToString() == "0")
					{
						todos_capturados = false;
					}
				}

				if (todos_capturados)
				{
					boton_aceptar.Enabled = true;
				}
				else
				{
					boton_aceptar.Enabled = false;
				}
			}
			else
			{
				txt_codigo.Focus();
			}
		}

		private void colorea_capturados()
		{
			DataGridViewRow row;
			
			for (int i = 0; i < recibir_bultos_grid.Rows.Count; i++)
			{
				row = recibir_bultos_grid.Rows[i];

				if(row.Cells["capturado"].Value.ToString() == "1")
				{
					recibir_bultos_grid.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
				}
			}
		}

		public void marca_capturado(string row_id)
		{
			DataGridViewRow row;

			for (int i = 0; i < recibir_bultos_grid.Rows.Count; i++)
			{
				row = recibir_bultos_grid.Rows[i];

				if (row.Cells["row_id"].Value.ToString() == row_id)
				{
					recibir_bultos_grid.Rows[i].Cells["capturado"].Value = "1";
				}
			}
		}

		private bool checa_existe(string row_id)
		{

			DataGridViewRow row;

			for (int i = 0; i < recibir_bultos_grid.Rows.Count; i++)
			{
				row = recibir_bultos_grid.Rows[i];

				if (row.Cells["row_id"].Value.ToString() == row_id)
				{
					return true;
				}
			}

			return false;
		}

		private void txt_codigo_Enter(object sender, EventArgs e)
		{
			recibir_bultos_grid.ClearSelection();
		}

		private void recibir_bultos_grid_KeyDown(object sender, KeyEventArgs e)
		{	
			if(Convert.ToInt32(e.KeyCode) == 46)
			{
				if(recibir_bultos_grid.SelectedRows.Count > 0)
				{
					delete_traspaso(recibir_bultos_grid.SelectedRows[0].Cells[0].Value.ToString());
				}
			}
		}

		private Dictionary<int, Dictionary<int, int>> folios_por_sucursal()
		{
			Dictionary<int, Dictionary<int, int>> folios = new Dictionary<int,Dictionary<int,int>>();

			string[] parametros;
			int sucursal_id;
			int folio;
			int total_bultos;

			DataGridViewRow row;

			for (int i = 0; i < recibir_bultos_grid.Rows.Count; i++)
			{
				row = recibir_bultos_grid.Rows[i];

				parametros = row.Cells["row_id"].Value.ToString().Split('_');

				sucursal_id = int.Parse(parametros[0]);
				folio = int.Parse(parametros[1]);
				total_bultos = int.Parse(row.Cells["numero_bultos"].Value.ToString());

				if(folios.ContainsKey(sucursal_id) == false)
				{
					folios.Add(sucursal_id, new Dictionary<int, int>());
				}

				if (folios[sucursal_id].ContainsKey(folio) == false)
				{
					folios[sucursal_id].Add(folio, total_bultos);
				}
			}

			return folios;
		}

		private void delete_traspaso(string row_id)
		{
			string[] parametros = row_id.Split('_');
			int folio = int.Parse(parametros[0].ToString());
			int bulto = int.Parse(parametros[1].ToString());

			var result = MessageBox.Show(String.Format("Desea eliminar los bultos del traspaso {0}?", folio), "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

			if(result == DialogResult.Yes)
			{
				Regex rgx = new Regex(String.Format(@"^{0}_[0-9]*$", folio));

				DataGridViewRow row;

				for (int i = 0; i < recibir_bultos_grid.Rows.Count; i++)
				{
					row = recibir_bultos_grid.Rows[i];

					if(rgx.IsMatch(row.Cells["row_id"].Value.ToString()))
					{
						recibir_bultos_grid.Rows[i].Selected = true;
					}
				}

				foreach (DataGridViewRow selected_row in recibir_bultos_grid.SelectedRows)
				{
					recibir_bultos_grid.Rows.Remove(selected_row);
				}
			}

			checa_todos_capturados();
		}

		//[System.Diagnostics.DebuggerNonUserCodeAttribute()]
		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{

            string sucursal_local = "192.168.1.250"; //esto es nuevo
			//PROCESA GRID PARA OBTENER SUCURSALES
			Dictionary<int, Dictionary<int, int>> sucursales = folios_por_sucursal();

			//CHECANDO CONECTIVIDAD A SUCURSALES
			worker.ReportProgress(30);

			DAO_Sucursales dao_sucursales = new DAO_Sucursales();
			DAO_Traspasos dao_traspasos = new DAO_Traspasos();

			foreach(KeyValuePair<int, Dictionary<int, int>> sucursal in sucursales)
			{
				int sucursal_id = sucursal.Key;
				Dictionary<int, int> folios = sucursal.Value;

				DTO_Sucursal sucursal_data = dao_sucursales.get_sucursal_data(sucursal_id);
                /*
				if (Red_helper.checa_online(sucursal_data.ip_sucursal) == false)
				{
					e.Result = String.Format("La sucursal {0} ({1}) no responde, intente de nuevo más tarde.", sucursal_data.nombre, sucursal_data.ip_sucursal);
					return;
				}*/
                //TIENE QUE SER OTRO METODO

				//if (Red_helper.checa_rest(sucursal_data.ip_sucursal) == false)
                if (Red_helper.checa_rest_online(sucursal_data.ip_sucursal) == false)
				{
					e.Result = String.Format("La SUCURSAL {0} ({1}) no responde la petición, intente de nuevo más tarde.", sucursal_data.nombre, sucursal_data.ip_sucursal);
					return;
				}
			}

			//VALIDANDO TRASPASOS
			worker.ReportProgress(60);

			foreach (KeyValuePair<int, Dictionary<int, int>> sucursal in sucursales)
			{
				int sucursal_id = sucursal.Key;
				Dictionary<int, int> folios = sucursal.Value;

				DTO_Sucursal sucursal_data = dao_sucursales.get_sucursal_data(sucursal_id);

				foreach (KeyValuePair<int, int> folio_pair in folios)
				{
					//RestClient client = new RestClient(String.Format("http://{0}/", sucursal_data.ip_sucursal));
					//RestRequest request = new RestRequest("rest/traspasos/valida_importacion_traspaso", Method.POST);

                    
                    RestClient client = new RestClient(String.Format("http://{0}/", sucursal_local));
                    RestRequest request = new RestRequest("rest/traspasos/valida_importacion_traspaso_sucursal", Method.POST);
					int folio = folio_pair.Key;
					int numero_bultos = folio_pair.Value;

					request.AddParameter("traspaso_id", folio);
					request.AddParameter("numero_bultos", numero_bultos);
                    request.AddParameter("sucursal_ip", sucursal_data.ip_sucursal);//esto es nuevo

					request.Timeout = 5000;
					request.RequestFormat = DataFormat.Json;

					IRestResponse response = client.Execute(request);
					JsonDeserializer json_deserializer = new JsonDeserializer();
					Valida_traspaso_result result = json_deserializer.Deserialize<Valida_traspaso_result>(response);

					if (result.result == false)
					{
						e.Result = String.Format("La sucursal {0} ({1}) ha respondido el siguiente error al validar el folio {2}: {3}", sucursal_data.nombre, sucursal_data.ip_sucursal, folio, result.informacion);
						return;
					}
				}
			}

			//IMPORTANDO TRASPASOS
			worker.ReportProgress(80);

			foreach (KeyValuePair<int, Dictionary<int, int>> sucursal in sucursales)
			{
				int sucursal_id = sucursal.Key;
				Dictionary<int, int> folios = sucursal.Value;

				DTO_Sucursal sucursal_data = dao_sucursales.get_sucursal_data(sucursal_id);

				foreach (KeyValuePair<int, int> folio_pair in folios)
				{
					//RestClient client = new RestClient(String.Format("http://{0}/", sucursal_data.ip_sucursal));
					//RestRequest request = new RestRequest("rest/traspasos/importa_traspaso", Method.POST);

                    RestClient client = new RestClient(String.Format("http://{0}/", sucursal_local));
                    RestRequest request = new RestRequest("rest/traspasos/importa_traspaso_sucursal", Method.POST);
					int folio = folio_pair.Key;
					int numero_bultos = folio_pair.Value;

					request.AddParameter("traspaso_id", folio);
                    request.AddParameter("sucursal_ip", sucursal_data.ip_sucursal);//esto es nuevo
					request.Timeout = 5000;
					request.RequestFormat = DataFormat.Json;

					IRestResponse response = client.Execute(request);
					JsonDeserializer json_deserializer = new JsonDeserializer();
					DTO_Traspaso traspaso_data = new DTO_Traspaso();

					try
					{
						traspaso_data = json_deserializer.Deserialize<DTO_Traspaso>(response);
						dao_traspasos.validar_remote_id(sucursal_id, traspaso_data.traspaso_id);
						dao_traspasos.insertar_traspaso(sucursal_id, traspaso_data);
                        //Aqui tendra una conexion mas para actualiar el campo de fecha recibido
                        //esto es nuevo
                        
                        RestClient cliente = new RestClient(String.Format("http://{0}/", sucursal_local));
                        RestRequest requeste = new RestRequest("rest/traspasos/set_marca_recibido_sucursal", Method.POST);
                        requeste.AddParameter("traspaso_id", folio);
                        requeste.AddParameter("sucursal_ip", sucursal_data.ip_sucursal);//esto es nuevo
                        requeste.Timeout = 5000;
                        requeste.RequestFormat = DataFormat.Json;
                        IRestResponse responses = cliente.Execute(requeste);
                         
                        ///fin de cosas nuevas

					}
					catch(Exception exception)
					{
						e.Result = String.Format("Hubo un error al importar el folio {0}: {1}", folio, exception.Message.ToString());
						return;
					}
				}
			}

			//LISTO
			worker.ReportProgress(100);
			e.Result = "SUCCESS";
		}
		 
		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			switch(e.ProgressPercentage)
			{
				case 30:
					progressbar_form.label_mensaje.Text = "Checando conectividad con sucursales...";
				break;

				case 60:
					progressbar_form.label_mensaje.Text = "Validando traspasos...";
				break;

				case 80:
					progressbar_form.label_mensaje.Text = "Importando traspasos...";
				break;

				case 100:
					progressbar_form.label_mensaje.Text = "Terminado";
				break;

				default:
				break;
			}

			progressbar_form.barra.Value = e.ProgressPercentage;
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				progressbar_form.Close();

				if (e.Result != null)
				{
					if(e.Result.ToString() == "SUCCESS")
					{
						MessageBox.Show(this, "Los traspasos han sido importados con éxito.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						this.Close();
					}
					else
					{
						MessageBox.Show(this, e.Result.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			catch(Exception exception)
			{ 
				Log_error.log(exception); 
			}

			boton_aceptar.Enabled = true;
		}


        public static string reversa(string cadena)
        {

            char[] arr = cadena.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string Decode(string cadena)
        {    

            string[] split_hash = cadena.Split('$');
            string cadena_original = "0";//FORMATO INCOMPATIBLE

            if( split_hash.Length == 5 )
            {
 
                int digito_verificador = Int32.Parse(split_hash[0]);
                int total = Int32.Parse(split_hash[4]);
                int totaldigito = 0;

                if (digito_verificador == (total + 7))
                {
                    string hashoriginal = split_hash[1] + split_hash[2] + split_hash[3]; //
                    for (int i = 0; i < hashoriginal.Length; i++)
                    {
                        string digito = Convert.ToString(hashoriginal[i]);
                        totaldigito = totaldigito + Int32.Parse(digito);
                    }

                    if (totaldigito == total)
                    {
                        cadena_original = split_hash[1] + "$" + split_hash[2] + "$" + split_hash[3];      
                    }
                    else
                    {
                        cadena_original = "-1";//DATOS ALTERADOS
                    }
                }
                else
                {
                    cadena_original = "-1";//DATOS ALTERADOS
                }

            }

            return cadena_original;
   
        }
        

    }
}