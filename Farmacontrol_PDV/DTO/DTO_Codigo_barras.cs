using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacontrol_PDV.DTO
{
	class DTO_Codigo_barras
	{
		private bool		codigo_valido			= false;
		public string		informacion				= "El tipo de código no es válido.";

		public string		codigo_original			{ get; set; }
		private string		tipo					{ get; set; }
		private string[]	parametros;

		public int origen_sucursal_id	{ get; set; }
		public int destino_sucursal_id	{ get; set; }
		public int folio				{ get; set; }
		public int numero_bulto			{ get; set; }
		public int total_bultos			{ get; set; }

		public void Valida_codigo(string codigo, string tipo)
		{
			if(codigo == "")
			{
				informacion = String.Format("{0} no recibió un código para procesar.", this.GetType().Name);
			}
			else
			{
				codigo_original = codigo;

				switch(tipo)
				{
					case "BULTO_TRASPASO":
						Bulto_traspaso();
						break;

					default:
						break;
				}
			}
		}

		private void Bulto_traspaso()
		{
			parametros = codigo_original.Split('$');
			
			if(parametros.Length == 5)
			{
				origen_sucursal_id	= int.Parse(parametros[0].ToString());
				destino_sucursal_id = int.Parse(parametros[1].ToString());
				folio				= int.Parse(parametros[2].ToString());
				numero_bulto		= int.Parse(parametros[3].ToString());
				total_bultos		= int.Parse(parametros[4].ToString());

				codigo_valido = true;
			}
			else
			{
				informacion = String.Format("El código {0} no tiene el número de parámetros correcto.", codigo_original);
			}
		}

		public bool Codigo_valido
		{
			get { return codigo_valido; }
			set { codigo_valido = value; }
		}
	}
}
