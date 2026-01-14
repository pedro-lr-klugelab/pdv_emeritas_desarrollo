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

namespace Farmacontrol_PDV.FORMS.ventas.ventas
{
	public partial class Importar_traspaso_venta : Form
	{
		DAO_Traspasos dao_traspasos = new DAO_Traspasos();
		DTO_Validacion dto_validacion = new DTO_Validacion();
		DTO_Traspaso dto_traspaso = new DTO_Traspaso();
		public object cast_dto_traspaso; 

		public string hash = "";

		public Importar_traspaso_venta()
		{
			InitializeComponent();
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public bool validar_cadena_entrada()
		{
			string[] split_hash = txt_folio.Text.Split('$');

			if(split_hash[0].Equals("TV"))
			{
				if(split_hash.Length == 4)
				{
					if(split_hash[3].Length == 32)
					{
						return true;
					}
				}
			}
			
			return false;
		}

		public void importar_traspaso_venta()
		{
			string[] split_hash = txt_folio.Text.Split('$');

			string hash_reconstruido = string.Format("{0}${1}${2}${3}", split_hash[0],split_hash[1],split_hash[2],Misc_helper.uuid_guiones(split_hash[3]));

			var result = dao_traspasos.importar_traspaso_para_venta(hash_reconstruido);

			dto_validacion = result.Item1;

			if (dto_validacion.status)
			{
				hash = hash_reconstruido;
				dto_traspaso = result.Item2;
				cast_dto_traspaso = result.Item2;
				this.Close();
			}
			else
			{
				MessageBox.Show(this,"Traspaso para venta no encontrado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txt_folio.SelectAll();
				txt_folio.Focus();
			}
		}

		private void txt_folio_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					if(txt_folio.Text.Trim().Length > 0)
					{
						bool cadena_valida = validar_cadena_entrada();

						if(cadena_valida)
						{
							if(!dao_traspasos.existe_traspaso_venta(txt_folio.Text))
							{
								importar_traspaso_venta();
							}
							else
							{
								MessageBox.Show(this, "Este folio ya ha sido usado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								txt_folio.SelectAll();
								txt_folio.Focus();
							}
						}
						else
						{
							MessageBox.Show(this,"Formato de folio incorrecto","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				break;
				case 27:
					if(txt_folio.TextLength > 0)
					{
						txt_folio.Text = "";
					}
					else
					{
						this.Close();
					}
				break;
			}
		}
	}
}
