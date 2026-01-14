using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.DAO;
using Farmacontrol_PDV.FORMS.comunes;
using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.CLASSES.PRINT;
using Farmacontrol_PDV.HELPERS;

namespace Farmacontrol_PDV.FORMS.inventarios.control_antibioticos
{
    public partial class Recetas_new : Form
    {

        public long? control_ab_receta_id = null;
        public List<DTO_detallado_generico> lista_articulos;

        public Recetas_new()
        {
            InitializeComponent();
        }

        public Recetas_new(long? control_ab_receta_id = null )
        {
            InitializeComponent();
            this.control_ab_receta_id = control_ab_receta_id;

            if (control_ab_receta_id > 0)
            {
                get_informacion_receta();
            }
            else
            {
                
                txt_cedula.Enabled = true;
                txt_doctor.Enabled = true;
                txt_direccion.Enabled = true;
                txt_comentarios.Enabled = true;
                txt_folio.Visible = true;
                lbl_folio.Visible = true;
            
            
            }
        }

        public Recetas_new(List<DTO_detallado_generico> articulos)
        {
            InitializeComponent();
            this.lista_articulos = articulos;
            dgv_articulos_receta.DataSource = lista_articulos;

            
            txt_cedula.Enabled = true;
            txt_doctor.Enabled = true;
            txt_direccion.Enabled = true;
            txt_comentarios.Enabled = true;
            txt_folio.Visible = true;
            lbl_folio.Visible = true;

        }

        private void get_informacion_receta()
        {
            chk_receta_fisica.Enabled = false;
           

            DAO_Antibioticos dao_ant = new DAO_Antibioticos();
            DTO_control_ab_recetas dto_temp = dao_ant.get_info_receta((long)control_ab_receta_id);

            txt_cedula.Text = dto_temp.cedula_profesional.ToString();
            txt_doctor.Text = dto_temp.doctor;
            txt_direccion.Text = dto_temp.direccion_consultorio;
            txt_folio.Text = dto_temp.folio_receta.ToString();
            txt_comentarios.Text = dto_temp.comentarios.ToString();

            
            txt_cedula.Enabled = true;
            txt_doctor.Enabled = true;
            txt_direccion.Enabled = true;
            txt_comentarios.Enabled = true;
            txt_folio.Visible = true;
            lbl_folio.Visible = true;

        }

        private void chk_receta_fisica_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_receta_fisica.Checked == true)
            {
                /*
                txt_cedula.Enabled = false;
                txt_doctor.Enabled = false;
                txt_direccion.Enabled = false;
                txt_comentarios.Enabled = false;
                 * */
                txt_folio.Visible = true;
                lbl_folio.Visible = true;
                txt_folio.Enabled = false;
                //checar un folio de autoincrementable

                DAO_Antibioticos dao_ant = new DAO_Antibioticos();
                long folio_receta = dao_ant.get_folio_receta_total();

                txt_folio.Text = folio_receta.ToString();

               
            }
            else
            {
                /*
                txt_cedula.Enabled = true;
                txt_doctor.Enabled = true;
                txt_direccion.Enabled = true;
                txt_comentarios.Enabled = true;
                */
                txt_folio.Visible = true;
                lbl_folio.Visible = true;
                txt_folio.Enabled = false;
                txt_folio.Text = "";

            }

        }

        private void txt_comentario2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_comentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_doctor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            DAO_Antibioticos control_antibioticos = new DAO_Antibioticos();
            DTO_control_ab_recetas dto_temp = new DTO_control_ab_recetas();
            long? nullable = null;

            if (control_ab_receta_id == null)
            {
                string mensaje;
                mensaje = "Está a punto de guardar los datos capturados. ¿Desea continuar?";
                if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (chk_receta_fisica.Checked != true)
                    {
                        dto_temp.cedula_profesional = (txt_cedula.Text.Trim().Equals("")) ? nullable : Convert.ToInt64(txt_cedula.Text);
                        dto_temp.doctor = txt_doctor.Text;
                        dto_temp.direccion_consultorio = txt_direccion.Text;
                        dto_temp.folio_receta = txt_folio.Text;
                        dto_temp.comentarios = txt_comentarios.Text;
                        dto_temp.tipo_receta = "P";
                        control_ab_receta_id = control_antibioticos.agregar_datos_receta(dto_temp);
                    }
                    else
                    {


                        dto_temp.cedula_profesional = (txt_cedula.Text.Trim().Equals("")) ? nullable : Convert.ToInt64(txt_cedula.Text);
                        dto_temp.doctor = txt_doctor.Text;
                        dto_temp.direccion_consultorio = txt_direccion.Text;
                        dto_temp.folio_receta = txt_folio.Text;
                        dto_temp.comentarios = txt_comentarios.Text;
                        dto_temp.tipo_receta = "T";
                       
                        control_ab_receta_id = control_antibioticos.agregar_datos_receta(dto_temp);
                    }

                    if (control_ab_receta_id > 0)
                    {
                        long folio;
                            
                        folio = control_antibioticos.get_folio_receta(control_ab_receta_id);

                        if(folio > 0)
                        {
                            mensaje = "\nSe guardó con el folio: " + folio.ToString();
                        }
                        else
                        {
                            mensaje = "";
                        }

                        MessageBox.Show(this, "Informacion guardada con éxito" + mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.control_ab_receta_id = control_ab_receta_id;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ocurrio un error al actualizar los datos, comuníquese a sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txt_folio.Focus();
                }
            }
            else
            {

                dto_temp.control_antibioticos_receta_id = (long)control_ab_receta_id;
                dto_temp.cedula_profesional = (txt_cedula.Text.Trim().Equals("")) ? nullable : Convert.ToInt64(txt_cedula.Text);
                dto_temp.doctor = txt_doctor.Text;
                dto_temp.direccion_consultorio = txt_direccion.Text;
                dto_temp.folio_receta = txt_folio.Text;
                dto_temp.comentarios = txt_comentarios.Text;

                if (control_antibioticos.actualiza_datos_receta(dto_temp))
                {
                    MessageBox.Show(this, "Informacion guardada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.control_ab_receta_id = control_ab_receta_id;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Ocurrio un error al actualizar los datos, comuníquese a sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void txt_cedula_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_cedula.Text.Trim().Length > 0)
                    {
                        txt_doctor.Focus();
                    }
                    break;
                case 27:
                    chk_receta_fisica.Focus();
                    break;
            }
        }

        private void txt_doctor_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_doctor.Text.Trim().Length > 0)
                    {
                        txt_direccion.Focus();
                    }
                    break;
                case 27:
                    txt_cedula.Focus();
                    break;
            }
        }

        private void txt_comentarios_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_comentarios.Text.Trim().Length > 0)
                    {
                        btn_guardar.Focus();
                    }
                    break;
                case 27:
                    btn_cancelar.Focus();
                    break;
            }
        }

        private void txt_direccion_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_direccion.Text.Trim().Length > 0)
                    {
                        txt_comentarios.Focus();
                    }
                    break;
                case 27:
                    txt_doctor.Focus();
                    break;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            string mensaje;
            mensaje = "Está a punto de salir de la captura de recetas.\n¿Desea continuar?";
            if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txt_cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }


        }
    }
}
