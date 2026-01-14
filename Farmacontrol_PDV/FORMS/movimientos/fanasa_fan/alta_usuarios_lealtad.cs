using Farmacontrol_PDV.CLASSES;
using Farmacontrol_PDV.DTO;
using Farmacontrol_PDV.HELPERS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Farmacontrol_PDV.FORMS.movimientos.fanasa_fan
{
    public partial class alta_usuarios_lealtad : Form
    {
        public string ip_servidor = ConfigurationManager.AppSettings["server_beneficios"];

        public alta_usuarios_lealtad()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void alta_usuarios_lealtad_Load(object sender, EventArgs e)
        {
            cbxEstado.SelectedIndex = 0;
            txtNombres.Focus();
        }

        #region KEYPRESS controles
        private void txtTelCasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
      

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            if (this.datosvalidos())
            {

                if (MessageBox.Show("¿Datos correctos?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   //ESTABLECE LA CONEXION CON EL SERVIDOR DE FARMACIA PARA LA CONSULTA D
                   //GUARDAR LOS DATOS EN VARIABLES PARA PODER ENVIARLO 
                    string nombre_cliente, apellido_pat, apellido_mat, fecha_nacimiento , sexo ,correo, telCasa,celular,calle,num_exterior,num_interior,colonia,municipio,ciudad,estado,pais,codigo_postal = "";


                    nombre_cliente = txtNombres.Text.ToString();
                    apellido_pat = txtApellidoPaterno.Text.ToString();
                    apellido_mat = txtApellidoMaterno.Text.ToString();
                    correo       = txtCorreo.Text.ToString();
                    fecha_nacimiento = dtNacimiento.Text.ToString();
                   // fecha_nacimiento = string.Format(dtNacimiento.Value.ToString(), "yyyy-MM-dd");

                    telCasa = txtTelCasa.Text.ToString();
                    celular = txtCelular.Text.ToString();

                    if (rdMasculino.Checked)
                        sexo = "Masculino";
                    else
                        sexo = "Femenino";

                    calle = txtCalle.Text.ToString();
                    num_exterior = txtNumEx.Text.ToString();
                    num_interior = txtNumInt.Text.ToString();
                    colonia = txtColonia.Text.ToString();
                    municipio = txtMunicipio.Text.ToString();
                    estado = cbxEstado.Text.ToString();
                    pais = txtPais.Text.ToString();
                    codigo_postal = txtCodigoPostal.Text.ToString();
                    ciudad = txtCiudad.Text.ToString();

                    DTO_WebServiceSoyFan val = new DTO_WebServiceSoyFan();

                    Rest_parameters parametros = new Rest_parameters();
                    parametros.Add("name", nombre_cliente);
                    parametros.Add("lastName1", apellido_pat);
                    parametros.Add("lastName2", apellido_mat);
                    parametros.Add("birthDate", fecha_nacimiento);
                    parametros.Add("email", correo);
                    parametros.Add("sex", sexo);
                    parametros.Add( "phone", celular != "" ? celular : telCasa );
                    parametros.Add( "street" , calle );
                    parametros.Add("externalNum", num_exterior);

                    parametros.Add("city", ciudad);
                    parametros.Add("state", estado);
                    parametros.Add("zipCode", codigo_postal);


                    val = Rest_helper.enlace_webservice_soyfan<DTO_WebServiceSoyFan>("webservice/gestionar_contactos", parametros, ip_servidor);

                    if (val.status)
                    {
                        
                        MessageBox.Show(this, val.mensaje, "Cliente registrado correctamente, ID CLIENTE : "+val.idcliente, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, val.mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }     
                    

                    

                }
                else
                    btnGuardarCliente.Focus();
            }
            else
                 MessageBox.Show(this, "Datos incompletos , revisa!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     
        }


        public bool datosvalidos()
        { 
            bool data_validos = true;

            if (txtNombres.Text.ToString() == "")
                return false;

            if (txtApellidoPaterno.Text.ToString() == "")
                return false;

            if (txtApellidoMaterno.Text.ToString() == "")
                return false;

            if (txtCorreo.Text.ToString() == "")
                return false;

            if (txtTelCasa.Text.ToString() == "" && txtCelular.Text.ToString() == "")
                return false;


            if (txtCalle.Text.ToString() == "")
                return false;

            if (txtNumEx.Text.ToString() == "" && txtNumInt.Text.ToString() == "")
                return false;

            if (txtColonia.Text.ToString() == "")
                return false;

            if (txtMunicipio.Text.ToString() == "")
                return false;

            if (txtCiudad.Text.ToString() == "")
                return false;

            if (cbxEstado.SelectedIndex == -1)
                return false;

            if (txtPais.Text.ToString() == "")
                return false;

            if (txtCodigoPostal.Text.ToString() == "")
                return false;


            return data_validos;
        
        }

        private void txtPais_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
