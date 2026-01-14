using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.HELPERS;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;

namespace Farmacontrol_PDV.FORMS.inventarios.ajuste_caducidades_lotes
{
    public partial class Ajuste_caducidades_lotes_principal : Form
    {
        private int articulo_id;
        DAO_Articulos dao_articulos = new DAO_Articulos();
        DTO_Articulo dto_articulo;

        public Ajuste_caducidades_lotes_principal()
        {
            InitializeComponent();
        }

        private void Ajuste_caducidades_lotes_principal_Shown(object sender, EventArgs e)
        {
            txt_amecop.Focus();
        }

        public void get_caducidades_de(DTO.DTO_Articulo articulo)
        {
            if (articulo.Caducidades.Rows.Count > 0)
            {
                cbb_caducidad_de.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_caducidad_de.DisplayMember = "Key";
                cbb_caducidad_de.ValueMember = "Value";

                Dictionary<string, string> source_caducidad_de = new Dictionary<string, string>();

                foreach (DataRow row in articulo.Caducidades.Rows)
                {
                    string fecha_caducidad = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
                    string fecha_value = Misc_helper.fecha(row["caducidad"].ToString());

                    if (!source_caducidad_de.ContainsKey(fecha_caducidad))
                    {
                        source_caducidad_de.Add(fecha_caducidad, fecha_value);
                    }
                }

                cbb_caducidad_de.DataSource = new BindingSource(source_caducidad_de, null);
                cbb_caducidad_de.DisplayMember = "Key";
                cbb_caducidad_de.ValueMember = "Value";

                cbb_caducidad_de.Enabled = true;
                cbb_caducidad_de.DroppedDown = true;
                cbb_caducidad_de.Focus();
            }
        }

        public void get_caducidades_a(DTO.DTO_Articulo articulo)
        {
            Dictionary<string, string> source_caducidades_a = new Dictionary<string, string>();

            if (articulo.Caducidades.Rows.Count > 0)
            {
                cbb_caducidad_a.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_caducidad_a.DisplayMember = "Key";
                cbb_caducidad_a.ValueMember = "Value";

                foreach (DataRow row in articulo.Caducidades.Rows)
                {
                    string fecha_key = Misc_helper.fecha(row["caducidad"].ToString(), "CADUCIDAD") + " (" + row["existencia"] + ")";
                    string fecha_value = Misc_helper.fecha(row["caducidad"].ToString());

                    if (!source_caducidades_a.ContainsKey(fecha_key))
                    {
                        source_caducidades_a.Add(fecha_key, fecha_value);
                    }
                }
            }

            if (!source_caducidades_a.ContainsKey("SIN CAD"))
            {
                source_caducidades_a.Add("SIN CAD", "0000-00-00");
            }

            source_caducidades_a.Add("OTRO", "OTRO");

            cbb_caducidad_a.DataSource = new BindingSource(source_caducidades_a, null);
            cbb_caducidad_a.DisplayMember = "Key";
            cbb_caducidad_a.ValueMember = "Value";

            cbb_caducidad_a.Enabled = true;
            cbb_caducidad_a.DroppedDown = true;

            cbb_caducidad_a.Focus();
        }

        private void cbb_caducidad_KeyDown_de(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    busqueda_lotes_de();
                    break;
                case 27:
                    txt_amecop.Enabled = true;
                    txt_amecop.Focus();
                    txt_amecop.SelectAll();
                    txt_producto.Text = "";
                    cbb_caducidad_de.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                    cbb_caducidad_de.DisplayMember = "Key";
                    cbb_caducidad_de.ValueMember = "";

                    cbb_caducidad_de.Enabled = false;
                    break;
            }
        }

        private void cbb_caducidad_KeyDown_a(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (cbb_caducidad_a.SelectedValue.ToString().Equals("OTRO"))
                    {
                        cbb_mes.Enabled = true;
                        cbb_mes.DroppedDown = true;
                        cbb_mes.Focus();

                        cbb_caducidad_a.Enabled = false;
                    }
                    else
                    {
                        busqueda_lotes_a();
                        cbb_caducidad_a.Enabled = false;
                    }
                    break;
                case 27:
                    nup_cantidad_de.Enabled = true;
                    nup_cantidad_de.Focus();

                    //cbb_caducidad_a.Items.Clear();
                    cbb_caducidad_a.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                    cbb_caducidad_a.DisplayMember = "Key";
                    cbb_caducidad_a.ValueMember = "Value";

                    cbb_caducidad_a.Enabled = false;
                    break;
            }
        }

        public void cargar_mes()
        {
            cbb_mes.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
            cbb_mes.DisplayMember = "Key";
            cbb_mes.ValueMember = "Value";

            Dictionary<string, string> meses = new Dictionary<string, string>();

            meses.Add("ENE", "01-01");
            meses.Add("FEB", "02-01");
            meses.Add("MAR", "03-01");
            meses.Add("ABR", "04-01");
            meses.Add("MAY", "05-01");
            meses.Add("JUN", "06-01");
            meses.Add("JUL", "07-01");
            meses.Add("AGO", "08-01");
            meses.Add("SEP", "09-01");
            meses.Add("OCT", "10-01");
            meses.Add("NOV", "11-01");
            meses.Add("DIC", "12-01");

            cbb_mes.DataSource = new BindingSource(meses, null);
            cbb_mes.DisplayMember = "Key";
            cbb_mes.ValueMember = "Value";
        }

        public void cargar_anios()
        {
            try
            {
                cbb_anio.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_anio.DisplayMember = "Key";
                cbb_anio.ValueMember = "Value";

                Dictionary<string, string> anios = new Dictionary<string, string>();

                int anio = Convert.ToDateTime(Misc_helper.fecha()).Year;

                for (int i = (anio - 1); i < (anio + 15); i++)
                {
                    anios.Add(i.ToString(), i.ToString());
                }

                cbb_anio.DataSource = new BindingSource(anios, null);
                cbb_anio.DisplayMember = "Key";
                cbb_anio.ValueMember = "Value";
            }
            catch (Exception e)
            {
                Log_error.log(e);
            }
        }

        private void cbb_caducidad_Enter(object sender, EventArgs e)
        {
            cargar_mes();
            cargar_anios();
        }

        public void busqueda_lotes_de()
        {
            DataTable result_lotes = dao_articulos.get_lotes((int)dto_articulo.Articulo_id, cbb_caducidad_de.SelectedValue.ToString());

            cbb_lote_de.Enabled = true;

            if (result_lotes.Rows.Count > 0)
            {
                cbb_lote_de.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_lote_de.DisplayMember = "Key";
                cbb_lote_de.ValueMember = "Value";

                Dictionary<string, string> source_lote_de = new Dictionary<string, string>();

                foreach (DataRow row in result_lotes.Rows)
                {
                    string lote_key = row["lote"].ToString().Trim().Equals(" ") || row["lote"].ToString().Trim().Equals("") ? "SIN LOTE" : row["lote"].ToString().Trim();
                    string lote_Value = row["lote"].ToString().Trim();

                    if (!source_lote_de.ContainsKey(lote_key))
                    {
                        source_lote_de.Add(lote_key, lote_Value);
                    }
                }

                cbb_lote_de.DataSource = new BindingSource(source_lote_de, null);
                cbb_lote_de.DisplayMember = "Key";
                cbb_lote_de.ValueMember = "Value";

                cbb_lote_de.DroppedDown = true;
                cbb_lote_de.Focus();
                cbb_caducidad_de.Enabled = false;
            }
        }

        public void busqueda_lotes_a()
        {
            DataTable result_lotes = dao_articulos.get_lotes((int)dto_articulo.Articulo_id,
                (cbb_caducidad_a.SelectedValue.ToString().Equals("OTRO")) ? string.Format("{0}-{1}", cbb_mes.SelectedItem, cbb_anio.SelectedItem) : cbb_caducidad_a.SelectedValue.ToString());
            cbb_lote_a.Enabled = true;

            Dictionary<string, string> source_lote_a = new Dictionary<string, string>();

            if (result_lotes.Rows.Count > 0)
            {
                cbb_lote_a.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_lote_a.DisplayMember = "Key";
                cbb_lote_a.ValueMember = "Value";

                foreach (DataRow row in result_lotes.Rows)
                {
                    string lote_key = (row["lote"].ToString().Equals(" ")) ? "SIN LOTE" : row["lote"].ToString();
                    string lote_value = row["lote"].ToString();

                    if (!source_lote_a.ContainsKey(lote_key))
                    {
                        source_lote_a.Add(lote_key, lote_value);
                    }
                }
            }

            if (!source_lote_a.ContainsKey("SIN LOTE"))
            {
                source_lote_a.Add("SIN LOTE", " ");
            }

            source_lote_a.Add("OTRO", "OTRO");

            cbb_lote_a.DataSource = new BindingSource(source_lote_a, null);
            cbb_lote_a.DisplayMember = "Key";
            cbb_lote_a.ValueMember = "Value";

            cbb_lote_a.DroppedDown = true;
            cbb_lote_a.Focus();
        }

        private void cbb_mes_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:
                    cbb_caducidad_a.Enabled = true;
                    cbb_caducidad_a.DroppedDown = true;
                    cbb_caducidad_a.Focus();

                    cbb_mes.Enabled = false;
                    break;
                case 13:
                    cbb_anio.Enabled = true;
                    cbb_anio.Focus();

                    cbb_mes.Enabled = false;
                    cbb_anio.DroppedDown = true;
                    break;
            }
        }

        private void cbb_anio_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    busqueda_lotes_a();
                    cbb_lote_a.Enabled = true;
                    cbb_lote_a.Focus();

                    cbb_anio.Enabled = false;
                    cbb_lote_a.DroppedDown = true;
                    break;
                case 27:
                    cargar_anios();

                    cbb_mes.Enabled = true;
                    cbb_mes.DroppedDown = true;
                    cbb_mes.Focus();
                    cbb_anio.Enabled = false;
                    break;
            }
        }

        private void cbb_lote_KeyDown_de(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                var existencia = dao_articulos.get_cantidad_articulo_existencias(txt_amecop.Text, cbb_caducidad_de.SelectedValue.ToString(), cbb_lote_de.SelectedValue.ToString());
                int cantidad_disponible = existencia.Item1;

                nup_cantidad_de.Enabled = true;
                nup_cantidad_de.Value = 1;
                nup_cantidad_de.Maximum = cantidad_disponible;
                nup_cantidad_de.Minimum = 1;
                nup_cantidad_de.Focus();
                nup_cantidad_de.Select(0, nup_cantidad_de.Text.Length);
                cbb_lote_de.Enabled = false;
            }
            else if (Convert.ToInt32(e.KeyCode) == 27)
            {
                cbb_lote_de.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_lote_de.DisplayMember = "Key";
                cbb_lote_de.ValueMember = "Value";

                cbb_caducidad_de.Enabled = true;
                cbb_caducidad_de.Focus();
                cbb_caducidad_de.DroppedDown = true;
                cbb_lote_de.Enabled = false;
            }
        }

        private void cbb_lote_KeyDown_a(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                if (cbb_lote_a.SelectedValue.Equals("OTRO"))
                {
                    txt_otro_lote.Enabled = true;
                    txt_otro_lote.Focus();
                    cbb_lote_a.Enabled = false;
                }
                else
                {
                    btn_afectar_existencias.Focus();
                }
            }
            else if (Convert.ToInt32(e.KeyCode) == 27)
            {
                cbb_lote_a.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
                cbb_lote_a.DisplayMember = "Key";
                cbb_lote_a.ValueMember = "Value";

                cbb_caducidad_a.Enabled = true;
                cbb_caducidad_a.Focus();
                cbb_caducidad_a.DroppedDown = true;
                cbb_lote_a.Enabled = false;
            }
        }

        private void txt_otro_lote_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_otro_lote.TextLength > 0)
                    {
                        btn_afectar_existencias.Focus();
                        //txt_otro_lote.Enabled = false;
                    }
                    break;
                case 27:
                    if (txt_otro_lote.TextLength > 0)
                    {
                        txt_otro_lote.Text = "";
                    }
                    else
                    {
                        cbb_lote_a.Enabled = true;
                        cbb_lote_a.DroppedDown = true;
                        cbb_lote_a.Focus();
                        txt_otro_lote.Enabled = false;
                    }
                    break;
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
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
                case 13:
                    if (txt_amecop.Text.Trim().Length > 0)
                    {
                        var cantidad_uso = dao_articulos.get_cantidad_articulo_modulos(txt_amecop.Text);

                        bool en_uso = false;
                        string en_uso_por = "";
                        string folios = "";
                        Int64 total_piezas = 0;

                        foreach (var item in cantidad_uso)
                        {
                            if (!item.Key.Equals("total"))
                            {
                                if (item.Value.Item1 > 0)
                                {
                                    en_uso = true;
                                    en_uso_por = item.Key;
                                    folios = item.Value.Item2;
                                    break;
                                }
                            }
                            else
                            {
                                total_piezas = item.Value.Item1;
                            }
                        }

                        if (en_uso)
                        {
                            MessageBox.Show(this, "Este producto se encuentra en uso por el modulo " + en_uso_por + " en los folios: " + folios, "Producto en uso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (total_piezas > 0)
                            {
                                dto_articulo = new DTO_Articulo();
                                dto_articulo = dao_articulos.get_articulo(txt_amecop.Text);
                                articulo_id = (int)dto_articulo.Articulo_id;
                                txt_producto.Text = dto_articulo.Nombre;
                                get_caducidades_de(dto_articulo);
                                txt_amecop.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show(this, "Producto sin existencia, imposible ajustar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    break;
            }
        }

        private void cbb_cantidad_de_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void nup_cantidad_de_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    get_caducidades_a(dto_articulo);
                    nup_cantidad_de.Enabled = false;
                    break;
                case 27:
                    cbb_lote_de.Enabled = true;
                    cbb_lote_de.Focus();
                    cbb_lote_de.DroppedDown = true;
                    nup_cantidad_de.Enabled = false;
                    nup_cantidad_de.Value = 1;
                    break;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_afectar_existencias_Click(object sender, EventArgs e)
        {

            string caducidad_de = cbb_caducidad_de.Text.ToString();

            string lote_de = cbb_lote_de.Text.ToString();

            if (caducidad_de.Trim().Length == 0 || lote_de.Trim().Length == 0)
            {

                MessageBox.Show(this, "Por favor, complementa los campos correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string str_caducidad_de = cbb_caducidad_de.SelectedValue.ToString();
            string str_lote_de = cbb_lote_de.SelectedValue.ToString();
            int cantidad_de = Convert.ToInt32(nup_cantidad_de.Value);
            string str_caducidad_a = "";
            string str_lote_a = "";
            //AGREGADO POR JOEL SANCHEZ
            txt_otro_lote.Text = txt_otro_lote.Text.Trim();

            if (cbb_caducidad_a.Text.Trim().Length == 0 || cbb_lote_a.Text.Trim().Length == 0)
            {

                MessageBox.Show(this, "Por favor, complementa los campos correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (cbb_caducidad_a.SelectedValue.Equals("OTRO"))
            {
                str_caducidad_a = string.Format("{0}-{1}", cbb_anio.SelectedValue.ToString(), cbb_mes.SelectedValue.ToString());
            }
            else
            {
                str_caducidad_a = cbb_caducidad_a.SelectedValue.ToString();
            }

            if (cbb_lote_a.SelectedValue.Equals("OTRO"))
            {
                if (txt_otro_lote.Text.Trim().Length == 0 || txt_otro_lote.TextLength == 0)
                {
                    MessageBox.Show(this, "Es necesario incluir un lote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_otro_lote.Focus();
                    return;
                }
                else
                {
                    str_lote_a = txt_otro_lote.Text.Trim();
                }

            }
            else
            {
                str_lote_a = cbb_lote_a.SelectedValue.ToString().Trim();
            }

            if (str_caducidad_a.Equals(str_caducidad_de) && str_lote_a.Equals(str_lote_de))
            {
                MessageBox.Show(this, "No se puede convertir a la misma caducidad y lote, elija otra caducidad y/o lote", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            afectar_ajustes_existencias(str_caducidad_de, str_lote_de, str_caducidad_a, str_lote_a, cantidad_de);

        }

        public void afectar_ajustes_existencias(string caducidad_de, string lote_de, string caducidad_a, string lote_a, int cantidad_de)
        {
            Login_form login = new Login_form();
            login.ShowDialog();

            if (login.empleado_id != null)
            {
                //DAO_Login dao_login = new DAO_Login();

                //if(dao_login.empleado_es_encargado((long)login.empleado_id))
                //{
                //AGREGADO POR JOEL SANCHEZ
                if (Convert.ToInt32(dao_articulos.get_cantidad_articulo_existencias(txt_amecop.Text, caducidad_de, lote_de).Item1) > 0)
                {


                    DAO_Ajustes_existencias dao_ajuste_existencias = new DAO_Ajustes_existencias();

                    long ajuste_existencia_id = dao_ajuste_existencias.crear_ajuste_existencia(Convert.ToInt64(login.empleado_id));

                    List<DTO_Detallado_ajustes_existencias> detallado_ajuste_existencias = new List<DTO_Detallado_ajustes_existencias>();

                    detallado_ajuste_existencias.Add(new DTO_Detallado_ajustes_existencias()
                    {
                        ajuste_existencia_id = ajuste_existencia_id,
                        articulo_id = articulo_id,
                        caducidad = caducidad_de,
                        lote = lote_de,
                        cantidad = (Convert.ToInt32(dao_articulos.get_cantidad_articulo_existencias(txt_amecop.Text, caducidad_de, lote_de).Item1) - cantidad_de)
                    });

                    detallado_ajuste_existencias.Add(new DTO_Detallado_ajustes_existencias()
                    {
                        ajuste_existencia_id = ajuste_existencia_id,
                        articulo_id = articulo_id,
                        caducidad = caducidad_a,
                        lote = lote_a,
                        cantidad = (Convert.ToInt32(dao_articulos.get_cantidad_articulo_existencias(txt_amecop.Text, caducidad_a, lote_a).Item1) + cantidad_de)
                    });

                    dao_ajuste_existencias.registrar_detallado_ajuste_existencia(detallado_ajuste_existencias);

                    long filas_afectadas = dao_ajuste_existencias.terminar_ajustes_existencias(ajuste_existencia_id, (Int64)login.empleado_id);

                    if (filas_afectadas > 0)
                    {
                        MessageBox.Show(this, "Ajuste de caducidades afectado correctamente", "Ajustes caducidades y lotes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset_form();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un error al intentar ajustar las caducidades y lotes, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show(this, "Producto sin existencia, verifica tu movimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //  }
                //  else
                // {
                //   MessageBox.Show(this, "Solo el encargado puede hacer el ajuste de caducidades y lotes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // }
            }
        }

        public void reset_form()
        {
            txt_amecop.Text = "";
            txt_amecop.Enabled = true;
            txt_amecop.Focus();

            cbb_caducidad_de.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
            cbb_caducidad_de.DisplayMember = "Key";
            cbb_caducidad_de.ValueMember = "Value";

            cbb_caducidad_de.Enabled = false;

            cbb_lote_de.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
            cbb_lote_de.DisplayMember = "Key";
            cbb_lote_de.ValueMember = "Value";
            cbb_lote_de.Enabled = false;

            nup_cantidad_de.Value = 1;
            nup_cantidad_de.Enabled = false;

            cbb_caducidad_a.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
            cbb_caducidad_a.DisplayMember = "Key";
            cbb_caducidad_a.ValueMember = "Value";
            cbb_caducidad_a.Enabled = false;

            cbb_lote_a.DataSource = new BindingSource(new Dictionary<string, string>() { { "", "" } }, null);
            cbb_lote_a.DisplayMember = "Key";
            cbb_lote_a.ValueMember = "Value";

            cbb_lote_a.Enabled = false;

            cargar_mes();
            cbb_mes.Enabled = false;
            cargar_anios();
            cbb_anio.Enabled = false;

            txt_otro_lote.Text = "";
            txt_otro_lote.Enabled = false;

            txt_producto.Text = "";
        }

        private void cbb_lote_de_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_otro_lote_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
