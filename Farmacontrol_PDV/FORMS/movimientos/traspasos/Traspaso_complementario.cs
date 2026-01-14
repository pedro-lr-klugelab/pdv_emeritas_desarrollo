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

namespace Farmacontrol_PDV.FORMS.movimientos.traspasos
{
	public partial class Traspaso_complementario : Form
	{
		private long traspaso_id;
		DAO_Traspasos dao_traspasos = new DAO_Traspasos();
		DTO_Traspaso dto_traspaso = new DTO_Traspaso();
		bool tengo_suficiente_existencias = true;

		public Traspaso_complementario(long traspaso_id)
		{
			this.traspaso_id = traspaso_id;

			InitializeComponent();

			dto_traspaso = dao_traspasos.get_informacion_traspaso(traspaso_id);
		}

		private void Traspaso_complementario_Shown(object sender, EventArgs e)
		{
			if (dto_traspaso.tipo.Equals("ENVIAR"))
			{
				DAO_Sucursales dao_sucursales = new DAO_Sucursales();
				var sucursal_data = dao_sucursales.get_sucursal_data(dto_traspaso.sucursal_id);

				if (Red_helper.checa_online(sucursal_data.ip_sucursal))
				{
					dgv_traspaso_complementario.DataSource = dao_traspasos.get_traspaso_complementario_enviar((long)dto_traspaso.traspaso_id,(long)dto_traspaso.sucursal_id);	
					validar_existencias();
				}
				else
				{
					MessageBox.Show(this, "La sucursal no responde, intentelo mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}
			else
			{
				dgv_traspaso_complementario.DataSource = dao_traspasos.get_traspaso_complementario(traspaso_id);
				
				if(dgv_traspaso_complementario.Rows.Count == 0)
				{
					MessageBox.Show(this, "No tienes ningun producto para enviar virtualmente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}
			}

			dgv_traspaso_complementario.ClearSelection();
		}

		void validar_existencias()
		{
			DAO_Articulos dao_articulos = new DAO_Articulos();
			DataTable detallado = dgv_traspaso_complementario.DataSource as DataTable;

			tengo_suficiente_existencias = true;

			foreach(DataGridViewRow row in dgv_traspaso_complementario.Rows)
			{
				string amecop = row.Cells["c_amecop"].Value.ToString();
				string caducidad = row.Cells["c_caducidad"].Value.ToString();
				string lote = row.Cells["c_lote"].Value.ToString();
				long cantidad_necesaria = (Convert.ToInt64(row.Cells["c_cantidad"].Value) - Convert.ToInt64(row.Cells["c_cantidad_origen"].Value));
				long existencia_vendible = dao_articulos.get_existencia_vendible(amecop,caducidad,lote);

				if(existencia_vendible < cantidad_necesaria)
				{
					dgv_traspaso_complementario.Rows[row.Index].DefaultCellStyle.BackColor = Color.FromArgb(255, 218, 218);
					tengo_suficiente_existencias = false;
				}
				else
				{
					dgv_traspaso_complementario.Rows[row.Index].DefaultCellStyle.BackColor = Color.Empty;
				}
			}
		}

		private void btn_terminar_traspaso_Click(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this,"Estas a punto de enviar el traspaso, ¿Deseas continuar?","Traspaso Complementario",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

			if(dr == DialogResult.Yes)
			{
				if(dto_traspaso.tipo.Equals("RECIBIR"))
				{
					long traspaso_id_complementario = dao_traspasos.crear_traspaso_complemetario(traspaso_id);

					if (traspaso_id_complementario > 0)
					{
						MessageBox.Show(this, "Traspaso complementario generado correctamente", "Traspaso complementario", MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.Close();
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al intentar generar el traspaso complementario, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}	
				}
				else
				{
					validar_existencias();
					if(tengo_suficiente_existencias)
					{
						DataTable detallado = dgv_traspaso_complementario.DataSource as DataTable;
						List<DTO_Traspaso_complementario> detallado_complementario = new List<DTO_Traspaso_complementario>();


						foreach (DataRow row in detallado.Rows)
						{
							DTO_Traspaso_complementario com = new DTO_Traspaso_complementario();
							com.detallado_traspaso_id = 0;
							com.amecop = row["amecop"].ToString();
							com.problema = row["producto"].ToString();
							com.caducidad = row["caducidad"].ToString();
							com.lote = row["lote"].ToString();
							com.cantidad_origen = Convert.ToInt64(row["cantidad_origen"]);
							com.cantidad = Convert.ToInt64(row["cantidad"]);
							com.problema = row["problema"].ToString();
							com.solucion = row["solucion"].ToString();

							detallado_complementario.Add(com);
						}

						long traspaso_id_complementario = dao_traspasos.crear_traspaso_complementario_enviar(traspaso_id, detallado_complementario);

						if (traspaso_id_complementario > 0)
						{
							MessageBox.Show(this, "Traspaso complementario generado correctamente", "Traspaso complementario", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.Close();
						}
						else
						{
							MessageBox.Show(this, "Ocurrio un error al intentar generar el traspaso complementario, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}	
					}
					else
					{
						MessageBox.Show(this,"Los productos en rojo indican que no cuentas con la existencia para enviar el traspaso complementario","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				}
			}
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void dgv_traspaso_complementario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			
		}
	}
}
