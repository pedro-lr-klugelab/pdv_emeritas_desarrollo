using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using System.Net;
using System.IO;
using System.IO.IsolatedStorage;


namespace Farmacontrol_PDV.FORMS.reportes.ceros
{
    public partial class Ceros_principal : Form
    {
        public int sucursal_id = 0;
        public string file;

        public Ceros_principal()
        {
            InitializeComponent();
            //dgv_reporte_ceros.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            
            dgv_reporte_ceros.SelectionMode = DataGridViewSelectionMode.CellSelect;
          //  dgv_reporte_ceros.MultiSelect = false;
            dgv_reporte_ceros.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            sucursal_id = Convert.ToInt32(Config_helper.get_config_local("sucursal_id"));
            get_reportes_disponibles();
        }

        private void get_reportes_disponibles()
        {
            DAO_Ceros dao_ceros = new DAO_Ceros();
            get_ceros_modificado();
          //  dgv_reportes.DataSource = dao_ceros.get_reportes_disponibles(sucursal_id);
           // dgv_reportes.ClearSelection();
           // dgv_reportes.Focus();
        }
        /*
        private void dgv_reportes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgv_reportes.Rows.Count > 0)
                {
                    if (dgv_reportes.SelectedRows.Count > 0)
                    {
                        long reporte_faltantes_id = 0;

                        var row = dgv_reportes.SelectedRows[0];

                        reporte_faltantes_id = Convert.ToInt64(row.Cells["reporte_faltantes_id"].Value);
                        DAO_Sucursales dao_sucursales = new DAO_Sucursales();
                        DTO_Sucursal dto_sucursal = dao_sucursales.get_sucursal_data(sucursal_id);

                        Cursor.Current = Cursors.WaitCursor;

                        var reporte_Ceros = DAO_Ceros.get_reporte_ceros(reporte_faltantes_id, dto_sucursal.etiqueta);
                        

                        Cursor.Current = Cursors.Default;

                        dgv_reporte_ceros.DataSource = reporte_Ceros;
                        dgv_reporte_ceros.ClearSelection();
                        
                    }
                    else
                    {
                        MessageBox.Show(this, "Debe seleccionar un reporte", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "No hay reportes disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        */
        public void get_ceros_modificado()
        {
            
                DAO_Sucursales dao_sucursales = new DAO_Sucursales();
                DTO_Sucursal dto_sucursal = dao_sucursales.get_sucursal_data(sucursal_id);

                Cursor.Current = Cursors.WaitCursor;

                var reporte_Ceros = DAO_Ceros.get_reporte_ceros( dto_sucursal.etiqueta);

                Cursor.Current = Cursors.Default;

                dgv_reporte_ceros.DataSource = reporte_Ceros;
                dgv_reporte_ceros.ClearSelection();
        
            
        }
    }
}
