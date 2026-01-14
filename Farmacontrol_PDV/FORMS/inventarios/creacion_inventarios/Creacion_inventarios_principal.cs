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
using System.Threading;
using System.Threading.Tasks;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.creacion_inventarios
{
	public partial class Creacion_inventarios_principal : Form
	{
		DAO_Inventarios dao_inventarios = new DAO_Inventarios();
		DTO_Inventario dto_inventario = new DTO_Inventario();
		System.Threading.Timer _timer = null;
		private bool no_inventariados_check = false;
		private bool diferencias_ckeck = false;

		BindingList<DTO_Inventario_folio_jornada> data = new BindingList<DTO_Inventario_folio_jornada>();

		public Creacion_inventarios_principal()
		{
			Creacion_inventarios_principal.CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			dgv_inventarios_folios.DataSource = data;
		}

		public void TimerWork(object obj)
		{
			if(dto_inventario.inventario_id > 0)
			{
				if (dto_inventario.fecha_fin.Equals(null))
				{
					DAO_Inventarios dao_in = new DAO_Inventarios();
					var result = dao_in.get_inventarios_folios(dto_inventario.inventario_id);

					foreach (DTO_Inventario_folio_jornada folio_grid in data)
					{
						foreach (DTO_Inventario_folio_jornada folio in result)
						{
							if (folio.inventario_folio_id == folio_grid.inventario_folio_id)
							{
								folio_grid.estado = folio.estado;
								folio_grid.terminal = folio.terminal;
								folio_grid.comentarios = folio.comentarios;
								break;
							}
						}
					}

					dgv_inventarios_folios.Refresh();

					dgv_inventarios_folios.ClearSelection();
				}	
			}
		}

		private void Creacion_inventarios_principal_Shown(object sender, EventArgs e)
		{
			long inventario_id = dao_inventarios.get_inventario_fin();
			_timer = new System.Threading.Timer(TimerWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

			if(inventario_id > 0)
			{
				lbl_mensaje_bloqueo.Parent = null;
				lbl_mensaje_bloqueo.Text = "";
				lbl_mensaje_bloqueo.Visible = false;
				rellenar_informacion_inventario(inventario_id);
			}
			else
			{
				lbl_mensaje_bloqueo.Parent = dgv_inventarios_folios;
				lbl_mensaje_bloqueo.Text = "No se encontraron inventarios anteriores";
				lbl_mensaje_bloqueo.Visible = true;

				txt_comentarios.Text = "";
				txt_comentarios.Enabled = false;
				btn_agregar.Enabled = false;
			}
		}

		public void rellenar_informacion_inventario(long inventario_id)
		{
			txt_comentarios.Enabled = true;
			btn_agregar.Enabled = true;

			dto_inventario = dao_inventarios.get_informacion_inventario(inventario_id);

			txt_empleado_captura.Text = dto_inventario.nombre_empleado_captura;
			txt_empleado_termina.Text = dto_inventario.nombre_empleado_termina;
            txt_fecha_creado.Text = (dto_inventario.fecha_inicio != null) ? Misc_helper.fecha(dto_inventario.fecha_inicio.ToString(), "LEGIBLE") : " - ";
            txt_fecha_terminado.Text = (dto_inventario.fecha_fin != null) ? Misc_helper.fecha(dto_inventario.fecha_fin.ToString(), "LEGIBLE") : " - ";
			txt_folio_busqueda_traspaso.Text = "" + dto_inventario.inventario_id;

			txt_estado.Text = (dto_inventario.fecha_fin.Equals(null)) ? "ABIERTO" : "CERRADO";
			txt_estado.BackColor = (dto_inventario.fecha_fin.Equals(null)) ? Color.Green : Color.Red;

			btn_iniciar_detener.Text = (dto_inventario.aceptando_capturas) ? "Detener captura" : "Iniciar captura" ;

			
			var result = dao_inventarios.get_inventarios_folios(dto_inventario.inventario_id);

			data.Clear();

			foreach(DTO_Inventario_folio_jornada folio in result)
			{
				data.Add(folio);
			}

			dgv_inventarios_folios.ClearSelection();
			

			btn_iniciar_detener.Enabled = (dto_inventario.fecha_fin.Equals(null)) ? true : false;

			validar_bloqueo();
		}

		public void validar_bloqueo()
		{
			if(dto_inventario.fecha_fin.Equals(null))
			{
				btn_agregar.Enabled = true;
				txt_comentarios.Enabled = true;
			}
			else
			{
				btn_agregar.Enabled = false;
				txt_comentarios.Enabled = false;
			}
		}

		private void btn_inicio_Click(object sender, EventArgs e)
		{
			long inventario_id = dao_inventarios.get_inventario_inicio();

			if(inventario_id > 0)
			{
				rellenar_informacion_inventario(inventario_id);
			}
		}

		private void btn_atras_Click(object sender, EventArgs e)
		{
			long inventario_id = dao_inventarios.get_inventario_atras(dto_inventario.inventario_id);

			if (inventario_id > 0)
			{
				rellenar_informacion_inventario(inventario_id);
			}
		}

		private void btn_siguiente_Click(object sender, EventArgs e)
		{
			long inventario_id = dao_inventarios.get_inventario_siguiente(dto_inventario.inventario_id);

			if (inventario_id > 0)
			{
				rellenar_informacion_inventario(inventario_id);
			}
		}

		private void btn_fin_Click(object sender, EventArgs e)
		{
			long inventario_id = dao_inventarios.get_inventario_fin();

			if (inventario_id > 0)
			{
				rellenar_informacion_inventario(inventario_id);
			}
		}

		private void dgv_inventarios_folios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if(dgv_inventarios_folios.Rows.Count > 0)
			{
				switch (e.ColumnIndex)
				{
					case 1:
						if (e.Value.ToString().Equals("SIN TERMINAL"))
						{
							e.CellStyle.BackColor = Color.FromArgb(255, 218, 218);
						}
						break;
					case 3:
						if (e.Value.ToString().Equals("CAPTURANDO"))
						{
							e.CellStyle.BackColor = Color.FromArgb(251, 249, 203);
						}
						else
						{
							e.CellStyle.BackColor = Color.FromArgb(255, 218, 218);
						}
						break;
				}
			}
		}

		private void txt_comentarios_Leave(object sender, EventArgs e)
		{
			dao_inventarios.set_comentario(dto_inventario.inventario_id, txt_comentarios.Text);
		}

		private void Creacion_inventarios_principal_FormClosed(object sender, FormClosedEventArgs e)
		{
			_timer.Dispose();	
		}

		private void dgv_inventarios_folios_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Log_error.log(e.Exception);
		}

		private void crear_jornada_inventario_Click(object sender, EventArgs e)
		{
			Login_form login = new Login_form("crear_inventario");
			login.ShowDialog();

			if(login.empleado_id != null)
			{
				DialogResult dr = MessageBox.Show(this,"¿Estas seguro de crear una nueva jornada de inventario?","Inventario",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if(dr == DialogResult.Yes)
				{
					var dto_validacion = dao_inventarios.crear_jornada_inventario((long)login.empleado_id);

					if(dto_validacion.status)
					{
						MessageBox.Show(this,dto_validacion.informacion,"Inventario",MessageBoxButtons.OK,MessageBoxIcon.Information);
						long inventario_id = dao_inventarios.get_inventario_fin();

						if (inventario_id > 0)
						{
							rellenar_informacion_inventario(inventario_id);
						}
					}
					else
					{
						MessageBox.Show(this, dto_validacion.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void terminar_jornada_inventario_Click(object sender, EventArgs e)
		{
			if(dto_inventario.inventario_id > 0)
			{

				if (dto_inventario.fecha_fin.Equals(null))
				{
					if (!dao_inventarios.get_aceptando_capturas(dto_inventario.inventario_id))
					{
						Informacion_general_inventario info = new Informacion_general_inventario(no_inventariados_check, diferencias_ckeck, dto_inventario.inventario_id, true);
						info.ShowDialog();
						if (info.inventario_terminado)
						{
							rellenar_informacion_inventario(dto_inventario.inventario_id);
						}
						else
						{
							no_inventariados_check = info.no_inventariados_check;
							diferencias_ckeck = info.diferencias_check;
						}
					}
					else
					{
						MessageBox.Show(this, "Es necesario detener la captura del inventario para poder terminarlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void btn_agregar_Click(object sender, EventArgs e)
		{
			if(dto_inventario.fecha_fin.Equals(null))
			{
				Login_form login = new Login_form("crear_inventario_folio");
				login.ShowDialog();

				if(login.empleado_id != null)
				{
					DialogResult dr = MessageBox.Show(this,"Estas a punto de crear un nuevo folio de captura, ¿Deseas continuar?","Crear Folio Captura",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

					if(dr == DialogResult.Yes)
					{
						var val = dao_inventarios.crear_inventario_folio(dto_inventario.inventario_id,(long)login.empleado_id);

						if(val.status)
						{
							MessageBox.Show(this,val.informacion,"Folio de captura",MessageBoxButtons.OK,MessageBoxIcon.Information);
							rellenar_informacion_inventario(dto_inventario.inventario_id);
						}
						else
						{
							MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		private void btn_iniciar_detener_Click(object sender, EventArgs e)
		{
			if(dto_inventario.inventario_id > 0)
			{

				if (btn_iniciar_detener.Text.Equals("Iniciar captura"))
				{
					if (dao_inventarios.set_aceptando_capturas(dto_inventario.inventario_id, true) > 0)
					{
						rellenar_informacion_inventario(dto_inventario.inventario_id);
						MessageBox.Show(this, "Se ha iniciado la captura de inventarios correctamente", "Inicio de capturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al intentar iniciar la captura de inventarios, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					if (dao_inventarios.set_aceptando_capturas(dto_inventario.inventario_id, false) > 0)
					{
						rellenar_informacion_inventario(dto_inventario.inventario_id);
						MessageBox.Show(this, "Se ha detenido la captura de inventarios correctamente", "Inicio de capturas", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show(this, "Ocurrio un error al intentar detener la captura de inventarios, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}	
			}
		}

		private void informacion_general_inventario_Click(object sender, EventArgs e)
		{
			if(dto_inventario.inventario_id > 0)
			{
				if (!dao_inventarios.get_aceptando_capturas(dto_inventario.inventario_id))
				{
					Informacion_general_inventario info = new Informacion_general_inventario(no_inventariados_check, diferencias_ckeck, dto_inventario.inventario_id);
					info.ShowDialog();
					no_inventariados_check = info.no_inventariados_check;
					diferencias_ckeck = info.diferencias_check;
				}
				else
				{
					MessageBox.Show(this, "Es necesario detener la captura del inventario para poder mostrarar la informacion solicitada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void Creacion_inventarios_principal_FormClosing(object sender, FormClosingEventArgs e)
		{
			_timer.Dispose();
		}

		private void buscar_jornada_inventario_Click(object sender, EventArgs e)
		{

		}

		private void txt_folio_busqueda_traspaso_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

		private void txt_folio_busqueda_traspaso_KeyDown(object sender, KeyEventArgs e)
		{
			var keycode = Convert.ToInt32(e.KeyCode);

			switch(keycode)
			{
				case 13:
					long inventario_id = Convert.ToInt64(txt_folio_busqueda_traspaso.Text.Trim());

					var inventario_data = dao_inventarios.get_informacion_inventario(inventario_id);

					if(inventario_data.inventario_id > 0)
					{
						dto_inventario = inventario_data;
						rellenar_informacion_inventario(inventario_data.inventario_id);
					}
				break;
			}
		}
	}
}
