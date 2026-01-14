using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
    public partial class Recepciones_mayoristas_principal : Form
    {
        DAO_Entradas dao_entradas = new DAO_Entradas();
        DTO_Entradas dto_entradas = new DTO_Entradas();
        DAO_Mayoristas dao_mayoristas = new DAO_Mayoristas();
        DAO_Articulos dao_articulos = new DAO_Articulos();
        private int? articulo_id = null;

        public Recepciones_mayoristas_principal()
        {
            InitializeComponent();
        }

        private void Recepciones_mayoristas_principal_Load(object sender, EventArgs e)
        {
            lbl_mensaje_bloqueo.Parent = dgv_entradas;
            long entrada_id = dao_entradas.get_entrada_fin();

            if (entrada_id > 0)
            {
                try
                {
                    dto_entradas = dao_entradas.get_informacion_entrada(entrada_id);
                    rellenar_informacion_entradas();
                }
                catch (Exception ex)
                {
                    Log_error.log(ex);
                }
            }
            else
            {
                btn_siguiente.Enabled = false;
                btn_atras.Enabled = false;
                btn_fin.Enabled = false;
                btn_inicio.Enabled = false;

                btn_terminar.Enabled = false;
                btn_imprimir.Enabled = false;
                btn_buscar.Enabled = false;
                txt_amecop.Enabled = false;

                //dgv_entradas.Enabled = true;

                lbl_mensaje_bloqueo.Text = "No se encontraron Entradas anteriores";
                lbl_mensaje_bloqueo.Visible = true;
            }
        }

        public void rellenar_informacion_entradas()
        {
            limpiar_informacion();


            btn_siguiente.Enabled = true;
            btn_atras.Enabled = true;
            btn_fin.Enabled = true;
            btn_inicio.Enabled = true;


            btn_terminar.Enabled = true;
            btn_imprimir.Enabled = true;
            btn_buscar.Enabled = true;

            dgv_entradas.DataSource = dao_entradas.get_detallado_entradas(dto_entradas.entrada_id);




            dgv_entradas.Refresh();

            txt_folio_busqueda_entradas.Text = dto_entradas.entrada_id.ToString();
            txt_empleado_captura.Text = dto_entradas.nombre_empleado_captura;
            txt_empleado_termina.Text = dto_entradas.nombre_empleado_termina;
            txt_fecha_creado.Text = (dto_entradas.fecha_creado != null) ? Misc_helper.fecha(dto_entradas.fecha_creado.ToString(), "LEGIBLE") : " - ";
            txt_fecha_terminado.Text = (dto_entradas.fecha_terminado != null) ? Misc_helper.fecha(dto_entradas.fecha_terminado.ToString(), "LEGIBLE") : " - ";
            txt_comentarios.Text = dto_entradas.comentarios;

            if (dto_entradas.fecha_terminado.Equals(null))
            {
                txt_estado.BackColor = Color.Green;
                txt_estado.Text = "ABIERTO";
            }
            else
            {
                txt_estado.BackColor = Color.Red;
                txt_estado.Text = "TERMINADO";
            }

            get_tipos_entradas();
            get_mayoristas();
            validar_bloqueo();
            get_totales();

            dgv_entradas.ClearSelection();
            txt_amecop.Focus();
        }

        public void get_tipos_entradas()
        {
            var tipo_entradas = Misc_helper.get_enums("farmacontrol_local.entradas", "tipo_entrada");

            Dictionary<string, string> contenido = new Dictionary<string, string>();
            contenido.Add("-", "* Selecciona un tipo * ");

            foreach (string tipo in tipo_entradas)
            {
                contenido.Add(tipo, tipo);
            }

            BindingSource source = new BindingSource(contenido, null);

            cbb_tipo.DataSource = source;
            cbb_tipo.DisplayMember = "Value";
            cbb_tipo.ValueMember = "Key";

            if (!dto_entradas.tipo_entrada.Equals(""))
            {
                cbb_tipo.SelectedValue = dto_entradas.tipo_entrada;
            }
        }

        public void get_mayoristas()
        {
            cbb_mayorista.DataSource = dao_mayoristas.get_all_mayoristas();
            cbb_mayorista.ValueMember = "Key";
            cbb_mayorista.DisplayMember = "Value";


            if (dto_entradas.mayorista_id > 0)
            {
                cbb_mayorista.SelectedValue = Convert.ToInt32(dto_entradas.mayorista_id);
            }
        }

        public void validar_bloqueo()
        {
            string[] factura_split = dto_entradas.factura.Split('_');

            if (factura_split[0].Equals("SF"))
            {
                if (factura_split[1].Length == 36)
                {
                    txt_factura.Enabled = false;
                    txt_factura.Text = "SIN FACTURA";
                    chb_sin_factura.Checked = true;
                }
                else
                {
                    txt_factura.Text = dto_entradas.factura;
                    txt_factura.Enabled = false;
                    chb_sin_factura.Checked = false;
                }
            }
            else
            {
                txt_factura.Enabled = false;
                txt_factura.Text = dto_entradas.factura;
                chb_sin_factura.Checked = false;
            }

            if (dto_entradas.fecha_terminado.Equals(null))
            {
                cbb_tipo.Enabled = true;
                cbb_mayorista.Enabled = true;

                txt_factura.Enabled = true;

                txt_comentarios.Enabled = true;
                txt_amecop.Enabled = true;
                txt_amecop.Text = "";
                txt_amecop.Focus();
                chb_sin_factura.Enabled = true;

                if (dto_entradas.terminal_id > 0)
                {
                    if (dto_entradas.terminal_id == (int)Misc_helper.get_terminal_id())
                    {
                        lbl_mensaje_bloqueo.Visible = false;
                        lbl_mensaje_bloqueo.Text = "";
                    }
                    else
                    {
                        //dgv_entradas.Enabled = true;
                        lbl_mensaje_bloqueo.Visible = true;
                        lbl_mensaje_bloqueo.Text = "ESTA ENTRADA PERTENECE A " + Misc_helper.get_nombre_terminal((int)dto_entradas.terminal_id);

                        txt_folio_busqueda_entradas.Focus();
                        txt_folio_busqueda_entradas.Select(0, txt_folio_busqueda_entradas.Text.Length);
                        cbb_tipo.Enabled = false;
                        cbb_mayorista.Enabled = false;
                        txt_factura.Enabled = false;
                        txt_comentarios.Enabled = false;
                        txt_amecop.Enabled = false;
                        chb_sin_factura.Enabled = false;
                    }
                }
                else
                {
                    //dgv_entradas.Enabled = true;
                    lbl_mensaje_bloqueo.Visible = true;
                    lbl_mensaje_bloqueo.Text = "ENTRADA SIN TERMINAL";

                    txt_folio_busqueda_entradas.Focus();
                    txt_folio_busqueda_entradas.Select(0, txt_folio_busqueda_entradas.Text.Length);
                    cbb_tipo.Enabled = false;
                    cbb_mayorista.Enabled = false;
                    txt_factura.Enabled = false;
                    txt_comentarios.Enabled = false;
                    txt_amecop.Enabled = false;
                    chb_sin_factura.Enabled = false;
                }
            }
            else
            {
                //dgv_entradas.Enabled = false;
                lbl_mensaje_bloqueo.Visible = false;
                lbl_mensaje_bloqueo.Text = "";

                txt_folio_busqueda_entradas.Focus();
                txt_folio_busqueda_entradas.Select(0, txt_folio_busqueda_entradas.Text.Length);
                cbb_tipo.Enabled = false;
                cbb_mayorista.Enabled = false;
                txt_factura.Enabled = false;
                txt_comentarios.Enabled = false;
                txt_amecop.Enabled = false;
                chb_sin_factura.Enabled = false;
            }

            /*if (dto_entradas.fecha_terminado.Equals(null))
			{
				txt_factura.Enabled = true;
				chb_sin_factura.Enabled = true;
			}
			else
			{
				txt_factura.Enabled = false;
				chb_sin_factura.Enabled = false;
			}*/
        }

        public void get_totales()
        {
            long numero_piezas = 0;
            decimal total_general = 0;

            if (dgv_entradas.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_entradas.Rows)
                {
                    numero_piezas += Convert.ToInt64(row.Cells["c_cantidad"].Value);
                    total_general += Convert.ToDecimal(row.Cells["c_total"].Value);
                }
            }

            txt_numero_piezas.Text = "" + numero_piezas;
            txt_total_general.Text = string.Format(@"{0:C2}", total_general);
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            Login_form login = new Login_form("crear_entradas");
            login.ShowDialog();

            if (login.empleado_id != null)
            {
                DialogResult dr = MessageBox.Show(this, "¿Esta seguro de querer crear una nueva entrada?", "Crear Entradas", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.Yes)
                {
                    var nueva_entrada = dao_entradas.crear_entrada((long)login.empleado_id);

                    if (nueva_entrada.entrada_id > 0)
                    {
                        dto_entradas = nueva_entrada;
                        rellenar_informacion_entradas();

                        cbb_mayorista.Focus();
                        cbb_mayorista.DroppedDown = true;
                        txt_factura.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un error al intentar crear la entrada, notifique a su administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_inicio_Click(object sender, EventArgs e)
        {
            long entrada_id = dao_entradas.get_entrada_inicio();

            if (entrada_id > 0)
            {
                dto_entradas = dao_entradas.get_informacion_entrada(entrada_id);
                rellenar_informacion_entradas();
            }
        }

        private void btn_atras_Click(object sender, EventArgs e)
        {
            long entrada_id = dao_entradas.get_entrada_atras(dto_entradas.entrada_id);

            if (entrada_id > 0)
            {
                dto_entradas = dao_entradas.get_informacion_entrada(entrada_id);
            }

            rellenar_informacion_entradas();
        }

        private void btn_siguiente_Click(object sender, EventArgs e)
        {
            long entrada_id = dao_entradas.get_entrada_siguiente(dto_entradas.entrada_id);

            if (entrada_id > 0)
            {
                dto_entradas = dao_entradas.get_informacion_entrada(entrada_id);
            }

            rellenar_informacion_entradas();
        }

        private void btn_fin_Click(object sender, EventArgs e)
        {
            long entrada_id = dao_entradas.get_entrada_fin();

            if (entrada_id > 0)
            {
                dto_entradas = dao_entradas.get_informacion_entrada(entrada_id);
                rellenar_informacion_entradas();
            }
        }

        private void txt_folio_busqueda_traspaso_Enter(object sender, EventArgs e)
        {
            dgv_entradas.ClearSelection();
        }

        private void txt_amecop_Enter(object sender, EventArgs e)
        {
            dgv_entradas.ClearSelection();
        }

        public void busqueda_caducidades()
        {
            cbb_caducidad.DataSource = null;
            cbb_caducidad.Enabled = true;

            var result_caducidades = dao_articulos.get_caducidades(articulo_id);
            Dictionary<string, string> caducidades = new Dictionary<string, string>();

            foreach (DataRow row in result_caducidades.Rows)
            {
                string caducidad = row["caducidad"].ToString();

                if (!caducidad.Equals("0000-00-00") && !caducidad.Equals("0000-00-00 00:00:00"))
                {
                    caducidades.Add(Misc_helper.fecha(caducidad, "CADUCIDAD") + " (" + row["existencia"] + ")", caducidad);
                }
            }

            caducidades.Add("SIN CAD", "0000-00-00");
            caducidades.Add("OTRO", "OTRO");

            BindingSource source = new BindingSource(caducidades, null);

            cbb_caducidad.DataSource = source;
            cbb_caducidad.DisplayMember = "Key";
            cbb_caducidad.ValueMember = "Value";

            cbb_caducidad.Focus();
            cbb_caducidad.DroppedDown = true;
            txt_amecop.Enabled = false;
        }

        public void busqueda_lotes()
        {
            cbb_anio.Enabled = false;
            cbb_lote.Enabled = true;

            var result_lotes = dao_articulos.get_lotes((int)articulo_id, cbb_caducidad.SelectedValue.ToString());

            Dictionary<string, string> lotes = new Dictionary<string, string>();

            foreach (DataRow row in result_lotes.Rows)
            {
                string lote = row["lote"].ToString();

                if (!lote.Trim().Equals(""))
                {
                    lotes.Add(lote, lote);
                }
            }

            lotes.Add("SIN LOTE", " ");
            lotes.Add("OTRO", "OTRO");

            BindingSource source = new BindingSource(lotes, null);

            cbb_lote.DataSource = source;
            cbb_lote.DisplayMember = "Key";
            cbb_lote.ValueMember = "Value";

            cbb_lote.Focus();
            cbb_lote.DroppedDown = true;
            cbb_caducidad.Enabled = false;
        }

        public void busqueda_producto()
        {
            try
            {
                DTO_Articulo articulo = dao_articulos.get_articulo(txt_amecop.Text);
                articulo_id = articulo.Articulo_id;

                if (articulo.Articulo_id != null)
                {
                    txt_producto.Text = articulo.Nombre;
                    busqueda_caducidades();
                }
                else
                {
                    txt_amecop.Text = "";
                    MessageBox.Show(this, "Producto no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Log_error.log(ex);
            }
        }

        public void cargar_mes()
        {
            BindingSource source = new BindingSource(new Dictionary<string, string>(){
                {"ENE","01-01"},
                {"FEB","02-01"},
                {"MAR","03-01"},
                {"ABR","04-01"},
                {"MAY","05-01"},
                {"JUN","06-01"},
                {"JUL","07-01"},
                {"AGO","08-01"},
                {"SEP","09-01"},
                {"OCT","10-01"},
                {"NOV","11-01"},
                {"DIC","12-01"}
            }, null);

            cbb_mes.DataSource = source;
            cbb_mes.DisplayMember = "Key";
            cbb_mes.ValueMember = "Value";
            cbb_mes.Enabled = true;
            cbb_mes.Focus();
            cbb_mes.DroppedDown = true;
        }

        public void cargar_anio()
        {
            Dictionary<int, int> anios = new Dictionary<int, int>();

            int anio_actual = Convert.ToInt32(Convert.ToDateTime(Misc_helper.fecha()).Year);
            int anio_anterior = anio_actual - 1;

            anios.Add(anio_anterior, anio_anterior);
            anios.Add(anio_actual, anio_actual);

            for (int count = (anio_actual + 1); count < anio_actual + 10; count++)
            {
                anios.Add(count, count);
            }

            BindingSource source = new BindingSource(anios, null);
            cbb_anio.DataSource = source;
            cbb_anio.DisplayMember = "Key";
            cbb_anio.ValueMember = "Value";
            cbb_anio.Enabled = true;
            cbb_anio.Focus();
            cbb_anio.DroppedDown = true;

            cbb_mes.Enabled = false;
            cbb_caducidad.Enabled = false;
        }

        private void txt_amecop_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_amecop.Text.Trim().Length > 0)
                    {
                        busqueda_producto();
                    }
                    break;
                case 40:
                    if (dgv_entradas.Rows.Count > 0)
                    {
                        //dgv_entradas.Enabled = true;
                        dgv_entradas.Focus();
                        dgv_entradas.CurrentCell = dgv_entradas.Rows[0].Cells["c_amecop"];
                        dgv_entradas.Rows[0].Selected = true;
                    }
                    break;
            }
        }

        private void cbb_caducidad_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (cbb_caducidad.SelectedValue.Equals("OTRO"))
                    {
                        cargar_mes();
                    }
                    else
                    {
                        busqueda_lotes();
                    }
                    break;
                case 27:
                    articulo_id = null;
                    txt_producto.Text = "";
                    txt_amecop.Text = "";
                    txt_amecop.Enabled = true;
                    txt_amecop.Focus();
                    cbb_caducidad.DataSource = null;
                    cbb_caducidad.Enabled = false;
                    break;
            }
        }

        private void cbb_lote_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (cbb_lote.SelectedValue.Equals("OTRO"))
                    {
                        txt_otro_lote.Enabled = true;
                        txt_otro_lote.Focus();
                        cbb_lote.Enabled = false;
                    }
                    else
                    {
                        txt_cantidad.Enabled = true;
                        txt_cantidad.Value = 1;
                        txt_cantidad.Select(0, txt_cantidad.Text.Length);
                        txt_cantidad.Focus();
                        cbb_lote.Enabled = false;
                    }
                    break;
                case 27:
                    cbb_caducidad.Enabled = true;
                    cbb_caducidad.Focus();
                    cbb_caducidad.DroppedDown = true;
                    cbb_lote.DataSource = null;
                    cbb_lote.Enabled = false;
                    break;
            }
        }

        private void cbb_mes_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    cargar_anio();
                    break;
                case 27:
                    cbb_caducidad.Enabled = true;
                    cbb_caducidad.Focus();
                    cbb_caducidad.DroppedDown = true;
                    cbb_mes.DataSource = null;
                    break;
            }
        }

        private void cbb_anio_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    busqueda_lotes();
                    break;
                case 27:
                    cbb_mes.Enabled = true;
                    cbb_mes.Focus();
                    cbb_mes.DroppedDown = true;
                    cbb_anio.DataSource = null;
                    cbb_anio.Enabled = false;
                    break;
            }
        }

        private void txt_otro_lote_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    txt_cantidad.Text = "1";
                    txt_cantidad.Enabled = true;
                    txt_cantidad.Focus();
                    txt_cantidad.Select(0, txt_cantidad.Text.Length);
                    txt_otro_lote.Enabled = false;
                    break;
                case 27:
                    cbb_lote.Enabled = true;
                    cbb_lote.Focus();
                    cbb_lote.DroppedDown = true;
                    txt_otro_lote.Text = "";
                    txt_otro_lote.Enabled = false;
                    break;
            }
        }

        public void limpiar_informacion()
        {
            txt_amecop.Enabled = true;
            txt_cantidad.Value = 1;
            txt_amecop.Text = "";
            txt_otro_lote.Text = "";
            cbb_lote.DataSource = null;
            cbb_anio.DataSource = null;
            cbb_mes.DataSource = null;
            cbb_caducidad.DataSource = null;
            txt_producto.Text = "";

            txt_cantidad.Enabled = false;

            articulo_id = null;

            txt_amecop.Focus();
        }

        private void registrar_producto()
        {
            // Caducidad elegida
            string caducidadStr =
                (cbb_caducidad.SelectedValue.Equals("OTRO"))
                    ? string.Format("{0}-{1}", cbb_anio.SelectedValue.ToString(), cbb_mes.SelectedValue.ToString()) // "yyyy-MM-dd"
                    : cbb_caducidad.SelectedValue.ToString();

            // Si es "SIN CAD" no advertimos
            bool esSinCad = caducidadStr == "0000-00-00";

            // Advertir caducidad corta
            if (!esSinCad && EsCaducidadCorta(caducidadStr, out int meses, out DateTime fechaCad))
            {
                var dr = MessageBox.Show(this,
                    $"Este artículo tiene caducidad corta: {fechaCad:dd/MMM/yyyy} (~{meses} mes(es) restante(s)).\n\n¿Deseas continuar con la captura?",
                    "Caducidad corta",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (dr != DialogResult.Yes)
                {
                    // Usuario decide no registrar
                    return;
                }
            }

            // === Inserta como ya lo haces ===
            if (dao_articulos.es_empaque((long)articulo_id))
            {
                string caducidad = caducidadStr;
                string lote = (cbb_lote.SelectedValue.Equals("OTRO")) ? txt_otro_lote.Text : cbb_lote.SelectedValue.ToString();
                int cantidad = Convert.ToInt32(txt_cantidad.Value);

                Articulos_empaques articulos_empaques = new Articulos_empaques((long)articulo_id, dto_entradas.entrada_id, caducidad, lote, cantidad);
                articulos_empaques.ShowDialog();

                if (articulos_empaques.registrar_empaque)
                {
                    var insert_id = dao_entradas.insertar_producto(
                        dto_entradas.entrada_id,
                        txt_amecop.Text,
                        caducidad,
                        lote,
                        Convert.ToInt64(txt_cantidad.Value)
                    );
                }
            }
            else
            {
                var insert_id = dao_entradas.insertar_producto(
                    dto_entradas.entrada_id,
                    txt_amecop.Text,
                    caducidadStr,
                    (cbb_lote.SelectedValue.Equals("OTRO")) ? txt_otro_lote.Text : cbb_lote.SelectedValue.ToString(),
                    Convert.ToInt64(txt_cantidad.Value)
                );
            }

            validar_oferta();
            limpiar_informacion();
        }


        private void txt_cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_cantidad.Value > 0)
                    {
                        registrar_producto();
                    }
                    break;
                case 27:
                    limpiar_informacion();
                    break;
            }
        }

        public void validar_oferta()
        {
            dao_entradas.actualizar_oferta((long)dto_entradas.mayorista_id, dto_entradas.entrada_id);
            dgv_entradas.DataSource = dao_entradas.get_detallado_entradas(dto_entradas.entrada_id);
            get_totales();
            txt_amecop.Focus();
        }

        private void cbb_mayorista_DropDownClosed(object sender, EventArgs e)
        {
            if (cbb_mayorista.Items.Count > 0)
            {
                long mayorista_id = Convert.ToInt64(cbb_mayorista.SelectedValue);
                long? nullable = null;
                dao_entradas.set_mayorista_id(dto_entradas.entrada_id, (mayorista_id > 0) ? mayorista_id : nullable);
                dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                validar_oferta();

                if (txt_factura.Enabled)
                {
                    txt_factura.Focus();
                }
            }
        }

        private void cbb_tipo_DropDownClosed(object sender, EventArgs e)
        {
            if (cbb_tipo.Items.Count > 0)
            {
                dao_entradas.set_tipo(dto_entradas.entrada_id, cbb_tipo.SelectedValue.ToString());
                txt_amecop.Focus();
            }
        }

        private void dgv_entradas_Leave(object sender, EventArgs e)
        {
            dgv_entradas.ClearSelection();
            //dgv_entradas.Enabled = false;
        }

        private void dgv_entradas_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 67:
                    if (e.Control)
                    {
                        Clipboard.SetText(dgv_entradas.SelectedRows[0].Cells["c_amecop"].Value.ToString());
                    }
                    break;
                case 27:
                    txt_amecop.Focus();
                    break;
                case 46:
                    if (dto_entradas.terminal_id != null && dto_entradas.terminal_id == Misc_helper.get_terminal_id() && dto_entradas.fecha_terminado == null)
                    {
                        long detallado_entrada_id = Convert.ToInt32(dgv_entradas.SelectedRows[0].Cells["detallado_entrada_id"].Value);
                        if (dao_entradas.es_producto_empaque(detallado_entrada_id))
                        {
                            DialogResult dr = MessageBox.Show(this, "Este producto se fue registrado como parte de un paquete, si lo elimina todos los productos dentro del paquete tambien seran eliminados, ¿Desea continuar?", "Eliminar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                dao_entradas.eliminar_detalado_entrada_paquete(dto_entradas.entrada_id, detallado_entrada_id);
                            }
                        }
                        else
                        {
                            dao_entradas.eliminar_detallado_entrada(detallado_entrada_id);
                        }


                        validar_oferta();
                    }
                    break;
            }
        }

        private void btn_terminar_Click(object sender, EventArgs e)

        {
            if (dto_entradas.entrada_id > 0)
            {
                if (dto_entradas.fecha_terminado.Equals(null) && dto_entradas.terminal_id != null && dto_entradas.terminal_id == Misc_helper.get_terminal_id())
                {
                    long mayorista_id = Convert.ToInt64(cbb_mayorista.SelectedValue);
                    string tipo_entrada = cbb_tipo.SelectedValue.ToString();
                    string factura = txt_factura.Text;
                    string comentarios_evalua = txt_comentarios.Text.ToUpper();


                    bool es_almacen = false;

                    if (comentarios_evalua.Contains("ALMACEN"))
                        es_almacen = true;


                    if (mayorista_id > 0)
                    {
                        if (!tipo_entrada.Equals("-"))
                        {

                            if (comentarios_evalua.Trim() != "")
                            {

                                if (txt_factura.Text.Trim().Length > 0)
                                {


                                    Login_form login = new Login_form("terminar_entradas");
                                    login.ShowDialog();

                                    if (login.empleado_id != null)
                                    {
                                        DialogResult dr = MessageBox.Show(this, "¿Estas seguro de querer terminar esta entrada?", "Terminar Entrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                        if (dr == DialogResult.Yes)
                                        {

                                            ///REVISA AL ALMACEN 

                                            if (es_almacen)
                                            {
                                                Valida_almacen valida = new Valida_almacen();

                                                Rest_parameters parametros = new Rest_parameters();
                                                parametros.Add("traspaso_id", factura);

                                                valida = Rest_helper.make_request_valida_almacen<Valida_almacen>("almacen_traspasos/almacen_sucursal", parametros);

                                                if (valida.status)
                                                {

                                                    ///LLENAR LA PAGINA
                                                    Termina_almacen termina_alm = new Termina_almacen();
                                                    parametros = new Rest_parameters();
                                                    parametros.Add("traspaso_id", factura);
                                                    parametros.Add("nombre_empleado", login.empleado_nombre);
                                                    parametros.Add("folio_sucursal", (long)dto_entradas.entrada_id);
                                                    parametros.Add("piezas_recibidas", txt_numero_piezas.Text);

                                                    termina_alm = Rest_helper.make_request_valida_folio_almacen<Termina_almacen>("almacen_traspasos/make_request_valida_folio_almacen", parametros);

                                                    if (termina_alm.status)
                                                    {
                                                        MessageBox.Show(this, "Entrada de almacen registrada correctamente", "Entradas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                        DTO_Validacion val = dao_entradas.terminar_entrada((long)dto_entradas.entrada_id, (long)dto_entradas.mayorista_id, (Convert.ToBoolean(chb_sin_factura.CheckState)) ? dto_entradas.factura : txt_factura.Text, (long)login.empleado_id);

                                                        if (val.status)
                                                        {
                                                            MessageBox.Show(this, val.informacion, "Entradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                                                            rellenar_informacion_entradas();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }

                                                    }
                                                    else
                                                    {

                                                        MessageBox.Show(this, "Error al registrar la compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                    }
                                                    /////fin de llenar la pagina  

                                                }
                                                else
                                                {
                                                    MessageBox.Show(this, "Error en la validacion del folio de captura y/o mayorista,revisa  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }

                                            }
                                            else
                                            {

                                                DTO_Validacion val = dao_entradas.terminar_entrada((long)dto_entradas.entrada_id, (long)dto_entradas.mayorista_id, (Convert.ToBoolean(chb_sin_factura.CheckState)) ? dto_entradas.factura : txt_factura.Text, (long)login.empleado_id);

                                                if (val.status)
                                                {
                                                    MessageBox.Show(this, val.informacion, "Entradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                                                    rellenar_informacion_entradas();
                                                }
                                                else
                                                {
                                                    MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }




                                            }


                                            ////////////////////////////////////////////////////



                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "Es necesario el folio de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {

                                MessageBox.Show(this, "Es necesario escribir algun comentario de la entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }


                        }
                        else
                        {
                            MessageBox.Show(this, "Es necesario seleccionar el tipo de la entrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Es necesario seleccionar un mayorista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txt_factura_Leave(object sender, EventArgs e)
        {
            dao_entradas.set_factura(dto_entradas.entrada_id, txt_factura.Text);
            dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
        }

        private void txt_comentarios_Leave(object sender, EventArgs e)
        {
            dao_entradas.set_comentarios(dto_entradas.entrada_id, txt_comentarios.Text);
            dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
        }

        private void chb_sin_factura_CheckedChanged(object sender, EventArgs e)
        {
            if (dto_entradas.fecha_terminado == null)
            {
                bool seleccionado = Convert.ToBoolean(chb_sin_factura.CheckState);

                if (seleccionado)
                {
                    dao_entradas.set_factura(dto_entradas.entrada_id, "SF_" + Misc_helper.uuid());
                    txt_factura.Enabled = false;
                    txt_factura.Text = "SIN FACTURA";
                    dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                }
                else
                {
                    dao_entradas.set_factura(dto_entradas.entrada_id, "");
                    txt_factura.Text = "";
                    txt_factura.Enabled = true;
                    dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                }
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {

        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            if (!dto_entradas.fecha_terminado.Equals(null))
            {
                Entradas entradas = new Entradas();
                entradas.construccion_ticket(dto_entradas.entrada_id, true);
                entradas.print();
            }
        }

        private void desasociar_terminal_Click(object sender, EventArgs e)
        {
            if (dto_entradas.entrada_id > 0)
            {
                if (dto_entradas.fecha_terminado.Equals(null))
                {
                    if (dto_entradas.terminal_id > 0)
                    {
                        Login_form login = new Login_form();
                        login.ShowDialog();

                        if (login.empleado_id != null)
                        {
                            if (((int)login.empleado_id == dto_entradas.empleado_id) || dto_entradas.empleado_id == 1)
                            {
                                var val = dao_entradas.desasociar_terminal(dto_entradas.entrada_id, (long)login.empleado_id);

                                if (val.status)
                                {
                                    MessageBox.Show(this, val.informacion, "Desasociar Terminal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                                    rellenar_informacion_entradas();
                                }
                                else
                                {
                                    MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Solo quien creo la entrada puede desasociarla de su terminal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Esta entrada no tiene terminal asociada, imposible desasociar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void asociar_terminal_Click(object sender, EventArgs e)
        {
            if (dto_entradas.entrada_id > 0)
            {
                if (dto_entradas.fecha_terminado == null)
                {
                    if (dto_entradas.terminal_id == 0)
                    {
                        Login_form login = new Login_form();
                        login.ShowDialog();

                        if (login.empleado_id != null)
                        {
                            var val = dao_entradas.asociar_terminal((long)dto_entradas.entrada_id, (long)login.empleado_id);

                            if (val.status)
                            {
                                MessageBox.Show(this, val.informacion, "Asociar Terminal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                                rellenar_informacion_entradas();
                            }
                            else
                            {
                                MessageBox.Show(this, val.informacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Esta sucursal ya cuenta con una terminal asignada!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_escanear_Click(object sender, EventArgs e)
        {
            if (dto_entradas.entrada_id > 0)
            {
                Control_escaneo control_escaneo = new Control_escaneo("scans_compras");
                control_escaneo.ShowDialog();

                if (control_escaneo.status)
                {
                    DAO_Entradas dao_entradas = new DAO_Entradas();
                    dao_entradas.registrar_nombre_archivo(dto_entradas.entrada_id, control_escaneo.nombre_uuid);
                }
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dto_entradas.entrada_id > 0)
            {
                dto_entradas = dao_entradas.get_informacion_entrada(dto_entradas.entrada_id);
                rellenar_informacion_entradas();
            }
        }

        private void txt_folio_busqueda_traspaso_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (dto_entradas.entrada_id > 0)
                    {
                        if (dao_entradas.existe_entrada_id(Convert.ToInt64(txt_folio_busqueda_entradas.Value)))
                        {
                            dto_entradas = dao_entradas.get_informacion_entrada(Convert.ToInt64(txt_folio_busqueda_entradas.Value));
                            rellenar_informacion_entradas();
                        }
                        else
                        {
                            MessageBox.Show(this, "Folio de recepcion de mayorista no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txt_folio_busqueda_entradas.Focus();
                            txt_folio_busqueda_entradas.Select(0, txt_folio_busqueda_entradas.Text.Length);
                        }
                    }
                    break;
            }

        }

        private void txt_folio_busqueda_traspaso_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_amecop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_otro_lote_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void chb_sin_factura_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void txt_factura_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_factura.Text.Trim().Length > 0)
                    {
                        cbb_tipo.Focus();
                        cbb_tipo.DroppedDown = true;
                    }
                    break;
            }
        }

        private void cbb_caducidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_amecop_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_entradas_DoubleClick(object sender, EventArgs e)
        {
            if (dto_entradas.entrada_id > 0)
            {
                if (dto_entradas.fecha_terminado.Equals(null))
                {

                    long detallado_entrada_id = Convert.ToInt32(dgv_entradas.SelectedRows[0].Cells["detallado_entrada_id"].Value);

                    Lote_caducidad lote_cad = new Lote_caducidad(detallado_entrada_id);
                    lote_cad.ShowDialog();






                }

            }
        }



        private bool TryParseCaducidad(string cad, out DateTime dt)
        {
            dt = DateTime.MinValue;
            if (string.IsNullOrWhiteSpace(cad)) return false;

            cad = cad.Trim();

            // "Sin caducidad" en tus datos
            if (cad == "0000-00-00" || cad.StartsWith("0000-00")) return false;

            // Quitar hora si viene "yyyy-MM-dd HH:mm:ss"
            int spaceIdx = cad.IndexOf(' ');
            string baseCad = (spaceIdx > 0) ? cad.Substring(0, spaceIdx) : cad;

            // Completar "yyyy-MM" -> "yyyy-MM-01"
            if (System.Text.RegularExpressions.Regex.IsMatch(baseCad, @"^\d{4}-\d{2}$"))
                baseCad += "-01";

            return DateTime.TryParse(baseCad, out dt);
        }

        // ¿Caducidad corta (<= 6 meses desde hoy)?
        private bool EsCaducidadCorta(string cad, out int mesesRestantes, out DateTime fechaCad)
        {
            mesesRestantes = 0;
            fechaCad = DateTime.MinValue;

            if (!TryParseCaducidad(cad, out fechaCad)) return false;

            DateTime limite = DateTime.Today.AddMonths(6);

            // meses aprox. restantes
            int m = (fechaCad.Year - DateTime.Today.Year) * 12 + (fechaCad.Month - DateTime.Today.Month);
            if (fechaCad.Day < DateTime.Today.Day) m -= 1;
            mesesRestantes = m < 0 ? 0 : m;

            return fechaCad <= limite;
        }









    }
}
