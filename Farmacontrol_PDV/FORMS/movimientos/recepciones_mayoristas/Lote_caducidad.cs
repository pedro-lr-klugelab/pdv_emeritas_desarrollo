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

namespace Farmacontrol_PDV.FORMS.movimientos.recepciones_mayoristas
{
    public partial class Lote_caducidad : Form
    {
       public string lotes;
       public string cad;
       public long detallado_id;


        public Lote_caducidad( long detallado_entrada_id )
        {
            InitializeComponent();

            detallado_id  = detallado_entrada_id;
     
        }


        private void LlenaMeses()
        { 
            
            BindingSource source = new BindingSource(new Dictionary<string, string>(){
                {"00","00-00"},
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

            cmbxMeses.DataSource = source;
            cmbxMeses.DisplayMember = "Key";
            cmbxMeses.ValueMember = "Value";
            cmbxMeses.Focus();  
            cmbxMeses.DroppedDown = true;
            cmbxMeses.Enabled = true;
           
        
        }

        private void LlenarAnios()
        {

            Dictionary<int, int> anios = new Dictionary<int, int>();

            int anio_actual = Convert.ToInt32(Convert.ToDateTime(Misc_helper.fecha()).Year);
          //  int anio_anterior = anio_actual -1 ;

            anios.Add(0000, 0000);
            anios.Add(anio_actual, anio_actual);

            for (int count = (anio_actual + 1); count < anio_actual + 10; count++)
            {
                anios.Add(count, count);
            }

            BindingSource source = new BindingSource(anios, null);
     
            cbxAnio.DataSource = source;
            cbxAnio.DisplayMember = "Key";
            cbxAnio.ValueMember = "Value";
             
        }

        private void Lote_caducidad_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            LlenaMeses();
            LlenarAnios();

            txtLotes.MaxLength = 4;
           
        }

        private void cmbxMeses_Enter(object sender, EventArgs e)
        {
      
            
        }

        private void cmbxMeses_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:
                    this.Close();
                    
                break;
                case 13:
                   
                    cbxAnio.DroppedDown = true;
                    cbxAnio.Focus();
                    cbxAnio.Enabled = true;
                    cmbxMeses.Enabled = false;
                break;
            }
        }

        private void cbxAnio_KeyDown(object sender, KeyEventArgs e)
        {

            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:

                    cmbxMeses.Focus();
                    cmbxMeses.Enabled = true;
                    cbxAnio.Enabled = false;
                    cmbxMeses.DroppedDown = true;

                 break;
                case 13:
                    txtLotes.Enabled = true;
                    txtLotes.Focus();
                    cbxAnio.Enabled = false;
                 break;
            }



        }

        private void txtLotes_KeyDown(object sender, KeyEventArgs e)
        {
            var keycode = Convert.ToInt32(e.KeyCode);

            switch (keycode)
            {
                case 27:

                    cbxAnio.Focus();
                    cbxAnio.Enabled = true;
                    txtLotes.Enabled = false;
                    cbxAnio.DroppedDown = true;

                    break;
                case 13:
                    this.set_lotes_caducidades();
                    //this.Close();
                    //MessageBox.Show(this, detallado_id.ToString(), "Valor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }

        }

        private void btnAceptar_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void btnAceptar_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void btnAceptar_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.set_lotes_caducidades();
        
        }


        public void set_lotes_caducidades()
        {
            long det_id = this.detallado_id;
         
            this.lotes = txtLotes.Text.ToString();
            this.cad = !(cmbxMeses.SelectedValue.Equals("00")) ? string.Format("{0}-{1}", cbxAnio.SelectedValue.ToString(), cmbxMeses.SelectedValue.ToString()) : cmbxMeses.SelectedValue.ToString();

            MessageBox.Show(this, detallado_id.ToString(), "Valor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(this, lotes.ToString(), "Lotes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(this, cad.ToString(), "Caducidad", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtLotes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }


    
    }
}
