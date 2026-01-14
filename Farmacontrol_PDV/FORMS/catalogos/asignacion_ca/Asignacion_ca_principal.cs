using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.catalogos.asignacion_ca
{
    public partial class Asignacion_ca_principal : Form
    {
        long? articulo_asignado_id = null;
        Dictionary<string, long> dic_clase_antibioticos = new Dictionary<string, long>();


        public Asignacion_ca_principal()
        {
            InitializeComponent();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void get_antibioticos()
        {
            DAO_Antibioticos dao = new DAO_Antibioticos();
            var lista_antibioticos = dao.get_all_antibioticos();

            List<string> values_autocomplete = new List<string>();

            foreach(var clase_antibiotico in lista_antibioticos)
            {
                values_autocomplete.Add(clase_antibiotico.nombre);

                if(!dic_clase_antibioticos.ContainsKey(clase_antibiotico.nombre))
                {
                    dic_clase_antibioticos.Add(clase_antibiotico.nombre,clase_antibiotico.clase_antibiotico_id);
                }
            }

            cbb_antibiotico.AutoCompleteCustomSource.AddRange(values_autocomplete.ToArray());
            cbb_antibiotico.AutoCompleteSource = AutoCompleteSource.CustomSource;

            /*
            cbb_antibiotico.DataSource = dao.get_all_antibioticos();
            cbb_antibiotico.DisplayMember = "nombre";
            cbb_antibiotico.ValueMember = "clase_antibiotico_id";
             */
        }

        void busqueda_producto()
        {
            DAO_Articulos dao_articulos = new DAO_Articulos();
            DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);

            if (articulo.Articulo_id != null)
            {
                if (articulo.activo)
                {
                    if(articulo.clase_antibiotico_id == null)
                    {
                        articulo_asignado_id = articulo.Articulo_id;
                        txt_amecop.Text = articulo.Amecop;
                        txt_nombre.Text = articulo.Nombre;
                        cbb_antibiotico.Focus();
                    }
                    else
                    {
                        txt_amecop.Text = "";
                        MessageBox.Show(this, "Este producto ya ha sido clasificado, si necesita reclasificar notifique a informatica.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_amecop.Focus();
                    }
                }
                else
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "El producto se encuentra en el catalgo pero esta marcado como inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_amecop.Focus();
                }
            }
            else
            {
                txt_amecop.Text = "";
                MessageBox.Show(this, "Producto No encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:
                    if (txt_amecop.Text.Trim().Length > 0)
                    {
                        txt_amecop.Text = "";
                        txt_nombre.Text = "";
                        articulo_asignado_id = null;
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
                case 13:
                    if (txt_amecop.TextLength > 0)
                    {
                        busqueda_producto();
                    }
                    break;
            }
        }

        private void cbb_antibiotico_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch(keycode)
            {
                case 27:
                    cbb_antibiotico.Text = "";
                    txt_amecop.Focus();
                    txt_amecop.SelectAll();
                break;
                case 13:
                btn_aceptar.Focus();
                break;
            }
        }

        private void Asignacion_ca_principal_Load(object sender, EventArgs e)
        {
            get_antibioticos();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            if(dic_clase_antibioticos.ContainsKey(cbb_antibiotico.Text.Trim()))
            {
                if(articulo_asignado_id != null)
                {
                    DAO_Articulos dao = new DAO_Articulos();
                    if(dao.actualizar_articulo_clase_antibiotico((long)articulo_asignado_id,dic_clase_antibioticos[cbb_antibiotico.Text.Trim()]))
                    {
                        MessageBox.Show(this, "Asignacion exitosa", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiar_informacion();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un error al intentar actualizar la informacion del producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "No hay un producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_amecop.Focus();
                    txt_amecop.SelectAll();
                }
            }
            else
            {
                MessageBox.Show(this,"No se selecciono una clase de antibiotico valida.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbb_antibiotico.Focus();
            }
        }

        void limpiar_informacion()
        {
            articulo_asignado_id = null;
            txt_amecop.Text = "";
            txt_nombre.Text = "";
            cbb_antibiotico.Text = "";
            txt_amecop.Focus();
        }
    }
}
