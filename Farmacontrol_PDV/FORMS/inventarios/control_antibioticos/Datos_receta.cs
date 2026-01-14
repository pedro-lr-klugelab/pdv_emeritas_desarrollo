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
    public partial class Datos_receta : Form
    {
        /*
        private string doctor;
        private string cedula;
        private string receta;
        private long id_control_antibioticos;
        private long movimiento;
        private long articulo_id;
        */
        public  DTO_Control_AB_receta dto_tmp =  null;

        public Datos_receta(DTO_Control_AB_receta dto_receta)
        {
            this.dto_tmp = dto_receta;
            /*
            this.id_control_antibioticos    = dto_receta.id_control_antibioticos;
            this.articulo_id                = dto_receta.articulo_id;
            this.movimiento                 = dto_receta.movimiento;
            this.doctor                     = dto_receta.doctor;
            this.cedula                     = dto_receta.cedula;
            this.receta                     = dto_receta.receta;
            */
            InitializeComponent();
        }

        private void Datos_receta_Load(object sender, EventArgs e)
        {
            txt_doctor.Text = dto_tmp.doctor;
            txt_cedula.Text = dto_tmp.cedula;
            txt_receta.Text = dto_tmp.receta;
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            /*
            string mensaje;
            mensaje = "Está a punto de guardar los datos capturados. ¿Desea continuar?";
            if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                DTO_Control_AB_receta dto_temp = new DTO_Control_AB_receta();
                dto_temp.control_antibiotico_id = this.dto_tmp.control_antibiotico_id;
                dto_temp.articulo_id = this.dto_tmp.articulo_id;
                dto_temp.tipo_movimiento = this.dto_tmp.tipo_movimiento;
                dto_temp.doctor = txt_doctor.Text;
                dto_temp.cedula = txt_cedula.Text;
                dto_temp.receta = txt_receta.Text;

                DAO_Antibioticos control_antibioticos = new DAO_Antibioticos();
                if (control_antibioticos.actualizar_datos_receta(dto_temp))
                {
                    MessageBox.Show(this, "Informacion guardada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dto_tmp = dto_temp;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Ocurrio un error al actualizar los datos, comuníquese a sistemas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else 
            {
                txt_doctor.Focus();
            }*/
        }

        private void txt_doctor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_receta_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txt_doctor_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_cedula_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_cedula.Text.Trim().Length > 0)
                    {
                        txt_receta.Focus();
                    }
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
            }
        }

        private void txt_receta_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 13:
                    if (txt_receta.Text.Trim().Length > 0)
                    {
                        btn_agregar.Focus();
                    }
                    break;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            string mensaje;
            mensaje = "Está a punto de salir sin guardar los datos. ¿Desea continuar?";
            if (MessageBox.Show(this, mensaje, "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                txt_doctor.Focus();
            }
        }
    }
}
