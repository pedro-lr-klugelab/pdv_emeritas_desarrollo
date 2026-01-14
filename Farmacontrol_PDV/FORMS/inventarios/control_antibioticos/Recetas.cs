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
    public partial class Recetas : Form
    {
        public long? control_ab_receta_id = null;
        public List<DTO_detallado_generico> lista_articulos;
        
        
        //public long control_ab_receta_id;
        public Recetas(long? control_ab_receta_id = null )
        {
            InitializeComponent();
            this.control_ab_receta_id = control_ab_receta_id;

            if (control_ab_receta_id > 0)
            {
                get_informacion_receta();
            }
        }

        public Recetas(List<DTO_detallado_generico> articulos)
        {
            InitializeComponent();
            this.lista_articulos = articulos;
            dgv_articulos_receta.DataSource = lista_articulos;
        }

        private void get_informacion_receta()
        {
            DAO_Antibioticos dao_ant = new DAO_Antibioticos();
            DTO_control_ab_recetas dto_temp = dao_ant.get_info_receta((long)control_ab_receta_id);

            txt_cedula.Text = dto_temp.cedula_profesional.ToString();
            txt_doctor.Text = dto_temp.doctor;
            txt_direccion.Text = dto_temp.direccion_consultorio ;
            txt_folio.Text = dto_temp.folio_receta.ToString();
            txt_comentarios.Text = dto_temp.comentarios.ToString();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            DAO_Antibioticos control_antibioticos = new DAO_Antibioticos();
            DTO_control_ab_recetas dto_temp = new DTO_control_ab_recetas();
            long? nullable = null;

            if (control_ab_receta_id == null)
            {
                if ((txt_folio.Text.Length > 0) || (txt_doctor.Text.Length > 0 && txt_direccion.Text.Length > 0))
                {
                    string tipo_receta = "T";
                    string mensaje;
                    mensaje = "Está a punto de guardar los datos capturados. ¿Desea continuar?";
                    if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if(txt_folio.Text.Length > 0)
                        {
                            tipo_receta = "P";
                        }

                        

                        dto_temp.cedula_profesional = (txt_cedula.Text.Trim().Equals("")) ? nullable : Convert.ToInt64(txt_cedula.Text);
                        dto_temp.doctor = txt_doctor.Text;
                        dto_temp.direccion_consultorio = txt_direccion.Text;
                        dto_temp.folio_receta = txt_folio.Text;
                        dto_temp.comentarios = txt_comentarios.Text;
                        dto_temp.tipo_receta = tipo_receta;

                        control_ab_receta_id = control_antibioticos.agregar_datos_receta(dto_temp);
                        if (control_ab_receta_id > 0)
                        {
                            MessageBox.Show(this, "Informacion guardada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(this, "No se puede guardar, debe capturar un folio de receta o el doctor y su dirección", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if(control_antibioticos.actualiza_datos_receta(dto_temp))
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

        private void txt_folio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_doctor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_folio_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_folio.Text.Trim().Length > 0)
                    {
                        txt_doctor.Focus();
                    }
                    break;
                case 27:
                    btn_cancelar.Focus();
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
                        txt_cedula.Focus();
                    }
                    break;
                case 27:
                    txt_folio.Focus();
                    break;
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
                        txt_direccion.Focus();
                    }
                break;
                case 27:
                    txt_doctor.Focus();
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
                    txt_cedula.Focus();
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

        private void txt_comentarios_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_direccion_TextChanged(object sender, EventArgs e)
        {

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
                    txt_direccion.Focus();
                    break;
            }
        }
    }
}
