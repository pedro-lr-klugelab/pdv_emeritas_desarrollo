using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Configuration;
using PdfSharp;
using System.Drawing.Imaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing.Drawing2D;
using WIA;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System.Reflection;

namespace Farmacontrol_PDV.FORMS.opciones.configuracion
{
	public partial class Configuracion_principal : Form
	{
		public Configuracion_principal()
		{
			InitializeComponent();
			get_impresoras();
			get_impresion_remota();
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;

            lblVersion.Text = "Version " + ver.ToString();

			try
			{
				get_escaner();
			}
			catch(Exception e)
			{
				Log_error.log(e);
			}
		}

		public void get_impresion_remota()
		{
			DAO_Terminales dao_terminales = new DAO_Terminales();
			var terminales = dao_terminales.get_all_terminales();

			List<DTO_Terminal> lista_terminales_tickets = new List<DTO_Terminal>();
			List<DTO_Terminal> lista_terminales_etiquetas = new List<DTO_Terminal>();

			foreach(DTO_Terminal terminal in terminales)
			{
				if(terminal.permitir_impresion_remota)
				{
					lista_terminales_tickets.Add(terminal);
					lista_terminales_etiquetas.Add(terminal);
				}
			}

			cbb_impresora_remota_tickets.DataSource = lista_terminales_tickets;
			cbb_impresora_remota_tickets.DisplayMember = "nombre";
			cbb_impresora_remota_tickets.ValueMember = "direccion_ip";

			chb_usar_impresora_remota_tickets.Checked = (Properties.Configuracion.Default.usar_impresora_remota_tickets == "0") ? false : true;
			cbb_impresora_remota_tickets.SelectedValue = Properties.Configuracion.Default.impresora_remota_tickets;

			cbb_impresora_remota_etiquetas.DataSource = lista_terminales_etiquetas;
			cbb_impresora_remota_etiquetas.DisplayMember = "nombre";
			cbb_impresora_remota_etiquetas.ValueMember = "direccion_ip";

			chb_usar_impresora_remota_etiquetas.Checked = (Properties.Configuracion.Default.usar_impresora_remota_etiquetas == "0") ? false : true;
			cbb_impresora_remota_etiquetas.SelectedValue = Properties.Configuracion.Default.impresora_remota_etiquetas;
		}

		public void get_impresoras()
		{
            try
            {
                PrintDocument impresoras = new PrintDocument();
                string impresora_default = impresoras.PrinterSettings.PrinterName;

                foreach (String nombre_impresora in PrinterSettings.InstalledPrinters)
                {
                    cbb_etiquetas.Items.Add(nombre_impresora);
                    cbb_tickets.Items.Add(nombre_impresora);
                }

                string impresora_tickets = Properties.Configuracion.Default.impresora_tickets;
                cbb_tickets.SelectedIndex = cbb_tickets.Items.IndexOf(impresora_tickets);

                string impresora_etiquetas = Properties.Configuracion.Default.impresora_etiquetas;
                cbb_etiquetas.SelectedIndex = cbb_etiquetas.Items.IndexOf(Properties.Configuracion.Default.impresora_etiquetas);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
		}

		public void get_escaner()
		{
			var deviceManager = new DeviceManager();

			Dictionary<string, string> contenido = new Dictionary<string, string>();

			if (Properties.Configuracion.Default.impresora_escaner.Equals(""))
			{
				contenido.Add("* Seleccinar escaner *", "");
			}

			for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
			{
				if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
				{
					continue;
				}

				contenido.Add(deviceManager.DeviceInfos[i].Properties["Name"].get_Value().ToString(), deviceManager.DeviceInfos[i].Properties["Name"].get_Value().ToString());
			}

			if (!Properties.Configuracion.Default.impresora_escaner.Equals(""))
			{
				if(contenido.Count == 0)
				{
					contenido.Add("* Seleccinar escaner *", "");
				}
			}

			BindingSource source = new BindingSource(contenido, null);
			cbb_escaner.DataSource = source;
			cbb_escaner.DisplayMember = "Key";
			cbb_escaner.ValueMember = "Value";

			cbb_escaner.SelectedValue = Properties.Configuracion.Default.impresora_escaner;
		}

		private void btn_cancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_guardar_cambios_Click(object sender, EventArgs e)
		{
			var value_etiquetas = cbb_etiquetas.SelectedItem;
			var value_tickets = cbb_tickets.SelectedItem;
			var value_escaner = cbb_escaner.SelectedValue;
			var value_impresora_remota_tickets = cbb_impresora_remota_tickets.SelectedValue;
			var value_impresora_remota_etiquetas = cbb_impresora_remota_etiquetas.SelectedValue;

			if(value_etiquetas != null)
			{
				Properties.Configuracion.Default.impresora_etiquetas = value_etiquetas.ToString();
			}

			if(value_tickets != null)
			{
				Properties.Configuracion.Default.impresora_tickets = value_tickets.ToString();
			}

			if(value_escaner != null)
			{
				Properties.Configuracion.Default.impresora_escaner = cbb_escaner.SelectedValue.ToString();
			}

			if(value_impresora_remota_tickets != null)
			{
				Properties.Configuracion.Default.impresora_remota_tickets = cbb_impresora_remota_tickets.SelectedValue.ToString();
			}

			if (value_impresora_remota_etiquetas != null)
			{
				Properties.Configuracion.Default.impresora_remota_etiquetas = cbb_impresora_remota_etiquetas.SelectedValue.ToString();
			}

			Properties.Configuracion.Default.usar_impresora_remota_tickets = (chb_usar_impresora_remota_tickets.Checked) ? "1" : "0";
			Properties.Configuracion.Default.usar_impresora_remota_etiquetas = (chb_usar_impresora_remota_etiquetas.Checked) ? "1" : "0";

			Properties.Configuracion.Default.Save();

			MessageBox.Show(this,"Los cambios fueron guardados correctamente","Configuracion",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		private void btn_print_Click(object sender, EventArgs e)
		{
			
		}

        private void chb_permitir_impresion_remota_CheckedChanged(object sender, EventArgs e)
        {
            DAO_Terminales dao_terminales = new DAO_Terminales();
            dao_terminales.set_permitir_impresion_remota(chb_permitir_impresion_remota.Checked);
        }
	}
}
